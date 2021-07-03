namespace PIMTool.Common
{
    public static class RouteConstants
    {
        public const string ProjectApi = "api/project";

        public const string GetAllProjects = "GetAll";
        public const string GetProjectClient = "GetProject/{0}";
        public const string GetProjectServer = "GetProject/{projectId}";
        public const string DeleteProject = "DeleteProject/{projectNumber}";
        public const string AddProject = "AddProject";
        public const string UpdateProject = "UpdateProject";
        public const string SearchProject = "SearchProject";

        public const string EmployeeApi = "api/employee";

        public const string GetAllEmployees = "GetAll";
    }
}
