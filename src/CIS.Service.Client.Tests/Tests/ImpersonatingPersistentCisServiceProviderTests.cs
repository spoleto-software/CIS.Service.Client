using CIS.Service.Client.Exceptions;
using CIS.Service.Client.Models;
using CIS.Service.Client.Services;
using CIS.Service.Client.Tests.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CIS.Service.Client.Tests.Tests
{
    public class ImpersonatingPersistentCisServiceProviderTests : BaseProviderTest
    {
        private ImpersonatingUser _user;

        [SetUp]
        public void Setup()
        {
            _user = ConfigurationHelper.GetImpersonatingUser();
        }

        [Test]
        public async Task LoadObjectListByMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var materialNames = await provider.LoadObjectListAsync<MaterialName>(_user, new SearchModel { Filter = "Identity = Guid.Parse(\"079e3f60-1151-454b-9c67-006cb965fe19\") OR Identity = Guid.Parse(\"079e3f60-1151-454b-9c67-006cb965fe09\")" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialNames, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadObjectByFilterByEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var employee = await provider.LoadObjectByFilterAsync<Employee>(_user, new SearchModel { Filter = $"{nameof(Employee.Name)} != \"test\"" });

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var employee = await provider.LoadObjectByFilterAsync<Employee>(_user, new SearchModel
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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var goodThing = await provider.LoadObjectByFilterAsync<GoodThingInfo>(_user, new SearchModel
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
        public async Task LoadObjectListByClientPersonInfo()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var clientPersonInfoList = await provider.LoadObjectListAsync<ClientPersonInfo>(_user, new SearchModel { Filter = $@"{nameof(ClientPersonInfo.Identity)} == Guid.Parse(""44413022-36FB-4F84-84E4-98D237BF0D2D"")" });

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var returnSlipList = await provider.LoadObjectListAsync<ReturnSlip>(_user, new SearchModel { Filter = $@"{nameof(ReturnSlip.AdditionalStateId)} == Guid.Parse(""79247678-11CB-E611-A97B-0050568329F0"")" });

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var employeeList = await provider.LoadObjectListAsync<Employee>(_user, new SearchModel { ExecuteExpression = $"TPF.GetPresentEmployees(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\"), DateTime.Today, true, Guid.Parse(\"BB01D476-D2BE-4F59-A190-002DA10A2867\"))" });

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var employeeList = await provider.LoadObjectListAsync<Employee>(_user, new SearchModel { Filter = "SqlEx.In(Identity, TPF.GetShopIdsByParent(Guid.Parse(\"cb9271b6-c16f-4bc6-bd32-55cd83c70211\")))" });

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var onlineClient = await provider.ReadAsync<OnlineClient>(_user, Guid.Parse("e6dfca91-058d-4346-9480-7de9afd24542"));

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            await provider.UpdateOnlyAsync<OnlineOrderBase>(_user, Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4613"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task DeleteWithoutExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            await provider.DeleteAsync<SaleSlipInternetOrder>(_user, Guid.NewGuid());

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task DeleteWithExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();
            var isException = false;

            // Act
            try
            {
                await provider.DeleteAsync<SaleSlipInternetOrder>(_user, Guid.NewGuid());
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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            await provider.UpdateOnlyAsync<OnlineOrderBase>(_user, Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), new Dictionary<string, object> { { nameof(OnlineOrderBase.DeliveryAddress), "Россия, г Москва, 123, д 123, кв 12345678" } }, false);

            await provider.UpdateOnlyAsync<OnlineOrderBase>(_user, Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" }, false);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task UpdateWithExceptionBySaleSlipInternetOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();
            var isException = false;

            // Act
            try
            {
                await provider.UpdateOnlyAsync<OnlineOrderBase>(_user, Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), new Dictionary<string, object> { { nameof(OnlineOrderBase.DeliveryAddress), "Россия, г Москва, 123, д 123, кв 12345678" } });

                await provider.UpdateOnlyAsync<OnlineOrderBase>(_user, Guid.Parse("e46b3eed-2d9c-4b34-858f-cace5abc4611"), () => new OnlineOrderBase { DeliveryAddress = "Россия, г Москва, 123, д 123, кв 12345678" });

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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            await provider.DeleteAsync<SaleSlipInternetOrder>(_user, Guid.NewGuid()); // test for no exceptions if the deleting object is not found

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task LoadObjectListWithEmptyLSByMaterial()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();

            // Act
            var materialList = await provider.LoadObjectListAsync<Material>(_user, new SearchModel { Filter = $"{nameof(Material.Identity)}  == Guid.Parse(\"d9d4c357-af4a-4a2f-9ca8-efdd91a33de1\")" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(materialList, Is.Not.Null);
            });
        }

        [Test]
        public async Task ReadByOrderedModel()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();
            var randomId = Guid.NewGuid();

            // Act
            var orderedModel = await provider.ReadAsync<OrderedModel>(_user, randomId);

            // Assert
            Assert.That(orderedModel, Is.Null);
        }

        [Test]
        public async Task ReadByOrder()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();
            var orderId = Guid.Parse("8335bf5b-10ef-4cdd-97ae-fe63f551d81a");

            // Act
            var orderedModel = await provider.ReadAsync<Order>(_user, orderId);

            // Assert
            Assert.Pass();
        }

        [Test]
        public async Task LoadObjectListWithSqlExInByShop()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();
            var filter = "SqlEx.In(Identity, new Guid[] { Guid.Parse(\"c39605e0-0b87-4a26-bcef-8c88abfbe4d3\") })";

            // Act
            var shops = await provider.LoadObjectListAsync<Shop>(_user, new SearchModel
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
            var provider = ServiceProvider.GetService<IImpersonatingPersistentCisServiceProvider>();
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
            var createdLegalPerson = await provider.CreateAsync(_user, legalPerson);

            // Assert
            Assert.Pass();
        }
    }
}