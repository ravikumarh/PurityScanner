using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class clsProductAttributeMappingByCountry
    {
        int product_attribute_country_mapping_id;
        int product_id;
        int attribute_id;
        string country_code;
        string attribute_name;
        string product_name;
        string country_name;
        DBManage DBobject = new DBManage();
        List<AttributeDetails> lstAttribute;
        List<ProductDetails> lstProduct;
        List<CountryDetails> lstCountry;
        int filter_product_id;
        int filter_attribute_id;
        string filter_country_code;


        public int ProductAttributeCountryMappingID
        {
            get { return product_attribute_country_mapping_id; }
            set { product_attribute_country_mapping_id = value; }
        }

        public int ProductID
        {
            get { return product_id; }
            set { product_id = value; }
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
        [Display(Name = "Product")]
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }
        [Display(Name = "Country")]
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

        public List<AttributeDetails> ListAttribute
        {
            get { return lstAttribute; }
            set { lstAttribute = value; }
        }

        public List<ProductDetails> ListProduct
        {
            get { return lstProduct; }
            set { lstProduct = value; }
        }

        public List<CountryDetails> ListCountry
        {
            get { return lstCountry; }
            set { lstCountry = value; }
        }


        public int FilterProductID
        {
            get { return filter_product_id; }
            set { filter_product_id = value; }
        }
        public int FilterAttributeID
        {
            get { return filter_attribute_id; }
            set { filter_attribute_id = value; }
        }
        public string FilterCountryCode
        {
            get { return filter_country_code; }
            set { filter_country_code = value; }
        }

        public clsProductAttributeMappingByCountry addDefaultData()
        {
            clsProductAttributeMappingByCountry objProdAttriMapping = new clsProductAttributeMappingByCountry();
            List<AttributeDetails> lstAttribute = new List<AttributeDetails>();
            List<ProductDetails> lstProduct = new List<ProductDetails>();
            List<CountryDetails> lstCountry = new List<CountryDetails>();
            AttributeDetails objAttribute;
            CountryDetails objCountry;
            ProductDetails objProduct;

            try
            {

                string str = "select country_code,country_name from CountryMaster WHERE is_active=1";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objCountry = new CountryDetails();
                        objCountry.CountryCode = Convert.ToString(dt.Rows[i]["country_code"]);
                        objCountry.CountryName = Convert.ToString(dt.Rows[i]["country_name"]);
                        lstCountry.Add(objCountry);
                    }
                }
                objProdAttriMapping.ListCountry = lstCountry;

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
                objProdAttriMapping.ListAttribute = lstAttribute;


                //str = "select product_id,product_name from ProductMaster where is_active=1";
                //DataTable dtProduct = DBobject.SelectData(str);
                //if (dtProduct.Rows.Count > 0)
                //{
                //    for (int i = 0; i < dtProduct.Rows.Count; i++)
                //    {
                //        objProduct = new ProductDetails();
                //        objProduct.ProductID = Convert.ToInt32(dtProduct.Rows[i]["product_id"]);
                //        objProduct.ProductName = Convert.ToString(dtProduct.Rows[i]["product_name"]);
                //        lstProduct.Add(objProduct);
                //    }
                //}
                objProdAttriMapping.ListProduct = lstProduct;

                return objProdAttriMapping;
            }
            catch (Exception ee)
            {
                return objProdAttriMapping;
            }
        }


        public List<clsProductAttributeMappingByCountry> getAllProdAttriMappingByID(int prodAttriMappingID = 0)
        {
            List<clsProductAttributeMappingByCountry> lstProdAttriMapping = new List<clsProductAttributeMappingByCountry>();
            try
            {
                string str = "";
                if (prodAttriMappingID > 0)
                {
                    //str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,ProductAttributeCountryMapping.product_id,CountryMaster.country_name, AttributesMaster.attribute_name,ProductMaster.product_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeCountryMapping.product_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and CountryMaster.is_active=1 and ProductAttributeCountryMapping.product_attribute_country_mapping_id=" + prodAttriMappingID + " Order by CountryMaster.country_name,ProductMaster.product_name,AttributesMaster.attribute_name";
                    str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id,ProductAttributeCountryMapping.country_code,CountryMaster.country_name, AttributesMaster.attribute_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1  and AttributesMaster.isActive=1  and CountryMaster.is_active=1 and ProductAttributeCountryMapping.product_attribute_country_mapping_id=" + prodAttriMappingID + " Order by CountryMaster.country_name,AttributesMaster.attribute_name";
                }
                else
                {
                   // str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,ProductAttributeCountryMapping.product_id,CountryMaster.country_name, AttributesMaster.attribute_name,ProductMaster.product_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeCountryMapping.product_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and CountryMaster.is_active=1 Order by CountryMaster.country_name,ProductMaster.product_name,AttributesMaster.attribute_name";
                    str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id,ProductAttributeCountryMapping.country_code,CountryMaster.country_name, AttributesMaster.attribute_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1  and AttributesMaster.isActive=1  and CountryMaster.is_active=1 Order by CountryMaster.country_name,AttributesMaster.attribute_name";
                }
                DataTable dtProdAttriMapping = DBobject.SelectData(str);
                if (dtProdAttriMapping.Rows.Count > 0)
                {
                    lstProdAttriMapping = populateProdAttriMappingList(dtProdAttriMapping);
                }
                return lstProdAttriMapping;
            }
            catch (Exception ee)
            {
                return lstProdAttriMapping;
            }
        }

        public List<clsProductAttributeMappingByCountry> getAllProdAttriMappingByCountryIDProductID(string countryCode)
        {
            List<clsProductAttributeMappingByCountry> lstProdAttriMapping = new List<clsProductAttributeMappingByCountry>();
            try
            {
                string str = "";
                //if (ProductId > 0 && !string.IsNullOrEmpty(countryCode))
                //{
                //    str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,ProductAttributeCountryMapping.product_id,CountryMaster.country_name, AttributesMaster.attribute_name,ProductMaster.product_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeCountryMapping.product_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and CountryMaster.is_active=1 and  ProductMaster.product_id=" + ProductId + " and CountryMaster.country_code='" + countryCode.Trim() + "' Order by CountryMaster.country_name,ProductMaster.product_name,AttributesMaster.attribute_name";
                //}
                //else if (ProductId > 0 && string.IsNullOrEmpty(countryCode))
                //{
                //    str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,ProductAttributeCountryMapping.product_id,CountryMaster.country_name, AttributesMaster.attribute_name,ProductMaster.product_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeCountryMapping.product_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and CountryMaster.is_active=1 and  ProductMaster.product_id=" + ProductId + "  Order by CountryMaster.country_name,ProductMaster.product_name,AttributesMaster.attribute_name";
                //}
                //else 
                if (!string.IsNullOrEmpty(countryCode))
                {
                    //str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,ProductAttributeCountryMapping.product_id,CountryMaster.country_name, AttributesMaster.attribute_name,ProductMaster.product_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeCountryMapping.product_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and CountryMaster.is_active=1 and CountryMaster.country_code='" + countryCode.Trim() + "' Order by CountryMaster.country_name,ProductMaster.product_name,AttributesMaster.attribute_name";
                    str = "Select  ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,CountryMaster.country_name, AttributesMaster.attribute_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and  AttributesMaster.isActive=1 and CountryMaster.is_active=1 and CountryMaster.country_code='" + countryCode.Trim() + "' Order by CountryMaster.country_name,AttributesMaster.attribute_name ";
                }
                else
                {
                   // str = "Select ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,ProductAttributeCountryMapping.product_id,CountryMaster.country_name, AttributesMaster.attribute_name,ProductMaster.product_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductAttributeCountryMapping.product_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and ProductMaster.is_active=1 and AttributesMaster.isActive=1 and CountryMaster.is_active=1 Order by CountryMaster.country_name,ProductMaster.product_name,AttributesMaster.attribute_name";
                    str = "Select  ProductAttributeCountryMapping.product_attribute_country_mapping_id,ProductAttributeCountryMapping.attribute_id, ProductAttributeCountryMapping.country_code,CountryMaster.country_name, AttributesMaster.attribute_name from ProductAttributeCountryMapping INNER JOIN CountryMaster ON ProductAttributeCountryMapping.country_code=CountryMaster.country_code INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=ProductAttributeCountryMapping.attribute_id WHERE ProductAttributeCountryMapping.is_active=1 and  AttributesMaster.isActive=1 and CountryMaster.is_active=1 Order by CountryMaster.country_name,AttributesMaster.attribute_name";
                }
                DataTable dtProdAttriMapping = DBobject.SelectData(str);
                if (dtProdAttriMapping.Rows.Count > 0)
                {
                    lstProdAttriMapping = populateProdAttriMappingList(dtProdAttriMapping);
                }
                return lstProdAttriMapping;
            }
            catch (Exception ee)
            {
                return lstProdAttriMapping;
            }
        }



        private List<clsProductAttributeMappingByCountry> populateProdAttriMappingList(DataTable dt)
        {
            List<clsProductAttributeMappingByCountry> lstAttributeTitle = new List<clsProductAttributeMappingByCountry>();
            try
            {
                clsProductAttributeMappingByCountry tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsProductAttributeMappingByCountry();
                        tmpObj.ProductAttributeCountryMappingID = Convert.ToInt32(dt.Rows[i]["product_attribute_country_mapping_id"]);
                        tmpObj.AttributeID = Convert.ToInt32(dt.Rows[i]["attribute_id"]);
                        tmpObj.AttributeName = Convert.ToString(dt.Rows[i]["attribute_name"]);
                        //tmpObj.ProductID = Convert.ToInt32(dt.Rows[i]["product_id"]);
                        //tmpObj.ProductName = Convert.ToString(dt.Rows[i]["product_name"]);
                        tmpObj.CountryCode = Convert.ToString(dt.Rows[i]["country_code"]);
                        tmpObj.CountryName = Convert.ToString(dt.Rows[i]["country_name"]);

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

        public int deleteProdAttriCountryMapping(clsProductAttributeMappingByCountry obj)
        {
            try
            {
                string str = "update ProductAttributeCountryMapping set is_active=0 where product_attribute_country_mapping_id=" + obj.ProductAttributeCountryMappingID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int editProdAttriCountryMapping(clsProductAttributeMappingByCountry obj)
        {
            try
            {
                string str = "update ProductAttributeCountryMapping set attribute_id=" + obj.AttributeID + ",country_code='" + obj.CountryCode + "' where product_attribute_country_mapping_id=" + obj.ProductAttributeCountryMappingID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int addProdAttriCountryMapping(clsProductAttributeMappingByCountry obj)
        {
            try
            {
                string str = "select * from [dbo].[ProductAttributeCountryMapping] where attribute_id=" + obj.AttributeID + " and country_code='" + obj.CountryCode + "'";
                DataTable dtProdAttriMapping = DBobject.SelectData(str);
                if (dtProdAttriMapping.Rows.Count <= 0)
                {

                    str = "insert into [dbo].[ProductAttributeCountryMapping](attribute_id,country_code,is_active)values(" + obj.AttributeID + ",'" + obj.CountryCode + "',1)";
                    return DBobject.IUD_Data(str);
                }
                else
                {
                    str = "select * from [dbo].[ProductAttributeCountryMapping] where  attribute_id=" + obj.AttributeID + " and country_code='" + obj.CountryCode + "' and is_active=0";
                    dtProdAttriMapping = DBobject.SelectData(str);
                    if (dtProdAttriMapping.Rows.Count > 0)
                    {
                        str = "update ProductAttributeCountryMapping set  attribute_id=" + obj.AttributeID + ",country_code='" + obj.CountryCode + "', is_active=1 where product_attribute_country_mapping_id=" + Convert.ToInt32(dtProdAttriMapping.Rows[0]["product_attribute_country_mapping_id"]) + "";
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