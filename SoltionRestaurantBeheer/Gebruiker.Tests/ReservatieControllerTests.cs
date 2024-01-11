using System;
using System.Collections.Generic;
using Eindopdracht.BL.Exceptions;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Eindopdracht.DL.Repositories;
using EindopdrachtGebruiker.REST.Controllers;
using EindopdrachtGebruiker.REST.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace GebruikerREST.Tests
{
    public class ReservatieControllerTests
    {
        private readonly Mock<ReservatieManager> mockReservatieManager;
        private readonly Mock<GebruikerManager> mockGebruikerManager;
        private readonly Mock<RestaurantManager> mockRestaurantManager;
        private readonly Mock<TafelManager> mockTafelManager;
        private readonly ReservatieController reservatieController;
        private readonly Mock<ILoggerFactory> mockLoggerFactory;

        public ReservatieControllerTests()
        {
            mockReservatieManager = new Mock<ReservatieManager>(new Mock<IReservatieRepository>().Object);
            mockGebruikerManager = new Mock<GebruikerManager>(new GebruikerRepository(" "));
            mockRestaurantManager = new Mock<RestaurantManager>(new RestaurantRepository(" "));
            mockTafelManager = new Mock<TafelManager>(new TafelRepository(" "));
            var mockLogger = new Mock<ILogger<ReservatieController>>();
            mockLoggerFactory = new Mock<ILoggerFactory>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            reservatieController = new ReservatieController(
                mockReservatieManager.Object,
                mockGebruikerManager.Object,
                mockRestaurantManager.Object,
                mockTafelManager.Object,
                mockLoggerFactory.Object
            );
        }

        //[Fact]
        //public void MaakReservatie_Returns_Ok_WhenSuccessful()
        //{
        //    int klantnummer = 1;
        //    string restaurantNaam = "TestRestaurant";

        //    DateTime specifiekeDatumEnTijd = DateTime.Today.Add(new TimeSpan(12, 00, 00)).AddDays(1);

        //    ReservatieInput reservatieInput = new ReservatieInput
        //    {
        //        AantalPlaatsen = 2,
        //        Datum = specifiekeDatumEnTijd
        //    };

        //    mockReservatieManager.Setup(m => m.MaakReservatie(It.IsAny<Reservatie>()));

        //    var response = reservatieController.MaakReservatie(klantnummer, restaurantNaam, reservatieInput);

        //    var result = Assert.IsType<OkObjectResult>(response.Result);
        //    var reservatie = Assert.IsType<Reservatie>(result.Value);

        //    Assert.Equal(2, reservatie.AantalPlaatsen);
        //}

        [Fact]
        public void MaakReservatie_Returns_BadRequest_WhenExceptionCaught()
        {
            int klantnummer = 1;
            string restaurantNaam = "TestRestaurant";
            ReservatieInput reservatieInput = new ReservatieInput { AantalPlaatsen = 2, Datum = DateTime.Now };

            mockGebruikerManager.Setup(m => m.GeefGebruikerById(It.IsAny<int>())).Returns(new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1));
            mockRestaurantManager.Setup(m => m.GeefRestaurantByNaam(It.IsAny<string>(), It.IsAny<bool>())).Returns(new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")));
            mockTafelManager.Setup(m => m.KiesTafel(It.IsAny<string>(), It.IsAny<int>())).Returns(new Tafel(4));
            mockReservatieManager.Setup(m => m.MaakReservatie(It.IsAny<Reservatie>())).Throws(new Exception("Simulated exception"));

            var response = reservatieController.MaakReservatie(klantnummer, restaurantNaam, reservatieInput);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void GeefReservatieById_Returns_Ok_WhenSuccessful()
        {
            int reservatienummer = 1;

            mockReservatieManager.Setup(m => m.GeefReservatieById(It.IsAny<int>())).Returns(new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4)));

            var response = reservatieController.GeefReservatieById(reservatienummer);

            Assert.IsType<OkObjectResult>(response.Result);
        }


        [Fact]
        public void GeefReservatieById_Returns_BadRequest_WhenExceptionCaught()
        {
            int reservatienummer = 1;

            mockReservatieManager.Setup(m => m.GeefReservatieById(It.IsAny<int>())).Throws(new Exception("Simulated exception"));

            var response = reservatieController.GeefReservatieById(reservatienummer);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void PasReservatieAan_Returns_BadRequest_WhenInvalidAantalPlaatsen()
        {
            int reservatienummer = 1;
            ReservatieInput reservatieInput = new ReservatieInput { AantalPlaatsen = 5, Datum = DateTime.Now };

            mockReservatieManager.Setup(m => m.GeefReservatieById(reservatienummer)).Returns(new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4)));

            var response = reservatieController.PasReservatieAan(reservatienummer, reservatieInput);

            Assert.IsType<BadRequestResult>(response);
        }

        [Fact]
        public void PasReservatieAan_Returns_Ok_WhenSuccessful()
        {
            int reservatienummer = 1;
            DateTime afgerondeDatum = RoundToNearestHalfHour(DateTime.Now.AddDays(1));
            ReservatieInput reservatieInput = new ReservatieInput { AantalPlaatsen = 2, Datum = afgerondeDatum };

            mockReservatieManager.Setup(m => m.GeefReservatieById(reservatienummer)).Returns(new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4)));
            mockReservatieManager.Setup(m => m.PasReservatieAan(It.IsAny<Reservatie>()));

            var response = reservatieController.PasReservatieAan(reservatienummer, reservatieInput);

            Assert.IsType<OkObjectResult>(response);
        }
        private DateTime RoundToNearestHalfHour(DateTime input)
        {
            int minutes = (input.Minute + 15) / 30 * 30;
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, minutes, 0);
        }


        [Fact]
        public void PasReservatieAan_Returns_BadRequest_WhenExceptionCaught()
        {
            int reservatienummer = 1;
            ReservatieInput reservatieInput = new ReservatieInput { AantalPlaatsen = 2, Datum = DateTime.Now };

            mockReservatieManager.Setup(m => m.GeefReservatieById(reservatienummer)).Returns(new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4)));
            mockReservatieManager.Setup(m => m.PasReservatieAan(It.IsAny<Reservatie>())).Throws(new Exception("Simulated exception"));

            var response = reservatieController.PasReservatieAan(reservatienummer, reservatieInput);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void AnnuleerReservatie_Returns_NoContent_WhenSuccessful()
        {
            int reservatienummer = 1;

            mockReservatieManager.Setup(m => m.GeefReservatieById(reservatienummer)).Returns(new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4)));
            mockReservatieManager.Setup(m => m.AnnuleerReservatie(reservatienummer));

            var response = reservatieController.AnnuleerReservatie(reservatienummer);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void AnnuleerReservatie_Returns_BadRequest_WhenExceptionCaught()
        {
            int reservatienummer = 1;

            mockReservatieManager.Setup(m => m.GeefReservatieById(reservatienummer)).Returns(new Reservatie(1, new Restaurant("Naam", new Locatie("9000", "Naam", "Naam", "5"), "Italiaans", new Contactgegevens("0199494", "test@test")), new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1), 4, DateTime.Now.AddDays(1), new Tafel(4)));
            mockReservatieManager.Setup(m => m.AnnuleerReservatie(reservatienummer)).Throws(new Exception("Simulated exception"));

            var response = reservatieController.AnnuleerReservatie(reservatienummer);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void ZoekReservaties_Returns_BadRequest_WhenExceptionCaught()
        {
            int klantnummer = 1;
            DateTime? datum = DateTime.Now;

            mockReservatieManager.Setup(m => m.ZoekReservaties(klantnummer, datum)).Throws(new Exception("Simulated exception"));

            var response = reservatieController.ZoekReservaties(klantnummer, datum);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }
    }
}