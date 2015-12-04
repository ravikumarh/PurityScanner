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
           
               ProductDetailsResponce obj= new ProductDetailsResponce();
               try
               {
                   obj.ResponseCode = "101";
                   obj.ResponseMessage = "Success";
                   obj.ResponseStatus = 1;
                   DataTable productData = getProductDetailByIDs(productRequestData.ProductIDs);
                   List<Product> lstProduct = new List<Product>();
                   for (int i = 0; i < productData.Rows.Count; i++)
                   {
                       Product projectObj = new Product();
                       projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                       projectObj.ImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                       projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                      // projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                       lstProduct.Add(projectObj);
                   }
                   obj.LstProducts = lstProduct;
               }
               catch (Exception ee)
               { 
               
               }
   

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
                DataTable productData = getProductDetailByImageKes(productDetailsRequestData.lstimageKeys);
                List<Product> lstProduct = new List<Product>();
                for (int i = 0; i < productData.Rows.Count; i++)
                {
                    Product projectObj = new Product();
                    projectObj.ProductName = Convert.ToString(productData.Rows[i]["ProductName"]);
                    projectObj.ImageUrl = Convert.ToString(productData.Rows[i]["ImageUrl"]);
                    projectObj.AttributeValueData = populateAttributeValueList(getAttribute(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                 //   projectObj.AttributeManifestoData = populateAttributeManifestoList(getAttributeManifesto(Convert.ToInt32(productData.Rows[i]["ProductID"])));
                    lstProduct.Add(projectObj);
                }
                obj.LstProducts = lstProduct;
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
                strCmd = "select AttributeTitles.attribute_title as AttributeTitle, ProductAttributeValues.manifesto_information as ManifestoInformation from ProductAttributeCountryMapping INNER JOIN AttributeTitles on AttributeTitles.attribute_id=ProductAttributeCountryMapping.attribute_id AND ProductAttributeCountryMapping.country_id=" + objManifestoRequest.CountryCode + " and AttributeTitles.language_id=" + objManifestoRequest.LanguageID + " inner join ProductAttributeValues ON ProductAttributeValues.attribute_id=ProductAttributeCountryMapping.attribute_id AND ProductAttributeValues.language_id=" + objManifestoRequest.LanguageID + "";
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
        DataTable getProductDetailByImageKes(List<ImageKeys> imageKes)
        {
            string strCmd = "";
            DataTable dt = new DataTable();
            string ids = "";

            try
            {

                for (int i = 0; i < imageKes.Count; i++)
                {
                    ids = ids + "" + imageKes[i].ImageKeysInfo + "";
                    if (i != (imageKes.Count - 1))
                    {
                        ids = ids + ",";
                    }
                }

                if (imageKes.Count > 0)
                {
                    strCmd = "Select ProductMaster.product_id as ProductID, ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from ProductMaster INNER JOIN ProductImages ON ProductMaster.product_id=ProductImages.product_id Where ProductImages.moodstock_image_key IN (" + ids + ")";
                }
                else
                {
                    strCmd = "Select ProductMaster.product_id as ProductID, ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from ProductMaster INNER JOIN ProductImages ON ProductMaster.product_id=ProductImages.product_id";
                }
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

        DataTable getAttribute(int productID)
       {
           DataTable tb = new DataTable();
           try
           {
               string strcmd = "";
               strcmd = "Select AttributeTitles.attribute_title as AttributeTitle,ProductAttributeValues.product_attribute_value as ProductAttributeValue from ProductAttributeValues INNER JOIN AttributeTitles on ProductAttributeValues.attribute_id=AttributeTitles.attribute_id Where ProductAttributeValues.product_id=" + productID + "";
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
                   lstAttributeValue.Add(tmpObj);
               }
           }
           catch (Exception ee)
           {

           }
           return lstAttributeValue;
       }

        DataTable getProductDetailByIDs(List<ProductIDs> productIDs)
       {
           string strCmd = "";
           DataTable dt = new DataTable();
           string ids = "";

           try
           {
               if (productIDs == null)
               {
                   productIDs = new List<ProductIDs>();
               }

               for (int i = 0; i < productIDs.Count; i++)
               {
                   ids = ids + "" + productIDs[i].ProductID + "";
                   if (i != (productIDs.Count - 1))
                   {
                       ids = ids + ",";
                   }
               }

                   if (productIDs.Count > 0)
                   {
                       strCmd = "Select ProductMaster.product_id as ProductID, ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from ProductMaster INNER JOIN ProductImages ON ProductMaster.product_id=ProductImages.product_id  Where ProductMaster.product_id IN (" + ids + ")";
                   }
                   else
                   {
                       strCmd = "Select ProductMaster.product_id as ProductID, ProductMaster.product_name As ProductName,ProductImages.image_url as ImageUrl from ProductMaster INNER JOIN ProductImages ON ProductMaster.product_id=ProductImages.product_id";
                   }
               SqlCommand cmd = new SqlCommand(strCmd, con);
               SqlDataAdapter da = new SqlDataAdapter(cmd);
               da.Fill(dt);
           }
           catch (Exception ee)
           { 
           
           }
           return dt;
       }
     
        DataTable getCountryMasterDetails(int countryID=0)
       {
           DataTable tb = new DataTable();
           try
           {
               string strcmd = "";
               if (countryID > 0)
               {
                   strcmd = "select country_id as CountryID,country_name as CountryName,country_default_language_id as CountryLanguageID from [dbo].[CountryMaster] where country_id=" + countryID + "";
               }
               else
               {
                   strcmd = "select country_id as CountryID,country_name as CountryName,country_default_language_id as CountryLanguageID from [dbo].[CountryMaster]";
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
                   tmpObj.CountryId= Convert.ToInt32(dtCountryMaster.Rows[i]["CountryID"]);
                   tmpObj.CountryName= Convert.ToString(dtCountryMaster.Rows[i]["CountryName"]);
                   tmpObj.CountryDefaultLanguageId = Convert.ToInt32(dtCountryMaster.Rows[i]["CountryLanguageID"]);
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
