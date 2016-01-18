using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class SubProductDetails
    {
        int sub_product_id;
        string sub_product_name;

        public int SubProductID
        {
            get { return sub_product_id; }
            set { sub_product_id = value; }
        }

        public string SubProductName
        {
            get { return sub_product_name; }
            set { sub_product_name = value; }
        }
    }
}