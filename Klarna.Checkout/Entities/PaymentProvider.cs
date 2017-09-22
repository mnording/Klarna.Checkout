using Newtonsoft.Json;

namespace Klarna.Checkout.Entities
{
    public class PaymentProvider
    {

        [JsonProperty(PropertyName = "name")]
        public string Name;
        [JsonProperty(PropertyName = "redirect_url")]
        public string RedirectUrl;
        [JsonProperty(PropertyName = "image_url")]
        public string ImageUrl;
        [JsonProperty(PropertyName = "fee")]
        public int Fee;
        [JsonProperty(PropertyName = "description")]
        public string Description;
        [JsonProperty(PropertyName = "countries")]
        public string[] Countries;
    }
}
