using Moq;
using Taxually.TechnicalTest.Core;
using Taxually.TechnicalTest.Interfaces;
using Xunit;
using Assert = Xunit.Assert;

namespace Taxually.TechnicalTest.Test
{
    [TestClass]
    public class UkVatRegistrationStrategyTests
    {
        [Fact]
        [TestMethod]
        public void CanHandle_ReturnsTrue_ForGB()
        {
            // Arrange
            var mockHttpClient = new Mock<ITaxuallyHttpClient>();
            var strategy = new UkVatRegistrationStrategy(mockHttpClient.Object);

            // Act
            var canHandle = strategy.CanHandle("GB");

            // Assert
            Assert.True(canHandle);
        }

        [Fact]
        [TestMethod]
        public void CanHandle_ReturnsFalse_ForOtherCountry()
        {
            // Arrange
            var mockHttpClient = new Mock<ITaxuallyHttpClient>();
            var strategy = new UkVatRegistrationStrategy(mockHttpClient.Object);

            // Act
            var canHandleFR = strategy.CanHandle("FR");
            var canHandleNull = strategy.CanHandle(null);

            // Assert
            Assert.False(canHandleFR);
            Assert.False(canHandleNull);
        }

        [Fact]
        [TestMethod]
        public async Task RegisterAsync_CallsHttpClient_WithCorrectParameters()
        {
            // Arrange
            var request = new VatRegistrationRequest
            {
                CompanyName = "UK Test Ltd",
                CompanyId = "UK123",
                Country = "GB"
            };

            var mockHttpClient = new Mock<ITaxuallyHttpClient>();
            var strategy = new UkVatRegistrationStrategy(mockHttpClient.Object);

            // Act
            await strategy.RegisterAsync(request);

            // Assert
            mockHttpClient.Verify(
                client => client.PostAsync("https://api.uktax.gov.uk", request),
                Times.Once);
        }
    }
}
