using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class ManifestoRequest
    {
       int countryCode;
       int languageID;

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
    }
}
