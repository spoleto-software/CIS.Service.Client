using CIS.Service.Client.Exceptions;
using CIS.Service.Client.Models;
using CIS.Service.Client.Services;
using CIS.Service.Client.Tests.Models;
using Core.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Rest.Azure.OData;

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
            var materialNameList = await provider.LoadObjectValueListAsync<LocalizableString, MaterialName>(new ValueSearchModel
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
            var materialNameList = await provider.LoadObjectValueListAsync<LocalizableString, MaterialName>(new ValueSearchModel
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
            var materialNameList = await provider.LoadObjectValueKeyListAsync<LocalizableString, MaterialName>(new ValueSearchModel
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
            var materialNameList = await provider.LoadObjectValueKeyListAsync<LocalizableString, MaterialName>(new ValueSearchModel
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
        public async Task DeleteWithExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var isException = false;

            // Act
            try
            {
                await provider.DeleteAsync<SaleSlipInternetOrder>(Guid.NewGuid());
            }
            catch (NotFoundException)
            {
                isException = true;
            }

            // Assert
            Assert.That(isException, Is.True);
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
        public async Task UpdateWithExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var isException = false;

            // Act
            try
            {
                await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), new Dictionary<string, object> { { nameof(OnlineOrderBase.DeliveryAddress), "Россия, г Москва, 123, д 123, кв 12345678" } });

                await provider.UpdateOnlyAsync<OnlineOrderBase>(Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

            }
            catch (NotFoundException)
            {
                isException = true;
            }

            // Assert
            Assert.That(isException, Is.True);
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

        [Test]
        public async Task LoadObjectListWithEmptyLSByMaterial()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var materialList = await provider.LoadObjectListAsync<Material>(new SearchModel { Filter = $"{nameof(Material.Identity)}  == Guid.Parse(\"d9d4c357-af4a-4a2f-9ca8-efdd91a33de1\")" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialList, Is.Not.Null);
            });
        }

        /// <summary>
        /// Label has constants in the expressions.<br/>
        /// These constants are translated to SQL as parameters.<br/>
        /// SQL builder must keep the same parameters in Select, GroupBy, OrderBy parts!
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task LoadObjectValueKeyListWithComplexLabelByManufacturerOfTrademark()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var objKeyList1 = await provider.LoadObjectValueKeyListAsync<string>(nameof(ManufacturerOfTrademark),
                new()
                {
                    Rows = 10,
                    Column = "Label",
                    GroupBy = "Label"
                });

            var objKeyList2 = await provider.LoadObjectValueKeyListAsync<string>(nameof(ManufacturerOfTrademark),
                new()
                {
                    Rows = 10,
                    Column = "Label",
                    Distinct = true
                });


            var objKeyList3 = await provider.LoadObjectValueKeyListAsync<string>(nameof(ManufacturerOfTrademark),
                new()
                {
                    Rows = 10,
                    Column = "Label",
                    Order = "Label",
                    Distinct = true
                });


            var objKeyList4 = await provider.LoadObjectValueKeyListAsync<string>(nameof(ManufacturerOfTrademark),
                new()
                {
                    Rows = 10,
                    Column = "Label",
                    GroupBy = "Label",
                    Order = "Label",
                    Distinct = true
                });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(objKeyList1, Is.Not.Null);
                Assert.That(objKeyList2, Is.Not.Null);
                Assert.That(objKeyList3, Is.Not.Null);
                Assert.That(objKeyList4, Is.Not.Null);
            });
        }

        [Test]
        public async Task BulkInsertByLanguage()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var objList = new List<Language>
            {
                new() { Identity = Guid.NewGuid(), Code = "tt1", Name = "test1", Order = 123456789 },
                new() { Identity = Guid.NewGuid(), Code = "tt2", Name = "test2", Order = 123456789 },
                new() { Identity = Guid.NewGuid(), Code = "tt3", Name = "test3", Order = 123456789 }
            };

            // Act
            await provider.BulkInsertAsync(objList);

            // Assert
            Assert.Pass(); // no exceptions mean it works well.
        }

        //https://d-fens.ch/2017/03/01/converting-odataqueryoptions-into-linq-expressions-in-c/
        //https://stackoverflow.com/questions/16445062/how-to-transform-odata-filter-to-a-linq-expression/16447514#16447514
        //https://stackoverflow.com/questions/55307370/how-to-take-odata-queryable-web-api-endpoint-filter-and-map-it-from-dto-object/55344775#55344775
        [Test]
        public void ODataTest()
        {
            // Arrange
            var oDataCriteria = new ODataQuery<Language>(x => x.Order == 1 && x.Name.Contains("test"))
            {
                OrderBy = "Name",
                Top = 100,
                Skip = 0,
                Expand = "Model"
            };

            // Act
            var filter = oDataCriteria.Filter;
            var toString = oDataCriteria.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(filter, Is.EqualTo("Order eq 1 and Name/any(c: c eq 'test')"));
                Assert.That(toString, Is.EqualTo("$filter=Order eq 1 and Name/any(c: c eq 'test')&$orderby=Name&$expand=Model&$top=100&$skip=0"));
            });
        }

        [Test]
        public async Task ExecuteSPTest()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var classParams = new Dictionary<string, object>
                {
                    {"orderedmodels", "[]" },
                    {"idmodel", Guid.Parse("ada17bd3-bf9e-4b69-81d3-6a2aacd31644")},
                    {"idcolor", Guid.Parse("d494b555-9c20-4375-9e17-73fc738ec9ce")},
                    {"colorcode", "P30"},
                    {"idshop", Guid.Parse("9b2537e1-c9d0-47a5-a115-3cd82c385cd6")},
                    {"sizes", "[{\"idsize\":\"2dc72726-4f85-4c6a-944d-b6f7dc5811ac\",\"cnt\":0}]" },
                    {"idstate", Guid.Parse("1df01cea-7d47-48bc-b19c-56ac510b02a3")},
                    {"idorder", Guid.Parse("b3fa0896-8b2d-4fb0-a2ad-7adf7ecd523e")},
                    {"price", 294D},
                    {"idcurrency", Guid.Parse("0e251393-ba0f-4704-9001-c6a0e36b248f")},
                    {"dateinput", DateTime.Now},
                    {"tmp_kit", null},
                    {"idrow", Guid.Parse("04c880d3-90bb-487a-b82a-2d8c4a6151fb")},
                    {"photonum", null},
                    {"custom_filter_string", null},
                    {"is_need_rfid", false },
                    {"idcountry", null}
                };

            // Act
            await provider.ExecuteSPAsync<EmptyResult>("\"_updateOrderedModel\"", classParams);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task LoadObjectValueKeyListWithFuncInColumnByColorParent()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();

            // Act
            var colorList = await provider.LoadObjectValueKeyListAsync<LocalizableString, Color>(new ValueSearchModel
            {
                Order = $"@{nameof(Color.Parent)}.{nameof(Color.Name)} DESC",
                Column = $"@{nameof(Color.Parent)}.{nameof(Color.Name)}",
                Rows = 10,
                Distinct = true
            });

            var colorListWithFunc = await provider.LoadObjectValueKeyListAsync<string, Color>(new ValueSearchModel
            {
                Order = $"SqlEx.SimplifyString(@{nameof(Color.Parent)}.{nameof(Color.Name)}, \"ru\") DESC",
                Column = $"SqlEx.SimplifyString(@{nameof(Color.Parent)}.{nameof(Color.Name)}, \"ru\")",
                Rows = 10,
                Distinct = true
            });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(colorList, Is.Not.Null);
                Assert.That(colorListWithFunc, Is.Not.Null);
            });
        }

        [Test]
        public async Task ReadByOrderedModel()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var randomId = Guid.NewGuid();

            // Act
            var orderedModel = await provider.ReadAsync<OrderedModel>(randomId);

            // Assert
            Assert.That(orderedModel, Is.Null);
        }

        [Test]
        public async Task ReadByOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var orderId = Guid.Parse("8335bf5b-10ef-4cdd-97ae-fe63f551d81a");

            // Act
            var orderedModel = await provider.ReadAsync<Order>(orderId);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task LoadObjectListWithSqlExInByShop()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var filter = "SqlEx.In(Identity, new Guid[] { Guid.Parse(\"c39605e0-0b87-4a26-bcef-8c88abfbe4d3\") })";

            // Act
            var shops = await provider.LoadObjectListAsync<Shop>(new SearchModel
            {
                Filter = filter,
                Select = $"{nameof(Shop.Identity)}, {nameof(Shop.FullName)}"
            });

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task CreateByLegalPerson()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var legalPerson = new LegalPerson
            {
                Name = "ИП Иванов Иван Иваныч Test",
                Address = "тут адрес",
                BankRequisites = "р/с xxx в банке",
                Inn = "123456789123",
                ShortName = "Иванов Иван Иваныч Test",
                RefLegalKindId = Guid.Parse("4e2cf109-d825-4dc8-937c-c85c33863b01"),
                IsResident = true,
                IsActive = true,
                FirmAddressId = Guid.Parse("64b2847c-9cac-4f53-a919-caf93fa42b01")
            };

            // Act
            var createdLegalPerson = await provider.CreateAsync(legalPerson);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task LoadObjectListOrderByFnAsync()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IPersistentCisServiceProvider>();
            var info = ConfigurationHelper.GetOrderByInfo();

            // Act
            var employees = await provider.LoadObjectListFnAsync<Employee>(info.FuncName, info.OrderByFields, info.Args?.Select(x => x.GetRealValue()).ToArray());

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(employees, Is.Not.Null);
            });
        }
    }
}