using AutoMapper;
using PIMTool.Client.Dictionary;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Generic;
using PIMTool.Services.Service.Models;
using System;
using System.Collections.Generic;

namespace PIMTool.Services.Service.Repository
{
    public class ProjectRepository : BaseRepository<ProjectEntity>, IProjectRepository
    {
        private readonly IMapper _mapper;
        public ProjectRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
        // Find the project which will be updated
        public ProjectEntity UpdateProject(int projectNumber)
        {
            ProjectEntity res = Session.QueryOver<ProjectEntity>()
                                .Where(p => p.ProjectNumber == projectNumber).SingleOrDefault();
            return res;
        }

        // Create a ProjectEntity from a SaveProjectResource
        public ProjectEntity NewProject(SaveProjectDto project)
        {
            var result = new ProjectEntity();
            result.Name = project.Name;
            result.Customer = project.Customer;
            result.ProjectNumber = project.ProjectNumber;
            result.StartDate = project.StartDate;
            result.GroupId = project.GroupId;           
            result.FinishDate = project.FinishDate;
            result.Status = project.Status;
            string[] members = project.Member.Split(',');
            foreach(var item in members)
            {
                EmployeeEntity employee = Session.QueryOver<EmployeeEntity>()
                                      .Where(p => p.Visa == item).SingleOrDefault();
                result.Employees.Add(employee);
            }           
            return result;
        }

        public SearchProjectQueryResult GetSearchProject(SearchProjectQuery query)
        {
            List<ProjectEntity> searchProjectsResult = new List<ProjectEntity>();
            if (query.SearchCriteria.Equals(string.Empty))
                searchProjectsResult = Session.QueryOver<ProjectEntity>()
                                        .Skip((query.SelectedPage - 1) * query.PageSize)
                                        .Take(query.PageSize)
                                        .List() as List<ProjectEntity>;
            else
                searchProjectsResult = Session.QueryOver<ProjectEntity>()
                                        .Where(p => p.Status == GetEnumValue<EStatusType>(query.SearchCriteria))
                                        .Skip((query.SelectedPage - 1) * query.PageSize)
                                        .Take(query.PageSize)
                                        .List() as List<ProjectEntity>;

            int totalItems = searchProjectsResult.Count;
            var projectDtoResult = _mapper.Map<List<ProjectEntity>, List<ProjectDto>>(searchProjectsResult);
            return new SearchProjectQueryResult(projectDtoResult, totalItems);
        }

        public static T GetEnumValue<T>(string str) where T : struct, IConvertible
        {
            Type enumType = typeof(T);
            if (!enumType.IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val;
            return Enum.TryParse<T>(str, true, out val) ? val : default(T);
        }
    }
}