using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Service.Entities
{
    [Serializable]
    public class EmployeeEntity : BaseEntity
    {
        public EmployeeEntity()
        {
            Projects = new List<ProjectEntity>();
        }
        public EmployeeEntity(string visa, string firstName, string lastName, DateTime birthDate)
        {
            Visa = visa;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }
        [Required, StringLength(3), Display(Name ="Employee Visa")]
        public virtual string Visa { get; set; }

        [Required, StringLength(50), Display(Name ="Employee First Name")]
        public virtual string FirstName { get; set; }

        [Required, StringLength(50), Display(Name ="Employee Last Name")]
        public virtual string LastName { get; set; }

        [Required, Display(Name ="Employee BirthDay")]
        public virtual DateTime BirthDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectEntity> Projects { get; set; }
    }
}