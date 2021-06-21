using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Common.BusinessObjects
{
    public class Project : BusinessObjectBase
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
    }
}
