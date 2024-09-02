using Locwise.Fireberry.Client;
using MSIMAG.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using PhoneNumbers;


namespace MSIMAG.FunctionApp.AirCall
{
    public class AirCallClient
    {
        private readonly string API_KEY =String.Empty;
        public const string AIRCALL_API_ROOTURL = "https://api.aircall.io";

        public AirCallClient(string apikey) 
        {
            API_KEY= apikey;
        }  
       
        public (bool,int) CreateSharedContact(ContactModel contact)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(AIRCALL_API_ROOTURL);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", API_KEY);
                var httpResponseMessage = httpClient.PostAsJsonAsync("v1/contacts", contact);
                var result = httpResponseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    var x = result.Content.ReadAsStringAsync().Result;

                }
            }
            
            return (true, 0);
        }

        public bool UpdateSharedContact(ContactModel contact) 
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(AIRCALL_API_ROOTURL);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", API_KEY);
                var httpResponseMessage = httpClient.PostAsJsonAsync($"v1/contacts/{contact.id}", contact);
                var result = httpResponseMessage.Result;
                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public static class AirCallExtensions {

        public static ContactModel ToContactModel(this Record account)         
        {
            if (account.TypeIdentifier.TypeID != 1)
            {
                throw new InvalidOperationException();
            }

            string accountname = account.Get("accountname", String.Empty);
            string phone = account.Get("telephone2", String.Empty);
            string mobile = account.Get("telephone1", String.Empty);
            string email = account.Get("emailaddress1", String.Empty);
            int aircallid = account.Get<int>("pcfaccountaircallid", 0);
            string firstname = account.Get("firstname", String.Empty);
            string lastname = account.Get("lastname", String.Empty);

            mobile = TryParsePhoneNumber(mobile);
            phone=TryParsePhoneNumber(phone);




            ContactModel result=new ContactModel();
            if(aircallid!=0) result.id= aircallid;


            if (!String.IsNullOrEmpty(firstname)) { 
                result.first_name=firstname;
            }

            if (!String.IsNullOrEmpty(lastname))
            {
                result.last_name = lastname;
            }

            if (String.IsNullOrEmpty(result.first_name) && String.IsNullOrEmpty(result.last_name)) {
                if (String.IsNullOrEmpty(accountname)) return null;
                result.first_name=accountname;            
            }

            if (!String.IsNullOrEmpty(email)) {
                List<EmailModel> emails = new List<EmailModel>();
                emails.Add(new EmailModel() { 
                     label="Primary",
                     value=email                
                }); 
                result.emails=emails.ToArray();
            }

            List<PhoneNumberModel> numbers = new List<PhoneNumberModel>();

            if (!String.IsNullOrEmpty(mobile)) {
                numbers.Add(new PhoneNumberModel()
                {
                    label = "Mobile",
                    value = mobile
                });            
            }

            if (!String.IsNullOrEmpty(phone))
            {
                numbers.Add(new PhoneNumberModel()
                {
                    label = "Phone",
                    value = phone
                });
            }

            if (numbers.Count() == 0) return null;             
            
            result.phone_numbers = numbers.ToArray();
            
            return result;
                
        }

        private static string TryParsePhoneNumber(string phone) 
        {

            if (String.IsNullOrEmpty(phone))
                return null;

            try
            {

           
            var phoneNumber = PhoneNumber.Parse(phone,CountryInfo.Germany);
            if (phoneNumber == null)
                return null;
            if (phoneNumber.HasNationalDestinationCode)
            {
                return phoneNumber.ToString("RFC3966");
            }
            else 
            { 
                return phoneNumber.ToString("N");
            }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return null;
            }
        }

    }
}
