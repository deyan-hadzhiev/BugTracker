using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace BugTracker.Models
{

    public class UserProfile
    {
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [StringLength(64, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [RegularExpression("^[a-zA-Z]+[a-zA-Z0-9]*$", ErrorMessage=" The {0} must contain only latin characters and numbers, and must start with a character.")]
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
        [RegularExpression("^[a-z0-9._]+@[a-z0-9]+.[a-z]+",ErrorMessage="The {0} is not in the right format: \"example@example.example\" .")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(24)]
        [RegularExpression("^[+]+[0-9]{8,}$", ErrorMessage="The {0} must contain only numbers and start with \" + \".")]
        [Display(Name = "Mobile number (+...)")]
        public string MobileNumber { get; set; }
    }

}
