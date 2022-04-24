using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using PIMTool.Services.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PIMTool.Services.Service.Map
{
    public class EmployeeMap : ClassMapping<EmployeeEntity>
    {
        public EmployeeMap()
        {       
            Schema("dbo");
            Table("Employee");
            Lazy(true);
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Visa);
            Property(x => x.FirstName);
            Property(x => x.LastName);
            Property(x => x.BirthDate);
            Version(e => e.RowVersion, map => map.Generated(VersionGeneration.Never));

            Bag(x => x.Projects, m =>
            {
                m.Lazy(CollectionLazy.NoLazy);
                m.Table("EmployeeProjects");
                m.Cascade(Cascade.None);
                m.Key(k => k.Column("EmployeeID"));
            }, r =>
            {
                r.ManyToMany(m =>
                {
                    m.Column("ProjectID");
                    m.Class(typeof(ProjectEntity));
                });
            });
        }
    }
}