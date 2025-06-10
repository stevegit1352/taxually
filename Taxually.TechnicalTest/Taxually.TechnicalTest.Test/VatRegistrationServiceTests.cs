using Moq;
using Taxually.TechnicalTest.Core;
using Taxually.TechnicalTest.Exceptions;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Services;
using Xunit;
using Assert = Xunit.Assert;

namespace Taxually.TechnicalTest.Test
{
    [TestClass]
    public class VatRegistrationServiceTests
    {
        [Fact]
        [TestMethod]
        public async Task RegisterAsync_UsesCorrectStrategy_ForSupportedCountry()
        {
            // Arrange
            var mockStrategy = new Mock<IVatRegistrationStrategy>();
            var request = new VatRegistrationRequest { Country = "GB" };

            mockStrategy.Setup(s => s.CanHandle("GB")).Returns(true);
            mockStrategy.Setup(s => s.RegisterAsync(request)).Returns(Task.CompletedTask);

            var service = new VatRegistrationService(new[] { mockStrategy.Object });

            await service.RegisterAsync(request);

            mockStrategy.Verify(s => s.RegisterAsync(request), Times.Once);
        }

        [Fact]
        [TestMethod]
        public async Task RegisterAsync_ThrowsException_ForUnsupportedCountry()
        {
            // Arrange
            var mockStrategy = new Mock<IVatRegistrationStrategy>();
            var request = new VatRegistrationRequest { Country = "IT" }; // Unsupported

            mockStrategy.Setup(s => s.CanHandle(It.IsAny<string>())).Returns(false);

            var service = new VatRegistrationService(new[] { mockStrategy.Object });

            await Assert.ThrowsAsync<UnsupportedCountryException>(() => service.RegisterAsync(request));
        }
    }
}
