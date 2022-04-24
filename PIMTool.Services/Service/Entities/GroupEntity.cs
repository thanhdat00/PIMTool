using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Service.Entities
{
    public class GroupEntity : BaseEntity
    {
        public virtual EmployeeEntity GroupLeader { get; set; }
        public virtual ICollection<ProjectEntity> Projects { get; set; } = new List<ProjectEntity>();
    }
}