using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIMAG.Core
{
    public static class Consts
    {
        public static string API_KEY = "55ad5b0a-0ab9-48a4-af69-a12678110f97";
        public static int PROPERTY_OBJECT_ID = 1003;
        public static int UNIT_OBJECT_ID = 1004;
        public static int MEASURE_OBJECT_ID = 1022;
        public static int RENTAL_AGREEMENT_OBJECT_ID = 1005;
        public static int CITY_OBJECT_ID = 1023;

        #region Propery Fields
       
        public const string PROPERTY_FIELD_YEAR_BUILT = "pcfpcfyearbuilt";
        public const string PROPERTY_FIELD_TOTAL_NUMBER_OF_UNITS = "pcfpcftotalnumberofunits";
        public const string PROPERTY_FIELD_STREET = "pcfstreet";
        public const string PROPERTY_FIELD_PROPERTY_PHOTO = "pcfpropertyphoto";
        public const string PROPERTY_FIELD_PROPERTY_NAME = "name";
        public const string PROPERTY_FIELD_PROPERTY_MANAGER = "pcfpcfpropertymanager";
        public const string PROPERTY_FIELD_POSTAL_CODE = "pcfpcfpostalcode";
        public const string PROPERTY_FIELD_PLUMBING_SYSTEM_STATUS = "pcfplumbingsystemstatus";
        public const string PROPERTY_FIELD_PBI_EMBEDDED = "pcfunitspbiembedded";
        public const string PROPERTY_FIELD_PARKING_AVAILABILITY = "pcfparkingavailability";
        public const string PROPERTY_FIELD_OWNER = "ownerid";
        public const string PROPERTY_FIELD_NUMBER_OF_FLOORS = "pcfpcfnumberoffloors";
        public const string PROPERTY_FIELD_MODIFIED_ON = "modifiedon";
        public const string PROPERTY_FIELD_MODIFIED_BY = "modifiedby";
        public const string PROPERTY_FIELD_LONGITUDE = "pcfpropertylongitude";
        public const string PROPERTY_FIELD_LATITUDE = "pcfpropertylatitude";
        public const string PROPERTY_FIELD_HOUSE_NUMBER = "pcfhousenumber";
        public const string PROPERTY_FIELD_HEATING_SYSTEM = "pcfpcfheatingsystem";
        public const string PROPERTY_FIELD_FULL_ADDRESS = "pcfpropertyfulladdress";
        public const string PROPERTY_FIELD_FIRE_SAFETY_EQUIPMENT = "pcfpcffiresafetyequipment";
        public const string PROPERTY_FIELD_EMERGENCY_EXITS = "pcfemergencyexits";
        public const string PROPERTY_FIELD_ELEVATOR_STATUS = "pcfpcfelevatorstatus";
        public const string PROPERTY_FIELD_ELECTRICAL_SYSTEM_STATUS = "pcfpcfelectricalsystemstatus";
        public const string PROPERTY_FIELD_DUMMY = "pcfdummy";
        public const string PROPERTY_FIELD_DESIGNATION = "pcfpropertyDesignation";
        public const string PROPERTY_FIELD_DATE_OF_NEXT_INSPECTION = "pcfpcfdateofnextinspection";
        public const string PROPERTY_FIELD_DATE_OF_LAST_INSPECTION = "pcfpcfdateoflastinspection";
        public const string PROPERTY_FIELD_CREATED_ON = "createdon";
        public const string PROPERTY_FIELD_CREATED_BY = "createdby";
        public const string PROPERTY_FIELD_COMPANY = "pcfporpertycompany";
        public const string PROPERTY_FIELD_CITY_DELETE = "pcfpropertycity";
        public const string PROPERTY_FIELD_CITY = "pcfpropertycitylookup";
        public const string PROPERTY_FIELD_BUILDING_TYPE = "pcfbuildingtype";
        public const string PROPERTY_FIELD_BUILDING_PHOTO = "pcfbuildingphoto";
        public const string PROPERTY_FIELD_BUILDING_AREA = "pcfpcfbuildingarea";
        public const string PROPERTY_FIELD_ACCESSIBILITY_FEATURES = "pcfpcfaccessibilityfeatures";
        public const string PROPERTY_FIELD_OLD_ID = "pcfpropertyoldid";
        public const string PROPERTY_FIELD_ID = "pcfpropertyid";
        public const string PROPERTY_FIELD_NO_OF_UNITS = "pcfpcfnumberofunits";
        public const string PROPERTY_FIELD_PREFIX = "pcfpropertyprefix";
        public const string PROPERTY_FIELD_TOTAL_PROPERTY_SIZE = "pcfpcfbuildingarea";
        public const string PROPERTY_FIELD_CITY_LOOKUP = "pcfpropertycitylookup";

        #endregion

        #region RENTAL_UNIT


        public const int RENTAL_UNIT_OBJECT_ID = 1004;

    
        public const string RENTAL_UNIT_FIELD_WARM_RENT_SQM_NUMBER = "pcfrentalwsqm";
        public const string RENTAL_UNIT_FIELD_WARM_SQM_NUMBER = "pcffuncwarmpersqm";
        public const string RENTAL_UNIT_FIELD_UNIT_TYPE_PICKLIST = "pcfunittype";
        public const string RENTAL_UNIT_FIELD_UNIT_SIZE_NUMBER = "pcfunitsize";
        public const string RENTAL_UNIT_FIELD_UNIT_SERIAL_NUMBER = "pcfunitserial";
        public const string RENTAL_UNIT_FIELD_UNIT_NUMBER_TEXT = "pcfunitnumber";
        public const string RENTAL_UNIT_FIELD_UNIT_NAME_TEXT = "name";
        public const string RENTAL_UNIT_FIELD_TYPE_OF_HEATING_PICKLIST = "pcfheatingheating";
        public const string RENTAL_UNIT_FIELD_TOTAL_WARM_NUMBER = "pcftotalwarmcosts";
        public const string RENTAL_UNIT_FIELD_TOTAL_KALT_NUMBER = "pcftotalcoldrent";
        public const string RENTAL_UNIT_FIELD_TOTAL_HK_NUMBER = "pcftotalheatingcosts";
        public const string RENTAL_UNIT_FIELD_TOTAL_BK_NUMBER = "pcftotaloperatingcosts";
        public const string RENTAL_UNIT_FIELD_STREET_TEXT = "pcfunitstreet";
        public const string RENTAL_UNIT_FIELD_STATUS_UNIT_PICKLIST = "pcfplstatusunit";
        public const string RENTAL_UNIT_FIELD_ROOMS_NUMBER = "pcfrooms";
        public const string RENTAL_UNIT_FIELD_RENT_INDEX_NUMBER = "pcfrentalindex";
        public const string RENTAL_UNIT_FIELD_RENOVATION_LOOKUP = "pcfludefectsrenovation";
        public const string RENTAL_UNIT_FIELD_PROPERTY_LOOKUP = "pcfunitproperty";
        public const string RENTAL_UNIT_FIELD_POSTAL_CODE_TEXT = "pcfunitcode";
        public const string RENTAL_UNIT_FIELD_OWNER_LOOKUP = "ownerid";
        public const string RENTAL_UNIT_FIELD_NUMBER_OF_BALCONIES_NUMBER = "pcfbalcony";
        public const string RENTAL_UNIT_FIELD_MODIFIED_ON_DATETIME = "modifiedon";
        public const string RENTAL_UNIT_FIELD_MODIFIED_BY_LOOKUP = "modifiedby";
        public const string RENTAL_UNIT_FIELD_LOCATION_IN_FLOOR_PICKLIST = "pcfunitlocation";
        public const string RENTAL_UNIT_FIELD_LAST_RENT_PROPOSAL_DO_NOT_DELETE_LOOKUP = "pcflurentagreement";
        public const string RENTAL_UNIT_FIELD_KALT_SQM_NUMBER = "pcfrentalsqm";
        public const string RENTAL_UNIT_FIELD_HOUSE_NUMBER_TEXT = "pcfhousenumber";
        public const string RENTAL_UNIT_FIELD_HK_SQM_NUMBER = "pcfrentalhsqm";
        public const string RENTAL_UNIT_FIELD_GARDEN_SIZE_NUMBER = "pcfgardensize";
        public const string RENTAL_UNIT_FIELD_FLOOR_PLANS_RICHTEXT = "pcffloorplans";
        public const string RENTAL_UNIT_FIELD_FLOOR_PICKLIST = "pcfunitfloor";
        public const string RENTAL_UNIT_FIELD_DUMMY_PICKLIST = "pcfsystemfield100";
        public const string RENTAL_UNIT_FIELD_DATE_OF_NEXT_ELECTRICAL_INSPECTION_DATE = "pcfeninspection";
        public const string RENTAL_UNIT_FIELD_DATE_OF_NEXT_CHIMNEY_SWEEP_INSPECTION_DATE = "pcfheatinginspection";
        public const string RENTAL_UNIT_FIELD_DATE_OF_LAST_ELECTRICAL_INSPECTION_DATE = "pcfelinspection";
        public const string RENTAL_UNIT_FIELD_DATE_OF_CYLINDER_CHANGE_DATE = "pcfcylinderchange";
        public const string RENTAL_UNIT_FIELD_CYLINDER_TYPE_PICKLIST = "pcfcylindertype";
        public const string RENTAL_UNIT_FIELD_CYLINDER_CHANGED_BY_PICKLIST = "pcfcylinderby";
        public const string RENTAL_UNIT_FIELD_CREATED_ON_DATETIME = "createdon";
        public const string RENTAL_UNIT_FIELD_CREATED_BY_LOOKUP = "createdby";
        public const string RENTAL_UNIT_FIELD_CITY_TEXT = "pcfunitcity";
        public const string RENTAL_UNIT_FIELD_CALCULATED_UNIT_SIZE_NUMBER = "pcfnumcalcunitsize";
        public const string RENTAL_UNIT_FIELD_BK_SQM_NUMBER = "pcfrentalssqm";
        public const string RENTAL_UNIT_FIELD_BALCONY_SIZE_NUMBER = "pcfbalconysize";
        public const string RENTAL_UNIT_FIELD_OLD_ID_TEXT = "pcfunitoldid";
        public const string RENTAL_UNIT_FIELD_ID_TEXT = "pcfunitid";
        public const string RENTAL_UNIT_FIELD_ID_TEXTAREA = "pcfsystemfield101";
        public const string RENTAL_UNIT_FIELD_RESERVATIONS_NUMBER = "pcfaggreservations";
        public const string RENTAL_UNIT_FIELD_RENT_OFFERS_NUMBER = "pcfrentoffers";
        public const string RENTAL_UNIT_FIELD_ACTIVE_LEGAL_CASES_NUMBER = "pcfactivelegalcases";


        #endregion

        #region Company

        public const string COMPANY_FIELD_PBI_RICHTEXT = "pcfsystemfield100";
        public const string COMPANY_FIELD_OWNER_LOOKUP = "ownerid";
        public const string COMPANY_FIELD_MODIFIED_ON_DATETIME = "modifiedon";
        public const string COMPANY_FIELD_MODIFIED_BY_LOOKUP = "modifiedby";
        public const string COMPANY_FIELD_CREATED_ON_DATETIME = "createdon";
        public const string COMPANY_FIELD_CREATED_BY_LOOKUP = "createdby";
        public const string COMPANY_FIELD_COMPANY_NUMBER_TEXT = "pcfcompanynumber";
        public const string COMPANY_FIELD_COMPANY_NAME_TEXT = "name";
        public const string COMPANY_FIELD_OLD_ID_TEXT = "pcfcompanyoldid";
        public const string COMPANY_FIELD_ID_TEXT = "pcfcompanyid";
        public const string COMPANY_FIELD_NO_OF_BANK_ACCOUNTS_NUMBER = "pcfbankaccounts";
        public const string COMPANY_FIELD_ACTIVE_PROPERTIES_NUMBER = "pcfcompanynumberoproperties";

        #endregion

        #region Account

        public const string ACCOUNT_FIELD_WEBSITE = "websiteurl";
        public const string ACCOUNT_FIELD_SUPPLIER_RECOMMENDED_BY = "pcfsuprecommendedby";
        public const string ACCOUNT_FIELD_SUPPLIER_MAIN_SUPPORTED_CITY = "pcfsuppmaincity";
        public const string ACCOUNT_FIELD_STATUS = "statuscode";
        public const string ACCOUNT_FIELD_SIC_CODE = "siccode";
        public const string ACCOUNT_FIELD_SHIPPING_ZIP = "shippingzip";
        public const string ACCOUNT_FIELD_SHIPPING_STREET = "shippingstreet";
        public const string ACCOUNT_FIELD_SHIPPING_STATE = "shippingstate";
        public const string ACCOUNT_FIELD_SHIPPING_METHOD = "shippingmethodcode";
        public const string ACCOUNT_FIELD_SHIPPING_COUNTRY = "shippingcountry";
        public const string ACCOUNT_FIELD_SHIPPING_CITY = "shippingcity";
        public const string ACCOUNT_FIELD_SECONDARY_EMAIL = "emailaddress2";
        public const string ACCOUNT_FIELD_RATING = "accountratingcode";
        public const string ACCOUNT_FIELD_PRIMARY_CONTACT = "primarycontactid";
        public const string ACCOUNT_FIELD_PHONE = "telephone2";
        public const string ACCOUNT_FIELD_PARENT_ACCOUNT = "parentaccountid";
        public const string ACCOUNT_FIELD_OWNERSHIP = "businesstypecode";
        public const string ACCOUNT_FIELD_OTHER_PHONE = "telephone3";
        public const string ACCOUNT_FIELD_OTHER_EMAIL = "emailaddress3";
        public const string ACCOUNT_FIELD_NEXT_TASK_DATE = "nextactivitydate";
        public const string ACCOUNT_FIELD_NEXT_MEETING_DATE = "nextactiondate";
        public const string ACCOUNT_FIELD_MODIFIED_ON = "modifiedon";
        public const string ACCOUNT_FIELD_MODIFIED_BY = "modifiedby";
        public const string ACCOUNT_FIELD_MOBILE = "telephone1";
        public const string ACCOUNT_FIELD_MIDDLE_NAME = "middlename";
        public const string ACCOUNT_FIELD_MAIN_SUPPLIER_LOB = "pcfsupplierlob";
        public const string ACCOUNT_FIELD_LEAD_SOURCE = "originatingleadcode";
        public const string ACCOUNT_FIELD_LAST_NAME = "lastname";
        public const string ACCOUNT_FIELD_IS_TENANT = "pcfsystemfield102";
        public const string ACCOUNT_FIELD_IS_SUPPLIER = "pcfsystemfield103";
        public const string ACCOUNT_FIELD_IS_BUYER = "pcfsystemfield104";
        public const string ACCOUNT_FIELD_INTERESTED_IN_UNIT_TYPE = "pcfnterestedinunittype";
        public const string ACCOUNT_FIELD_INTERESTED_IN_FLOOR = "pcfinterestedinfloor";
        public const string ACCOUNT_FIELD_INTERESTED_IN_CITY = "pcfinterestedcity";
        public const string ACCOUNT_FIELD_INDUSTRY = "industrycode";
        public const string ACCOUNT_FIELD_GENDER = "pcfgender";
        public const string ACCOUNT_FIELD_FIRST_NAME = "firstname";
        public const string ACCOUNT_FIELD_FAX = "pcfsystemfield3";
        public const string ACCOUNT_FIELD_EMPLOYEES = "numberofemployees";
        public const string ACCOUNT_FIELD_EMAIL_OPT_OUT = "isvalidforemailcode";
        public const string ACCOUNT_FIELD_EMAIL = "emailaddress1";
        public const string ACCOUNT_FIELD_EIN_VAT_NUMBER = "pcfsystemfield5";
        public const string ACCOUNT_FIELD_DUMMY = "pcfsystemfield105";
        public const string ACCOUNT_FIELD_DESCRIPTION = "description";
        public const string ACCOUNT_FIELD_CREATED_ON = "createdon";
        public const string ACCOUNT_FIELD_CREATED_BY = "createdby";
        public const string ACCOUNT_FIELD_COMPANY_NAME_SUPPLIER = "pcfssupplierfirmname";
        public const string ACCOUNT_FIELD_BIRTHDATE = "birthdaydate";
        public const string ACCOUNT_FIELD_BILLING_ZIP = "billingzipcode";
        public const string ACCOUNT_FIELD_BILLING_STREET = "billingstreet";
        public const string ACCOUNT_FIELD_BILLING_STATE = "billingstate";
        public const string ACCOUNT_FIELD_BILLING_COUNTRY = "billingcountry";
        public const string ACCOUNT_FIELD_BILLING_CITY = "billingcity";
        public const string ACCOUNT_FIELD_BALKON_PREFERENCES = "pcfInterestedInBalkon";
        public const string ACCOUNT_FIELD_ANNUAL_REVENUE = "revenue";
        public const string ACCOUNT_FIELD_ACCOUNT_TYPE = "accounttypecode";
        public const string ACCOUNT_FIELD_ACCOUNT_OWNER = "ownerid";
        public const string ACCOUNT_FIELD_ACCOUNT_NUMBER = "accountnumber";
        public const string ACCOUNT_FIELD_ACCOUNT_NAME = "accountname";
        public const string ACCOUNT_FIELD_ACCOUNT_AGE_DAYS = "accountageindays";
        public const string ACCOUNT_FIELD_OLD_ID = "pcfmieteroldid";
        public const string ACCOUNT_FIELD_ID = "pcfaccountid";
        public const string ACCOUNT_FIELD_TOTALDEBIT = "pcfaccounttotaldebit";
        public const string ACCOUNT_FIELD_TOTALCREDIT = "pcftotalcredit";
        public const string ACCOUNT_FIELD_TOTAL_INCOME = "pcfTotalIncome";
        public const string ACCOUNT_FIELD_POTENTIAL_INCOME = "pcfPotentialIncomeFromDeals";
        public const string ACCOUNT_FIELD_BALANCE = "pcfsystemfield100";
        public const string ACCOUNT_FIELD_NOT_RELEVANT = "pcfstatusnotrelevant";
        public const string ACCOUNT_FIELD_IN_TOTAL = "pcfstatusintotal";
        public const string ACCOUNT_FIELD_IN_RESERVATION = "pcfstatusinreservation";
        public const string ACCOUNT_FIELD_IN_PROGRESS = "pcfstatusinprogress";
        public const string ACCOUNT_FIELD_COMPLETED = "pcfstatuscompleted";
        public const string ACCOUNT_FIELD_CLOSED_FAILED = "pcfclosedfailed";
        public const string ACCOUNT_FIELD_ACTIVE = "pcfsystemfield101";

        #endregion

        #region Rental Agreement
      

        public const string RENTAL_AGREEMENT_FIELD_WARM_RENT_SQM_NUMBER = "pcfwarmrentsqm";
        public const string RENTAL_AGREEMENT_FIELD_WARM_RENT_NUMBER = "pcfswarmrent";
        public const string RENTAL_AGREEMENT_FIELD_WAITING_FOR_DOCUMENTS_DATE = "pcfwaitingfordocumentsdate";
        public const string RENTAL_AGREEMENT_FIELD_VIEWING_DATE_DATETIME = "pcfviewingdate";
        public const string RENTAL_AGREEMENT_FIELD_VAT_NUMBER = "pcfvat";
        public const string RENTAL_AGREEMENT_FIELD_UPDATE_UNIT_CHILD_PICKLIST = "pcfupdateunitchild";
        public const string RENTAL_AGREEMENT_FIELD_THERE_IS_VAT_TEXT = "pcfthereisvat";
        public const string RENTAL_AGREEMENT_FIELD_THE_RENTAL_AGREEMENT_BEGINS_ON_DATE = "pcfrentalagreementbegins";
        public const string RENTAL_AGREEMENT_FIELD_TENANT_NAME_3_TEXT = "pcftenantname3";
        public const string RENTAL_AGREEMENT_FIELD_TENANT_NAME_2_TEXT = "pcftenantname2";

        public const string RENTAL_AGREEMENT_FIELD_RENATAL_UNIT_LOOKUP = "pcfunits";
        public const string RENTAL_AGREEMENT_FIELD_MIETER_LOOKUP = "pcfaccountname";

        //Contract termination date 
        public const string RENTAL_AGREEMENT_FIELD_CONTRACT_TERMINATION_DATE = "pcfcontractterminationdate";

        #endregion

        #region Measure Fields

        public const string MEASURE_FIELD_UNIT_LOOKUP = "pcflurentalunits";
        public const string MEASURE_FIELD_POSITION_PICKLIST = "pcfplposition";
        public const string MEASURE_FIELD_PROTOCOL_PICKLIST = "pcfplprotokol";
        public const string MEASURE_FIELD_COUNTER_TYPE_PICKLIST = "pcfplmetertype";
        public const string MEASURE_FIELD_METET_NUMBER_TEXT = "pcfmeternumber";
        public const string MEASURE_FIELD_METET_READING_NUMBER = "pcfmeterreading";
        public const string MEASURE_FIELD_DATE_DATE = "pcfdatecounter";
        public const string MEASURE_FIELD_MAIN_METER_PICKLIST = "pcfplmainmeter";
        public const string MEASURE_FIELD_RENTAL_AGREEMENT_LOOKUP = "pcflurentalagreement";
        #endregion

        #region Rental Agreement Fields

        public const string RENTAL_AGREEMENT_SYSTEM_NAME = "customobject1005";
        public const string RENTAL_AGREEMENT_FIELD_ID = "customobject1005id";
        public const string RENTAL_AGREEMENT_FIELD_UNIT_LOOKUP = "pcfunits";
        public const string RENTAL_AGREEMENT_FIELD_NAME_TEXT = "name";
        public const string RENTAL_AGREEMENT_FIELD_STATUS_REASON_PICKLIST = "pcfstatusreason";
        public const string RENTAL_AGREEMENT_FIELD_AGREEMENT_STATUS_PICKLIST = "pcfsystemfield100";
        public const string RENTAL_AGREEMENT_FIELD_STATUS_OFFER_A_PICKLIST = "pcfstatusoffera";

        #endregion

        #region City Fields

        public const string CITY_FIELD_NAME_TEXT = "name";
        public const string CITY_FIELD_LONGITUDE_NUMBER = "pcfcitylongitude";
        public const string CITY_FIELD_LATITUDE_NUMBER = "pcfcityLatitude";

        #endregion

        #region Document Type Fields
        public const string DOCUMENT_TYPE_SYSTEM_NAME = "customobject1033";
        public const string DOCUMENT_TYPE_NAME = "name";
        public const string DOCUMENT_TYPE_STATUS = "pcfstatusmapping";
        public const string DOCUMENT_TYPE_SOURCE_COL1 = "pcfsourcecol1";
        public const string DOCUMENT_TYPE_SOURCE_COL2 = "pcfsourcecol2";
        public const string DOCUMENT_TYPE_SOURCE_COL3 = "pcfsourcecol3";
        public const string DOCUMENT_TYPE_SOURCE_COL4 = "pcfsourcecol4";
        public const string DOCUMENT_TYPE_SOURCE_COL5 = "pcfsourcecol5";
        public const string DOCUMENT_TYPE_SOURCE_COL6 = "pcfsourcecol6";
        public const string DOCUMENT_TYPE_DESTINATION_COL1 = "pcfdestinationcol1";
        public const string DOCUMENT_TYPE_DESTINATION_COL2 = "pcfdestinationcol2";
        public const string DOCUMENT_TYPE_DESTINATION_COL3 = "pcfdestinationcol3";
        public const string DOCUMENT_TYPE_DESTINATION_COL4 = "pcfdestinationcol4";
        public const string DOCUMENT_TYPE_DESTINATION_COL5 = "pcfdestinationcol5";
        public const string DOCUMENT_TYPE_DESTINATION_COL6 = "pcfdestinationcol6";
        public const string DOCUMENT_TYPE_DESTINATION_OBJECT_NAME = "pcfdestinationobjectname";
        public const string DOCUMENT_TYPE_DESTINATION_OBJECT_TYPE = "pcfdestinationobjecttype";
        public const string DOCUMENT_TYPE_RELATED_TO_ID = "pcfrelatedtoid";

        #endregion

    }
}
