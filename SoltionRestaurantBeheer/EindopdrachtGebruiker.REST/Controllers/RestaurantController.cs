using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
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

        [HttpGet]
        public ActionResult<List<Restaurant>> ZoekRestaurants(string? postcode, string? keuken)
        {
            try
            {
                _logger.LogInformation($"ZoekRestaurant opgeroepen: {postcode}, {keuken}");

                List<Restaurant> restaurants = _restaurantManager.ZoekRestaurants(postcode, keuken);

                _logger.LogInformation($"Restaurant correct opgehaald: {postcode}, {keuken}");

                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Restaurants niet correct opgehaald: {postcode}, {keuken}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Reservatie/{datum}/{aantalPlaatsen}/Restaurant")]
        public ActionResult<List<Restaurant>> ZoekVrijeRestaurants(DateTime datum, int aantalPlaatsen, string? postcode, string? keuken)
        {
            try
            {
                _logger.LogInformation($"ZoekVrijeRestaurants opgeroepen: {datum}, {aantalPlaatsen}");

                List<Restaurant> restaurants = _restaurantManager.ZoekVrijeRestaurants(datum, aantalPlaatsen, postcode, keuken);

                _logger.LogInformation($"Restaurants correct opgehaald: {datum}, {aantalPlaatsen}");

                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Restaurants niet correct opgehaald: {datum}, {aantalPlaatsen}");
                return BadRequest(ex.Message);
            }
        }
    }
}