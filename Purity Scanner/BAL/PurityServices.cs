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

        //public bool checkSubProductsById(int ProductId,int languageID)
        //{
        //    if (obj.getSubProductDetialsByProductID(ProductId, languageID).Rows.Count > 1)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        public bool checkSubProductsByImageKey(string ImageKey,int languageID)
        {
            if (obj.getSubProductDetialsByImageKey(ImageKey, languageID).Rows.Count > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public ProductDetailsResponce getAllProductsByIDs(ProductsByIDsRequest productRequestData)
        //{
        //    return obj.getAllProductsByIDs(productRequestData);
        //}

        public ProductSubProductResponse getProductSubProductDetailsByID(ProductsByIDsRequest productRequestData)
        {
            return obj.getProductSubProductDetailsByID(productRequestData);
        }

        public AllProductMetaDataResponce getAllProductsMetaData(ManifestoRequest manifestoData)
        {
            return obj.getAllProductsMetaData(manifestoData);
        }


        //public ProductDetailsResponce getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        //{
        //    return obj.getProductDetailsByImageKey(productDetailsRequestData);
        //}

        public ProductSubProductResponse getProductSubProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        {
            return obj.getProductSubProductDetailsByImageKey(productDetailsRequestData);
        }


        public ManifestoResponce GetManifesto(ManifestoRequest manifestoRequestData)
        {
            return obj.GetManifesto(manifestoRequestData);
        }
    }
}
