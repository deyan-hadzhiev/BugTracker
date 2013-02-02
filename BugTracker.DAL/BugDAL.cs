using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BugTracker.DAL
{
    public static class BugDAL
    {
        public static List<Bug> ListAllBugs()
        {
            BugTrackerEntities db = new BugTrackerEntities();

            var bugs = db.Bugs.Where( x => x.Status != "Deleted" && x.Status != "Closed");

            return bugs.ToList();
        }

        public static List<Bug> ListBugsByProjectId(int projectId)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            var bugs = db.Bugs.Where(x => x.Status != "Deleted" && x.Status != "Closed" && x.ProjectId == projectId);

            return bugs.ToList();
        }

        public static Bug GetBugById(int bugId)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            Bug bug = db.Bugs.Find(bugId);
            if (bug.Status == "Deleted")
            {
                return null;
            }
            return bug;
        }

        public static void AddBug(int userId, int projectId, string priority, string status, DateTime discoverDate, string description)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            UserProfile user = db.UserProfiles.FirstOrDefault(x => x.UserId == userId);
            Project project = db.Projects.FirstOrDefault(x => x.ProjectId == projectId);

            db.Bugs.Add(new Bug
            {
                UserProfile = user,
                Project = project,
                Priority = priority,
                Status = status,
                DiscoverDate = discoverDate,
                Description = description

            });

            db.SaveChanges();
        }

        public static void UpdateBug(int bugId, string priority, string status, string description)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            Bug bug = db.Bugs.Find(bugId);

            bug.Priority = priority;
            bug.Status = status;
            bug.Description = description;

            db.Entry(bug).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static void RemoveBug(int bugId)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            Bug bug = db.Bugs.Find(bugId);
            
            bug.Status = "Deleted";
            db.Entry(bug).State = EntityState.Modified;

            db.SaveChanges();
        }
    }
}
