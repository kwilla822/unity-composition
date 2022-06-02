using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.IntegrationTests.Libraries.Repositories.Crud.Write.Configuration
{
    [TestClass]
    public class AppSettingRepositoryIntegrationTests
    {
        private ICommandOperations<AppSetting> _commandRepository;
            
        [TestInitialize]
        public void Initialize()
        {
            var manager = new UnityManager();
            _commandRepository = manager.GetService<ICommandOperations<AppSetting>>("AppSettingCommandRepository");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_commandRepository != null)
                _commandRepository.Dispose();
            _commandRepository = null;
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
            var actual = _commandRepository.Create(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(command.Entity.LastModifiedDate, actual.LastModifiedDate);

            //Cleanup
            _commandRepository.Delete(command);
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
            var actual = _commandRepository.Update(command);

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
            var actual = _commandRepository.Delete(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);

            //Cleanup
            _commandRepository.Create(command);
        }
    }
}
