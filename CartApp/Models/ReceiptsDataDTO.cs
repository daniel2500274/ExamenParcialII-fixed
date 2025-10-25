using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Models
{
    // DTO para pasarle los datos de la factura al modulo de imporesion
    public record ReceiptsDataDTO(
        List<Item> Items,
        decimal Subtotal,
        decimal Discount,
        decimal Vat,
        decimal Shipping,
        decimal Fragile,
        decimal Total
    );
}