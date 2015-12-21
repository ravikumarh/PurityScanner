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

        public string Indicator
        {
            get { return indicator; }
            set { indicator = value; }
        }
    }
}
