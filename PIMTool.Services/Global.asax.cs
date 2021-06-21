using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;
using PIMTool.Common;
using PIMTool.Services.DependencyInjection;

namespace PIMTool.Services
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Code that runs on application startup
            IoC.Initialize(
                new StandardKernel(new NinjectSettings { LoadExtensions = true }),
                new ServiceBindingModule());

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);


        }
    }
}
