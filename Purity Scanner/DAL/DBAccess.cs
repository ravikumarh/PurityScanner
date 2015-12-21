using PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace DAL
{
    public class DBAccess
    {
        // string myConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["PurityServicesConnection"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PurityServicesConnection"].ConnectionString);
        public AppMetaDataResponce getAppMetadata(string SecurityKey)
        {
            AppMetaDataResponce obj = new AppMetaDataResponce();
            obj.ResponseCode = "101";
            obj.ResponseMessage = "Success";
            obj.ResponseStatus = 1;
            obj.CountryMasterInfo = populateCountryMasterList(getCountryMasterDetails());
            obj.LanguageMasterInfo = populateLanguageMasterList(getLanguageMasterDetails());
            obj.AppMetaDataInfo = populateAppMetaDataList(getAppMetaDataDetails());
            return obj;
        }
    
        public ProductDetailsResponce getAllProductsByIDs(ProductsByIDsRequest productRequestData)
        {

            ProductDetailsResponce obj = new ProductDetailsResponce();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;
                DataTable productData = getProductDetailByIDs(productRequestData);
                List<Product> lstProduct = new List<Product>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    Product projectObj = new Product();
                    projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID));
                    // projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                    lstProduct.Add(projectObj);
                }
                if (productData.Rows.Count > 2)
                {
                    obj.MultipleProduct = true;
                }
                else
                {
                    obj.MultipleProduct = false;
                }


                obj.LstProducts = lstProduct;
            }
            catch (Exception ee)
            {

            }


            return obj;

        }




        public ProductSubProductResponse getProductSubProductDetailsByID(ProductsByIDsRequest productRequestData)
        {
            ProductSubProductResponse obj = new ProductSubProductResponse();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;
                DataTable productData = getProductDetailByIDs(productRequestData);
                List<ProductForSubProduct> lstProduct = new List<ProductForSubProduct>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    ProductForSubProduct projectObj = new ProductForSubProduct();
                    projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    List<SubProduct> tmpLstSubProduct = new List<SubProduct>();
                    List<AttributeValue> objAttributeValue = new List<AttributeValue>();
                    DataTable subProductDetails = getSubProductDetialsByProductID(Convert.ToInt32(productData.Rows[i]["ProductID"]));
                    if (subProductDetails.Rows.Count > 0)
                    {
                        for (int j = 0; j < subProductDetails.Rows.Count; j++)
                        {
                            SubProduct subProd = new SubProduct();
                            subProd.SubProductId = Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]);
                            subProd.Title = Convert.ToString(subProductDetails.Rows[j]["SubProductTitle"]);
                            subProd.AttributeValueData = populateAttributeValueList(getAttributeForSubProduct(Convert.ToInt32(productData.Rows[i]["ProductID"]), Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]), productRequestData.LanguageID));
                            tmpLstSubProduct.Add(subProd);
                        }
                    }
                    else
                    {
                       objAttributeValue = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID));
                    }
                    projectObj.AttributeValueData = objAttributeValue; 
                    projectObj.SubProducts = tmpLstSubProduct;

                        //  projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID));
                        // projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                        lstProduct.Add(projectObj);
                }
                //if (productData.Rows.Count > 2)
                //{
                    obj.MultipleProduct = true;
                //}
                //else
                //{
                //    obj.MultipleProduct = false;
                //}


                obj.LstProducts = lstProduct;
            }
            catch (Exception ee)
            {

            }


            return obj;
        }

        public AllProductMetaDataResponce getAllProductsMetaData(ManifestoRequest manifestoData)
        {
            AllProductMetaDataResponce obj = new AllProductMetaDataResponce();
            obj.ResponseCode = "101";
            obj.ResponseMessage = "Success";
            obj.ResponseStatus = 1;
            obj.LstAllProductMetaData = populateAllProductMetaData(GetAllProductDetails(manifestoData));
            return obj;
        }
        public ProductDetailsResponce getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        {
            ProductDetailsResponce obj = new ProductDetailsResponce();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;
                DataTable productData = getProductDetailByImageKes(productDetailsRequestData);
                List<Product> lstProduct = new List<Product>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    Product projectObj = new Product();
                    projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productDetailsRequestData.LanguageID));
                    //   projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                    lstProduct.Add(projectObj);
                }
                if (productData.Rows.Count > 2)
                {
                    obj.MultipleProduct = true;
                }
                else
                {
                    obj.MultipleProduct = false;
                }
                obj.LstProducts = lstProduct;
            }
            catch (Exception ee)
            {

            }


            return obj;
        }



        public ProductSubProductResponse getProductSubProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        {
            ProductSubProductResponse obj = new ProductSubProductResponse();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;
                DataTable productData = getProductDetailByImageKes(productDetailsRequestData);
                List<ProductForSubProduct> lstProduct = new List<ProductForSubProduct>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    ProductForSubProduct projectObj = new ProductForSubProduct();
                    projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    List<SubProduct> tmpLstSubProduct = new List<SubProduct>();
                    List<AttributeValue> objAttributeValue = new List<AttributeValue>();
                    DataTable subProductDetails = getSubProductDetialsByProductID(Convert.ToInt32(productData.Rows[i]["ProductID"]));
                    if (subProductDetails.Rows.Count > 0)
                    {
                        for (int j = 0; j < subProductDetails.Rows.Count; j++)
                        {
                            SubProduct subProd = new SubProduct();
                            subProd.SubProductId = Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]);
                            subProd.Title = Convert.ToString(subProductDetails.Rows[j]["SubProductTitle"]);
                            subProd.AttributeValueData = populateAttributeValueList(getAttributeForSubProduct(Convert.ToInt32(productData.Rows[i]["ProductID"]), Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]), productDetailsRequestData.LanguageID));
                            tmpLstSubProduct.Add(subProd);
                        }
                    }
                    else
                    {
                        objAttributeValue = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productDetailsRequestData.LanguageID));
                    }
                    projectObj.AttributeValueData = objAttributeValue;
                   projectObj.SubProducts = tmpLstSubProduct;
                   lstProduct.Add(projectObj);
          
                    obj.MultipleProduct = true;
                    obj.LstProducts = lstProduct;
                }
            }
            catch (Exception ee)
            {

            }


            return obj;
        }


        public ManifestoResponce GetManifesto(ManifestoRequest manifestoRequestData)
        {

            ManifestoResponce obj = new ManifestoResponce();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;


                DataTable ManifestoData = getManifestoDataByCountryCodeLanguageID(manifestoRequestData);
                Manifesto objManifesto = new Manifesto();
                objManifesto.CompanyMissionStatement = getMissionStatementByLanguageID(manifestoRequestData.LanguageID);
                List<AttributeManifesto> lstobjAttributeManifesto = new List<AttributeManifesto>();
                AttributeManifesto objAttributeManifesto;
                for (int i = 0; i < ManifestoData.Rows.Count; i++)
                {
                    objAttributeManifesto = new AttributeManifesto();
                    objAttributeManifesto.Title = Convert.ToString(ManifestoData.Rows[i]["AttributeTitle"]);
                    objAttributeManifesto.ManifestoInformation = Convert.ToString(ManifestoData.Rows[i]["ManifestoInformation"]);
                    lstobjAttributeManifesto.Add(objAttributeManifesto);
                }
                objManifesto.ManifestoAttributeData = lstobjAttributeManifesto;
                obj.ManifestoData = objManifesto;
            }
            catch (Exception ee)
            {

            }


            return obj;
        }



        DataTable getManifestoDataByCountryCodeLanguageID(ManifestoRequest objManifestoRequest)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            string ids = "";

            try
            {
                //  strCmd = "select DISTINCT AttributeTitles.attribute_title as AttributeTitle, ProductAttributeValues.manifesto_information as ManifestoInformation  from ProductAttributeCountryMapping   INNER JOIN CountryMaster ON CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND is_active=1   INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id   AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and  AttributeTitles.language_id=" + objManifestoRequest.LanguageID + "  inner join ProductAttributeValues ON ProductAttributeValues.attribute_id=ProductAttributeCountryMapping.attribute_id AND ProductAttributeValues.language_id=" + objManifestoRequest.LanguageID + "";
                strCmd = "select DISTINCT AttributeTitles.attribute_title as AttributeTitle, ProductAttributeValues.manifesto_information as  ManifestoInformation  from ProductAttributeCountryMapping  INNER JOIN CountryMaster ON CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND is_active=1  AND CountryMaster.country_default_language_id=" + objManifestoRequest.LanguageID + "  INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and  AttributeTitles.language_id=CountryMaster.country_default_language_id inner join ProductAttributeValues ON ProductAttributeValues.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeValues.language_id=CountryMaster.country_default_language_id UNION  select DISTINCT AttributeTitles.attribute_title as AttributeTitle, ProductAttributeValues.manifesto_information as  ManifestoInformation  from ProductAttributeCountryMapping  INNER JOIN CountryMaster ON CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND is_active=1  INNER JOIN SecondaryCountryLanguageMapping SL ON SL.country_code=CountryMaster.country_code AND SL.language_id=" + objManifestoRequest.LanguageID + "  INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and   AttributeTitles.language_id=SL.language_id inner join ProductAttributeValues ON ProductAttributeValues.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeValues.language_id=SL.language_id";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ee)
            {

            }
            return dt;
        }


        String getMissionStatementByLanguageID(int languageID)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            string MissionStatement = "";

            try
            {
                strCmd = "select mission_statement as MissionStatement from CompanyMissionInformation where language_id==" + languageID + "";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    MissionStatement = Convert.ToString(dt.Rows[0]["MissionStatement"]);
                }
            }
            catch (Exception ee)
            {

            }
            return MissionStatement;
        }
        DataTable getProductDetailByImageKes(ProductDetailsResquest productDetailsRequestData)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            // string ids = "";

            try
            {

                //for (int i = 0; i <productDetailsRequestData.lstimageKeys.Count; i++)
                //{
                //    ids = ids + "'" + productDetailsRequestData.lstimageKeys[i].ImageKeysInfo + "'";
                //    if (i != (productDetailsRequestData.lstimageKeys.Count - 1))
                //    {
                //        ids = ids + ",";
                //    }
                //}

                //  if (productDetailsRequestData.lstimageKeys.Count > 0)
                //  {
                strCmd = "select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl 	From CountryMaster   INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id AND ProductMaster.is_active=1 WHERE CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "'AND CountryMaster.is_active=1 AND ProductImages.moodstock_image_key='" + productDetailsRequestData.ImageKey.Trim() + "'";
                // strCmd = strCmd + " UNION Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "' AND ProductMaster.product_id=" + productDetailsRequestData.ComparingWithProductID + "";
                strCmd = strCmd + " UNION Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                if (productDetailsRequestData.ComparingWithProductID > 0)
                {
                    strCmd = strCmd + "" + productDetailsRequestData.ComparingWithProductID + ",";
                }
                strCmd = strCmd + "3)";
                //  }
                //  else
                //  {
                //      strCmd = "select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl 	From CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id AND ProductMaster.is_active=1	 WHERE 	CountryMaster.is_active=1 AND CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "'";
                //  }
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ee)
            {

            }
            return dt;
        }


        DataTable getAttributeManifesto(int productID)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
                strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ProductAttributeValues.manifesto_information as ManifestoInformation from ProductAttributeValues INNER JOIN AttributeTitles on ProductAttributeValues.attribute_id=AttributeTitles.attribute_id Where ProductAttributeValues.product_id=" + productID + "";
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }

        DataTable GetAllProductDetails(ManifestoRequest manifestoData)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
                // strcmd = "Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, ProductAttributeValues.product_attribute_value as productDetails from ProductMaster pm  INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1  INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code  INNER JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND ProductAttributeValues.language_id=" + manifestoData.LanguageID + " AND ProductAttributeValues.attribute_id=9";
                strcmd = "Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, ProductAttributeValues.product_attribute_value as productDetails from ProductMaster pm  INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 AND CountryMaster.country_default_language_id=" + manifestoData.LanguageID + " INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code  INNER JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=CountryMaster.country_default_language_id AND ProductAttributeValues.attribute_id=9  UNION  Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, ProductAttributeValues.product_attribute_value as productDetails from ProductMaster pm INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 INNER JOIN SecondaryCountryLanguageMapping SL ON SL.country_code=CountryMaster.country_code AND SL.language_id=" + manifestoData.LanguageID + "   INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code  INNER JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=SL.language_id  AND ProductAttributeValues.attribute_id=9";
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }

        List<AllProductMetaData> populateAllProductMetaData(DataTable dtlstAllproductMetaData)
        {
            List<AllProductMetaData> lstProductDetails = new List<AllProductMetaData>();

            try
            {
                AllProductMetaData tmpObj;
                for (int i = 0; i < dtlstAllproductMetaData.Rows.Count; i++)
                {
                    tmpObj = new AllProductMetaData();
                    tmpObj.ProductID = Convert.ToInt32(dtlstAllproductMetaData.Rows[i]["ProductID"]);
                    tmpObj.ProductName = Convert.ToString(dtlstAllproductMetaData.Rows[i]["ProductName"]);
                    tmpObj.ProductImageUrl = Convert.ToString(dtlstAllproductMetaData.Rows[i]["ImageUrl"]);
                    tmpObj.ProductDetails = Convert.ToString(dtlstAllproductMetaData.Rows[i]["productDetails"]);
                    lstProductDetails.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }

            return lstProductDetails;
        }

        List<AttributeManifesto> populateAttributeManifestoList(DataTable dtlstAttributeManifesto)
        {
            List<AttributeManifesto> lstAttributeValue = new List<AttributeManifesto>();
            try
            {
                AttributeManifesto tmpObj;
                for (int i = 0; i < dtlstAttributeManifesto.Rows.Count; i++)
                {
                    tmpObj = new AttributeManifesto();
                    tmpObj.Title = Convert.ToString(dtlstAttributeManifesto.Rows[i]["AttributeTitle"]);
                    tmpObj.ManifestoInformation = Convert.ToString(dtlstAttributeManifesto.Rows[i]["ManifestoInformation"]);
                    lstAttributeValue.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }
            return lstAttributeValue;
        }

        DataTable getAttribute(int productID, int languageID)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";

                //string strquery = "select * from [dbo].[ProductSubProductMapping] where product_id=" + productRequestData.ProductID + "";
                //if (getData(strquery).Rows.Count > 0)
                //{

                //}
                //else
                //{

                strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ProductAttributeValues.product_attribute_value as ProductAttributeValue,AttributesMaster.indicator as Indicator from ProductAttributeValues INNER JOIN AttributeTitles on ProductAttributeValues.attribute_id=AttributeTitles.attribute_id   AND  ProductAttributeValues.language_id=AttributeTitles.language_id INNER JOIN AttributesMaster ON  AttributesMaster.attribute_id=ProductAttributeValues.attribute_id Where ProductAttributeValues.product_id=" + productID + " AND ProductAttributeValues.language_id=" + languageID + " AND ProductAttributeValues.attribute_id not in (8,9,10,11,12,14)";
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }

        DataTable getAttributeForSubProduct(int productID, int SubProductId, int languageID)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";

                //string strquery = "select * from [dbo].[ProductSubProductMapping] where product_id=" + productRequestData.ProductID + "";
                //if (getData(strquery).Rows.Count > 0)
                //{

                //}
                //else
                //{

                strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ProductAttributeValues.product_attribute_value as ProductAttributeValue,AttributesMaster.indicator as Indicator from ProductAttributeValues INNER JOIN AttributeTitles on ProductAttributeValues.attribute_id=AttributeTitles.attribute_id   AND  ProductAttributeValues.language_id=AttributeTitles.language_id INNER JOIN AttributesMaster ON  AttributesMaster.attribute_id=ProductAttributeValues.attribute_id Where ProductAttributeValues.product_id=" + productID + " AND ProductAttributeValues.sub_product_id="+SubProductId+" AND ProductAttributeValues.language_id=" + languageID + " AND ProductAttributeValues.attribute_id not in (8,9,10,11,12,14)";
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }


        List<AttributeValue> populateAttributeValueList(DataTable dtlstAttributeValue)
        {
            List<AttributeValue> lstAttributeValue = new List<AttributeValue>();
            try
            {
                AttributeValue tmpObj;
                for (int i = 0; i < dtlstAttributeValue.Rows.Count; i++)
                {
                    tmpObj = new AttributeValue();
                    tmpObj.Title = Convert.ToString(dtlstAttributeValue.Rows[i]["AttributeTitle"]);
                    tmpObj.Values = Convert.ToString(dtlstAttributeValue.Rows[i]["ProductAttributeValue"]);
                    tmpObj.Indicator = Convert.ToString(dtlstAttributeValue.Rows[i]["Indicator"]);
                    lstAttributeValue.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }
            return lstAttributeValue;
        }


     public DataTable getSubProductDetialsByProductID(int ProductId)
        {
            DataTable dt = new DataTable();
            try
            {
                string str = "select SubProductDetails.sub_product_id as SubProductId, SubProductDetails.title as SubProductTitle from SubProductDetails INNER JOIN ProductSubProductMapping ON ProductSubProductMapping.sub_product_id=SubProductDetails.sub_product_id WHERE ProductSubProductMapping.product_id=" + ProductId + "";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ee)
            {
                return dt;
            }
        }

     public DataTable getSubProductDetialsByImageKey(string ImageKey)
     {
         DataTable dt = new DataTable();
         try
         {
             string str = " select SubProductDetails.sub_product_id as SubProductId, SubProductDetails.title as SubProductTitle from SubProductDetails INNER JOIN ProductSubProductMapping ON ProductSubProductMapping.sub_product_id=SubProductDetails.sub_product_id WHERE ProductSubProductMapping.product_id=(select DISTINCT TOP 1 product_id from ProductImages where LTRIM(RTRIM(moodstock_image_key))= '"+ImageKey.Trim()+"')";
             SqlCommand cmd = new SqlCommand(str, con);
             SqlDataAdapter da = new SqlDataAdapter(cmd);
             da.Fill(dt);
             return dt;
         }
         catch (Exception ee)
         {
             return dt;
         }
     }



        DataTable getProductDetailByIDs(ProductsByIDsRequest productRequestData)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            string ids = "";

            try
            {
                //if (productRequestData.ProductIDs == null)
                //{
                //    productRequestData.ProductIDs = new List<ProductIDs>();
                //}

                //for (int i = 0; i < productRequestData.ProductIDs.Count; i++)
                //{
                //    ids = ids + "" + productRequestData.ProductIDs[i].ProductID + "";
                //    if (i != (productRequestData.ProductIDs.Count - 1))
                //    {
                //        ids = ids + ",";
                //    }
                //}

                //  if (productRequestData.ProductIDs.Count > 0)
                //  {
                //string strquery = "select * from [dbo].[ProductSubProductMapping] where product_id=" + productRequestData.ProductID + "";
                //if (getData(strquery).Rows.Count > 0)
                //{

                //}
                //else
                //{
                    strCmd = "Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ProductID + "";
                    //strCmd = strCmd + "UNION Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ComparingWithProductID + "";
                    strCmd = strCmd + "UNION Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id IN (";

                    if (productRequestData.ComparingWithProductID > 0)
                    {
                        strCmd = strCmd + "" + productRequestData.ComparingWithProductID + ",";
                    }
                    strCmd = strCmd + "3)";
               // }

                //   }
                //   else
                //   {
                //      strCmd = "Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from  CountryMaster  INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code  INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id Where CountryMaster.country_code='" + productRequestData.CountryCode + "'";
                //  }
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ee)
            {

            }
            return dt;
        }

        DataTable getCountryMasterDetails(string countryCode = "")
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
                if (!string.IsNullOrEmpty(countryCode))
                {
                    strcmd = "select country_code as CountryCode,country_name as CountryName,country_default_language_id as CountryLanguageID,country_secondary_language_id as CountrySecondaryLanguageID from [dbo].[CountryMaster] where country_code='" + countryCode + "'";
                }
                else
                {
                    strcmd = "select country_code as CountryCode,country_name as CountryName,country_default_language_id as CountryLanguageID,country_secondary_language_id as CountrySecondaryLanguageID from [dbo].[CountryMaster]";
                }
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }

        DataTable getSecondaryLanguagesByID(string countrycode)
        {

            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
                if (!string.IsNullOrEmpty(countrycode))
                {
                    strcmd = "select sl.language_id as LanguageID,LanguageMaster.language_name as LanguageName From SecondaryCountryLanguageMapping sl INNER JOIN LanguageMaster ON LanguageMaster.language_id=sl.language_id where sl.country_code='" + countrycode + "'";
                }
                else
                {
                    strcmd = "select sl.language_id as LanguageID,LanguageMaster.language_name as LanguageName From SecondaryCountryLanguageMapping sl INNER JOIN LanguageMaster ON LanguageMaster.language_id=sl.language_id";
                }
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }
        List<CountryMaster> populateCountryMasterList(DataTable dtCountryMaster)
        {
            List<CountryMaster> lstCountryMasterDetails = new List<CountryMaster>();
            try
            {
                CountryMaster tmpObj;
                for (int i = 0; i < dtCountryMaster.Rows.Count; i++)
                {
                    tmpObj = new CountryMaster();
                    tmpObj.CountryCode = Convert.ToString(dtCountryMaster.Rows[i]["CountryCode"]);
                    tmpObj.CountryName = Convert.ToString(dtCountryMaster.Rows[i]["CountryName"]);
                    tmpObj.CountryDefaultLanguageId = Convert.ToInt32(dtCountryMaster.Rows[i]["CountryLanguageID"]);
                    // tmpObj.CountrySecondaryLanguageID = Convert.ToInt32(dtCountryMaster.Rows[i]["CountrySecondaryLanguageID"]);
                    DataTable langugaeTable = getSecondaryLanguagesByID(tmpObj.CountryCode);
                    List<SecondaryLanguagesOfCountry> lstSecLanguage = new List<SecondaryLanguagesOfCountry>();
                    SecondaryLanguagesOfCountry tmpLanguage;
                    for (int j = 0; j < langugaeTable.Rows.Count; j++)
                    {
                        tmpLanguage = new SecondaryLanguagesOfCountry();
                        tmpLanguage.SecondaryLanguageId = Convert.ToInt32(langugaeTable.Rows[j]["LanguageID"]);
                        tmpLanguage.SecondaryLanguageName = Convert.ToString(langugaeTable.Rows[j]["LanguageName"]);
                        lstSecLanguage.Add(tmpLanguage);
                    }
                    tmpObj.LstSecondaryLanguage = lstSecLanguage;

                    lstCountryMasterDetails.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }
            return lstCountryMasterDetails;
        }
        DataTable getLanguageMasterDetails(int languageID = 0)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
                if (languageID > 0)
                {
                    strcmd = "select language_id as LanguageID,language_name as LanguageName,is_active as IsActive from [dbo].[LanguageMaster] Where language_id=" + languageID + "";
                }
                else
                {
                    strcmd = "select language_id as LanguageID,language_name as LanguageName,is_active as IsActive from [dbo].[LanguageMaster]";
                }
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }
        List<LanguageMaster> populateLanguageMasterList(DataTable dtLanguageMaster)
        {
            List<LanguageMaster> lstLanguageMasterDetails = new List<LanguageMaster>();
            try
            {
                LanguageMaster tmpObj;
                for (int i = 0; i < dtLanguageMaster.Rows.Count; i++)
                {
                    tmpObj = new LanguageMaster();
                    tmpObj.LanguageId = Convert.ToInt32(dtLanguageMaster.Rows[i]["LanguageID"]);
                    tmpObj.LanguageName = Convert.ToString(dtLanguageMaster.Rows[i]["LanguageName"]);
                    tmpObj.IsActive = Convert.ToBoolean(dtLanguageMaster.Rows[i]["IsActive"]);
                    lstLanguageMasterDetails.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }
            return lstLanguageMasterDetails;
        }
        DataTable getAppMetaDataDetails(int AppMetaDataID = 0)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
                if (AppMetaDataID > 0)
                {
                    strcmd = "select app_metadata_id as appMetaDataID,app_metadata_key as appMetaDataKey,modified_date_time as modifyDate,value as Value from [dbo].[AppMetaData] where app_metadata_id=" + AppMetaDataID + "";
                }
                else
                {
                    strcmd = "select app_metadata_id as appMetaDataID,app_metadata_key as appMetaDataKey,modified_date_time as modifyDate,value as Value from [dbo].[AppMetaData]";
                }
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }
        List<AppMetaData> populateAppMetaDataList(DataTable dtAppMetaData)
        {
            List<AppMetaData> lstLanguageMasterDetails = new List<AppMetaData>();
            try
            {
                AppMetaData tmpObj;
                for (int i = 0; i < dtAppMetaData.Rows.Count; i++)
                {
                    tmpObj = new AppMetaData();
                    tmpObj.AppMetadataId = Convert.ToInt32(dtAppMetaData.Rows[i]["appMetaDataID"]);
                    tmpObj.AppMetadataKey = Convert.ToString(dtAppMetaData.Rows[i]["appMetaDataKey"]);
                    tmpObj.Value = Convert.ToString(dtAppMetaData.Rows[i]["Value"]);
                    tmpObj.ModifiedDateTime = Convert.ToDateTime(dtAppMetaData.Rows[i]["modifyDate"]);
                    lstLanguageMasterDetails.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }
            return lstLanguageMasterDetails;
        }

    }
}
