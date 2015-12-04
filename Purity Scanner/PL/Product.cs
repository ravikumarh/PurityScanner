using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
  public  class Product
    {
        string productName;
        string imageUrl;
        List<AttributeValue> attributeValues;
       // List<AttributeManifesto> attributeManifestos;

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; }
        }
        //public List<AttributeManifesto> AttributeManifestoData
        //{
        //    get { return attributeManifestos; }
        //    set { attributeManifestos = value; }
        //}

        public List<AttributeValue> AttributeValueData
        {
            get { return attributeValues; }
            set { attributeValues = value; }
        }

    }
}
