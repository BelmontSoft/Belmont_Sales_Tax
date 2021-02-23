using Belmont_Sales_Tax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BST_Tests
{
    [TestClass]
    public class CheckoutTests
    {
        [TestMethod]
        public void RoundCentsTest()
        {
            Assert.AreEqual(Checkout.RoundCents(13.7279), 13.73);
        }

        [TestMethod]
        public void CalculateItemTaxesTest()
        {
            double importTaxRate = Checkout.GetImportTaxRate();
            double basicTaxRate = Checkout.GetSalesTaxRate();

            Item basicItem = new Item("Charlie Parker's Greatest Hits", 10.99);
            Item basicTaxExemptItem = new Item("loaf of bread", 1.49, true);
            Item importedItem = new Item("A Corgi straight from Wales", 3000, false, true);
            Item basicTaxFreeImportedItem = new Item("Imported can of olive oil", 8.99, true, true);

            Assert.AreEqual(Checkout.CalculateItemTaxes(basicTaxExemptItem), 0);
            Assert.AreEqual(
                Checkout.CalculateItemTaxes(basicItem),
                Math.Round((Math.Round((basicItem.GetPrice() * basicTaxRate) * 20, MidpointRounding.AwayFromZero) / 20), 1)
                );
            Assert.AreEqual(
                Checkout.CalculateItemTaxes(basicTaxFreeImportedItem),
                Math.Round((Math.Round((basicTaxFreeImportedItem.GetPrice() * importTaxRate) * 20, MidpointRounding.AwayFromZero) / 20), 1)
                );
            Assert.AreEqual(
               Checkout.CalculateItemTaxes(importedItem),
               Math.Round((Math.Round(((importedItem.GetPrice() * importTaxRate) + (importedItem.GetPrice() * basicTaxRate)) * 20, MidpointRounding.AwayFromZero) / 20), 1)
               );
        }
    }
}
