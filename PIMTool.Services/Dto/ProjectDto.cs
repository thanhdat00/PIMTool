﻿using PIMTool.Client.Dictionary;
using System;

namespace PIMTool.Services.Resource
{
    public class ProjectDto : BaseDto
    {
        public bool IsSelected { get; set; } = false;
        public virtual string Name
        {
            get;
            set;
        }
        public virtual int ProjectNumber { get; set; }
        public virtual string Customer { get; set; }
        public virtual EStatusType Status { get; set; }
        public virtual string Member { get; set; }
        public virtual DateTime StartDate
        {
            get;
            set;
        }
        public virtual DateTime? FinishDate
        {
            get;
            set;
        }
        public int GroupId { get; set; }
        //public virtual ICollection<EmployeeEntity> Employees { get; set; }      
    }
}