using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
   public class ManifestoResponce
    {
        int responseStatus;
        string responseCode;
        string responseMessage;
        Manifesto manifestoData;

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

        public Manifesto ManifestoData
        {
            get { return manifestoData; }
            set { manifestoData = value; }
        }
    }
}
