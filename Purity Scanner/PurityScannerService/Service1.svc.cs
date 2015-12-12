using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using PL;
using BAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ServiceModel.Activation;
using System.IO;
namespace PurityScannerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.

    //  [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        PurityServices obj = new PurityServices();

        public string APIGet(GetSecurityKey param)
        {
            return "Security Value=" + Convert.ToString(param.SecurityKey);
        }
        public string GetData()
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\test.txt", true);
            //file.WriteLine("Hiii");
            //file.Close();
            return "hiii";
        }

        public string Test()
        {
            return "Testing Success";
        }

        public Stream getAppMetadata(GetSecurityKey SecurityKey)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\test.txt", true);
            //file.WriteLine("SecurityKey ID" + SecurityKey.SecurityKey + "");
            //file.Close();
            AppMetaDataResponce objResponce = obj.getAppMetadata(SecurityKey.SecurityKey);
            string str = JsonConvert.SerializeObject(objResponce);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return ms;
        }

        public Stream getAllProductsByIDs(ProductsByIDsRequest productRequestData)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\test.txt", true);
            //file.WriteLine("getAllProductsByIDs Language ID :-" + productRequestData.LanguageID + "");
            MemoryStream ms;
            if (productRequestData != null)
            {

              //  file.WriteLine("getAllProductsByIDs: -Language ID : " + productRequestData.LanguageID + " Country ID :" + productRequestData.CountryCode + ", SecurityKey :- " + productRequestData.SecurityKey + ", ProductIds Count :- " + productRequestData.ProductIDs.Count + " ComparingWithProductID :- " + productRequestData.ComparingWithProductID + ", UserLattitude :-" + productRequestData.UserLattitude + ", UserLongitude:-" + productRequestData.UserLongitude + "");
              //  file.Close();
                ProductDetailsResponce objResponce = obj.getAllProductsByIDs(productRequestData);

                string str = JsonConvert.SerializeObject(objResponce);

                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
                return ms;
            }
          //  file.Close();
            return ms = new MemoryStream(Encoding.UTF8.GetBytes("Bad Request...."));

        }


        public Stream getAllProductsMetaData(ManifestoRequest manifestoData)
        {
            MemoryStream ms;
            if (manifestoData != null)
            {
              
                AllProductMetaDataResponce allProductDetails = obj.getAllProductsMetaData(manifestoData.CountryCode);
                string str = JsonConvert.SerializeObject(allProductDetails);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
                return ms;
            }
            return ms = new MemoryStream(Encoding.UTF8.GetBytes("Bad Request...."));
        }

        public Stream getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\test.txt", true);
            //file.WriteLine("getProductDetailsByImageKey: -Language ID : " + productDetailsRequestData.LanguageID + " Country ID :" + productDetailsRequestData.CountryCode + " ");
            //file.Close();
            ProductDetailsResponce objResponce = obj.getProductDetailsByImageKey(productDetailsRequestData);
            string str = JsonConvert.SerializeObject(objResponce);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return ms;
        }

        public Stream GetManifesto(ManifestoRequest manifestoData)
        {
            //System.IO.StreamWriter file = new System.IO.StreamWriter("D:\\test.txt", true);
            //file.WriteLine("Language ID :" + manifestoData.LanguageID + " Country ID :" + manifestoData.CountryCode + " ");
            //file.Close();
            ManifestoResponce objResponce = obj.GetManifesto(manifestoData);
            string str = JsonConvert.SerializeObject(objResponce);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return ms;
        }

    }
}
