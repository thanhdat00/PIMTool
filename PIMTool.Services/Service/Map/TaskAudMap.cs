using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PIMTool.Services.Service.Entities;

namespace PIMTool.Services.Service.Map
{
    public class TaskAudMap : ClassMapping<TaskAudEntity>
    {

        public TaskAudMap()
        {
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name);
            Property(x => x.DeadlineDate);
            Property(e => e.RowVersion);
            Property(x => x.ProjectId);
            Property(x => x.TaskId);
            Property(x => x.Action);
        }
    }
}