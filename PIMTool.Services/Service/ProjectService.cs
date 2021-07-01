using System;
using System.Collections.Generic;
using AutoMapper;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Communication;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Pattern;
using PIMTool.Services.Service.Repository;

namespace PIMTool.Services.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        public ProjectService(IUnitOfWorkProvider unitOfWorkProvider,
            IProjectRepository projectRepository)
        {
            _unitOfWorkProvider = unitOfWorkProvider;
            _projectRepository = projectRepository;
        }

        public IList<Entities.ProjectEntity> GetAll()
        {
            IList<Entities.ProjectEntity> result;
            using (var scope = _unitOfWorkProvider.Provide())
            {
                result = _projectRepository.GetAll();
                scope.Complete();
            }
            return result;
        }

        public Entities.ProjectEntity GetById(int projectId)
        {
            ProjectEntity result;
            using (var scope = _unitOfWorkProvider.Provide())
            {
                result = _projectRepository.GetById(projectId);
                scope.Complete();
            }
            return result;
        }

        public ProjectResponse DeleteById(int projectId)
        {
            using (var scope = _unitOfWorkProvider.Provide())
            {
                var exsitingProject = _projectRepository.GetById(projectId);

                if (exsitingProject == null)
                    return new ProjectResponse("Project not found");

                try
                {
                    _projectRepository.Delete(exsitingProject);
                    scope.Complete();
                    return new ProjectResponse(exsitingProject);
                }
                catch (Exception ex)
                {
                    return new ProjectResponse($"An error occurred when deleting the project: {ex.Message}");
                }

            }
        }

        // Delete a project using project number
        public ProjectResponse DeleteByProjectNumber(int value)
        {
            using (var scope = _unitOfWorkProvider.Provide())
            {
                var exsitingProject = _projectRepository.UpdateProject(value);

                if (exsitingProject == null)
                    return new ProjectResponse("Project not found");

                try
                {
                    _projectRepository.Delete(exsitingProject);
                    scope.Complete();
                    return new ProjectResponse(exsitingProject);
                }
                catch (Exception ex)
                {
                    return new ProjectResponse($"An error occurred when deleting the project: {ex.Message}");
                }

            }
        }

        public ProjectResponse Save(SaveProjectDto project)
        {
            using (var scope = _unitOfWorkProvider.Provide())
            {
                try
                {
                    var newProject = _projectRepository.NewProject(project);
                    _projectRepository.Add(newProject);
                    scope.Complete();
                    return new ProjectResponse(newProject);
                }
                catch (Exception ex)
                {
                    return new ProjectResponse($"An error occurred when saving the project: {ex.Message}");
                }
            }
        }

        public ProjectResponse Update(SaveProjectDto project)
        {
            using (var scope = _unitOfWorkProvider.Provide())
            {
                try
                {
                    var updateProject = _projectRepository.UpdateProject(project.ProjectNumber);
                    updateProject.Name = project.Name;
                    updateProject.Customer = project.Customer;
                    updateProject.Status = project.Status;
                    updateProject.GroupId = project.GroupId;
                    updateProject.StartDate = project.StartDate;
                    updateProject.FinishDate = project.FinishDate;
                    _projectRepository.SaveOrUpdate(updateProject);
                    scope.Complete();
                    return new ProjectResponse(updateProject);
                }
                catch (Exception ex)
                {
                    return new ProjectResponse($"An error occurred when updating the project: {ex.Message}");
                }
            }
        }
    }
}