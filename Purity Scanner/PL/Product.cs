using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
  public  class Product
    {
        int productId;
        string productName;
        string productImageUrl;
        string productType; 
        List<AttributeValue> attributeValues;
       // List<AttributeManifesto> attributeManifestos;


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
        //public List<AttributeManifesto> AttributeManifestoData
        //{
        //    get { return attributeManifestos; }
        //    set { attributeManifestos = value; }
        //}
        public string Title
        {
            get {return  productType; }
            set { productType = value; }
        }

        public List<AttributeValue> AttributeValueData
        {
            get { return attributeValues; }
            set { attributeValues = value; }
        }

    }
}
