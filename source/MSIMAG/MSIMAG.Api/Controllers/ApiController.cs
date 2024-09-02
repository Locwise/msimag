using Locwise.Fireberry.Client;
using Microsoft.AspNetCore.Mvc;
using MSIMAG.Api.Models;
using MSIMAG.Core.Data;
using System.Buffers.Text;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace MSIMAG.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ILogger<ApiController> _logger;
        private readonly IDataManager _dataManager;
        private readonly IMappingStore _mappingStore;

        public ApiController(ILogger<ApiController> logger, IDataManager dataManager, IMappingStore mappingStore)
        {
                _logger= logger;
                _dataManager = dataManager;
                _mappingStore= mappingStore;
        }


        [HttpGet("status")]
        public IActionResult Status() { 
        
           
            return new OkResult();

        }

        [HttpPost("filechanged")]
        public IActionResult HandleFileChange([FromBody] FileChangedRequestModel fileevent) 
        {

            var mappings = _mappingStore.GetMappings(fileevent.FileType);
            if(mappings.Count()==0)
            {
                var documentRecord=_dataManager.GetDocumentType(fileevent.FileType);
                documentRecord.CreateNote("There is a problem with one of the fields.\r\nSome of the fields are missing, therefore the mapping was not successful");
                documentRecord.CreateNote("There is a problem with one of the fields.\r\nSome of the fields are missing, therefore the mapping was not successful");
                return new OkResult();
                
            }
            var record=_dataManager.GetRecord(fileevent.FileID, 1015);
            var reletedToObjectNumber = mappings.First().TargetObjectType;

            var fieldNameVsLabel = _dataManager.GetObjectTypeByType(reletedToObjectNumber);

            var reletedTo =Guid.Parse(record.Get(mappings.First().RelatedTo, ""));
            var targetRecord = _dataManager.GetRecord(reletedTo, reletedToObjectNumber);
            var ifTargetIsNotNull = false;

            var str = "";
            try
            {
                foreach (var item in mappings)
                {
                    if (record.Keys.Contains(item.SourceField))
                    {
                        var field = fieldNameVsLabel.FirstOrDefault(f => f.FieldName == item.TargetField);
                        if(field == null)
                        {
                            ErorFunction(fileevent.FileType, $"There is a problem with {item.TargetField} field ");

                            return new OkResult();
                        }
                        var type = field.FieldType;
                        object sourceVal, targetVal;
                        if (type != "Number")
                        {
                            sourceVal = record.Get(item.SourceField, "");//chek if Source is not null?
                            targetVal = targetRecord.Get(item.TargetField, "");

                        }
                        else
                        {
                            sourceVal = record.Get(item.SourceField, 1);
                            targetVal = targetRecord.Get(item.TargetField, 1);
                        }

                        if (targetVal != "")
                        {
                            if (!sourceVal.Equals(targetVal))
                            {
                                ifTargetIsNotNull = true;
                            }

                        }
                        targetRecord[item.TargetField] = record[item.SourceField];

                        str += $"{field.Label} : {RemoveTimeFromDate(targetVal.ToString())}\r\n";
                    }
                    else
                    {
                        ErorFunction(fileevent.FileType, $"There is a problem with {item.SourceField} field ");
                        return new OkResult();
                    
                    }
                }
               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating fields");
                ErorFunction(fileevent.FileType, "There is a problem with one of the fields.\r\nSome of the fields are missing/wrong, therefore the mapping was not successful");
                return new OkResult();
            }
            if (!ifTargetIsNotNull)
            {
                _dataManager.UpdateRecord(targetRecord);
            }
            else
            {
                var Filecomment= $"Could not update fields in {mappings.First().TargetObjectName} as the fields are already populated.\r\n\r\nNeed to check the {mappings.First().TargetObjectName} : https://app.fireberry.com/app/record/{reletedToObjectNumber}/{reletedTo}\r\nThe validate suggested values from {fileevent.FileType} document are:\r\n{str}";
                var ReletedComment = $"Could not update fields in {mappings.First().TargetObjectName} as the fields are already populated. \r\nNeed to check the values , the suggested values from {fileevent.FileType} document are:\r\n{str}";
                _dataManager.saveNote(record, Filecomment);
                _dataManager.saveNote(targetRecord, ReletedComment);
            }

            return new OkResult();
        }


        private string RemoveTimeFromDate(string dateTimeString)
        {
            if (DateTime.TryParse(dateTimeString, out DateTime dateTime))
            {
                return dateTime.ToString("yyyy-MM-dd");
            }
            else
            {
                return dateTimeString;
            }
        }

        private void ErorFunction(string fileType,string comment)
        {
            var documentRecord = _dataManager.GetDocumentType(fileType);
            documentRecord["pcfstatusmapping"] = 3;
            var v = _dataManager.UpdateRecord(documentRecord);
            documentRecord.CreateNote(comment);
        }



    }
}
