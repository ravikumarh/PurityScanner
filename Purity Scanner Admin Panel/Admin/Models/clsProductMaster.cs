using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class clsProductMaster
    {
        int product_id;
        string product_name;
        string last_modified;
        bool is_active;
        DBManage DBobject = new DBManage();
        public int productId
        {
            get { return product_id; }
            set { product_id = value; }
        }

        [Display(Name = "Product Name")]
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }

        [Display(Name = "Last Modified")]
        public string LastModified
        {
            get { return last_modified; }
            set { last_modified = value; }
        }

        [Display(Name = "Is Active")]
        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }

        public int addProduct(clsProductMaster obj)
        {
            try
            {

                string str = "Select * from ProductMaster where product_name='" + obj.ProductName + "'";
                DataTable dt = DBobject.SelectData(str);
                if (dt.Rows.Count <= 0)
                {
                    obj.IsActive = true;
                    str = "insert into ProductMaster(product_name,last_modified,is_active)values('" + obj.ProductName + "',Convert(datetime,'" + DateTime.Now.ToString("MM-dd-yyyy") + "'),'" + obj.IsActive + "')";
                    return DBobject.IUD_Data(str);
                }
                else
                {
                    str = "Select * from ProductMaster where product_name='" + obj.ProductName + "' and is_active=0";
                     dt = DBobject.SelectData(str);
                     if (dt.Rows.Count > 0)
                     {
                         str = "update ProductMaster set product_name='" + obj.ProductName + "',last_modified=Convert(datetime,'" + DateTime.Now.ToString("MM-dd-yyyy") + "'),is_active=1 where product_id=" +Convert.ToInt32(dt.Rows[0]["product_id"]) + "";
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

        public int editProduct(clsProductMaster obj)
        {
            try
            {
                string str = "update ProductMaster set product_name='" + obj.ProductName + "',last_modified=Convert(datetime,'" + DateTime.Now.ToString("MM-dd-yyyy") + "') where product_id=" + obj.productId + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public int deleteProduct(clsProductMaster obj)
        {
            try
            {
                string str = "update ProductMaster set is_active='" + obj.IsActive + "' where product_id= " + obj.productId + "";
                return DBobject.IUD_Data(str);
            }
            catch (Exception ee)
            {
                return 0;
            }
        }

        public List<clsProductMaster> getProductsById(int id = 0)
        {
            List<clsProductMaster> lstProducts = new List<clsProductMaster>();
            try
            {
                string str = "";
                if (id > 0)
                {
                    str = "Select * from ProductMaster where is_active=1 and product_id=" + id + "";
                }
                else
                {
                    str = "Select * from ProductMaster where is_active=1";
                }
                DataTable dtProducts = DBobject.SelectData(str);
                if (dtProducts.Rows.Count > 0)
                {
                    lstProducts = populateProductList(dtProducts);
                }
                return lstProducts;
            }
            catch (Exception ee)
            {
                return lstProducts;
            }

        }

        private List<clsProductMaster> populateProductList(DataTable dt)
        {
            List<clsProductMaster> lstProducts = new List<clsProductMaster>();
            try
            {
                clsProductMaster tmpObj;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tmpObj = new clsProductMaster();
                        tmpObj.productId = Convert.ToInt32(dt.Rows[i]["product_id"]);
                        tmpObj.ProductName = Convert.ToString(dt.Rows[i]["product_name"]);
                        tmpObj.LastModified = Convert.ToString(dt.Rows[i]["last_modified"]);
                        tmpObj.IsActive = (bool)dt.Rows[i]["is_active"];
                        lstProducts.Add(tmpObj);
                    }
                }
                return lstProducts;
            }
            catch (Exception ee)
            {
                return lstProducts;
            }
        }

    }
}