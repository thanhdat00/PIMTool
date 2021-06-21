using Newtonsoft.Json;
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
            Employees = new List<EmployeeEntity>();
            //Group = new GroupEntity();
        }

        [Required, StringLength(100), Display(Name = "Project Name")]
        public virtual string Name
        {
            get;
            set;
        }

        public virtual int ProjectNumber { get; set; }
        public virtual string Customer { get; set; }
        public virtual string Status { get; set; }
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

        public virtual ICollection<TaskEntity> Task { get; set; }
        
        public virtual ICollection<EmployeeEntity> Employees { get; set; }
        public virtual int GroupId { get; set; }

        public virtual string GetMember()
        {
            string res = "";
            foreach(var employee in Employees)
            {
                res += employee.Visa;
                res += ",";
            }
            return res;
        }
    }
}