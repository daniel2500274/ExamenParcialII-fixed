using CartApp.Models;
using CartApp.Settings;

namespace CartApp.Services.Notify
{
    public class ReceiptPrinter
    {
        public void Print(List<Item> items, decimal subtotal, decimal discount, decimal vat, decimal shipping, decimal fragile, decimal total)
        {
            Console.WriteLine("\n========== RECIBO ==========");
            foreach (Item it in items)
            {
                Console.WriteLine($"Producto: {it.Name,-15} Q{it.Price,6} {(it.IsFragile ? "(Frágil)" : "")}");
            }
            Console.WriteLine($"Subtotal: Q{subtotal:F2}");
            Console.WriteLine($"Descuento: -Q{discount:F2}");
            Console.WriteLine($"IVA ({StaticConfig.VatRate:P0}): Q{vat:F2}");
            Console.WriteLine($"Envío: Q{shipping:F2}");
            Console.WriteLine($"Recargo frágil: Q{fragile:F2}");
            Console.WriteLine("------------------------------");
            Console.WriteLine($"TOTAL: Q{total:F2}");
            Console.WriteLine("==============================\n");
        }
    }
}
