using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Admin.Controllers
{
    public class LanguageController : Controller
    {
        //
        // GET: /Tmp/
        clsLanguageMaster objOperation = new clsLanguageMaster();
         [Authorize]
        public ActionResult LanguageMaster()
        {
            try
            {
                ViewData["Message"] = null;
                List<clsLanguageMaster> obj = objOperation.getLanguagesById();
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
                return View(new List<clsLanguageMaster>());
            }
            
        }


        [HttpPost]
        [Authorize]
        public ActionResult AddLanguage(clsLanguageMaster obj)
        {
            try
            {
                int result = objOperation.addLanguage(obj);
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
                return Redirect("LanguageMaster");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("LanguageMaster");
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult updateLanguage(clsLanguageMaster obj)
        {
            try
            { 
            int result = objOperation.editLanguage(obj);
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
            return Redirect("LanguageMaster");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("LanguageMaster");
            }
        }

        [HttpPost]
        [Authorize]

        public ActionResult LanguageDelete(clsLanguageMaster obj)
        {
            try
            { 
            int result = objOperation.deleteLanguage(obj);
            if (result > 0)
            {
                TempData["msgLabel"] = "Record deleted successfully.";
            }
            else
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
            }
            return Redirect("LanguageMaster");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("LanguageMaster");
            }
        }

        [HttpPost]
        [Authorize]
        public string EditLanguage(string id)
        {
            try
            {
                clsLanguageMaster obj = new clsLanguageMaster();
                List<clsLanguageMaster> objAll = objOperation.getLanguagesById(Convert.ToInt32(id));
                if (objAll.Count > 0)
                {
                    obj = objAll[0];
                }

                return RenderRazorViewToString("EditLanguage", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditLanguage", new clsLanguageMaster());
            }
        }


        [HttpPost]
        [Authorize]
        public string DeleteLanguage(string id)
        {
            try
            {
            clsLanguageMaster obj = new clsLanguageMaster();
            obj.LanguageId =Convert.ToInt32(id);
            return RenderRazorViewToString("DeleteLanguage", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditLanguage", new clsLanguageMaster());
            }
        }

        [HttpPost]
        [Authorize]
        public string viewLanguageDetails(string id)
        {
            try
            { 
            clsLanguageMaster obj = new clsLanguageMaster();
            List<clsLanguageMaster> objAll = objOperation.getLanguagesById(Convert.ToInt32(id));
            if (objAll.Count > 0)
            {
                obj = objAll[0];
            }

            return RenderRazorViewToString("viewLanguageDetails", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditLanguage", new clsLanguageMaster());
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
