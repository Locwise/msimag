

using Locwise.Fireberry.Client;
using MSIMAG.Core;
using System.Data;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Security.Principal;
using OfficeOpenXml;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata.Ecma335;
using MSIMAG.Core.Entities;

namespace MSIMAG.ProprtyInitializer
{
    public class Program
    {
        public static Record? property = null;
        private static IFireberryClient _client;
        private static string folder=String.Empty;
        public const string ENERGY_REPORT_PARSER_URL = "";
        private static DataSet dsDataFile=new DataSet();
        private static Dictionary<string, Guid> cities;
       

        public static int Main(string[] args)
        {
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            Console.WriteLine("MSIMAG Property Initializer Started");

            Arguments arguments=Arguments.Parse(args);
            var valid = arguments.IsValid();
            if (valid.Item1 == false) { 
                Console.WriteLine(valid.Item2);
                return -1;
            }
            try
            {
                folder = arguments.DirectoryPath;
                dsDataFile = LoadExcelFile(arguments.DataFilePath);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed To Load Data File");
                return -2;
            }

            Console.WriteLine("Initializing Client");
            FireberryClientFactory factory = new FireberryClientFactory();
            _client = factory.Create(Consts.API_KEY);

            cities = GetValuesForObject(1023, "name");

            if (CreateProperty() == false)
            {
                Console.WriteLine("Failed To Create Property");
                return -1;
            }
            ParseRows();

            return 0;
        }

        /// <summary>
        /// Create Property Based on the excell, use energy report when available
        /// </summary>                                                
        public static bool CreateProperty() 
        {
            property = _client.CreateNew(Consts.PROPERTY_OBJECT_ID);
            property[Consts.PROPERTY_FIELD_PROPERTY_NAME] = dsDataFile.Tables["Property Info"].Rows[0]["Property Name"].ToString();
            
            var city= dsDataFile.Tables["Property Info"].Rows[0]["City"].ToString();

            if (cities.ContainsKey(city))
            {
                property[Consts.PROPERTY_FIELD_CITY] = cities[city].ToString();
            }
            
            property[Consts.PROPERTY_FIELD_PREFIX] = dsDataFile.Tables["Property Info"].Rows[0]["Property PREFIX"].ToString();
            property[Consts.PROPERTY_FIELD_COMPANY] = CreateOrGetCompanyByNumber(dsDataFile.Tables["Property Info"].Rows[0]["Company Number"].ToString());
            property[Consts.PROPERTY_FIELD_STREET] = dsDataFile.Tables["Property Info"].Rows[0]["Street"].ToString();
            property[Consts.PROPERTY_FIELD_POSTAL_CODE] = dsDataFile.Tables["Property Info"].Rows[0]["Postal Code"].ToString();
            property[Consts.PROPERTY_FIELD_YEAR_BUILT] = dsDataFile.Tables["Property Info"].Rows[0]["Year Built"].ToString();
            property[Consts.PROPERTY_FIELD_NUMBER_OF_FLOORS] = dsDataFile.Tables["Property Info"].Rows[0]["# Floors"].ToString();
            property[Consts.PROPERTY_FIELD_TOTAL_NUMBER_OF_UNITS] = dsDataFile.Tables["Property Info"].Rows[0]["# Units"].ToString();
            property[Consts.PROPERTY_FIELD_HOUSE_NUMBER] = dsDataFile.Tables["Property Info"].Rows[0]["House Number"].ToString();
            property[Consts.PROPERTY_FIELD_TOTAL_PROPERTY_SIZE]= dsDataFile.Tables["Property Info"].Rows[0]["Total Area"].ToString();

            var energyreport = FindEnergyReport();
            if (energyreport != null)
            {
                Console.Write($"Energy Report Found:{Path.GetFileName(energyreport)}");
                var reportdata=ParseEnergyReport(energyreport); //Try to parse the report
                if (reportdata != null) 
                { 
                    //TODO :Populate property's data from the report                    
                }
            }
            else 
            {
                Console.Write($"Energy Report Not Found");
            }
           
            Console.WriteLine($"Creating Property: {JsonSerializer.Serialize<Record>(property)}");
            Console.WriteLine("Press Y to continue, N to cancel");
            var res=Console.ReadLine();
            if (res==null || res.ToUpper()!="Y")
            {
                Console.WriteLine("Canceled By User");
                return false;
            }
            if (energyreport != null)
            {
                byte[] AsBytes = File.ReadAllBytes(energyreport);
                String AsBase64String = Convert.ToBase64String(AsBytes);
                property.AddFile(Path.GetFileName(energyreport), AsBase64String);
            }

            FixValues(property);
            var serveresult=_client.InsertRecord(property);

            if (serveresult.success)
            {
                property = serveresult.created;
                return true;

            }
            else 
            { 
                return false;
            }
        }

        private static Guid CreateOrGetCompanyByNumber(string companynumber)
        {
            var companies = _client.Query(1000, new string[] { }, $"pcfcompanynumber = '{companynumber.ToLower()}'");

            if(companies.Count() > 0)
            {
                return companies.First().Id;
            }
          
            var record=_client.CreateNew(1000);
            record["name"]=companynumber;
            record["pcfcompanynumber"]=companynumber;
            var created = _client.InsertRecord(record).created;
            return created.Id;
                       
        }

        private static string? FindEnergyReport() {
            var fileslist = Directory.EnumerateFiles(folder);
            foreach (var file in fileslist)
            {
                if (Path.GetExtension(file) == ".pdf") 
                    return file;
            }
            return null;    
        }


        public static int ParseRows()
        {
            if (!dsDataFile.Tables.Contains("Rental Units") || dsDataFile.Tables["Rental Units"].Rows.Count==0)
            {
                Console.WriteLine("Sheet Units is missing or empty!");
                return 0;
            }

            int i = 0;
            foreach (DataRow row in dsDataFile.Tables["Rental Units"].Rows) {
                ProcessRow(row);
                i++;
            }

            return i;

        }

        public static void ProcessRow(DataRow row) 
        {
          
            var unit = CreateUnit(row);
            if(unit== null)
            {
                throw new Exception("Failed To Create Unit");
            }

            bool hasTanent = row["Tenant name"] != DBNull.Value;
            if (hasTanent) 
            {
                (bool,Record) account_result = CreateOrUpdateAccount(row);
                CreateRentalAgreement(row, account_result.Item2.Id, unit.Id);
            }
        } 

        public static Record? CreateUnit(DataRow row) 
        {
            var unit = _client.CreateNew(Consts.RENTAL_UNIT_OBJECT_ID);
            unit[Consts.RENTAL_UNIT_FIELD_PROPERTY_LOOKUP] = property.Id;
            //unit[Consts.RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST]=

                      
            if (row["Einheitennummer"]!=DBNull.Value)       unit[Consts.RENTAL_UNIT_FIELD_UNIT_NUMBER_TEXT] =  row["Einheitennummer"];
            if (row["Kalt/Sqm"] != DBNull.Value)            unit[Consts.RENTAL_UNIT_FIELD_KALT_SQM_NUMBER]  =  row["Kalt/Sqm"];
            if (row["BK/Sqm"] != DBNull.Value)              unit[Consts.RENTAL_UNIT_FIELD_BK_SQM_NUMBER]    =  row["BK/Sqm"];
            if (row["HK/Sqm"] != DBNull.Value)              unit[Consts.RENTAL_UNIT_FIELD_HK_SQM_NUMBER]    =  row["HK/Sqm"];
            if (row["Einheitengroße"] != DBNull.Value)      unit[Consts.RENTAL_UNIT_FIELD_UNIT_SIZE_NUMBER] =  row["Einheitengroße"];
            if (row["Etage"] != DBNull.Value)               unit[Consts.RENTAL_UNIT_FIELD_FLOOR_PICKLIST] = row["Etage"];
            if (row["Standort"] != DBNull.Value)            unit[Consts.RENTAL_UNIT_FIELD_LOCATION_IN_FLOOR_PICKLIST] = row["Standort"];
            if (row["# Zimmern"] != DBNull.Value)           unit[Consts.RENTAL_UNIT_FIELD_ROOMS_NUMBER] = row["# Zimmern"];
            if (row["# Balkon"] != DBNull.Value)            unit[Consts.RENTAL_UNIT_FIELD_NUMBER_OF_BALCONIES_NUMBER] = row["# Balkon"];
            if (row["Balkonflächengrößen"] != DBNull.Value) unit[Consts.RENTAL_UNIT_FIELD_BALCONY_SIZE_NUMBER] = row["Balkonflächengrößen"];
            if (row["# Gardens"] != DBNull.Value)           unit[Consts.RENTAL_UNIT_FIELD_GARDEN_SIZE_NUMBER] = row["# Gardens"];
            if (row["Gartenflächengrößen"] != DBNull.Value) unit[Consts.RENTAL_UNIT_FIELD_GARDEN_SIZE_NUMBER] = row["Gartenflächengrößen"];

            FixValues(unit);

            var res=_client.InsertRecord(unit);
            return res.success?res.created:null;
        }

        private static void SetValueOnRecord(Record record,DataRow row,string fieldname,string columnname)
        {
            if (row[columnname] == DBNull.Value)
                return;
            record[fieldname] = row[columnname];

            if (record[fieldname] == null || String.IsNullOrEmpty(record[fieldname].ToString()))
            {
                return;
            }
            var field = _client.GetFieldInfo(record.TypeIdentifier.TypeID, fieldname);
            if (field == null)
            {
                return;
            }
            if (field.IsPicklist && field.values != null)
            {
                var val = field.values.FirstOrDefault(x => x.name.ToLower() == record[fieldname].ToString().ToLower());
                if (val != null)
                {
                    record[fieldname] = val.value;
                }
            }

            if (field.IsNumber)
            {
                if (field.Precision == null || field.Precision.Value == 0)
                {
                    record[fieldname] = int.Parse(record[fieldname].ToString());
                }
                else
                {
                    record[fieldname] = float.Parse(record[fieldname].ToString());
                }

            }

        }

        public static Record? CreateRentalAgreement(DataRow row, Guid accountid, Guid unitid) 
        {
            var agreeemnet = _client.CreateNew(Consts.RENTAL_AGREEMENT_OBJECT_ID);
            agreeemnet[Consts.RENTAL_AGREEMENT_FIELD_RENATAL_UNIT_LOOKUP] = unitid;
            agreeemnet[Consts.RENTAL_AGREEMENT_FIELD_MIETER_LOOKUP] = accountid;
            if (row["Current HK/Sqm"]!=DBNull.Value) 
            {
                double d;
                if (double.TryParse(row["Current HK/Sqm"].ToString(), out d)) {
                    agreeemnet["pcfhksqm"] = d;  
                }
            }

            if (row["Current BK/Sqm"] != DBNull.Value)
            {
                double d;
                if (double.TryParse(row["Current BK/Sqm"].ToString(), out d))
                {
                    agreeemnet["pcfhksqm"] = d;
                }
            }

            if (row["Current Kalt/Sqm"] != DBNull.Value)
            {
                double d;
                if (double.TryParse(row["Current Kalt/Sqm"].ToString(), out d))
                {
                    agreeemnet["pcfcoldrentsqm"] = d;
                }
            }


            if (row["Start day"] != DBNull.Value)
            {
                //?Contract Activation Date
                DateTime d;
                if (DateTime.TryParse(row["Start day"].ToString(), out d))
                {
                    agreeemnet["pcfstartofrental"] = d;
                }
            }

            if (row["Min period(Years)"] != DBNull.Value) 
            {
                int years;
                if (int.TryParse(row["Min period(Years)"].ToString(), out years)) {
                    agreeemnet["pcfminimumrentalperiod"]= years.ToString();
                }
            }

            if (row["Mietstrategie"] != DBNull.Value)
            {              
                agreeemnet["pcfplstrategy"] = row["Mietstrategie"].ToString();                
            }

            if (row["Finish day"] != DBNull.Value)
            {
                //should it be (Lease End Date) pcfexitdate instead?
                DateTime d;
                if (DateTime.TryParse(row["Finish day"].ToString(), out d))
                {
                    agreeemnet["pcfcontractterminationdate"] = d;
                }
            }
           
      
            FixValues(agreeemnet);
             var res =_client.InsertRecord(agreeemnet);
            return res.created;
        }

        private static void FixValues(Record row) 
        {
            foreach (var key in row.Keys)
            {
                if (row[key] == null || String.IsNullOrEmpty(row[key].ToString()))
                {
                    continue;
                }
                var field = _client.GetFieldInfo(row.TypeIdentifier.TypeID, key);
                if (field == null) {
                    continue;
                }
                if (field.IsPicklist && field.values !=null)
                {
                    var val = field.values.FirstOrDefault(x => x.name.ToLower() == row[key].ToString().ToLower());
                    if (val != null)
                    {
                        row[key]=val.value;
                    }
                }
                
                if (field.IsNumber) {
                    if (field.Precision == null || field.Precision.Value == 0)
                    {
                        row[key] = int.Parse(row[key].ToString());
                    }
                    else 
                    {
                        row[key] = float.Parse(row[key].ToString());
                    }
                
                }
                
                if(field.IsDate && row[key].GetType() !=typeof(DateTime) )
                {
                    DateTime d;
                    if (DateTime.TryParse(row[key].ToString().ToString(), out d))
                    {
                        row[key] = d;
                    }
                    else
                    {
                        Console.WriteLine($"Can't Parse '{row[key]}' as DateTime");
                        row.Remove(key);
                    }
                }
                
            }


        }

        private static (bool,Record?) CreateOrUpdateAccount(DataRow row ) 
        { 
            string email = row["Email Address"]==DBNull.Value? null : row["Email Address"].ToString();
            string phone= row["Mobile number"] == DBNull.Value ? null : row["Mobile number"].ToString();
            string tanent_name = row["Tenant name"].ToString();

            if (!String.IsNullOrEmpty(email))
            {
                var res=_client.Query(1, new string[] { }, $"{Consts.ACCOUNT_FIELD_EMAIL} = '{email}'");
                if (res.Count() > 1)
                {
                    Console.WriteLine($"More than One Account with the same email: {email}. returning first");
                    return (false, res.First());
                }
                else if (res.Count() == 1)
                {
                    return (false, res.First());
                }
                else 
                {
                    Console.WriteLine($"email {email} Not Found");
                }
            }

            if (!String.IsNullOrEmpty(phone))
            {
                var res = _client.Query(1, new string[] { }, $"{Consts.ACCOUNT_FIELD_MOBILE} = '{phone}'");
                if (res.Count() > 1)
                {
                    Console.WriteLine($"More than One Account with the same phone: {phone}. returning first");
                    return (false, res.First());
                }
                else if (res.Count() == 1)
                {
                    return (false, res.First());
                }
                else
                {
                    Console.WriteLine($"phone {phone} Not Found");
                }
            }

            var account = _client.CreateNew(1);
            account[Consts.ACCOUNT_FIELD_ACCOUNT_NAME] = tanent_name;
            if (!String.IsNullOrEmpty(phone))
            {
                account[Consts.ACCOUNT_FIELD_MOBILE] =phone;
            }
            if(!String.IsNullOrEmpty(email))
            {
                account[Consts.ACCOUNT_FIELD_EMAIL] = email;
            }

            var result= _client.InsertRecord(account);
            return (result.success, result.created);
        }


        private static EnergyReportData? ParseEnergyReport(string path) 
        {
            if (String.IsNullOrEmpty(ENERGY_REPORT_PARSER_URL))
                return null;
            
            var response=UploadFile(path,ENERGY_REPORT_PARSER_URL).Result;
            if(response.IsSuccessStatusCode)
            {
                var x = response.Content.ReadAsStringAsync().Result;
                try
                {
                    EnergyReportData t = JsonSerializer.Deserialize<EnergyReportData>(x, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    return t;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed To Parse Server Response. Response :{x} Error Message: {ex.Message}");                    
                }                
            }
            return null;
        }

        private static async Task<HttpResponseMessage> UploadFile(string path,string url)
        {
            var client = new HttpClient
            {
                BaseAddress = new(url) 
            };

            await using var stream = System.IO.File.OpenRead("path");
            using var request = new HttpRequestMessage(HttpMethod.Post, "file");
            using var content = new MultipartFormDataContent
                {
                    { new StreamContent(stream), "file", Path.GetFileName(path) }
                };

            request.Content = content;
            return await client.SendAsync(request);
        }

        public static DataSet LoadExcelFile(string filePath)
        {
            DataSet dataSet = new DataSet();

            using (var excelPackage = new ExcelPackage(new FileInfo(filePath)))
            {
                foreach (var worksheet in excelPackage.Workbook.Worksheets)
                {                                        
                    DataTable dataTable = LoadWorksheet(worksheet);
                    dataTable.TableName = worksheet.Name;
                    dataSet.Tables.Add(dataTable);
                }
            }

            return dataSet;
        }

        private static DataTable LoadWorksheet(ExcelWorksheet worksheet, bool hasHeaders = true)
        {
            if (worksheet.Cells.Count() == 0) {

                return new DataTable() { 
                    TableName= worksheet.Name                
                };
            }
            
            DataTable tbl = new DataTable();
            foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
            {
                tbl.Columns.Add(hasHeaders ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
            }
            var startRow = hasHeaders ? 2 : 1;
            for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
            {
                var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                DataRow row = tbl.Rows.Add();
                foreach (var cell in wsRow)
                {
                    row[cell.Start.Column - 1] = cell.Text;
                }
            }
            return tbl;
        }


        private static Dictionary<string,Guid> GetValuesForObject(int typecode,string keyfield)
        {
            var result=new Dictionary<string,Guid>();
            var records=_client.GetAllRecords(typecode);
            records.ToList().ForEach(record => {
                if (record[keyfield] == null || String.IsNullOrEmpty(record[keyfield].ToString()))
                    return;
                if (!result.ContainsKey(record[keyfield].ToString()))
                {                                                
                    result.Add(record[keyfield].ToString(), record.Id);
                }                                    
            });
            return result;
        }
    }



    public class Arguments
    {
        public Guid? PropertyID { get; set; } = null;
        public string DirectoryPath { get; set; }
        public string DataFilePath { get; set; }

        public static Arguments Parse(string[] args) {
            Arguments result = new Arguments() { };

            if (args.Length > 0) { 
                result.DirectoryPath= args[0];
            }

            if (args.Length > 1)
            {
                Guid g;
                if(Guid.TryParse(args[1],out g))
                    result.PropertyID=g;
            }

            result.DataFilePath= Path.Combine(result.DirectoryPath, "data.xlsx");

            return result;
        }
        public (bool, string?) IsValid() 
        {
            if (String.IsNullOrEmpty(DirectoryPath)) return (false, "Missing Folder Path");
            if(!Directory.Exists(DirectoryPath)) return (false, $"{DirectoryPath} directory does not exist");        
            if (!File.Exists(DataFilePath)) {
                return (false, "Missing Data File");
            }
            
            return (true, null);
        } 
    }

    public class EnergyReportData 
    {
        public string Name { get; set; }
    }
 }