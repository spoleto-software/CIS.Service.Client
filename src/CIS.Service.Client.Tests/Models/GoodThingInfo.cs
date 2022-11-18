using System;
using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Tests.Models
{
    internal class GoodThingInfo : IdentityObject, IBody
    {
        public string Code { get; set; }

        public decimal FirstPrice { get; set; }

        public Guid? ModelId { get; set; }

        public Guid? ColorId { get; set; }

        public byte[] PreviewImage { get; set; }

        public Guid? TrademarkId { get; set; }
    }
}
