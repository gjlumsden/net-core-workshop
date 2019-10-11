using DiscountCalculator.Core;
using System;

namespace DiscountCalculator.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var discountEngine = new DiscountEngine();

            while (true)
            {
                Console.WriteLine("Enter date joined:");
                var input = Console.ReadLine();

                DateTime dateJoined;
                if (!DateTime.TryParse(input, out dateJoined))
                {
                    Console.WriteLine($"Invalid input: '{input}'.");
                }

                var discount = discountEngine.GetDiscountAmount(dateJoined);
                Console.WriteLine($"Available discount: {discount * 100}%");
            }


        }
    }
}