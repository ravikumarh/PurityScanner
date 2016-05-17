using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Models
{
    public class clsAttributeTitle
    {
        int attribute_title_id;
        int attribute_id;
        string attribute_name;
        string attribute_title;
        int language_id;
        string language_name;
        List<AttributeDetails> lstAttribute;
        List<LanguageDetails> lngDetails;
        DBManage DBobject = new DBManage();
        int filter_attribute_id;
        int filter_attri_language_id;
        int delete_attribute_title_id;
        string short_description;
        string manifesto_information;


        public int AttributeTitleID
        {
            get { return attribute_title_id; }
            set { attribute_title_id = value; }
        }
        public int AttributeID
        {
            get { return attribute_id; }
            set { attribute_id = value; }
        }
        [Display(Name = "Attribute")]
        public string AttributeName
        {
            get { return attribute_name; }
            set { attribute_name = value; }
        }
        [Display(Name = "Attribute Title")]
        public string AttributeTitle
        {
            get { return attribute_title; }
            set { attribute_title = value; }
        }
        public int LanguageID
        {
            get { return language_id; }
            set { language_id = value; }
        }
        [Display(Name = "Language")]
        public string LanguageName
        {
            get { return language_name; }
            set { language_name = value; }
        }

        public List<AttributeDetails> ListAttribute
        {
            get { return lstAttribute; }
            set { lstAttribute = value; }
        }

        public List<LanguageDetails> ListLanguage
        {
            get { return lngDetails; }
            set { lngDetails = value; }
        }

        public int FilterAttributeID
        {
            get { return filter_attribute_id; }
            set { filter_attribute_id = value; }
        }
        public int FilterAttributeLanguageID
        {
            get { return filter_attri_language_id; }
            set { filter_attri_language_id = value; }
        }

         public int DeleteAttributeTitleID
        {
            get { return delete_attribute_title_id; }
            set { delete_attribute_title_id = value; }
        }



         [Display(Name = "Short Info")]
         public string ShortDescription
         {
             get { return short_description; }
             set { short_description = value; }
         }
         [Display(Name = "Manifesto Info")]
         [AllowHtml]
         public string ManifestoInformation
         {
             get { return manifesto_information; }
             set { manifesto_information = value; }
         }
        
        public clsAttributeTitle addAttributeDefaultData()
        {
            clsAttributeTitle objAttributeTitle = new clsAttributeTitle();
            List<LanguageDetails> lstLnaguage = new List<LanguageDetails>();
            List<AttributeDetails> lstAttribute = new List<AttributeDetails>();
            LanguageDetails obj;
            AttributeDetails objAttribute;
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
                objAttributeTitle.ListLanguage = lstLnaguage;

                str = "select attribute_id,attribute_name from AttributesMaster where isActive=1 AND attribute_id not in (8,9,10,14)";
                DataTable dtAttribute = DBobject.SelectData(str);
                if (dtAttribute.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAttribute.Rows.Count; i++)
                    {
                        objAttribute = new AttributeDetails();
                        objAttribute.AttributeId = Convert.ToInt32(dtAttribute.Rows[i]["attribute_id"]);
                        objAttribute.AttributeName = Convert.ToString(dtAttribute.Rows[i]["attribute_name"]);
                        lstAttribute.Add(objAttribute);
                    }
                }
                objAttributeTitle.ListAttribute = lstAttribute;
                return objAttributeTitle;
            }
            catch (Exception ee)
            {
                return objAttributeTitle;
            }
        }

        public List<clsAttributeTitle> getAllAttributeTitleByID(int attributeTitleID = 0)
        {
            List<clsAttributeTitle> lstAttributeTitle = new List<clsAttributeTitle>();
            try
            {
                string str = "";
                if (attributeTitleID > 0)
                {
                    str = "Select AttributesMaster.attribute_id,AttributesMaster.attribute_name,AttributeTitles.attribute_title_id,AttributeTitles.attribute_title, LanguageMaster.language_id,LanguageMaster.language_name,AttributeTitles.short_description,AttributeTitles.manifesto_information from AttributesMaster INNER JOIN AttributeTitles ON AttributesMaster.attribute_id=AttributeTitles.attribute_id INNER JOIN LanguageMaster ON AttributeTitles.language_id=LanguageMaster.language_id WHERE AttributeTitles.attribute_title_id=" + attributeTitleID + " and AttributesMaster.isActive=1  and LanguageMaster.is_active=1 and AttributeTitles.is_active=1";
                }
                else
                {
                    str = "Select AttributesMaster.attribute_id,AttributesMaster.attribute_name,AttributeTitles.attribute_title_id,AttributeTitles.attribute_title, LanguageMaster.language_id,LanguageMaster.language_name,AttributeTitles.short_description,AttributeTitles.manifesto_information from AttributesMaster INNER JOIN AttributeTitles ON AttributesMaster.attribute_id=AttributeTitles.attribute_id INNER JOIN LanguageMaster ON AttributeTitles.language_id=LanguageMaster.language_id WHERE AttributesMaster.isActive=1  and LanguageMaster.is_active=1 and AttributeTitles.is_active=1";
                }
                DataTable dtAttributeTitles = DBobject.SelectData(str);
                if (dtAttributeTitles.Rows.Count > 0)
                {
                    lstAttributeTitle = populateAttributeTitleList(dtAttributeTitles);
                }
                return lstAttributeTitle;
            }
            catch (Exception ee)
            {
                return lstAttributeTitle;
            }
        }

        public List<clsAttributeTitle> getAllAttributeTitleByAttriIDLangID(int attriID, int langId)
        {
            List<clsAttributeTitle> lstAttributeTitle = new List<clsAttributeTitle>();
            try
            {
                string str = "";
                if (attriID > 0 && langId > 0)
                {
                    str = "Select AttributesMaster.attribute_id,AttributesMaster.attribute_name,AttributeTitles.attribute_title_id,AttributeTitles.attribute_title, LanguageMaster.language_id,LanguageMaster.language_name,AttributeTitles.short_description,AttributeTitles.manifesto_information from AttributesMaster INNER JOIN AttributeTitles ON AttributesMaster.attribute_id=AttributeTitles.attribute_id INNER JOIN LanguageMaster ON AttributeTitles.language_id=LanguageMaster.language_id WHERE  AttributesMaster.isActive=1  and LanguageMaster.is_active=1 and AttributeTitles.is_active=1 and AttributesMaster.attribute_id=" + attriID + " and LanguageMaster.language_id=" + langId + " ";
                }
                else if (attriID > 0 && langId <= 0)
                {
                    str = "Select AttributesMaster.attribute_id,AttributesMaster.attribute_name,AttributeTitles.attribute_title_id,AttributeTitles.attribute_title, LanguageMaster.language_id,LanguageMaster.language_name,AttributeTitles.short_description,AttributeTitles.manifesto_information from AttributesMaster INNER JOIN AttributeTitles ON AttributesMaster.attribute_id=AttributeTitles.attribute_id INNER JOIN LanguageMaster ON AttributeTitles.language_id=LanguageMaster.language_id WHERE  AttributesMaster.isActive=1  and LanguageMaster.is_active=1 and AttributeTitles.is_active=1 and AttributesMaster.attribute_id=" + attriID + "";
                }
                else if (attriID <= 0 && langId > 0)
                {
                    str = "Select AttributesMaster.attribute_id,AttributesMaster.attribute_name,AttributeTitles.attribute_title_id,AttributeTitles.attribute_title, LanguageMaster.language_id,LanguageMaster.language_name,AttributeTitles.short_description,AttributeTitles.manifesto_information from AttributesMaster INNER JOIN AttributeTitles ON AttributesMaster.attribute_id=AttributeTitles.attribute_id INNER JOIN LanguageMaster ON AttributeTitles.language_id=LanguageMaster.language_id WHERE  AttributesMaster.isActive=1  and LanguageMaster.is_active=1 and AttributeTitles.is_active=1 and LanguageMaster.language_id=" + langId + " ";
                }
                else
                {
                    str = "Select AttributesMaster.attribute_id,AttributesMaster.attribute_name,AttributeTitles.attribute_title_id,AttributeTitles.attribute_title, LanguageMaster.language_id,LanguageMaster.language_name,AttributeTitles.short_description,AttributeTitles.manifesto_information from AttributesMaster INNER JOIN AttributeTitles ON AttributesMaster.attribute_id=AttributeTitles.attribute_id INNER JOIN LanguageMaster ON AttributeTitles.language_id=LanguageMaster.language_id WHERE AttributesMaster.isActive=1  and LanguageMaster.is_active=1 and AttributeTitles.is_active=1";
                }
                DataTable dtAttributeTitles = DBobject.SelectData(str);
                if (dtAttributeTitles.Rows.Count > 0)
                {
                    lstAttributeTitle = populateAttributeTitleList(dtAttributeTitles);
                }
                return lstAttributeTitle;
            }
            catch (Exception ee)
            {
                return lstAttributeTitle;
            }
        }
        private List<clsAttributeTitle> populateAttributeTitleList(DataTable dt)
        {
            List<clsAttributeTitle> lstAttributeTitle = new List<clsAttributeTitle>();
            try
            {
                clsAttributeTitle tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsAttributeTitle();
                        tmpObj.AttributeID = Convert.ToInt32(dt.Rows[i]["attribute_id"]);
                        tmpObj.AttributeName = Convert.ToString(dt.Rows[i]["attribute_name"]);
                        tmpObj.AttributeTitleID = Convert.ToInt32(dt.Rows[i]["attribute_title_id"]);
                        tmpObj.AttributeTitle = Convert.ToString(dt.Rows[i]["attribute_title"]);
                        tmpObj.LanguageID = Convert.ToInt32(dt.Rows[i]["language_id"]);
                        tmpObj.LanguageName = Convert.ToString(dt.Rows[i]["language_name"]);
                        tmpObj.ShortDescription = Convert.ToString(dt.Rows[i]["short_description"]);
                        tmpObj.ManifestoInformation = Convert.ToString(dt.Rows[i]["manifesto_information"]);
                        lstAttributeTitle.Add(tmpObj);
                    }
                }
                return lstAttributeTitle;
            }
            catch (Exception ee)
            {
                return lstAttributeTitle;
            }
        }

        public int deleteAttributeTitle(clsAttributeTitle obj)
        {
            try
            {
                string str = "update AttributeTitles set is_active=0 where attribute_title_id=" + obj.AttributeTitleID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int editAttributeTitle(clsAttributeTitle obj)
        {
            try
            {
                //string str = "select * from AttributeTitles where attribute_id=" + obj.AttributeID + " and language_id=" + obj.LanguageID + "";
                //DataTable dtAttributeTitles = DBobject.SelectData(str);
                //if (dtAttributeTitles.Rows.Count <= 0)
                //{
                string str = "update AttributeTitles set attribute_id=" + obj.attribute_id + ",attribute_title='" + obj.attribute_title + "',language_id=" + obj.language_id + ",short_description='" + obj.ShortDescription + "',manifesto_information='"+obj.ManifestoInformation+"' where attribute_title_id=" + obj.AttributeTitleID + "";
                    return DBobject.IUD_Data(str);
                //}
                //else
                //{
                //    return 2;
                //}
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int addAttributeTitle(clsAttributeTitle obj)
        {
            try
            {
                string str = "select * from AttributeTitles where attribute_id=" + obj.AttributeID + " and language_id=" + obj.LanguageID + "";
                DataTable dtAttributeTitles = DBobject.SelectData(str);
                if (dtAttributeTitles.Rows.Count <= 0)
                {

                    str = "insert into AttributeTitles(attribute_id,attribute_title,language_id,is_active,short_description,manifesto_information)values(" + obj.attribute_id + ",'" + obj.attribute_title + "'," + obj.language_id + ",1,'"+obj.ShortDescription+"','"+obj.ManifestoInformation+"')";
                    return DBobject.IUD_Data(str);

                }
                else
                {
                    str = "select * from AttributeTitles where attribute_id=" + Convert.ToInt32(dtAttributeTitles.Rows[0]["attribute_id"]) + " and language_id=" + obj.LanguageID + " and is_active=0 ";
                    dtAttributeTitles = DBobject.SelectData(str);
                    if (dtAttributeTitles.Rows.Count <= 0)
                    {
                        str = "update AttributeTitles set attribute_id=" + obj.attribute_id + ",attribute_title='" + obj.attribute_title + "',language_id=" + obj.language_id + " where attribute_title_id=" + Convert.ToInt32(dtAttributeTitles.Rows[0]["attribute_title_id"]) + "";
                        return DBobject.IUD_Data(str);
                    }
                    else
                    {

                        return 2;
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