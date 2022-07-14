using System;
using System.Globalization;
using System.Text.Json.Serialization;
using CIS.Service.Client.Helpers;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The serializable value for any service.
    /// </summary>
    public class WebValue
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public WebValue()
        {
        }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        public WebValue(object value)
        {
            Value = value;
            WebType = new WebType(value?.GetType());
        }

        /// <summary>
        /// Gets or sets the object value of some property.
        /// </summary>
        [JsonPropertyName("Value")]
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the object key.
        /// </summary>
        [JsonPropertyName("WebType")]
        public WebType WebType { get; set; }

        /// <summary>
        /// Gets or sets the object key.
        /// </summary>
        public Type GetRealType() => WebType?.GetRealType();

        /// <summary>
        /// Gets or sets the object value of some property.
        /// </summary>
        public object GetRealValue()
        {
            var value = Value;
            if (value == null)
            {
                return null;
            }

            var realType = GetRealType();

            if (value is string str)
            {
                if (realType == typeof(string))
                {
                    return str;
                }

                return JsonHelper.FromJson(str, realType);// ObjectConverter.FromString(str, realType);
            }

            return Convert.ChangeType(value, realType, CultureInfo.InvariantCulture); // ObjectConverter.ChangeType(value, realType);
        }
    }
}
