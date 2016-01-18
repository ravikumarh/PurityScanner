using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class clsAttributeMaster
    {
        int attribute_id;
        string attribute_name;
        string attribute_image_url;
        bool is_active;
        DBManage DBobject = new DBManage();
        public int AttributeId
        {
            get { return attribute_id; }
            set { attribute_id = value; }
        }

        [Display(Name = "Attribute Name")]
        public string AttributeName
        {
            get { return attribute_name; }
            set { attribute_name = value; }
        }

        [Display(Name = "Attribute Image")]
        public string AttributeImageUrl
        {
            get { return attribute_image_url; }
            set { attribute_image_url = value; }
        }

        [Display(Name = "Is Active")]
        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }



        public int addAttribute(clsAttributeMaster obj)
        {
            try
            {
                string str = "select * from AttributesMaster where attribute_name='" + obj.AttributeName + "'";
                DataTable dtAttribute = DBobject.SelectData(str);
                if (dtAttribute.Rows.Count <= 0)
                {
                    obj.IsActive = true;
                    str = "insert into AttributesMaster(attribute_name,attribute_image_url,isActive)values('" + obj.AttributeName + "','" + obj.AttributeImageUrl + "','" + obj.IsActive + "')";
                    return DBobject.IUD_Data(str);
                }
                else
                {
                    str = "select * from AttributesMaster where attribute_name='" + obj.AttributeName + "' and  isActive=0";
                    dtAttribute = DBobject.SelectData(str);
                    if (dtAttribute.Rows.Count > 0)
                    {
                        str = "update AttributesMaster set isActive=1, attribute_name='" + obj.AttributeName + "',attribute_image_url='" + obj.AttributeImageUrl + "' where attribute_id=" +Convert.ToInt32(dtAttribute.Rows[0]["attribute_id"]) + "";
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

        public int editAttribute(clsAttributeMaster obj)
        {
            try
            {
                //string str = "select * from AttributesMaster where attribute_name='" + obj.AttributeName + "'and  isActive=1";
                //DataTable dtAttribute = DBobject.SelectData(str);
                //if (dtAttribute.Rows.Count <= 0)
                //{
                string str = "update AttributesMaster set attribute_name='" + obj.AttributeName + "',attribute_image_url='" + obj.AttributeImageUrl + "' where attribute_id=" + obj.AttributeId + "";
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

        public int deleteAttribute(clsAttributeMaster obj)
        {
            try
            {
                string str = "update AttributesMaster set isActive='" + obj.IsActive + "' where attribute_id= " + obj.AttributeId + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public List<clsAttributeMaster> getAttributesById(int id = 0)
        {
            List<clsAttributeMaster> lstAttributes = new List<clsAttributeMaster>();
            try
            {
                string str = "";
                if (id > 0)
                {
                    str = "Select * from AttributesMaster where isActive=1 and attribute_id=" + id + "";
                }
                else
                {
                    str = "Select * from AttributesMaster where isActive=1";
                }
                DataTable dtAttribute = DBobject.SelectData(str);
                if (dtAttribute.Rows.Count > 0)
                {
                    lstAttributes = populateAttributeList(dtAttribute);
                }
                return lstAttributes;
            }
            catch (Exception ee)
            {
                return lstAttributes;
            }

        }

        private List<clsAttributeMaster> populateAttributeList(DataTable dt)
        {
            List<clsAttributeMaster> lstAttribute = new List<clsAttributeMaster>();
            try
            {
                clsAttributeMaster tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsAttributeMaster();
                        tmpObj.AttributeId = Convert.ToInt32(dt.Rows[i]["attribute_id"]);
                        tmpObj.AttributeName = Convert.ToString(dt.Rows[i]["attribute_name"]);
                        tmpObj.AttributeImageUrl = Convert.ToString(dt.Rows[i]["attribute_image_url"]);
                        tmpObj.IsActive = (bool)dt.Rows[i]["isActive"];
                        lstAttribute.Add(tmpObj);
                    }
                }
                return lstAttribute;
            }
            catch (Exception ee)
            {
                return lstAttribute;
            }
        }
    }
}