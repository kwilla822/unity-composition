using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Diagnostics.Logging;
using FiveWattGroup.Entities.CodeFirst.Logging;
using FiveWattGroup.Entities.Enums.Diagnostics.Logging;
using FiveWattGroup.Entities.Pocos.Crud.Write;

namespace FiveWattGroup.IntegrationTests.Diagnostics.Logging
{
    [TestClass]
    public class LogManagerIntegrationTests
    {
        [TestMethod]
        public void LogManager_WriteLogEntry()
        {
            //Arrange
            var expected = new Log
            {
                CreatedOnUtc = DateTimeOffset.UtcNow.UtcDateTime,
                FullMessage = "This is a test exception message.",
                IpAddress = Environment.MachineName,
                LogLevelId = (int)LogLevelTypeCode.Error,
                ShortMessage = "This is a test log message.",
            };

            var command = new CommandRequest<Log>
            {
                Entity = expected,
                SaveChanges = true
            };

            //Act
            var actual = LogManager.WriteLogEntry(command);

            //Assert
            Assert.IsTrue(actual.Id > 0);
            Assert.AreEqual(expected.CreatedOnUtc, actual.CreatedOnUtc);
            Assert.AreEqual(expected.FullMessage, actual.FullMessage);
            Assert.AreEqual(expected.IpAddress, actual.IpAddress);
            Assert.AreEqual(expected.LogLevelId, actual.LogLevelId);
            Assert.AreEqual(expected.ShortMessage, actual.ShortMessage);
        }
    }
}
