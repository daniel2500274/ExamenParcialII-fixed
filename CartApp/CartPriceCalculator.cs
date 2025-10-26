using CartApp.Core.Calcs;
using CartApp.Core.Calcs.Interfaces;
using CartApp.Core.Discounts;
using CartApp.Core.Discounts.Interfaces;

using CartApp.Models;

using CartApp.Services.Loggin;
using CartApp.Services.Notify;
using CartApp.Services.Notify.interfaces;

using CartApp.Settings; 

namespace CartApp
{
    public class CartPriceCalculator
    {
        // cargo las interfases con los contratos de funcionalidad abstraidos
        private readonly ILogger _logger;
        private readonly ICartValidator _validator;
        private readonly ISubtotalCalculator _subtotalCalculator;
        private readonly IDiscountCalculator _discountCalculator;
        private readonly IShippingCalculator _shippingCalculator;
        private readonly ISurchargeCalculator _fragileSurchargeCalculator;
        private readonly IIVACalculator _vatCalculator;
        private readonly INotificationService _notificationService;

         
        public CartPriceCalculator()
        {
            // para cargo las configuraciones por defecto
            var config = new ConfigsReader();

            // inizialiacion del logger
            _logger = new ConsoleLogger();

            // creo las instancias para las calculadoreas
            _validator = new CartValidator();
            _subtotalCalculator = new SubtotalCalculator();
            _shippingCalculator = new ShippingCalculator(config);
            _fragileSurchargeCalculator = new FragileSurchargeCalculator(config);
            _vatCalculator = new IVACalculator(config);

            // Configuración de reglas de descuento
            // Creamos la lista de reglas que usará el calculador
            var discountRules = new List<IDiscountRule>
            {
                new VipDiscountRule(config),
                new CouponDiscountRule(config)
                // si en el futuro se agrega un nuevo descuento se carga desde acá
            };
            _discountCalculator = new DiscountCalculator(discountRules);

            // Servicios de notificación
            var emailSender = new EmailSender();
            var printer     = new ReceiptPrinter();
            var formatter   = new ReceiptFormatter();

            _notificationService = new NotificationService(emailSender, printer, formatter, config);
        }

       
        public decimal CalculateTotal(List<Item> items, string? coupon, bool isVip, bool emailReceipt)
        {
            _logger.Info("Inicio de cálculo de carrito.");

            // verifico que si traiga contenido el carrito de items
            try
            {
                _validator.Validate(items);
            }
            catch (ArgumentException ex)
            {
                _logger.Error(ex.Message);
                throw;
            }

            if (items == null || items.Count == 0)
            {
                _logger.Info("Carrito vacío: total = 0.");
                return 0m;
            }

             
            decimal subtotal = _subtotalCalculator.Calculate(items);

             
            var discountContext = new DiscountsDTO(items, coupon, isVip);
            decimal discount = _discountCalculator.Calculate(subtotal, discountContext);

            decimal discountedSubtotal = subtotal - discount;

            decimal shipping = _shippingCalculator.Calculate(subtotal);
            decimal fragileSurcharge = _fragileSurchargeCalculator.Calculate(items);
            decimal vat = _vatCalculator.Calculate(discountedSubtotal);

            
            decimal total = discountedSubtotal + shipping + fragileSurcharge + vat;

             
            var receiptData = new ReceiptsDataDTO(
                items,
                subtotal,
                discount,
                vat,
                shipping,
                fragileSurcharge,
                total
            );

            
            _notificationService.PrintConsoleReceipt(receiptData);

           
            if (emailReceipt)
            {
                _notificationService.SendEmailReceipt(receiptData);
            }

            _logger.Info($"Fin de cálculo. Total: Q{total:F2}");
            return total;
        } 
    }
}