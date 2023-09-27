using CIS.Service.Client.Models;
using CIS.Service.Client.Services;
using CIS.Service.Client.Tests.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CIS.Service.Client.Tests.Tests
{
    public class ImpersonatingMetaSystemProviderTests : BaseProviderTest
    {
        private ImpersonatingUser _user;

        [SetUp]
        public void Setup()
        {
            _user = ConfigurationHelper.GetImpersonatingUser();
        }

        [Test]
        public async Task LoadAttributesForMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var attributeList = await provider.LoadAttributes<MaterialName>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadAttributesForMaterialNameWithContext()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var attributeList = await provider.LoadAttributes<MaterialName>(_user, new() { Name = "test" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadAttributesForEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var attributeList = await provider.LoadAttributes<Employee>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadAttributesForEmployeeWithContext()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var attributeList = await provider.LoadAttributes<Employee>(_user, new() { Name = "Test Test", IsActive = true});

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }
    }
}