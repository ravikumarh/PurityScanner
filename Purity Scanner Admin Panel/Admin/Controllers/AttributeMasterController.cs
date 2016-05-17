using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class AttributeMasterController : Controller
    {
        //
        clsAttributeMaster objOperation = new clsAttributeMaster();
        // GET: /AttributeMaster/
         [Authorize]
        public ActionResult ListAttribute()
        {
            try
            {
                ViewData["Message"] = null;
                List<clsAttributeMaster> obj = objOperation.getAttributesById();
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
                return View(new List<clsAttributeMaster>());
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AttributeDelete(clsAttributeMaster obj)
        {
            try
            {
                int result = objOperation.deleteAttribute(obj);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                return Redirect("ListAttribute");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttribute");
            }
        }

        [HttpPost]
        [Authorize]
        public string DeleteAttribute(string id)
        {
            clsAttributeMaster obj = new clsAttributeMaster();
            obj.AttributeId = Convert.ToInt32(id);
            return RenderRazorViewToString("DeleteAttribute", obj);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AttributeAdd(clsAttributeMaster obj, IEnumerable<HttpPostedFileBase> uploadFile)
        {
            try
            {
                foreach (var file in uploadFile)
                {
                    if (file != null)
                    {
                        // code for saving the image file to a physical location.
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine("C:\\PurityScannerService\\PurityScannerService\\Images", fileName);
                        file.SaveAs(path);

                        // prepare a relative path to be stored in the database and used to display later on.
                        path = "http://tnex.wiredviews.com";
                        path = path + Url.Content(Path.Combine("~/Images", fileName));
                        obj.AttributeImageUrl = path;
                        int result = objOperation.addAttribute(obj);
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
                    }
                }
                return Redirect("ListAttribute");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttribute");
            }

        }


        [HttpPost]
        [Authorize]
        public ActionResult updateAttribute(clsAttributeMaster obj, IEnumerable<HttpPostedFileBase> edituploadFile)
        {
            try
            {
                foreach (var file in edituploadFile)
                {
                    if (file != null)
                    {
                        // code for saving the image file to a physical location.
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine("C:\\PurityScannerService\\PurityScannerService\\Images", fileName);//Path.Combine(Server.MapPath("~/Images"), fileName);
                        file.SaveAs(path);

                        // prepare a relative path to be stored in the database and used to display later on.
                        path = "http://tnex.wiredviews.com";
                        path = path + Url.Content(Path.Combine("~/Images", fileName));
                        obj.AttributeImageUrl = path;
                        int result = objOperation.editAttribute(obj);
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
                    }
                }

                //List<clsAttributeMaster> objAll = objOperation.getAttributesById();
                return Redirect("ListAttribute");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttribute");
            }
        }

        [HttpPost]
        [Authorize]
        public string EditAttribute(string id)
        {
            try
            {
                clsAttributeMaster obj = new clsAttributeMaster();
                List<clsAttributeMaster> objAll = objOperation.getAttributesById(Convert.ToInt32(id));
                if (objAll.Count > 0)
                {
                    obj = objAll[0];
                }

                return RenderRazorViewToString("EditAttribute", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditAttribute", new clsAttributeMaster());
            }
        }


        [HttpPost]
        [Authorize]
        public string viewAttribute(string id)
        {
            try
            {
                clsAttributeMaster obj = new clsAttributeMaster();
                List<clsAttributeMaster> objAll = objOperation.getAttributesById(Convert.ToInt32(id));
                if (objAll.Count > 0)
                {
                    obj = objAll[0];
                }

                return RenderRazorViewToString("viewAttribute", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditAttribute", new clsAttributeMaster());
            }
        }

         [Authorize]
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