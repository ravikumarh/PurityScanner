using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PL;
using System.IO;

namespace PurityScannerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [WebInvoke(UriTemplate = "APIGet", Method = "POST")]
        string APIGet(GetSecurityKey param);

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        string GetData();
        [OperationContract]
        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        string Test();

        [OperationContract]
        [WebInvoke(UriTemplate = "getAppMetadata", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream getAppMetadata(GetSecurityKey SecurityKey);




        [OperationContract]
        [WebInvoke(UriTemplate = "getAllProductsByIDs", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream getAllProductsByIDs(ProductsByIDsRequest productRequestData);


        [OperationContract]
        [WebInvoke(UriTemplate = "getAllProductsMetaData", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream getAllProductsMetaData(ManifestoRequest manifestoData);

        [OperationContract]
        [WebInvoke(UriTemplate = "getProductDetailsByImageKey", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData);

        [OperationContract]
        [WebInvoke(UriTemplate = "GetManifesto", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream GetManifesto(ManifestoRequest manifestoData);

    }

    [DataContract]
    public class GetSecurityKey
    {
        [DataMember]
        public string SecurityKey { get; set; }
    }



    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    //[DataContract]
    //public class CountryMaster
    //{
    //    int country_id = 0;
    //    string country_name = "";
    //    int country_default_language_id = 0;
    //    bool is_active = true;

    //    [DataMember]
    //    public bool IsActive
    //    {
    //        get { return is_active; }
    //        set { is_active = value; }
    //    }

    //    [DataMember]
    //    public string CountryName
    //    {
    //        get { return country_name; }
    //        set { country_name = value; }
    //    }

    //    [DataMember]
    //    public int CountryId
    //    {
    //        get { return country_id; }
    //        set { country_id = value; }
    //    }

    //    [DataMember]
    //    public int CountryDefaultLanguageId
    //    {
    //        get { return country_default_language_id; }
    //        set { country_default_language_id = value; }
    //    }
    //}

    //[DataContract]
    //public class LanguageMaster
    //{

    //    int language_id = 0;
    //    string language_name = "";
    //    bool is_active = true;


    //    [DataMember]
    //    public int LanguageId
    //    {
    //        get { return language_id; }
    //        set{language_id=value;}
    //    }

    //    [DataMember]
    //    public string LanguageName
    //    {
    //        get { return language_name; }
    //        set { language_name = value; }
    //    }

    //    [DataMember]
    //    public bool IsActive
    //    {
    //        get { return is_active; }
    //        set { is_active = value; }
    //    }


    //}

    //[DataContract]
    //public class AppMetaData
    //{ 
    //   int app_metadata_id=0;
    //   string app_metadata_key = "";
    //   string app_metadata_value = "";
    //   DateTime modified_date_time;

    //   [DataMember]
    //   public int AppMetadataId
    //   {
    //       get { return app_metadata_id; }
    //       set { app_metadata_id = value; }
    //   }

    //   [DataMember]
    //   public string AppMetadataKey
    //   {
    //       get { return app_metadata_key; }
    //       set { app_metadata_key = value; }
    //   }

    //    [DataMember]
    //   public string Value
    //   {
    //       get { return app_metadata_value; }
    //       set { app_metadata_value = value; }
    //   }

    //    [DataMember]
    //    public DateTime ModifiedDateTime
    //    {
    //        get { return modified_date_time; }
    //        set { modified_date_time = value; }
    //      }


    //}

    //[DataContract]
    //public class AttributeValue
    //{
    //    string title = "";
    //    string values = "";

    //    [DataMember]
    //    public string Title
    //    {
    //        get { return title; }
    //        set { title = value; }
    //    }

    //    [DataMember]
    //    public string Values
    //    {
    //        get { return values; }
    //        set { values = value; }
    //    }
    //}


    //[DataContract]
    //public class AttributeManifesto
    //{
    //    string title;
    //    string manifestoInformation;

    //    [DataMember]
    //    public string Title
    //    {
    //        get { return title; }
    //        set { title = value; }
    //    }

    //    [DataMember]
    //    public string ManifestoInformation
    //    {
    //        get { return manifestoInformation; }
    //        set { manifestoInformation = value; }
    //    }

    //}

    //[DataContract]
    //public class Product
    //{
    //    string productName;
    //    List<AttributeValue> attributeValues;
    //    List<AttributeManifesto> attributeManifestos;

    //    [DataMember]
    //    public string ProductName
    //    {
    //        get { return productName; }
    //        set { productName = value; }
    //    }

    //    [DataMember]
    //    public List<AttributeManifesto> AttributeManifestoData
    //    {
    //        get { return attributeManifestos; }
    //        set { attributeManifestos = value; }
    //    }

    //    [DataMember]
    //    public List<AttributeValue> AttributeValueData
    //    {
    //        get { return attributeValues; }
    //        set { attributeValues = value; }
    //    }


    //}

    //[DataContract]
    //public class Manifesto
    //{
    //    string companyMissionStatement;
    //    List<AttributeManifesto> manifestoAttribute;

    //    [DataMember]
    //    public string CompanyMissionStatement
    //    {
    //        get { return companyMissionStatement; }
    //        set { companyMissionStatement = value; }
    //    }

    //    [DataMember]
    //    public List<AttributeManifesto> ManifestoAttributeData
    //    {
    //        get { return manifestoAttribute; }
    //        set { manifestoAttribute = value; }

    //    }

    //}

    //[DataContract]
    //public class AppMetaDataResponce
    //{ 
    //    int responseStatus;
    //    string responseCode;
    //    string responseMessage;
    //    List<CountryMaster> lstCountryMaster;
    //    List<LanguageMaster> lstLanguageMaster;
    //    List<AppMetaData> lstAppMetaData;

    //    [DataMember]
    //    public int ResponseStatus
    //    {
    //        get { return responseStatus; }
    //        set { responseStatus = value; }
    //    }

    //    [DataMember]
    //    public string ResponseCode
    //    {
    //        get { return responseCode; }
    //        set { responseCode = value; }
    //    }

    //    [DataMember]
    //    public string ResponseMessage
    //    {
    //        get { return responseMessage; }
    //        set { responseMessage = value; }
    //    }

    //    [DataMember]
    //    public List<CountryMaster> CountryMasterInfo
    //    {
    //        get { return lstCountryMaster; }
    //        set { lstCountryMaster = value; }
    //    }

    //    [DataMember]
    //    public List<LanguageMaster> LanguageMasterInfo
    //    {
    //        get { return lstLanguageMaster; }
    //        set { lstLanguageMaster = value; }
    //    }

    //    [DataMember]
    //    public List<AppMetaData> AppMetaDataInfo
    //    {
    //        get { return lstAppMetaData; }
    //        set { lstAppMetaData = value; }
    //    }
    //}

}
