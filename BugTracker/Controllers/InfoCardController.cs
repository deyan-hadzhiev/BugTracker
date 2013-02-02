using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.DAL;
using BugTracker.Models;
using PagedList;

namespace BugTracker.Controllers
{
    [Authorize]
    public class InfoCardController : Controller
    {
        //
        // GET: /InfoCard/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListAllInfoCards(string sortOrder = "Id", int page = 1)
        {
            List<BugTracker.DAL.UserProfile> userProfiles = UserProfileDAL.ListAllUserProfiles();
            List<InfoCardViewModel> infoCards = new List<InfoCardViewModel>();

            foreach (var user in userProfiles)
            {
                infoCards.Add(new InfoCardViewModel(InfoCardDAL.GetUserInfoCard(user)));
            }

            infoCards = SortInfoCards(infoCards, sortOrder);

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.IdSortParm = sortOrder == "Id" ? "Id desc" : "Id";
            ViewBag.NameSortParm = sortOrder == "Name" ? "Name desc" : "Name";
            ViewBag.BugsSortParm = sortOrder == "Bugs" ? "Bugs desc" : "Bugs";
            ViewBag.ProjectsSortParm = sortOrder == "Projects" ? "Projects desc" : "Projects";
            ViewBag.ActivitySortParm = sortOrder == "Activity" ? "Activity desc" : "Activity";

            int pageSize = 15;

            return View(infoCards.ToPagedList<InfoCardViewModel>(page, pageSize));
        }

        #region Helpers

        private List<InfoCardViewModel> SortInfoCards(List<InfoCardViewModel> iCards, string sortOrder)
        {
            if (sortOrder == "")
            {
                sortOrder = "Id";
            }

            switch (sortOrder)
            {
                case "Id":
                    iCards = iCards.OrderBy(x => x.UserId).ToList();
                    break;
                case "Id desc":
                    iCards = iCards.OrderByDescending(x => x.UserId).ToList();
                    break;
                case "Name":
                    iCards = iCards.OrderBy(x => x.UserName).ToList();
                    break;
                case "Name desc":
                    iCards = iCards.OrderByDescending(x => x.UserName).ToList();
                    break;
                case "Bugs":
                    iCards = iCards.OrderBy(x => x.NumberOfBugs).ToList();
                    break;
                case "Bugs desc":
                    iCards = iCards.OrderByDescending(x => x.NumberOfBugs).ToList();
                    break;
                case "Projects":
                    iCards = iCards.OrderBy(x => x.NumberOfProjects).ToList();
                    break;
                case "Projects desc":
                    iCards = iCards.OrderByDescending(x => x.NumberOfProjects).ToList();
                    break;
                case "Activity":
                    iCards = iCards.OrderBy(x => x.LastActivity).ToList();
                    break;
                case "Activity desc":
                    iCards = iCards.OrderByDescending(x => x.LastActivity).ToList();
                    break;
                default:
                    iCards = iCards.OrderBy(x => x.UserId).ToList();
                    break;
            }
            return iCards;
        }

        #endregion
    }
}
