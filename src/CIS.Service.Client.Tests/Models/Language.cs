using CIS.Service.Client.Interfaces;
using Core.Common;

namespace CIS.Service.Client.Tests.Models
{
    public class Language : IdentityObject, IBody
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public int? Order { get; set; }

        public string ImageName { get; set; }
    }
}
