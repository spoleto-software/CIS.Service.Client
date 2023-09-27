using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.Service.Client.MetaSystem
{
    /// <summary>
    /// The meta-system attribute.
    /// </summary>
    public class MetaAttribute : MetaObject
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [JsonRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Required]
        [JsonRequired]
        public MetaAttributeType Type { get; set; }

        /// <summary>
        /// Gets or sets whether the attribute is visible.
        /// </summary>
        [Required]
        [JsonRequired]
        public bool IsVisible { get; set; }

        /// <summary>
        /// Gets or sets whether the attribute is enabled in UI.
        /// </summary>
        [Required]
        [JsonRequired]
        public bool IsEnabled { get; set; }

        public override string ToString() => $"{Name}, {nameof(Type)}: {Type}, {nameof(IsEnabled)}: {IsEnabled}, {nameof(IsVisible)}: {IsVisible}";
    }
}
