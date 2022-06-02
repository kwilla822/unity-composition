using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.IntegrationTests.Libraries.BusinessObjects.Crud.Write.Configuration
{
    [TestClass]
    public class AppSettingManagerIntegrationTests
    {
        private ICommandOperations<AppSetting> _commandManager;
            
        [TestInitialize]
        public void Initialize()
        {
            var manager = new UnityManager();
            _commandManager = manager.GetService<ICommandOperations<AppSetting>>("AppSettingCommandManager");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_commandManager != null)
                _commandManager.Dispose();
            _commandManager = null;
        }

        [TestMethod]
        public void AppSetting_Create()
        {
            //Arrange
            var command = new CommandRequest<AppSetting>
            {
                Entity = new AppSetting
                {
                    ConfigKey = "Key1",
                    ConfigValue = "Value1",
                    Category = "Category1",
                    SubCategory = "SubCategory1",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user1",
                    LastModifiedDate = DateTimeOffset.Now
                },
                SaveChanges = true
            };

            //Act
            var actual = _commandManager.Create(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(command.Entity.LastModifiedDate, actual.LastModifiedDate);

            //Cleanup
            _commandManager.Delete(command);
        }

        [TestMethod]
        public void AppSetting_Update()
        {
            //Arrange
            var command = new CommandRequest<AppSetting>
            {
                Entity = new AppSetting
                {
                    ConfigKey = "TestKey",
                    ConfigValue = "TestValue",
                    Category = "Category1",
                    SubCategory = "SubCategory1",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user1",
                    LastModifiedDate = DateTimeOffset.Now
                },
                SaveChanges = true
            };

            //Act
            var actual = _commandManager.Update(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(command.Entity.LastModifiedDate, actual.LastModifiedDate);
        }

        [TestMethod]
        public void AppSetting_Delete()
        {
            //Arrange
            var command = new CommandRequest<AppSetting>
            {
                Entity = new AppSetting
                {
                    ConfigKey = "TestKey",
                    ConfigValue = "TestValue",
                    Category = "Category1",
                    SubCategory = "SubCategory1",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user1",
                    LastModifiedDate = DateTimeOffset.Now
                },
                SaveChanges = true
            };

            //Act
            var actual = _commandManager.Delete(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);

            //Cleanup
            _commandManager.Create(command);
        }
    }
}
