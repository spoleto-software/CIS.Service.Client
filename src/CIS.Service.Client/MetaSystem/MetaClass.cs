using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CIS.Service.Client.MetaSystem
{
    /// <summary>
    /// The meta-system class.
    /// </summary>
    public class MetaClass : MetaObject
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [JsonRequired]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
