using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class CompanyMissionInfoController : Controller
    {

        clsCompanyMissionInfo objOperations = new clsCompanyMissionInfo();
        public ActionResult ListCompanyMissionInfo()
        {
            try
            {
                ViewData["Message"] = null;
                List<clsCompanyMissionInfo> obj = new List<clsCompanyMissionInfo>();
                clsCompanyMissionInfo tmpObj = new clsCompanyMissionInfo();
                tmpObj = objOperations.addCompanyMissionDefaultData();
                obj = objOperations.getAllCompanyMissionByID();
                ViewBag.objCompanyMission = tmpObj;
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
                return View(new List<clsCompanyMissionInfo>());
            }
        }

        [HttpPost]
        public string EditCompanyMissionInfo(string id)
        {
            try
            {
                int ID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                List<clsCompanyMissionInfo> obj = new List<clsCompanyMissionInfo>();
                clsCompanyMissionInfo tmpObj = new clsCompanyMissionInfo();
                obj = objOperations.getAllCompanyMissionByID(ID);
                if (obj.Count > 0)
                {
                    tmpObj = obj[0];
                }
                tmpObj.ListLanguage = objOperations.addCompanyMissionDefaultData().ListLanguage;

                return RenderRazorViewToString("EditCompanyMissionInfo", tmpObj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditCompanyMissionInfo", new clsCompanyMissionInfo());
            }
        }


        [HttpPost]
        public string ViewCompanyMissionInfo(string id)
        {
            try
            {
                int ID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                List<clsCompanyMissionInfo> obj = new List<clsCompanyMissionInfo>();
                clsCompanyMissionInfo tmpObj = new clsCompanyMissionInfo();
                obj = objOperations.getAllCompanyMissionByID(ID);
                if (obj.Count > 0)
                {
                    tmpObj = obj[0];
                }
                tmpObj.ListLanguage = objOperations.addCompanyMissionDefaultData().ListLanguage;

                return RenderRazorViewToString("ViewCompanyMissionInfo", tmpObj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("ViewCompanyMissionInfo", new clsCompanyMissionInfo());
            }
        }


        [HttpPost]
        public string DeleteCompanyMissionInfo(string id)
        {
            try
            {
                int ID = 0;
                if (!string.IsNullOrEmpty(id))
                {
                    ID = Convert.ToInt32(id);
                }
                List<clsCompanyMissionInfo> obj = new List<clsCompanyMissionInfo>();
                clsCompanyMissionInfo tmpObj = new clsCompanyMissionInfo();
                obj = objOperations.getAllCompanyMissionByID(ID);
                if (obj.Count > 0)
                {
                    tmpObj = obj[0];
                }
                tmpObj.ListLanguage = objOperations.addCompanyMissionDefaultData().ListLanguage;

                return RenderRazorViewToString("DeleteCompanyMissionInfo", tmpObj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteCompanyMissionInfo", new clsCompanyMissionInfo());
            }
        }

        [HttpPost]
        public ActionResult DeleteCompanyMission(clsCompanyMissionInfo Objtmp)
        {
            try
            {
                int result = objOperations.DeleteCompanyMission(Objtmp);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }
                return Redirect("ListCompanyMissionInfo");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListCompanyMissionInfo");
            }
        }

        [HttpPost]
        public ActionResult EditCompanyMission(clsCompanyMissionInfo Objtmp)
        {
            try
            {
                int result = objOperations.EditCompanyMission(Objtmp);
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
                return Redirect("ListCompanyMissionInfo");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListCompanyMissionInfo");
            }
        }

        [HttpPost]
        public ActionResult AddCompanyMission(clsCompanyMissionInfo Objtmp)
        {
            try
            {
                int result = objOperations.AddCompanyMission(Objtmp);
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
                return Redirect("ListCompanyMissionInfo");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListCompanyMissionInfo");
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