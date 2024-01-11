using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtBeheerder.REST.Controllers;
using EindopdrachtBeheerder.REST.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Beheerder.Tests
{
    public class TafelControllerTests
    {
        private readonly Mock<TafelManager> mockTafelManager;
        private readonly TafelController tafelController;

        public TafelControllerTests()
        {
            mockTafelManager = new Mock<TafelManager>(new Mock<ITafelRepository>().Object);

            var mockLoggerFactory = new Mock<Microsoft.Extensions.Logging.ILoggerFactory>();
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<TafelController>>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            tafelController = new TafelController(
                mockTafelManager.Object,
                mockLoggerFactory.Object
            );
        }

        [Fact]
        public void MaakTafel_Returns_Ok_WhenSuccessful()
        {
            string restaurantNaam = "TestRestaurant";
            var tafelInput = new TafelInput { Plaatsen = 4 };

            mockTafelManager.Setup(m => m.MaakTafel(It.IsAny<string>(), It.IsAny<Tafel>()));

            var response = tafelController.MaakTafel(restaurantNaam, tafelInput);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void MaakTafel_Returns_BadRequest_OnException()
        {
            string restaurantNaam = "TestRestaurant";
            var tafelInput = new TafelInput { Plaatsen = 4 };

            mockTafelManager.Setup(m => m.MaakTafel(It.IsAny<string>(), It.IsAny<Tafel>()))
                .Throws(new Exception("Test exception"));

            var response = tafelController.MaakTafel(restaurantNaam, tafelInput);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void GeefTafelById_Returns_Ok_WhenSuccessful()
        {
            int tafelId = 1;

            mockTafelManager.Setup(m => m.GeefTafelById(It.IsAny<int>())).Returns(new Tafel(4));

            var response = tafelController.GeefTafelById(tafelId);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GeefTafelById_Returns_BadRequest_OnException()
        {
            int tafelId = 1;

            mockTafelManager.Setup(m => m.GeefTafelById(It.IsAny<int>()))
                .Throws(new Exception("Test exception"));

            var response = tafelController.GeefTafelById(tafelId);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}