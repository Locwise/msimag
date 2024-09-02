namespace MSIMAG.Api
{
    public interface IMappingStore
    {


        IEnumerable<Mapping> GetMappings(int filetype);
        IEnumerable<Mapping> GetMappings(string filetype);


    }


  
}
