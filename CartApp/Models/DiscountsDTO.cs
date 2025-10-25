using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Models
{
    // DTO para pasar datos a las reglas de descuento
    public class DiscountsDTO
    {
        public List<Item> Items { get; }
        public string? Coupon { get; }
        public bool IsVip { get; }

        public DiscountsDTO(List<Item> items, string? coupon, bool isVip)
        {
            Items = items;
            Coupon = coupon;
            IsVip = isVip;
        }
    }
}
