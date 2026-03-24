using System;
using System.Collections.Generic;
using System.Linq;

namespace GlobalMart.Services
{
    public class PricingService : IPricingService
    {
        public decimal GetBasePrice()
        {
            return 100;
        }
        public decimal CalculatePrice(decimal basePrice, string promoCode)
        {
            decimal finalPrice = basePrice;

            if (!string.IsNullOrEmpty(promoCode))
            {
                switch (promoCode.ToUpper())
                {
                    case "WINTER25":
                        finalPrice -= basePrice * 0.15m;
                        break;

                    case "FREESHIP":
                        finalPrice -= 5.00m;
                        break;

                    case "CU25":
                        finalPrice -= basePrice * 0.25m;
                        break;

                    case "ABHAY":
                        finalPrice -= 50.00m;
                        break;
                }
            }

            return finalPrice < 0 ? 0 : finalPrice;
        }

        public IEnumerable<string> GetAvailablePromoCodes()
        {
            
            return new List<string>
            {
                "WINTER25",
                "FREESHIP",
                "CU25",
                "ABHAY"
            };
        }
    }
}
