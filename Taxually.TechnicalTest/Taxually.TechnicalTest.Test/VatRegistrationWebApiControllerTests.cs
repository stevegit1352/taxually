using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Taxually.TechnicalTest.Controllers;
using Xunit;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Core;
using Taxually.TechnicalTest.Exceptions;
using Assert = Xunit.Assert;

namespace Taxually.TechnicalTest.Test
{
    [TestClass]
    public class VatRegistrationWebApiControllerTests
    {
        private readonly Mock<IVatRegistrationService> _mockService;
        private readonly VatRegistrationWebApiController _controller;

        public VatRegistrationWebApiControllerTests()
        {
            _mockService = new Mock<IVatRegistrationService>();
            _controller = new VatRegistrationWebApiController(_mockService.Object);
        }

        [Fact]
        [TestMethod]
        public async Task Post_ReturnsOk_WhenRegistrationSucceeds()
        {
            // Arrange
            var request = new VatRegistrationRequest
            {
                CompanyName = "Test Ltd",
                CompanyId = "123456",
                Country = "GB"
            };

            _mockService
                .Setup(service => service.RegisterAsync(It.IsAny<VatRegistrationRequest>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Post(request);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        [TestMethod]
        public async Task Post_ReturnsBadRequest_WhenCountryIsUnsupported()
        {
            // Arrange
            var request = new VatRegistrationRequest
            {
                CompanyName = "Unknown Co",
                CompanyId = "999999",
                Country = "ZZ" // Unsupported
            };

            _mockService
                .Setup(service => service.RegisterAsync(It.IsAny<VatRegistrationRequest>()))
                .ThrowsAsync(new UnsupportedCountryException());

            // Act
            var result = await _controller.Post(request);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Country not supported", badRequestResult.Value);
        }
    }
}
