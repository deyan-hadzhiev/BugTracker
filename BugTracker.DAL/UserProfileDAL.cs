using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.DAL
{
    public static class UserProfileDAL
    {
        public static List<UserProfile> ListAllUserProfiles()
        {
            BugTrackerEntities db = new BugTrackerEntities();

            List<UserProfile> list = db.UserProfiles.ToList();

            return list;
        }

        public static UserProfile SelectUserById(int userId)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            UserProfile user = db.UserProfiles.Find(userId);

            return user;
        }

        public static void UpdateUser(int userId, string email, string firstName, string lastName, string phoneNumber)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            UserProfile user = db.UserProfiles.Find(userId);
            user.Email = email;
            user.FirstName = firstName;
            user.LastName = lastName;
            user.PhoneNumber = phoneNumber;

            db.Entry(user).State = EntityState.Modified;

            db.SaveChanges();
        }

        public static bool UserExists(string userName)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            return db.UserProfiles.Any(x => x.UserName == userName);
        }

        public static int GetUserId(string userName)
        {
            BugTrackerEntities db = new BugTrackerEntities();
            UserProfile profile = db.UserProfiles.FirstOrDefault(x => x.UserName == userName);
            return profile.UserId;
        }

        public static void RemoveUserFromBugs(int userId)
        {
            BugTrackerEntities db = new BugTrackerEntities();

            IQueryable<Bug> bugs = db.Bugs.Where(x => x.UserId == userId);

            foreach (var bug in bugs)
            {
                bug.UserId = null;
                db.Entry(bug).State = EntityState.Modified;
            }

            db.SaveChanges();
        }
    }
}
