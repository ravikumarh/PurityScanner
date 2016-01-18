using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class clsCountryMaster
    {
        DBManage DBobject = new DBManage();
        string country_code;
        string country_name;
        string language_name;
        string secondary_language_name;
        int language_id;
        int secondary_language_id;
        bool is_active;
        List<LanguageDetails> lngDetails;

        [Display(Name = "Country Code")]
        public string CountryCode
        {
            get { return country_code; }
            set { country_code = value; }
        }
        [Display(Name = "Country Name")]
        public string CountryName
        {
            get { return country_name; }
            set { country_name = value; }
        }
        [Display(Name = "Language")]
        public string LanguageName
        {
            get { return language_name; }
            set { language_name = value; }
        }

        [Display(Name = "Secondary language")]
        public string SecondaryLanguageName
        {
            get { return secondary_language_name; }
            set { secondary_language_name = value; }
        }
        public int LanguageId
        {
            get { return language_id; }
            set { language_id = value; }
        }

        public int SecondaryLanguageId
        {
            get { return secondary_language_id; }
            set { secondary_language_id = value; }
        }
        [Display(Name = "Is Active")]
        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }
        public List<LanguageDetails> ListLanguage
        {
            get { return lngDetails; }
            set { lngDetails = value; }
        }

        public List<clsCountryMaster> getAllCountryByCode(string countryCode = null)
        {
            List<clsCountryMaster> lstCountry = new List<clsCountryMaster>();
            try
            {
                string str = "";
                if (countryCode != null)
                {
                    str = "select CountryMaster.country_code,CountryMaster.country_name,CountryMaster.country_default_language_id,LanguageMaster.language_name,SecondaryCountryLanguageMapping.language_id as SecondaryLanguageId,secondaryLanguageMaster.language_name as SecondaryLanguageName ,CountryMaster.is_active from [dbo].[CountryMaster] INNER JOIN LanguageMaster ON LanguageMaster.language_id=CountryMaster.country_default_language_id INNER JOIN SecondaryCountryLanguageMapping ON SecondaryCountryLanguageMapping.country_code=CountryMaster.country_code INNER JOIN LanguageMaster secondaryLanguageMaster ON secondaryLanguageMaster.language_id=SecondaryCountryLanguageMapping.language_id WHERE CountryMaster.country_code='" + countryCode + "' and CountryMaster.is_active=1 and LanguageMaster.is_active=1";
                }
                else
                {
                    str = "select CountryMaster.country_code,CountryMaster.country_name,CountryMaster.country_default_language_id,LanguageMaster.language_name,SecondaryCountryLanguageMapping.language_id as SecondaryLanguageId,secondaryLanguageMaster.language_name as SecondaryLanguageName ,CountryMaster.is_active from [dbo].[CountryMaster] INNER JOIN LanguageMaster ON LanguageMaster.language_id=CountryMaster.country_default_language_id INNER JOIN SecondaryCountryLanguageMapping ON SecondaryCountryLanguageMapping.country_code=CountryMaster.country_code INNER JOIN LanguageMaster secondaryLanguageMaster ON secondaryLanguageMaster.language_id=SecondaryCountryLanguageMapping.language_id WHERE  CountryMaster.is_active=1 and LanguageMaster.is_active=1";
                }
                DataTable dtCountry = DBobject.SelectData(str);
                if (dtCountry.Rows.Count > 0)
                {
                    lstCountry = populateCountryList(dtCountry);
                }
                return lstCountry;
            }
            catch (Exception ee)
            {
                return lstCountry;
            }
        }

        private List<clsCountryMaster> populateCountryList(DataTable dt)
        {
            List<clsCountryMaster> lstCountry = new List<clsCountryMaster>();
            try
            {


                clsCountryMaster tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsCountryMaster();
                        tmpObj.CountryCode = Convert.ToString(dt.Rows[i]["country_code"]);
                        tmpObj.CountryName = Convert.ToString(dt.Rows[i]["country_name"]);
                        tmpObj.LanguageId = Convert.ToInt32(dt.Rows[i]["country_default_language_id"]);
                        tmpObj.LanguageName = Convert.ToString(dt.Rows[i]["language_name"]);
                        tmpObj.SecondaryLanguageId = Convert.ToInt32(dt.Rows[i]["SecondaryLanguageId"]);
                        tmpObj.SecondaryLanguageName = Convert.ToString(dt.Rows[i]["SecondaryLanguageName"]);
                        tmpObj.IsActive = (bool)dt.Rows[i]["is_active"];
                        lstCountry.Add(tmpObj);
                    }
                }
                return lstCountry;
            }
            catch (Exception ee)
            {
                return lstCountry;
            }
        }

        public clsCountryMaster addCountryDefaultData(string countryCode = null)
        {
            clsCountryMaster objCountry = new clsCountryMaster();
            List<LanguageDetails> lstLnaguage = new List<LanguageDetails>();
            LanguageDetails obj;
            try
            {

                string str = "select language_id,language_name from languageMaster where is_active=1";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        obj = new LanguageDetails();
                        obj.LanguageId = Convert.ToInt32(dt.Rows[i]["language_id"]);
                        obj.LanguageName = Convert.ToString(dt.Rows[i]["language_name"]);
                        lstLnaguage.Add(obj);
                    }
                }

                objCountry.ListLanguage = lstLnaguage;
                return objCountry;
            }
            catch (Exception ee)
            {
                return objCountry;
            }
        }

        public int editCountry(clsCountryMaster obj)
        {
            try
            {
                string str = "update CountryMaster set country_default_language_id=" + obj.LanguageId + ",country_name='" + obj.CountryName + "' where country_code='" + obj.CountryCode + "'";
                int i = DBobject.IUD_Data(str);
                if (i > 0)
                {
                    str = "update SecondaryCountryLanguageMapping set language_id=" + obj.SecondaryLanguageId + "  where country_code='" + obj.CountryCode + "'";
                    i += DBobject.IUD_Data(str);
                }

                return i;
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int deleteCountry(clsCountryMaster obj)
        {
            try
            {
                string str = "update CountryMaster set is_active=0 where country_code='" + obj.CountryCode + "'";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int addCountry(clsCountryMaster obj)
        {
            try
            {
                string str = "select * from CountryMaster where country_code='" + obj.CountryCode + "'";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count <= 0)
                {

                    str = "insert into CountryMaster(country_code,country_name,country_default_language_id,is_active) values('" + obj.CountryCode + "','" + obj.CountryName + "'," + obj.LanguageId + ",1)";
                    int i = DBobject.IUD_Data(str);
                    if (i > 0)
                    {
                        str = "insert into SecondaryCountryLanguageMapping(country_code,language_id)values('" + obj.CountryCode + "'," + obj.LanguageId + ")";
                        i = i + DBobject.IUD_Data(str);
                    }
                    return i;
                }
                else
                {
                     str = "select * from CountryMaster where country_code='" + obj.CountryCode + "' and is_active=1";
                     dt = DBobject.SelectData(str);
                     if (dt.Rows.Count <= 0)
                     {
                         str = "update CountryMaster set country_default_language_id=" + obj.LanguageId + ",country_name='" + obj.CountryName + "', is_active=1 where country_code='" + obj.CountryCode + "'";
                         int i = DBobject.IUD_Data(str);
                         if (i > 0)
                         {
                             str = "update SecondaryCountryLanguageMapping set language_id=" + obj.SecondaryLanguageId + "  where country_code='" + obj.CountryCode + "'";
                             i += DBobject.IUD_Data(str);
                         }
                         return i;
                     }
                     else
                     {
                         return 3;
                     }

                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
    }
}