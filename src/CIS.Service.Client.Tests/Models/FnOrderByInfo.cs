using CIS.Service.Client.Models;

namespace CIS.Service.Client.Tests.Models
{
    public class FnOrderByInfo
    {
        public string FuncName { get; set; }

        public string OrderByFields { get; set; }

        public List<WebValue> Args { get; set; }
    }
}
