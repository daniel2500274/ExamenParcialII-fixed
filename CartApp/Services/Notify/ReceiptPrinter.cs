using CartApp.Models;
using CartApp.Services.Notify.interfaces;
using CartApp.Settings;

namespace CartApp.Services.Notify
{
    public class ReceiptPrinter : IReceiptPrinter
    { 
        public void Print(string receiptText)
        {
            Console.WriteLine("\n========== RECIBO ==========");
            Console.WriteLine(receiptText); // Simplemente imprime el texto ya formateado
            Console.WriteLine("==============================\n");
        } 
    }
}
