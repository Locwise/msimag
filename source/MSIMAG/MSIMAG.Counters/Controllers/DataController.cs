using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Caching.Memory;
using MSIMAG.Core.Data;
using MSIMAG.Core.Entities;
using MSIMAG.Counters.Models;
using PropertyModel = MSIMAG.Counters.Models.PropertyModel;

namespace MSIMAG.Counters.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataManager _dataManager;
        private readonly IMemoryCache _memoryCache;
        public DataController(IDataManager dataManager, IMemoryCache memoryCache) { 
            _dataManager = dataManager;
            _memoryCache = memoryCache;
        }



        //[HttpGet("cities")]
        //public IActionResult GetCities()
        //{            
        //    var res = _memoryCache.Get<CityModel[]>("cities");
        //    if(res!=null)
        //        return new OkObjectResult(res);

        //    CityModel[] cities = _dataManager.GetCities().ToList()
        //        .Select(c=>new CityModel() {
        //           // Id= c.Id,
        //            Code = (c["pcfcityprefix"] !=null)? c["pcfcityprefix"].ToString():String.Empty                                     
        //        })
        //        .ToArray();
        //    _memoryCache.Set<CityModel[]>("cities",cities,TimeSpan.FromHours(5));
        //    return new OkObjectResult(cities);
        //}

        [HttpGet("properties")]
        public IActionResult GetProperties() 
        {  
            var properties=_dataManager.GetAllProperties();
            var result=new List<PropertyModel>();

            foreach(var property in properties)
            {
                result.Add(property.ToModel());
            }

            return Ok(result.ToArray());        
        }

        /// <summary>
        /// Will return complete info including units
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet("properties/{id}")]
        public IActionResult GetPropertyByID(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("properties/{id}/units")]
        public IActionResult GetPropertyUnits(Guid id)
        {
            var units = _dataManager.GetUnitsByProperty(id);
            var result = new List<UnitModel>();

            foreach (var unit in units)
            {
                result.Add(unit.ToModel());
            }

            return Ok(result.ToArray());

        }

        [HttpGet("properties/city/{city}")]
        public IActionResult GetPropertyByCity(string city)
        {
            var properties = _dataManager.GetPropertiesByCity(city);
            var result = new List<PropertyModel>();

            foreach (var pror in properties) 
            {
                result.Add(pror.ToModel());
            }

            return Ok(result.ToArray());

        }
        [HttpPost("properties/units")]
        public IActionResult GetPropertyUnits2([FromBody] Guid id)
        {
            var units = _dataManager.GetUnitsByProperty(id);
            var result = new List<UnitModel>();

            foreach (var unit in units)
            {
                result.Add(unit.ToModel());
            }

            return Ok(result.ToArray());
        }


        [HttpPost("properties/{id}/units/{unitid}/measure")]
        public IActionResult InsertMeasure(Guid id,Guid unitid)
        {
            throw new NotImplementedException();
        }

        [HttpGet("units")]
        public IActionResult GetUnits()
        {
            var units = _dataManager.GetAllUnits();
            var result = new List<UnitModel>();

            foreach (var unit in units)
            {
                result.Add(unit.ToModel());
            }

            return Ok(result.ToArray());

        }
        [HttpGet("cities")]
        public IActionResult GetCities()
        {
            var cities = _dataManager.GetAllCities();
            var result = new List<CityModel>();

            foreach (var city in cities)
            {
                result.Add(city.ToModel());
            }

            return Ok(result.ToArray());

        }
        [HttpGet("rentalAgreements/unit/{unitId}")]
        public IActionResult GetRentalAgreementsByUnit(string unitId)
        {
            var rentalAgreements = _dataManager.GetRentalAgreementsByUnit(unitId);
            var result = new List<RentalAgreementModel>();

            foreach (var ra in rentalAgreements)
            {
                result.Add(ra.ToModel());
            }

            return Ok(result.ToArray());

        }

    }
}
