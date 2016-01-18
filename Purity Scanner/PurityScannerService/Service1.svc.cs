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
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
namespace PurityScannerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
  
    //  [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,ConcurrencyMode = ConcurrencyMode.Multiple)]
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        string clientUploadFilePath = System.Configuration.ConfigurationManager.AppSettings["clientUploadFilePath"];
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
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            file.WriteLine("SecurityKey ID" + SecurityKey.SecurityKey + "");
            file.Close();
            AppMetaDataResponce objResponce = obj.getAppMetadata(SecurityKey.SecurityKey);
            string str = JsonConvert.SerializeObject(objResponce);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return ms;
        }

        public Stream getAllProductsByIDs(ProductsByIDsRequest productRequestData)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            file.WriteLine("getAllProductsByIDs Language ID :-" + productRequestData.LanguageID + "");
            file.Close();
            MemoryStream ms;
            string str;
            if (productRequestData != null)
            {

          
                if (obj.checkSubProductsById(productRequestData.ProductID))
                {
                    ProductSubProductResponse response=obj.getProductSubProductDetailsByID(productRequestData);
                    str = JsonConvert.SerializeObject(response);
                }
                else
                {
                    ProductDetailsResponce objResponce = obj.getAllProductsByIDs(productRequestData);
                    str = JsonConvert.SerializeObject(objResponce);
                }
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
                return ms;
            }
          
            return ms = new MemoryStream(Encoding.UTF8.GetBytes("Bad Request...."));

        }


        public Stream getAllProductsMetaData(ManifestoRequest manifestoData)
        {
            MemoryStream ms;
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            file.WriteLine("getAllProductsMetaData");
            file.Close();
            if (manifestoData != null)
            {
              
                AllProductMetaDataResponce allProductDetails = obj.getAllProductsMetaData(manifestoData);
                string str = JsonConvert.SerializeObject(allProductDetails);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
                return ms;
            }
            return ms = new MemoryStream(Encoding.UTF8.GetBytes("Bad Request...."));
        }

        public Stream getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            file.WriteLine("getProductDetailsByImageKey: -Language ID : " + productDetailsRequestData.LanguageID + " Country ID :" + productDetailsRequestData.CountryCode + " ");
            file.Close();
            string str;
            if (obj.checkSubProductsByImageKey(productDetailsRequestData.ImageKey))
            {
                ProductSubProductResponse response = obj.getProductSubProductDetailsByImageKey(productDetailsRequestData);
                str = JsonConvert.SerializeObject(response);
            }
            else
            {
                ProductDetailsResponce objResponce = obj.getProductDetailsByImageKey(productDetailsRequestData);
                str = JsonConvert.SerializeObject(objResponce);
            }
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return ms;
        }

        public Stream GetManifesto(ManifestoRequest manifestoData)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            file.WriteLine("Language ID :" + manifestoData.LanguageID + " Country ID :" + manifestoData.CountryCode + " ");
            file.Close();
            ManifestoResponce objResponce = obj.GetManifesto(manifestoData);
            string str = JsonConvert.SerializeObject(objResponce);
            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(str));
            return ms;
        }

        public Stream uploadImage(ImageUpload imgData)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            file.WriteLine("uploadImage");
            file.Close();
            int k = 1;
            ImageUploadRespponse objResult = new ImageUploadRespponse();
            //Image t = Image.FromFile(@"" + clientUploadFilePath + "\\Calcium.png");
            //MemoryStream mt = new MemoryStream();
            //t.Save(mt, System.Drawing.Imaging.ImageFormat.Png);
            //Byte[] bt = mt.ToArray();
            //imgData.ImageData = bt;
            try
            {
                if (imgData.ImageData.Length > 0)
                {

                    Byte[] byteImage = Convert.FromBase64String(imgData.ImageData);
                    Size size = new Size(600, 400);
                    string fileName = "NewImage.png";
                    int i=1;
                    while (File.Exists(@"" + clientUploadFilePath + "\\" + fileName + ""))
                    {
                        fileName = "NewImage" + i + ".png";
                        i++;
                    }
                    //File.Delete(@"" + clientUploadFilePath + "\\" + fileName + "");
                    MemoryStream ms = new MemoryStream(byteImage);
                    Image img = Image.FromStream(ms);
                    img=(Image)(new Bitmap(img,size));
                    img.Save(@"" + clientUploadFilePath + "\\" + fileName + "", System.Drawing.Imaging.ImageFormat.Png);
                   // Attachment attachment = new Attachment(@"" + clientUploadFilePath + "\\" + fileName + "", MediaTypeNames.Application.Octet);
                   // MailMessage msg = new MailMessage();
                   // msg.From = new MailAddress("ravinharihar@gmail.com");
                   // msg.To.Add("ravinharihar@gmail.com");
                   // msg.Subject = "test";
                   // msg.Body = "<p>Hello Admin,</p><p>User try to scan the following bottle from</p> <p>  <B>City:-</B>" + imgData.City + "</p> <p> <B>Country.:-</B>" + imgData.Country + "</p><P>Unfortunately we don not seem to have this bottle in our database or there was an error in the scan.</P>";
                   // msg.Priority = MailPriority.High;
                   // msg.Attachments.Add(attachment);
                   // msg.IsBodyHtml = true;
                   // SmtpClient client = new SmtpClient();//smtp.gmail.com
                   // client.Host = "smtp.wiredviews.com";
                   //// client.Port = 587;
                   // client.EnableSsl = true;
                   // client.DeliveryMethod = SmtpDeliveryMethod.Network;
                   // client.UseDefaultCredentials = false;
                   // client.Credentials = new NetworkCredential("ravinharihar@gmail.com", "shreya2304");
                   // client.Timeout = 200000;
                   // client.Send(msg);
                  
                    objResult.ResponseStatus = 1;
                    objResult.ResponseCode = "101";
                    objResult.ResponseMessage = "Image uploaded successfully.";
                    string str = JsonConvert.SerializeObject(objResult);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    MemoryStream msResult = new MemoryStream(Encoding.UTF8.GetBytes(str));
                    return msResult;
                  
                }
                else
                {
                    objResult.ResponseStatus = 0;
                    objResult.ResponseCode = "100";
                    objResult.ResponseMessage = "Image not uploaded.";
                    string str = JsonConvert.SerializeObject(objResult);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                    MemoryStream msResult = new MemoryStream(Encoding.UTF8.GetBytes(str));
                    return msResult;
                   
                }
            }
            catch (Exception ee)
            {
                objResult.ResponseStatus = 0;
                objResult.ResponseCode = "100";
                objResult.ResponseMessage = Convert.ToString(ee.Message);
                string str = JsonConvert.SerializeObject(objResult);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json; charset=utf-8";
                MemoryStream msResult = new MemoryStream(Encoding.UTF8.GetBytes(str));
                return msResult;
            }
          //  return "Image not uploaded.";
        }

        public byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

    }
}
