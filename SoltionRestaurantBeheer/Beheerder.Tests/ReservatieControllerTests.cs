using System;
using System.Collections.Generic;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtBeheerder.REST.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Beheerder.Tests
{
    public class ReservatieControllerTests
    {
        private readonly Mock<ReservatieManager> mockReservatieManager;
        private readonly Mock<ILogger<ReservatieController>> mockLogger;
        private readonly ReservatieController reservatieController;

        public ReservatieControllerTests()
        {
            mockReservatieManager = new Mock<ReservatieManager>(new Mock<IReservatieRepository>().Object);
            mockLogger = new Mock<ILogger<ReservatieController>>();

            reservatieController = new ReservatieController(
                mockReservatieManager.Object,
                new LoggerFactory(new[] { new Microsoft.Extensions.Logging.Debug.DebugLoggerProvider() })
            );
        }

        [Fact]
        public void GeefOverzichtReservaties_Returns_Ok_WhenSuccessful()
        {
            string restaurantNaam = "TestRestaurant";
            DateTime beginDatum = DateTime.Now;
            DateTime? eindDatum = DateTime.Now.AddDays(1);
            var expectedReservaties = new List<Reservatie> { new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4))};

            mockReservatieManager.Setup(m => m.GeefOverzichtReservaties(restaurantNaam, beginDatum, eindDatum)).Returns(expectedReservaties);

            var response = reservatieController.GeefOverzichtReservaties(restaurantNaam, beginDatum, eindDatum);

            Assert.IsType<OkObjectResult>(response.Result);
            var okResult = (OkObjectResult)response.Result;
            Assert.Equal(expectedReservaties, okResult.Value);
        }

        [Fact]
        public void GeefOverzichtReservaties_Returns_BadRequest_OnException()
        {
            string restaurantNaam = "TestRestaurant";
            DateTime beginDatum = DateTime.Now;
            DateTime? eindDatum = DateTime.Now.AddDays(1);

            mockReservatieManager.Setup(m => m.GeefOverzichtReservaties(restaurantNaam, beginDatum, eindDatum)).Throws(new Exception("Test exception"));

            var response = reservatieController.GeefOverzichtReservaties(restaurantNaam, beginDatum, eindDatum);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}