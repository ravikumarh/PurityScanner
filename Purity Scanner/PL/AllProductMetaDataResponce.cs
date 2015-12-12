using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class AllProductMetaDataResponce
    {

        int responseStatus;
        string responseCode;
        string responseMessage;
        List<AllProductMetaData> lstAllProductMetaData;
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

        public List<AllProductMetaData> LstAllProductMetaData
        {
            get { return lstAllProductMetaData; }
            set { lstAllProductMetaData = value; }
        }

    }
}
