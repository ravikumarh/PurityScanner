using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class AppMetaDataResponce
    {
        int responseStatus;
        string responseCode;
        string responseMessage;
        List<CountryMaster> lstCountryMaster;
        List<LanguageMaster> lstLanguageMaster;
        List<AppMetaData> lstAppMetaData;

        public int ResponseStatus
        {
            get { return responseStatus; }
            set { responseStatus = value; }
        }

        public string ResponseCode
        {
            get { return responseCode; }
            set { responseCode = value; }
        }

        public string ResponseMessage
        {
            get { return responseMessage; }
            set { responseMessage = value; }
        }

        public List<CountryMaster> CountryMasterInfo
        {
            get { return lstCountryMaster; }
            set { lstCountryMaster = value; }
        }

        public List<LanguageMaster> LanguageMasterInfo
        {
            get { return lstLanguageMaster; }
            set { lstLanguageMaster = value; }
        }

        public List<AppMetaData> AppMetaDataInfo
        {
            get { return lstAppMetaData; }
            set { lstAppMetaData = value; }
        }
    }
}
