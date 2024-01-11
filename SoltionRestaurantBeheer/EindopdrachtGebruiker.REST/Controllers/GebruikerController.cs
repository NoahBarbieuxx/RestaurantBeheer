using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtGebruiker.REST.Models.Input;
using EindopdrachtGebruiker.REST.Models.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Tracing;

namespace EindopdrachtGebruiker.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly GebruikerManager _gebruikerManager;
        private readonly ILogger _logger;

        public GebruikerController(GebruikerManager gebruikerManager, ILoggerFactory loggerFactory)
        {
            _gebruikerManager = gebruikerManager;
            _logger = loggerFactory.AddFile("Logs/Gebruikerlogs.txt").CreateLogger("Gebruiker");
        }

        [HttpGet("{klantnummer}")]
        public ActionResult<Gebruiker> GeefGebruikerById(int klantnummer)
        {
            try
            {
                _logger.LogInformation($"GeefGebruikerById opgeroepen: {klantnummer}");

                if (_gebruikerManager.GeefGebruikerById(klantnummer) == null)
                {
                    _logger.LogError($"Gebruiker niet gevonden: {klantnummer}");
                    return BadRequest();
                }
                else
                {
                    Gebruiker gebruiker = _gebruikerManager.GeefGebruikerById(klantnummer);
                    _logger.LogInformation($"Gebruiker correct opgehaald: {klantnummer}");
                    return Ok(gebruiker);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Gebruiker niet correct opgehaald: {klantnummer}");
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<GebruikerOutput> RegistreerGebruiker([FromBody] GebruikerInput gebruikerInput)
        {
            try
            {
                Gebruiker gebruiker = new Gebruiker(gebruikerInput.Naam, gebruikerInput.Email, gebruikerInput.Telefoonnummer, gebruikerInput.Locatie, gebruikerInput.Actief);
                
                if (_gebruikerManager.HeeftGebruiker(gebruiker))
                {
                    _logger.LogError($"Gebruiker bestaat al");
                    return BadRequest();
                }
                else
                {
                    _gebruikerManager.RegistreerGebruiker(gebruiker);
                    _logger.LogInformation("Gebruiker correct geregistreerd");
                    return CreatedAtAction(nameof(GeefGebruikerById), new { klantnummer = gebruiker.Klantnummer }, gebruikerInput);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Gebruiker niet correct geregistreerd");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{klantnummer}")]
        public IActionResult PasGebruikerAan(int klantnummer, [FromBody] GebruikerInput gebruikerInput)
        {
            try
            {
                Gebruiker gebruiker = new Gebruiker(klantnummer, gebruikerInput.Naam, gebruikerInput.Email, gebruikerInput.Telefoonnummer, gebruikerInput.Actief, gebruikerInput.Locatie);

                if (_gebruikerManager.GeefGebruikerById(klantnummer) == null)
                {
                    _logger.LogError($"Gebruiker niet gevonden: {klantnummer}");
                    return BadRequest();
                }
                else
                {
                    _gebruikerManager.PasGebruikerAan(gebruiker);
                    _logger.LogInformation($"Gebruiker correct aangepast: {klantnummer}");
                    return Ok(gebruiker);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Gebruiker niet correct aangepast: {klantnummer}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{klantnummer}")]
        public IActionResult SchrijfGebruikerUit(int klantnummer)
        {
            try
            {
                if (_gebruikerManager.GeefGebruikerById(klantnummer) == null)
                {
                    _logger.LogError($"Gebruiker niet gevonden: {klantnummer}");
                    return BadRequest();
                }
                else
                {
                    _gebruikerManager.SchrijfGebruikerUit(klantnummer);
                    _logger.LogInformation($"Gebruiker correct uitgeschreven: {klantnummer}");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Gebruiker niet correct uitgeschreven: {klantnummer}");
                return NotFound(ex.Message);
            }
        }
    }
}