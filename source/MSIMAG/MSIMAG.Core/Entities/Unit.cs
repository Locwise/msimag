using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Entities
{
    public class Unit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type {  get; set; }
        public string PropertyId { get; set; }
    }
}
