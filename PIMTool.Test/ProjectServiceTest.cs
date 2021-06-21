using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Util;
using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using PIMTool.Services.Service;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Repository;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace PIMTool.Test
{
    [TestClass]
    public class ProjectServiceTest : BaseTest
    {
        protected static readonly ILog log = LogManager.GetLogger("PIMToolApp");
        private ProjectDataGenerator _generator;

        protected override INinjectModule GetServiceBindingModule()
            => new ProjectRepositoryTestModule();

        public IProjectRepository ProjectRepository => Kernel.Get<IProjectRepository>();
        public IProjectService ProjectService => Kernel.Get<IProjectService>();

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Setup();
            _generator = new ProjectDataGenerator(UnitOfWorkProvider, ProjectRepository);
        }

        [Test]
        public void TestListNumberOfTasks()
        {
            ProjectEntity project1 = new ProjectEntity();
            try
            {
                project1 = _generator.InitProject("Project1");


                var task1_1 = _generator.AddTask("Task1_1", project1);
                var task2_1 = _generator.AddTask("Task2_1", project1);
                var task3_1 = _generator.AddTask("Task3_1", project1);

                _generator.AddProject(project1);

                Assert.IsTrue(project1.Id > 0);
                Assert.IsTrue(task1_1.Id > 0);
                Assert.IsTrue(task2_1.Id > 0);
                Assert.IsTrue(task3_1.Id > 0);
                IList<ProjectEntity> listDbProjects;
                using (var scope = UnitOfWorkProvider.Provide())
                {
                    listDbProjects = ProjectService.GetAll();
                    scope.Complete();
                }
                // get task
                Assert.IsTrue(listDbProjects.Any(i => i.Name == "Project1"));
                var countTask = listDbProjects.FirstOrDefault(i => i.Name == "Project1").Task.Count;
                Assert.IsTrue(countTask == 3);


                project1 = _generator.InitProject("Test2");
                _generator.AddProject(project1);
                Assert.IsTrue(project1.Id > 0);

            }
            finally
            {
                if (project1.Id > 0)
                {
                    _generator.DeleteProject(project1);
                }
            }

        }


        [Test]
        public void TestShowTopTenNewTasksEachProject()
        {
            List<ProjectEntity> projectList = new List<ProjectEntity>();
            try
            {
                int numberProjectsCreate = 100;
                for (int i = 1; i <= numberProjectsCreate; i++)
                {
                    var project = _generator.InitProject("Project" + i);
                    _generator.AddTask("Task1_" + i, project);
                    _generator.AddTask("Task2_" + i, project);
                    _generator.AddTask("Task3_" + i, project);

                    _generator.AddProject(project);
                    projectList.Add(project);
                }
                // ReSharper disable once CollectionNeverQueried.Local
                Dictionary<long, List<TaskEntity>> toptenTaskEachProjects = new Dictionary<long, List<TaskEntity>>();
                IList<ProjectEntity> listDbProjects = new List<ProjectEntity>();
                using (var scope = UnitOfWorkProvider.Provide())
                {
                    listDbProjects = ProjectService.GetAll();
                    listDbProjects.ForEach(itemProject =>
                    {
                        List<TaskEntity> tasks = itemProject.Task.OrderByDescending(i => i.Id).Take(10).ToList();
                        toptenTaskEachProjects.Add(itemProject.Id, tasks);

                    });
                    scope.Complete();
                }

                Assert.IsTrue(listDbProjects.Any(i => i.Name == "Project10"));
            }
            finally
            {
                foreach (ProjectEntity project in projectList)
                {
                    _generator.DeleteProject(project);
                }
            }

        }

        [Test]
        public void TestUpdateDeadline()
        {
            ProjectEntity project1 = new ProjectEntity();
            try
            {
                project1 = _generator.InitProject("Project1");


                var task1_1 = _generator.AddTask("Task1_1", project1);
                var task2_1 = _generator.AddTask("Task2_1", project1);
                var task3_1 = _generator.AddTask("Task3_1", project1);

                _generator.AddProject(project1);

                Assert.IsTrue(project1.Id > 0);
                Assert.IsTrue(task1_1.Id > 0);
                Assert.IsTrue(task2_1.Id > 0);
                Assert.IsTrue(task3_1.Id > 0);
                var utcDate = DateTime.Now;

                using (var scope = UnitOfWorkProvider.Provide())
                {
                    var dbProject1 = ProjectService.GetById(project1.Id);
                    dbProject1.Task[0].DeadlineDate = utcDate;
                    _generator.Merge(dbProject1);
                }

                // load again and check finishDate update successfuly
                using (var scope = UnitOfWorkProvider.Provide())
                {
                    var dbProject1 = ProjectService.GetById(project1.Id);
                    Assert.IsTrue(dbProject1.Task[0].DeadlineDate == utcDate);
                }

            }
            finally
            {
                if (project1.Id > 0)
                {
                    _generator.DeleteProject(project1);
                }
            }
        }



        [Test]
        public void TestUpdateTaskProject()
        {
            ProjectEntity project1 = new ProjectEntity();

            try
            {
                project1 = _generator.InitProject("Project1");
                var task = _generator.AddTask("Task1_1", project1);
                TaskAudEntity taskAud = null;
                _generator.AddProject(project1);

                var utcDate = DateTime.Now;
                try
                {
                    using (var globalScope = UnitOfWorkProvider.Provide())
                    {
                        using (var scope = UnitOfWorkProvider.Provide())
                        {
                            var taskDb = _generator.GetTask(task.Id);
                            taskDb.DeadlineDate = DateTime.MinValue;
                            _generator.MergeTask(taskDb);
                            scope.Complete();
                        }

                        using (var scope = UnitOfWorkProvider.Provide())
                        {
                            var taskDb = _generator.GetTask(task.Id);
                            taskAud = _generator.AddTaskAud(taskDb, taskDb.RowVersion > task.RowVersion ? "UpdatedOK" : "UpdateKO");
                            taskAud = _generator.MergeTaskAud(taskAud);


                            scope.Complete();
                        }
                        globalScope.Complete();
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
                using (var scope = UnitOfWorkProvider.Provide())
                {
                    Assert.IsNotNull(taskAud);
                    taskAud = _generator.GetTaskAud(taskAud.Id);
                    Assert.IsTrue(taskAud.Name != null);
                }


            }
            finally
            {
                if (project1.Id > 0)
                {
                    _generator.DeleteProject(project1);
                }
            }
        }

    }
}
