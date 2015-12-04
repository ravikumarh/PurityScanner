using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class Manifesto
    {
        string companyMissionStatement;
        List<AttributeManifesto> manifestoAttribute;

        public string CompanyMissionStatement
        {
            get { return companyMissionStatement; }
            set { companyMissionStatement = value; }
        }

        public List<AttributeManifesto> ManifestoAttributeData
        {
            get { return manifestoAttribute; }
            set { manifestoAttribute = value; }

        }
    }
}
