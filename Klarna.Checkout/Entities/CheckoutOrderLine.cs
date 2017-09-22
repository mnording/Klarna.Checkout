using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Klarna.Entities;
using Newtonsoft.Json;

namespace Klarna.Checkout.Entities
{
    public class CheckoutOrderLine:OrderLine
    {
        [JsonProperty(PropertyName = "total_amount")]
        public int TotalAmount { get; set; }
        [JsonProperty(PropertyName = "total_discount_amount")]
        public int TotalDiscountAmount { get; set; }
        [JsonProperty(PropertyName = "total_tax_amount")]
        public int TotalTaxAmount { get; set; }

        public CheckoutOrderLine(string name, int quantity, int unit_price, int tax_rate) : base(name, quantity,
            unit_price, tax_rate)
        {
            TotalAmount = UnitPrice * Quantity;
            TotalTaxAmount = CalculateTaxAmountFromGross();
        }
        private int CalculateTaxAmountFromGross()
        {
            double taxratefordivide = TaxRate / 10000.00;
            var priceexvat = TotalAmount / (1 + (taxratefordivide));
           var vatAmount = priceexvat - TotalAmount;
            vatAmount = vatAmount * -1;
            vatAmount = Math.Round(vatAmount);
            return (int)(vatAmount);
        }
    }
}
