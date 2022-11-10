using CIS.Service.Client.Interfaces;
using System;

namespace CIS.Service.Client.ConsoleApp
{
    public class OnlineOrderBase : IdentityObject
    {
        public long Number { get; set; }

        public string DeliveryAddress { get; set; }

        public Guid? EmployeeId { get; set; }

        public Employee Employee { get; set; }
    }
}
