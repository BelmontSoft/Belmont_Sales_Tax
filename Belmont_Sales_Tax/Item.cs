/// <summary>
/// +=================================================+
/// | Author:   Jeremiah Belmont
/// | Class:    Item
/// | Summary:  Represents an item for sale.
/// |           Records the item's name, price, whether
/// |           it is exempt from basic taxes, and
/// |           whether it is imported.
/// +=================================================+
/// </summary>
using System;

namespace Belmont_Sales_Tax
{
    public class Item
    {
        private String name;
        private double price; 
        private bool isBasicTaxExempt;
        private bool isImported;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the item. Is required.</param>
        /// <param name="price">The amount the item costs. Must be non-negative.</param>
        public Item(String name, double price)
        {
            if (name.Length != 0 && price >= 0)
            {
                this.name = name;
                this.price = RoundPrice(price);
                this.isBasicTaxExempt = false;
                this.isImported = false;
            }
            else
            {
                throw new ArgumentException("Invalid parameters!");
            }
        }
        /// <summary>
        /// Constructor that includes a parameter for initializing tax exemption status.
        /// </summary>
        /// <param name="name">The name of the item. Is required.</param>
        /// <param name="price">The amount the item costs. Must be non-negative.</param>
        /// <param name="isBasicTaxExempt">Whether the item is exempt from basic taxes.</param>
        public Item(String name, double price, bool isBasicTaxExempt)
        {
            if (name.Length != 0 && price >= 0)
            {
                this.name = name;
                this.price = RoundPrice(price);
                this.isBasicTaxExempt = isBasicTaxExempt;
                this.isImported = false;
            }
            else
            {
                throw new ArgumentException("Invalid parameters!");
            }
        }

        /// <summary>
        /// Constructor that includes a parameter for initializing tax exemption status.
        /// </summary>
        /// <param name="name">The name of the item. Is required.</param>
        /// <param name="price">The amount the item costs. Must be non-negative.</param>
        /// <param name="isBasicTaxExempt">Whether the item is exempt from basic taxes.</param>
        /// <param name="isImported">Whether the item is imported.</param>
        public Item(String name, double price, bool isBasicTaxExempt, bool isImported)
        {
            if (name.Length != 0 && price >= 0)
            {
                this.name = name;
                this.price = RoundPrice(price);
                this.isBasicTaxExempt = isBasicTaxExempt;
                this.isImported = isImported;
            }
            else
            {
                throw new ArgumentException("Invalid parameters!");
            }
        }

        /// <summary>
        /// Directs Equals method to Item's equivalent of it.
        /// </summary>
        /// <param name="obj">Object to compare against.</param>
        /// <returns>True if object is equivalent.</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Item);
        }

        /// <summary>
        /// Custom Equals method.
        /// </summary>
        /// <param name="item">Item to compare against.</param>
        /// <returns>True if object is equivalent.</returns>
        public bool Equals(Item item)
        {
            // If parameter is null, return false.
            if (Object.ReferenceEquals(item, null))
            {
                return false;
            }

            // Optimization for a common success case.
            if (Object.ReferenceEquals(this, item))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != item.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return this == item;
        }

        /// <summary>
        /// Overloaded == operator.
        /// </summary>
        /// <param name="lhs">Lefthand Item to evaluate.</param>
        /// <param name="rhs">Righthand item to evaluate.</param>
        /// <returns>Whether Items are equivalent.</returns>
        public static bool operator ==(Item lhs, Item rhs)
        {
            return (lhs.GetName() == rhs.GetName() 
                && lhs.GetPrice() == rhs.GetPrice()
                && lhs.IsBasicTaxExempt() == rhs.IsBasicTaxExempt()
                && lhs.IsImported() == rhs.IsImported());
        }

        /// <summary>
        /// Overloaded != operator.
        /// </summary>
        /// <param name="lhs">Lefthand Item to evaluate.</param>
        /// <param name="rhs">Righthand item to evaluate.</param>
        /// <returns>Whether Items are non-equivalent.</returns>
        public static bool operator !=(Item lhs, Item rhs)
        {
            return (lhs.GetName() != rhs.GetName() 
                || lhs.GetPrice() != rhs.GetPrice()
                || lhs.IsBasicTaxExempt() != rhs.IsBasicTaxExempt()
                || lhs.IsImported() != rhs.IsImported());
        }

        /// <summary>
        /// Custom hash code retriever.
        /// </summary>
        /// <returns>Hash based off Item's name and price.</returns>
        public override int GetHashCode()
        {
            return this.name.GetHashCode() + this.price.GetHashCode();
        }

        /// <summary>
        /// Sets Item name.
        /// </summary>
        /// <param name="name">Name to set. Must not be null.</param>
        public void SetName(String name)
        {
            if (name.Length != 0)
            {
                this.name = name;
            }
            else
            {
                throw new ArgumentException("Name must be at least one character in length!");
            }
        }

        /// <summary>
        /// Sets item price.
        /// </summary>
        /// <param name="price">Price to set. Must be non-negative.</param>
        public void SetPrice(double price)
        {
            if (price >= 0)
            {
                this.price = RoundPrice(price);
            }
            else
            {
                throw new ArgumentException("Price must be non-negative!");
            }
        }

        /// <summary>
        /// Sets basic tax exemption status.
        /// </summary>
        /// <param name="exemptionStatus">Status to set.</param>
        public void SetTaxExemptionStatus(bool exemptionStatus)
        {
            this.isBasicTaxExempt = exemptionStatus;
        }

        /// <summary>
        /// Sets imported status.
        /// </summary>
        /// <param name="importedStatus">Status to set.</param>
        public void SetImportedStatus(bool importedStatus)
        {
            this.isImported = importedStatus;
        }

        /// <summary>
        /// Gets Item name.
        /// </summary>
        /// <returns>Name of item.</returns>
        public String GetName()
        {
            return this.name;
        }

        /// <summary>
        /// Gets Item price.
        /// </summary>
        /// <returns>Price of item.</returns>
        public double GetPrice()
        {
            return this.price;
        }

        /// <summary>
        /// Gets Item's tax exemption status.
        /// </summary>
        /// <returns>True if item is exempt from basic taxes.</returns>
        public bool IsBasicTaxExempt()
        {
            return this.isBasicTaxExempt;
        }

        /// <summary>
        /// Gets Item's imported status.
        /// </summary>
        /// <returns>True if item is imported.</returns>
        public bool IsImported()
        {
            return this.isImported;
        }

        /// <summary>
        /// Rounds item's price to the nearest cent.
        /// </summary>
        /// <param name="price">Price of the item.</param>
        /// <returns>Price of the item rounded to the nearest cent.</returns>
        private double RoundPrice(double price)
        {
            return Math.Round(price, 2, MidpointRounding.AwayFromZero);
        }
    }
}
