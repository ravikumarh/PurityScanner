using Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class AttributeTitleController : Controller
    {
        clsAttributeTitle obj = new clsAttributeTitle();
        clsAttributeTitle objControl;

         [Authorize]
        public ActionResult ListAttributeTitle()
        {
            try
            {
                ViewData["Message"] = null;
                objControl = (clsAttributeTitle)TempData["tmpClassObj"];
                List<clsAttributeTitle> lstAttributeTitle;//= obj.getAllAttributeTitleByID();
                if (objControl != null)
                {
                    if (objControl.FilterAttributeID > 0 || objControl.FilterAttributeLanguageID > 0)
                    {
                        lstAttributeTitle = obj.getAllAttributeTitleByAttriIDLangID(objControl.FilterAttributeID, objControl.FilterAttributeLanguageID);
                    }
                    else
                    {
                        lstAttributeTitle = obj.getAllAttributeTitleByID();
                    }
                }
                else
                {
                    lstAttributeTitle = obj.getAllAttributeTitleByID();
                }
                clsAttributeTitle tmpObj;
                tmpObj = obj.addAttributeDefaultData();
                if (objControl != null)
                {
                    tmpObj.FilterAttributeID = objControl.FilterAttributeID;
                    tmpObj.FilterAttributeLanguageID = objControl.FilterAttributeLanguageID;
                }
                ViewBag.lstAttribute = new SelectList(tmpObj.ListAttribute, "AttributeId", "AttributeName");
                ViewBag.lngList = new SelectList(tmpObj.ListLanguage, "LanguageId", "LanguageName");
                ViewBag.attributeTitleObj = tmpObj;
                TempData["tmpClassObj"] = null;
                if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
                {
                    ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                    TempData["msgLabel"] = null;
                }
                return View(lstAttributeTitle);
            }
            catch (Exception ee)
            {
                ViewData["Message"] = "Something went wrong,Please try again...";
                return View(new List<clsAttributeTitle>());
            }
        }

        [HttpPost]
        [Authorize]
        public string DeleteAttributeTitle(string id, string filterAttributeId, string filterLanguageId)
        {
            try
            {
                clsAttributeTitle tmpObj = new clsAttributeTitle();
                int filterAttriID = 0;
                int filterLangID = 0;
                if (!string.IsNullOrEmpty(filterAttributeId))
                {
                    filterAttriID = Convert.ToInt32(filterAttributeId);
                }
                if (!string.IsNullOrEmpty(filterLanguageId))
                {
                    filterLangID = Convert.ToInt32(filterLanguageId);
                }
                tmpObj.AttributeTitleID = Convert.ToInt32(id);
                tmpObj.FilterAttributeID = filterAttriID;
                tmpObj.FilterAttributeLanguageID = filterLangID;
                return RenderRazorViewToString("DeleteAttributeTitle", tmpObj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("DeleteAttributeTitle", new clsAttributeTitle());
            }
        }

        [HttpPost]
        [Authorize]
        public string EditAttributeTitle(string id, string filterAttributeId, string filterLanguageId)
        {
            try
            {
                int filterAttriID = 0;
                int filterLangID = 0;
                if (!string.IsNullOrEmpty(filterAttributeId))
                {
                    filterAttriID = Convert.ToInt32(filterAttributeId);
                }
                if (!string.IsNullOrEmpty(filterLanguageId))
                {
                    filterLangID = Convert.ToInt32(filterLanguageId);
                }
                clsAttributeTitle objtmp = new clsAttributeTitle();
                List<clsAttributeTitle> lst = new List<clsAttributeTitle>();
                lst = obj.getAllAttributeTitleByID(Convert.ToInt32(id));
                objtmp = obj.addAttributeDefaultData();
                ViewBag.lstAttribute = new SelectList(objtmp.ListAttribute, "AttributeId", "AttributeName");
                ViewBag.lngList = new SelectList(objtmp.ListLanguage, "LanguageId", "LanguageName");
                if (lst.Count > 0)
                {
                    objtmp = lst[0];
                }
                objtmp.FilterAttributeID = filterAttriID;
                objtmp.FilterAttributeLanguageID = filterLangID;
                return RenderRazorViewToString("EditAttributeTitle", objtmp);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditAttributeTitle", new clsAttributeTitle());
            }
        }


        [HttpPost]
        [Authorize]
        public string ViewAttributeTitle(string id, string filterAttributeId, string filterLanguageId)
        {
            try
            {
                int filterAttriID = 0;
                int filterLangID = 0;
                if (!string.IsNullOrEmpty(filterAttributeId))
                {
                    filterAttriID = Convert.ToInt32(filterAttributeId);
                }
                if (!string.IsNullOrEmpty(filterLanguageId))
                {
                    filterLangID = Convert.ToInt32(filterLanguageId);
                }
                List<clsAttributeTitle> lst = new List<clsAttributeTitle>();
                lst = obj.getAllAttributeTitleByID(Convert.ToInt32(id));
                if (lst.Count > 0)
                {
                    obj = lst[0];
                }
                obj.FilterAttributeID = filterAttriID;
                obj.FilterAttributeLanguageID = filterLangID;
                return RenderRazorViewToString("ViewAttributeTitle", obj);
            }
            catch (Exception ee)
            {
                return RenderRazorViewToString("EditAttributeTitle", new clsAttributeTitle());
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult getAllAttributeTitleByAttriIDLangID(clsAttributeTitle objtmp)
        {
            try
            {
                clsAttributeTitle tmpObj = new clsAttributeTitle();
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterAttributeLanguageID = objtmp.FilterAttributeLanguageID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListAttributeTitle");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttributeTitle");
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult AttributeTitleDelete(clsAttributeTitle objtmp)
        {
            try
            {
                int result = obj.deleteAttributeTitle(objtmp);
                if (result > 0)
                {
                    TempData["msgLabel"] = "Record deleted successfully.";
                }
                else
                {
                    TempData["msgLabel"] = "Something went wrong,Please try again...";
                }

                clsAttributeTitle tmpObj = new clsAttributeTitle();
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterAttributeLanguageID = objtmp.FilterAttributeLanguageID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListAttributeTitle");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttributeTitle");
            }
        }


        [HttpPost]
        [Authorize]
        public ActionResult AttributeTitleEdit(clsAttributeTitle objtmp)
        {
            try
            {
                int result = obj.editAttributeTitle(objtmp);
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
                clsAttributeTitle tmpObj = new clsAttributeTitle();
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterAttributeLanguageID = objtmp.FilterAttributeLanguageID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListAttributeTitle");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttributeTitle");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult AttributeTitleAdd(clsAttributeTitle objtmp)
        {
            try
            {
                int result = obj.addAttributeTitle(objtmp);
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
                clsAttributeTitle tmpObj = new clsAttributeTitle();
                tmpObj.FilterAttributeID = objtmp.FilterAttributeID;
                tmpObj.FilterAttributeLanguageID = objtmp.FilterAttributeLanguageID;
                TempData["tmpClassObj"] = tmpObj;
                return Redirect("ListAttributeTitle");
            }
            catch (Exception ee)
            {
                TempData["msgLabel"] = "Something went wrong,Please try again...";
                return Redirect("ListAttributeTitle");
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