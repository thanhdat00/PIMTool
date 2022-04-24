using PIMTool.Services.Service;
using PIMTool.Services.Service.Repository;

namespace PIMTool.Test
{
    public class ProjectRepositoryTestModule : BaseRepositoryTestModule
    {
        public override void Load()
        {
            LoadModule();
            Bind<IProjectRepository>().To<ProjectRepository>();
            Bind<IProjectService>().To<ProjectService>();
            Bind<IEmployeeRepository>().To<EmployeeRepository>();
            Bind<IGroupRepository>().To<GroupRepository>();
            Bind<ITaskAudRepository>().To<TaskAudRepository>();
        }

    }


}
