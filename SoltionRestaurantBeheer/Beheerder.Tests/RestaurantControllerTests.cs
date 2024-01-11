using Castle.Core.Logging;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtBeheerder.REST.Controllers;
using EindopdrachtBeheerder.REST.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using Xunit;

namespace Beheerder.Tests
{
    public class RestaurantControllerTests
    {
        private readonly Mock<RestaurantManager> mockRestaurantManager;
        private readonly RestaurantController restaurantController;

        public RestaurantControllerTests()
        {
            mockRestaurantManager = new Mock<RestaurantManager>(new Mock<IRestaurantRepository>().Object);

            var mockLoggerFactory = new Mock<Microsoft.Extensions.Logging.ILoggerFactory>();
            var mockLogger = new Mock<Microsoft.Extensions.Logging.ILogger<RestaurantController>>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            restaurantController = new RestaurantController(
                mockRestaurantManager.Object,
                mockLoggerFactory.Object
            );
        }

        [Fact]
        public void RegistreerRestaurant_Returns_Ok_WhenSuccessful()
        {
            RestaurantInput restaurantInput = new RestaurantInput("naam", new Locatie("1000", "gent", "gent", "1"), "Italiaans", new Contactgegevens("0444", "afef@"));

            mockRestaurantManager.Setup(m => m.RegistreerRestaurant(It.IsAny<Restaurant>()));

            var response = restaurantController.RegistreerRestaurant(restaurantInput);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void RegistreerRestaurant_Returns_BadRequest_OnException()
        {
            RestaurantInput restaurantInput = new RestaurantInput("naam", new Locatie("1000", "gent", "gent", "1"), "Italiaans", new Contactgegevens("0444", "afef@"));

            mockRestaurantManager.Setup(m => m.RegistreerRestaurant(It.IsAny<Restaurant>()))
                .Throws(new Exception("Test exception"));

            var response = restaurantController.RegistreerRestaurant(restaurantInput);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void PasRestaurantAan_Returns_Ok_WhenSuccessful()
        {
            RestaurantInput restaurantInput = new RestaurantInput("naam", new Locatie("1000", "gent", "gent", "1"), "Italiaans", new Contactgegevens("0444", "afef@"));
            string restaurantNaam = "naam";

            mockRestaurantManager.Setup(m => m.PasRestaurantAan(It.IsAny<Restaurant>()));

            var response = restaurantController.PasRestaurantAan(restaurantNaam, restaurantInput);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void PasRestaurantAan_Returns_BadRequest_OnException()
        {
            string restaurantNaam = "naam";
            RestaurantInput restaurantInput = new RestaurantInput("naam", new Locatie("1000", "gent", "gent", "1"), "Italiaans", new Contactgegevens("0444", "afef@"));

            mockRestaurantManager.Setup(m => m.PasRestaurantAan(It.IsAny<Restaurant>()))
                .Throws(new Exception("Test exception"));

            var response = restaurantController.PasRestaurantAan(restaurantNaam, restaurantInput);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void VerwijderRestaurant_Returns_NoContent_WhenSuccessful()
        {
            string restaurantNaam = "TestRestaurant";

            mockRestaurantManager.Setup(m => m.VerwijderRestaurant(It.IsAny<string>()));

            var response = restaurantController.VerwijderRestaurant(restaurantNaam);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void VerwijderRestaurant_Returns_NotFound_OnException()
        {
            string restaurantNaam = "TestRestaurant";

            mockRestaurantManager.Setup(m => m.VerwijderRestaurant(It.IsAny<string>()))
                .Throws(new Exception("Test exception"));

            var response = restaurantController.VerwijderRestaurant(restaurantNaam);

            Assert.IsType<NotFoundObjectResult>(response);
        }

    }
}