using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ProductAttributeValuesController : Controller
    {
        clsProductAttributeValue obj = new clsProductAttributeValue();
        clsProductAttributeValue objControl;
         [Authorize]
        public ActionResult ListProductAttributeValues()
        {
            try
            {
                ViewData["Message"] = null;
                objControl = (clsProductAttributeValue)TempData["tmpClassObj"];
                clsProductAttributeValue tmpObj;
                List<clsProductAttributeValue> lstProductAttributeValue;//= obj.getAllProductAttributeValueByID();
                if (objControl != null)
                {
                    if (objControl.FilterAttributeID > 0 || objControl.FilterProductID > 0 || objControl.FilterLanguageID > 0 || objControl.FilterSubProductID > 0)
                    {
                        lstProductAttributeValue = obj.getAllProductImageByCountryCodeProductID(objControl.FilterProductID, objControl.FilterSubProductID, objControl.FilterAttributeID, objControl.FilterLanguageID);
                    }
                    else
                    {
                        lstProductAttributeValue = obj.getAllProductAttributeValueByID();
                    }
                }
                else
                {
                    lstProductAttributeValue = obj.getAllProductAttributeValueByID();
                }
                tmpObj = obj.addProductAttributeValueDefaultData();
                ViewBag.lstAttribute = new SelectList(tmpObj.ListAttribute, "AttributeID", "AttributeName");
                ViewBag.lstProduct = new SelectList(tmpObj.ListProduct, "ProductID", "ProductName");
                ViewBag.lstSubProduct = new SelectList(tmpObj.ListSubProduct, "SubProductID", "SubProductName");
                ViewBag.lstLanguage = new SelectList(tmpObj.ListLanguage, "LanguageID", "LanguageName");
                if (objControl != null)
                {
                    tmpObj.FilterProductID = objControl.FilterProductID;
                    tmpObj.FilterAttributeID = objControl.FilterAttributeID;
                    tmpObj.FilterLanguageID = objControl.FilterLanguageID;
                    tmpObj.FilterSubProductID = objControl.FilterSubProductID;
                }
                ViewBag.prodAttributeValueObj = tmpObj;
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
                {
                    ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                    TempData["msgLabel"] = null;
                }
                TempData["tmpClassObj"] = null;

                return View(lstProductAttributeValue);
            }
            catch (Exception ee)
            {
                ViewData["Message"] = "Something went wrong,Please try again...";
                return View(new List<clsProductAttributeValue>());
            }
        }
         [Authorize]
        public ActionResult AddProductAttributeValue(int id)
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public string EditProductAttributeValue(string id, string FilterProduct, string FilterSubProduct, string FilterAttribute, string FilterLanguage)
        {

            int ID = 0, filterProductID = 0, filterSubProductID = 0, filterAttributeID = 0, filterLanguageID = 0;
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterProductID = Convert.ToInt32(FilterProduct);
                }
                if (!string.IsNullOrEmpty(FilterSubProduct))
                {
                    filterSubProductID = Convert.ToInt32(FilterSubProduct);
                }
                if (!string.IsNullOrEmpty(FilterAttribute))
                {
                    filterAttributeID = Convert.ToInt32(FilterAttribute);
                }
                if (!string.IsNullOrEmpty(FilterLanguage))
                {
                    filterLanguageID = Convert.ToInt32(FilterLanguage);
                }

                clsProductAttributeValue tmpObj;
                clsProductAttributeValue Objtmp = new clsProductAttributeValue();
                List<clsProductAttributeValue> lstProductAttributeValue = obj.getAllProductAttributeValueByID(ID);
                tmpObj = obj.addProductAttributeValueDefaultData();
                if (lstProductAttributeValue.Count > 0)
                {
                    Objtmp = lstProductAttributeValue[0];
                }
                Objtmp.ListAttribute = tmpObj.ListAttribute;
                Objtmp.ListLanguage = tmpObj.ListLanguage;
                Objtmp.ListProduct = tmpObj.ListProduct;
                Objtmp.ListSubProduct = tmpObj.ListSubProduct;
                Objtmp.FilterAttributeID = filterAttributeID;
                Objtmp.FilterLanguageID = filterLanguageID;
                Objtmp.FilterProductID = filterProductID;
                Objtmp.FilterSubProductID = filterSubProductID;
                return RenderRazorViewToString("EditProductAttributeValue", Objtmp);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditProductAttributeValue", new clsProductAttributeValue());
            }
        }
        [HttpPost]
        [Authorize]
        public string ViewProductAttributeValue(string id, string FilterProduct, string FilterSubProduct, string FilterAttribute, string FilterLanguage)
        {

            int ID = 0, filterProductID = 0, filterSubProductID = 0, filterAttributeID = 0, filterLanguageID = 0;
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterProductID = Convert.ToInt32(FilterProduct);
                }
                if (!string.IsNullOrEmpty(FilterSubProduct))
                {
                    filterSubProductID = Convert.ToInt32(FilterSubProduct);
                }
                if (!string.IsNullOrEmpty(FilterAttribute))
                {
                    filterAttributeID = Convert.ToInt32(FilterAttribute);
                }
                if (!string.IsNullOrEmpty(FilterLanguage))
                {
                    filterLanguageID = Convert.ToInt32(FilterLanguage);
                }
                clsProductAttributeValue tmpObj;
                clsProductAttributeValue Objtmp = new clsProductAttributeValue();
                List<clsProductAttributeValue> lstProductAttributeValue = obj.getAllProductAttributeValueByID(ID);
                tmpObj = obj.addProductAttributeValueDefaultData();
                if (lstProductAttributeValue.Count > 0)
                {
                    Objtmp = lstProductAttributeValue[0];
                }
                Objtmp.ListAttribute = tmpObj.ListAttribute;
                Objtmp.ListLanguage = tmpObj.ListLanguage;
                Objtmp.ListProduct = tmpObj.ListProduct;
                Objtmp.ListSubProduct = tmpObj.ListSubProduct;
                Objtmp.FilterAttributeID = filterAttributeID;
                Objtmp.FilterLanguageID = filterLanguageID;
                Objtmp.FilterProductID = filterProductID;
                Objtmp.FilterSubProductID = filterSubProductID;
                return RenderRazorViewToString("ViewProductAttributeValue", Objtmp);

            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("ViewProductAttributeValue", new clsProductAttributeValue());

            }

        }
        [HttpPost]
        [Authorize]
        public string DeleteProductAttributeValue(string id, string FilterProduct, string FilterSubProduct, string FilterAttribute, string FilterLanguage)
        {

            int ID = 0, filterProductID = 0, filterSubProductID = 0, filterAttributeID = 0, filterLanguageID = 0;
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterProductID = Convert.ToInt32(FilterProduct);
                }
                if (!string.IsNullOrEmpty(FilterSubProduct))
                {
                    filterSubProductID = Convert.ToInt32(FilterSubProduct);
                }
                if (!string.IsNullOrEmpty(FilterAttribute))
                {
                    filterAttributeID = Convert.ToInt32(FilterAttribute);
                }
                if (!string.IsNullOrEmpty(FilterLanguage))
                {
                    filterLanguageID = Convert.ToInt32(FilterLanguage);
                }
                clsProductAttributeValue Objtmp = new clsProductAttributeValue();
                Objtmp.ProductAttributeValuesID = ID;
                Objtmp.FilterAttributeID = filterAttributeID;
                Objtmp.FilterLanguageID = filterLanguageID;
                Objtmp.FilterProductID = filterProductID;
                Objtmp.FilterSubProductID = filterSubProductID;
                return RenderRazorViewToString("DeleteProductAttributeValue", Objtmp);

            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProductAttributeValue", new clsProductAttributeValue());

            }

        }
        [HttpPost]
        [Authorize]
        public ActionResult ProductAttributeValueDelete(clsProductAttributeValue objtmp)
        {
            try
            {
                int result = obj.deleteProductAttributeValue(objtmp);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                clsProductAttributeValue tmpObj = new clsProductAttributeValue();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterLanguageID = objtmp.FilterLanguageID;
                tmpObj.FilterSubProductID = objtmp.FilterSubProductID;
                ViewBag.prodAttributeValueObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductAttributeValues");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductAttributeValues");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult ProductAttributeValueEdit(clsProductAttributeValue objtmp)
        {
            try
            {
                int result = obj.EditProductAttributeValue(objtmp);
                if (result > 0)
                {
                    if (result == 2)
                    {
                        TempData["msgLabel"] = "Record already present,You can not update with this values.";
                    }
                    else
                    {
                        TempData["msgLabel"] = "Record updated successfully.";
                    }

                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                clsProductAttributeValue tmpObj = new clsProductAttributeValue();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterLanguageID = objtmp.FilterLanguageID;
                tmpObj.FilterSubProductID = objtmp.FilterSubProductID;
                ViewBag.prodAttributeValueObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductAttributeValues");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductAttributeValues");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult ProductAttributeValueAdd(clsProductAttributeValue objtmp)
        {
           // System.IO.StreamWriter file;// = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
            try
            {
                int result = obj.AddProductAttributeValue(objtmp);
                if (result > 0)
                {
                    if (result == 2)
                    {
                        TempData["msgLabel"] = "Record already present,You can not added with this values.";
                    }
                    else
                    {
                        TempData["msgLabel"] = "Record added successfully.";
                    }

                }
                else
                {
                   // file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
                   // file.WriteLine("Return Zero");
                  //  file.Close();
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                clsProductAttributeValue tmpObj = new clsProductAttributeValue();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterLanguageID = objtmp.FilterLanguageID;
                tmpObj.FilterSubProductID = objtmp.FilterSubProductID;
                ViewBag.prodAttributeValueObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductAttributeValues");
            }
            catch (Exception ee)
            {
               // file = new System.IO.StreamWriter("C:\\PurityScannerService\\test.txt", true);
               // file.WriteLine("Error");
              //  file.Close();
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductAttributeValues");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult getAllProdValuesByFilterIDs(clsProductAttributeValue objtmp)
        {
            try
            {
                //List<clsProductAttributeValue> lstProductAttributeValue;

                //if (objtmp.FilterAttributeID > 0 || objtmp.FilterProductID > 0 || objtmp.FilterLanguageID > 0 || objtmp.FilterSubProductID > 0)
                //{
                //    lstProductAttributeValue = obj.getAllProductImageByCountryCodeProductID(objtmp.FilterProductID, objtmp.FilterSubProductID, objtmp.FilterAttributeID, objtmp.FilterLanguageID);
                //}
                //else
                //{
                //    lstProductAttributeValue = obj.getAllProductAttributeValueByID();
                //}
                clsProductAttributeValue tmpObj = new clsProductAttributeValue();
                // tmpObj = obj.addProductAttributeValueDefaultData();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterLanguageID = objtmp.FilterLanguageID;
                tmpObj.FilterSubProductID = objtmp.FilterSubProductID;
                ViewBag.prodAttributeValueObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductAttributeValues");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductAttributeValues");
            }

        }
         [Authorize]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

    }
}
