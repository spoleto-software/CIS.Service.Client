using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.ConsoleApp
{
    public class OnlineOrderBase : IdentityObject
    {
        public long Number { get; set; }

        public string DeliveryAddress { get; set; }
    }
}
