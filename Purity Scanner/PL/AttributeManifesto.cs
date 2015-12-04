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

       public string Title
        {
            get { return title; }
            set { title = value; }
        }

       public string ManifestoInformation
        {
            get { return manifestoInformation; }
            set { manifestoInformation = value; }
        }
    
    }
}
