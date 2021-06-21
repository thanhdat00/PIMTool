using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PIMTool.Services.Service.Entities;

namespace PIMTool.Services.Service.Map
{
    public class TaskMap : ClassMapping<TaskEntity>
    {

        public TaskMap()
        {
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name);
            Property(x => x.DeadlineDate);
            Version(e => e.RowVersion, versionMapper => versionMapper.Generated(VersionGeneration.Never));
            ManyToOne(x => x.Project, map => { map.Column("ProjectID"); map.Cascade(Cascade.None); });

        }
    }
}