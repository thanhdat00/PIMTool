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

        }

    }


}
