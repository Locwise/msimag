using Google.Type;
using Locwise.Fireberry.Client;
using Locwise.Fireberry.Client.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MSIMAG.Core;
using MSIMAG.Core.Data;
using MSIMAG.Core.Entities;
using MSIMAG.Counters.Models;
using static Google.Cloud.Vision.V1.ProductSearchResults.Types;

namespace MSIMAG.Counters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasureController : ControllerBase
    {
        private readonly IFireberryClient _fireberryClient;
        private readonly IDataManager _dataManager;
        private readonly IMemoryCache _memoryCache;

        private readonly ILogger<MeasureController> _logger;

        public MeasureController(ILogger<MeasureController> logger, IFireberryClient fireberryClient,
            IDataManager dataManager,
            IMemoryCache memoryCache)
        {
            _logger = logger;
            _fireberryClient = fireberryClient;
            _dataManager = dataManager;
            _memoryCache = memoryCache;
        }
        [HttpGet("protocol")]
        public IActionResult GetMeasureProtocol()
        {
            List<PickupListValue> protocols = _dataManager.GetPickupListValue("customobject1022","pcfplprotokol");
            var result = new List<PropertyModel>();



            return Ok(protocols.ToArray());
        }
        [HttpGet("position")]
        public IActionResult GetMeasurePosition()
        {
            List<PickupListValue> protocols = _dataManager.GetPickupListValue("customobject1022", "pcfplposition");
            var result = new List<PropertyModel>();



            return Ok(protocols.ToArray());
        }
        [HttpGet("type")]
        public IActionResult GetMeasureCounterType()
        {
            List<PickupListValue> protocols = _dataManager.GetPickupListValue("customobject1022", "pcfplmetertype");
            var result = new List<PropertyModel>();



            return Ok(protocols.ToArray());
        }
        [HttpGet("unit/{unitId}")]
        public IActionResult GetMeasureByUnit(string unitId)
        {
            var mesure = _dataManager.GetMeasureByUnit(unitId);
            var result = new List<MeasureModel>();

     
                foreach (var m in mesure)
                {
                    result.Add(new MeasureModel { Date = m.Date, CounterType = m.CounterType, MeterReading = m.MeterReading });
                }
            
            return Ok(result.ToArray()); ;
        }

        [HttpPost("process")]
        public IActionResult ProcesssImage(IFormFile file) {

            var result = new List<string>();
            if(Random.Shared.Next(0, 10) > 5)
            {
                return new OkObjectResult(new string[] { });
            }

            if (Random.Shared.Next(0, 10) > 5)
            {
                result.Add("ABC1234567890");
                result.Add("4r-234068767");
                result.Add("34543fhgfhjhg645645");
            }
            else {
                result.Add("456456456456");
                result.Add("AAAAAAAAAAAAA");
            }

                return new OkObjectResult(result);

        }

        [HttpPost]
        public IActionResult CreateNewMeasure([FromForm] Measure newMeasure)
        {
            try
            {
                _dataManager.CreateMeasuere(newMeasure);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //public IActionResult Index()
        //{
        //    List<Unit> units = null;
        //    if (_memoryCache.TryGetValue("units", out units))
        //    {

        //    }
        //    else
        //    {
        //        units = _dataManager.GetAllUnits().OrderBy(x=>x.Name).ToList();
        //        _memoryCache.Set<List<Unit>>("units", units);
        //    }

        //    ViewBag.Units = units;

        //    var countertypes = new List<LookupItem<int>>();
        //    foreach (var item in Enum.GetValues(typeof(CounterType)))
        //    {
        //        countertypes.Add(new LookupItem<int>()
        //        {
        //            Id = (int)item,
        //            Name = Enum.GetName(typeof(CounterType), item).Replace("_"," ")
        //        });
        //    }

        //    ViewBag.Countertypes = countertypes;    



        //    var protocols= new List<LookupItem<int>>();
        //    foreach (var item in Enum.GetValues(typeof(Protocol)))
        //    {
        //        protocols.Add(new LookupItem<int>()
        //        {
        //            Id = (int)item,
        //            Name = Enum.GetName(typeof(Protocol), item).Replace("_", " ")
        //        });
        //    }

        //    ViewBag.Protocols = protocols;

        //    return View();
        //}



        //[HttpPost]
        //public IActionResult SaveMeasure()
        //{
        //    return View();
        //}
    }


    public class LookupItem<Key> {
        public Key Id { get; set; }
        public string Name { get; set; }

    }
}
