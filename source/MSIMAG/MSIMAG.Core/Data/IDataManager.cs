using Locwise.Fireberry.Client;
using Locwise.Fireberry.Client.Metadata;
using MSIMAG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core.Data
{
    public interface IDataManager
    {
        IEnumerable<Rental> GetAllRentals();
        Tenant GetTenantByID(Guid id);
        IEnumerable<Activity> GetAllActivitiesByRental(Guid rentalid);
        IEnumerable<Taarif> GetAllTaarifByRental(Guid rentalid);
        
        bool SaveActivities(IEnumerable<Activity> activities);

        IEnumerable<Unit> GetAllUnits();
        IEnumerable<Property> GetAllProperties();

        IEnumerable<Unit> GetUnitsByProperty(Guid propertyId);

    IEnumerable<Property> GetPropertyByUnit(Guid unitId);

        IEnumerable<Property> GetPropertiesByCity(string city);

        List<PickupListValue> GetPickupListValue(string objectn, string fieldName);
         void CreateMeasuere(Measure measure);

         IEnumerable<RentalAgreement> GetRentalAgreementsByUnit(string unitId);
         IEnumerable<Measure> GetMeasureByUnit(string unitId);
         IEnumerable<City> GetAllCities();


      
        IEnumerable<Record> GetCities();

        Record? GetAccountByID(Guid id);

        Record GetRecord(Guid id, int typeCode);
        bool UpdateRecord(Record record);
        bool CreateTask(Dictionary<string, object> dic);
        void saveNote(Record record,string comment);
        RecordsCollection GetMappings(string filetype);

        List<ObjectTypeFields> GetObjectTypeByType(int type);
        Record GetDocumentType(string type);
    }
}
