using System;

namespace CIS.Service.Client.Models
{
    public class WebApiOption
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string LoginType { get; set; }

        public string WebAPIEndpointAddress { get; set; }

        public string WebAPITokenEndpointAddress { get; set; }

        public string ClientCode { get; set; }

        public string ClientSecret { get; set; }

        public MediaType MediaType { get; set; } = MediaType.Json;
    }
}
