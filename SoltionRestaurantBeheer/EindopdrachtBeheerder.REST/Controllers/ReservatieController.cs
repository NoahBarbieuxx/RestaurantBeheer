using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EindopdrachtBeheerder.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservatieController : ControllerBase
    {
        private readonly ReservatieManager _reservatieManager;
        private readonly ILogger _logger;

        public ReservatieController(ReservatieManager reservatieManager, ILoggerFactory loggerFactory)
        {
            _reservatieManager = reservatieManager;
            _logger = loggerFactory.AddFile("Logs/Reservatielogs.txt").CreateLogger("Reservatie");
        }

        [HttpGet("Restaurant/{naam}/Reservatie/{datum}")]
        public ActionResult<List<Reservatie>> GeefOverzichtReservaties(string naam, DateTime datum)
        {
            try
            {
                _logger.LogInformation($"GeefOverzichtReservaties opgeroepen: {naam}, {datum}");
                List<Reservatie> reservaties = _reservatieManager.GeefOverzichtReservaties(naam, datum);
                _logger.LogInformation($"Reservaties correct opgehaald: {naam}, {datum}");
                return Ok(reservaties);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Reservaties niet correct opgehaald: {naam}, {datum}");
                return NotFound(ex.Message);
            }
        }
    }
}