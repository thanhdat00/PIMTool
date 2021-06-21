using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using PIMTool.Services.Service;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Repository;

namespace PIMTool.Test
{
    public class ProjectRepositoryTest : BaseTest
    {
        private ProjectDataGenerator _generator;

        protected override INinjectModule GetServiceBindingModule()
            => new ProjectRepositoryTestModule();

        public IProjectRepository ProjectRepository => Kernel.Get<IProjectRepository>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Setup();
            _generator = new ProjectDataGenerator(UnitOfWorkProvider, ProjectRepository);
        }

        [Test]
        public void TestAddProject()
        {
            ProjectEntity project = new ProjectEntity();
            try
            {
                project = _generator.InitProject("Test");

                _generator.AddProject(project);
                Assert.IsTrue(project.Id > 0);

            }
            finally
            {
                if (project.Id > 0)
                {
                    _generator.DeleteProject(project);
                }
            }

        }
    }
}
