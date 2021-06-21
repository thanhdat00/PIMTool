using System.Collections.Generic;
using PIMTool.Services.Service.Pattern;
using PIMTool.Services.Service.Repository;

namespace PIMTool.Services.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;
        private readonly IProjectRepository _projectRepository;

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
            return _unitOfWorkProvider.PerformActionInUnitOfWork<Entities.ProjectEntity>(() => _projectRepository.GetById(projectId));
        }
    }
}