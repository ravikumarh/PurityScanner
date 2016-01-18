using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DAL;
using System.ComponentModel.DataAnnotations;
namespace Admin.Models
{
    public class clsSubProduct
    {
        int Id;
        int product_id;
        int sub_product_id;
        string product_name;
        string sub_product_name;
        List<ProductDetails> lstProduct;
        List<SubProductDetails> lstSubProduct;
        DBManage DBobject = new DBManage();

        public int ID
        {
            get { return Id; }
            set { Id = value; }
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
        [Display(Name="Product Name")]
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }
         [Display(Name = "Sub Product Name")]
        public string SubProductName
        {
            get { return sub_product_name; }
            set { sub_product_name = value; }
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



        public List<clsSubProduct> getAllSubProductByID(int id = 0)
        {
            List<clsSubProduct> lstProductSubProduct = new List<clsSubProduct>();
            try
            {
                string str = "";
                if (id > 0)
                {
                    str = "select ProductSubProductMapping.id,ProductSubProductMapping.product_id,ProductSubProductMapping.sub_product_id, ProductMaster.product_name,SubProductDetails.title from ProductSubProductMapping INNER JOIN ProductMaster ON ProductSubProductMapping.product_id=ProductMaster.product_id and ProductMaster.is_active=1 INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id WHERE ProductSubProductMapping.id=" + id + "";
                }
                else
                {
                    str = "select ProductSubProductMapping.id,ProductSubProductMapping.product_id,ProductSubProductMapping.sub_product_id, ProductMaster.product_name,SubProductDetails.title from ProductSubProductMapping INNER JOIN ProductMaster ON ProductSubProductMapping.product_id=ProductMaster.product_id and ProductMaster.is_active=1 INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id";
                }
                DataTable dtSubProduct = DBobject.SelectData(str);
                if (dtSubProduct.Rows.Count > 0)
                {
                    lstProductSubProduct = populateProductSubProductList(dtSubProduct);
                }
                return lstProductSubProduct;
            }
            catch (Exception ee)
            {
                return lstProductSubProduct;
            }
        }
        private List<clsSubProduct> populateProductSubProductList(DataTable dt)
        {
            List<clsSubProduct> lstProductSubProduct = new List<clsSubProduct>();
            try
            {


                clsSubProduct tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsSubProduct();
                        tmpObj.ID = Convert.ToInt32(dt.Rows[i]["id"]);
                        tmpObj.ProductID = Convert.ToInt32(dt.Rows[i]["product_id"]);
                        tmpObj.SubProductID = Convert.ToInt32(dt.Rows[i]["sub_product_id"]);
                        tmpObj.ProductName = Convert.ToString(dt.Rows[i]["product_name"]);
                        tmpObj.SubProductName = Convert.ToString(dt.Rows[i]["title"]);
                        lstProductSubProduct.Add(tmpObj);
                    }
                }
                return lstProductSubProduct;
            }
            catch (Exception ee)
            {
                return lstProductSubProduct;
            }
        }
        public clsSubProduct addSubProductDefaultData(string countryCode = null)
        {
            clsSubProduct objSubProduct = new clsSubProduct();
            List<ProductDetails> lstProduct = new List<ProductDetails>();
            List<SubProductDetails> lstSubProduct = new List<SubProductDetails>();
            ProductDetails obj;
            SubProductDetails objSub;
            try
            {

                string str = "select product_id,product_name  from ProductMaster where is_active=1";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        obj = new ProductDetails();
                        obj.ProductID = Convert.ToInt32(dt.Rows[i]["product_id"]);
                        obj.ProductName = Convert.ToString(dt.Rows[i]["product_name"]);
                        lstProduct.Add(obj);
                    }
                }

                objSubProduct.ListProduct = lstProduct;

                str = "select sub_product_id,title  from SubProductDetails";
                dt = DBobject.SelectData(str);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objSub = new SubProductDetails();
                        objSub.SubProductID = Convert.ToInt32(dt.Rows[i]["sub_product_id"]);
                        objSub.SubProductName = Convert.ToString(dt.Rows[i]["title"]);
                        lstSubProduct.Add(objSub);
                    }
                }

                objSubProduct.ListSubProduct = lstSubProduct;

                return objSubProduct;
            }
            catch (Exception ee)
            {
                return objSubProduct;
            }
        }

        public int DeleteSubProduct(clsSubProduct obj)
        {
            try
            {
                string str = "delete from ProductSubProductMapping where id=" + obj.ID + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int EditSubProduct(clsSubProduct obj)
        {
            try
            {
                //string str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductID + "";
                //DataTable dt = DBobject.SelectData(str);
                //if (dt.Rows.Count <= 0)
                //{
                string str = "update ProductSubProductMapping set product_id=" + obj.ProductID + ",sub_product_id=" + obj.SubProductID + " WHERE id=" + obj.ID + "";
                    return DBobject.IUD_Data(str);
                //}
                //else
                //{
                //    return 0;
                //}
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int AddSubProduct(clsSubProduct obj)
        {
            try
            {
                string str = "select * from ProductSubProductMapping where product_id=" + obj.ProductID + " and sub_product_id=" + obj.SubProductID + "";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count <= 0)
                {
                    str = "insert into ProductSubProductMapping(product_id,sub_product_id)values(" + obj.ProductID + "," + obj.SubProductID + ")";
                    return DBobject.IUD_Data(str);
                }
                else
                {
                    return 2;
                }
            }
            catch (Exception ee)
            {
                return 0;
            }
        }
    }
}