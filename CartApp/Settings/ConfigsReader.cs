using CartApp.Settings.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartApp.Settings
{
    public class ConfigsReader : IConfigsDTO
    {
        public decimal DiscountCode => StaticConfig.Promo10DiscountRate;
        public decimal VipExtraDiscountRate => StaticConfig.VipExtraDiscountRate;
        public decimal ShippingThreshold => StaticConfig.ShippingThreshold;
        public decimal ShippingBelowFee => StaticConfig.ShippingBelowFee;
        public decimal ShippingAboveOrEqualFee => StaticConfig.ShippingAboveOrEqualFee;
        public decimal FragileSurcharge => StaticConfig.FragileSurcharge;
        public decimal IVARate => StaticConfig.VatRate;
        public string ReceiptEmailTo => StaticConfig.ReceiptEmailTo;
        public string ReceiptEmailSubject => StaticConfig.ReceiptEmailSubject;
    }
}
