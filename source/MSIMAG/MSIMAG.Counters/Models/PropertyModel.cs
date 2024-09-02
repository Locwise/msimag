using MSIMAG.Core.Entities;
namespace MSIMAG.Counters.Models
{
    public class PropertyModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Address { get; set; }
        public string City { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public decimal UnitNumber { get; set; }
        public Guid CityID { get; set; }
       
        public string CityName { get; set; }

    }

    public static class PropertyModelExtensions {

        public static PropertyModel ToModel(this Property property) {

            return new PropertyModel { 
                ID = property.ID,
                City= property.City,
                Latitude= property.Latitude,
                Longitude= property.Longitude,
                Address= property.Address,
                UnitNumber= property.UnitNumber,
               // CityID= property.CityID
            };
        
        }

        

    }
}
