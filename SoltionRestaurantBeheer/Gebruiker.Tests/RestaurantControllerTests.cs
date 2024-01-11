using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtGebruiker.REST.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GebruikerREST.Tests
{
    public class RestaurantControllerTests
    {
        private readonly Mock<RestaurantManager> mockManager;
        private readonly RestaurantController restaurantController;
        private readonly Mock<ILoggerFactory> mockLoggerFactory;

        public RestaurantControllerTests()
        {
            var mockRepo = new Mock<IRestaurantRepository>();

            mockLoggerFactory = new Mock<ILoggerFactory>();
            var mockLogger = new Mock<ILogger<RestaurantController>>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            mockManager = new Mock<RestaurantManager>(mockRepo.Object);

            restaurantController = new RestaurantController(mockManager.Object, mockLoggerFactory.Object);
        }

        [Fact]
        public void ZoekRestaurants_Returns_Ok_WhenSuccessful()
        {
            string postcode = "9000";
            string keuken = "Italiaans";
            mockManager.Setup(m => m.ZoekRestaurants(postcode, keuken)).Returns(new List<Restaurant> { new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")) });

            var response = restaurantController.ZoekRestaurants(postcode, keuken);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void ZoekRestaurants_Returns_BadRequest_WhenExceptionCaught()
        {
            string postcode = "9000";
            string keuken = "Italiaans";
            mockManager.Setup(m => m.ZoekRestaurants(postcode, keuken)).Throws(new Exception("Simulated exception"));

            var response = restaurantController.ZoekRestaurants(postcode, keuken);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void ZoekVrijeRestaurants_Returns_Ok_WhenSuccessful()
        {
            DateTime datum = DateTime.Now;
            int aantalPlaatsen = 2;
            string postcode = "9000";
            string keuken = "Italiaans";

            mockManager.Setup(m => m.ZoekVrijeRestaurants(datum, aantalPlaatsen, postcode, keuken)).Returns(new List<Restaurant> { new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")) });

            var response = restaurantController.ZoekVrijeRestaurants(datum, aantalPlaatsen, postcode, keuken);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void ZoekVrijeRestaurants_Returns_BadRequest_WhenExceptionCaught()
        {
            DateTime datum = DateTime.Now;
            int aantalPlaatsen = 2;
            string postcode = "1234AB";
            string keuken = "Italian";

            mockManager.Setup(m => m.ZoekVrijeRestaurants(datum, aantalPlaatsen, postcode, keuken)).Throws(new Exception("Simulated exception"));

            var response = restaurantController.ZoekVrijeRestaurants(datum, aantalPlaatsen, postcode, keuken);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}