using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class ProductForSubProduct
    {
        int productId;
        string productName;
        string productImageUrl;
        List<SubProduct> subproducts;
        List<AttributeValue> attributeValueData;

        public int ProductID
        {
            get { return productId; }
            set { productId = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public string ProductImageUrl
        {
            get { return productImageUrl; }
            set { productImageUrl = value; }
        }

        public List<SubProduct> SubProducts
        {
            get { return subproducts; }
            set { subproducts = value; }
        }
        public List<AttributeValue> AttributeValueData
        {
            get { return attributeValueData; }
            set { attributeValueData = value; }
        }
    }
}
