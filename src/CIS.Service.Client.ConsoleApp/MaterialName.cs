using CIS.Service.Client.Interfaces;
using Core.Common;

namespace CIS.Service.Client.ConsoleApp
{
    public class MaterialName : IdentityObject, IBody
    {
        public LocalizableString Name { get; set; }
    }
}
