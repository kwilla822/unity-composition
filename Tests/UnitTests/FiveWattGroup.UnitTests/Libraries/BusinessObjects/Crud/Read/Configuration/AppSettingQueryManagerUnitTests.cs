using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FiveWattGroup.BusinessObjects.Crud.Read;
using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Repositories.Crud.Read;

namespace FiveWattGroup.UnitTests.Libraries.BusinessObjects.Crud.Read.Configuration
{
    [TestClass]
    public class AppSettingQueryManagerUnitTests
    {  
        [TestMethod]
        public void AppSetting_Read_Many_By_Filter()
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
            var query = new QueryRequest<AppSetting> {Filter = x => x.LastModifiedBy == "user1"};

            var mockRepository = new Mock<IQueryOperations<AppSetting, IQueryable<AppSetting>>>();
            mockRepository.Setup(x => x.Read(query)).Returns(expected);

            //Act
            var queryManager = new QueryManager<AppSetting>(mockRepository.Object);
            var actual = queryManager.Read(query);

            //Assert
            Assert.AreEqual(expected.Count(), actual.Count());         
        }
    }
}
