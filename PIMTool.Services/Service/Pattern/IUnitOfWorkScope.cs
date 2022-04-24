using System;
using NHibernate;

namespace PIMTool.Services.Service.Pattern
{

    public interface IUnitOfWorkScope : IDisposable
    {
        ISession Session
        {
            get;
        }

        void Complete();

        /// <summary>
        /// Initialize a lazy many-to-one or one-to-may/many-to-many (i.e. a colleciton)
        /// </summary>
        /// <param name="proxy"></param>
        void InitializeProxy(object proxy);
    }
}