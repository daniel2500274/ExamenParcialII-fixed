using CartApp.Core.Calcs.Interfaces;
using CartApp.Settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs
{
    public class ShippingCalculator : IShippingCalculator
    {
        private readonly IConfigsDTO _config;

        public ShippingCalculator(IConfigsDTO config)
        {
            _config = config;
        }
        public decimal Calculate(decimal subtotal)
        {
            return subtotal < _config.ShippingThreshold
                   ? _config.ShippingBelowFee  
                   : _config.ShippingAboveOrEqualFee;
        }

    }
}
