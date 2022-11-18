using CIS.Service.Client.Exceptions;
using CIS.Service.Client.Models;
using CIS.Service.Client.Services;
using CIS.Service.Client.Tests.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CIS.Service.Client.Tests.Tests
{
    public class PersistentCisServiceProviderTests : BaseProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoadObjectValueListWithFuncInColumnByMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var materialNameList = await provider.LoadObjectValueListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"{nameof(MaterialName.Name)} DESC",
                Column = $"{nameof(MaterialName.Name)}",
                Rows = 10,
                Distinct = true
            });

            var materialNameWithFuncList = await provider.LoadObjectValueListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"SqlEx.SimplifyString({nameof(MaterialName.Name)}, \"ru\") DESC",
                Column = $"SqlEx.SimplifyString({nameof(MaterialName.Name)}, \"ru\")",
                Rows = 10,
                Distinct = true
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialNameList, Is.Not.Null);
                Assert.That(materialNameWithFuncList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectValueListWithFuncAndLabelInColumnByMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var materialNameList = await provider.LoadObjectValueListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"Label",
                Column = $"Label",
                Rows = 10,
                Distinct = true
            });

            var materialNameListWuthFunc = await provider.LoadObjectValueListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"SqlEx.SimplifyString(Label, \"ru\")",
                Column = $"SqlEx.SimplifyString(Label, \"ru\")",
                Rows = 10,
                Distinct = true
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialNameList, Is.Not.Null);
                Assert.That(materialNameListWuthFunc, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectValueKeyListWithFuncInColumnByMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var materialNameList = await provider.LoadObjectValueKeyListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"{nameof(MaterialName.Name)} DESC",
                Column = $"{nameof(MaterialName.Name)}",
                Rows = 10,
                Distinct = true
            });

            var materialNameListWithFunc = await provider.LoadObjectValueKeyListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"SqlEx.SimplifyString({nameof(MaterialName.Name)}, \"ru\") DESC",
                Column = $"SqlEx.SimplifyString({nameof(MaterialName.Name)}, \"ru\")",
                Rows = 10,
                Distinct = true
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialNameList, Is.Not.Null);
                Assert.That(materialNameListWithFunc, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectValueKeyListWithFuncAndLabelInColumnByMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var materialNameList = await provider.LoadObjectValueKeyListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"Label",
                Column = $"Label",
                Rows = 10,
                Distinct = true
            });

            var materialNameListWithFunc = await provider.LoadObjectValueKeyListAsync<string, MaterialName>(new ValueSearchModel
            {
                Order = $"SqlEx.SimplifyString(Label, \"ru\")",
                Column = $"SqlEx.SimplifyString(Label, \"ru\")",
                Rows = 10,
                Distinct = true
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialNameList, Is.Not.Null);
                Assert.That(materialNameListWithFunc, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectValueListWithFuncAndNestedPropertyInColumnByOnlineOrderBase()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var filter = $"{nameof(OnlineOrderBase.Number)} = 67529 OR {nameof(OnlineOrderBase.Number)} = 27103 OR {nameof(OnlineOrderBase.Number)} = 19524";

            // Act
            var employeeValueList1 = await provider.LoadObjectValueListAsync<string, OnlineOrderBase>(new ValueSearchModel
            {
                Filter = filter,
                Column = $"{nameof(OnlineOrderBase.Employee)}.Label",
                Distinct = true
            });

            var employeeValueList2 = await provider.LoadObjectValueListAsync<string, OnlineOrderBase>(new ValueSearchModel
            {
                Filter = filter,
                Column = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Order = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Distinct = true
            });

            var employeeValueList3 = await provider.LoadObjectValueListAsync<string, OnlineOrderBase>(new ValueSearchModel
            {
                Filter = filter,
                Column = $"{nameof(OnlineOrderBase.Employee)}.Label",
                Order = $"{nameof(OnlineOrderBase.Employee)}.Label",
                GroupBy = $"{nameof(OnlineOrderBase.Employee)}.Label",
            });

            var employeeValueList4 = await provider.LoadObjectValueListAsync<string>(nameof(OnlineOrderBase), new ValueSearchModel
            {
                Filter = filter,
                Column = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Order = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Distinct = true
            });

            var employeeValueList5 = await provider.LoadObjectValueListAsync<string>(nameof(OnlineOrderBase), new ValueSearchModel
            {
                Filter = filter,
                Column = $"{nameof(OnlineOrderBase.Employee)}.Label",
                Order = $"{nameof(OnlineOrderBase.Employee)}.Label",
                GroupBy = $"{nameof(OnlineOrderBase.Employee)}.Label",
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employeeValueList1, Is.Not.Null);
                Assert.That(employeeValueList2, Is.Not.Null);
                Assert.That(employeeValueList3, Is.Not.Null);
                Assert.That(employeeValueList4, Is.Not.Null);
                Assert.That(employeeValueList5, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectValueKeyListWithFuncAndNestedPropertyInColumnByOnlineOrderBase()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var filter = $"{nameof(OnlineOrderBase.Number)} = 67529 OR {nameof(OnlineOrderBase.Number)} = 27103 OR {nameof(OnlineOrderBase.Number)} = 19524";

            var employeeValueKeyList1 = await provider.LoadObjectValueKeyListAsync<string, OnlineOrderBase>(new ValueSearchModel
            {
                Filter = filter,
                Column = $"{nameof(OnlineOrderBase.Employee)}.Label",
                Distinct = true
            });

            var employeeValueKeyList2 = await provider.LoadObjectValueKeyListAsync<string, OnlineOrderBase>(new ValueSearchModel
            {
                Filter = filter,
                Column = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Order = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Distinct = true
            });

            var employeeValueKeyList3 = await provider.LoadObjectValueKeyListAsync<string, OnlineOrderBase>(new ValueSearchModel
            {
                Filter = filter,
                Column = $"{nameof(OnlineOrderBase.Employee)}.Label",
                Order = $"{nameof(OnlineOrderBase.Employee)}.Label",
                GroupBy = $"{nameof(OnlineOrderBase.Employee)}.Label",
            });

            var employeeValueKeyList4 = await provider.LoadObjectValueKeyListAsync<string>(nameof(OnlineOrderBase), new ValueSearchModel
            {
                Filter = filter,
                Column = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Order = $"SqlEx.SimplifyString({nameof(OnlineOrderBase.Employee)}.Label, \"ru\")",
                Distinct = true
            });

            var employeeValueKeyList5 = await provider.LoadObjectValueKeyListAsync<string>(nameof(OnlineOrderBase), new ValueSearchModel
            {
                Filter = filter,
                Column = $"{nameof(OnlineOrderBase.Employee)}.Label",
                Order = $"{nameof(OnlineOrderBase.Employee)}.Label",
                GroupBy = $"{nameof(OnlineOrderBase.Employee)}.Label",
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employeeValueKeyList1, Is.Not.Null);
                Assert.That(employeeValueKeyList2, Is.Not.Null);
                Assert.That(employeeValueKeyList3, Is.Not.Null);
                Assert.That(employeeValueKeyList4, Is.Not.Null);
                Assert.That(employeeValueKeyList5, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListByMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var materialNames = await provider.LoadObjectListAsync<MaterialName>(new SearchModel { Filter = "Identity = Guid.Parse(\"079e3f60-1151-454b-9c67-006cb965fe19\") OR Identity = Guid.Parse(\"079e3f60-1151-454b-9c67-006cb965fe09\")" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialNames, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListByGoodThingInfo()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var goodThingList = await provider.LoadObjectListAsync<GoodThingInfo>(new SearchModel
            {
                Filter = $"{nameof(GoodThingInfo.Code)} = \"12463093\"",
                //GroupBy = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}",
                Order = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}",
                Select = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}"
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(goodThingList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectByFilterByEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var employee = await provider.LoadObjectByFilterAsync<Employee>(new SearchModel { Filter = $"{nameof(Employee.Name)} != \"test\"" });


            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectByFilterWithCustomSelectByEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var employee = await provider.LoadObjectByFilterAsync<Employee>(new SearchModel
            {
                Filter = $"{nameof(Employee.SamAccountName)} != \"i.ivanov\" && {nameof(Employee.IsActive)} == true",
                Select = $"{nameof(Employee.Identity)},{nameof(Employee.DateReceipt)}"
            });


            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employee, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectByFilterWithCustomSelectByGoodThingInfo()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var goodThing = await provider.LoadObjectByFilterAsync<GoodThingInfo>(new SearchModel
            {
                Filter = $"{nameof(GoodThingInfo.Code)} = \"12463093\"",
                GroupBy = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}",
                Select = $"{nameof(GoodThingInfo.TrademarkId)},{nameof(GoodThingInfo.ColorId)}"
            });


            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(goodThing, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListCodeFnByObjectNumber()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var newNumberList = await provider.LoadObjectListCodeFnAsync<ObjectNumber>("GetNewNumber", "BaseSlip", false, 1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(newNumberList, Is.Not.Null);
                Assert.That(newNumberList, Has.Count.EqualTo(1));
                Assert.That(newNumberList[0].Number, Is.GreaterThanOrEqualTo(0));
            });
        }

        [Test]
        public async Task LoadObjectListSPByPersistentRole()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var rolesList = await provider.LoadObjectListFnAsync<PersistentRole>("\"CIS\".\"_getRolesFn\"", "AF8EBF72-6CA2-4A65-88D0-34B248FE0D6E");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rolesList, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetCountObjectListByEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var count = await provider.GetCountObjectListAsync<Employee>(new FilterModel { Filter = $"{nameof(Employee.Name)} = \"Иван\"" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(count, Is.GreaterThanOrEqualTo(0));
            });
        }

        [Test]
        public async Task LoadObjectListWithoutFilterByLanguage()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var languageList = await provider.LoadObjectListAsync<Language>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(languageList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListByCountry()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var countryList = await provider.LoadObjectListAsync<Country>(new SearchModel { Order = nameof(Country.AccCode) });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(countryList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListByClientPersonInfo()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var clientPersonInfoList = await provider.LoadObjectListAsync<ClientPersonInfo>(new SearchModel { Filter = $@"{nameof(ClientPersonInfo.Identity)} == Guid.Parse(""44413022-36FB-4F84-84E4-98D237BF0D2D"")" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(clientPersonInfoList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListByReturnSlip()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var returnSlipList = await provider.LoadObjectListAsync<ReturnSlip>(new SearchModel { Filter = $@"{nameof(ReturnSlip.AdditionalStateId)} == Guid.Parse(""79247678-11CB-E611-A97B-0050568329F0"")" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(returnSlipList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListWithExecuteExpressionByEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var employeeList = await provider.LoadObjectListAsync<Employee>(new SearchModel { ExecuteExpression = $"TPF.GetPresentEmployees(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\"), DateTime.Today, true, Guid.Parse(\"BB01D476-D2BE-4F59-A190-002DA10A2867\"))" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employeeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectListWithFuncInFilterByEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var employeeList = await provider.LoadObjectListAsync<Employee>(new SearchModel { Filter = "SqlEx.In(Identity, TPF.GetShopIdsByParent(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\")))" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employeeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task ReadByOnlineClient()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var onlineClient = await provider.ReadAsync<OnlineClient>(Guid.Parse("e6dfca91-058d-4346-9480-7de9afd24542"));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(onlineClient, Is.Not.Null);
            });
        }

        [Test]
        public async Task UpdateByOnlineOrderBase()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4613"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task DeleteWithoutExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.DeleteAsync<SaleSlipInternetOrder>(Guid.NewGuid());

            // Assert
            Assert.Pass();
        }

        [Test]
        public void DeleteWithExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();


            // Assert
            Assert.Throws<NotFoundException>(async () =>
            {
                // Act
                await provider.DeleteAsync<SaleSlipInternetOrder>(Guid.NewGuid());

            });
        }

        [Test]
        public async Task UpdateWithoutExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), new Dictionary<string, object> { { nameof(OnlineOrderBase.DeliveryAddress), "Россия, г Москва, 123, д 123, кв 12345678" } }, false);

            await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" }, false);

            // Assert
            Assert.Pass();
        }

        [Test]
        public void UpdateWithExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();


            // Assert
            Assert.Throws<NotFoundException>(async () =>
            {
                // Act
                await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), new Dictionary<string, object> { { nameof(OnlineOrderBase.DeliveryAddress), "Россия, г Москва, 123, д 123, кв 12345678" } });

                await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

            });
        }

        [Test]
        public async Task DeleteBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.DeleteAsync<SaleSlipInternetOrder>(Guid.NewGuid()); // test for no exceptions if the deleting object is not found

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task ExecuteWithoutArgsBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.ExecuteAsync(() => new SaleSlipInternetOrder
            {
                Identity = Guid.Parse("BDA0EC3C-743A-4E89-A769-00009AB4DD0E"),
                DateEnd = DateTime.Parse("2022-06-02"),
                Note = "Test slip",
                PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
            },
            "CreateSlip");

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task ExecuteWithArgsBySaleSlip()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.ExecuteAsync(() => new SaleSlip
            {
                DateEnd = DateTime.Parse("2022-06-02"),
                Note = "Test slip",
                PointSiteTDId = Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A67")
            },
            "CloseSlip", false);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task ExecuteWithArgsBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var saleSlipId = Guid.NewGuid();
            var codes = new List<string>() { "12639760" };

            // Act
            await provider.ExecuteAsync(() => new SaleSlipInternetOrder
            {
                FiscalRegisterId = Guid.Parse("F3EA7C57-9EB0-4D56-970C-6992AEAF06D2"),
                PointSiteTDId = Guid.Parse("21446014-c597-4bc6-935a-4cd9a9145711")
            },
             "CreateSlip",
             saleSlipId, codes, Guid.Parse("8217683B-F6F1-4DF7-9313-8F328DEF1DF2"), Guid.Parse("21446014-c597-4bc6-935a-4cd9a9145710"), Guid.Parse("A88626AB-F0F7-4589-A624-081838A09037"));

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task ExecuteWithoutArgsBySaleSlipInternetOrderId()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.ExecuteAsync<SaleSlipInternetOrder>(Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A68"), "CalcBonusAmount");

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task ExecuteWithArgsBySaleSlipId()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            await provider.ExecuteAsync<SaleSlip>(Guid.Parse("ED8B4DF9-9D4B-46E0-979E-000041814A68"), "CloseSlip", false);

            // Assert
            Assert.Pass();
        }
    }
}