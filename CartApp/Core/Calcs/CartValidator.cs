using CartApp.Core.Calcs.Interfaces;
using CartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Core.Calcs
{
    public class CartValidator: ICartValidator
    {
        public void Validate(List<Item> items)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }
            foreach (var item in items)
            {
                if (item.Price < 0)
                {
                    throw new ArgumentException($"Negative price for item: {item.Name}");
                }
            }
        }
    } 
}
