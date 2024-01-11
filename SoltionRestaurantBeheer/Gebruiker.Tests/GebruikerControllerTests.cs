using System;
using Castle.Core.Logging;
using Eindopdracht.BL.Interfaces;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtGebruiker.REST.Controllers;
using EindopdrachtGebruiker.REST.Models.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Serilog.Core;
using Xunit;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace GebruikerREST.Tests
{
    public class GebruikerControllerTests
    {
        private readonly Mock<GebruikerManager> mockManager;
        private readonly GebruikerController gebruikerController;
        private readonly Mock<ILoggerFactory> mockLoggerFactory;

        public GebruikerControllerTests()
        {
            var mockRepo = new Mock<IGebruikerRepository>();

            mockLoggerFactory = new Mock<ILoggerFactory>();
            var mockLogger = new Mock<ILogger<GebruikerController>>();
            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(mockLogger.Object);

            mockManager = new Mock<GebruikerManager>(mockRepo.Object);

            gebruikerController = new GebruikerController(mockManager.Object, mockLoggerFactory.Object);
        }

        [Fact]
        public void Registreer_Return_Ok()
        {
            mockManager.Setup(m => m.HeeftGebruiker(It.IsAny<Eindopdracht.BL.Models.Gebruiker>())).Returns(false);

            GebruikerInput gebruiker = new GebruikerInput("Test", "Test@test", "015555", new Locatie("9000", "Test", "TEst", "2"));
            var response = gebruikerController.RegistreerGebruiker(gebruiker);

            Assert.IsType<CreatedAtActionResult>(response.Result);
        }

        [Fact]
        public void Registreer_Returns_BadRequest_WhenUserAlreadyExists()
        {
            GebruikerInput existingUserInput = new GebruikerInput("ExistingUser", "existing@test", "012345", new Locatie("1000", "ExistingCity", "ExistingStreet", "1"));
            mockManager.Setup(m => m.HeeftGebruiker(It.IsAny<Eindopdracht.BL.Models.Gebruiker>())).Returns(true);

            var response = gebruikerController.RegistreerGebruiker(existingUserInput);

            Assert.IsType<BadRequestResult>(response.Result);
        }

        [Fact]
        public void Registreer_Returns_BadRequest_WhenExceptionCaught()
        {
            GebruikerInput userInput = new GebruikerInput("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"));
            mockManager.Setup(m => m.HeeftGebruiker(It.IsAny<Eindopdracht.BL.Models.Gebruiker>())).Returns(false);
            mockManager.Setup(m => m.RegistreerGebruiker(It.IsAny<Eindopdracht.BL.Models.Gebruiker>())).Throws(new Exception("Simulated exception"));

            var response = gebruikerController.RegistreerGebruiker(userInput);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void GeefGebruikerById_Returns_Ok_WhenSuccessful()
        {
            int klantnummer = 1;
            mockManager.Setup(m => m.GeefGebruikerById(klantnummer)).Returns(new Eindopdracht.BL.Models.Gebruiker("Test", "test@test", "012345", new Locatie("1000", "City", "Street", "1"), 1));

            var response = gebruikerController.GeefGebruikerById(klantnummer);

            Assert.IsType<OkObjectResult>(response.Result);
        }

        [Fact]
        public void GeefGebruikerById_Returns_BadRequest_WhenExceptionCaught()
        {
            int klantnummer = 1;
            mockManager.Setup(m => m.GeefGebruikerById(klantnummer)).Throws(new Exception("Simulated exception"));

            var response = gebruikerController.GeefGebruikerById(klantnummer);

            Assert.IsType<BadRequestObjectResult>(response.Result);
        }

        [Fact]
        public void PasGebruikerAan_Returns_Ok_WhenSuccessful()
        {
            int klantnummer = 1;
            GebruikerInput updatedUserInput = new GebruikerInput("UpdatedUser", "updated@test", "012345", new Locatie("1000", "UpdatedCity", "UpdatedStreet", "1"));
            mockManager.Setup(m => m.PasGebruikerAan(It.IsAny<Eindopdracht.BL.Models.Gebruiker>()));

            var response = gebruikerController.PasGebruikerAan(klantnummer, updatedUserInput);

            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public void PasGebruikerAan_Returns_BadRequest_WhenExceptionCaught()
        {
            int klantnummer = 1;
            GebruikerInput updatedUserInput = new GebruikerInput("UpdatedUser", "updated@test", "012345", new Locatie("1000", "UpdatedCity", "UpdatedStreet", "1"));
            mockManager.Setup(m => m.PasGebruikerAan(It.IsAny<Eindopdracht.BL.Models.Gebruiker>())).Throws(new Exception("Simulated exception"));

            var response = gebruikerController.PasGebruikerAan(klantnummer, updatedUserInput);

            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public void SchrijfGebruikerUit_Returns_NoContent_WhenSuccessful()
        {
            int klantnummer = 1;
            mockManager.Setup(m => m.SchrijfGebruikerUit(klantnummer));

            var response = gebruikerController.SchrijfGebruikerUit(klantnummer);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public void SchrijfGebruikerUit_Returns_BadRequest_WhenExceptionCaught()
        {
            int klantnummer = 1;
            mockManager.Setup(m => m.SchrijfGebruikerUit(klantnummer)).Throws(new Exception("Simulated exception"));

            var response = gebruikerController.SchrijfGebruikerUit(klantnummer);

            Assert.IsType<BadRequestObjectResult>(response);
        }
    }
}