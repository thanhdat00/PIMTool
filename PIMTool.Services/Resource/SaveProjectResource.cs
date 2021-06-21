using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Resource
{
    public class SaveProjectResource
    {
        public virtual int GroupId { get; set; }
        public virtual int ProjectNumber { get; set; }
        public virtual string Name
        {
            get;
            set;
        }
        public virtual string Customer { get; set; }
        public virtual string Status { get; set; }
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
        public virtual string Member { get; set; }
    }
}