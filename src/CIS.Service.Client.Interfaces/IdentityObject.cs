using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CIS.Service.Client.Interfaces
{
    /// <summary>
    /// The object with the <see cref="Identity"/> property.
    /// </summary>
    public class IdentityObject : IIdentity, IBody
    {
        /// <summary>
        /// Gets or sets the primary identifier.
        /// </summary>
        [JsonPropertyName("_identity")]
        [DataMember(Name = "_identity")]
        public Guid? Identity { get; set; }
    }
}
