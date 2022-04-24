using AutoMapper;
using PIMTool.Client.Dictionary;
using PIMTool.Services.Resource;
using PIMTool.Services.Service.Entities;
using PIMTool.Services.Service.Generic;
using PIMTool.Services.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

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
        public ProjectEntity FindProject(int projectNumber)
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
            if (project.Member != null)
            {
                string[] members = project.Member.Split(',');
                foreach (var item in members)
                {
                    EmployeeEntity employee = Session.QueryOver<EmployeeEntity>()
                                          .Where(p => p.Visa == item).SingleOrDefault();
                    result.Employees.Add(employee);
                }

            }         
            return result;
        }

        public SearchProjectQueryResult GetSearchProject(SearchProjectQuery query)
        {
            List<ProjectEntity> searchProjectsResult = new List<ProjectEntity>();
            int totalItems;
            if (query.SearchCriteria.Equals("none"))
            {
                totalItems = Session.QueryOver<ProjectEntity>().RowCount();
                searchProjectsResult = Session.QueryOver<ProjectEntity>()
                        .Skip((query.SelectedPage - 1) * query.PageSize)
                        .Take(query.PageSize)
                        .List() as List<ProjectEntity>;
            }

            else
            {
                totalItems = Session.QueryOver<ProjectEntity>()
                                        .Where(p => p.Status == GetEnumValue<EStatusType>(query.SearchCriteria))
                                        .RowCount();
                searchProjectsResult = Session.QueryOver<ProjectEntity>()
                                        .Where(p => p.Status == GetEnumValue<EStatusType>(query.SearchCriteria))
                                        .Skip((query.SelectedPage - 1) * query.PageSize)
                                        .Take(query.PageSize)
                                        .List() as List<ProjectEntity>;
            }

            var projectDtoResult = _mapper.Map<List<ProjectEntity>, List<ProjectDto>>(searchProjectsResult);
            return new SearchProjectQueryResult(projectDtoResult, totalItems);
        }

        public static T GetEnumValue<T>(string description) where T : struct, IConvertible
        {
            var type = typeof(T);
            if (!type.IsEnum)
                throw new ArgumentException();
            FieldInfo[] fields = type.GetFields();
            var field = fields
                            .SelectMany(f => f.GetCustomAttributes(
                                typeof(DescriptionAttribute), false), (
                                    f, a) => new { Field = f, Att = a })
                            .Where(a => ((DescriptionAttribute)a.Att)
                                .Description == description).SingleOrDefault();
            return field == null ? default(T) : (T)field.Field.GetRawConstantValue();
        }
    }
}