﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CIS.Service.Client.Models
{
    /// <summary>
    /// The login model for Windows authentication based on Negotiate.
    /// </summary>
    public class LoginModelNegotiate : LoginModelBase
    {
        /// <summary>
        /// Gets or sets the user Id.
        /// </summary>
        [Required(ErrorMessage = "UserId is not specified.")]
        public Guid UserId { get; set; }
    }
}
