/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    ShoppingCart
/// | Summary:  Represents a shopping cart capable of
/// |           holding a variety of items.
/// |           Stores contained Items in a dictionary
/// |           of Item-quantity pairs.
/// +=================================================+
/// </summary>
using System;
using System.Collections.Generic;

namespace Belmont_Sales_Tax
{
    public class ShoppingCart
    {
        private Dictionary<Item, int> contents;
        private ShoppingCartHelper helper;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ShoppingCart()
        {
            contents = new Dictionary<Item, int>();
            helper = new ShoppingCartHelper();
        }

        /// <summary>
        /// Adds the Item described in the input parameter to the cart.
        /// </summary>
        /// <param name="rawItemString">String describing Item to add. Expects pattern "[QUANTITY] [ITEM CONTAINER] of [ITEM NAME] at [PRICE]"</param>
        public void AddToCart(String rawItemString)
        {
            // Strip the quantity out of the provided String.
            int quantity = helper.ProcessItemQuantity(ref rawItemString);
            // Convert the remaining String characters into an Item.
            Item newItem = helper.ProcessItem(rawItemString);
            // Add the results to the cart.
            AddToCart(newItem, quantity);
        }

        /// <summary>
        /// Adds the Item into the cart's Item dictionary if it's new. Increases Item quantity if it's not unique.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void AddToCart(Item item)
        {
            if (this.contents.ContainsKey(item))
            {
                contents[item] += 1;
            }
            else
            {
                contents.Add(item, 1);
            }
        }

        /// <summary>
        /// Adds the Item into the cart's Item dictionary if it's new. Increases Item quantity if it's not unique.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <param name="quantity">Quantity to add.</param>
        public void AddToCart(Item item, int quantity)
        {
            if (this.contents.ContainsKey(item))
            {
                contents[item] += quantity;
            }
            else
            {
                contents.Add(item, quantity);
            }
        }

        /// <summary>
        /// Gets the cart's contents.
        /// </summary>
        /// <returns>Item-Quantity dictionary representing cart contents.</returns>
        public Dictionary<Item, int> GetContents()
        {
            return this.contents;
        }
    }
}
