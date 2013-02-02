using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Admin homepage";

            return View();
        }
        
    }
}
