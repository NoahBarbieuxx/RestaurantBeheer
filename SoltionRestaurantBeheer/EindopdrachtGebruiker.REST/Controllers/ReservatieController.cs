using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtGebruiker.REST.Models.Input;
using EindopdrachtGebruiker.REST.Models.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EindopdrachtGebruiker.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservatieController : ControllerBase
    {
        private readonly ReservatieManager _reservatieManager;
        private readonly GebruikerManager _gebruikerManager;
        private readonly RestaurantManager _restaurantManager;
        private readonly TafelManager _tafelManager;
        private readonly ILogger _logger;

        public ReservatieController(ReservatieManager reservatieManager, GebruikerManager gebruikerManager, RestaurantManager restaurantManager, TafelManager tafelManager, ILoggerFactory loggerFactory)
        {
            _reservatieManager = reservatieManager;
            _gebruikerManager = gebruikerManager;
            _restaurantManager = restaurantManager;
            _tafelManager = tafelManager;
            _logger = loggerFactory.AddFile("Logs/Reservatielogs.txt").CreateLogger("Reservatie");
        }

        [HttpPost("Gebruiker/{klantnummer}/Restaurant/{naam}")]
        public ActionResult<ReservatieOutput> MaakReservatie(int klantnummer, string naam, [FromBody] ReservatieInput reservatieInput)
        {
            try
            {
                _logger.LogInformation($"MaakReservatie opgeroepen!");

                Gebruiker gebruiker = _gebruikerManager.GeefGebruikerById(klantnummer);
                Restaurant restaurant = _restaurantManager.GeefRestaurantByNaam(naam, false);
                Tafel tafel = _tafelManager.KiesTafel(naam, reservatieInput.AantalPlaatsen);

                DateTime afgerondeDatum = RoundToNearestHalfHour(reservatieInput.Datum);
                Reservatie reservatie = new Reservatie(restaurant, gebruiker, reservatieInput.AantalPlaatsen, afgerondeDatum, tafel);

                _reservatieManager.MaakReservatie(reservatie);

                _logger.LogInformation($"Reservatie correct aangemaakt!");

                return Ok(reservatie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservatie niet correct aangemaakt!");
                return BadRequest(ex.Message);
            }
        }

        private DateTime RoundToNearestHalfHour(DateTime input)
        {
            int minutes = (input.Minute + 15) / 30 * 30;
            return new DateTime(input.Year, input.Month, input.Day, input.Hour, minutes, 0);
        }

        [HttpGet("geefreservatiebyid{reservatienummer}")]
        public ActionResult<Gebruiker> GeefReservatieById(int reservatienummer)
        {
            try
            {
                _logger.LogInformation($"GeefReservatieById opgeroepen: {reservatienummer}!");

                Reservatie reservatie = _reservatieManager.GeefReservatieById(reservatienummer);

                _logger.LogInformation($"Reservatie correct opgehaald: {reservatienummer}!");

                return Ok(reservatie);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservatie niet correct opgehaald: {reservatienummer}!");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{reservatienummer}")]
        public IActionResult PasReservatieAan(int reservatienummer, [FromBody] ReservatieInput reservatieInput)
        {
            try
            {
                _logger.LogInformation($"PasReservatieAan opgeroepen: {reservatienummer}!");

                Reservatie reservatie = _reservatieManager.GeefReservatieById(reservatienummer);

                if (reservatieInput.AantalPlaatsen > reservatie.Tafel.Plaatsen)
                {
                    _logger.LogError($"Aantal plaatsen incorrect: {reservatienummer}!");
                    return BadRequest();
                }
                else
                {
                    Reservatie updatedReservatie = new Reservatie(reservatienummer, reservatie.Restaurantinfo, reservatie.Contactpersoon, reservatieInput.AantalPlaatsen, reservatieInput.Datum, reservatie.Tafel);

                    _reservatieManager.PasReservatieAan(updatedReservatie);

                    _logger.LogInformation($"Reservatie correct aangepast: {reservatienummer}!");

                    return Ok(updatedReservatie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Resercatie niet correct aangepast: {reservatienummer}!");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{reservatienummer}")]
        public IActionResult AnnuleerReservatie(int reservatienummer)
        {
            try
            {
                _logger.LogInformation($"AnnuleerReservatie opgeroepen: {reservatienummer}!");

                Reservatie reservatie = _reservatieManager.GeefReservatieById(reservatienummer);

                if (reservatie.Datum < DateTime.Now)
                {
                    _logger.LogError($"Reservatie ligt in het verleden: {reservatienummer}!");
                    return BadRequest();
                }
                else
                {
                    _reservatieManager.AnnuleerReservatie(reservatienummer);

                    _logger.LogInformation($"Reservatie correct geannuleerd: {reservatienummer}!");

                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservatie niet correct geannuleerd: {reservatienummer}!");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Gebruiker/{klantnummer}/Reservatie")]
        public ActionResult<List<Reservatie>> ZoekReservaties(int klantnummer, DateTime? datum)
        {
            try
            {
                _logger.LogInformation($"ZoekReservaties opgeroepen: {datum}!");

                List<Reservatie> reservaties = _reservatieManager.ZoekReservaties(klantnummer, datum);

                _logger.LogInformation($"Reservaties correct opgehaald!");

                return Ok(reservaties);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservaties niet correct opgehaald: {datum}!");
                return BadRequest(ex.Message);
            }
        }
    }
}