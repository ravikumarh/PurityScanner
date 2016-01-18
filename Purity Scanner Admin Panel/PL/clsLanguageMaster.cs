using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
  public class clsLanguageMaster
    {
      int language_id;
      string languagae_name;
      bool is_active;

    
      public int LanguageId
      {
          get { return language_id; }
          set { language_id = value; }
      }

      public string LanguageName
      {
          get { return languagae_name; }
          set { languagae_name = value; }
      }
      public bool IsActive
      {
          get { return is_active; }
          set { is_active = value; }
      }
    }
}
