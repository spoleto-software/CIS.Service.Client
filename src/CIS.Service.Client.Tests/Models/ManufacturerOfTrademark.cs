using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Tests.Models
{
    public class ManufacturerOfTrademark : IdentityObject, IBody
    {
        public Guid ManufacturerId { get; set; }

        public Guid? SupplierId { get; set; }

        public Guid TrademarkId { get; set; }

        public Guid? ManufactureTypeId { get; set; }

        public bool? Actual { get; set; }
    }
}
