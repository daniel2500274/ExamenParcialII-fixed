using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Services.Notify.interfaces
{
    internal interface IReceiptPrinter
    {
        void Print(string receiptText);
    }
}
