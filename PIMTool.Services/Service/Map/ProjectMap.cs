using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using PIMTool.Client.Dictionary;
using PIMTool.Services.Service.Entities;

namespace PIMTool.Services.Service.Map
{
    public class ProjectMap : ClassMapping<ProjectEntity>
    {
        public ProjectMap()
        {
            Schema("dbo");
            Table("Project");
            Lazy(true);
            Id(x => x.Id, map =>
            {
                map.Column("ID");
                map.Generator(Generators.Identity);
            });
            Property(x => x.Name, m =>
            {
                m.Column("Name");
                m.NotNullable(true);
            });
            Property(x => x.ProjectNumber);
            Property(x => x.Customer);
            Property(x => x.Status, m =>
            {
                m.Type<EnumStringType<EStatusType>>();
                m.NotNullable(true);
            });

            Property(x => x.StartDate, m =>
            {
                m.Column("StartDate");
                m.NotNullable(true);
            });
            Property(x => x.FinishDate);

            Version(e => e.RowVersion, versionMapper =>
            {
                versionMapper.Column("RowVersion");
                versionMapper.Generated(VersionGeneration.Never);
            });

            Bag(x => x.Task, colmap =>
            {
                colmap.Key(x =>
                {
                    x.Column("ProjectID");
                });
                colmap.Lazy(CollectionLazy.NoLazy);
                colmap.Inverse(true);
                colmap.Cascade(Cascade.All | Cascade.DeleteOrphans);
            }, map => { map.OneToMany(a => a.Class(typeof(TaskEntity))); });

            Bag(x => x.Employees, m =>
            {
                m.Lazy(CollectionLazy.NoLazy);
                m.Table("EmployeeProjects");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("ProjectID"));
            }, r =>
            {
                r.ManyToMany(m =>
                {
                    m.Column("EmployeeID");
                    m.Class(typeof(EmployeeEntity));
                });
            });

            Property(x => x.GroupId);
        }
    }
}