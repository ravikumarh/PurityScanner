using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class LanguageMaster
    {

        int language_id = 0;
        string language_name = "";
        bool is_active = true;

       public int LanguageId
        {
            get { return language_id; }
            set { language_id = value; }
        }

        public string LanguageName
        {
            get { return language_name; }
            set { language_name = value; }
        }

        public bool IsActive
        {
            get { return is_active; }
            set { is_active = value; }
        }
    }
}
