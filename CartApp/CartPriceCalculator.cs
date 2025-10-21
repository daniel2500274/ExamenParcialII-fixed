using System.Text;

namespace CartApp
{
    public class CartPriceCalculator
    {
        public decimal CalculateTotal(List<Item> items, string? coupon, bool isVip, bool emailReceipt)
        {
            ConsoleLogger logger = new ConsoleLogger();
            logger.Info("Inicio de cálculo de carrito.");

            if (items == null || items.Count == 0)
            {
                logger.Info("Carrito vacío: total = 0.");
                return 0m;
            }

            foreach (Item it in items)
            {
                if (it.Price < 0)
                {
                    logger.Error($"Precio negativo detectado en '{it.Name}'.");
                    throw new ArgumentException($"Negative price for item: {it.Name}");
                }
            }

            decimal subtotal = 0m;
            foreach (Item it in items)
            {
                subtotal += it.Price;
            }

            decimal discount = 0m;

            if (!string.IsNullOrWhiteSpace(coupon) &&
                coupon.Trim().ToUpperInvariant() == "PROMO10")
            {
                discount += subtotal * StaticConfig.Promo10DiscountRate;
            }

            if (isVip)
            {
                discount += subtotal * StaticConfig.VipExtraDiscountRate;
            }

            decimal discountedSubtotal = subtotal - discount;

            decimal shipping = subtotal < StaticConfig.ShippingThreshold
                ? StaticConfig.ShippingBelowFee
                : StaticConfig.ShippingAboveOrEqualFee;

            bool anyFragile = false;
            foreach (Item it in items)
            {
                if (it.IsFragile)
                {
                    anyFragile = true;
                    break;
                }
            }

            decimal fragileSurcharge = anyFragile ? StaticConfig.FragileSurcharge : 0m;
            decimal vat = Math.Round(discountedSubtotal * StaticConfig.VatRate, 2, MidpointRounding.AwayFromZero);
            decimal total = discountedSubtotal + shipping + fragileSurcharge + vat;

            ReceiptPrinter printer = new ReceiptPrinter();
            printer.Print(items, subtotal, discount, vat, shipping, fragileSurcharge, total);

            if (emailReceipt)
            {
                EmailSender emailSender = new EmailSender();
                string body = BuildEmailBody(items, subtotal, discount, vat, shipping, fragileSurcharge, total);
                emailSender.Send(StaticConfig.ReceiptEmailTo, StaticConfig.ReceiptEmailSubject, body);
            }

            logger.Info($"Fin de cálculo. Total: Q{total:F2}");
            return total;
        }

        private string BuildEmailBody(List<Item> items, decimal subtotal, decimal discount, decimal vat, decimal shipping, decimal fragile, decimal total)
        {
            StringBuilder lines = new StringBuilder();
            lines.AppendLine("RECIBO DE COMPRA");
            foreach (Item it in items)
            {
                lines.AppendLine($"- {it.Name}  Q{it.Price:F2} {(it.IsFragile ? "(Frágil)" : "")}");
            }
            lines.AppendLine($"Subtotal: Q{subtotal:F2}");
            lines.AppendLine($"Descuento: -Q{discount:F2}");
            lines.AppendLine($"IVA: Q{vat:F2}");
            lines.AppendLine($"Envío: Q{shipping:F2}");
            lines.AppendLine($"Recargo frágil: Q{fragile:F2}");
            lines.AppendLine($"TOTAL: Q{total:F2}");
            return lines.ToString();
        }
    }
}
