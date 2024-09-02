using MSIMAG.Core.Entities;

namespace MSIMAG.Counters.Models
{
    public class UnitModel
    {
        public Guid ID { get; set; } = Guid.NewGuid();
        public string Name { get; set; }=String.Empty;
        public string Type { get; set; }=String.Empty;
        public string PropertyId { get; set; }

    }
    public static class UnitModelExtensions
    {


        public static UnitModel ToModel(this Unit unit)
        {

            return new UnitModel
            {
                ID = unit.Id,
                Name = unit.Name,
                Type = unit.Type,
                PropertyId = unit.PropertyId,
                
            };

        }
    }
}
