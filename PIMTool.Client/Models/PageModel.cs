using System.Windows.Input;

namespace PIMTool.Client.Models
{
    public class PageModel
    {
        public string PageNumber { get; set; }
        public ICommand PageSelectCommand { get; set; }
        public PageModel(string pageNumber, ICommand pageSelectCommand)
        {
            PageNumber = pageNumber;
            PageSelectCommand = pageSelectCommand;
        }
    }
}
