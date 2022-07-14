using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.ConsoleApp
{
    public class Employee : IdentityObject, IBody
    {
        public string Name { get; set; }


        public string Surname { get; set; }
    }
}
