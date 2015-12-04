using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class ProductsByIDsRequest
    {
        int countryCode;
        int languageID;
        string securityKey;
        List<ProductIDs> productIDs=new List<ProductIDs>();
        int comparingWithProductID;
        string userLattitude;
        string userLongitude;

        public int CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        public int LanguageID
        {
            get { return languageID; }
            set { languageID = value; }
        }
        public string SecurityKey
        {
            get { return securityKey; }
            set { securityKey = value; }
        }

        public int ComparingWithProductID
        {
            get { return comparingWithProductID; }
            set { comparingWithProductID = value; }
        }

        public string UserLattitude
        {
            get { return userLattitude; }
            set { userLattitude = value; }
        }

        public string UserLongitude
        {
            get { return userLongitude; }
            set { userLongitude = value; }
        }
        public List<ProductIDs> ProductIDs
        {
            get { return productIDs; }
            set { productIDs = value; }
        }
    }

 public  class ProductIDs
   {
       int productID;
       public int ProductID
       {
           get { return productID; }
           set { productID = value; }
       }
   }
}
