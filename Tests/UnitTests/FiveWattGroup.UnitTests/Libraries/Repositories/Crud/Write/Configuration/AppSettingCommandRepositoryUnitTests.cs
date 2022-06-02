using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Write;
using FiveWattGroup.Repositories.Crud.Write;

namespace FiveWattGroup.UnitTests.Libraries.Repositories.Crud.Write.Configuration
{
    [TestClass]
    public class AppSettingCommandRepositoryUnitTests
    {
        [TestMethod]
        public void AppSetting_Create()
        {
            //Arrange
            var expected = new AppSetting
            {
                ConfigKey = "Key1",
                ConfigValue = "Value1",
                Category = "Category1",
                SubCategory = "SubCategory",
                CreatedDate = DateTimeOffset.Now,
                LastModifiedBy = "user1",
                LastModifiedDate = DateTimeOffset.Now
            };

            var mockContext = new Mock<IEntityContext>();
            mockContext.Setup(x => x.Create(It.IsAny<CommandRequest<AppSetting>>())).Returns(expected);

            //Act
            var commandRepository = new CommandRepository<AppSetting>(mockContext.Object);
            var actual = commandRepository.Create(new CommandRequest<AppSetting>{Entity = expected});

            //Assert
            mockContext.Verify(x => x.Create(It.IsAny<CommandRequest<AppSetting>>()), Times.Once);

            Assert.AreEqual(expected.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(expected.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.SubCategory, actual.SubCategory);
            Assert.AreEqual(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(expected.LastModifiedDate, actual.LastModifiedDate);
        }

        [TestMethod]
        public void AppSetting_Update()
        {
            //Arrange
            var expected = new AppSetting
            {
                ConfigKey = "Key1",
                ConfigValue = "Value1",
                Category = "Category1",
                SubCategory = "SubCategory",
                CreatedDate = DateTimeOffset.Now,
                LastModifiedBy = "user1",
                LastModifiedDate = DateTimeOffset.Now
            };

            var mockContext = new Mock<IEntityContext>();
            mockContext.Setup(x => x.Update(It.IsAny<CommandRequest<AppSetting>>())).Returns(expected);

            //Act
            var commandRepository = new CommandRepository<AppSetting>(mockContext.Object);
            var actual = commandRepository.Update(new CommandRequest<AppSetting> {Entity = expected});

            //Assert
            mockContext.Verify(x => x.Update(It.IsAny<CommandRequest<AppSetting>>()), Times.Once);

            Assert.AreEqual(expected.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(expected.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.SubCategory, actual.SubCategory);
            Assert.AreEqual(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(expected.LastModifiedDate, actual.LastModifiedDate);
        }

        [TestMethod]
        public void AppSetting_Delete()
        {
            //Arrange
            var expected = new AppSetting
            {
                ConfigKey = "Key1",
                ConfigValue = "Value1",
                Category = "Category1",
                SubCategory = "SubCategory",
                CreatedDate = DateTimeOffset.Now,
                LastModifiedBy = "user1",
                LastModifiedDate = DateTimeOffset.Now
            };

            var mockContext = new Mock<IEntityContext>();
            mockContext.Setup(x => x.Delete(It.IsAny<CommandRequest<AppSetting>>())).Returns(expected);

            //Act
            var commandRepository = new CommandRepository<AppSetting>(mockContext.Object);
            var actual = commandRepository.Delete(new CommandRequest<AppSetting> {Entity = expected});

            //Assert
            mockContext.Verify(x => x.Delete(It.IsAny<CommandRequest<AppSetting>>()), Times.Once);

            Assert.AreEqual(expected.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(expected.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.SubCategory, actual.SubCategory);
            Assert.AreEqual(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(expected.LastModifiedDate, actual.LastModifiedDate);
        }
    }
}
