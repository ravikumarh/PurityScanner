using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class AttributeValue
    {
        string title = "";
        string values = "";
        string indicator = "";
        string attribute_image_url="";

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Values
        {
            get { return values; }
            set { values = value; }
        }
        public string Attribute_Image_Url
        {
            get {
                return attribute_image_url;
            }
            set { attribute_image_url = value; }
        }

        public string Indicator
        {
            get { return indicator; }
            set { indicator = value; }
        }
    }
}
