using Belmont_Sales_Tax;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BST_Tests
{
    [TestClass]
    public class ShoppingCartTests
    {
        [TestMethod]
        public void ConstructorTest()
        {
            ShoppingCart testCart = new ShoppingCart();
        }

        [TestMethod]
        public void AddItemTest()
        {
            ShoppingCart testCart = new ShoppingCart();
            Dictionary<Item, int> testDict = new Dictionary<Item, int>();
            Item sake = new Item("bottle of sake", 9.99);
            Item chocolate = new Item("box of chocolate", 2.99);

            testCart.AddToCart(sake);
            testCart.AddToCart(chocolate, 3);
            testDict = testCart.GetContents();

            Assert.IsTrue(testDict[sake] == 1);
            Assert.IsTrue(testDict[chocolate] == 3);
        }

        [TestMethod]
        public void AddItemStringTest()
        {
            ShoppingCart testCart = new ShoppingCart();
            Dictionary<Item, int> testCartContents = new Dictionary<Item, int>();
            String sakeString = "3 bottles of sake at 9.99";
            Item sake = new Item("bottle of sake", 9.99);

            testCart.AddToCart(sakeString);

            testCartContents = testCart.GetContents();

            foreach (KeyValuePair<Item, int> itemSet in testCartContents)
            {
                Assert.IsTrue(itemSet.Key == sake);
                Assert.IsTrue(testCartContents[itemSet.Key] == 3);
            }
        }
    }
}
