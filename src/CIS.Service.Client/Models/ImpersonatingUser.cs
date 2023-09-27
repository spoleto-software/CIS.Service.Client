using System;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The user impersonating feature.
    /// </summary>
    public record ImpersonatingUser
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the user login type.
        /// </summary>
        public LoginType LoginType { get; set; } = LoginType.Windows;
    }
}
