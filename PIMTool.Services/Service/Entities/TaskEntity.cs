using System;

namespace PIMTool.Services.Service.Entities
{
    public class TaskEntity : BaseEntity
    {
        public virtual ProjectEntity Project { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime? DeadlineDate
        {
            get;
            set;
        }
    }
}