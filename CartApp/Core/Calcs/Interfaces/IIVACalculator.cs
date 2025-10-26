using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs.Interfaces
{
    public interface IIVACalculator
    {
        decimal Calculate(decimal discoutedSubtotal);
    }
}
