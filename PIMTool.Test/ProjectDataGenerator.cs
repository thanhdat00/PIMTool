using Microsoft.VisualStudio.TestTools.UnitTesting;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Pattern;
using PIMTool.Services.Service.Repository;
using System;

namespace PIMTool.Test
{
    public class ProjectDataGenerator
    {
        public IUnitOfWorkProvider UnitOfWorkProvider { get; set; }
        public IProjectRepository ProjectRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IGroupRepository GroupRepository { get; set; }
        public ITaskAudRepository TaskAudRepository { get; set; }

        public ProjectDataGenerator(IUnitOfWorkProvider unitOfWorkProvider, IProjectRepository projectRepository, 
            IEmployeeRepository employeeRepository, IGroupRepository groupRepository, ITaskAudRepository taskAudRepository)
        {
            UnitOfWorkProvider = unitOfWorkProvider;
            ProjectRepository = projectRepository;
            EmployeeRepository = employeeRepository;
            GroupRepository = groupRepository;
            TaskAudRepository = taskAudRepository;
        }

        public EmployeeEntity InitEmployee()
        {
            EmployeeEntity res; 
            using (var scope = UnitOfWorkProvider.Provide())
            {
                res = EmployeeRepository.GetById(1);
                scope.Complete();
            }
            return res;
        }

        public GroupEntity InitGroup()
        {
            GroupEntity res;
            using (var scope = UnitOfWorkProvider.Provide())
            {
                res = GroupRepository.GetById(1);
                scope.Complete();
            }
            return res;
        }

        public ProjectEntity InitProject(string projectName,int count)
        {
            GroupEntity mockGroup = InitGroup();
            return new ProjectEntity()
            {
                GroupId = 1,
                ProjectNumber = 119 + count,
                Name = projectName,
                FinishDate = null,
                StartDate = DateTime.Now.AddYears(-1),
                Customer = "ThanhDat",
                Status = Client.Dictionary.EStatusType.NEW,
            };
        }

        public TaskEntity AddTask(string taskName, ProjectEntity project)
        {
            var newTask = new TaskEntity()
            {
                Name = taskName,
                Project = project
            };
            project.Task.Add(newTask);
            return newTask;
        }


        public TaskAudEntity AddTaskAud(TaskEntity task, string action)
        {
            var newTaskAud = new TaskAudEntity()
            {
                Name = task.Name,
                ProjectId = task.Project.Id,
                TaskId = task.Id,
                DeadlineDate = task.DeadlineDate,
                Action = action,
            };
            using (var scope = UnitOfWorkProvider.Provide())
            {
                TaskAudRepository.Add(newTaskAud);
                scope.Complete();
            }
            
            return newTaskAud;
        }


        public void AddProject(ProjectEntity project)
        {
            using (var scope = UnitOfWorkProvider.Provide())
            {
                ProjectRepository.Add(project);

                Assert.IsTrue(project.Id > 0);

                scope.Complete();
            }
        }

        public void Merge(ProjectEntity project)
        {
            using (var scope = UnitOfWorkProvider.Provide())
            {
                ProjectRepository.Merge(project);

                Assert.IsTrue(project.Id > 0);

                scope.Complete();
            }
        }

        public TaskAudEntity GetTaskAud(int id)
        {
            TaskAudEntity result;
            using (var scope = UnitOfWorkProvider.Provide())
            {
                result = TaskAudRepository.GetById(id);

                Assert.IsTrue(result.Id > 0);

                scope.Complete();
            }
            return result;
        }

        public TaskEntity GetTask(int taskId)
        {
            TaskEntity result;
            using (var scope = UnitOfWorkProvider.Provide())
            {
                result = ProjectRepository.GetOtherById<TaskEntity>(taskId);

                Assert.IsTrue(result.Id > 0);

                scope.Complete();
            }
            return result;
        }
        public TaskAudEntity MergeTaskAud(TaskAudEntity task)
        {

            using (var scope = UnitOfWorkProvider.Provide())
            {
                task = TaskAudRepository.Merge(task);

                Assert.IsTrue(task.Id > 0);

                scope.Complete();
            }
            return task;
        }

        public TaskEntity MergeTask(TaskEntity task)
        {
            using (var scope = UnitOfWorkProvider.Provide())
            {
                task = ProjectRepository.MergeOther(task);

                Assert.IsTrue(task.Id > 0);

                scope.Complete();
            }
            return task;
        }

        public void DeleteProject(ProjectEntity project)
        {
            using (var scope = UnitOfWorkProvider.Provide())
            {
                var projectDb = ProjectRepository.GetById((project.Id));
                ProjectRepository.Delete(projectDb);

                scope.Complete();
            }
        }
    }
}
