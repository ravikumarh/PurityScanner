using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class SubProductController : Controller
    {
        clsSubProduct objOperations = new clsSubProduct();
         [Authorize]
        public ActionResult ListSubProduct()
        {
            try
            {
                ViewData["Message"] = null;
                List<clsSubProduct> obj = new List<clsSubProduct>();
                clsSubProduct tmpObj = new clsSubProduct();
                tmpObj = objOperations.addSubProductDefaultData();
                obj = objOperations.getAllSubProductByID();
                ViewBag.objSubProduct = tmpObj;
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
                return View(new List<clsSubProduct>());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult EditSubProduct(string id)
        {
            try
            {
                int ID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                List<clsSubProduct> obj = new List<clsSubProduct>();
                clsSubProduct tmpObj = new clsSubProduct();
                obj = objOperations.getAllSubProductByID(ID);
                if (obj.Count > 0)
                {
                    tmpObj = obj[0];
                }
                clsSubProduct tmp;
                tmp = objOperations.addSubProductDefaultData();
                tmpObj.ListProduct = tmp.ListProduct;
                tmpObj.ListSubProduct = tmp.ListSubProduct;
                return PartialView("EditSubProduct", tmpObj);
               // return RenderRazorViewToString("EditSubProduct", tmpObj);
            }
            catch (Exception ee)
            {
                return PartialView("EditSubProduct", new clsSubProduct());
               // return RenderRazorViewToString("EditSubProduct", new clsSubProduct());
            }
        }


        [HttpPost]
        [Authorize]
        public string ViewSubProduct(string id)
        {
            try
            {
                int ID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                List<clsSubProduct> obj = new List<clsSubProduct>();
                clsSubProduct tmpObj = new clsSubProduct();
                obj = objOperations.getAllSubProductByID(ID);
                if (obj.Count > 0)
                {
                    tmpObj = obj[0];
                }
                clsSubProduct tmp;
                tmp = objOperations.addSubProductDefaultData();
                tmpObj.ListProduct = tmp.ListProduct;
                tmpObj.ListSubProduct = tmp.ListSubProduct;

                return RenderRazorViewToString("ViewSubProduct", tmpObj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditSubProduct", new clsSubProduct());
            }
        }


        [HttpPost]
        [Authorize]
        public string DeleteSubProduct(string id)
        {
            try
            {
                int ID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                List<clsSubProduct> obj = new List<clsSubProduct>();
                clsSubProduct tmpObj = new clsSubProduct();

                obj = objOperations.getAllSubProductByID(ID);
                if (obj.Count > 0)
                {
                    tmpObj = obj[0];
                }
                clsSubProduct tmp;
                tmp = objOperations.addSubProductDefaultData();
                tmpObj.ListProduct = tmp.ListProduct;
                tmpObj.ListSubProduct = tmp.ListSubProduct;

                return RenderRazorViewToString("DeleteSubProduct", tmpObj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditSubProduct", new clsSubProduct());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult SubProductDelete(clsSubProduct Objtmp)
        {
            try
            {
                int result = objOperations.DeleteSubProduct(Objtmp);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                return Redirect("ListSubProduct");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListSubProduct");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult SubProductEdit(clsSubProduct Objtmp)
        {
            try
            {
                int result = objOperations.EditSubProduct(Objtmp);
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
                return Redirect("ListSubProduct");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListSubProduct");
            }

        }

        [HttpPost]
        [Authorize]
        public ActionResult SubProductAdd(clsSubProduct Objtmp)
        {
            try
            {
                int result = objOperations.AddSubProduct(Objtmp);
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
                return Redirect("ListSubProduct");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListSubProduct");
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