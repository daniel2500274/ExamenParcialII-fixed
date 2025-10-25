using CartApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Services.Notify.interfaces
{
    public interface IReceiptFormatter
    {
        string Format(ReceiptsDataDTO receiptData);
    }
}
