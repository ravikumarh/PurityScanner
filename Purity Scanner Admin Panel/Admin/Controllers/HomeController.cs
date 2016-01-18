using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Admin.Models;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           // ViewData["Message"] = null;
            if (!string.IsNullOrEmpty(Convert.ToString(TempData["msgLabel"])))
            {
               // ViewData["Message"] = Convert.ToString(TempData["msgLabel"]);
                TempData["msgLabel"] = null;
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(clsAccount objLogin)
        {
            try
            {
                clsAccount obj = new clsAccount();
                int result = obj.loginUser(objLogin);
                if (result > 0)
                {
                    TempData["msgLabel"] = "User login successfully.";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["Message"] = "Please confirm username and password";
                    return View();
                }
            }
            catch (Exception ee)
            {
                ViewData["Message"] = "Please confirm username and password";
                return View();
            }
            
        }
       
    }

}