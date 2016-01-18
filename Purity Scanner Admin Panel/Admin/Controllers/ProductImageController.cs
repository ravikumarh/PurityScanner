using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ProductImageController : Controller
    {
        //
        clsProductImage obj = new clsProductImage();
        clsProductImage objControl;

        public ActionResult ListProductImage()
        {
            try
            {
                ViewData["Message"] = null;
                objControl = (clsProductImage)TempData["tmpClassObj"];
                clsProductImage tmpObj;
                List<clsProductImage> lstProductImage;// = obj.getAllProductImageByID();
                if (objControl != null)
                {
                    if (objControl.FilterAttributeID > 0 || objControl.FilterProductID > 0 || !string.IsNullOrEmpty(objControl.FilterCountryCode))
                    {
                        lstProductImage = obj.getAllProductImageByCountryCodeProductID(objControl.FilterProductID, objControl.FilterCountryCode);
                    }
                    else
                    {
                        lstProductImage = obj.getAllProductImageByID();
                    }
                }
                else
                {
                    lstProductImage = obj.getAllProductImageByID();
                }
                tmpObj = obj.addProductImageDefaultData();
                ViewBag.lstCountry = new SelectList(tmpObj.ListCountry, "CountryCode", "CountryName");
                ViewBag.lstProduct = new SelectList(tmpObj.ListProduct, "ProductID", "ProductName");
                if (objControl != null)
                {
                    tmpObj.FilterProductID = objControl.FilterProductID;
                    tmpObj.FilterCountryCode = objControl.FilterCountryCode;
                }
                ViewBag.prodImageObj = tmpObj;
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
                {
                    ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                    TempData["msgLabel"] = null;
                }
                TempData["tmpClassObj"] = null;
                return View(lstProductImage);
            }
            catch (Exception ee)
            {
                ViewData["Message"] = "Something went wrong,Please try again...";
                return View(new List<clsProductImage>());
            }
        }

        [HttpPost]
        public string DeleteProductImage(string id, string FilterProduct, string FilterCountry)
        {
            try
            {
                int filterproductID = 0;
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterproductID = Convert.ToInt32(FilterProduct);
                }
                string filtercontryCode = Convert.ToString(FilterCountry);
                //clsCountryMaster obj = new clsCountryMaster();
                obj.ProductImageID = Convert.ToInt32(id);
                obj.FilterProductID = filterproductID;
                obj.FilterCountryCode = filtercontryCode;
                return RenderRazorViewToString("DeleteProductImage", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProductImage", new clsProductImage());
            }
        }

        [HttpPost]
        public string EditProductImage(string id, string FilterProduct, string FilterCountry)
        {
            try
            {
                int filterproductID = 0;
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterproductID = Convert.ToInt32(FilterProduct);
                }
                string filtercontryCode = Convert.ToString(FilterCountry);
                clsProductImage objtmp = new clsProductImage();
                List<clsProductImage> lst = new List<clsProductImage>();
                lst = obj.getAllProductImageByID(Convert.ToInt32(id));
                objtmp = obj.addProductImageDefaultData();
                ViewBag.lstCountry = new SelectList(objtmp.ListCountry, "CountryCode", "CountryName");
                ViewBag.lstProduct = new SelectList(objtmp.ListProduct, "ProductID", "ProductName");
                if (lst.Count > 0)
                {
                    objtmp = lst[0];
                }
                objtmp.FilterProductID = filterproductID;
                objtmp.FilterCountryCode = filtercontryCode;
                return RenderRazorViewToString("EditProductImage", objtmp);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProductImage", new clsProductImage());
            }
        }


        [HttpPost]
        public string ViewProductImage(string id, string FilterProduct, string FilterCountry)
        {
            try
            {
                int filterproductID = 0;
                if (!string.IsNullOrEmpty(FilterProduct))
                {
                    filterproductID = Convert.ToInt32(FilterProduct);
                }
                string filtercontryCode = Convert.ToString(FilterCountry);
                List<clsProductImage> lst = new List<clsProductImage>();
                lst = obj.getAllProductImageByID(Convert.ToInt32(id));
                if (lst.Count > 0)
                {
                    obj = lst[0];
                }
                obj.FilterProductID = filterproductID;
                obj.FilterCountryCode = filtercontryCode;
                return RenderRazorViewToString("ViewProductImage", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProductImage", new clsProductImage());
            }
        }

        [HttpPost]
        public ActionResult getAllProdImageByCountryIDProductID(clsProductImage objtmp)
        {
            try
            {
                List<clsProductImage> lstProductImage = obj.getAllProductImageByCountryCodeProductID(objtmp.FilterProductID, objtmp.FilterCountryCode);
                clsProductImage tmpObj = new clsProductImage();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterCountryCode = objtmp.FilterCountryCode;
                ViewBag.prodImageObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                TempData["msgLabel"] = null;
                return Redirect("ListProductImage");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductImage");
            }
        }
        [HttpPost]
        public ActionResult ProductImageDelete(clsProductImage objtmp)
        {
            try
            {
                int result = obj.deleteProductImage(objtmp);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                 clsProductImage tmpObj = new clsProductImage();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterCountryCode = objtmp.FilterCountryCode;
                ViewBag.prodImageObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductImage");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductImage");
            }
        }


        [HttpPost]
        public ActionResult ProductImageEdit(clsProductImage objtmp, IEnumerable<HttpPostedFileBase> edituploadFile)
        {
            try
            {
                int result = 0;
                foreach (var file in edituploadFile)
                {
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        file.SaveAs(path);
                        path = "http://tnex.wiredviews.com";
                        path = path + Url.Content(Path.Combine("~/Images", fileName));
                        objtmp.ProductImageUrl = path;
                        result = obj.editProductImage(objtmp);


                    }
                }
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
                clsProductImage tmpObj = new clsProductImage();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterCountryCode = objtmp.FilterCountryCode;
                ViewBag.prodImageObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductImage");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductImage");
            }
        }

        [HttpPost]
        public ActionResult ProductImageAdd(clsProductImage objtmp, IEnumerable<HttpPostedFileBase> uploadFile)
        {
            try
            {
                int result = 0;
                foreach (var file in uploadFile)
                {
                    if (file != null)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/Images"), fileName);
                        file.SaveAs(path);
                        path = "http://tnex.wiredviews.com";
                        path = path + Url.Content(Path.Combine("~/Images", fileName));
                        objtmp.ProductImageUrl = path;
                        result = obj.addProductImage(objtmp);
                    }
                }

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
                clsProductImage tmpObj = new clsProductImage();
                tmpObj.FilterProductID = objtmp.FilterProductID;
                tmpObj.FilterCountryCode = objtmp.FilterCountryCode;
                ViewBag.prodImageObj = tmpObj;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListProductImage");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProductImage");
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