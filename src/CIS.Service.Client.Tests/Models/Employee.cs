using CIS.Service.Client.Interfaces;
using System;

namespace CIS.Service.Client.Tests.Models
{
    public class Employee : IdentityObject, IBody
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsActive { get; set; }

        public string SamAccountName { get; set; }

        public DateTime DateReceipt { get; set; }

        public DateTime? DateDismissal { get; set; }
    }
}
