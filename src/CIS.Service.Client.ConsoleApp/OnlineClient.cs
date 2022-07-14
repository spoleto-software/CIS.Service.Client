using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.ConsoleApp
{
    public class OnlineClient : IdentityObject, IBody
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}
