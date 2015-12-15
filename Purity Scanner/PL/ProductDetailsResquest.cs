using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class ProductDetailsResquest
    {
     string countryCode;
     int languageID;
     string securityKey;
     List<ImageKeys> imageKeys;
     int comparingWithProductID;
     string userLattitude;
     string userLongitude;

     public string CountryCode
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
     public List<ImageKeys> lstimageKeys
     {
         get { return imageKeys; }
         set { imageKeys = value; }
     }

    }

  public class ImageKeys
   {
       string imageKeys;
       public string ImageKeysInfo
       {
           get { return imageKeys; }
           set { imageKeys = value; }
       }
   }
}
