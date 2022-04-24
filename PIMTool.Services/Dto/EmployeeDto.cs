using PIMTool.Services.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Resource
{
    public class EmployeeDto : BaseDto
    {
        public virtual string Visa { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime BirthDate { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}