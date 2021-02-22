using System;

namespace Belmont_Sales_Tax
{
    class Program
    {
        static void Main(string[] args)
        {
            ShoppingCart cart = new ShoppingCart();
            String inputString = null;

            Console.WriteLine("Add items to your cart (ex: 2 bottles of cooking oil at 4.99). Enter the word <checkout> or <c> when finished.");
            inputString = Console.ReadLine();

            while (inputString.ToLower().Trim() != "checkout" && inputString.ToLower().Trim() != "c")
            {
                cart.AddToCart(inputString);
                inputString = Console.ReadLine();
            }

            Console.WriteLine("\n\nGenerating Receipt:");

            Checkout.Begin(cart);
        }
    }
}
