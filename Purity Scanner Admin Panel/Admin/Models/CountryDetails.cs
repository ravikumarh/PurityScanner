using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class CountryDetails
    {
        string country_code;
        string country_name;

        public string CountryCode
        {
            get { return country_code; }
            set { country_code = value; }
        }
        public string CountryName
        {
            get { return country_name; }
            set { country_name = value; }
        }
    }
}