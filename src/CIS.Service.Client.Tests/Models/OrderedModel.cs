using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Tests.Models
{
    public class OrderedModel : IdentityObject
    {
        public Guid ShopId { get; set; }

        public Guid? SizeId { get; set; }

        public Guid ModelId { get; set; }

        public Guid? DocumentId { get; set; }

        public int Quantity { get; set; }

        public bool? IsConfirmed { get; set; }

        public Guid? ColorId { get; set; }

        public string ColorCode { get; set; }
    }
}
