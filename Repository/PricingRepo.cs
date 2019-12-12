using Model;
using System.Collections.Generic;

namespace Repository
{
    public interface IPricingRepo
    {
        IDictionary<string, Pricing> GetPricing();
    }

    public class PricingRepo : IPricingRepo
    {

        public PricingRepo() {}

        public IDictionary<string, Pricing> GetPricing()
        {
            var pricing = new Dictionary<string, Pricing>()
            {
                {"A", new Pricing { ProductCode = "A", UnitPrice = 1.25m, VolumeSize = 3, VolumePrice = 3m } },
                {"B", new Pricing { ProductCode = "B", UnitPrice = 4.25m, VolumeSize = 1, VolumePrice = 4.25m } },
                {"C", new Pricing { ProductCode = "C", UnitPrice = 1.00m, VolumeSize = 6, VolumePrice = 5m } },
                {"D", new Pricing { ProductCode = "D", UnitPrice = 0.75m, VolumeSize = 1, VolumePrice = 0.75m } },
            };

            return pricing;
        }
    }
}
