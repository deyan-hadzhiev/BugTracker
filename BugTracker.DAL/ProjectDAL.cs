using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BugTracker.DAL
{
    public static class ProjectDAL
    {
        public static List<Project> ListAllProjects()
        {
            BugTrackerEntities db = new BugTrackerEntities();
            return db.Projects.ToList();
        }

        public static Project FindProject(int id)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            return db.Projects.Find(id);
        }

        public static List<Project> SearchProjects(string filter)
        {
            if (filter == "")
            {
                return ListAllProjects();
            }
            
            BugTrackerEntities db = new BugTrackerEntities();
            
            return db.Projects.Where(x => x.ProjectName.ToLower().Contains(filter.ToLower())).ToList();
        }

        public static void AddProject(string projectName, string description)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            Project pr = new Project { ProjectName = projectName, Description = description };
            db.Projects.Add(pr);
            
            db.SaveChanges();
        }

        public static void UpdateProject(int projectId, string projectName, string description)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            Project project = db.Projects.Find(projectId);

            project.ProjectName = projectName;
            project.Description = description;

            db.Entry(project).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static void RemoveProject(int projectId)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            
            Project project = db.Projects.Find(projectId);

            IQueryable<Bug> bugs = db.Bugs.Where(x => x.ProjectId == projectId);
            foreach (var bug in bugs)
            {
                db.Bugs.Remove(bug);
            }

            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public static bool ProjectExists(string projectName)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            return db.Projects.Any(x => x.ProjectName == projectName);
        }

        public static int GetProjectId(string projectName)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            Project project = db.Projects.FirstOrDefault(x => x.ProjectName == projectName);
            return (project != null) ? project.ProjectId : 0;
        }

        public static bool ProjectExists(int id, string projectName)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            Project project = db.Projects.FirstOrDefault(x => x.ProjectName == projectName);

            if (project != null)
            {
                return project.ProjectId != id;
            }
            else
            {
                return false;
            }
        }
    }
}
