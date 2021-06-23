using System.ComponentModel;

namespace PIMTool.Client.Dictionary
{
    public enum EStatusType
    {
        [Description("New")]
        NEW = 1,

        [Description("Plan")]
        PLA = 2,

        [Description("In Progress")]
        INP = 3,

        [Description("Finished")]
        FIN = 4
    }
}
