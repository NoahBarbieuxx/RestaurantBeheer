using Eindopdracht.BL;
using Eindopdracht.BL.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Eindopdracht.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GebruikerController : ControllerBase
    {
        private readonly GebruikerManager _manager;

        public GebruikerController(GebruikerManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public ActionResult<Gebruiker> RegistreerGebruiker(Gebruiker gebruiker)
        {
            try
            {
                _manager.RegistreerGebruiker(gebruiker);
                //return CreatedAtAction(nameof(GeefGebruikerVolgensId), new { klantnummer = gebruiker.Klantnummer }, gebruiker);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{klantnummer}")]
        public IActionResult PasGebruikerAan(int klantnummer, [FromBody] Gebruiker gebruiker)
        {
            try
            {
                if (!_manager.HeeftGebruiker(klantnummer))
                {
                    return NotFound();
                }
                if (gebruiker.Klantnummer != klantnummer)
                {
                    return BadRequest();
                }

                _manager.PasGebruikerAan(gebruiker);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{klantnummer}")]
        public IActionResult SchrijfGebruikerUit(int klantnummer)
        {
            try
            {
                if (!_manager.HeeftGebruiker(klantnummer))
                {
                    return NotFound();
                }

                Gebruiker gebruiker = _manager.GeefGebruikerById(klantnummer);
                _manager.SchrijfGebruikerUit(gebruiker);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    } 
}