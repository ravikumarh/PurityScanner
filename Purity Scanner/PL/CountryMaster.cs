﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class CountryMaster
    {
        string country_code;
        string country_name = "";
        int country_default_language_id = 0;
        List<SecondaryLanguagesOfCountry> lstSecondaryLanguage;
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
       public string CountryCode
        {
            get { return country_code; }
            set { country_code = value; }
        }
        public int CountryDefaultLanguageId
        {
            get { return country_default_language_id; }
            set { country_default_language_id = value; }
        }

        public List<SecondaryLanguagesOfCountry> LstSecondaryLanguage
        {
            get { return lstSecondaryLanguage; }
            set { lstSecondaryLanguage = value; }
        }
    }
}
