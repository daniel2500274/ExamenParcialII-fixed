using CartApp.Core.Discounts.Interfaces;
using CartApp.Models;
using CartApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Discounts
{
    public class VipDiscountRule:IDiscountRule
    {
        private readonly IConfigsDTO _config;
        public VipDiscountRule(IConfigsDTO config)
        {
            _config = config;
        }
        public decimal CalculateDiscount(decimal subtotal, DiscountsDTO context)

        {
            if (context.IsVip)
            {
                return subtotal * _config.VipExtraDiscountRate;
            }
            return 0m;
        }
    }
}
