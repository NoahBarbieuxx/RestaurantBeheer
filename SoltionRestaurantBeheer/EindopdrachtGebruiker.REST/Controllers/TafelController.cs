//using Eindopdracht.BL.Managers;
//using Eindopdracht.BL.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;

//namespace EindopdrachtGebruiker.REST.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TafelController : ControllerBase
//    {
//        private readonly TafelManager _tafelManager;
//        private readonly ILogger _logger;

//        public TafelController(TafelManager tafelManager, ILoggerFactory loggerFactory)
//        {
//            _tafelManager = tafelManager;
//            _logger = loggerFactory.AddFile("Logs/Tafellogs.txt").CreateLogger("Tafel");
//        }

//        [HttpGet("geeftafelbynummer{tafelnummer}")]
//        public ActionResult<Reservatie> GeefTafelByNummer(string tafelnummer)
//        {
//            try
//            {
//                _logger.LogInformation($"GeefTafelByNummer opgeroepen: {tafelnummer}");
//                Tafel tafel = _tafelManager.GeefTafelByNummer(tafelnummer);

//                if (tafel == null)
//                {
//                    _logger.LogError($"Tafel niet gevonden: {tafelnummer}");
//                    return BadRequest();
//                }
//                else
//                {
//                    _logger.LogInformation($"Tafel correct opgehaald: {tafelnummer}");
//                    return Ok(tafel);
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Tafel niet correct opgehaad: {tafelnummer}");
//                return NotFound(ex.Message);
//            }
//        }

//        [HttpGet("{datum}")]
//        public ActionResult<List<Reservatie>> GeefTafelsByDatum(DateTime datum)
//        {
//            try
//            {
//                _logger.LogInformation($"GeefTafelsByDatum opgeroepen: {datum}");
//                List<Tafel> tafels = _tafelManager.GeefTafelsByDatum(datum);
//                _logger.LogInformation($"Tafels correct opgehaald: {datum}");
//                return Ok(tafels);
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Tafels niet correct opgehaald: {datum}");
//                return NotFound(ex.Message);
//            }
//        }
//    }
//}