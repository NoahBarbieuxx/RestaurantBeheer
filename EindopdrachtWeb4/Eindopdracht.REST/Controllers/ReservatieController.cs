using Eindopdracht.BL;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Eindopdracht.REST.Mappers;
using Eindopdracht.REST.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eindopdracht.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservatieController : ControllerBase
    {
        private readonly ReservatieManager _manager;
        private readonly RestaurantManager _rManager;
        private readonly GebruikerManager _gManager;
        public ReservatieController(ReservatieManager manager, RestaurantManager rManager, GebruikerManager gManager)
        {
            _manager = manager;
            _rManager = rManager;
            _gManager = gManager;
        }

        [HttpPost("Gebruiker/{klantnummer}/Restaurant/{naam}/Reservatie")]
        public ActionResult<Reservatie> MaakReservatie(int klantnummer, string naam, [FromBody] ReservatieInput inputDto)
        {
            try
            {
                Reservatie reservatie = _manager.MaakReservatie(MapToDomain.MapToReservatieDomain(inputDto));
                return CreatedAtAction(nameof(GeefReservatieById), new { reservatieNummer = reservatie.Reservatienummer }, MapFromDomain.MapFromReservatieDomain(reservatie));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}