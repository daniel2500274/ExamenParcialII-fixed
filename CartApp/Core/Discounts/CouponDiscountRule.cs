using CartApp.Core.Discounts.Interfaces;
using CartApp.Models;
using CartApp.Settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Discounts
{
    public class CouponDiscountRule:IDiscountRule
    {
        private readonly IConfigsDTO _configs;
        public CouponDiscountRule (IConfigsDTO configs)
        {
            _configs = configs;
        }

        public decimal CalculateDiscount(decimal subtotal, DiscountsDTO context)
        {
            if (!string.IsNullOrWhiteSpace(context.Coupon) && context.Coupon.Trim().ToUpperInvariant() == "PROMO10")
            {
                return subtotal * _configs.DiscountCode;
            }
            return 0m;
        }

    }
}
