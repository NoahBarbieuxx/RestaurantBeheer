using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using EindopdrachtBeheerder.REST.Models.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EindopdrachtBeheerder.REST.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantManager _restaurantManager;
        private readonly ILogger _logger;

        public RestaurantController(RestaurantManager restaurantManager, ILoggerFactory loggerFactory)
        {
            _restaurantManager = restaurantManager;
            _logger = loggerFactory.AddFile("Logs/Restaurantlogs.txt").CreateLogger("Restaurant");
        }

        [HttpPost]
        public ActionResult<Restaurant> RegistreerRestaurant([FromBody] Restaurant restaurant)
        {
            try
            {
                if (_restaurantManager.HeeftRestaurant(restaurant))
                {
                    _logger.LogError($"Restaurant bestaat al");
                    return BadRequest();
                }
                else
                {
                    _restaurantManager.RegistreerRestaurant(restaurant);
                    _logger.LogInformation($"Restaurant correct aangemaakt");
                    return Ok(restaurant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Restaurant niet correct aangemaakt");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{naam}")]
        public IActionResult PasRestaurantAan(string naam, [FromBody] RestaurantInput restaurantInput)
        {
            try
            {
                Restaurant restaurant = new Restaurant(naam, restaurantInput.Locatie, restaurantInput.Keuken, restaurantInput.Contactgegevens);

                if (_restaurantManager.GeefRestaurantByNaam(naam) == null)
                {
                    _logger.LogError($"Restaurant bestaat niet: {naam}");
                    return BadRequest();
                }
                else
                {
                    _restaurantManager.PasRestaurantAan(restaurant);
                    _logger.LogInformation($"Restaurant correct aangepast: {naam}");
                    return Ok(restaurant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Restaurant niet correct aangepast: {naam}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{naam}")]
        public IActionResult VerwijderRestaurant(string naam)
        {
            try
            {
                if (_restaurantManager.GeefRestaurantByNaam(naam) == null)
                {
                    _logger.LogError($"Restaurant bestaat niet: {naam}");
                    return BadRequest();
                }
                else
                {
                    _restaurantManager.VerwijderRestaurant(naam);
                    _logger.LogInformation($"Restaurant correct verwijderd: {naam}");
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Restaurant niet correct verwijderd: {naam}");
                return NotFound(ex.Message);
            }
        }
    }
}