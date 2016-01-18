using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;

namespace Admin.Models
{
    public class clsLanguageMaster
    {
        int language_id;
        string languagae_name;
        bool is_active;
        DBManage DBobject = new DBManage();
        public int LanguageId
        {
            get { return language_id; }
            set { language_id = value; }
        }

        [Display(Name = "Language Name")]
        public string LanguageName
        {
            get { return languagae_name; }
            set { languagae_name = value; }
        }
        [Display(Name = "Is Active")]
        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }

        public int addLanguage(clsLanguageMaster obj)
        {
            try
            {
                string str = "Select * from LanguageMaster where language_name='" + obj.LanguageName + "'";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count <= 0)
                {
                    obj.IsActive = true;
                    str = "insert into LanguageMaster(language_name,is_active)values('" + obj.LanguageName + "','" + obj.IsActive + "')";
                    return DBobject.IUD_Data(str);
                }
                else
                {
                    str = "Select * from LanguageMaster where language_name='" + obj.LanguageName + "' and is_active=1";
                    dt = DBobject.SelectData(str);
                    if (dt.Rows.Count <= 0)
                    {
                        str = "Select * from LanguageMaster where language_name='" + obj.LanguageName + "' and is_active=0";
                        dt = DBobject.SelectData(str);
                        if (dt.Rows.Count > 0)
                        {
                            str = "update LanguageMaster set is_active=1 where language_id=" + Convert.ToInt32(dt.Rows[0]["language_id"]) + "";
                            return DBobject.IUD_Data(str);
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 2; 
                    }
                }
                return 0;
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int editLanguage(clsLanguageMaster obj)
        {
            try
            {
                string str = "update LanguageMaster set language_name='" + obj.LanguageName + "' where language_id=" + obj.LanguageId + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int deleteLanguage(clsLanguageMaster obj)
        {
            try
            {
                string str = "update LanguageMaster set is_active='" + obj.IsActive + "' where language_id= " + obj.LanguageId + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public List<clsLanguageMaster> getLanguagesById(int id = 0)
        {
            List<clsLanguageMaster> lstlanguages = new List<clsLanguageMaster>();
            try
            {
                string str = "";
                if (id > 0)
                {
                    str = "Select * from LanguageMaster where is_active=1 and language_id=" + id + "";
                }
                else
                {
                    str = "Select * from LanguageMaster where is_active=1";
                }
                DataTable dtLanguges = DBobject.SelectData(str);
                if (dtLanguges.Rows.Count > 0)
                {
                    lstlanguages = populateLanguageList(dtLanguges);
                }
                return lstlanguages;
            }
            catch (Exception ee)
            {
                return lstlanguages;
            }

        }

        private List<clsLanguageMaster> populateLanguageList(DataTable dt)
        {
            List<clsLanguageMaster> lstlanguages = new List<clsLanguageMaster>();
            try
            {
                clsLanguageMaster tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsLanguageMaster();
                        tmpObj.LanguageId = Convert.ToInt32(dt.Rows[i]["language_id"]);
                        tmpObj.LanguageName = Convert.ToString(dt.Rows[i]["language_name"]);
                        tmpObj.IsActive = (bool)dt.Rows[i]["is_active"];
                        lstlanguages.Add(tmpObj);
                    }
                }
                return lstlanguages;
            }
            catch (Exception ee)
            {
                return lstlanguages;
            }
        }



    }
}
