using System;
using System.ComponentModel.DataAnnotations;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The light login model without password.
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Gets or sets the client code.
        /// </summary>
        [Required(ErrorMessage = "Client code is not specified.")]
        public string ClientCode { get; set; }

        /// <summary>
        /// Gets or sets the client secret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the login type.
        /// </summary>
        [Required(ErrorMessage = "Login type is not specified.")]
        public string LoginType { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required(ErrorMessage = "UserName is not specified.")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the user Id.
        /// </summary>
        [Required(ErrorMessage = "UserId is not specified.")]
        public Guid UserId { get; set; }
    }
}
