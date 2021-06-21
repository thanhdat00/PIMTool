using log4net;
using NHibernate;
using Ninject;
using Ninject.Activation;

namespace PIMTool.Services.Service.Pattern.SessionFactory
{
    public class PIMToolSessionFactoryProvider<T> : Provider<ISessionFactory> where T : PIMToolSessionFactory, new()
    {
        [Inject]
        public ILog Logger { get; set; }

        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = new T();
            sessionFactory.Logger = Logger;
            return sessionFactory.GetSessionFactory();
        }
    }
}
