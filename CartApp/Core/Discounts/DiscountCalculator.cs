using CartApp.Core.Discounts.Interfaces;
using CartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Discounts
{
    public class DiscountCalculator : IDiscountCalculator
    {
        private readonly IEnumerable<IDiscountRule> _rules;
        public DiscountCalculator(IEnumerable<IDiscountRule> rules)
        {
            _rules = rules;
        }
        public decimal Calculate(decimal subtotal, DiscountsDTO context)
        {
            decimal totalDiscount = 0m;
            foreach (var rule in _rules)
            {
                totalDiscount += rule.CalculateDiscount(subtotal, context);
            }
            return totalDiscount;
        }
    }
}
