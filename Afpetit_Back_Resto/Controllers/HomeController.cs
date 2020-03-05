using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Afpetit_Back_Resto.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["restaurant"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Connexion", "Restaurants");
            }            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}