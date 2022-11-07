using CIS.Service.Client.Interfaces;
using System;

namespace CIS.Service.Client.ConsoleApp
{
    public class ObjectNumber : IdentityObject, IBody
    {
        public long? Number { get; set; }

        public string Prefix { get; set; }

        public int? MaxLengthNumber { get; set; }

        public Guid PersistentClassId { get; set; }
    }
}
