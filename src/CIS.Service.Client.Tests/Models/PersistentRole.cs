using CIS.Service.Client.Interfaces;
using System;

namespace CIS.Service.Client.Tests.Models
{
    public class PersistentRole : IdentityObject, IBody
    {
        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public string Description { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
