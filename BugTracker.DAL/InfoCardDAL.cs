using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BugTracker.DAL
{
    public static class InfoCardDAL
    {
        public static void AddUserInfoCard(int userId)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            InfoCard iCard = new InfoCard 
            {
                UserProfile = db.UserProfiles.Find(userId),
                NumberOfBugs = 0,
                NumberOfProjects = 0,
                LastActivity = DateTime.Now,
                LastAction = "Registration of the Tester."
            };

            db.InfoCards.Add(iCard);
            db.SaveChanges();
        }

        public static void RemoveUserInfoCard(int userId)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            InfoCard iCard = db.InfoCards.Find( userId );
            db.InfoCards.Remove(iCard);

            db.SaveChanges();
        }

        public static void UpdateInfoCardDescription(int iCardId, string description)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            InfoCard iCard = db.InfoCards.Find(iCardId);
            iCard.LastActivity = DateTime.Now;
            iCard.LastAction = description;

            db.Entry(iCard).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static void AddBugToInfoCard(int userId, int projectId)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            InfoCard iCard = db.InfoCards.Find(userId);
            Project project = db.Projects.Find(projectId);

            if (!iCard.Projects.Contains(project))
            {
                iCard.NumberOfProjects += 1;
                iCard.Projects.Add(project);
            }

            iCard.NumberOfBugs += 1;

            iCard.LastActivity = DateTime.Now;

            iCard.LastAction = "Added new bug for project \"" + project.ProjectName + "\"";

            db.Entry(iCard).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static void UpdateBugInfoCard(int userId, int bugId, string action)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            InfoCard iCard = db.InfoCards.Find(userId);

            iCard.LastActivity = DateTime.Now;

            iCard.LastAction = action + " bug #" + bugId;

            db.Entry(iCard).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static InfoCard GetUserInfoCard(UserProfile user)
        {
            InfoCard iCard = user.InfoCard;
            return iCard;
        }
    }
}
