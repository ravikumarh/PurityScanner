using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class clsProductImage
    {
        int product_image_id;
        int product_id;
        int sub_product_id;
        string sub_product_name;
        string product_name;
        string country_code;
        string country_name;
        string product_image_url;
        string moodstock_image_key;
        List<CountryDetails> lstCountry;
        List<ProductDetails> lstProduct;
        List<SubProductDetails> lstSubProduct;
        DBManage DBobject = new DBManage();
        int filter_product_id;
        int filter_sub_product_id;
        int filter_attribute_id;
        string filter_country_code;
        public int ProductImageID
        {
            get { return product_image_id; }
            set { product_image_id = value; }
        }
        public int ProductID
        {
            get { return product_id; }
            set { product_id = value; }
        }
        public int SubProductId
        {
            get { return sub_product_id; }
            set { sub_product_id = value; }
        }
        [Display(Name = "Sub Product")]
        public string SubProductName
        {
            get { return sub_product_name; }
            set { sub_product_name = value; }
        }


        [Display(Name = "Product")]
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }
        public string CountryCode
        {
            get { return country_code; }
            set { country_code = value; }
        }
        [Display(Name = "Country")]
        public string CountryName
        {
            get { return country_name; }
            set { country_name = value; }
        }
        [Display(Name = "Image")]
        public string ProductImageUrl
        {
            get { return product_image_url; }
            set { product_image_url = value; }
        }
        [Display(Name = "Image Key")]
        public string MoodstockImageKey
        {
            get { return moodstock_image_key; }
            set { moodstock_image_key = value; }
        }
        public List<ProductDetails> ListProduct
        {
            get { return lstProduct; }
            set { lstProduct = value; }
        }
        public List<SubProductDetails> ListSubProduct
        {
            get { return lstSubProduct; }
            set { lstSubProduct = value; }
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
        public int FilterSubProductId
        {
            get { return filter_sub_product_id; }
            set { filter_sub_product_id = value; }
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
        public clsProductImage addProductImageDefaultData()
        {
            clsProductImage objProductImage = new clsProductImage();
            List<CountryDetails> lstCountry = new List<CountryDetails>();
            List<ProductDetails> lstProduct = new List<ProductDetails>();
            List<SubProductDetails> lstSubProduct = new List<SubProductDetails>();
            CountryDetails obj;
            ProductDetails objProduct;
            SubProductDetails objSubProduct;

            try
            {
                string str = "select country_code,country_name from CountryMaster WHERE is_active=1";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        obj = new CountryDetails();
                        obj.CountryCode = Convert.ToString(dt.Rows[i]["country_code"]);
                        obj.CountryName = Convert.ToString(dt.Rows[i]["country_name"]);
                        lstCountry.Add(obj);
                    }
                }
                objProductImage.ListCountry = lstCountry;

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
                objProductImage.ListProduct = lstProduct;

                str = "select sub_product_id, title as sub_product_name from SubProductDetails";
                DataTable dtSubProduct = DBobject.SelectData(str);
                if (dtSubProduct.Rows.Count > 0)
                {
                    for (int i = 0; i < dtSubProduct.Rows.Count; i++)
                    {
                        objSubProduct = new SubProductDetails();
                        objSubProduct.SubProductID = Convert.ToInt32(dtSubProduct.Rows[i]["sub_product_id"]);
                        objSubProduct.SubProductName = Convert.ToString(dtSubProduct.Rows[i]["sub_product_name"]);
                        lstSubProduct.Add(objSubProduct);
                    }
                }
                objProductImage.ListSubProduct = lstSubProduct;
                return objProductImage;
            }
            catch (Exception ee)
            {
                return objProductImage;
            }
        }
        public List<clsProductImage> getAllProductImageByID(int productImageID = 0)
        {
            List<clsProductImage> lstProductImage = new List<clsProductImage>();
            try
            {
                string str = "";
                if (productImageID > 0)
                {
                    //str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.country_code, CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and  ProductImages.product_image_id=" + productImageID + "";

                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName,ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and  ProductImages.product_image_id=" + productImageID + "";
                }

                else
                {
                    //str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.country_code, CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1";

                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName,ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1";
                }

                DataTable dtProductImage = DBobject.SelectData(str);
                if (dtProductImage.Rows.Count > 0)
                {
                    lstProductImage = populateProductImageList(dtProductImage);
                }
                return lstProductImage;
            }
            catch (Exception ee)
            {
                return lstProductImage;
            }
        }
        public List<clsProductImage> getAllProductImageByCountryCodeProductID(int productID, int subproductID, string countryCode)
        {
            List<clsProductImage> lstProductImage = new List<clsProductImage>();
            try
            {
                string str = "";
                if (productID > 0 && subproductID > 0 && !string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and ProductMaster.product_id=" + productID + " and SubProductDetails.sub_product_id=" + subproductID + " and CountryMaster.country_code='" + countryCode + "'";
                }
                else if (productID > 0 && subproductID <= 0 && string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and ProductMaster.product_id=" + productID + "";
                }
                else if (productID <= 0 && subproductID <= 0 && !string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and CountryMaster.country_code='" + countryCode + "'";
                }
                else if (productID <= 0 && subproductID > 0 && !string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and SubProductDetails.sub_product_id=" + subproductID + " and CountryMaster.country_code='" + countryCode + "'";
                }
                else if (productID > 0 && subproductID > 0 && string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and ProductMaster.product_id=" + productID + " and SubProductDetails.sub_product_id=" + subproductID + " ";
                }
                else if (productID > 0 && subproductID <= 0 && string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and ProductMaster.product_id=" + productID + "";
                }

                else if (productID <= 0 && subproductID > 0 && string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and  SubProductDetails.sub_product_id=" + subproductID + " ";
                }
                else if (productID > 0 && subproductID <= 0 && !string.IsNullOrEmpty(countryCode))
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as SubProductName, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1 and  ProductMaster.product_id=" + productID + " and CountryMaster.country_code='" + countryCode + "'";
                }
                //else
                //{
                //    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.country_code, CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1";
                //}
                else
                {
                    str = "select ProductImages.product_image_id,ProductImages.product_id,ProductMaster.product_name,ProductImages.sub_product_id,SubProductDetails.title as sub_product_name, ProductImages.country_code,CountryMaster.country_name,ProductImages.image_url,ProductImages.moodstock_image_key from ProductImages INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN CountryMaster ON CountryMaster.country_code=ProductImages.country_code LEFT JOIN SubProductDetails ON ProductImages.sub_product_id = SubProductDetails.sub_product_id WHERE CountryMaster.is_active=1 and ProductMaster.is_active=1 and ProductImages.is_active=1";
                }

                DataTable dtProductImage = DBobject.SelectData(str);
                if (dtProductImage.Rows.Count > 0)
                {
                    lstProductImage = populateProductImageList(dtProductImage);
                }
                return lstProductImage;
            }
            catch (Exception ee)
            {
                return lstProductImage;
            }
        }
        private List<clsProductImage> populateProductImageList(DataTable dt)
        {
            List<clsProductImage> lstProductImage = new List<clsProductImage>();
            try
            {
                clsProductImage tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsProductImage();
                        tmpObj.ProductImageID = Convert.ToInt32(dt.Rows[i]["product_image_id"]);
                        tmpObj.ProductName = Convert.ToString(dt.Rows[i]["product_name"]);
                        tmpObj.ProductID = Convert.ToInt32(dt.Rows[i]["product_id"]);
                        tmpObj.SubProductName = Convert.ToString(dt.Rows[i]["SubProductName"]);
                        if (dt.Rows[i]["sub_product_id"] != System.DBNull.Value)
                        {
                            tmpObj.SubProductId = Convert.ToInt32(dt.Rows[i]["sub_product_id"]);
                        }
                        else
                        {
                            tmpObj.SubProductId = 0;
                        }
                        tmpObj.CountryCode = Convert.ToString(dt.Rows[i]["country_code"]);
                        tmpObj.CountryName = Convert.ToString(dt.Rows[i]["country_name"]);
                        tmpObj.ProductImageUrl = Convert.ToString(dt.Rows[i]["image_url"]);
                        tmpObj.MoodstockImageKey = Convert.ToString(dt.Rows[i]["moodstock_image_key"]);
                        lstProductImage.Add(tmpObj);
                    }
                }
                return lstProductImage;
            }
            catch (Exception ee)
            {
                return lstProductImage;
            }
        }
        public int deleteProductImage(clsProductImage obj)
        {
            try
            {
                string str = "update ProductImages set is_active=0 where product_image_id=" + obj.ProductImageID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
        public int editProductImage(clsProductImage obj)
        {
            try
            {
                if (!string.IsNullOrEmpty(obj.ProductImageUrl))
                {
                    string str = "select * from ProductImages where  moodstock_image_key='" + obj.MoodstockImageKey + "' and country_code='" + obj.CountryCode + "' and is_active=1";
                    DataTable dtProductImage = DBobject.SelectData(str);
                    if (dtProductImage.Rows.Count <= 0)
                    {
                        str = "select * from ProductImages where product_id=" + obj.ProductID + " and country_code='" + obj.CountryCode + "' AND sub_product_id=" + obj.SubProductId + "";
                        dtProductImage = DBobject.SelectData(str);
                        if (dtProductImage.Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dtProductImage.Rows[0]["product_image_id"]) == obj.product_image_id)
                            {
                                str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                                dtProductImage = DBobject.SelectData(str);
                                if (dtProductImage.Rows.Count <= 0)
                                {
                                    str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                    DBobject.IUD_Data(str);
                                }
                                str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',image_url='" + obj.ProductImageUrl + "',moodstock_image_key='" + obj.MoodstockImageKey + "' where product_image_id=" + obj.ProductImageID + "";
                                return DBobject.IUD_Data(str);
                            }
                            else
                            {
                                return 2;
                            }
                        }
                        else
                        {
                            str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                            dtProductImage = DBobject.SelectData(str);
                            if (dtProductImage.Rows.Count <= 0)
                            {
                                str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                DBobject.IUD_Data(str);
                            }
                            str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',image_url='" + obj.ProductImageUrl + "',moodstock_image_key='" + obj.MoodstockImageKey + "' where product_image_id=" + obj.ProductImageID + "";
                            return DBobject.IUD_Data(str);
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dtProductImage.Rows[0]["product_image_id"]) == obj.product_image_id)
                        {
                            str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                            dtProductImage = DBobject.SelectData(str);
                            if (dtProductImage.Rows.Count <= 0)
                            {
                                str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                DBobject.IUD_Data(str);
                            }
                            str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',image_url='" + obj.ProductImageUrl + "',moodstock_image_key='" + obj.MoodstockImageKey + "' where product_image_id=" + obj.ProductImageID + "";
                            return DBobject.IUD_Data(str);
                        }
                        else
                        {
                            return 3;
                        }
                    }
                }
                else
                {
                    string str = "select * from ProductImages where  moodstock_image_key='" + obj.MoodstockImageKey + "' and country_code='" + obj.CountryCode + "' and is_active=1";
                    DataTable dtProductImage = DBobject.SelectData(str);
                    if (dtProductImage.Rows.Count <= 0)
                    {
                        str = "select * from ProductImages where product_id=" + obj.ProductID + " and country_code='" + obj.CountryCode + "' AND sub_product_id=" + obj.SubProductId + "";
                        dtProductImage = DBobject.SelectData(str);
                        if (dtProductImage.Rows.Count <= 0)
                        {
                            str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                            dtProductImage = DBobject.SelectData(str);
                            if (dtProductImage.Rows.Count <= 0)
                            {
                                str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                DBobject.IUD_Data(str);
                            }
                            str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',moodstock_image_key='" + obj.MoodstockImageKey + "' where product_image_id=" + obj.ProductImageID + "";
                            return DBobject.IUD_Data(str);
                        }
                        else
                        {
                            if (Convert.ToInt32(dtProductImage.Rows[0]["product_image_id"]) == obj.product_image_id)
                            {
                                str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                                dtProductImage = DBobject.SelectData(str);
                                if (dtProductImage.Rows.Count <= 0)
                                {
                                    str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                    DBobject.IUD_Data(str);
                                }
                                str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',moodstock_image_key='" + obj.MoodstockImageKey + "' where product_image_id=" + obj.ProductImageID + "";
                                return DBobject.IUD_Data(str);
                            }
                            else
                            {
                                return 2;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(dtProductImage.Rows[0]["product_image_id"]) == obj.product_image_id)
                        {
                            str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                            dtProductImage = DBobject.SelectData(str);
                            if (dtProductImage.Rows.Count <= 0)
                            {
                                str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                DBobject.IUD_Data(str);
                            }
                            str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',moodstock_image_key='" + obj.MoodstockImageKey + "' where product_image_id=" + obj.ProductImageID + "";
                            return DBobject.IUD_Data(str);
                        }
                        else
                        {
                            return 3;
                        }
                    }
                }

            }
            catch (Exception ee)
            {
                return 0;
            }
        }
        public int addProductImage(clsProductImage obj)
        {
            try
            {
                string str = "select * from ProductImages where  moodstock_image_key='" + obj.MoodstockImageKey + "' and country_code='" + obj.CountryCode + "' and is_active=1";
                DataTable dtProductImage = DBobject.SelectData(str);
                if (dtProductImage.Rows.Count <= 0)
                {

                    str = "select * from ProductImages where product_id=" + obj.ProductID + "  and country_code='" + obj.CountryCode + "' AND sub_product_id=" + obj.SubProductId + "";
                    dtProductImage = DBobject.SelectData(str);
                    if (dtProductImage.Rows.Count <= 0)
                    {
                        str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                        dtProductImage = DBobject.SelectData(str);
                        if (dtProductImage.Rows.Count <= 0)
                        {
                            str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                            DBobject.IUD_Data(str);
                        }

                        str = "insert into ProductImages(product_id, sub_product_id, country_code,image_url, moodstock_image_key, is_active)values(" + obj.ProductID + ",'" + obj.SubProductId + "','" + obj.CountryCode + "','" + obj.ProductImageUrl + "','" + obj.MoodstockImageKey + "',1)";
                        return DBobject.IUD_Data(str);
                    }
                    else
                    {
                        str = "select * from ProductImages where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "  and country_code='" + obj.CountryCode + "' and is_active=0";
                        dtProductImage = DBobject.SelectData(str);
                        if (dtProductImage.Rows.Count > 0)
                        {
                            int imageProductId = Convert.ToInt32(dtProductImage.Rows[0]["product_image_id"]);
                            str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductId + "";
                            dtProductImage = DBobject.SelectData(str);
                            if (dtProductImage.Rows.Count <= 0)
                            {
                                str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductId + ")";
                                DBobject.IUD_Data(str);
                            }
                            str = "update ProductImages set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductId + ",country_code='" + obj.CountryCode + "',image_url='" + obj.ProductImageUrl + "',moodstock_image_key='" + obj.MoodstockImageKey + "', is_active=1 where product_image_id=" + imageProductId + "";
                            return DBobject.IUD_Data(str);
                        }
                        else
                        {
                            return 2;
                        }
                    }
                }
                else
                {
                    return 3;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
    }
}