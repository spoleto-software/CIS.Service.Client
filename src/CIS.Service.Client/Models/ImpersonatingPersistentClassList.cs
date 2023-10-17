using System.Collections.Generic;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The persistent class list with user impersonating feature.
    /// </summary>
    public class ImpersonatingPersistentClassList
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the object.
        /// </summary>
        public List<string> ObjectClassNames { get; set; }

        /// <summary>
        /// Gets or sets the WithAttributes flag.
        /// </summary>
        public bool WithAttributes { get; set; }
    }
}
