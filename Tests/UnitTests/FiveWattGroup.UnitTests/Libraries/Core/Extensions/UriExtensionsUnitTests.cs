using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FiveWattGroup.Core.Extensions;

namespace FiveWattGroup.UnitTests.Libraries.Core.Extensions
{
    [TestClass]
    public class UriExtensionsUnitTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Append_Null_ShouldThrow_ArgumentNullException()
        {
            //Arrange
            var baseUri = new Uri("http://localhost:9090");
            
            //Act
            baseUri.Append(null);
        }

        [TestMethod]
        public void Append_EmptyString_ShouldReturn_BaseUri()
        {
            //Arrange
            var baseUri = new Uri("http://localhost:9090");
            
            //Act
            var actualUri = baseUri.Append(string.Empty);

            //Assert
            Assert.AreEqual(baseUri.AbsoluteUri, actualUri.AbsoluteUri);
        }

        [TestMethod]
        public void Append_SomeUri_ShouldReturn_CombinedUri()
        {
            //Arrange
            var baseUri = new Uri("http://localhost:9090");
            var expectedUri = new Uri("http://localhost:9090/Configuration");
            
            //Act
            var actualUri = baseUri.Append("Configuration");

            //Assert
            Assert.AreEqual(expectedUri.AbsoluteUri, actualUri.AbsoluteUri);
        }
    }
}
