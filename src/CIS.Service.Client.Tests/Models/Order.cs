using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Tests.Models
{
    public class Order : IdentityObject
    {
        public string Name { get; set; }

        public int Number { get; set; }

        public Guid? ManufacturerOfTrademarkId { get; set; }
    }
}
