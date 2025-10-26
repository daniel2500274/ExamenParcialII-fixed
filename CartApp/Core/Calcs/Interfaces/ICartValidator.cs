using CartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs.Interfaces
{
    public interface ICartValidator
    {
        void Validate(List<Item> items);
    }
}
