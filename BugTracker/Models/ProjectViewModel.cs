using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BugTracker.DAL;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class ProjectViewModel
    {
        [Display(Name = "Project ID")]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public ProjectViewModel() 
        { 
        }

        public ProjectViewModel(int projectId = 0, string projectName = "", string description = "")
        {
            this.ProjectId = projectId;
            this.ProjectName = projectName;
            this.Description = description;
        }

        public ProjectViewModel(Project project)
        {
            this.ProjectId = project.ProjectId;
            this.ProjectName = project.ProjectName;
            this.Description = project.Description;
        }
    }
}