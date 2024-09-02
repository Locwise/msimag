using MSIMAG.Core.Entities;

namespace MSIMAG.Counters.Models
{
    public class CityModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Code { get; set; }
    }
    public static class CityModelExtensions
    {

        public static CityModel ToModel(this City city)
        {

            return new CityModel
            {
                ID = city.ID,
                Latitude = city.Latitude,
                Longitude = city.Longitude,
                Name = city.Name,
            };

        }
    }
    }
