using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BugTracker.DAL;
using BugTracker.Models;
using WebMatrix.WebData;
using PagedList;

namespace BugTracker.Controllers
{
    [Authorize]
    public class BugController : Controller
    {
        //
        // GET: /Bug/

        public ActionResult Index()
        {   
            return View();
        }

        public ActionResult SelectProject(int projectId = 0)
        {
            ViewBag.ProjectsList = new SelectList(CreateListOfProjects(), "ProjectId", "ProjectName", projectId);
            return View();
        }

        public ActionResult ListBugsByProject(int projectId = 0, string sortOrder = "Date", int page = 1)
        {
            ViewBag.ProjectsList = new SelectList(CreateListOfProjects(), "ProjectId", "ProjectName", projectId);

            List<BugViewModel> bugs = new List<BugViewModel>();

            if (projectId == 0)
            {
                bugs = CreateListOfBugs(BugDAL.ListAllBugs());
            }
            else
            {
                bugs = CreateListOfBugs(BugDAL.ListBugsByProjectId(projectId));
            }

            bugs = SortBugs(bugs, sortOrder);

            ViewBag.CurrentSortOrder = sortOrder;
            ViewBag.TesterSortParm = sortOrder == "Tester" ? "Tester desc" : "Tester";
            ViewBag.ProjectSortParm = sortOrder == "Project" ? "Project desc" : "Project";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            ViewBag.PrioritySortParm = sortOrder == "Priority" ? "Priority desc" : "Priority";
            ViewBag.StatusSortParm = sortOrder == "Status" ? "Status desc" : "Status";

            int pageSize = 15;

            return View(bugs.ToPagedList<BugViewModel>(page, pageSize));
        }

        //
        // GET: /Bug/Details/5

        public ActionResult Details(int id = 0)
        {
            Bug bugDB = BugDAL.GetBugById(id);
            if (bugDB == null)
            {
                return RedirectToAction("DeletedBug");
            }

            BugViewModel bug = new BugViewModel(bugDB);
            return View(bug);
        }

        //
        // GET: /Bug/Create

        public ActionResult Create(string returnAction)
        {
            ViewBag.ReturnAction = returnAction;

            List<ProjectViewModel> projects = new List<ProjectViewModel>();
            foreach (var item in ProjectDAL.ListAllProjects())
            {
                projects.Add(new ProjectViewModel(item));
            }
            ViewBag.ProjectId = new SelectList(projects, "ProjectId", "ProjectName");

            ViewBag.PriorityList = new SelectList(PriorityList);
            //List<UserProfileViewModel> testers = new List<UserProfileViewModel>();
            //foreach (var item in UserProfileDAL.ListAllUserProfiles())
            //{
            //    testers.Add(new UserProfileViewModel(item));
            //}
            //ViewBag.UserId = new SelectList(testers, "UserId", "UserName");

            CreateBugViewModel bug = new CreateBugViewModel();
            return View(bug);
        }

        //
        // POST: /Bug/Create

        [HttpPost]
        public ActionResult Create(CreateBugViewModel bug, string returnAction)
        {
            if (ModelState.IsValid)
            {
                BugDAL.AddBug(
                    bug.TesterId,
                    bug.ProjectId,
                    bug.Priority,
                    bug.Status,
                    bug.DiscoverDate,
                    bug.Description);

                InfoCardDAL.AddBugToInfoCard(bug.TesterId, bug.ProjectId);

                return RedirectToAction( returnAction);
            }

            List<ProjectViewModel> projects = new List<ProjectViewModel>();
            foreach (var item in ProjectDAL.ListAllProjects())
            {
                projects.Add(new ProjectViewModel(item));
            }
            ViewBag.ProjectId = new SelectList(projects, "ProjectId", "ProjectName", bug.ProjectId);

            ViewBag.PriorityList = new SelectList(PriorityList);
            //ViewBag.UserId = new SelectList(UserProfileDAL.ListAllUserProfiles(), "UserId", "UserName", bug.TesterId);
            return View(bug);
        }

        //
        // GET: /Bug/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Bug bugDB = BugDAL.GetBugById(id);
            if (bugDB == null)
            {
                return RedirectToAction("DeletedBug");
            }

            BugViewModel bug = new BugViewModel(bugDB);

            ViewBag.StatusList = new SelectList(StatusList, bug.Status);
            ViewBag.PriorityList = new SelectList(PriorityList, bug.Priority);
            
            return View(bug);
        }

        //
        // POST: /Bug/Edit/5

        [HttpPost]
        public ActionResult Edit(BugViewModel bug)
        {
            if (ModelState.IsValid)
            {
                BugDAL.UpdateBug(
                    bug.BugId,
                    bug.Priority,
                    bug.Status,
                    bug.Description
                    );

                InfoCardDAL.UpdateBugInfoCard(WebSecurity.CurrentUserId, bug.BugId, "Edited");

                return RedirectToAction("Index");
            }

            ViewBag.StatusList = new SelectList(StatusList, bug.Status);
            ViewBag.PriorityList = new SelectList(PriorityList, bug.Priority);

            return View(bug);
        }

        //
        // GET: /Bug/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Bug bugDB = BugDAL.GetBugById(id);
            if (bugDB == null)
            {
                return RedirectToAction("DeletedBug");
            }

            BugViewModel bug = new BugViewModel(bugDB);

            return View(bug);
        }

        //
        // POST: /Bug/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            BugDAL.RemoveBug(id);

            InfoCardDAL.UpdateBugInfoCard(WebSecurity.CurrentUserId, id, "Deleted");

            return RedirectToAction("Index");
        }

        public ActionResult DeletedBug()
        {
            return View();
        }


        #region Helpers

        private List<BugViewModel> SortBugs(List<BugViewModel> bugs , string sortOrder)
        {
            if (sortOrder == "")
            {
                sortOrder = "Date";
            }
            switch (sortOrder)
            {
                case "Date":
                    bugs = bugs.OrderBy(x => x.DiscoverDate).ToList();
                    break;
                case "Date desc":
                    bugs = bugs.OrderByDescending(x => x.DiscoverDate).ToList();
                    break;
                case "Tester":
                    bugs = bugs.OrderBy(x => x.TesterName).ToList();
                    break;
                case "Tester desc":
                    bugs = bugs.OrderByDescending(x => x.TesterName).ToList();
                    break;
                case "Project":
                    bugs = bugs.OrderBy(x => x.ProjectName).ToList();
                    break;
                case "Project desc":
                    bugs = bugs.OrderByDescending(x => x.ProjectName).ToList();
                    break;
                case "Priority":
                    bugs = bugs.OrderBy(x => x.PriorityId).ToList();
                    break;
                case "Priority desc":
                    bugs = bugs.OrderByDescending(x => x.PriorityId).ToList();
                    break;
                case "Status":
                    bugs = bugs.OrderBy(x => x.StatusId).ToList();
                    break;
                case "Status desc":
                    bugs = bugs.OrderByDescending(x => x.StatusId).ToList();
                    break;
                default:
                    bugs = bugs.OrderBy(x => x.DiscoverDate).ToList();
                    break;
            }
            return bugs;
        }

        private List<ProjectViewModel> CreateListOfProjects()
        {
            List<ProjectViewModel> projects = new List<ProjectViewModel>();
            foreach (var item in ProjectDAL.ListAllProjects())
            {
                projects.Add(new ProjectViewModel(item));
            }
            projects = projects.OrderBy(x => x.ProjectName).ToList();

            //Insert a project indicating all projects
            projects.Insert(0, new ProjectViewModel(0, "All projects", "List all projects"));

            return projects;
        }

        private List<BugViewModel> CreateListOfBugs(List<Bug> bugsDB)
        {
            List<BugViewModel> bugs = new List<BugViewModel>();
            foreach (var bug in bugsDB)
            {
                bugs.Add(new BugViewModel(bug));
            }

            return bugs;
        }

        public List<string> StatusList = new List<string> 
        {
            "New",
            "In Progress",
            "Fixed",
            "Closed"
        };

        public List<string> PriorityList = new List<string>
        {
            "Critical",
            "High",
            "Normal",
            "Low"
        };
        #endregion
    }
}