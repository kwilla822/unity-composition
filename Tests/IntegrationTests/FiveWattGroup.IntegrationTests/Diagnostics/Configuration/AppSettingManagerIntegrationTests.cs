using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Diagnostics.Configuration;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.IntegrationTests.Diagnostics.Configuration
{
    [TestClass]
    public class AppSettingManagerIntegrationTests
    {
        [TestMethod]
        public void AppSettingManager_GetAppSettings_Filter_By()
        {
            //Arrange
            var expected = new List<AppSetting>
            {
                new AppSetting
                {
                    ConfigKey = "TestKey",
                    ConfigValue = "TestValue",
                    Category = "Category1",
                    SubCategory = "SubCategory1",
                    LastModifiedBy = "user1"
                }
            };

            //Act
            var actual =
                AppSettingManager.GetAppSettings(new QueryRequest<AppSetting> {Filter = x => x.ConfigKey == "TestKey"})
                    .ToList();

            //Assert
            Assert.AreEqual(expected.Count(), actual.Count());
        }

        [TestMethod]
        public void AppSettingManager_GetAppSetting_By_ConfigKey()
        {
            //Arrange
            var expected = new AppSetting
            {
                ConfigKey = "TestKey",
                ConfigValue = "TestValue",
                Category = "Category1",
                SubCategory = "SubCategory1",
                LastModifiedBy = "user1"
            };

            //Act
            var actual = AppSettingManager.GetAppSetting("TestKey");

            //Assert
            Assert.AreEqual(expected.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(expected.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.SubCategory, actual.SubCategory);
            Assert.AreEqual(expected.LastModifiedBy, actual.LastModifiedBy);
        }

        [TestMethod]
        public void AppSettingManager_Create()
        {
            //Arrange
            var command = new CommandRequest<AppSetting>
            {
                Entity =
                    new AppSetting
                    {
                        ConfigKey = "Key1",
                        ConfigValue = "Value1",
                        Category = "Category1",
                        SubCategory = "SubCategory1",
                        CreatedDate = DateTimeOffset.Now,
                        LastModifiedBy = "user1",
                        LastModifiedDate = DateTimeOffset.Now
                    },
                SaveChanges = false
            };

            //Act
            var actual = AppSettingManager.Create(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(command.Entity.LastModifiedDate, actual.LastModifiedDate);

            //Cleanup
            AppSettingManager.Delete(command);
        }

        [TestMethod]
        public void AppSettingManager_Update()
        {
            //Arrange
            var command = new CommandRequest<AppSetting>
            {
                Entity =
                    new AppSetting
                    {
                        ConfigKey = "TestKey",
                        ConfigValue = "TestValue",
                        Category = "Category1",
                        SubCategory = "SubCategory1",
                        CreatedDate = DateTimeOffset.Now,
                        LastModifiedBy = "user1",
                        LastModifiedDate = DateTimeOffset.Now
                    },
                SaveChanges = false
            };

            //Act
            var actual = AppSettingManager.Update(command);

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
        public void AppSettingManager_Delete()
        {
            //Arrange
            var command = new CommandRequest<AppSetting>
            {
                Entity =
                    new AppSetting
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
            var actual = AppSettingManager.Delete(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);

            //Cleanup
            AppSettingManager.Create(command);
        }
    }
}
