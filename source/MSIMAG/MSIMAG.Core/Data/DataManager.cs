using Locwise.Fireberry.Client;
using Locwise.Fireberry.Client.Metadata;
using Microsoft.AspNetCore.Http;
using MSIMAG.Core.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MSIMAG.Core.Data
{
    public class DataManager : IDataManager
    {
        private readonly IFireberryClient _client;

        public DataManager(IFireberryClient client)
        {
            _client = client;
            Init();
        }

        private void Init()
        {

        }

        public IEnumerable<Activity> GetAllActivitiesByRental(Guid rentalid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Rental> GetAllRentals()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Taarif> GetAllTaarifByRental(Guid rentalid)
        {
            throw new NotImplementedException();
        }

        public Tenant GetTenantByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SaveActivities(IEnumerable<Activity> activities)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Unit> GetAllUnits()
        {
            var records = _client.GetAllRecords(Consts.UNIT_OBJECT_ID, new IFireberryClientOptions()
            {
                PageSize = 1000
            });
            var result = new List<Unit>();
            List<PickupListValue> unitTypePickupListValue = GetPickupListValue("customobject1004", $"{Consts.RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST}");


            foreach (var record in records)
            {
                result.Add(new Unit
                {
                    Id = record.Id,
                    Name = record[Consts.RENTAL_UNIT_FIELD_UNIT_NAME_TEXT]?.ToString(),
                    Type = findFieldName(unitTypePickupListValue, record[Consts.RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST])?.ToString(),
                    PropertyId = record[Consts.RENTAL_UNIT_FIELD_PROPERTY_LOOKUP]?.ToString(),
                });
            }

            return result;
        }
        public List<PickupListValue> GetPickupListValue(string objectn, string fieldName)
        {
            var type = _client.GetObjectType(objectn, true);
            var field = type.Fields.FirstOrDefault(x => x.FieldName == fieldName);
            List<PickupListValue> r = new List<PickupListValue>();

            foreach (var x in field.values)
            {
                r.Add(new PickupListValue()
                {
                    name = x.name,
                    value = x.value
                });
            }

            return r;
        }
        private string findFieldName(List<PickupListValue> values, object v)
        {
            foreach (var item in values)
            {
                if (v != null)
                {
                    if (item.value.ToString() == v.ToString())
                    {
                        return item.name;
                    }
                }
            }

            return null; // או ערך ברירת מחדל אחר שתבחר
        }

        public IEnumerable<Property> GetAllProperties()
        {
            var records = _client.GetAllRecords(Consts.PROPERTY_OBJECT_ID, new IFireberryClientOptions()
            {
                PageSize = 1000
            });

            var result = records.Select(record => new Property
            {
                ID = record.Id,
                City = record[Consts.PROPERTY_FIELD_CITY]?.ToString(),
                Longitude = float.TryParse(record[Consts.PROPERTY_FIELD_LONGITUDE]?.ToString(), out var longitude) ? longitude : 0,
                Latitude = float.TryParse(record[Consts.PROPERTY_FIELD_LATITUDE]?.ToString(), out var latitude) ? latitude : 0,
                Address = record[Consts.PROPERTY_FIELD_FULL_ADDRESS]?.ToString(),
                UnitNumber = decimal.TryParse(record[Consts.PROPERTY_FIELD_NO_OF_UNITS]?.ToString(), out var unitNumber) ? unitNumber : 0,
                CityID = record[Consts.PROPERTY_FIELD_CITY_LOOKUP] != null ? Guid.Parse(record[Consts.PROPERTY_FIELD_CITY_LOOKUP].ToString()) : Guid.Empty
            }).ToList();

            return result;
        }
        public IEnumerable<City> GetAllCities()
        {
            var records = _client.GetAllRecords(Consts.CITY_OBJECT_ID, new IFireberryClientOptions()
            {
                PageSize = 1000
            });

            var result = records.Select(record => new City
            {
                ID = record.Id,
                Name = record[Consts.CITY_FIELD_NAME_TEXT]?.ToString(),
                Longitude = float.TryParse(record[Consts.CITY_FIELD_LONGITUDE_NUMBER]?.ToString(), out var longitude) ? longitude : 0,
                Latitude = float.TryParse(record[Consts.CITY_FIELD_LATITUDE_NUMBER]?.ToString(), out var latitude) ? latitude : 0,

            }).ToList();

            return result;
        }

        public IEnumerable<Unit> GetUnitsByProperty(Guid propertyId)
        {

            var records = _client.Query("customobject1004", new string[] { "customobject1004id", $"{Consts.RENTAL_UNIT_FIELD_UNIT_NAME_TEXT}", $"{Consts.RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST}" }, $"({Consts.RENTAL_UNIT_FIELD_PROPERTY_LOOKUP} = '{propertyId}')");
            var result = new List<Unit>();
            List<PickupListValue> unitTypePickupListValue = GetPickupListValue("customobject1004", $"{Consts.RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST}");

            foreach (var record in records)
            {

                result.Add(new Unit
                {
                    Id = Guid.Parse(record["customobject1004id"].ToString()),
                    Name = record[Consts.RENTAL_UNIT_FIELD_UNIT_NAME_TEXT]?.ToString(),
                    Type = record[Consts.RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST]?.ToString(),
                    PropertyId = record[Consts.RENTAL_UNIT_FIELD_PROPERTY_LOOKUP].ToString()

                });
            }

            return result;
        }

        public IEnumerable<Property> GetPropertyByUnit(Guid unitId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Property> GetPropertiesByCity(string city)
        {
            var records = _client.Query("customobject1003", new string[] { "customobject1003id", $"{Consts.PROPERTY_FIELD_CITY}", $"{Consts.PROPERTY_FIELD_LONGITUDE}", $"{Consts.PROPERTY_FIELD_FULL_ADDRESS}", $"{Consts.PROPERTY_FIELD_LATITUDE}" }, $"({Consts.PROPERTY_FIELD_CITY} = '{city}')");
            var result = records.Select(record => new Property
            {
                ID = Guid.Parse(record["customobject1003id"].ToString()),
                City = record[Consts.PROPERTY_FIELD_CITY]?.ToString(),
                Longitude = float.TryParse(record[Consts.PROPERTY_FIELD_LONGITUDE]?.ToString(), out var longitude) ? longitude : 0,
                Latitude = float.TryParse(record[Consts.PROPERTY_FIELD_LATITUDE]?.ToString(), out var latitude) ? latitude : 0,
                Address = record[Consts.PROPERTY_FIELD_FULL_ADDRESS]?.ToString(),
                UnitNumber = decimal.TryParse(record[Consts.PROPERTY_FIELD_NO_OF_UNITS]?.ToString(), out var unitNumber) ? unitNumber : 0
            }).ToList();

            return result;
        }

        public void CreateMeasuere(Measure measure)
        {
            var record = _client.CreateNew(Consts.MEASURE_OBJECT_ID);
            record[Consts.MEASURE_FIELD_POSITION_PICKLIST] = measure.Position;
            record[Consts.MEASURE_FIELD_COUNTER_TYPE_PICKLIST] = measure.CounterType;
            record[Consts.MEASURE_FIELD_PROTOCOL_PICKLIST] = measure.Protocol;
            record[Consts.MEASURE_FIELD_UNIT_LOOKUP] = measure.Unit;
            record[Consts.MEASURE_FIELD_DATE_DATE] = DateTime.Today;
            record[Consts.MEASURE_FIELD_METET_READING_NUMBER] = measure.MeterReading;
            record[Consts.MEASURE_FIELD_METET_NUMBER_TEXT] = measure.MeterNumber;
            record[Consts.MEASURE_FIELD_MAIN_METER_PICKLIST] = measure.IsMainCounter;
            if (measure.RentalAgreement != "null")
            {
                record[Consts.MEASURE_FIELD_RENTAL_AGREEMENT_LOOKUP] = measure.RentalAgreement;
            }

            IFormFile file = measure.FileUpload;
            long length = file.Length;
            using var fileStream = file.OpenReadStream();
            byte[] bytes = new byte[length];
            fileStream.Read(bytes, 0, (int)file.Length);
            string filecontent = Convert.ToBase64String(bytes);
            record.AddFile(file.FileName, filecontent);

            var v = _client.InsertRecord(record);

        }

        public IEnumerable<RentalAgreement> GetRentalAgreementsByUnit(string unitId)
        {
            var records = _client.Query("customobject1005", new string[] { "customobject1005id", $"{Consts.RENTAL_AGREEMENT_FIELD_AGREEMENT_STATUS_PICKLIST}", $"{Consts.RENTAL_AGREEMENT_FIELD_NAME_TEXT}", $"{Consts.RENTAL_AGREEMENT_FIELD_STATUS_OFFER_A_PICKLIST}", $"{Consts.RENTAL_AGREEMENT_FIELD_STATUS_REASON_PICKLIST}" }, $"({Consts.RENTAL_AGREEMENT_FIELD_UNIT_LOOKUP} = '{unitId}')");

            //var records = _client.Query("customobject1005", new string[] { "customobject1005id", "pcfsystemfield100", "name", "pcfstatusoffera", "pcfstatusreason" }, $"(pcfunit = '{"db5db874-9ec3-4d71-bd23-a3991a00dcae"}')");
            var result = records.Select(record => new RentalAgreement
            {
                ID = Guid.Parse(record[Consts.RENTAL_AGREEMENT_FIELD_ID].ToString()),
                StatuReason = record[Consts.RENTAL_AGREEMENT_FIELD_STATUS_REASON_PICKLIST]?.ToString(),
                RentalOffersName = record[Consts.RENTAL_AGREEMENT_FIELD_NAME_TEXT]?.ToString(),
                AgreementStatus = record[Consts.RENTAL_AGREEMENT_FIELD_AGREEMENT_STATUS_PICKLIST]?.ToString(),
                StatusOfferA = record[Consts.RENTAL_AGREEMENT_FIELD_STATUS_OFFER_A_PICKLIST]?.ToString()
            }).ToList();

            return result;
        }

        public IEnumerable<Measure> GetMeasureByUnit(string unitId)
        {
            var records = _client.Query("customobject1022", new string[] { "name", $"{Consts.MEASURE_FIELD_DATE_DATE}", $"{Consts.MEASURE_FIELD_METET_READING_NUMBER}", $"{Consts.MEASURE_FIELD_COUNTER_TYPE_PICKLIST}" }, $"({Consts.MEASURE_FIELD_UNIT_LOOKUP} = '{unitId}')");

            var result = records.Select(record => new Measure
            {
                Date = DateTime.Parse(record[Consts.MEASURE_FIELD_DATE_DATE].ToString()),
                MeterReading = record[Consts.MEASURE_FIELD_METET_READING_NUMBER]?.ToString(),
                CounterType = record[Consts.MEASURE_FIELD_COUNTER_TYPE_PICKLIST]?.ToString(),

            }).ToList();

            return result;
        }




        public IEnumerable<Record> GetCities()
        {
            return _client.GetAllRecords(1023).ToArray();
        }

        public Record? GetAccountByID(Guid id)
        {
            return _client.GetRecordById(1, id);
        }
        public Record GetRecord(Guid id, int typeCode)
        {
            var record = _client.GetRecordById(typeCode, id);
            return record;
        }
        public bool UpdateRecord(Record record)
        {
            var res = _client.UpdateRecord(record);
            return res;
        }
        public bool CreateTask(Dictionary<string, object> dic)
        {
            var record = _client.CreateNew(10);
            foreach (var item in dic)
            {
                record[item.Key] = item.Value;
            }
            _client.InsertRecord(record, false);
            return false;
        }
        public void saveNote(Record record, string comment)
        {
            var res = record.CreateNote(comment);
        }
        public RecordsCollection GetMappings(string filetype)
        {
            var record = _client.Query($"{Consts.DOCUMENT_TYPE_SYSTEM_NAME}", new string[] { "name", "pcfsourcecol1" }, $"(name = '{filetype}')");
            //_client.Metadata.ValidateFields
            return record;
        }
        public List<ObjectTypeFields> GetObjectTypeByType(int type)
        {
            var objType = _client.GetObjectType(type, true).Fields;
            var result = objType.Select(obj => new ObjectTypeFields
            {
                Label = obj.Label,
                FieldName = obj.FieldName,
                FieldType = obj.SystemFieldTypeName
            }).ToList();
            return result;
        }

        public Record GetDocumentType(string type)
        {
            RecordsCollection records = _client.Query($"{Consts.DOCUMENT_TYPE_SYSTEM_NAME}", new string[] {  }, $"({Consts.DOCUMENT_TYPE_NAME} = {type})");
            var id= records.FirstOrDefault().Id;
            if (records.Any())
            {
                var record=GetRecord(id, 1033);
                return record;
            }
            return records.FirstOrDefault();
        }

    }
}
