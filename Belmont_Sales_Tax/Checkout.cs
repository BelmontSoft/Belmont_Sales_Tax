/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    Checkout
/// | Summary:  Static class representing the checkout
/// |           process. Calculates taxes and total
/// |           costs for all items in a shopping cart
/// |           and outputs the results.
/// +=================================================+
/// </summary>
using System;
using System.Collections.Generic;
using System.Text;

namespace Belmont_Sales_Tax
{
    public static class Checkout
    {
        // Define tax rates.
        const double salesTaxRate = .10;
        const double importTaxRate = .05;

        /// <summary>
        /// Calculate taxes and total costs for all items in the shopping cart. Output results.
        /// </summary>
        /// <param name="cart">Cart containing items ready for checkout.</param>
        public static void Begin(ShoppingCart cart)
        {
            // Define placeholder variables for use in checkout process.
            double salesTaxTotal = 0;
            double totalCost = 0;
            int quantity = 0;
            double previousItemCategoryCost = 0;
            double itemTax = 0;
            double itemCost = 0;
            String previousItemCategory = null;

            // Sort cart contents for cleaner receipt printing.
            SortedDictionary<Item, int> cartContents = new SortedDictionary<Item, int>(cart.GetContents(), new ItemCustomComparer());

            StringBuilder receiptLine = new StringBuilder();
            StringBuilder finishedReceiptLine = new StringBuilder();

            // Exit if cart is empty.
            if (cartContents.Count <= 0)
            {
                return;
            }

            // Iterate through every unique item in the cart. Print Item values to receipt.
            foreach (KeyValuePair<Item, int> itemSet in cartContents)
            {
                if (itemSet.Value >= 0)
                {
                    if (previousItemCategory == null)
                    {
                        // First Item in cart. Initialize variables accordingly.
                        quantity = itemSet.Value;
                        
                        // Get Item's tax info, add to running total.
                        itemTax = CalculateItemTaxes(itemSet.Key);
                        salesTaxTotal += (itemTax * quantity);
                        itemCost = itemSet.Key.GetPrice() + itemTax;
                        totalCost = (itemCost * quantity);

                        // Record this Item's name and cost in case the following item is of the same type; makes for cleaner printing.
                        previousItemCategoryCost = (itemCost * quantity);
                        previousItemCategory = itemSet.Key.GetName();

                        // If Item quantity is greater than 1, create (quantity @ price) bracket on the current receipt line.
                        if (itemSet.Value > 1)
                        {
                            receiptLine.AppendFormat("({0} @ {1:0.00})", quantity, itemCost);
                        }
                    }
                    else if (itemSet.Key.GetName() == previousItemCategory)
                    {
                        // This Item has the same name as the previous Item, just a different price; Update and append info accordingly.
                        if (receiptLine.ToString().EndsWith(")"))
                        {
                            // 2+ sets of quantity-value pairs already exist for this Item, so skip creating the initial (quantity @ price) bracket.
                            quantity = itemSet.Value;

                            // Get Item's tax info, add to running total.
                            itemTax = CalculateItemTaxes(itemSet.Key);
                            salesTaxTotal += (itemTax * quantity);
                            itemCost = itemSet.Key.GetPrice() + itemTax;
                            totalCost += (itemCost * quantity);

                            // Record this Item's cost in case the following item is of the same type; makes for cleaner printing.
                            previousItemCategoryCost += (itemCost * quantity);

                            // Extend the receipt line's original set of quantity-value pairs to include the new pair.
                            receiptLine = receiptLine.Remove(receiptLine.Length - 1, 1);
                            receiptLine.AppendFormat(", {0} @ {1:0.00})", quantity, itemCost);
                        }
                        else
                        {
                            // Append a bracket for storing the previous item's (quantity @ price).
                            receiptLine.AppendFormat("({0} @ {1:0.00})", quantity, itemCost); 

                            // Get the new Item's quantity.
                            quantity = itemSet.Value;

                            // Get Item's tax info, add to running total.
                            itemTax = CalculateItemTaxes(itemSet.Key);
                            salesTaxTotal += (itemTax * quantity);
                            itemCost = itemSet.Key.GetPrice() + itemTax;
                            totalCost += (itemCost * quantity);

                            // Record this Item's cost in case the following item is of the same type; makes for cleaner printing.
                            previousItemCategoryCost += (itemCost * quantity);

                            // Extend the receipt line's original set of quantity-value pairs to include the new pair.
                            receiptLine = receiptLine.Remove(receiptLine.Length - 1, 1);
                            receiptLine.AppendFormat(", {0} @ {1:0.00})", quantity, itemCost);
                        }
                    }
                    else if (itemSet.Value > 1)
                    {
                        // Print out any running totals from the previous Item.
                        finishedReceiptLine = new StringBuilder();
                        finishedReceiptLine.AppendFormat("{0}: {1:0.00} {2}", previousItemCategory, previousItemCategoryCost, receiptLine);
                        finishedReceiptLine[0] = char.ToUpper(finishedReceiptLine[0]);
                        Console.WriteLine(finishedReceiptLine.ToString());

                        quantity = itemSet.Value;

                        // Get Item's tax info, add to running total.
                        itemTax = CalculateItemTaxes(itemSet.Key);
                        salesTaxTotal += (itemTax * quantity);
                        itemCost = itemSet.Key.GetPrice() + itemTax;
                        totalCost += (itemCost * quantity);

                        // Item quantity is greater than 1; add the (quantity @ price) bracket to the new receipt line.
                        receiptLine = new StringBuilder();
                        receiptLine.AppendFormat("({0} @ {1:0.00})", quantity, itemCost);

                        // Record this Item's name and cost in case the following item is of the same type; makes for cleaner printing.
                        previousItemCategoryCost = (itemCost * quantity);
                        previousItemCategory = itemSet.Key.GetName();
                    }
                    else
                    {
                        // Print out any running totals from the previous Item.
                        finishedReceiptLine = new StringBuilder();
                        finishedReceiptLine.AppendFormat("{0}: {1:0.00} {2}", previousItemCategory, previousItemCategoryCost, receiptLine);
                        finishedReceiptLine[0] = char.ToUpper(finishedReceiptLine[0]);
                        Console.WriteLine(finishedReceiptLine.ToString());

                        receiptLine = new StringBuilder();

                        quantity = itemSet.Value;

                        // Get Item's tax info, add to running total.
                        itemTax = CalculateItemTaxes(itemSet.Key);
                        salesTaxTotal += (itemTax * quantity);
                        itemCost = itemSet.Key.GetPrice() + itemTax;
                        totalCost += (itemCost * quantity);

                        // Record this Item's name and cost in case the following item is of the same type; makes for cleaner printing.
                        previousItemCategoryCost = (itemCost * quantity);
                        previousItemCategory = itemSet.Key.GetName();
                    }
                }
            }

            // All Items accounted for; print out any remaining info from previous Item.
            finishedReceiptLine = new StringBuilder();
            finishedReceiptLine.AppendFormat("{0}: {1:0.00} {2}", previousItemCategory, previousItemCategoryCost, receiptLine);
            finishedReceiptLine[0] = char.ToUpper(finishedReceiptLine[0]);
            Console.WriteLine(finishedReceiptLine.ToString());

            // Print final Sales Taxes total.
            finishedReceiptLine = new StringBuilder();
            finishedReceiptLine.AppendFormat("Sales Taxes: {0:0.00}", RoundCents(salesTaxTotal));
            Console.WriteLine(finishedReceiptLine.ToString());

            // Print final total cost.
            finishedReceiptLine = new StringBuilder();
            finishedReceiptLine.AppendFormat("Total: {0:0.00}", RoundCents(totalCost));
            Console.WriteLine(finishedReceiptLine.ToString());
        }

        /// <summary>
        /// Calculates an Item's Basic Sales Tax and the Imported Tax. 
        /// </summary>
        /// <param name="item">Item to tax.</param>
        /// <returns>Total tax for the Item, rounded up to the nearest 0.05.</returns>
        public static double CalculateItemTaxes(Item item)
        {
            double basicSalesTax = 0;
            double importTax = 0;
            double itemPrice = item.GetPrice();
            
            if (!item.IsBasicTaxExempt())
            {
                basicSalesTax = (itemPrice * salesTaxRate);
            }

            if (item.IsImported())
            {
                importTax = (itemPrice * importTaxRate);
            }

            // Return the sum of the two tax values rounded up to the nearest 0.05.
            return Math.Round((Math.Round((basicSalesTax + importTax) * 20, MidpointRounding.AwayFromZero) / 20), 1);

        }

        /// <summary>
        /// Rounds a double to two decimal places.
        /// </summary>
        /// <param name="value">Value to round.</param>
        /// <returns>Original value rounded to two decimal places.</returns>
        public static double RoundCents(double value)
        {
            return value = Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Gets the default basic sales tax rate.
        /// </summary>
        /// <returns>Basic sales tax rate.</returns>
        public static double GetSalesTaxRate()
        {
            return salesTaxRate;
        }

        /// <summary>
        /// Gets the default import tax rate.
        /// </summary>
        /// <returns>Import tax rate.</returns>
        public static double GetImportTaxRate()
        {
            return importTaxRate;
        }
    }    
}
