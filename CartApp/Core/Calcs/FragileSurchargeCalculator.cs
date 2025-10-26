using CartApp.Core.Calcs.Interfaces;
using CartApp.Models;
using CartApp.Settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs
{
    public class FragileSurchargeCalculator:ISurchargeCalculator
    {
        private readonly IConfigsDTO _config;
        public FragileSurchargeCalculator(IConfigsDTO config)
        {
            _config = config;
        }

        public decimal Calculate(List<Item> items)
        {
            bool anyFragile = items.Any(item => item.IsFragile);
            return anyFragile ? _config.FragileSurcharge : 0m;
        }
    }
}
