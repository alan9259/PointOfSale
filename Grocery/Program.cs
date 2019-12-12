using PointOfSale.Services;
using Repository;
using System;

namespace Grocery
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = Console.ReadLine();

            var pricingRepo = new PricingRepo();
            var checkOutService = new CheckOutService(pricingRepo);

            var output = checkOutService.CheckOut(input);

            Console.WriteLine(output);
        }
    }
}
