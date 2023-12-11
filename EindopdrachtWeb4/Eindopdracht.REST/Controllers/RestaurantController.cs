using Eindopdracht.BL.Managers;
using Eindopdracht.BL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Eindopdracht.REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantManager _manager;

        public RestaurantController(RestaurantManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        [Route("filter")]
        public ActionResult<List<Restaurant>> ZoekRestaurants(string postcode, string keuken)
        {
            try
            {
                List<Restaurant> restaurants = _manager.ZoekRestaurants(postcode, keuken);
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}