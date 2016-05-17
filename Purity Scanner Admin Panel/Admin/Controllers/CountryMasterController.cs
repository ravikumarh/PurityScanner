using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class CountryMasterController : Controller
    {
        //
        // GET: /CountryMaster/
        clsCountryMaster obj = new clsCountryMaster();

         [Authorize]
        public ActionResult AddCountry()
        {
            clsCountryMaster c = new clsCountryMaster();
            ViewBag.lngList = new SelectList(obj.addCountryDefaultData().ListLanguage, "LanguageId", "LanguageName");
            return View();
        }
         [Authorize]
        public ActionResult ListCountry()
        {
            try
            {
                ViewData["Message"] = null;
                List<clsCountryMaster> lstCountry = obj.getAllCountryByCode();
                ViewBag.lngList = new SelectList(obj.addCountryDefaultData().ListLanguage, "LanguageId", "LanguageName");
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
                {
                    ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                    TempData["msgLabel"] = null;
                }
                return View(lstCountry);
            }
            catch (Exception ee)
            {
                ViewData["msgLabel"] = "Something went wrong,Please try again...";
                return View(new List<clsCountryMaster>());
            }
        }
        [HttpPost]
        [Authorize]
        public string EditCountry(string id)
        {
            try
            {
                clsCountryMaster objtmp = new clsCountryMaster();
                List<clsCountryMaster> lst = new List<clsCountryMaster>();
                lst = obj.getAllCountryByCode(id);
                ViewBag.lngList = new SelectList(obj.addCountryDefaultData().ListLanguage, "LanguageId", "LanguageName");
                if (lst.Count > 0)
                {
                    objtmp = lst[0];
                }

                return RenderRazorViewToString("EditCountry", objtmp);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditCountry", new clsCountryMaster());
            }
        }
        [HttpPost]
        [Authorize]
        public string DeleteCountry(string id)
        {
            try
            {
                obj.CountryCode = id;
                return RenderRazorViewToString("DeleteCountry", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditCountry", new clsCountryMaster());
            }
        }
        [HttpPost]
        [Authorize]
        public string ViewCountry(string id)
        {
            try
            {
                List<clsCountryMaster> lst = new List<clsCountryMaster>();
                lst = obj.getAllCountryByCode(id);
                if (lst.Count > 0)
                {
                    obj = lst[0];
                }
                return RenderRazorViewToString("ViewCountry", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditCountry", new clsCountryMaster());
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult updateCountry(clsCountryMaster objtmp)
        {
            try
            {
                int result = obj.editCountry(objtmp);
                if (result > 0)
                {
                    if (result == 3)
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
                return Redirect("ListCountry");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListCountry");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult CountryDelete(clsCountryMaster objtmp)
        {
            try
            {
                int result = obj.deleteCountry(objtmp);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }

                return Redirect("ListCountry");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListCountry");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult AddCountry(clsCountryMaster objtmp)
        {
            try
            {
                int result = obj.addCountry(objtmp);
                if (result > 0)
                {
                    if (result == 3)
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
                return Redirect("ListCountry");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListCountry");
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