using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{
    public class Property
    {
        public Guid ID { get; set; }
        public string City { get; set; }
        public Guid CityID { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string Address { get; set; }
        public decimal UnitNumber { get; set; }
    }
}
