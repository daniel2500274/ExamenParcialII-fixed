using CartApp.Core.Calcs.Interfaces;
using CartApp.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs
{
    internal class IVACalculator:IIVACalculator
    {
        private readonly IConfigsDTO _config;
        public IVACalculator(IConfigsDTO config)
        {
            _config = config;
        }
        public decimal Calculate(decimal discountedSubtotal)
        {
            return Math.Round(discountedSubtotal * _config.IVARate, 2, MidpointRounding.AwayFromZero);
        }
    }
}
