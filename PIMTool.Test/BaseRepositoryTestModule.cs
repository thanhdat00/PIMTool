using log4net;
using NHibernate;
using Ninject;
using Ninject.Modules;
using PIMTool.Services.Constants;
using PIMTool.Services.Service.Pattern;
using PIMTool.Services.Service.Pattern.SessionFactory;

namespace PIMTool.Test
{
    public abstract class BaseRepositoryTestModule : NinjectModule
    {
        public void LoadModule()
        {
            log4net.Config.XmlConfigurator.Configure();
            var logger = LogManager.GetLogger(WebConstants.WEB_SERVER_TRACE);

            Bind<ILog>().ToConstant(logger);
            Bind<ISessionFactory>().ToProvider<PIMToolSessionFactoryProvider<PIMToolSessionFactory>>()
                 .InSingletonScope();
            Bind<IUnitOfWorkProvider>()
                .To<UnitOfWorkProvider>()
                .WithConstructorArgument(WebConstants.SessionFactory, x => x.Kernel.Get<ISessionFactory>());
            Bind<ISession>().ToMethod(x => x.Kernel.Get<ISessionFactory>().OpenSession());
        }
    }
}
