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



        [OperationContract]
        [WebInvoke(UriTemplate = "uploadImage", Method = "POST", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare)]
        Stream uploadImage(ImageUpload imgData);


    }

    [DataContract]
    public class GetSecurityKey
    {
        [DataMember]
        public string SecurityKey { get; set; }
    }

}
