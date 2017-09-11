using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Klarna.Checkout.Entities
{
    public class ShippingOptions
    {
        [JsonProperty(PropertyName = "id")]
        public int Id;
        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "description")]
        public string Description;
        [JsonProperty(PropertyName = "promo")]
        public string Promo;
        [JsonProperty(PropertyName = "price")]
        public int Price;
        [JsonProperty(PropertyName = "tax_amount")]
        public int TaxAmount;
        [JsonProperty(PropertyName = "tax_rate")]
        public int TaxRate;
        [JsonProperty(PropertyName = "preselected")]
        public bool Preselected;
        [JsonProperty(PropertyName = "shipping_method")]
        public string ShippingMethod;
    }
}
