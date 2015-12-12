using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class AllProductMetaData
    {
        int productID;
        string productName;
        string productDetails;
        string productImageUrl;
       public int ProductID
       {
           get { return productID; }
           set { productID = value; }
       }

       public string ProductName
       {
           get { return productName; }
           set { productName = value; }
       }
       public string ProductDetails
       {
           get { return productDetails; }
           set { productDetails = value; }
       }

       public string ProductImageUrl
       {
           get { return productImageUrl; }
           set { productImageUrl = value; }
       }
    }

}
