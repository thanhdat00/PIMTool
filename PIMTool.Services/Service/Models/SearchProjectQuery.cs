namespace PIMTool.Services.Service.Models
{
    public class SearchProjectQuery
    {
        public string SearchCriteria { get; set; } = "none";
        public int SelectedPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}