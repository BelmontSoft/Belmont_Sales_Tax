/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    ItemCustomComparer
/// | Summary:  Custom IComparer for Item class.
/// |           Order of priority is:
/// |           1. Alphabetical order of names
/// |           2. Size of the price value
/// |           3. Tax exempt > Not tax exempt
/// |           4. Imported > Not imported
/// +=================================================+
/// </summary>
using System;
using System.Collections.Generic;

namespace Belmont_Sales_Tax
{
    public class ItemCustomComparer : IComparer<Item>
    {
        /// <summary>
        /// Custom Compare method for Item class implementations.
        /// </summary>
        /// <param name="x">First Item to compare.</param>
        /// <param name="y">Second Item to compare.</param>
        /// <returns></returns>
        public int Compare(Item x, Item y)
        {
            // Prioritize the Item whose name is first alphabetically.
            if (x.GetName() != y.GetName())
            {
                return String.Compare(x.GetName(), y.GetName());
            }
            else if (x.GetPrice() != y.GetPrice())
            {
                // Names are equal; prioritize price values instead.
                if (x.GetPrice() < y.GetPrice())
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else if (x.IsBasicTaxExempt() != y.IsBasicTaxExempt())
            {
                // Names and prices are equal; prioritize basic tax exemption status instead.
                if (y.IsBasicTaxExempt())
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else if (x.IsImported() != y.IsImported())
            {
                // Names, prices and tax exemption status are equal; prioritize import status.
                if (y.IsImported())
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
            else
            {
                // Everything is equal.
                return 0;
            }
        }
    }
}
