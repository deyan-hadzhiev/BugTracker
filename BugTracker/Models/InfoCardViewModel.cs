using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.DAL;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class InfoCardViewModel
    {
        [Display(Name = "User ID")]
        public int UserId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name="Number Of Projects")]
        public int NumberOfProjects { get; set; }
        
        [Display(Name="Number Of Bugs")]
        public int NumberOfBugs { get; set; }

        [Display(Name="Date of last activity")]
        [DataType(DataType.DateTime)]
        public DateTime LastActivity { get; set; }

        [Display(Name="Description of last action")]
        [DataType(DataType.MultilineText)]
        public string LastAction { get; set; }

        public InfoCardViewModel(int userId ,int numProjects, int numBugs, DateTime lastActivity, string lastAction)
        {
            this.UserId = userId;
            this.UserName = UserProfileDAL.SelectUserById(userId).UserName;
            this.NumberOfProjects = numProjects;
            this.NumberOfBugs = numBugs;
            this.LastActivity = lastActivity;
            this.LastAction = lastAction;
        }

        public InfoCardViewModel( InfoCard iCard)
        {
            this.UserId = iCard.UserId;
            this.UserName = iCard.UserProfile.UserName;
            this.NumberOfProjects = iCard.NumberOfProjects;
            this.NumberOfBugs = iCard.NumberOfBugs;
            this.LastActivity = iCard.LastActivity;
            this.LastAction = iCard.LastAction;
        }
    }
}