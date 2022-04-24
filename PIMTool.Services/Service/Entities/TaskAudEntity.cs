using System;

namespace PIMTool.Services.Service.Entities
{
    public class TaskAudEntity : BaseEntity
    {
        public virtual int ProjectId { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DeadlineDate
        {
            get;
            set;
        }
        public virtual int TaskId
        {   
            get;
            set;
        }
        public virtual string Action { get; set; }
    }
}