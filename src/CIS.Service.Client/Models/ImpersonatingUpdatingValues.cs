using System.Collections.Generic;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The updating values with user impersonating feature.
    /// </summary>
    public class ImpersonatingUpdatingValues
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public ImpersonatingUser User { get; set; }

        /// <summary>
        /// Gets or sets the updating values.
        /// </summary>
        public Dictionary<string, object> UpdatingValues { get; set; }
    }
}
