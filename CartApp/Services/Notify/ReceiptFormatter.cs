using CartApp.Models;
using CartApp.Services.Notify.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CartApp.Services.Notify
{
    public class ReceiptFormatter: IReceiptFormatter
    {
        public string Format(ReceiptsDataDTO dataRecipt)
        {
            StringBuilder lines = new StringBuilder();
            lines.AppendLine("RECIBO DE COMPRA");
            foreach (Item it in dataRecipt.Items)
            {
                lines.AppendLine($"- {it.Name}  Q{it.Price:F2} {(it.IsFragile ? "(Frágil)" : "")}");
            }
            lines.AppendLine($"Subtotal: Q{dataRecipt.Subtotal:F2}");
            lines.AppendLine($"Descuento: -Q{dataRecipt.Discount:F2}");
            lines.AppendLine($"IVA: Q{dataRecipt.Vat:F2}");
            lines.AppendLine($"Envío: Q{dataRecipt.Shipping:F2}");
            lines.AppendLine($"Recargo frágil: Q{dataRecipt.Fragile:F2}");
            lines.AppendLine($"TOTAL: Q{dataRecipt.Total:F2}");
            return lines.ToString();
        }
    }
}
