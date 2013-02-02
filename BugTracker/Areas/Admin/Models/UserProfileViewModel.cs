using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BugTracker.DAL;

namespace BugTracker.Areas.Admin.Models
{
    public class UserProfileViewModel
    {
        [Display(Name="User ID")]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        public UserProfileViewModel(int id, string userName, string email, string firstName = "", string lastName = "", string phoneNumber = "")
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

    public class DetailsByNameViewModel
    {
        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = " The {0} must contain only latin characters and numbers, and must start with a character.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

    }

    public class EditUserProfileViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        [StringLength(64)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(64)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(64)]
        [RegularExpression("^[a-z0-9._]+@[a-z0-9]+.[a-z]+", ErrorMessage = "The {0} is not in the right format: \"example@example.example\" .")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(24)]
        [RegularExpression("^[+]+[0-9]{8,}$", ErrorMessage = "The {0} must contain only numbers and start with \" + \".")]
        [Display(Name = "Phone number (+...)")]
        public string PhoneNumber { get; set; }

        public EditUserProfileViewModel()
        {
        }

        public EditUserProfileViewModel(UserProfileViewModel profile)
        {
            this.UserId = profile.UserId;
            this.UserName = profile.UserName;
            this.FirstName = profile.FirstName;
            this.LastName = profile.LastName;
            this.Email = profile.Email;
            this.PhoneNumber = profile.PhoneNumber;
        }
    }

    public class AddUserProfileViewModel
    {
        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage = " The {0} must contain only latin characters and numbers, and must start with a character.")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [StringLength(64)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [StringLength(64)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(64)]
        [RegularExpression("^[a-z0-9]+@[a-z0-9]+.[a-z]+", ErrorMessage = "The {0} is not in the right format: \"example@example.example\" .")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(24)]
        [RegularExpression("^[+]+[0-9]{8,}$", ErrorMessage = "The {0} must contain only numbers and start with \" + \".")]
        [Display(Name = "Mobile number (+...)")]
        public string MobileNumber { get; set; }
    }
}