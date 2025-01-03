﻿using CIS.Service.Client.Models;
using CIS.Service.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CIS.Service.Client.Tests.Tests
{
    public class BaseProviderTest
    {
        private ServiceProvider _serviceProvider;

        protected ServiceProvider ServiceProvider => _serviceProvider;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var services = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
            services.AddHttpClient();

            services.AddHttpClient(CisServiceNegotiateTokenProvider.NegotiateName)
                .ConfigurePrimaryHttpMessageHandler(h => new HttpClientHandler
                {
                    UseDefaultCredentials = true
                });

            services.AddOptions();
            services.Configure<WebApiOption>(ConfigurationHelper.Configuration.GetSection(nameof(WebApiOption)));

            //services.AddSingleton<ICisServiceTokenProvider, CisServiceCredentialsTokenProvider>();
            services.AddSingleton<ICisServiceTokenProvider, CisServiceNegotiateTokenProvider>();
            services.AddSingleton<ICisServiceProvider, CisServiceProvider>();
            services.AddSingleton<IPersistentCisServiceProvider, PersistentCisServiceProvider>();
            services.AddSingleton<IImpersonatingPersistentCisServiceProvider, PersistentCisServiceProvider>();
            services.AddSingleton<IImpersonatingMetaSystemProvider, PersistentCisServiceProvider>();

            _serviceProvider = services.BuildServiceProvider();
        }

        [OneTimeTearDown]
        public void Dispose()
        {
            _serviceProvider.Dispose();
        }
    }
}
