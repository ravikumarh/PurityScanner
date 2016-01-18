using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class LanguageDetails
    {
        int language_id;
        string languagae_name;
        public int LanguageId
        {
            get { return language_id; }
            set { language_id = value; }
        }

        public string LanguageName
        {
            get { return languagae_name; }
            set { languagae_name = value; }
        }
    }
}