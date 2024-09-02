using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{
    public class City
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
    }
}
