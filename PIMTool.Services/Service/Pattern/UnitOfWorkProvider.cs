using System;
using System.Data;
using log4net;
using NHibernate;

namespace PIMTool.Services.Service.Pattern
{

    public class UnitOfWorkProvider : IUnitOfWorkProvider
    {
        public const string BUSINESS_SERVER_TRACE = "BusinessServerTrace";
        private readonly ISessionFactory m_sessionFactory;
        private static readonly ILog s_log = LogManager.GetLogger(BUSINESS_SERVER_TRACE);

        public UnitOfWorkProvider(ISessionFactory sessionFactory)
        {
            m_sessionFactory = sessionFactory;
        }

        public UnitOfWorkScope Provide()
        {
            s_log.Debug("Provide");
            return Provide(DetermineScopeOption());
        }

        public UnitOfWorkScope Provide(IsolationLevel isolationLevel)
        {
            s_log.Debug($"Provide with isolation level {isolationLevel}");
            return Provide(DetermineScopeOption(), isolationLevel);
        }

        private UnitOfWorkScopeOption DetermineScopeOption()
        {
            var scopeOption = UnitOfWorkScopeOption.Required;
            if (IsSessionFactoryOfAmbientScopeDifferent())
            {
                s_log.Info("using new unit of work, because ambient unit of work uses different session factory.");
                scopeOption = UnitOfWorkScopeOption.RequiresNew;
            }
            return scopeOption;
        }

        public UnitOfWorkScope Provide(UnitOfWorkScopeOption scopeOption)
        {
            s_log.Debug($"Provide with scopeOption = {scopeOption}");
            return Provide(scopeOption, IsolationLevel.ReadCommitted);
        }

        public UnitOfWorkScope Provide(UnitOfWorkScopeOption scopeOption, IsolationLevel isolationLevel)
        {
            s_log.Debug($"Provide with scopeOption = {scopeOption}, isolationLevel = {isolationLevel}");
            if (IsSessionFactoryOfAmbientScopeDifferent() && scopeOption == UnitOfWorkScopeOption.Required)
            {
                s_log.Error("ambient unit of work had different session factory then unit of work provider.");
                throw new ArgumentException(
                    "ambient unit of work has incompatible session, please use RequiresNew to create a new Unit of Work.");
            }
            return new UnitOfWorkScope(scopeOption, isolationLevel, m_sessionFactory);
        }

        private bool IsSessionFactoryOfAmbientScopeDifferent()
        {
            return (UnitOfWorkScope.Current != null && UnitOfWorkScope.Current.Session != null &&
                    m_sessionFactory != UnitOfWorkScope.Current.Session.SessionFactory);
        }

        public void PerformActionInUnitOfWork(Action action)
        {
            PerformActionInUnitOfWork(action, DetermineScopeOption(), IsolationLevel.ReadCommitted);
        }

        public void PerformActionInUnitOfWork(Action action, UnitOfWorkScopeOption scopeOption)
        {
            PerformActionInUnitOfWork(action, scopeOption, IsolationLevel.ReadCommitted);
        }

        public void PerformActionInUnitOfWork(Action action, IsolationLevel isolationLevel)
        {
            PerformActionInUnitOfWork(action, DetermineScopeOption(), isolationLevel);
        }

        public void PerformActionInUnitOfWork(Action action, UnitOfWorkScopeOption scopeOption,
            IsolationLevel isolationLevel)
        {
            using (var scope = Provide(scopeOption, isolationLevel))
            {
                action();
                scope.Complete();
            }
        }

        public T PerformActionInUnitOfWork<T>(Func<T> action)
        {
            return PerformActionInUnitOfWork(action, DetermineScopeOption(), IsolationLevel.ReadCommitted);
        }

        public T PerformActionInUnitOfWork<T>(Func<T> action, UnitOfWorkScopeOption scopeOption)
        {
            return PerformActionInUnitOfWork(action, scopeOption, IsolationLevel.ReadCommitted);
        }

        public T PerformActionInUnitOfWork<T>(Func<T> action, UnitOfWorkScopeOption scopeOption,
            IsolationLevel isolationLevel)
        {
            T result;
            using (var scope = Provide(scopeOption, isolationLevel))
            {
                result = action();
                scope.Complete();
            }

            return result;
        }
    }
}