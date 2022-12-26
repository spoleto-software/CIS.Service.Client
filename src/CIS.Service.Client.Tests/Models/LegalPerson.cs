using CIS.Service.Client.Interfaces;

namespace CIS.Service.Client.Tests.Models
{
    public class LegalPerson : IdentityObject, IBody
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string BankRequisites { get; set; }

        public string Okpo { get; set; }

        public string Inn { get; set; }

        public string Kpp { get; set; }

        public string Phones { get; set; }

        public Guid? HeadId { get; set; }

        public Guid? ChiefAccountantId { get; set; }

        public Guid? CashierId { get; set; }

        public Guid? ManufactureTypeId { get; set; }

        public string ShortName { get; set; }

        public Guid? RefLegalKindId { get; set; }

        public Guid? ParentId { get; set; }

        public bool IsResident { get; set; }

        public DateTime? DateActsWith { get; set; }

        public bool IsActive { get; set; }

        public Guid? FirmAddressId { get; set; }
    }
}
