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
    public class clsProductAttributeValue
    {
        int product_attribute_value_id;
        int product_id;
        int sub_product_id;
        int attribute_id;
        int language_id;
        string product_name;
        string attribute_name;
        string language_name;
        string sub_product_name;
        string product_attribute_value;
        string short_description;
        string manifesto_information;
        string last_modeified_date_time;
        List<ProductDetails> lstProduct;
        List<AttributeDetails> lstAttribute;
        List<LanguageDetails> lstLanguage;
        List<SubProductDetails> lstSubProduct;
        int filter_product_id;
        int filter_sub_product_id;
        int filter_attribute_id;
        int filter_language_id;
        DBManage DBobject = new DBManage();
        public int ProductAttributeValuesID
        {
            get { return product_attribute_value_id; }
            set { product_attribute_value_id = value; }
        }
        public int ProductID
        {
            get { return product_id; }
            set { product_id = value; }
        }

        public int SubProductID
        {
            get { return sub_product_id; }
            set { sub_product_id = value; }
        }
        public int AttributeID
        {
            get { return attribute_id; }
            set { attribute_id = value; }
        }

        public int LanguageID
        {
            get { return language_id; }
            set { language_id = value; }
        }
        [Display(Name = "Product")]
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }
        [Display(Name = "Attribute")]
        public string AttributeName
        {
            get { return attribute_name; }
            set { attribute_name = value; }
        }
        [Display(Name = "Language")]
        public string LanguageName
        {
            get { return language_name; }
            set { language_name = value; }
        }
        [Display(Name = "Sub Product")]
        public string SubProductName
        {
            get { return sub_product_name; }
            set { sub_product_name = value; }
        }
        [Display(Name = "Value")]
        public string ProductAttributeValues
        {
            get { return product_attribute_value; }
            set { product_attribute_value = value; }
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
        public string LastModifiedDateTime
        {
            get { return last_modeified_date_time; }
            set { last_modeified_date_time = value; }
        }

        public List<ProductDetails> ListProduct
        {
            get { return lstProduct; }
            set { lstProduct = value; }
        }
        public List<LanguageDetails> ListLanguage
        {
            get { return lstLanguage; }
            set { lstLanguage = value; }
        }
        public List<AttributeDetails> ListAttribute
        {
            get { return lstAttribute; }
            set { lstAttribute = value; }
        }
        public List<SubProductDetails> ListSubProduct
        {
            get { return lstSubProduct; }
            set { lstSubProduct = value; }
        }

        public int FilterProductID
        {
            get { return filter_product_id; }
            set { filter_product_id = value; }
        }

        public int FilterSubProductID
        {
            get { return filter_sub_product_id; }
            set { filter_sub_product_id = value; }
        }
        public int FilterAttributeID
        {
            get { return filter_attribute_id; }
            set { filter_attribute_id = value; }
        }

        public int FilterLanguageID
        {
            get { return filter_language_id; }
            set { filter_language_id = value; }
        }


        public List<clsProductAttributeValue> getAllProductAttributeValueByID(int productattributevalueID = 0)
        {
            List<clsProductAttributeValue> lstProductAttributeValue = new List<clsProductAttributeValue>();
            try
            {
                string str = "";
                if (productattributevalueID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and ProductAttributeValues.product_attribute_value_id=" + productattributevalueID + "";
                }
                else
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1";
                }
                DataTable dtProductAttributeValue = DBobject.SelectData(str);
                if (dtProductAttributeValue.Rows.Count > 0)
                {
                    lstProductAttributeValue = populateProductAttributeValueList(dtProductAttributeValue);
                }
                return lstProductAttributeValue;
            }
            catch (Exception ee)
            {
                return lstProductAttributeValue;
            }
        }

        private List<clsProductAttributeValue> populateProductAttributeValueList(DataTable dt)
        {
            List<clsProductAttributeValue> lstProductAttributeValue = new List<clsProductAttributeValue>();
            try
            {
                clsProductAttributeValue tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsProductAttributeValue();
                        tmpObj.ProductAttributeValuesID = Convert.ToInt32(dt.Rows[i]["product_attribute_value_id"]);
                        tmpObj.ProductName = Convert.ToString(dt.Rows[i]["product_name"]);
                        tmpObj.ProductID = Convert.ToInt32(dt.Rows[i]["product_id"]);
                        tmpObj.SubProductID = Convert.ToInt32(dt.Rows[i]["sub_product_id"]);
                        tmpObj.SubProductName = Convert.ToString(dt.Rows[i]["title"]);
                        tmpObj.AttributeID = Convert.ToInt32(dt.Rows[i]["attribute_id"]);
                        tmpObj.AttributeName = Convert.ToString(dt.Rows[i]["attribute_name"]);
                        tmpObj.LanguageID = Convert.ToInt32(dt.Rows[i]["language_id"]);
                        tmpObj.LanguageName = Convert.ToString(dt.Rows[i]["language_name"]);
                        tmpObj.ProductAttributeValues = Convert.ToString(dt.Rows[i]["product_attribute_value"]);
                        tmpObj.ShortDescription = Convert.ToString(dt.Rows[i]["short_description"]);
                        tmpObj.ManifestoInformation = Convert.ToString(dt.Rows[i]["manifesto_information"]);
                        tmpObj.LastModifiedDateTime = Convert.ToString(dt.Rows[i]["last_modified_date_time"]);
                        lstProductAttributeValue.Add(tmpObj);
                    }
                }
                return lstProductAttributeValue;
            }
            catch (Exception ee)
            {
                return lstProductAttributeValue;
            }
        }

        public clsProductAttributeValue addProductAttributeValueDefaultData()
        {
            clsProductAttributeValue objProductAttributeValue = new clsProductAttributeValue();
            List<AttributeDetails> lstAttribute = new List<AttributeDetails>();
            List<ProductDetails> lstProduct = new List<ProductDetails>();
            List<SubProductDetails> lstSubProduct = new List<SubProductDetails>();
            List<LanguageDetails> lstLanguage = new List<LanguageDetails>();
            AttributeDetails objAttribute;
            ProductDetails objProduct;
            SubProductDetails objSubProduct;
            LanguageDetails objLanguage;
            try
            {

                string str = "select attribute_id,attribute_name from AttributesMaster where isActive=1";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objAttribute = new AttributeDetails();
                        objAttribute.AttributeId = Convert.ToInt32(dt.Rows[i]["attribute_id"]);
                        objAttribute.AttributeName = Convert.ToString(dt.Rows[i]["attribute_name"]);
                        lstAttribute.Add(objAttribute);
                    }
                }
                objProductAttributeValue.ListAttribute = lstAttribute;

                str = "select product_id,product_name from ProductMaster where is_active=1";
                DataTable dtProduct = DBobject.SelectData(str);
                if (dtProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtProduct.Rows.Count; i++)
                    {
                        objProduct = new ProductDetails();
                        objProduct.ProductID = Convert.ToInt32(dtProduct.Rows[i]["product_id"]);
                        objProduct.ProductName = Convert.ToString(dtProduct.Rows[i]["product_name"]);
                        lstProduct.Add(objProduct);
                    }
                }
                objProductAttributeValue.ListProduct = lstProduct;
                str = "select sub_product_id,title from SubProductDetails";
                DataTable dtSubProduct = DBobject.SelectData(str);
                if (dtSubProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSubProduct.Rows.Count; i++)
                    {
                        objSubProduct = new SubProductDetails();
                        objSubProduct.SubProductID = Convert.ToInt32(dtSubProduct.Rows[i]["sub_product_id"]);
                        objSubProduct.SubProductName = Convert.ToString(dtSubProduct.Rows[i]["title"]);
                        lstSubProduct.Add(objSubProduct);
                    }
                }
                objProductAttributeValue.ListSubProduct = lstSubProduct;
                str = "select language_id,language_name from LanguageMaster where is_active=1";
                DataTable dtLanguage = DBobject.SelectData(str);
                if (dtLanguage.Rows.Count > 0)
                {
                    for (int i = 0; i < dtLanguage.Rows.Count; i++)
                    {
                        objLanguage = new LanguageDetails();
                        objLanguage.LanguageId = Convert.ToInt32(dtLanguage.Rows[i]["language_id"]);
                        objLanguage.LanguageName = Convert.ToString(dtLanguage.Rows[i]["language_name"]);
                        lstLanguage.Add(objLanguage);
                    }
                }
                objProductAttributeValue.ListLanguage = lstLanguage;

                return objProductAttributeValue;
            }
            catch (Exception ee)
            {
                return objProductAttributeValue;
            }
        }

        public int deleteProductAttributeValue(clsProductAttributeValue tmpObj)
        {
            try
            {
                string str = "update ProductAttributeValues set is_active=0 where product_attribute_value_id=" + tmpObj.ProductAttributeValuesID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
        public int EditProductAttributeValue(clsProductAttributeValue tmpObj)
        {
            try
            {
                string str = "update ProductAttributeValues set product_id=" + tmpObj.ProductID + ",sub_product_id=" + tmpObj.SubProductID + ",attribute_id=" + tmpObj.AttributeID + ",language_id=" + tmpObj.LanguageID + ",product_attribute_value='" + tmpObj.ProductAttributeValues + "',short_description='" + tmpObj.ShortDescription + "',manifesto_information='" + tmpObj.ManifestoInformation + "',last_modified_date_time=Convert(datetime,'" + DateTime.Now + "',103) where product_attribute_value_id=" + tmpObj.ProductAttributeValuesID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int AddProductAttributeValue(clsProductAttributeValue tmpObj)
        {
            try
            {
                string str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE  ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + tmpObj.SubProductID + " and ProductMaster.product_id=" + tmpObj.ProductID + " and LanguageMaster.language_id=" + tmpObj.LanguageID + " and AttributesMaster.attribute_id=" + tmpObj.AttributeID + "";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count <= 0)
                {

                    str = "insert into ProductAttributeValues(product_id,sub_product_id,attribute_id,language_id,product_attribute_value,short_description,manifesto_information,last_modified_date_time,is_active) values(" + tmpObj.ProductID + "," + tmpObj.SubProductID + "," + tmpObj.AttributeID + "," + tmpObj.LanguageID + ",'" + tmpObj.ProductAttributeValues + "','" + tmpObj.ShortDescription + "','" + tmpObj.ManifestoInformation + "',Convert(datetime,'" + DateTime.Now + "',103),1)";
                    return DBobject.IUD_Data(str);
                }
                else
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=0 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + tmpObj.SubProductID + " and ProductMaster.product_id=" + tmpObj.ProductID + " and LanguageMaster.language_id=" + tmpObj.LanguageID + " and AttributesMaster.attribute_id=" + tmpObj.AttributeID + "";
                    dt = DBobject.SelectData(str);
                    if (dt.Rows.Count > 0)
                    {
                        str = "update ProductAttributeValues set product_id=" + tmpObj.ProductID + ",sub_product_id=" + tmpObj.SubProductID + ",attribute_id=" + tmpObj.AttributeID + ",language_id=" + tmpObj.LanguageID + ",product_attribute_value='" + tmpObj.ProductAttributeValues + "',short_description='" + tmpObj.ShortDescription + "',manifesto_information='" + tmpObj.ManifestoInformation + "',last_modified_date_time=Convert(datetime,'" + DateTime.Now + "',103),is_active=1 where product_attribute_value_id=" + Convert.ToInt32(dt.Rows[0]["product_attribute_value_id"]) + "";
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

        public List<clsProductAttributeValue> getAllProductImageByCountryCodeProductID(int productID, int subProductID, int attributeID, int languageID)
        {
            List<clsProductAttributeValue> lstProductAttributeValue = new List<clsProductAttributeValue>();
            try
            {
                string str = "";
                if (productID > 0 && subProductID > 0 && attributeID > 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + subProductID + " and ProductMaster.product_id=" + productID + " and LanguageMaster.language_id=" + languageID + " and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID > 0 && subProductID > 0 && attributeID > 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + subProductID + " and ProductMaster.product_id=" + productID + "  and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID > 0 && subProductID > 0 && attributeID <= 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + subProductID + " and ProductMaster.product_id=" + productID + "";
                }
                else if (productID > 0 && subProductID <= 0 && attributeID <= 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and ProductMaster.product_id=" + productID + "";
                }


                else if (productID <= 0 && subProductID > 0 && attributeID > 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + subProductID + "  and LanguageMaster.language_id=" + languageID + " and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID <= 0 && subProductID <= 0 && attributeID > 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1  and LanguageMaster.language_id=" + languageID + " and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID <= 0 && subProductID <= 0 && attributeID <= 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1  and LanguageMaster.language_id=" + languageID + "";
                }
                else if (productID <= 0 && subProductID > 0 && attributeID <= 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + subProductID + " and LanguageMaster.language_id=" + languageID + "";
                }
                else if (productID > 0 && subProductID <= 0 && attributeID <= 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and ProductMaster.product_id=" + productID + " and LanguageMaster.language_id=" + languageID + "";
                }


                else if (productID > 0 && subProductID > 0 && attributeID <= 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and SubProductDetails.sub_product_id=" + subProductID + " and ProductMaster.product_id=" + productID + " and LanguageMaster.language_id=" + languageID + "";
                }

                else if (productID > 0 && subProductID <= 0 && attributeID > 0 && languageID > 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and  ProductMaster.product_id=" + productID + "  and LanguageMaster.language_id=" + languageID + " and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID > 0 && subProductID <= 0 && attributeID > 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and ProductMaster.product_id=" + productID + " and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID <= 0 && subProductID > 0 && attributeID <= 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1  and SubProductDetails.sub_product_id=" + subProductID + "";
                }
                else if (productID <= 0 && subProductID > 0 && attributeID > 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1  and SubProductDetails.sub_product_id=" + subProductID + " and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else if (productID <= 0 && subProductID <= 0 && attributeID > 0 && languageID <= 0)
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1 and AttributesMaster.attribute_id=" + attributeID + "";
                }
                else
                {
                    str = "select  ProductAttributeValues.product_attribute_value_id,ProductAttributeValues.product_id,ProductMaster.product_name,ProductAttributeValues.sub_product_id,SubProductDetails.title,ProductAttributeValues.attribute_id,AttributesMaster.attribute_name,ProductAttributeValues.language_id,LanguageMaster.language_name,ProductAttributeValues.product_attribute_value,ProductAttributeValues.short_description,ProductAttributeValues.manifesto_information,ProductAttributeValues.last_modified_date_time from [dbo].[ProductAttributeValues] INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeValues.product_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductAttributeValues.sub_product_id INNER JOIN LanguageMaster ON LanguageMaster.language_id=ProductAttributeValues.language_id WHERE ProductAttributeValues.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and LanguageMaster.is_active=1";
                }
                DataTable dtProductAttributeValue = DBobject.SelectData(str);
                if (dtProductAttributeValue.Rows.Count > 0)
                {
                    lstProductAttributeValue = populateProductAttributeValueList(dtProductAttributeValue);
                }
                return lstProductAttributeValue;
            }
            catch (Exception ee)
            {
                return lstProductAttributeValue;
            }
        }


    }
}