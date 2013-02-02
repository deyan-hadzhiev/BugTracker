using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace BugTracker.Areas.Admin.Controllers
{
    [Authorize(Roles="Admin")]
    public class AdminController : Controller
    {

    }
}
