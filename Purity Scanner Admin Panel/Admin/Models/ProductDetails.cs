using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class ProductDetails
    {
        int product_id;
        string product_name;

        public int ProductID
        {
            get { return product_id; }
            set { product_id = value; }
        }
        public string ProductName
        {
            get { return product_name; }
            set { product_name = value; }
        }

        public string SubProductName { get; set; }
    }
}