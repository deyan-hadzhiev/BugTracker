using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.Data;
using System.Web.Security;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to my first ASP.NET MVC Application";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Little description about the project";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contacting the developer of this project";

            return View();
        }

    }
}
