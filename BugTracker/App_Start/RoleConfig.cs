using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;
using BugTracker.DAL;

namespace BugTracker
{
    public class RoleConfig
    {
        public static void RegisterRoles()
        {
            if (!WebSecurity.Initialized)
            {
                WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfiles", "UserId",
                                                         "UserName", autoCreateTables: true);
            }


            if (!Roles.RoleExists("Admin"))
            {
                Roles.CreateRole("Admin");
            }

            if (!Roles.RoleExists("User"))
            {
                Roles.CreateRole("User");
            }

            if (!WebSecurity.UserExists("admin"))
            {
                WebSecurity.CreateUserAndAccount(
                    "admin",
                    "administratorska",
                    new
                    {
                        FirstName = "Deyan",
                        LastName = "Hadzhiev",
                        Email = "deyan.z.hadzhiev@gmail.com",
                        PhoneNumber = "+359887931216"
                    });

                InfoCardDAL.AddUserInfoCard(UserProfileDAL.GetUserId("admin"));
                InfoCardDAL.UpdateInfoCardDescription(UserProfileDAL.GetUserId("admin"), "Registration of system's Admin");
            }

            if (!Roles.GetRolesForUser("admin").Contains("Admin"))
            {
                Roles.AddUserToRole("admin", "Admin");
            }
        }
    }
}