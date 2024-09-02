using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MSIMAG.FunctionApp.AirCall
{
    public class ContactModel
    {
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public int? id { get; set; }
        public string first_name { get; set; } = String.Empty;
        public string last_name { get; set; }= String.Empty;
        public string company_name { get; set; } = String.Empty;
        public bool is_shared { get; set; } = true;
        public PhoneNumberModel[] phone_numbers { get; set; } = new PhoneNumberModel[] { };
        public EmailModel[] emails { get; set; } = new EmailModel[] { };

        [JsonIgnore]
        public bool IsNew
        {
            get {
                return (id == null || id <= 0);            
            }
        }

    }

    public class EmailModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }

    public class PhoneNumberModel
    {
        public int? id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
    }
}
