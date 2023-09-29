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
            var attributeList = await provider.LoadAttributesAsync<MaterialName>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadAttributesForMaterialNameNonGeneric()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var attributeList = await provider.LoadAttributesAsync(_user, typeof(MaterialName).Name);

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
            var attributeList = await provider.LoadAttributesAsync<MaterialName>(_user, new() { Identity = Guid.NewGuid(), Name = "test" });

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
            var attributeList = await provider.LoadAttributesAsync<Employee>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadAttributesForEmployeeNonGeneric()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var attributeList = await provider.LoadAttributesAsync(_user, typeof(Employee).Name);

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
            var attributeList = await provider.LoadAttributesAsync<Employee>(_user, new() { Identity = Guid.NewGuid(), Name = "Test Test", IsActive = true});

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attributeList, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForMaterialName()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync<MaterialName>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForMaterialNameNonGeneric()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync(_user, typeof(MaterialName).Name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForMaterialNameWithContext()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync<MaterialName>(_user, new() { Identity = Guid.NewGuid(), Name = "test" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForEmployee()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync<Employee>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForEmployeeNonGeneric()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync(_user, typeof(Employee).Name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForEmployeeWithContext()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync<Employee>(_user, new() { Identity = Guid.NewGuid(), Name = "Test Test", IsActive = true });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForColor()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync<Color>(_user);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForColorNonGeneric()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync(_user, typeof(Color).Name);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }

        [Test]
        public async Task LoadMetaClassForColorWithContext()
        {
            // Arrange
            var provider = ServiceProvider.GetService<IImpersonatingMetaSystemProvider>();

            // Act
            var metaClass = await provider.LoadMetaClassAsync<Color>(_user, new() { Identity = Guid.NewGuid(), Name = "Цвет тестовый" });

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(metaClass, Is.Not.Null);
            });
        }
    }
}