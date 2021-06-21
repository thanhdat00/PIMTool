using System;
using System.Data;

namespace PIMTool.Services.Service.Pattern
{

    public interface IUnitOfWorkProvider
    {

        UnitOfWorkScope Provide();

        UnitOfWorkScope Provide(UnitOfWorkScopeOption scopeOption);

        UnitOfWorkScope Provide(UnitOfWorkScopeOption scopeOption, IsolationLevel isolationLevel);

        void PerformActionInUnitOfWork(Action action);

        void PerformActionInUnitOfWork(Action action, UnitOfWorkScopeOption scopeOption);

        void PerformActionInUnitOfWork(Action action, UnitOfWorkScopeOption scopeOption, IsolationLevel isolationLevel);

        T PerformActionInUnitOfWork<T>(Func<T> action);

        T PerformActionInUnitOfWork<T>(Func<T> action, UnitOfWorkScopeOption scopeOption);

        T PerformActionInUnitOfWork<T>(Func<T> action, UnitOfWorkScopeOption scopeOption, IsolationLevel isolationLevel);
    }
}