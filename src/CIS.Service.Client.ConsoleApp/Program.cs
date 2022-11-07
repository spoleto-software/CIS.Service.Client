using System;
using System.Collections.Generic;
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

// Примеры работы с клиентом
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
                    //var employee = await provider.LoadObjectByFilter<Employee>(new SearchModel { Filter=$"{nameof(Employee.Name)} != \"test\""});

                    var newNumber1 = await persistentProvider.LoadObjectListCodeFnAsync<ObjectNumber>("GetNewNumber", "BaseSlip", false, 1);
                    var newNumber2 = await provider.LoadObjectListCodeFnAsync<ObjectNumber>("GetNewNumber", "BaseSlip", false, 1);

                    var roles1 = await provider.LoadObjectListSPAsync<PersistentRole>("\"CIS\".\"_getRolesFn\"", "AF8EBF72-6CA2-4A65-88D0-34B248FE0D6E");
                    var roles2 = await persistentProvider.LoadObjectListFnAsync<PersistentRole>("\"CIS\".\"_getRolesFn\"", "AF8EBF72-6CA2-4A65-88D0-34B248FE0D6E");
                    var count = await persistentProvider.GetCountObjectListAsync<Employee>(new FilterModel { Filter = $"{nameof(Employee.Name)} = \"Иван\"" });
                    var languages = await persistentProvider.LoadObjectListAsync<Language>();
                    try
                    {
                        //await persistentProvider.DeleteAsync<SaleSlipInternetOrder>(Guid.NewGuid());

                        //await persistentProvider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), new Dictionary<string, object> { { nameof(OnlineOrderBase.DeliveryAddress), "Россия, г Москва, 123, д 123, кв 12345678" } });

                        //await persistentProvider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

                    }
                    catch (Exception e)
                    {

                    }

                    var countryList = await persistentProvider.LoadObjectListAsync<Country>(new SearchModel { Order = nameof(Country.AccCode)});
                    var clientPersonInfos = await persistentProvider.LoadObjectListAsync<ClientPersonInfo>(new SearchModel { Filter = $@"{nameof(ClientPersonInfo.Identity)} == Guid.Parse(""44413022-36FB-4F84-84E4-98D237BF0D2D"")" });
                    var returnSlips = await persistentProvider.LoadObjectListAsync<ReturnSlip>(new SearchModel { Filter = $@"{nameof(ReturnSlip.AdditionalStateId)} == Guid.Parse(""79247678-11CB-E611-A97B-0050568329F0"")" });

                    var employee = await persistentProvider.LoadObjectByFilterAsync<Employee>(new SearchModel
                    {
                        Filter = $"{nameof(Employee.SamAccountName)} != \"i.ivanov\" && {nameof(Employee.IsActive)} == true",
                        Select = $"{nameof(Employee.Identity)},{nameof(Employee.DateReceipt)}"
                    });

                    // метод без аргументов:
                    //await persistentProvider.Execute<SaleSlipInternetOrder>(Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A68"), "CalcBonusAmount");
                    //// метод с аргументами:
                    //await persistentProvider.Execute<SaleSlip>(Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A68"), "CloseSlip", false);

                    var saleSlip = new SaleSlip
                    {
                        Identity = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67"),
                        DateEnd = DateTime.Parse("2022-06-02"),
                        Note = "Test slip",
                        PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
                    };

                    var saleSlipId = Guid.NewGuid();
                    var codes = new List<string>() { "12639760" };

                    await persistentProvider.ExecuteAsync(() => new SaleSlipInternetOrder
                    {
                        FiscalRegisterId = Guid.Parse("F3EA7C57-9EB0-4D56-970C-6992AEAF06D2"),
                        PointSiteTDId = Guid.Parse("21446014-c597-4bc6-935a-4cd9a9145711")
                    },
                    "CreateSlip",
                    saleSlipId, codes, Guid.Parse("8217683B-F6F1-4DF7-9313-8F328DEF1DF2"), Guid.Parse("21446014-c597-4bc6-935a-4cd9a9145710"), Guid.Parse("A88626AB-F0F7-4589-A624-081838A09037"));

                    // метод без аргументов:
                    await persistentProvider.ExecuteAsync(() => new SaleSlipInternetOrder
                    {
                        Identity = Guid.Parse("BDA0EC3C-743A-4E89-A769-00009AB4DD0E"),
                        DateEnd = DateTime.Parse("2022-06-02"),
                        Note = "Test slip",
                        PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
                    },
                    "CreateSlip");

                    // метод с аргументами:
                    await persistentProvider.ExecuteAsync(() => new SaleSlip
                    {
                        DateEnd = DateTime.Parse("2022-06-02"),
                        Note = "Test slip",
                        PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
                    },
                    "CloseSlip", false);

                    var employeeList = await persistentProvider.LoadObjectListAsync<Employee>(new SearchModel { ExecuteExpression = $"TPF.GetPresentEmployees(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\"), DateTime.Today, true, Guid.Parse(\"BB01D476-D2BE-4F59-A190-002DA10A2867\"))" });

                    var testExpression = await persistentProvider.LoadObjectListAsync<Employee>(new SearchModel { Filter = "SqlEx.In(Identity, TPF.GetShopIdsByParent(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\")))" });

                    var goodThing = await persistentProvider.LoadObjectByFilterAsync<GoodThingInfo>(new SearchModel
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
