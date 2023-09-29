using System.Collections.Generic;
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

        /// <summary>
        /// Gets or sets the attributes
        /// </summary>
        [Required]
        [JsonRequired]
        public List<MetaAttribute> Attributes { get; set; }

        /// <summary>
        /// Gets or sets the class permissions.
        /// </summary>
        [Required]
        [JsonRequired]
        public Dictionary<PermissionType, bool> ClassPermissions { get; set; }
    }
}
