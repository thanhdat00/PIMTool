using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Dependencies;
using PIMTool.Common;

namespace PIMTool.Services
{
    public sealed class DependencyResolver : IDependencyResolver
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsSubclassOf(typeof(ApiController)))
            {
                // Controlles should be able to be resolved or throw an exception otherwise
                return IoC.Get(serviceType);
            }
            return IoC.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return IoC.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}
