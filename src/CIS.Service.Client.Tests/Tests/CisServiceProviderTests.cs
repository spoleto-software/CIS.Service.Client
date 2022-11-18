using CIS.Service.Client.Services;
using CIS.Service.Client.Tests.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CIS.Service.Client.Tests.Tests
{
    public class CisServiceProviderTests : BaseProviderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task LoadObjectListCodeFnByObjectNumber()
        {
            // Arrange
            var provider = ServiceProvider.GetService<ICisServiceProvider>();

            // Act
            var newNumberList = await provider.LoadObjectListCodeFnAsync<ObjectNumber>("GetNewNumber", "BaseSlip", false, 1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(newNumberList, Is.Not.Null);
                Assert.That(newNumberList, Has.Count.EqualTo(1));
                Assert.That(newNumberList[0].Body.Number, Is.GreaterThanOrEqualTo(0));
            });
        }

        [Test]
        public async Task LoadObjectListSPByPersistentRole()
        {
            // Arrange
            var provider = ServiceProvider.GetService<ICisServiceProvider>();

            // Act
            var rolesList = await provider.LoadObjectListFnAsync<PersistentRole>("\"CIS\".\"_getRolesFn\"", "AF8EBF72-6CA2-4A65-88D0-34B248FE0D6E");

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rolesList, Is.Not.Null);
            });
        }
    }
}