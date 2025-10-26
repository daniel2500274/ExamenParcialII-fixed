using CartApp.Core.Calcs.Interfaces;
using CartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs
{
    public class SubtotalCalculator : ISubtotalCalculator
    {
        public decimal Calculate(List<Item> items)
        {
            return items.Sum(item => item.Price);
        }
    }
}
