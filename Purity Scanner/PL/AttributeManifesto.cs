using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class AttributeManifesto
    {
        string title;
        string manifestoInformation;
        string short_description;
        string attribute_image_url;
        Boolean isCompare;

       public string Title
        {
            get { return title; }
            set { title = value; }
        }
       public Boolean IsCompare
       {
           get { return isCompare; }
           set { isCompare = value; }
       }

       public string ShortDescription
       {
           get { return short_description; }
           set { short_description = value; }
       }

       public string AttributeImageUrl
       {
           get { return attribute_image_url; }
           set { attribute_image_url = value; }
       }
       public string ManifestoInformation
        {
            get { return manifestoInformation; }
            set { manifestoInformation = value; }
        }

      
    
    }
}
