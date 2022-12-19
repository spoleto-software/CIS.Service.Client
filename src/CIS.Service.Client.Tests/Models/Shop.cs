using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIS.Service.Client.Interfaces;
using Core.Common;

namespace CIS.Service.Client.Tests.Models
{
    public class Shop : IdentityObject
    {
        public LocalizableString FullName { get; set; }
    }
}
