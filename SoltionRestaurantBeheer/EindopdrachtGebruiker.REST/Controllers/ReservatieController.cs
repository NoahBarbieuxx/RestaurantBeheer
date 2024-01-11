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

        [HttpGet("geefreservatiebyid{reservatienummer}")]
        public ActionResult<Gebruiker> GeefReservatieById(int reservatienummer)
        {
            try
            {
                _logger.LogInformation($"GeefReservatieById opgeroepen: {reservatienummer}");
                Reservatie reservatie = _reservatieManager.GeefReservatieById(reservatienummer);

                if (reservatie == null)
                {
                    _logger.LogError($"Reservatie niet gevonden: {reservatienummer}");
                    return BadRequest();
                }
                else
                {
                    _logger.LogInformation($"Reservatie correct opgehaald: {reservatienummer}");
                    return Ok(reservatie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservatie niet correct opgehaald: {reservatienummer}");
                return NotFound(ex.Message);
            }
        }

        [HttpGet("Gebruiker/{klantnummer}/Reservatie/{datum}")]
        public ActionResult<List<Reservatie>> ZoekReservaties(int klantnummer, DateTime datum)
        {
            try
            {
                _logger.LogInformation($"ZoekReservaties opgeroepen: {datum}");

                if (_gebruikerManager.GeefGebruikerById(klantnummer) == null)
                {
                    _logger.LogError($"Gebruiker niet gevonden: {klantnummer}");
                    return BadRequest();
                }

                List<Reservatie> reservaties = _reservatieManager.ZoekReservaties(datum);
                foreach(Reservatie reseravatie in reservaties)
                {
                    if (reseravatie.Contactpersoon.Klantnummer != klantnummer)
                    {
                        reservaties.Remove(reseravatie);
                    }
                }

                if (reservaties.Count == 0)
                {
                    _logger.LogError($"Reservaties niet gevonden: {datum}");
                    return BadRequest();
                }
                else
                {
                    _logger.LogInformation($"Reservaties correct opgehaald: {datum}");
                    return Ok(reservaties);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservaties niet correct opgehaald: {datum}");
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Gebruiker/{klantnummer}/Restaurant/{naam}")]
        public ActionResult<ReservatieOutput> MaakReservatie(int klantnummer, string naam, [FromBody] ReservatieInput reservatieInput)
        {
            try
            {
                Gebruiker gebruiker = _gebruikerManager.GeefGebruikerById(klantnummer);
                Restaurant restaurant = _restaurantManager.GeefRestaurantByNaam(naam);
                Tafel tafel = _tafelManager.KiesTafel(reservatieInput.AantalPlaatsen);

                if (gebruiker == null)
                {
                    _logger.LogError($"Gebruiker niet gevonden: {gebruiker}");
                    return BadRequest();
                }
                if (restaurant == null)
                {
                    _logger.LogError($"Restaurant niet gevonden: {naam}");
                    return BadRequest();
                }
                if (tafel == null)
                {
                    _logger.LogError("Tafel niet gevonden");
                    return BadRequest();
                }

                Reservatie reservatie = new Reservatie(restaurant, gebruiker, reservatieInput.AantalPlaatsen, reservatieInput.Datum, tafel);

                if (_reservatieManager.HeeftReservatie(reservatie))
                {
                    _logger.LogError($"Reservatie bestaat al");
                    return BadRequest();
                }
                else
                {
                    _reservatieManager.MaakReservatie(reservatie);
                    _logger.LogInformation($"Reservatie correct aangemaakt");
                    return CreatedAtAction(nameof(GeefReservatieById), new { reservatienummer = reservatie.Reservatienummer }, reservatieInput);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservatie niet correct aangemaakt");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{reservatienummer}")]
        public IActionResult PasReservatieAan(int reservatienummer, [FromBody] ReservatieInput reservatieInput)
        {
            try
            {
                Reservatie reservatie = _reservatieManager.GeefReservatieById(reservatienummer);

                if (reservatie == null)
                {
                    _logger.LogError($"Reservatie niet gevonden: {reservatienummer}");
                    return BadRequest();
                }
                if (reservatieInput.AantalPlaatsen >= reservatie.AantalPlaatsen)
                {
                    _logger.LogError($"Aantal plaatsen incorrect: {reservatienummer}");
                    return BadRequest();
                }
                else
                {
                    Reservatie updatedReservatie = new Reservatie(reservatienummer, reservatie.Restaurantinfo, reservatie.Contactpersoon, reservatieInput.AantalPlaatsen, reservatieInput.Datum, reservatie.Tafel);
                    _logger.LogInformation($"Reservatie correct aangepast: {reservatienummer}");
                    _reservatieManager.PasReservatieAan(updatedReservatie);
                    return Ok(updatedReservatie);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Resercatie niet correct aangepast: {reservatienummer}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{reservatienummer}")]
        public IActionResult AnnuleerReservatie(int reservatienummer)
        {
            try
            {
                Reservatie reservatie = _reservatieManager.GeefReservatieById(reservatienummer);

                if (reservatie == null)
                {
                    _logger.LogError($"Reservatie niet gevonden: {reservatienummer}");
                    return BadRequest();
                }
                if (reservatie.Datum < DateTime.Now)
                {
                    _logger.LogError($"Reservatie ligt in het verleden: {reservatienummer}");
                    return BadRequest();
                }
                else
                {
                    _reservatieManager.AnnuleerReservatie(reservatienummer);
                    _logger.LogInformation($"Reservatie correct geannuleerd: {reservatienummer}");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservatie niet correct geannuleerd: {reservatienummer}");
                return NotFound(ex.Message);
            }
        }
    }
}