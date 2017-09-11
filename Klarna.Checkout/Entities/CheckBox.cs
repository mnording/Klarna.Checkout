using Newtonsoft.Json;

namespace Klarna.Checkout.Entities
{
    public class CheckBox
    {
        [JsonProperty(PropertyName = "color_link")]
        public string Text;
        [JsonProperty(PropertyName = "color_link")]
        public bool IsChecked;
        [JsonProperty(PropertyName = "color_link")]
        public bool IsRequired;
    }
}
