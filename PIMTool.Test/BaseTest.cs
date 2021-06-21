using Ninject;
using Ninject.Modules;
using NUnit.Framework;
using PIMTool.Services.Service.Pattern;

namespace PIMTool.Test
{
    public class BaseTest
    {
        public IUnitOfWorkProvider UnitOfWorkProvider
        {
            get
            {
                return Kernel.Get<UnitOfWorkProvider>();
            }
        }
        public IKernel Kernel { get; private set; }

        [TestFixtureSetUp]
        public void Setup()
        {
            PrepareNinjectModuleBinding();
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void PrepareNinjectModuleBinding()
        {

            Kernel = new StandardKernel();
            var ninjectModule = GetServiceBindingModule();

            if (ninjectModule != null && !Kernel.HasModule(ninjectModule.Name))
            {
                Kernel.Load(ninjectModule);
            }
        }

        protected virtual INinjectModule GetServiceBindingModule()
        {
            return null;
        }
    }
}
