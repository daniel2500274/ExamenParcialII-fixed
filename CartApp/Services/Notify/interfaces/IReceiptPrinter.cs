using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Services.Notify.interfaces
{
    public interface IReceiptPrinter
    {
        void Print(string receiptText);
    }
}
