using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Composition.Unity.Common;
using FiveWattGroup.Contracts.Crud.Read;
using FiveWattGroup.Entities.CodeFirst.Configuration;
using FiveWattGroup.Entities.Pocos.Crud.Read;

namespace FiveWattGroup.IntegrationTests.Libraries.Repositories.Crud.Read.Configuration
{
    [TestClass]
    public class AppSettingRepositoryIntegrationTests
    {
        private IQueryOperations<AppSetting, IQueryable<AppSetting>> _queryRepository;
            
        [TestInitialize]
        public void Initialize()
        {
            var manager = new UnityManager();
            _queryRepository = manager.GetService<IQueryOperations<AppSetting, IQueryable<AppSetting>>>("AppSettingQueryRepository");
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_queryRepository != null)
                _queryRepository.Dispose();
            _queryRepository = null;
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
            var actual = _queryRepository.Read(new QueryRequest<AppSetting> { Filter = x => x.ConfigKey == "TestKey" });

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
    }
}
