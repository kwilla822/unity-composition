using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.DataAccess.Entity;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.IntegrationTests.Libraries.DataAccess.Entity.Configuration
{
    [TestClass]
    public class AppSettingEntityContextIntegrationTests
    {
        private IEntityContext _context;

        [TestInitialize]
        public void Initialize()
        {
            _context = new UnityManager().GetService<IEntityContext>("Configuration");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_context != null)
                _context.Dispose();
            _context = null;
        }

        [TestMethod]
        public void AppSetting_Read_By_Filter()
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
                    LastModifiedBy = "user1",
                }
            }.AsQueryable();

            //Act
            var actual = _context.Read(new QueryRequest<AppSetting> { Filter = x => x.ConfigKey == "TestKey" });

            //Assert
            expected.ToList().ForEach(expectedItem =>
            {
                var actualItem = actual.Single(a => a.ConfigKey == expectedItem.ConfigKey);
                Assert.AreEqual(expectedItem.ConfigKey, actualItem.ConfigKey);
                Assert.AreEqual(expectedItem.ConfigValue, actualItem.ConfigValue);
                Assert.AreEqual(expectedItem.Category, actualItem.Category);
                Assert.AreEqual(expectedItem.SubCategory, actualItem.SubCategory);
                Assert.AreEqual(expectedItem.LastModifiedBy, actualItem.LastModifiedBy);
            });
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
            var actual = _context.Create(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(command.Entity.LastModifiedDate, actual.LastModifiedDate);

            //Cleanup
            _context.Delete(command);
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
            var actual = _context.Update(command);

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
            var actual = _context.Delete(command);

            //Assert
            Assert.AreEqual(command.Entity.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(command.Entity.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(command.Entity.Category, actual.Category);
            Assert.AreEqual(command.Entity.SubCategory, actual.SubCategory);
            Assert.AreEqual(command.Entity.LastModifiedBy, actual.LastModifiedBy);

            //Cleanup
            _context.Create(command);
        }
    }
}
