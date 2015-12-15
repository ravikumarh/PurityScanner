using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL;
using DAL;

namespace BAL
{
   public class PurityServices
    {
       DBAccess obj = new DBAccess();
       public AppMetaDataResponce getAppMetadata(string SecurityKey)
       {
           return obj.getAppMetadata(SecurityKey);
       }

       public ProductDetailsResponce getAllProductsByIDs(ProductsByIDsRequest productRequestData)
       {
           return obj.getAllProductsByIDs(productRequestData);
       }

       public AllProductMetaDataResponce getAllProductsMetaData(ManifestoRequest manifestoData)
       {
           return obj.getAllProductsMetaData(manifestoData);
       }


       public ProductDetailsResponce getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
       {
           return obj.getProductDetailsByImageKey(productDetailsRequestData);
       }

       public ManifestoResponce GetManifesto(ManifestoRequest manifestoRequestData)
       {
           return obj.GetManifesto(manifestoRequestData);
       }
    }
}
