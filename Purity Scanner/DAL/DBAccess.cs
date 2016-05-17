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

        //public ProductDetailsResponce getAllProductsByIDs(ProductsByIDsRequest productRequestData)
        //{

        //    ProductDetailsResponce obj = new ProductDetailsResponce();
        //    try
        //    {
        //        obj.ResponseCode = "101";
        //        obj.ResponseMessage = "Success";
        //        obj.ResponseStatus = 1;
        //        DataTable productData = getProductDetailByIDs(productRequestData);
        //        List<Product> lstProduct = new List<Product>();
        //        for (int i = 0; i < productData.Rows.Count; i++)
        //        {
        //            Product projectObj = new Product();
        //            projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
        //            projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
        //            projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
        //            projectObj.Title = Convert.ToString(productData.Rows[i]["title"]);
        //            projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID, productRequestData.CountryCode));
        //            // projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
        //            lstProduct.Add(projectObj);
        //        }
        //        if (productData.Rows.Count > 2)
        //        {
        //            obj.MultipleProduct = true;
        //        }
        //        else
        //        {
        //            obj.MultipleProduct = false;
        //        }


        //        obj.LstProducts = lstProduct;
        //    }
        //    catch (Exception ee)
        //    {

        //    }


        //    return obj;

        //}




        public ProductSubProductResponse getProductSubProductDetailsByID(ProductsByIDsRequest productRequestData)
        {
            ProductSubProductResponse obj = new ProductSubProductResponse();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;
                DataTable productData = getProductSubDetailByIDs(productRequestData);
                List<ProductForSubProduct> lstProduct = new List<ProductForSubProduct>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    ProductForSubProduct projectObj = new ProductForSubProduct();
                    projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    List<SubProduct> tmpLstSubProduct = new List<SubProduct>();
                    List<AttributeValue> objAttributeValue = new List<AttributeValue>();
                    DataTable subProductDetails = getSubProductDetialsByProductID(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID,productRequestData.CountryCode);
                    if (subProductDetails.Rows.Count > 1)
                    {
                        for (int j = 0; j < subProductDetails.Rows.Count; j++)
                        {
                            SubProduct subProd = new SubProduct();
                            subProd.SubProductId = Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]);
                            subProd.Title = Convert.ToString(subProductDetails.Rows[j]["SubProductTitle"]);
                            subProd.SubProductImageUrl = Convert.ToString(subProductDetails.Rows[j]["SubProductImageUrl"]);
                            subProd.AttributeValueData = populateAttributeValueList(getAttributeForSubProduct(Convert.ToInt32(productData.Rows[i]["ProductID"]), Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]), productRequestData.LanguageID, productRequestData.CountryCode));
                            tmpLstSubProduct.Add(subProd);
                        }

                        if (i == 0)
                        {
                            obj.MultipleProduct = true;
                        }
                       

                    }
                    else
                    {
                        if (i == 0)
                        {
                            obj.MultipleProduct = false;
                        }
                       projectObj.Title = Convert.ToString(subProductDetails.Rows[0]["SubProductTitle"]);
                       objAttributeValue = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID, productRequestData.CountryCode, Convert.ToInt32(subProductDetails.Rows[0]["SubProductId"])));
                    }
                    projectObj.AttributeValueData = objAttributeValue; 
                    projectObj.SubProducts = tmpLstSubProduct;

                        //  projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productRequestData.LanguageID));
                        // projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                        lstProduct.Add(projectObj);
                }
                //if (productData.Rows.Count > 2)
                //{
                    
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
        //public ProductDetailsResponce getProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        //{
        //    ProductDetailsResponce obj = new ProductDetailsResponce();
        //    try
        //    {
        //        obj.ResponseCode = "101";
        //        obj.ResponseMessage = "Success";
        //        obj.ResponseStatus = 1;
        //        DataTable productData = getProductDetailByImageKes(productDetailsRequestData);
        //        List<Product> lstProduct = new List<Product>();
        //        for (int i = 0; i < productData.Rows.Count; i++)
        //        {
        //            Product projectObj = new Product();
        //            projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
        //            projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
        //            projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
        //            projectObj.Title = Convert.ToString(productData.Rows[i]["title"]);
        //            projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productDetailsRequestData.LanguageID, productDetailsRequestData.CountryCode));
        //            //   projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
        //            lstProduct.Add(projectObj);
        //        }
        //        if (productData.Rows.Count > 2)
        //        {
        //            obj.MultipleProduct = true;
        //        }
        //        else
        //        {
        //            obj.MultipleProduct = false;
        //        }
        //        obj.LstProducts = lstProduct;
        //    }
        //    catch (Exception ee)
        //    {

        //    }


        //    return obj;
        //}



        public ProductSubProductResponse getProductSubProductDetailsByImageKey(ProductDetailsResquest productDetailsRequestData)
        {
            ProductSubProductResponse obj = new ProductSubProductResponse();
            try
            {
                obj.ResponseCode = "101";
                obj.ResponseMessage = "Success";
                obj.ResponseStatus = 1;
                DataTable productData = getProductSubDetailByImageKes(productDetailsRequestData);
                List<ProductForSubProduct> lstProduct = new List<ProductForSubProduct>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    ProductForSubProduct projectObj = new ProductForSubProduct();
                    projectObj.ProductID = Convert.ToInt32(productData.Rows[i]["ProductID"]);
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ProductImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    List<SubProduct> tmpLstSubProduct = new List<SubProduct>();
                    List<AttributeValue> objAttributeValue = new List<AttributeValue>();
                    DataTable subProductDetails = getSubProductDetialsByProductID(Convert.ToInt32(productData.Rows[i]["ProductID"]), productDetailsRequestData.LanguageID, productDetailsRequestData.CountryCode);
                    if (subProductDetails.Rows.Count > 1)
                    {
                        for (int j = 0; j < subProductDetails.Rows.Count; j++)
                        {
                            SubProduct subProd = new SubProduct();
                            subProd.SubProductId = Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]);
                            subProd.Title = Convert.ToString(subProductDetails.Rows[j]["SubProductTitle"]);
                            subProd.SubProductImageUrl = Convert.ToString(subProductDetails.Rows[j]["SubProductImageUrl"]);
                            subProd.AttributeValueData = populateAttributeValueList(getAttributeForSubProduct(Convert.ToInt32(productData.Rows[i]["ProductID"]), Convert.ToInt32(subProductDetails.Rows[j]["SubProductId"]), productDetailsRequestData.LanguageID, productDetailsRequestData.CountryCode));
                            tmpLstSubProduct.Add(subProd);
                        }

                        if (i == 0)
                        {
                            obj.MultipleProduct = true;
                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            obj.MultipleProduct = false;
                        }
                        projectObj.Title = Convert.ToString(subProductDetails.Rows[0]["SubProductTitle"]);
                        objAttributeValue = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"]), productDetailsRequestData.LanguageID, productDetailsRequestData.CountryCode, Convert.ToInt32(subProductDetails.Rows[0]["SubProductId"])));
                    }
                    projectObj.AttributeValueData = objAttributeValue;
                   projectObj.SubProducts = tmpLstSubProduct;
                   lstProduct.Add(projectObj);
          
                   
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
                    objAttributeManifesto.ManifestoInformation = Convert.ToString(ManifestoData.Rows[i]["ManifestoInformation"]).Replace("\r\n","");
                    objAttributeManifesto.AttributeImageUrl = Convert.ToString(ManifestoData.Rows[i]["ImageUrl"]);
                    objAttributeManifesto.ShortDescription = Convert.ToString(ManifestoData.Rows[i]["short_description"]);
                    objAttributeManifesto.IsCompare = Convert.ToBoolean(ManifestoData.Rows[i]["isCompare"]);
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
               // strCmd = "select DISTINCT AttributeTitles.attribute_title as AttributeTitle, ProductAttributeValues.manifesto_information as  ManifestoInformation,AttributesMaster.attribute_image_url as ImageUrl,ProductAttributeValues.short_description  from ProductAttributeCountryMapping  INNER JOIN CountryMaster ON CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND CountryMaster.is_active=1  AND CountryMaster.country_default_language_id=" + objManifestoRequest.LanguageID + "  INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and  AttributeTitles.language_id=CountryMaster.country_default_language_id inner join ProductAttributeValues ON ProductAttributeValues.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeValues.language_id=CountryMaster.country_default_language_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=AttributeTitles.attribute_id  AND AttributesMaster.attribute_id NOT IN (8,9,10,14) UNION  select DISTINCT AttributeTitles.attribute_title as AttributeTitle, ProductAttributeValues.manifesto_information as  ManifestoInformation,AttributesMaster.attribute_image_url as ImageUrl,ProductAttributeValues.short_description  from ProductAttributeCountryMapping  INNER JOIN CountryMaster ON CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND CountryMaster.is_active=1  INNER JOIN SecondaryCountryLanguageMapping SL ON SL.country_code=CountryMaster.country_code AND SL.language_id=" + objManifestoRequest.LanguageID + "  INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and   AttributeTitles.language_id=SL.language_id inner join ProductAttributeValues ON ProductAttributeValues.attribute_id=ProductAttributeCountryMapping.attribute_id  AND ProductAttributeValues.language_id=SL.language_id  INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=AttributeTitles.attribute_id  AND AttributesMaster.attribute_id NOT IN (8,9,10,14)";
                strCmd = "select  AttributeTitles.attribute_title as AttributeTitle, AttributeTitles.manifesto_information as ManifestoInformation,AttributesMaster.attribute_image_url as ImageUrl,AttributeTitles.short_description,ProductAttributeCountryMapping.isCompare  from ProductAttributeCountryMapping  INNER JOIN CountryMaster ON  CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND  CountryMaster.is_active=1  AND CountryMaster.country_default_language_id=" + objManifestoRequest.LanguageID + " INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and  AttributeTitles.language_id=CountryMaster.country_default_language_id INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=AttributeTitles.attribute_id  AND   AttributesMaster.attribute_id NOT IN (8,9,10,14)  UNION select  AttributeTitles.attribute_title as AttributeTitle, AttributeTitles.manifesto_information as ManifestoInformation,AttributesMaster.attribute_image_url as ImageUrl,AttributeTitles.short_description,ProductAttributeCountryMapping.isCompare    from ProductAttributeCountryMapping  INNER JOIN CountryMaster ON  CountryMaster.country_code='" + objManifestoRequest.CountryCode + "' AND  CountryMaster.is_active=1  AND CountryMaster.country_secondary_language_id=" + objManifestoRequest.LanguageID + " INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id   AND ProductAttributeCountryMapping.country_code=CountryMaster.country_code and    AttributeTitles.language_id=CountryMaster.country_secondary_language_id   INNER JOIN AttributesMaster ON AttributesMaster.attribute_id=AttributeTitles.attribute_id  AND   AttributesMaster.attribute_id NOT IN (8,9,10,14)  Order by attribute_title";
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
        DataTable getProductSubDetailByImageKes(ProductDetailsResquest productDetailsRequestData)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            // string ids = "";

            try
            {
                //Comented for sub product image
                //strCmd = "select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl 	From CountryMaster   INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id AND ProductMaster.is_active=1 WHERE CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "'AND CountryMaster.is_active=1 AND ProductImages.moodstock_image_key='" + productDetailsRequestData.ImageKey.Trim() + "' AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
                //strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                //strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";


                strCmd = "select DISTINCT ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,(CASE WHEN stillProduct.image_url is null OR stillProduct.image_url='' THEN (CASE WHEN sparklingProduct.image_url is null OR sparklingProduct.image_url='' THEN (CASE WHEN lightSparklingProduct.image_url is null OR lightSparklingProduct.image_url='' THEN '' ELSE lightSparklingProduct.image_url  END) ELSE sparklingProduct.image_url END) ELSE stillProduct.image_url  END ) as ImageUrl 	From CountryMaster   INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id AND ProductMaster.is_active=1 LEFT JOIN ProductImages stillProduct on stillProduct.product_id=ProductMaster.product_id and  stillProduct.country_code=CountryMaster.country_code and stillProduct.sub_product_id=1  LEFT JOIN ProductImages sparklingProduct on sparklingProduct.product_id=ProductMaster.product_id and  sparklingProduct.country_code=CountryMaster.country_code and sparklingProduct.sub_product_id=2  LEFT JOIN ProductImages lightSparklingProduct on lightSparklingProduct.product_id=ProductMaster.product_id  and  lightSparklingProduct.country_code=CountryMaster.country_code and lightSparklingProduct.sub_product_id=3  WHERE CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "'AND CountryMaster.is_active=1 AND ProductImages.moodstock_image_key='" + productDetailsRequestData.ImageKey.Trim() + "' AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
                strCmd = strCmd + " UNION ALL Select DISTINCT ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,(CASE WHEN stillProduct.image_url is null OR stillProduct.image_url='' THEN (CASE WHEN sparklingProduct.image_url is null OR sparklingProduct.image_url='' THEN (CASE WHEN lightSparklingProduct.image_url is null OR lightSparklingProduct.image_url='' THEN '' ELSE lightSparklingProduct.image_url  END) ELSE sparklingProduct.image_url END) ELSE stillProduct.image_url  END ) as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id LEFT JOIN ProductImages stillProduct on stillProduct.product_id=ProductMaster.product_id and  stillProduct.country_code=CountryMaster.country_code and stillProduct.sub_product_id=1  LEFT JOIN ProductImages sparklingProduct on sparklingProduct.product_id=ProductMaster.product_id and  sparklingProduct.country_code=CountryMaster.country_code and sparklingProduct.sub_product_id=2  LEFT JOIN ProductImages lightSparklingProduct on lightSparklingProduct.product_id=ProductMaster.product_id  and  lightSparklingProduct.country_code=CountryMaster.country_code and lightSparklingProduct.sub_product_id=3   Where CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
               
                
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ee)
            {

            }
            return dt;
        }


        DataTable getProductDetailByImageKes(ProductDetailsResquest productDetailsRequestData)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            // string ids = "";

            try
            {
                //strCmd = "select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title 	From CountryMaster   INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id AND ProductMaster.is_active=1 LEFT JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id LEFT JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductAttributeValues WHERE product_id=ProductMaster.product_id  and language_id=" + productDetailsRequestData.LanguageID + ")  WHERE CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "'AND CountryMaster.is_active=1 AND ProductImages.moodstock_image_key='" + productDetailsRequestData.ImageKey.Trim() + "' AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
                //strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id LEFT JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id LEFT JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductAttributeValues WHERE product_id=1  and language_id=" + productDetailsRequestData.LanguageID + ")   Where CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                //strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
                strCmd = "select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title 	From CountryMaster   INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id AND ProductMaster.is_active=1 INNER JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductSubProductMapping WHERE product_id=ProductMaster.product_id)  WHERE CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "'AND CountryMaster.is_active=1 AND ProductImages.moodstock_image_key='" + productDetailsRequestData.ImageKey.Trim() + "' AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
                strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductSubProductMapping WHERE product_id=1)   Where CountryMaster.country_code='" + productDetailsRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productDetailsRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productDetailsRequestData.LanguageID + " and country_code='" + productDetailsRequestData.CountryCode + "')))";
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
              
                //strcmd = "Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, STUFF((SELECT ', '+p.product_attribute_value from ProductAttributeValues P WHERE p.language_id=" + manifestoData.LanguageID + " and p.attribute_id=9 and P.product_id=pm.product_id FOR XML PATH('')),1,1,'') As productDetails from ProductMaster pm  INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 AND CountryMaster.country_default_language_id=" + manifestoData.LanguageID + " INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code  INNER JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=CountryMaster.country_default_language_id AND ProductAttributeValues.attribute_id=9 group by pm.product_id,pm.product_name,ip.image_url  UNION  Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, STUFF((SELECT ', '+p.product_attribute_value from ProductAttributeValues P WHERE p.language_id=" + manifestoData.LanguageID + " and p.attribute_id=9 and P.product_id=pm.product_id FOR XML PATH('')),1,1,'') As productDetails from ProductMaster pm INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 INNER JOIN SecondaryCountryLanguageMapping SL ON SL.country_code=CountryMaster.country_code AND SL.language_id=" + manifestoData.LanguageID + "   INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code  INNER JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=SL.language_id  AND ProductAttributeValues.attribute_id=9 group by pm.product_id,pm.product_name,ip.image_url";

                //strcmd = "Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, STUFF((SELECT ', '+p.product_attribute_value from ProductAttributeValues P WHERE p.language_id=" + manifestoData.LanguageID + " and P.product_id=pm.product_id FOR XML PATH('')),1,1,'') As productDetails from ProductMaster pm  INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 AND CountryMaster.country_default_language_id=" + manifestoData.LanguageID + " INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code and ip.is_active=1 and pm.is_active=1  LEFT JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=CountryMaster.country_default_language_id and ProductAttributeValues.is_active=1 LEFT JOIN ProductSubProductMapping ON pm.product_id=ProductSubProductMapping.product_id  group by pm.product_id,pm.product_name,ip.image_url having pm.product_id NOT IN (1)  UNION  Select pm.product_id as ProductID,pm.product_name as ProductName,ip.image_url as ImageUrl, STUFF((SELECT ', '+p.product_attribute_value from ProductAttributeValues P WHERE p.language_id=" + manifestoData.LanguageID + "  and P.product_id=pm.product_id FOR XML PATH('')),1,1,'') As productDetails from ProductMaster pm INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 INNER JOIN SecondaryCountryLanguageMapping SL ON SL.country_code=CountryMaster.country_code AND SL.language_id=" + manifestoData.LanguageID + "   INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code and ip.is_active=1 and pm.is_active=1  LEFT JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=SL.language_id and ProductAttributeValues.is_active=1  LEFT JOIN ProductSubProductMapping ON pm.product_id=ProductSubProductMapping.product_id  group by pm.product_id,pm.product_name,ip.image_url having pm.product_id NOT IN (1) ORDER BY ProductName";

                strcmd = "Select pm.product_id as ProductID,pm.product_name as ProductName,(CASE WHEN stillProduct.image_url is null OR stillProduct.image_url='' THEN (CASE WHEN sparklingProduct.image_url is null OR sparklingProduct.image_url='' THEN (CASE WHEN lightSparklingProduct.image_url is null OR lightSparklingProduct.image_url=''  THEN '' ELSE lightSparklingProduct.image_url  END) ELSE sparklingProduct.image_url END)  ELSE stillProduct.image_url  END ) as ImageUrl,  STUFF((SELECT ', '+p.product_attribute_value from ProductAttributeValues P WHERE p.language_id=" + manifestoData.LanguageID + " and P.product_id=pm.product_id FOR XML PATH('')),1,1,'') As productDetails from ProductMaster pm  INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 AND CountryMaster.country_default_language_id=" + manifestoData.LanguageID + " INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code and ip.is_active=1 and pm.is_active=1  LEFT JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=CountryMaster.country_default_language_id and ProductAttributeValues.is_active=1 LEFT JOIN ProductSubProductMapping ON pm.product_id=ProductSubProductMapping.product_id  LEFT JOIN ProductImages stillProduct on stillProduct.product_id=pm.product_id and stillProduct.country_code=CountryMaster.country_code and stillProduct.sub_product_id=1 LEFT JOIN ProductImages sparklingProduct on sparklingProduct.product_id=pm.product_id and sparklingProduct.country_code=CountryMaster.country_code and sparklingProduct.sub_product_id=2 LEFT JOIN ProductImages lightSparklingProduct on lightSparklingProduct.product_id=pm.product_id and lightSparklingProduct.country_code=CountryMaster.country_code and lightSparklingProduct.sub_product_id=3 group by pm.product_id,pm.product_name,stillProduct.image_url,sparklingProduct.image_url,lightSparklingProduct.image_url having pm.product_id NOT IN (1) UNION  Select pm.product_id as ProductID,pm.product_name as ProductName,(CASE WHEN stillProduct.image_url is null OR stillProduct.image_url='' THEN (CASE WHEN sparklingProduct.image_url is null OR sparklingProduct.image_url='' THEN (CASE WHEN lightSparklingProduct.image_url is null OR lightSparklingProduct.image_url=''  THEN '' ELSE lightSparklingProduct.image_url  END) ELSE sparklingProduct.image_url END)  ELSE stillProduct.image_url  END ) as ImageUrl,  STUFF((SELECT ', '+p.product_attribute_value from ProductAttributeValues P WHERE p.language_id=" + manifestoData.LanguageID + "  and P.product_id=pm.product_id FOR XML PATH('')),1,1,'') As productDetails from ProductMaster pm INNER JOIN CountryMaster ON CountryMaster.country_code='" + manifestoData.CountryCode + "' AND CountryMaster.is_active=1 INNER JOIN SecondaryCountryLanguageMapping SL ON SL.country_code=CountryMaster.country_code AND SL.language_id=" + manifestoData.LanguageID + "   INNER JOIN ProductImages ip ON ip.product_id=pm.product_id AND ip.country_code=CountryMaster.country_code and ip.is_active=1 and pm.is_active=1  LEFT JOIN ProductAttributeValues ON ProductAttributeValues.product_id=pm.product_id AND  ProductAttributeValues.language_id=SL.language_id and ProductAttributeValues.is_active=1  LEFT JOIN ProductSubProductMapping ON pm.product_id=ProductSubProductMapping.product_id LEFT JOIN ProductImages stillProduct on stillProduct.product_id=pm.product_id and stillProduct.country_code=CountryMaster.country_code and stillProduct.sub_product_id=1 LEFT JOIN ProductImages sparklingProduct on sparklingProduct.product_id=pm.product_id and sparklingProduct.country_code=CountryMaster.country_code and sparklingProduct.sub_product_id=2 LEFT JOIN ProductImages lightSparklingProduct on lightSparklingProduct.product_id=pm.product_id and lightSparklingProduct.country_code=CountryMaster.country_code and lightSparklingProduct.sub_product_id=3 group by pm.product_id,pm.product_name,stillProduct.image_url,sparklingProduct.image_url,lightSparklingProduct.image_url having pm.product_id NOT IN (1) ORDER BY ProductName";
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

        DataTable getAttribute(int productID, int languageID,string CountryCode,int SubProductID)
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

              //  strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ProductAttributeValues.product_attribute_value as ProductAttributeValue,AttributesMaster.indicator as Indicator,AttributesMaster.attribute_image_url from ProductAttributeValues INNER JOIN AttributeTitles on ProductAttributeValues.attribute_id=AttributeTitles.attribute_id   AND  ProductAttributeValues.language_id=AttributeTitles.language_id INNER JOIN AttributesMaster ON  AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN ProductAttributeCountryMapping PACM on PACM.attribute_id=AttributesMaster.attribute_id  AND PACM.country_code='" + CountryCode + "' Where ProductAttributeValues.product_id=" + productID + " AND ProductAttributeValues.language_id=" + languageID + " AND ProductAttributeValues.attribute_id not in (8,9,10,11,12,14) and ProductAttributeValues.is_active=1";
                strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ISNULL(ProductAttributeValues.product_attribute_value,0)  as ProductAttributeValue,AttributesMaster.indicator as Indicator,AttributesMaster.attribute_image_url  from  AttributesMaster LEFT JOIN ProductAttributeValues ON AttributesMaster.attribute_id=ProductAttributeValues.attribute_id and ProductAttributeValues.product_id=" + productID + "  AND ProductAttributeValues.sub_product_id="+SubProductID+" AND ProductAttributeValues.language_id=" + languageID + "  and ProductAttributeValues.is_active=1 INNER JOIN AttributeTitles on AttributesMaster.attribute_id=AttributeTitles.attribute_id   AND   AttributeTitles.language_id =" + languageID + " INNER JOIN ProductAttributeCountryMapping PACM on PACM.attribute_id=AttributesMaster.attribute_id  AND PACM.country_code='" + CountryCode + "'   Where AttributesMaster.attribute_id not in (8,9,10,11,12,14)";
                SqlCommand cmd = new SqlCommand(strcmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(tb);
            }
            catch (Exception ee)
            {

            }
            return tb;
        }

        DataTable getAttributeForSubProduct(int productID, int SubProductId, int languageID,string CountryCode)
        {
            DataTable tb = new DataTable();
            try
            {
                string strcmd = "";
               // strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ProductAttributeValues.product_attribute_value as ProductAttributeValue,AttributesMaster.indicator as Indicator,AttributesMaster.attribute_image_url from ProductAttributeValues INNER JOIN AttributeTitles on ProductAttributeValues.attribute_id=AttributeTitles.attribute_id   AND  ProductAttributeValues.language_id=AttributeTitles.language_id INNER JOIN AttributesMaster ON  AttributesMaster.attribute_id=ProductAttributeValues.attribute_id INNER JOIN ProductAttributeCountryMapping PACM on PACM.attribute_id=AttributesMaster.attribute_id  AND PACM.country_code='" + CountryCode + "' Where ProductAttributeValues.product_id=" + productID + " AND ProductAttributeValues.sub_product_id=" + SubProductId + " AND ProductAttributeValues.language_id=" + languageID + " AND ProductAttributeValues.attribute_id not in (8,9,10,11,12,14) and ProductAttributeValues.is_active=1";
                strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ISNULL(ProductAttributeValues.product_attribute_value,0) as ProductAttributeValue,AttributesMaster.indicator as  Indicator,AttributesMaster.attribute_image_url from  AttributesMaster LEFT JOIN ProductAttributeValues ON  AttributesMaster.attribute_id=ProductAttributeValues.attribute_id and  ProductAttributeValues.product_id=" + productID + " AND ProductAttributeValues.sub_product_id=" + SubProductId + " AND   ProductAttributeValues.language_id=" + languageID + "  and ProductAttributeValues.is_active=1   INNER JOIN AttributeTitles on AttributesMaster.attribute_id=AttributeTitles.attribute_id   AND  AttributeTitles.language_id =" + languageID + "  INNER JOIN ProductAttributeCountryMapping PACM on PACM.attribute_id=AttributesMaster.attribute_id  AND PACM.country_code='" + CountryCode + "'    WHERE AttributesMaster.attribute_id not in (8,9,10,11,12,14)";
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
                    tmpObj.Attribute_Image_Url=Convert.ToString(dtlstAttributeValue.Rows[i]["attribute_image_url"]);
                    lstAttributeValue.Add(tmpObj);
                }
            }
            catch (Exception ee)
            {

            }
            return lstAttributeValue;
        }


     public DataTable getSubProductDetialsByProductID(int ProductId,int languageID,string CountryCode)
        {
            DataTable dt = new DataTable();
            try
            {
                //string str = "select SubProductDetails.sub_product_id as SubProductId, SubProductDetails.title as SubProductTitle from SubProductDetails INNER JOIN ProductSubProductMapping ON ProductSubProductMapping.sub_product_id=SubProductDetails.sub_product_id WHERE ProductSubProductMapping.product_id=" + ProductId + "  and   SubProductDetails.sub_product_id IN (select distinct sub_product_id from ProductAttributeValues WHERE product_id=" + ProductId + "  and language_id=" + languageID + ")";
                string str = "select SubProductDetails.sub_product_id as SubProductId, SubProductDetails.title as SubProductTitle ,ProductImages.image_url as SubProductImageUrl from SubProductDetails INNER JOIN ProductSubProductMapping ON ProductSubProductMapping.sub_product_id=SubProductDetails.sub_product_id INNER JOIN ProductImages on ProductImages.product_id=ProductSubProductMapping.product_id and ProductImages.sub_product_id=ProductSubProductMapping.sub_product_id  and ProductImages.country_code='" + CountryCode + "' WHERE ProductSubProductMapping.product_id=" + ProductId + "  and   SubProductDetails.sub_product_id IN (select distinct sub_product_id from ProductSubProductMapping WHERE product_id=" + ProductId + ")";
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

     public DataTable getSubProductDetialsByImageKey(string ImageKey,int languageID)
     {
         DataTable dt = new DataTable();
         try
         {
             //string str = "select SubProductDetails.sub_product_id as SubProductId, SubProductDetails.title as SubProductTitle from SubProductDetails INNER JOIN ProductSubProductMapping ON ProductSubProductMapping.sub_product_id=SubProductDetails.sub_product_id WHERE ProductSubProductMapping.product_id=(select DISTINCT TOP 1 product_id from ProductImages where LTRIM(RTRIM(moodstock_image_key))= '" + ImageKey.Trim() + "') and  SubProductDetails.sub_product_id IN (select distinct sub_product_id from ProductAttributeValues WHERE product_id=(select DISTINCT TOP 1 product_id from ProductImages where LTRIM(RTRIM(moodstock_image_key))=  '" + ImageKey.Trim() + "')   and language_id=" + languageID + ")";
             string str = "select SubProductDetails.sub_product_id as SubProductId, SubProductDetails.title as SubProductTitle from SubProductDetails INNER JOIN ProductSubProductMapping ON ProductSubProductMapping.sub_product_id=SubProductDetails.sub_product_id WHERE ProductSubProductMapping.product_id=(select DISTINCT TOP 1 product_id from ProductImages where LTRIM(RTRIM(moodstock_image_key))= '" + ImageKey.Trim() + "') and  SubProductDetails.sub_product_id IN (select distinct sub_product_id from ProductSubProductMapping WHERE product_id=(select DISTINCT TOP 1 product_id from ProductImages where LTRIM(RTRIM(moodstock_image_key))=  '" + ImageKey.Trim() + "'))";
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

                //strCmd = "Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id LEFT JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id LEFT JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductAttributeValues WHERE product_id=" + productRequestData.ProductID + "  and language_id=" + productRequestData.LanguageID + ")  Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ProductID + " AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "'))) ";
                //strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id LEFT JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id LEFT JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductAttributeValues WHERE product_id=1  and language_id=" + productRequestData.LanguageID + ")   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                //strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "'))) ";

                strCmd = "Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductSubProductMapping WHERE product_id=" + productRequestData.ProductID + ")  Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ProductID + " AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "'))) ";
                strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl,SubProductDetails.title  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id INNER JOIN ProductSubProductMapping on ProductSubProductMapping.product_id=ProductMaster.product_id INNER JOIN SubProductDetails ON SubProductDetails.sub_product_id=ProductSubProductMapping.sub_product_id and  SubProductDetails.sub_product_id IN  (select distinct sub_product_id from ProductSubProductMapping WHERE product_id=1)   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "'))) ";
                SqlCommand cmd = new SqlCommand(strCmd, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ee)
            {

            }
            return dt;
        }

        DataTable getProductSubDetailByIDs(ProductsByIDsRequest productRequestData)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            string ids = "";

            try
            {

                //strCmd = "Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ProductID + " AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "')))";
                //strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id    Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                //strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "')))";
             
                //Commeted at 06-May-2016 for sub product Image
                //strCmd = "Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ProductID + " AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "')))";
                //strCmd = strCmd + " UNION ALL Select ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id    Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                //strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "')))";

                strCmd = "Select DISTINCT ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,(CASE WHEN stillProduct.image_url is null OR stillProduct.image_url='' THEN (CASE WHEN sparklingProduct.image_url is null OR sparklingProduct.image_url='' THEN (CASE WHEN lightSparklingProduct.image_url is null OR lightSparklingProduct.image_url=''  THEN '' ELSE lightSparklingProduct.image_url  END) ELSE sparklingProduct.image_url END)  ELSE stillProduct.image_url  END ) as ImageUrl from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   LEFT JOIN ProductImages stillProduct on stillProduct.product_id=ProductMaster.product_id and  stillProduct.country_code=CountryMaster.country_code and stillProduct.sub_product_id=1  LEFT JOIN ProductImages sparklingProduct on sparklingProduct.product_id=ProductMaster.product_id and  sparklingProduct.country_code=CountryMaster.country_code and sparklingProduct.sub_product_id=2  LEFT JOIN ProductImages lightSparklingProduct on lightSparklingProduct.product_id=ProductMaster.product_id and  lightSparklingProduct.country_code=CountryMaster.country_code and lightSparklingProduct.sub_product_id=3  Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id=" + productRequestData.ProductID + " AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "')))";
                strCmd = strCmd + " UNION ALL Select DISTINCT ProductMaster.product_id as ProductID,ProductMaster.product_name As ProductName,(CASE WHEN stillProduct.image_url is null OR stillProduct.image_url='' THEN (CASE WHEN sparklingProduct.image_url is null OR sparklingProduct.image_url='' THEN (CASE WHEN lightSparklingProduct.image_url is null OR lightSparklingProduct.image_url=''  THEN '' ELSE lightSparklingProduct.image_url  END) ELSE sparklingProduct.image_url END)  ELSE stillProduct.image_url  END ) as ImageUrl  from  CountryMaster INNER JOIN ProductImages ON ProductImages.country_code=CountryMaster.country_code   INNER JOIN ProductMaster ON ProductMaster.product_id=ProductImages.product_id   LEFT JOIN ProductImages stillProduct on stillProduct.product_id=ProductMaster.product_id and  stillProduct.country_code=CountryMaster.country_code and stillProduct.sub_product_id=1  LEFT JOIN ProductImages sparklingProduct on sparklingProduct.product_id=ProductMaster.product_id and  sparklingProduct.country_code=CountryMaster.country_code and sparklingProduct.sub_product_id=2  LEFT JOIN ProductImages lightSparklingProduct on lightSparklingProduct.product_id=ProductMaster.product_id and  lightSparklingProduct.country_code=CountryMaster.country_code and lightSparklingProduct.sub_product_id=3  Where CountryMaster.country_code='" + productRequestData.CountryCode + "' AND ProductMaster.product_id IN (";
                strCmd = strCmd + "1) AND ((CountryMaster.country_default_language_id=(select language_id from LanguageMaster where language_id=" + productRequestData.LanguageID + ")) OR (1=(select COUNT(language_id) from SecondaryCountryLanguageMapping where language_id=" + productRequestData.LanguageID + " and country_code='" + productRequestData.CountryCode + "')))";
               
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
