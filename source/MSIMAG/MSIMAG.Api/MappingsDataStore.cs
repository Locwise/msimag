
using Locwise.Fireberry.Client;
using Microsoft.Extensions.Caching.Memory;
using MSIMAG.Core.Entities;
using MSIMAG.Core;

namespace MSIMAG.Api
{
    public class MappingsDataStore : IMappingStore
    {
        private readonly IFireberryClient _client;
      
        private readonly IMemoryCache _cache;

        public MappingsDataStore(IFireberryClient client, IMemoryCache cache)
        {
            _client= client;
            _cache= cache;
        }

        public IEnumerable<Mapping> GetMappings(int filetype)
        {
            var result= new List<Mapping>();
            if (_cache.TryGetValue($"mappings_{filetype}", out result)) {
                return result ?? new List<Mapping>();
            }
            else
            {
                result = LoadMappings(filetype);
                if(result!=null)
                {
                    _cache.Set<List<Mapping>>($"mappings_{filetype}", result);
                }
                return result ?? new List<Mapping>();
            }
        }

        public IEnumerable<Mapping> GetMappings(string filetype)
        {
            var result = new List<Mapping>();
            if (_cache.TryGetValue($"mappings_{filetype}", out result))
            {
                return result ?? new List<Mapping>();
            }
            else
            {
                result = LoadMappings(filetype);

                if (result != null)
                {
                    _cache.Set<List<Mapping>>($"mappings_{filetype}", result);
                }
                return result ?? new List<Mapping>();
            }
        }

        /// <summary>
        /// Loads DocumentTypes from the crm and build mappings
        /// </summary>
        private List<Mapping> LoadMappings(int filetype) {

            throw new NotImplementedException();

        }

        /// <summary>
        /// Loads DocumentTypes from the crm and build mappings
        /// </summary>
        private List<Mapping> LoadMappings(string filetype)
        {
            var records = _client.Query($"{Consts.DOCUMENT_TYPE_SYSTEM_NAME}", new string[] { 
                $"{Consts.DOCUMENT_TYPE_NAME}", 
                $"{Consts.DOCUMENT_TYPE_RELATED_TO_ID}", 
                $"{Consts.DOCUMENT_TYPE_DESTINATION_OBJECT_TYPE}", 
                $"{Consts.DOCUMENT_TYPE_DESTINATION_OBJECT_NAME}", 
                $"{Consts.DOCUMENT_TYPE_SOURCE_COL1}", 
                $"{Consts.DOCUMENT_TYPE_SOURCE_COL2}",
                $"{Consts.DOCUMENT_TYPE_SOURCE_COL3}",
                $"{Consts.DOCUMENT_TYPE_SOURCE_COL4}",
                $"{Consts.DOCUMENT_TYPE_SOURCE_COL5}",
                $"{Consts.DOCUMENT_TYPE_SOURCE_COL6}", 
                $"{Consts.DOCUMENT_TYPE_DESTINATION_COL1}",
                $"{Consts.DOCUMENT_TYPE_DESTINATION_COL2}",
                $"{Consts.DOCUMENT_TYPE_DESTINATION_COL3}",
                $"{Consts.DOCUMENT_TYPE_DESTINATION_COL4}",
                $"{Consts.DOCUMENT_TYPE_DESTINATION_COL5}",
                $"{Consts.DOCUMENT_TYPE_DESTINATION_COL6}", 
                $"{Consts.DOCUMENT_TYPE_STATUS}" }, 
                $"({Consts.DOCUMENT_TYPE_NAME} = {filetype})");

            List<Mapping> mappings = new List<Mapping>();
            string soutce = "pcfsourcecol";
            string destination = "pcfdestinationcol";
            try
            {
                foreach (var record in records)
            {
                    var t=record.Get($"{Consts.DOCUMENT_TYPE_STATUS}", "");
                    if (record[$"{Consts.DOCUMENT_TYPE_STATUS}"] == "" || record.Get($"{Consts.DOCUMENT_TYPE_STATUS}", "") !="Active")
                    {
                        return null;
                    }

                        for (int i = 1; i <= 6; i++)
                {
                    string sourceNow = soutce + i;
                    string destinationNow = destination + i;
                    if (record[sourceNow] != null || record[destinationNow] != null)
                        mappings.Add(new Mapping
                        {
                        SourceField = record[sourceNow].ToString(),
                        TargetField = record[destinationNow].ToString(),
                        TargetObjectType = ConvertToInt(record[$"{Consts.DOCUMENT_TYPE_DESTINATION_OBJECT_TYPE}"].ToString()), // המרת הערך לטיפוס int
                        TargetObjectName = record[$"{Consts.DOCUMENT_TYPE_DESTINATION_OBJECT_NAME}"].ToString(),
                        Override = false,
                        RelatedTo = record[$"{Consts.DOCUMENT_TYPE_RELATED_TO_ID}"].ToString()
                        });
                }
            }
            }
            catch (Exception ex)
            {
                // טיפול בשגיאה, לדוגמה רישום הודעת שגיאה
                return null;
            }

            return mappings;

        }
        private int ConvertToInt(string numberString)
        {
            if (decimal.TryParse(numberString, out decimal numberDecimal))
            {
                return (int)numberDecimal;
            }
            else
            {
                throw new FormatException("The string is not in a correct format.");
            }
        }
    }
}
