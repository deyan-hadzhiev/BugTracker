using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using BugTracker.DAL;
using BugTracker.Areas.Admin.Models;

namespace BugTracker.Areas.Admin.Controllers
{
    public class ProjectController : AdminController
    {
        //
        // GET: /Admin/Project/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListProjects(string searchString)
        {
            List<Project> projectsDB = new List<Project>();
            if (String.IsNullOrEmpty(searchString))
            {
                projectsDB = ProjectDAL.ListAllProjects();
            }
            else
            {
                projectsDB = ProjectDAL.SearchProjects(searchString);
            }

            List<ProjectViewModel> list = new List<ProjectViewModel>();
            foreach (var project in projectsDB)
            {
                list.Add(new ProjectViewModel(project));
            }
            return View(list);
        }
        //
        // GET: /Admin/Project/Details/5

        public ActionResult Details(int id = 0)
        {
            ProjectViewModel project = new ProjectViewModel(ProjectDAL.FindProject(id));
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // GET: /Admin/Project/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Project/Create

        [HttpPost]
        public ActionResult Create(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ProjectDAL.ProjectExists(model.ProjectName))
                {
                    ViewBag.Error = "A project with that name already exists";
                    return View(model);
                }

                ProjectDAL.AddProject(model.ProjectName, model.Description);

                InfoCardDAL.UpdateInfoCardDescription( 
                    WebSecurity.CurrentUserId,
                    "Added new project: \"" + model.ProjectName + "\""
                    );

                return RedirectToAction("Details", new { id = ProjectDAL.GetProjectId(model.ProjectName) });
            }

            return View(model);
        }

        //
        // GET: /Admin/Project/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ProjectViewModel project = new ProjectViewModel(ProjectDAL.FindProject(id));
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /Admin/Project/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (ProjectDAL.ProjectExists(id, model.ProjectName))
                {
                    ViewBag.Error = "A project with that name already exists";
                    return View(model);
                }

                ProjectDAL.UpdateProject(id, model.ProjectName, model.Description);

                InfoCardDAL.UpdateInfoCardDescription(
                    WebSecurity.CurrentUserId,
                    "Edited project: \"" + model.ProjectName + "\""
                    );

                return RedirectToAction("Details", new { id = model.ProjectId});
            }
            return View(model);
        }

        //
        // GET: /Admin/Project/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ProjectViewModel project = new ProjectViewModel(ProjectDAL.FindProject(id));
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // POST: /Admin/Project/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectDAL.RemoveProject(id);

            InfoCardDAL.UpdateInfoCardDescription(
                    WebSecurity.CurrentUserId,
                    "Deleted project: #" + id
                    );

            return RedirectToAction("Index");
        }
    }
}