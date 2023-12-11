using Eindopdracht.BL;
using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eindopdracht.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservatieController : ControllerBase
    {
        private readonly ReservatieManager _manager;

        public ReservatieController(ReservatieManager manager)
        {
            _manager = manager;
        }

        [HttpPost]
        public ActionResult<Reservatie> MaakReservatie(Reservatie reservatie)
        {
            try
            {
                _manager.MaakReservatie(reservatie);
                //return CreatedAtAction(nameof(GeefReservatieVolgensId), new { reservatienummer = reservatie.Reservatienummer }, reservatie);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}   

// commit