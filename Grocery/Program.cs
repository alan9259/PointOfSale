using PointOfSale.Services;
using Repository;
using System;

namespace Grocery
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press q to quit");

            var quit = false;

            while (!quit)
            {
                Console.WriteLine("Enter product codes eg, AABBCC");

                var input = Console.ReadLine();

                if (input == "q")
                {
                    quit = true;
                }
                else
                {
                    var pricingRepo = new PricingRepo();
                    var checkOutService = new CheckOutService(pricingRepo);

                    var output = checkOutService.CheckOut(input);

                    Console.WriteLine(output);
                }
            }
        }
    }
}
