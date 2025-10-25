using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Settings
{
    //Interfaz para definir la configuración
    public interface IConfigsDTO
    {
        decimal DiscountCode { get; }
        decimal VipExtraDiscountRate { get; }
        decimal ShippingThreshold { get; }
        decimal ShippingBelowFee { get; }
        decimal ShippingAboveOrEqualFee { get; }
        decimal FragileSurcharge { get; }
        decimal VatRate { get; }
        string ReceiptEmailTo { get; }
        string ReceiptEmailSubject { get; }
    }
}
