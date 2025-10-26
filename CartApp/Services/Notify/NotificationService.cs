using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// En: CartApp/Services/Notify/NotificationService.cs

using CartApp.Models;
using CartApp.Services.Notify.interfaces;
using CartApp.Settings.interfaces;

namespace CartApp.Services.Notify
{ 
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;
        private readonly IReceiptPrinter _printer;
        private readonly IReceiptFormatter _formatter;
        private readonly IConfigsDTO _config;

        public NotificationService(
            IEmailSender emailSender,
            IReceiptPrinter printer,
            IReceiptFormatter formatter,
            IConfigsDTO config)
        {
            _emailSender = emailSender;
            _printer = printer;
            _formatter = formatter;
            _config = config;
        }

        public void SendEmailReceipt(ReceiptsDataDTO data)
        {
            string bodyEmail = _formatter.Format(data); 
            _emailSender.Send(_config.ReceiptEmailTo, _config.ReceiptEmailSubject, bodyEmail);
        }
         
        public void PrintConsoleReceipt(ReceiptsDataDTO data)
        {
            string receiptBody = _formatter.Format(data);
            _printer.Print(receiptBody);
        }
    }
}