using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtBeheerder.REST.Models.Input;
using EindopdrachtBeheerder.REST.Models.Output;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EindopdrachtBeheerder.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TafelController : ControllerBase
    {
        private readonly TafelManager _tafelManager;
        private readonly ILogger _logger;

        public TafelController(TafelManager tafelManager, ILoggerFactory loggerFactory)
        {
            _tafelManager = tafelManager;
            _logger = loggerFactory.AddFile("Logs/Tafellogs.txt").CreateLogger("Tafel");
        }

        [HttpPost]
        public ActionResult<TafelOutput> MaakTafel(string naam, [FromBody] TafelInput tafelInput)
        {
            try
            {
                _logger.LogInformation($"MaakTafel opgeroepen!");

                Tafel tafel = new Tafel(tafelInput.Plaatsen);

                _tafelManager.MaakTafel(naam, tafel);

                _logger.LogInformation($"Tafel correct aangemaakt!");

                return Ok(tafel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tafel niet correct aangemaakt!");
                return BadRequest(ex.Message);
            }
        }
    }
}