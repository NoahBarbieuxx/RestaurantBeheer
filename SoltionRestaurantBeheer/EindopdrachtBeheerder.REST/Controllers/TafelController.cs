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
        private readonly RestaurantManager _restaurantManager;
        private readonly ILogger _logger;

        public TafelController(TafelManager tafelManager, RestaurantManager restaurantManager, ILoggerFactory loggerFactory)
        {
            _tafelManager = tafelManager;
            _restaurantManager = restaurantManager;
            _logger = loggerFactory.AddFile("Logs/Tafellogs.txt").CreateLogger("Tafel");
        }

        [HttpPost]
        public ActionResult<TafelOutput> MaakTafel(string naam, [FromBody] TafelInput tafelInput)
        {
            try
            {
                Restaurant restaurant = _restaurantManager.GeefRestaurantByNaam(naam);

                if (restaurant == null)
                {
                    _logger.LogError($"Restaurant niet gevonden: {naam}");
                    return BadRequest();
                }

                Tafel tafel = new Tafel(tafelInput.Tafelnummer, tafelInput.Plaatsen, restaurant);

                if (_tafelManager.HeeftTafel(tafel))
                {
                    _logger.LogError($"Tafel bestaat al");
                    return BadRequest();
                }
                else
                {
                    _tafelManager.MaakTafel(tafel);
                    _logger.LogInformation($"Tafel correct aangemaakt");
                    return Ok(tafel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Tafel niet correct aangemaakt");
                return BadRequest(ex.Message);
            }
        }
    }
}