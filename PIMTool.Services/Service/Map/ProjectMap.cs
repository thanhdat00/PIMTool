using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace PIMTool.Services.Service.Map
{
    public class ProjectMap : ClassMapping<Entities.ProjectEntity>
    {
        public ProjectMap()
        {
            Schema("dbo");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name, map => map.NotNullable(true));
            Property(x => x.StartDate, map => map.NotNullable(true));
            Property(x => x.FinishDate);
            Version(e => e.RowVersion, versionMapper => versionMapper.Generated(VersionGeneration.Never));
            Bag(x => x.Task, colmap =>
            {
                colmap.Key(x => x.Column("ProjectID"));
                colmap.Inverse(true);
                colmap.Cascade(Cascade.All);
            }, map => { map.OneToMany(); });
        }
    }
}