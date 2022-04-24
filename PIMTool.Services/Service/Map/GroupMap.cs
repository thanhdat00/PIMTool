using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PIMTool.Services.Service.Entities;

namespace PIMTool.Services.Service.Map
{
    public class GroupMap : ClassMapping<GroupEntity>
    {
        public GroupMap()
        {
            Schema("dbo");
            Table("Group");
            Lazy(true);
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Version(e => e.RowVersion, map =>
            {
                map.Column("RowVersion");
                map.Generated(VersionGeneration.Never);
            });

            Bag(x => x.Projects, m =>
            {
                m.Lazy(CollectionLazy.NoLazy);
                m.Key(k => k.Column("GroupID"));
                m.Cascade(Cascade.DeleteOrphans | Cascade.All);
            }, r => r.OneToMany(m =>
            {
                m.Class(typeof(ProjectEntity));
            }));

            ManyToOne(x => x.GroupLeader, m =>
            {
                m.Column("GroupLeaderID");
                m.Unique(true);
                m.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}