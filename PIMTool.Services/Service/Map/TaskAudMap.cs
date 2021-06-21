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
            Table("TaskAUD");
            Lazy(true);
            Id(x => x.Id, map =>
                map.Generator(Generators.Identity)
            );
            Property(x => x.Name);
            Property(x => x.DeadlineDate);
            Version(e => e.RowVersion, versionMapper =>
            {
                versionMapper.Column("RowVersion");
                versionMapper.Generated(VersionGeneration.Never);
            });
            Property(x => x.ProjectId, m => m.Column("ProjectID"));
            Property(x => x.TaskId, m => m.Column("TaskID"));
            Property(x => x.Action);
        }
    }
}