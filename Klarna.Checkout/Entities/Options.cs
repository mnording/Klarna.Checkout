using Newtonsoft.Json;

namespace Klarna.Checkout.Entities
{
    public class Options
    {
        [JsonProperty(PropertyName = "allow_separate_shipping_address")]
        public bool AllowSeparateShippingAddress;
        /// <summary>
        /// CSS hex color, e.g. "#FF9900"
        /// </summary>
        [JsonProperty(PropertyName = "color_button")]
        public string ColorButton;
        /// <summary>
        /// CSS hex color, e.g. "#FF9900"
        /// </summary>
        [JsonProperty(PropertyName = "color_button_text")]
        public string ColorButtonText;
        /// <summary>
        /// CSS hex color, e.g. "#FF9900"
        /// </summary>
        [JsonProperty(PropertyName = "color_checkbox")]
        public string CcolorCheckbox;
        /// <summary>
        /// CSS hex color, e.g. "#FF9900"
        /// </summary>
        [JsonProperty(PropertyName = "color_checkbox_checkmark")]
        public string ColorCheckboxCheckmark;
        /// <summary>
        /// CSS hex color, e.g. "#FF9900"
        /// </summary>
        [JsonProperty(PropertyName = "color_header")]
        public string ColorHeader;
        /// <summary>
        /// CSS hex color, e.g. "#FF9900"
        /// </summary>
        [JsonProperty(PropertyName = "color_link")]
        public string ColorLink;
        /// <summary>
        /// Border radius
        /// </summary>
        [JsonProperty(PropertyName = "radius_border")]
        public string Radius_Border;
        [JsonProperty(PropertyName = "date_of_birth_mandatory")]
        public bool DateOfBirthMandatory;
        /// <summary>
        /// If true, the Order Detail subtodals view is expanded. Default: false
        /// </summary>
        [JsonProperty(PropertyName = "show_subtotal_detail")]
        public bool ShowSubtotalDetail;
        [JsonProperty(PropertyName = "shipping_details")]
        public string ShippingDetails;
        [JsonProperty(PropertyName = "require_validate_callback_succes")]
        public bool RequireValidateCallbackSucces;
        [JsonProperty(PropertyName = "additional_checkbox")]
        public CheckBox AdditionalCheckbox;


    }
}
