using Ninject.Modules;
using PIMTool.Client.WebApiClient;
using PIMTool.Client.WebApiClient.Services;

namespace PIMTool.Client.DependencyInjection
{
    public class ClientBindingModule : NinjectModule
    {
        public override void Load()
        {
            BindWebApiClients();
            BindTransportLayer();
        }

        private void BindWebApiClients()
        {
            Bind<IProjectWebApiClient>().To<ProjectWebApiClient>().InSingletonScope();
            Bind<IEmployeeWebApiClient>().To<EmployeeWebApiClient>().InSingletonScope();
        }

        private void BindTransportLayer()
        {
            Bind<IHttpClientFactory>().To<HttpClientFactory>().InSingletonScope();
        }
    }
}
