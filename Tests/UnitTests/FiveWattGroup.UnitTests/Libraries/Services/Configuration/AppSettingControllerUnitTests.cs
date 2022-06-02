using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FiveWattGroup.BusinessObjects.Crud.Read;
using FiveWattGroup.BusinessObjects.Crud.Write;
using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Contracts.Crud.Write;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;
using FiveWattGroup.Services.Configuration.Controllers;

namespace FiveWattGroup.UnitTests.Libraries.Services
{
    [TestClass]
    public class AppSettingControllerUnitTests
    {
        [TestMethod]
        public void AppSetting_Read_Single_Filter_By()
        {
            //Arrange
            var data = new List<AppSetting>
            {
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
                new AppSetting
                {
                    ConfigKey = "Key2",
                    ConfigValue = "Value2",
                    Category = "Category2",
                    SubCategory = "SubCategory2",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user1",
                    LastModifiedDate = DateTimeOffset.Now
                },
                new AppSetting
                {
                    ConfigKey = "Key3",
                    ConfigValue = "Value3",
                    Category = "Category3",
                    SubCategory = "SubCategory3",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user2",
                    LastModifiedDate = DateTimeOffset.Now
                }
            }.AsQueryable();

            var expected = data.Where(x => x.ConfigKey == "Key1");

            var mockQueryManager = new Mock<IQueryOperations<AppSetting, IEnumerable<AppSetting>>>();
            mockQueryManager.Setup(x => x.Read(It.IsAny<QueryRequest<AppSetting>>())).Returns(expected);

            var mockCommandManager = new Mock<ICommandOperations<AppSetting>>();

            //Act
            var controller = new AppSettingController(mockQueryManager.Object, mockCommandManager.Object);
            var response = controller.ReadSingle(new QueryRequest<AppSetting> {Filter = x => x.ConfigKey == "Key1"});
            var actual = response.Content.ReadAsAsync<AppSetting>().Result;

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(expected.Single(), actual);
        }

        [TestMethod]
        public void AppSetting_Read_Many_Filter_By()
        {
            //Arrange
            var data = new List<AppSetting>
            {
                new AppSetting
                {
                    ConfigKey = "Key1",
                    ConfigValue = "Value1",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user1",
                    LastModifiedDate = DateTimeOffset.Now
                },
                new AppSetting
                {
                    ConfigKey = "Key2",
                    ConfigValue = "Value2",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user1",
                    LastModifiedDate = DateTimeOffset.Now
                },
                new AppSetting
                {
                    ConfigKey = "Key3",
                    ConfigValue = "Value3",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user2",
                    LastModifiedDate = DateTimeOffset.Now
                }
            }.AsQueryable();

            var expected = data.Where(x => x.LastModifiedBy == "user1");

            var mockQueryManager = new Mock<IQueryOperations<AppSetting, IEnumerable<AppSetting>>>();
            mockQueryManager.Setup(x => x.Read(It.IsAny<QueryRequest<AppSetting>>())).Returns(expected);

            var mockCommandManager = new Mock<ICommandOperations<AppSetting>>();

            //Act
            var controller = new AppSettingController(mockQueryManager.Object, mockCommandManager.Object);
            var response =
                controller.ReadAsEnumerable(new QueryRequest<AppSetting> {Filter = x => x.LastModifiedBy == "user1"});
            var actual = response.Content.ReadAsAsync<List<AppSetting>>().Result;

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Assert.AreEqual(expected.Count(), actual.Count());
        }

        [TestMethod]
        public void AppSetting_Create()
        {
            //Arrange
            var expected = new AppSetting
            {
                ConfigKey = "Key1",
                ConfigValue = "Value1",
                Category = "Category1",
                SubCategory = "SubCategory1",
                CreatedDate = DateTimeOffset.Now,
                LastModifiedBy = "user1",
                LastModifiedDate = DateTimeOffset.Now
            };

            var mockQueryManager = new Mock<IQueryOperations<AppSetting, IEnumerable<AppSetting>>>();
            var mockCommandManager = new Mock<ICommandOperations<AppSetting>>();
            mockCommandManager.Setup(x => x.Create(It.IsAny<CommandRequest<AppSetting>>())).Returns(expected);

            //Act
            var controller = new AppSettingController(mockQueryManager.Object, mockCommandManager.Object);
            var response = controller.Create(new CommandRequest<AppSetting> {Entity = expected});
            var actual = response.Content.ReadAsAsync<AppSetting>().Result;

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            
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
                SubCategory = "SubCategory1",
                CreatedDate = DateTimeOffset.Now,
                LastModifiedBy = "user1",
                LastModifiedDate = DateTimeOffset.Now
            };

            var mockQueryManager = new Mock<IQueryOperations<AppSetting, IEnumerable<AppSetting>>>();
            var mockCommandManager = new Mock<ICommandOperations<AppSetting>>();
            mockCommandManager.Setup(x => x.Update(It.IsAny<CommandRequest<AppSetting>>())).Returns(expected);

            //Act
            var controller = new AppSettingController(mockQueryManager.Object, mockCommandManager.Object);
            var response = controller.Update(new CommandRequest<AppSetting> { Entity = expected });
            var actual = response.Content.ReadAsAsync<AppSetting>().Result;

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

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
                SubCategory = "SubCategory1",
                CreatedDate = DateTimeOffset.Now,
                LastModifiedBy = "user1",
                LastModifiedDate = DateTimeOffset.Now
            };

            var mockQueryManager = new Mock<IQueryOperations<AppSetting, IEnumerable<AppSetting>>>();
            var mockCommandManager = new Mock<ICommandOperations<AppSetting>>();
            mockCommandManager.Setup(x => x.Delete(It.IsAny<CommandRequest<AppSetting>>())).Returns(expected);

            //Act
            var controller = new AppSettingController(mockQueryManager.Object, mockCommandManager.Object);
            var response = controller.Delete(new CommandRequest<AppSetting> {Entity = expected});
            var actual = response.Content.ReadAsAsync<AppSetting>().Result;

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

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
