using PIMTool.Services.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Resource
{
    public class EmployeeResource : BaseDto
    {
        public virtual string Visa { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public virtual ICollection<ProjectEntity> ProjectList { get; set; } = new List<ProjectEntity>();
    }
}