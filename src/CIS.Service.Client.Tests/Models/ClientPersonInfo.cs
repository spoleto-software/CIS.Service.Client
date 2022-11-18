using CIS.Service.Client.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIS.Service.Client.Tests.Models
{
    public class ClientPersonInfo : IdentityObject, IBody
    {
        public decimal? AverageSale { get; set; }
    }
}
