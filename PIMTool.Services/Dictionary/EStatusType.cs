using System.ComponentModel;

namespace PIMTool.Client.Dictionary
{
    public enum EStatusType
    {
        [Description("New")]
        NEW = 0,

        [Description("Plan")]
        PLA = 1,

        [Description("In Progress")]
        INP = 2,

        [Description("Finished")]
        FIN = 3
    }
}
