using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models
{
    public class AttributeDetails
    {
        int attribute_id;
        string attribute_name;
        public int AttributeId
        {
            get { return attribute_id; }
            set { attribute_id = value; }
        }

        public string AttributeName
        {
            get { return attribute_name; }
            set { attribute_name = value; }
        }
    }
}