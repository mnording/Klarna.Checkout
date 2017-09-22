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

        public CheckoutOrderLine(string name, int quantity, int unitPrice, int taxRate) : base(name, quantity,
            unitPrice, taxRate)
        {
            TotalAmount = UnitPrice * Quantity;
            TotalTaxAmount = CalculateTaxAmountTotal(TotalAmount);
        }
        
    }
}
