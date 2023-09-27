using System.Text;
using CIS.Service.Client.Models;
using Microsoft.Extensions.Configuration;

namespace CIS.Service.Client.Tests
{
    internal static class ConfigurationHelper
    {
        private static readonly IConfigurationRoot _config;

        static ConfigurationHelper()
        {
            _config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true)
               .AddJsonFile("appsettings.Development.json", optional: true)
               .AddUserSecrets("afa513ac-4172-4482-86eb-3b47ad6ef49d")
               .Build();
        }

        public static IConfigurationRoot Configuration => _config;

        public static ImpersonatingUser GetImpersonatingUser()
        {
            var user = _config.GetSection(nameof(ImpersonatingUser)).Get<ImpersonatingUser>();

            return user;
        }
    }
}
