using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;
using BugTracker.DAL;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class BugViewModel
    {
        public int BugId { get; set; }
        public int TesterId { get; set; }
        public int ProjectId { get; set; }
        public int StatusId { get; set; }
        public int PriorityId { get; set; }

        [Display(Name="Tester")]
        public string TesterName { get; set; }

        [Display(Name="Project")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name="Priority")]
        public string Priority { get; set; }

        [Required]
        [Display(Name="Status")]
        public string Status { get; set; }

        [Display(Name="Date Of Discovery")]
        [DataType(DataType.DateTime)]
        public DateTime DiscoverDate { get; set; }

        [Required]
        [Display(Name="Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public BugViewModel()
        {
        }

        public BugViewModel(Bug bug)
        {
            this.BugId = bug.BugId;
            this.TesterId = ((bug.UserId == null) ? (0) : ((int) bug.UserId) );
            this.TesterName = ((bug.UserId == null) ? ("No Author") : (bug.UserProfile.UserName));
            this.ProjectId = bug.ProjectId;
            this.ProjectName = bug.Project.ProjectName;
            this.Priority = bug.Priority;
            this.PriorityId = GetPriorityId(this.Priority);
            this.Status = bug.Status;
            this.StatusId = GetStatusId(this.Status);
            this.DiscoverDate = bug.DiscoverDate;
            this.Description = bug.Description;
        }

        //I know this isn't the best way, but I'm out of time
        //I think it would be better to implement the states and priorities in the DataBase
        private int GetStatusId(string str)
        {
            switch (str)
            {
                case "New": return 1;
                case "In Progress": return 2;
                case "Fixed": return 3;
                case "Closed": return 4;
                default: return 0;
            }
        }

        private int GetPriorityId(string str)
        {
            switch (str)
            {
                case "Critical": return 1;
                case "High": return 2;
                case "Normal": return 3;
                case "Low": return 4;
                default: return 0;
            }
        }
    }

    public class CreateBugViewModel
    {
        [Required]
        public int ProjectId { get; set; }

        [Display(Name="Project")]
        public string ProjectName { get; set; }

        public int TesterId { get; set; }
        
        [Display(Name="Tester")]
        public string TesterName { get; set; }

        [Required]
        [Display(Name="Priority")]
        public string Priority { get; set; }

        [Required]
        [Display(Name="Status")]
        public string Status { get; set; }

        [Display(Name="Date Of Discovery")]
        public DateTime DiscoverDate { get; set; }

        [Required]
        [Display(Name="Description")]
        public string Description { get; set; }

        public CreateBugViewModel()
        {
            this.TesterId = WebSecurity.CurrentUserId;
            this.TesterName = WebSecurity.CurrentUserName;
            this.Priority = "Normal";
            this.Status = "New";
            this.DiscoverDate = DateTime.Now;
        }
    }
}