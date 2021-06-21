using System.Collections.Generic;
 
namespace PIMTool.Client.Dictionary
{
    public class StatusDictionary
    {
        public Dictionary<object, object> Status = new Dictionary<object, object>();
        public StatusDictionary()
        {
            Status.Add("NEW", "New");
            Status.Add("PLA", "Planned");
            Status.Add("INP", "In Progress");
            Status.Add("FIN", "Finished");
            Status.Add(0, "NEW");
            Status.Add(1, "PLA");
            Status.Add(2, "INP");
            Status.Add(3, "FIN");
        }
    }
}
