using System.Collections.Generic;
using Klarna.Checkout.Entities;
using Klarna.Entities;
using Newtonsoft.Json;

namespace Klarna.Checkout
{
    public class CheckoutOrder : Order
    {
        [JsonProperty(PropertyName = "html_snippet")]
        public string Snippet;
        [JsonProperty(PropertyName = "status")]
        public string Status;
        [JsonProperty(PropertyName = "order_id")]
        public string OrderId;
        [JsonProperty(PropertyName = "purchase_country")]
        public string PurchaseCountry;
        [JsonProperty(PropertyName = "purchase_currency")]
        public string PurchaseCurrency;
        [JsonProperty(PropertyName = "locale")]
        public string Locale;

        [JsonProperty(PropertyName = "order_lines")]
        public List<CheckoutOrderLine> Orderlines;
        [JsonProperty(PropertyName = "order_amount")]
        public int OrderAmount;
        [JsonProperty(PropertyName = "order_tax_amount")]
        public int OrderTaxAmount;
        [JsonProperty(PropertyName = "merchant_reference1")]
        public int MerchantReference1;
        [JsonProperty(PropertyName = "merchant_reference2")]
        public int MerchantReference2;
        [JsonProperty(PropertyName = "external_payment_methods")]
        public List<PaymentProvider> ExternalPaymentMethods;
        [JsonProperty(PropertyName = "attachment")]
        public Attachment Attachment;
        [JsonProperty(PropertyName = "external_checkouts")]
        public List<PaymentProvider> ExternalCheckouts;
        [JsonProperty(PropertyName = "shipping_countries")]
        public List<string> ShippingCountries;
        [JsonProperty(PropertyName = "shipping_options")]
        public List<ShippingOptions> ShippingOptions;
        /// <summary>
        /// Pass through field, can be used for any data
        /// </summary>
        [JsonProperty(PropertyName = "merchant_data")]
        public string MerchantData;
        /// <summary>
        /// The shipping option that the customer selected
        /// </summary>
        [JsonProperty(PropertyName = "selected_shipping_option")]
        public ShippingOptions SelectedShippingOption;
        public CheckoutOrder():base(null,null)
        {
            
        }
        public CheckoutOrder(List<CheckoutOrderLine> orderlines, MerchantUrls urls, Address shippAddress=null, Address billAddress = null) : base( urls,shippAddress,billAddress)
        {
            Orderlines = orderlines;
            RecalcOrderTotals();
           
        }

       
        private void RecalcOrderTotals()
        {
            OrderAmount = 0;
            OrderTaxAmount = 0;
            
            foreach (CheckoutOrderLine line in Orderlines)
            {
                OrderAmount += line.UnitPrice * line.Quantity;
                OrderTaxAmount += line.TotalTaxAmount;
            }
        }
        public void SetNewOrderlines(List<CheckoutOrderLine> orderlines)
        {
            Orderlines = orderlines;
            RecalcOrderTotals();
        }
        
    }
}
