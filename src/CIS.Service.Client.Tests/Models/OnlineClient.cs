using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Tests.Models
{
    public class OnlineClient : IdentityObject, IBody
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
