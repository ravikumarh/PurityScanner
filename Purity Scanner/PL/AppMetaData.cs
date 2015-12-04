using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class AppMetaData
    {
        int app_metadata_id = 0;
        string app_metadata_key = "";
        string app_metadata_value = "";
        DateTime modified_date_time;

        public int AppMetadataId
        {
            get { return app_metadata_id; }
            set { app_metadata_id = value; }
        }

        public string AppMetadataKey
        {
            get { return app_metadata_key; }
            set { app_metadata_key = value; }
        }

        public string Value
        {
            get { return app_metadata_value; }
            set { app_metadata_value = value; }
        }

         public DateTime ModifiedDateTime
        {
            get { return modified_date_time; }
            set { modified_date_time = value; }
        }
    }
}
