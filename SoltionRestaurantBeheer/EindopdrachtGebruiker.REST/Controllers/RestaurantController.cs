using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.Xml;

namespace EindopdrachtGebruiker.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantManager _restaurantManager;
        private readonly ILogger _logger;

        public RestaurantController(RestaurantManager restaurantManager, ILoggerFactory loggerFactory)
        {
            _restaurantManager = restaurantManager;
            _logger = loggerFactory.AddFile("Logs/Restaurantlogs.txt").CreateLogger("Restaurant");
        }

        [HttpGet("{postcode}/{keuken}")]
        public ActionResult<List<Restaurant>> ZoekRestaurants(string postcode, string keuken)
        {
            try
            {
                _logger.LogInformation($"ZoekRestaurant opgeroepen: {postcode}, {keuken}");
                List<Restaurant> restaurants = _restaurantManager.ZoekRestaurants(postcode, keuken);

                if (restaurants.Count == 0)
                {
                    _logger.LogError($"Restaurants niet gevonden: {postcode}, {keuken}");
                    return BadRequest();
                }
                else
                {
                    _logger.LogInformation($"Restaurant correct opgehaald: {postcode}, {keuken}");
                    return Ok(restaurants);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Restaurants niet correct opgehaald: {postcode}, {keuken}");
                return NotFound(ex.Message);
            }
        }
    }
}