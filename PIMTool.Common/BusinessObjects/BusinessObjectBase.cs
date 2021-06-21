using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Common.BusinessObjects
{
    public class BusinessObjectBase
    {
        public int Id { get; set; }
        public int RowVersion { get; set; }
    }
}
