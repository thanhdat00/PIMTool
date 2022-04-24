using System;
using log4net;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using PIMTool.Services.Service.Map;

namespace PIMTool.Services.Service.Pattern.SessionFactory
{
    public class PIMToolSessionFactory
    {

        public ILog Logger
        {
            get; set;
        }

        protected virtual string DefaultSchema { get { return String.Empty; } }


        public virtual string GetConnectionName()
        {
            return "PIMContext";
        }

        public ISessionFactory GetSessionFactory()
        {
            Logger.Debug($"Create session factory for connection name = {GetConnectionName()} with schema = {DefaultSchema}");
            var config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.ConnectionStringName = GetConnectionName();
                db.Dialect<MsSql2012Dialect>();
                db.Driver<SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.LogFormattedSql = true;
                db.BatchSize = 100;
            }
                )
                .SetProperty(NHibernate.Cfg.Environment.WrapResultSets, Boolean.TrueString)
                .SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, Boolean.FalseString)
                .SetProperty(NHibernate.Cfg.Environment.QueryStartupChecking, Boolean.TrueString)
                .SetProperty(NHibernate.Cfg.Environment.PrepareSql, Boolean.TrueString)
                .SetProperty(NHibernate.Cfg.Environment.DefaultSchema, DefaultSchema);



            var mapper = new ModelMapper();
            MappingTable(mapper);
            var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();

            config.AddDeserializedMapping(mappings, null);

            return config.BuildSessionFactory();
        }

        public virtual void MappingTable(ModelMapper mapper)
        {
            mapper.AddMapping<EmployeeMap>();
            mapper.AddMapping<GroupMap>();
            mapper.AddMapping<ProjectMap>();
            mapper.AddMapping<TaskMap>();
            mapper.AddMapping<TaskAudMap>();                  
        }
    }
}
