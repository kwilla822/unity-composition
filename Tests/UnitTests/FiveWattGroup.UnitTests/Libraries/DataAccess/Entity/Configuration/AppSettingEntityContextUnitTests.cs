using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FiveWattGroup.Contracts.DataAccess.Data;
using FiveWattGroup.DataAccess.Entity;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.UnitTests.Libraries.DataAccess.Entity.Configuration
{
    [TestClass]
    public class AppSettingContextUnitTests
    {
        [TestMethod]
        public void AppSetting_Read_Many_By_Filter()
        {
            //Arrange
            var data = GetData();

            var mockSet = CreateMockSet(data.AsQueryable());
            var mockContext = CreateMockDataContext(mockSet);

            var expected = data.Where(x => x.LastModifiedBy == "user1");
            var query = new QueryRequest<AppSetting> { Filter = x => x.LastModifiedBy == "user1" };

            //Act
            var entityContext = new EntityContext(mockContext.Object);
            var actual = entityContext.Read(query);

            //Assert
            mockContext.Verify(x => x.SetOf<AppSetting>(), Times.Once);
            Assert.AreEqual(expected.Count(), actual.Count());
        }

        [TestMethod]
        public void AppSetting_Create()
        {
            //Arrange
            var data = GetData();
            var command = CreateCommand();

            var expected = command.Entity;

            var mockSet = CreateMockSet(data.AsQueryable());
            var mockContext = CreateMockDataContext(mockSet);
            mockContext.Setup(x => x.SetOf<AppSetting>().Add(It.IsAny<AppSetting>())).Returns(command.Entity);

            //Act
            var entityContext = new EntityContext(mockContext.Object);
            var actual = entityContext.Create(command);

            //Verify
            mockContext.Verify(x => x.SetOf<AppSetting>().Add(command.Entity), Times.Once);

            if (command.SaveChanges)
                mockContext.Verify(x => x.SaveChanges(), Times.Once);

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
            var data = GetData();
            var command = CreateCommand();

            var expected = command.Entity;

            var mockSet = CreateMockSet(data.AsQueryable());
            var mockContext = CreateMockDataContext(mockSet);
            mockContext.Setup(x => x.Find(It.IsAny<AppSetting>())).Returns(command.Entity);
            mockContext.Setup(x => x.CopyValues(It.IsAny<AppSetting>(), It.IsAny<AppSetting>())).Returns(command.Entity);

            //Act
            var entityContext = new EntityContext(mockContext.Object);
            var actual = entityContext.Update(command);

            //Verify
            mockContext.Verify(x => x.Find(command.Entity), Times.Once);
            mockContext.Verify(x => x.CopyValues(command.Entity, command.Entity), Times.Once);
            mockContext.Verify(x => x.SetState(command.Entity, EntityState.Modified), Times.Once);

            if (command.SaveChanges)
                mockContext.Verify(x => x.SaveChanges(), Times.Once);

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
            var data = GetData();
            var command = CreateCommand();

            var expected = command.Entity;

            var mockSet = CreateMockSet(data.AsQueryable());
            var mockContext = CreateMockDataContext(mockSet);
            mockContext.Setup(x => x.Find(It.IsAny<AppSetting>())).Returns(command.Entity);

            //Act
            var entityContext = new EntityContext(mockContext.Object);
            var actual = entityContext.Delete(command);

            //Verify
            mockContext.Verify(x => x.Find(It.IsAny<AppSetting>()), Times.Once);

            if (command.SaveChanges)
                mockContext.Verify(x => x.SaveChanges(), Times.Once);

            Assert.AreEqual(expected.ConfigKey, actual.ConfigKey);
            Assert.AreEqual(expected.ConfigValue, actual.ConfigValue);
            Assert.AreEqual(expected.CreatedDate, actual.CreatedDate);
            Assert.AreEqual(expected.Category, actual.Category);
            Assert.AreEqual(expected.SubCategory, actual.SubCategory);
            Assert.AreEqual(expected.LastModifiedBy, actual.LastModifiedBy);
            Assert.AreEqual(expected.LastModifiedDate, actual.LastModifiedDate);
        }

        private Mock<IDbContext> CreateMockDataContext(Mock<IDbSet<AppSetting>> mockSet)
        {
            var mockContext = new Mock<IDbContext>();
            mockContext.Setup(x => x.SetOf<AppSetting>()).Returns(mockSet.Object);

            return mockContext;
        }

        private Mock<IDbSet<AppSetting>> CreateMockSet(IQueryable<AppSetting> queryable)
        {
            var mockSet = new Mock<IDbSet<AppSetting>>();
            mockSet.Setup(x => x.Provider).Returns(queryable.Provider);
            mockSet.Setup(x => x.Expression).Returns(queryable.Expression);
            mockSet.Setup(x => x.ElementType).Returns(queryable.ElementType);
            mockSet.Setup(x => x.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mockSet;
        }

        private IQueryable<AppSetting> GetData()
        {
            return new List<AppSetting>
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
        }

        private CommandRequest<AppSetting> CreateCommand()
        {
            return new CommandRequest<AppSetting>
            {
                Entity = new AppSetting
                {
                    ConfigKey = "Key4",
                    ConfigValue = "Value4",
                    CreatedDate = DateTimeOffset.Now,
                    LastModifiedBy = "user3",
                    LastModifiedDate = DateTimeOffset.Now
                }
            };
        }
    }
}
