using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class SecondaryLanguagesOfCountry
    {
       int secondaryLanguageId;
       string secondaryLanguageName;

       public int SecondaryLanguageId
       {
           get { return secondaryLanguageId; }
           set { secondaryLanguageId = value; }
       }

       public string SecondaryLanguageName
       {
           get { return secondaryLanguageName; }
           set { secondaryLanguageName = value; }
       }


    }
}
