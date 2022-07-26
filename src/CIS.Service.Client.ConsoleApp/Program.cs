using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CIS.Service.Client.Helpers;
using CIS.Service.Client.Models;
using CIS.Service.Client.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace CIS.Service.Client.ConsoleApp
{
    internal class Program
    {
        private static IConfiguration _configuration;

        private static IConfiguration Configuration
        {
            get
            {
                if (_configuration == null)
                {
                    _configuration = ConfigureConfiguration(new ConfigurationBuilder(), Environment.GetEnvironmentVariable("NETCORE_ENVIRONMENT"))
                        .Build();
                }

                return _configuration;
            }
        }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var testObj = new TestClass
            {
                Name = "test123",
                PreviewImage = new byte[5] { 1, 22, 39, 44, 9 }
            };

            var json = JsonHelper.ToJson(testObj);
            var fromJson = JsonHelper.FromJson<TestClass>(json);
            if (!testObj.PreviewImage.SequenceEqual(fromJson.PreviewImage))
            {
                Console.WriteLine("Not good!");
            }

            var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpClient();

                   services.AddOptions();
                   services.Configure<WebApiOption>(Configuration.GetSection(nameof(WebApiOption)));

                   //var clientCode = Configuration["ClientCode"];
                   //var clientSecret = Configuration["ClientSecret"];
                   //var userName = Configuration["UserName"];
                   //var userId = Guid.Parse(Configuration["UserId"]);
                   //var pwd = Configuration["Password"];

                   //services.AddSingleton(_ =>
                   //Options.Create(
                   //    new WebApiOption
                   //    {
                   //        ClientCode = clientCode,
                   //        ClientSecret = clientSecret,
                   //        LoginType = "Database",
                   //        UserName = userName,
                   //        Password = pwd,
                   //        UserId = userId,
                   //        WebAPIEndpointAddress = "http://127.0.0.1:8000/",
                   //        WebAPITokenEndpointAddress = "http://127.0.0.1:8000/",
                   //        MediaType = MediaType.Json
                   //    }));

                   services.AddSingleton<ICisServiceTokenProvider, CisServiceCredentialsTokenProvider>();
                   services.AddSingleton<ICisServiceProvider, CisServiceProvider>();
                   services.AddSingleton<IPersistentCisServiceProvider, PersistentCisServiceProvider>();

               }).UseConsoleLifetime();

            var host = builder.Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var services = serviceScope.ServiceProvider;

                try
                {
                    var provider = services.GetRequiredService<ICisServiceProvider>();
                    var persistentProvider = services.GetRequiredService<IPersistentCisServiceProvider>();

                    var employee = await provider.LoadObjectByFilter<Employee>(new SearchModel { Filter=$"{nameof(Employee.Name)} != \"test\""});

                    // метод без аргументов:
                    await persistentProvider.Execute<SaleSlip>(Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A68"), "CalcBonusAmount");
                    // метод с аргументами:
                    await persistentProvider.Execute<SaleSlip>(Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A68"), "CloseSlip", false);

                    var saleSlip = new SaleSlip
                    {
                        Identity = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67"),
                        DateEnd = DateTime.Parse("2022-06-02"),
                        Note = "Test slip",
                        PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
                    };

                    // метод без аргументов:
                    await persistentProvider.Execute(() => new SaleSlip
                    {
                        Identity = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67"),
                        DateEnd = DateTime.Parse("2022-06-02"),
                        Note = "Test slip",
                        PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
                    },
                    "CalcBonusAmount");

                    // метод с аргументами:
                    await persistentProvider.Execute(() => new SaleSlip
                    {
                        DateEnd = DateTime.Parse("2022-06-02"),
                        Note = "Test slip",
                        PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
                    },
                    "CloseSlip", false);

                    var employeeList = await persistentProvider.LoadObjectListAsync<Employee>(new SearchModel { ExecuteExpression = $"TPF.GetPresentEmployees(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\"), DateTime.Today, true, Guid.Parse(\"BB01D476-D2BE-4F59-A190-002DA10A2867\"))" });

                    var testExpression = await persistentProvider.LoadObjectListAsync<Employee>(new SearchModel { Filter = "SqlEx.In(Identity, TPF.GetShopIdsByParent(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\")))" });

                    var goodThing = await persistentProvider.LoadObjectByFilter<GoodThingInfo>(new SearchModel
                    {
                        Filter = $"{nameof(GoodThingInfo.Code)} = \"12463093\"",
                        GroupBy = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}",
                        Select = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}"
                    });
                    var onlineClient = await persistentProvider.ReadAsync<OnlineClient>(Guid.Parse("e6dfca91-058d-4346-9480-7de9afd24542"));

                    await persistentProvider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4613"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

                    Console.WriteLine("Ok");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error Occured");
                }
            }
        }

        private static IConfigurationBuilder ConfigureConfiguration(IConfigurationBuilder configurationBuilder, string environmentName)
        {
            return configurationBuilder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
