using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DAL;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models
{
    public class clsCompanyMissionInfo
    {
        DBManage DBobject = new DBManage();
        int company_mission_information_id;
        int language_id;
        string mission_statement;
        string video_url;
        string language_name;
        List<LanguageDetails> lstLanguage;

        public int CompanyMissionInfoID
        {
            get { return company_mission_information_id; }
            set { company_mission_information_id = value; }

        }

        public int LanguageID
        {
            get { return language_id; }
            set { language_id = value; }
        }
        [Display(Name = "Mission Statement")]
        public string MissionStatement
        {
            get { return mission_statement; }
            set { mission_statement = value; }
        }
        [Display(Name = "Video Url")]
        public string VideoUrl
        {
            get { return video_url; }
            set { video_url = value; }
        }
        [Display(Name = "Language")]
        public string LanguageName
        {
            get { return language_name; }
            set { language_name = value; }
        }

        public List<LanguageDetails> ListLanguage
        {
            get { return lstLanguage; }
            set { lstLanguage = value; }

        }

        public List<clsCompanyMissionInfo> getAllCompanyMissionByID(int companyMissionID = 0)
        {
            List<clsCompanyMissionInfo> lstCompanyMission = new List<clsCompanyMissionInfo>();
            try
            {
                string str = "";
                if (companyMissionID > 0)
                {
                    str = "select company_mission_information_id,CompanyMissionInformation.language_id,LanguageMaster.language_name,mission_statement,modified_date_time,video_url from CompanyMissionInformation INNER JOIN LanguageMaster ON LanguageMaster.language_id=CompanyMissionInformation.language_id and LanguageMaster.is_active=1 and CompanyMissionInformation.company_mission_information_id=" + companyMissionID + "";
                }
                else
                {
                    str = "select company_mission_information_id,CompanyMissionInformation.language_id,LanguageMaster.language_name,mission_statement,modified_date_time,video_url from CompanyMissionInformation INNER JOIN LanguageMaster ON LanguageMaster.language_id=CompanyMissionInformation.language_id and LanguageMaster.is_active=1";
                }
                DataTable dtCompanyMission = DBobject.SelectData(str);
                if (dtCompanyMission.Rows.Count > 0)
                {
                    lstCompanyMission = populateCompanyMissionList(dtCompanyMission);
                }
                return lstCompanyMission;
            }
            catch (Exception ee)
            {
                return lstCompanyMission;
            }
        }
        private List<clsCompanyMissionInfo> populateCompanyMissionList(DataTable dt)
        {
            List<clsCompanyMissionInfo> lstCompanyMission = new List<clsCompanyMissionInfo>();
            try
            {


                clsCompanyMissionInfo tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsCompanyMissionInfo();
                        tmpObj.CompanyMissionInfoID = Convert.ToInt32(dt.Rows[i]["company_mission_information_id"]);
                        tmpObj.LanguageID = Convert.ToInt32(dt.Rows[i]["language_id"]);
                        tmpObj.LanguageName = Convert.ToString(dt.Rows[i]["language_name"]);
                        tmpObj.MissionStatement = Convert.ToString(dt.Rows[i]["mission_statement"]);
                        tmpObj.VideoUrl = Convert.ToString(dt.Rows[i]["video_url"]);
                        lstCompanyMission.Add(tmpObj);
                    }
                }
                return lstCompanyMission;
            }
            catch (Exception ee)
            {
                return lstCompanyMission;
            }
        }
        public clsCompanyMissionInfo addCompanyMissionDefaultData(string countryCode = null)
        {
            clsCompanyMissionInfo objCompanyMission = new clsCompanyMissionInfo();
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

                objCompanyMission.ListLanguage = lstLnaguage;
                return objCompanyMission;
            }
            catch (Exception ee)
            {
                return objCompanyMission;
            }
        }

        public int DeleteCompanyMission(clsCompanyMissionInfo obj)
        {
            try
            {
                string str = "Delete from CompanyMissionInformation where company_mission_information_id=" + obj.CompanyMissionInfoID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int EditCompanyMission(clsCompanyMissionInfo obj)
        {
            try
            {
                string str = "update CompanyMissionInformation set language_id=" + obj.LanguageID + ",mission_statement='" + obj.MissionStatement + "',modified_date_time=Convert(datetime,'" + DateTime.Now + "',103),video_url='" + obj.VideoUrl + "' where company_mission_information_id=" + obj.CompanyMissionInfoID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int AddCompanyMission(clsCompanyMissionInfo obj)
        {
            try
            {
                string str = "insert into CompanyMissionInformation(language_id,mission_statement,modified_date_time,video_url)values(" + obj.LanguageID + ",'" + obj.MissionStatement + "',Convert(datetime,'" + DateTime.Now + "',103),'" + obj.VideoUrl + "')";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
    }
}