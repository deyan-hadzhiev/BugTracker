using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.DAL;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class UserProfileViewModel
    {
        [Display(Name="User ID")]
        public int UserId { get; set; }

        [Display(Name="User Name")]
        public string UserName { get; set; }

        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Display(Name="E-mail")]
        public string Email { get; set; }

        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        public UserProfileViewModel(int id,string userName, string email, string firstName = "", string lastName = "", string phoneNumber = "")
        {
            this.UserId = id;
            this.UserName = userName;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
        }

        public UserProfileViewModel(BugTracker.DAL.UserProfile profile)
        {
            this.UserId = profile.UserId;
            this.UserName = profile.UserName;
            this.FirstName = profile.FirstName;
            this.LastName = profile.LastName;
            this.Email = profile.Email;
            this.PhoneNumber = profile.PhoneNumber;
        }
    }
}