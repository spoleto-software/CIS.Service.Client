using CIS.Service.Client.Interfaces;
using Core.Common;

namespace CIS.Service.Client.ConsoleApp
{
    public class Country : IdentityObject, IBody
    {
        public LocalizableString Name { get; set; }

        /// <summary>
        /// Код страны
        /// </summary>
        public int PhoneCode { get; set; }

        /// <summary>
        /// Код страны (бухгалтерский)
        /// </summary>
        public int AccCode { get; set; }

        public string CountryCode { get; set; }
    }
}
