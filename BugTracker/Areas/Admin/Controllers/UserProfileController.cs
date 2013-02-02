using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using BugTracker.Areas.Admin.Models;
using System.Web.Security;
using BugTracker.DAL;
using BugTracker.Filters;

namespace BugTracker.Areas.Admin.Controllers
{
    [InitializeSimpleMembership]
    public class UserProfileController : AdminController
    {
        public ActionResult Index()
        {
            List<UserProfile> users = BugTracker.DAL.UserProfileDAL.ListAllUserProfiles();
            List<UserProfileViewModel> list = new List<UserProfileViewModel>();
            foreach (var item in users)
            {
                list.Add(new UserProfileViewModel(item));
            }

            return View(list);
        }

        public ActionResult ListAllUsers()
        {
            List<UserProfile> users = BugTracker.DAL.UserProfileDAL.ListAllUserProfiles();
            List<UserProfileViewModel> list = new List<UserProfileViewModel>();
            foreach (var item in users)
            {
                list.Add(new UserProfileViewModel(item));
            }

            return View(list);
        }

        public ActionResult DetailsUser(int id)
        {
            UserProfileViewModel user = new UserProfileViewModel(UserProfileDAL.SelectUserById(id));
            return View(user);
        }

        public ActionResult DetailsUserByName()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DetailsUserByName(DetailsByNameViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserProfileDAL.UserExists(model.UserName))
                {
                    return RedirectToAction("DetailsUser", new { id = UserProfileDAL.GetUserId(model.UserName) });
                }
                else
                {
                    ViewBag.Error = "This user name does not exist";
                    return View(model);
                }
            }
            return View(model);
        }

        public ActionResult DeleteUser(int id)
        {
            UserProfileViewModel user = new UserProfileViewModel(UserProfileDAL.SelectUserById(id));
            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        public ActionResult DeleteUserConfirmed(int id)
        {
            UserProfileViewModel user = new UserProfileViewModel(UserProfileDAL.SelectUserById(id));

            InfoCardDAL.UpdateInfoCardDescription(
                    WebSecurity.CurrentUserId,
                    "Deleted user: \"" + user.UserName + "\""
                    );

            UserProfileDAL.RemoveUserFromBugs(id);

            InfoCardDAL.RemoveUserInfoCard(id);

            Roles.RemoveUserFromRoles(user.UserName, Roles.GetRolesForUser(user.UserName));

            ((SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.UserName);

            ((SimpleMembershipProvider)Membership.Provider).DeleteUser(user.UserName, true);

            return RedirectToAction("Index");
        }

        public ActionResult EditUser(int id)
        {
            EditUserProfileViewModel user = new EditUserProfileViewModel(new UserProfileViewModel(UserProfileDAL.SelectUserById(id)));
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(int id, EditUserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserProfileDAL.UpdateUser(id, model.Email, model.FirstName, model.LastName, model.PhoneNumber);

                InfoCardDAL.UpdateInfoCardDescription(
                    WebSecurity.CurrentUserId,
                    "Edited user: \"" + model.UserName + "\""
                    );

                return RedirectToAction("DetailsUser", new { id = model.UserId });
            }
            return View(model);
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(AddUserProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    if (UserProfileDAL.UserExists(model.UserName))
                    {
                        ViewBag.Error = "This user name is already in use";
                        return View(model);
                    }
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password,
                        new
                        {
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            PhoneNumber = model.MobileNumber
                        }, false);

                    Roles.AddUserToRole(model.UserName, "User");

                    InfoCardDAL.AddUserInfoCard(UserProfileDAL.GetUserId(model.UserName));

                    InfoCardDAL.UpdateInfoCardDescription(
                        WebSecurity.CurrentUserId,
                        "Created user: \"" + model.UserName + "\""
                    );
                    
                    return RedirectToAction("DetailsUser", new{id = UserProfileDAL.GetUserId(model.UserName)});
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion

    }
}
