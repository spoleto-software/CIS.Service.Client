using System.Collections.Generic;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The persistent object with user impersonating feature.
    /// </summary>
    public class ImpersonatingDictionaryObject
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        public Dictionary<string, object> Object { get; set; }
    }
}
