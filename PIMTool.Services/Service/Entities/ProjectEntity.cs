using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Services.Service.Entities
{
    public class ProjectEntity : BaseEntity
    {
        public ProjectEntity()
        {
            Task = new List<TaskEntity>();
        }

        [Required, StringLength(100), Display(Name = "Project Name")]
        public virtual string Name
        {
            get;
            set;
        }

        [Required, Display(Name = "Start Date")]
        public virtual DateTime StartDate
        {
            get;
            set;
        }

        [Display(Name = "Finish Date")]
        public virtual DateTime? FinishDate
        {
            get;
            set;
        }

        public virtual IList<TaskEntity> Task { get; set; }
    }
}