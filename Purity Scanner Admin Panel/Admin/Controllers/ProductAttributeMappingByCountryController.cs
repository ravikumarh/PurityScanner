using Admin.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ProductAttributeMappingByCountryController : Controller
    {

        clsProductAttributeMappingByCountry objOperation = new clsProductAttributeMappingByCountry();
        clsProductAttributeMappingByCountry objControl;
        public ActionResult ListProdAttriCountryMapping()
        {
            try
            {
                ViewData["Message"] = null;
                objControl = (clsProductAttributeMappingByCountry)TempData["tmpClassObj"];
                clsProductAttributeMappingByCountry tmpObj;
                List<clsProductAttributeMappingByCountry> lstProdAttriMapping;// = objOperation.getAllProdAttriMappingByID();
                if (objControl != null)
                {
                    if (objControl.FilterAttributeID > 0 || objControl.FilterProductID > 0 || !string.IsNullOrEmpty(objControl.FilterCountryCode))
                    {
                        lstProdAttriMapping = objOperation.getAllProdAttriMappingByCountryIDProductID(objControl.FilterProductID, objControl.FilterCountryCode);
                        if (lstProdAttriMapping.Count > 0)
                        {
                            lstProdAttriMapping[0].FilterAttributeID = objControl.FilterAttributeID;
                            lstProdAttriMapping[0].FilterCountryCode = objControl.FilterCountryCode;
                            lstProdAttriMapping[0].FilterProductID = objControl.FilterProductID;
                        }
                    }
                    else
                    {
                        lstProdAttriMapping = objOperation.getAllProdAttriMappingByID();
                    }
                }
                else
                {
                    lstProdAttriMapping = objOperation.getAllProdAttriMappingByID();
                }


                tmpObj = objOperation.addDefaultData();
                if (objControl != null)
                {
                    tmpObj.FilterAttributeID = objControl.FilterAttributeID;
                    tmpObj.FilterCountryCode = objControl.FilterCountryCode;
                    tmpObj.FilterProductID = objControl.FilterProductID;
                }
                ViewBag.lstAttribute = new SelectList(tmpObj.ListAttribute, "AttributeId", "AttributeName");
                ViewBag.lstProduct = new SelectList(tmpObj.ListProduct, "ProductID", "ProductName");
                ViewBag.lstCountry = new SelectList(tmpObj.ListCountry, "CountryCode", "CountryName");
                ViewBag.mainlstProduct = new SelectList(tmpObj.ListProduct, "ProductID", "ProductName");
                ViewBag.mainlstCountry = new SelectList(tmpObj.ListCountry, "CountryCode", "CountryName");
                ViewBag.classObj = tmpObj;
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
                {
                    ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                    TempData["msgLabel"] = null;
                }
                return View(lstProdAttriMapping);
            }
            catch (Exception ee)
            {
                ViewData["Message"] = "Something went wrong,Please try again...";
                return View(new List<clsProductAttributeMappingByCountry>());
            }
        }

        [HttpPost]
        public ActionResult AddProdAttriCountryMapping(clsProductAttributeMappingByCountry objtmp)
        {
            try
            {
                clsProductAttributeMappingByCountry tmpObj = new clsProductAttributeMappingByCountry();
                int result = objOperation.addProdAttriCountryMapping(objtmp);
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
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterCountryCode = objtmp.FilterCountryCode;
                tmpObj.FilterProductID = objtmp.FilterProductID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProdAttriCountryMapping");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProdAttriCountryMapping");
            }
        }

        [HttpPost]
        public ActionResult ProdAttriCountryMappingEdit(clsProductAttributeMappingByCountry objtmp)
        {
            try
            {
                clsProductAttributeMappingByCountry tmpObj = new clsProductAttributeMappingByCountry();
                int result = objOperation.editProdAttriCountryMapping(objtmp);
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
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterCountryCode = objtmp.FilterCountryCode;
                tmpObj.FilterProductID = objtmp.FilterProductID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProdAttriCountryMapping");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProdAttriCountryMapping");
            }
        }

        [HttpPost]
        public ActionResult ProdAttriCountryMappingDelete(clsProductAttributeMappingByCountry objtmpdelete)
        {
            try
            {
                clsProductAttributeMappingByCountry tmpObj = new clsProductAttributeMappingByCountry();
                int result = objOperation.deleteProdAttriCountryMapping(objtmpdelete);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                tmpObj.FilterAttributeID = objtmpdelete.FilterAttributeID;
                tmpObj.FilterCountryCode = objtmpdelete.FilterCountryCode;
                tmpObj.FilterProductID = objtmpdelete.FilterProductID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProdAttriCountryMapping");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProdAttriCountryMapping");
            }
        }
        [HttpPost]
        public ActionResult getAllProdAttriMappingByCountryIDProductID(clsProductAttributeMappingByCountry tmpObj)
        {
            try
            {
                clsProductAttributeMappingByCountry tmp = new clsProductAttributeMappingByCountry();
                tmp.FilterAttributeID = tmpObj.FilterAttributeID;
                tmp.FilterCountryCode = tmpObj.FilterCountryCode;
                tmp.FilterProductID = tmpObj.FilterProductID;
                TempData["tmpClassObj"] = tmp;
                TempData["msgLabel"] = null;
                return Redirect("ListProdAttriCountryMapping");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProdAttriCountryMapping");
            }

        }

        [HttpPost]
        public string DeleteProdAttriCountryMapping(string id, string FilterProduct, string FilterAttribute, string FilterCountry)
        {
            try
            {
                int filterproductID = 0;
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterproductID = Convert.ToInt32(FilterProduct);
                }
                int filterattributeID = 0;
                if (!string.IsNullOrEmpty(FilterAttribute))
                {
                    filterattributeID = Convert.ToInt32(FilterAttribute);
                }
                string filtercontryCode = Convert.ToString(FilterCountry);
                clsProductAttributeMappingByCountry objtmp = new clsProductAttributeMappingByCountry();
                objtmp.ProductAttributeCountryMappingID = Convert.ToInt32(id);
                objtmp.FilterAttributeID = filterattributeID;
                objtmp.FilterCountryCode = filtercontryCode;
                objtmp.FilterProductID = filterproductID;
                return RenderRazorViewToString("DeleteProdAttriCountryMapping", objtmp);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProdAttriCountryMapping", new clsProductAttributeMappingByCountry());
            }
        }

        [HttpPost]
        public string EditProdAttriCountryMapping(string id, string FilterProduct, string FilterAttribute, string FilterCountry)
        {
            try
            {
                int filterproductID = 0;
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterproductID = Convert.ToInt32(FilterProduct);
                }
                int filterattributeID = 0;
                if (!string.IsNullOrEmpty(FilterAttribute))
                {
                    filterattributeID = Convert.ToInt32(FilterAttribute);
                }
                string filtercontryCode = Convert.ToString(FilterCountry);
                clsProductAttributeMappingByCountry objTmp;
                clsProductAttributeMappingByCountry objtmp = new clsProductAttributeMappingByCountry();
                List<clsProductAttributeMappingByCountry> lst = new List<clsProductAttributeMappingByCountry>();
                lst = objOperation.getAllProdAttriMappingByID(Convert.ToInt32(id));
                objTmp = objOperation.addDefaultData();
                ViewBag.lstAttribute = new SelectList(objTmp.ListAttribute, "AttributeId", "AttributeName");
                ViewBag.lstProduct = new SelectList(objTmp.ListProduct, "ProductID", "ProductName");
                ViewBag.lstCountry = new SelectList(objTmp.ListCountry, "CountryCode", "CountryName");
                if (lst.Count > 0)
                {
                    objtmp = lst[0];
                }
                objtmp.FilterAttributeID = filterattributeID;
                objtmp.FilterCountryCode = filtercontryCode;
                objtmp.FilterProductID = filterproductID;
                return RenderRazorViewToString("EditProdAttriCountryMapping", objtmp);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProdAttriCountryMapping", new clsProductAttributeMappingByCountry());
            }
        }

        [HttpPost]
        public string ViewProdAttriCountryMapping(string id, string FilterProduct, string FilterAttribute, string FilterCountry)
        {
            try
            {
                int filterproductID = 0;
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterproductID = Convert.ToInt32(FilterProduct);
                }
                int filterattributeID = 0;
                if (!string.IsNullOrEmpty(FilterAttribute))
                {
                    filterattributeID = Convert.ToInt32(FilterAttribute);
                }
                string filtercontryCode = Convert.ToString(FilterCountry);
                List<clsProductAttributeMappingByCountry> lst = new List<clsProductAttributeMappingByCountry>();
                lst = objOperation.getAllProdAttriMappingByID(Convert.ToInt32(id));
                if (lst.Count > 0)
                {
                    objOperation = lst[0];
                }
                objOperation.FilterAttributeID = filterattributeID;
                objOperation.FilterCountryCode = filtercontryCode;
                objOperation.FilterProductID = filterproductID;
                return RenderRazorViewToString("ViewProdAttriCountryMapping", objOperation);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProdAttriCountryMapping", new clsProductAttributeMappingByCountry());
            }
        }

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