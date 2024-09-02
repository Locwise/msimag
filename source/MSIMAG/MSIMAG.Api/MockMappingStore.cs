
namespace MSIMAG.Api
{
    public class MockMappingStore : IMappingStore
    {
        public IEnumerable<Mapping> GetMappings(int filetype)
        {
          
            List<Mapping> mappings = new List<Mapping>();

            mappings.Add(new Mapping()
            {
                SourceField = "pcfdatesigning",
                TargetField = "pcfstartofrental",
                Override = false,
                RelatedTo = "pcflurentaloffer",
                TargetObjectName = "Rental Agreement",
                TargetObjectType = 1005
            }); 
            mappings.Add(new Mapping()
            {
                SourceField = "pcfdatestartagreement",
                TargetField = "pcfleasestartdate",
                Override = false,
                RelatedTo= "pcflurentaloffer",
                TargetObjectName = "Rental Agreement",
                TargetObjectType = 1005

            });
            mappings.Add(new Mapping()
            {
                SourceField = "pcfnumminrentperiod",
                TargetField = "pcfminimumrentalperiodyears",
                Override = false,
                RelatedTo = "pcflurentaloffer",
                TargetObjectName = "Rental Agreement",
                TargetObjectType = 1005

            });

            return mappings;



        }


        public IEnumerable<Mapping> GetMappings(string filetype)
        {

            return GetMappings(10);

        }
    }
}
