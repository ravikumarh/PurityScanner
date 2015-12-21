using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public  class ProductSubProductResponse
    {
     int responseStatus;
     string responseCode;
     string responseMessage;
     bool multipleproduct;
     List<ProductForSubProduct> lstProducts;
 

     public int ResponseStatus
     {
         get { return responseStatus; }
         set { responseStatus = value; }
     }
     public string ResponseCode
     {
         get { return responseCode; }
         set { responseCode = value; }
     }
     public string ResponseMessage
     {
         get { return responseMessage; }
         set { responseMessage = value; }
     }
     public bool MultipleProduct
     {
         get { return multipleproduct; }
         set { multipleproduct = value; }
     }

     public List<ProductForSubProduct> LstProducts
     {
         get { return lstProducts; }
         set { lstProducts = value; }
     }
    }
}
