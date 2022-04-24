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
            Table("Task");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name, m =>
            {
                m.Column("Name");
            });
            Property(x => x.DeadlineDate, m =>
            {
                m.Column("DeadlineDate");
            });
            Version(e => e.RowVersion, versionMapper =>
            {
                versionMapper.Column("RowVersion");
                versionMapper.Generated(VersionGeneration.Never);
            });
            ManyToOne(x => x.Project, map => 
            { 
                map.Column("ProjectID");
                map.Class(typeof(ProjectEntity));
                map.NotNullable(true);
                map.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}