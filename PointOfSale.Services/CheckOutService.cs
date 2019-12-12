using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace PointOfSale.Services
{
    public interface ICheckOutService
    {
        string CheckOut(string products);
    }

    public class CheckOutService : ICheckOutService
    {
        private readonly IPricingRepo _pricingRepo;
        private readonly IDictionary<string, int> _productDict;
        private IDictionary<string, Pricing> _pricingDict;
        public CheckOutService(IPricingRepo pricingRepo)
        {
            _pricingRepo = pricingRepo;
            _productDict = new Dictionary<string, int>();
        }


        /// <summary>
        /// Calculate the total price for an entire shopping cart
        /// </summary>
        /// <param name="productCodes"></param>
        /// <returns>$1.50</returns>
        public string CheckOut(string productCodes)
        {
            decimal total;
            string formattedTotal;
            string trimmedCodes;

            if (string.IsNullOrWhiteSpace(productCodes))
            {
                throw new NullReferenceException("Invalid input");
            }

            trimmedCodes = productCodes.Trim().ToUpper();

            SetPricing();

            for (int i = 0; i < trimmedCodes.Length; i++)
            {
                var code = trimmedCodes[i].ToString();

                if (string.IsNullOrWhiteSpace(code))
                {
                    continue;
                }

                ScanProduct(code);
            }

            total = CalculateTotal();

            //in real world, a money type would be created to deal with curreny and string conversion
            formattedTotal = total.ToString("C2", CultureInfo.CreateSpecificCulture("en-NZ"));

            return formattedTotal;
        }


        private void SetPricing()
        {
            _pricingDict = _pricingRepo.GetPricing();
        }

        private void ScanProduct(string productCode)
        {
            if (string.IsNullOrWhiteSpace(productCode))
            {
                return;
            }
            
            //count the total number of the same product
            if (_productDict.ContainsKey(productCode)) 
            {
                _productDict[productCode]++;
            }
            else
            {
                _productDict.Add(productCode, 1);
            }

        }

        private decimal CalculateTotal()
        {
            var total = 0m;

            foreach (var product in _productDict)
            {
                if (_pricingDict.TryGetValue(product.Key, out Pricing p))
                {
                    //calculate volumed total base on the total number of code and the volume size
                    var volumedTotal = (product.Value / p.VolumeSize) * p.VolumePrice;
                    var unitTotal = (product.Value % p.VolumeSize) * p.UnitPrice;

                    total += volumedTotal + unitTotal;
                }
            }

            return total;
        }
    }
}
