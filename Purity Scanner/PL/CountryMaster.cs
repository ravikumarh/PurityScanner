using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class CountryMaster
    {
        int country_id = 0;
        string country_name = "";
        int country_default_language_id = 0;
       int country_secondary_language_id =0; 
        bool is_active = true;
        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }
       public string CountryName
        {
            get { return country_name; }
            set { country_name = value; }
        }
       public int CountryId
        {
            get { return country_id; }
            set { country_id = value; }
        }
        public int CountryDefaultLanguageId
        {
            get { return country_default_language_id; }
            set { country_default_language_id = value; }
        }

        public int CountrySecondaryLanguageID
        {
            get { return country_secondary_language_id; }
            set { country_secondary_language_id = value; }
        }
    }
}
