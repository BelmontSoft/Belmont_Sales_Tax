/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    ShoppingCartHelper
/// | Summary:  Helper class for ShoppingCart.
/// |           Holds a list of common Item names that 
/// |           are exempt from basic taxes, as well as
/// |           a list of common Item containers in
/// |           both singular/plural forms.
/// +=================================================+
/// </summary>
using System;
using System.Text.RegularExpressions;

namespace Belmont_Sales_Tax
{
    public class ShoppingCartHelper
    {
        private ExemptionList exemptionList;
        private ContainerPluralsList containerPluralsList;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShoppingCartHelper()
        {
            exemptionList = new ExemptionList();
            containerPluralsList = new ContainerPluralsList();
        }

        /// <summary>
        /// Strips the quantity out of a String describing an Item.
        /// </summary>
        /// <param name="rawItemString">String (passed by reference!) describing an Item. Quantity will be removed from it.</param>
        /// <returns>Quantity described in the String.</returns>
        public int ProcessItemQuantity(ref String rawItemString)
        {
            // If no quantity indicator is discovered, default to 1.
            int quantity = 1;

            // Prime a Regex pattern to discover whether quantity is stored in the target String.
            String quantityPattern = @"^(\d+)";
            Regex regex = new Regex(quantityPattern);
            Match match = regex.Match(rawItemString);

            if (match.Success)
            {
                // Quantity was found: store into a variable and strip the related text out of the target String.
                quantity = Int32.Parse(match.Value);
                rawItemString = regex.Replace(rawItemString, "");
                rawItemString = rawItemString.Trim();
            }

            return quantity;
        }

        /// <summary>
        /// Converts a String describing an Item into an Item object.
        /// </summary>
        /// <param name="rawItemString">String describing an Item.</param>
        /// <returns>Item based on the provided description.</returns>
        public Item ProcessItem(String rawItemString)
        {
            rawItemString = rawItemString.Trim();
            rawItemString = rawItemString.ToLower();

            String containerName = null;
            double price = 0;
            bool imported = false;
            bool isTaxExempt = false;

            // Declare patterns that will be used for Regex matching.
            String importedPattern = @"\b(imported)";
            String priceAtPattern = @"(?=\s(at|@)\s).*";
            String pricePattern = @"(?<=(\sat|\s@)\s).*";
            String itemPluralPattern = @"(s)(?=\sof\s)";
            String itemContainerPattern = @".+?(?=of)";

            // Evaluate whether the item container is a plural (i.e., boxes of cupcake mix)
            Regex regex = new Regex(itemPluralPattern);
            Match match = regex.Match(rawItemString);
            if (match.Success)
            {
                // Plural container discovered; isolate the name of the container.
                Regex regexP = new Regex(itemContainerPattern);
                match = regexP.Match(rawItemString);

                containerName = match.Value;
                containerName = containerName.Trim();

                // If the container's singular form is stored on record, substitute it for the plural 
                if (containerPluralsList.Contains(containerName))
                {
                    rawItemString = regexP.Replace(rawItemString, "", 1);
                    rawItemString = String.Format("{0} {1}", containerPluralsList.GetSingular(containerName), rawItemString);
                }
            }

            // Evaluate whether the item is imported
            regex = new Regex(importedPattern);
            match = regex.Match(rawItemString);
            if (match.Success)
            {
                imported = true;
            }

            // Evalute the item's price
            regex = new Regex(pricePattern);
            match = regex.Match(rawItemString);
            if (match.Success)
            {
                // Price discovered; store into a variable and strip related text out of the target String.
                price = Double.Parse(match.Value);
                if (price >= 0)
                {
                    regex = new Regex(priceAtPattern);
                    rawItemString = regex.Replace(rawItemString, "");
                    rawItemString = rawItemString.Trim();
                }
            }

            // If remaining String contents are present in the exemption list, record tax exemption status
            if (exemptionList.Contains(rawItemString))
            {
                isTaxExempt = true;
            }

            // Create and return Item object using gathered details.
            Item newItem = new Item(rawItemString, price, isTaxExempt, imported);
            return newItem;
        }
    }
}
