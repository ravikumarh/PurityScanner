using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class ProductMasterController : Controller
    {
        clsProductMaster objOperation = new clsProductMaster();
        public ActionResult ListProduct()
        {
            try
            {
                ViewData["Message"] = null;
                List<clsProductMaster> obj = objOperation.getProductsById();
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
                {
                    ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                    TempData["msgLabel"] = null;
                }
                return View(obj);
            }
            catch (Exception ee)
            {
                ViewData["Message"] = "Something went wrong,Please try again...";
                return View(new List<clsProductMaster>());
            }
        }



        [HttpPost]
        public ActionResult AddProduct(clsProductMaster obj)
        {
            try
            {
                int result = objOperation.addProduct(obj);
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
                return Redirect("ListProduct");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProduct");
            }

        }


        [HttpPost]

        public ActionResult ProductDelete(clsProductMaster obj)
        {
            try
            {
                int result = objOperation.deleteProduct(obj);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                return Redirect("ListProduct");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProduct");
            }
        }

        [HttpPost]
        public ActionResult updateProduct(clsProductMaster obj)
        {
            try
            {
                int result = objOperation.editProduct(obj);
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
                return Redirect("ListProduct");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListProduct");
            }
        }

        [HttpPost]
        public string DeleteProduct(string id)
        {
            try
            {
                clsProductMaster obj = new clsProductMaster();
                obj.productId = Convert.ToInt32(id);
                return RenderRazorViewToString("DeleteProduct", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProduct", new clsProductMaster());
            }
        }



        [HttpPost]
        public string EditProduct(string id)
        {
            try
            {
                clsProductMaster obj = new clsProductMaster();
                List<clsProductMaster> objAll = objOperation.getProductsById(Convert.ToInt32(id));
                if (objAll.Count > 0)
                {
                    obj = objAll[0];
                }

                return RenderRazorViewToString("EditProduct", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProduct", new clsProductMaster());
            }
        }


        [HttpPost]
        public string viewProduct(string id)
        {
            try
            {
                clsProductMaster obj = new clsProductMaster();
                List<clsProductMaster> objAll = objOperation.getProductsById(Convert.ToInt32(id));
                if (objAll.Count > 0)
                {
                    obj = objAll[0];
                }

                return RenderRazorViewToString("viewProduct", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteProduct", new clsProductMaster());
            }
        }

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}