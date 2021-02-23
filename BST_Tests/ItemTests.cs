using Belmont_Sales_Tax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BST_Tests
{
    [TestClass]
    public class ItemUnitTest
    {
        [TestMethod]
        public void SimpleConstructorTest()
        {
            String sake = "bottle of sake";
            double sakePrice = 13.99;
            Item item = new Item(sake, sakePrice);

            Assert.AreEqual(item.GetName(), sake);
            Assert.AreEqual(item.GetPrice(), sakePrice);
        }

        [TestMethod]
        public void OverloadedConstructorTest1()
        {
            String sake = "bottle of sake";
            double sakePrice = 13.99;
            bool taxExempt = true;
            Item item = new Item(sake, sakePrice, taxExempt);

            Assert.AreEqual(item.GetName(), sake);
            Assert.AreEqual(item.GetPrice(), sakePrice);
            Assert.AreEqual(item.IsBasicTaxExempt(), taxExempt);
        }

        [TestMethod]
        public void OverloadedConstructorTest2()
        {
            String sake = "bottle of sake";
            double sakePrice = 13.99;
            bool taxExempt = true;
            bool imported = true;
            Item item = new Item(sake, sakePrice, taxExempt, imported);

            Assert.AreEqual(item.GetName(), sake);
            Assert.AreEqual(item.GetPrice(), sakePrice);
            Assert.AreEqual(item.IsBasicTaxExempt(), taxExempt);
            Assert.AreEqual(item.IsImported(), imported);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid parameters!")]
        public void InvalidConstructorTest()
        {
            Item veryGoodSakeDeal = new Item("bottle of sake", -19.99);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid parameters!")]
        public void InvalidConstructorTest2()
        {
            Item namelessItem = new Item("", 19.99);
            Item nullNamedItem = new Item(null, 19.99);
        }

        [TestMethod]
        public void EqualsTest()
        {
            Item sake1 = new Item("bottle of sake", 13.99);
            Item sake2 = new Item("bottle of sake", 12.99, true, true);
            Item expensiveSake = new Item("bottle of sake", 29.99);
            Item somehowTaxExemptSake = new Item("bottle of sake", 14.99, true, false);
            Item importedSake = new Item("bottle of sake", 14.99, false, true);
            sake2.SetPrice(13.99);
            sake2.SetTaxExemptionStatus(false);
            sake2.SetImportedStatus(false);

            Assert.AreEqual(sake1, sake2);
            Assert.AreNotEqual(sake1, expensiveSake);
            Assert.AreNotEqual(sake1, somehowTaxExemptSake);
            Assert.AreNotEqual(sake1, importedSake);
        }

        [TestMethod]
        public void EqualToOperatorTest()
        {
            Item sake1 = new Item("bottle of sake", 13.99);
            Item sake2 = new Item("bottle of sake", 12.99, true, true);
            Item expensiveSake = new Item("bottle of sake", 29.99);
            Item somehowTaxExemptSake = new Item("bottle of sake", 14.99, true, false);
            Item importedSake = new Item("bottle of sake", 14.99, false, true);
            sake2.SetPrice(13.99);
            sake2.SetTaxExemptionStatus(false);
            sake2.SetImportedStatus(false);

            Assert.IsTrue(sake1 == sake2);
            Assert.IsFalse(sake1 == expensiveSake);
            Assert.IsFalse(sake1 == somehowTaxExemptSake);
            Assert.IsFalse(sake1 == importedSake);
        }

        [TestMethod]
        public void NotEqualToOperatorTest()
        {
            Item sake1 = new Item("bottle of sake", 13.99);
            Item sake2 = new Item("bottle of sake", 12.99, true, true);
            Item expensiveSake = new Item("bottle of sake", 29.99);
            Item somehowTaxExemptSake = new Item("bottle of sake", 14.99, true, false);
            Item importedSake = new Item("bottle of sake", 14.99, false, true);
            sake2.SetPrice(13.99);
            sake2.SetTaxExemptionStatus(false);
            sake2.SetImportedStatus(false);

            Assert.IsTrue(sake1 == sake2);
            Assert.IsFalse(sake1 == expensiveSake);
            Assert.IsFalse(sake1 == somehowTaxExemptSake);
            Assert.IsFalse(sake1 == importedSake);

            Assert.IsTrue(sake1 != expensiveSake);
            Assert.IsTrue(sake1 != somehowTaxExemptSake);
            Assert.IsTrue(sake1 != importedSake);
            Assert.IsFalse(sake1 != sake2);
        }

        [TestMethod]
        public void RoundPriceTest()
        {
            Item sake = new Item("bottle of sake", 12.809999);
            Assert.IsTrue(sake.GetPrice() == 12.81);
        }
    }
}