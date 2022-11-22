using CIS.Service.Client.Interfaces;
using Core.Common;

namespace CIS.Service.Client.Tests.Models
{
    public class Material : IdentityObject, IBody
    {
        public LocalizableString Name { get; set; }

        public Guid? FabricTypeId { get; set; }
    }
}
