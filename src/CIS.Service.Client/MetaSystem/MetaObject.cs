using System;

namespace CIS.Service.Client.MetaSystem
{
    /// <summary>
    /// The base object for all meta-objects.
    /// </summary>
    public class MetaObject
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Guid? Identity { get; set; }
    }
}
