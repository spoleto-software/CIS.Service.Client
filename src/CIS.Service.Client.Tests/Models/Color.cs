using CIS.Service.Client.Interfaces;
using Core.Common;

namespace CIS.Service.Client.Tests.Models
{
    public class Color : IdentityObject, IBody
    {
        public LocalizableString Name { get; set; }

        public Guid? ParentId { get; set; }

        public Color Parent { get; set; }
    }
}
