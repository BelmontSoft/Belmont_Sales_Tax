using Belmont_Sales_Tax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BST_Tests
{
    [TestClass]
    public class ShoppingCartHelperTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ShoppingCartHelper testCartHelper = new ShoppingCartHelper();
        }

        [TestMethod]
        public void ProcessItemQuantityTest()
        {
            ShoppingCartHelper testCartHelper = new ShoppingCartHelper();
            String testItemString = "3 bottles of sake at 13.99";

            Assert.AreEqual(testCartHelper.ProcessItemQuantity(ref testItemString), 3);
            Assert.AreEqual(testItemString, "bottles of sake at 13.99");
        }

        [TestMethod]
        public void ProcessUndefinedItemQuantityTest()
        {
            ShoppingCartHelper testCartHelper = new ShoppingCartHelper();
            String testItemString = "bottles of sake at 13.99";

            Assert.AreEqual(testCartHelper.ProcessItemQuantity(ref testItemString), 1);
            Assert.AreEqual(testItemString, "bottles of sake at 13.99");
        }

        [TestMethod]
        public void ProcessItemTest()
        {
            ShoppingCartHelper testCartHelper = new ShoppingCartHelper();
            String testItemString = "bottles of sake at 13.99";
            String testItemString2 = "imported magazine at 6.95";

            Item sake = new Item("bottle of sake", 13.99);
            Item processedSake = testCartHelper.ProcessItem(testItemString);

            Assert.IsTrue(sake.GetName() == processedSake.GetName());
            Assert.IsTrue(sake.GetPrice() == processedSake.GetPrice());
            Assert.IsTrue(sake.IsBasicTaxExempt() == processedSake.IsBasicTaxExempt());
            Assert.IsTrue(sake.IsImported() == processedSake.IsImported());

            Item importedMagazine = new Item("imported magazine", 6.95, true, true);
            Item processedImportedMagazine = testCartHelper.ProcessItem(testItemString2);

            Assert.IsTrue(importedMagazine.GetName() == processedImportedMagazine.GetName());
            Assert.IsTrue(importedMagazine.GetPrice() == processedImportedMagazine.GetPrice());
            Assert.IsTrue(importedMagazine.IsBasicTaxExempt() == processedImportedMagazine.IsBasicTaxExempt());
            Assert.IsTrue(importedMagazine.IsImported() == processedImportedMagazine.IsImported());
        }
    }
}