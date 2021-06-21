using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PIMTool.Common
{
    public static class IoC
    {
        private static IKernel _kernel;

        public static T Get<T>()
        {
            return _kernel.Get<T>();
        }

        public static T Get<T>(string name)
        {
            return _kernel.Get<T>(name);
        }

        public static T TryGet<T>()
        {
            return _kernel.TryGet<T>();
        }

        public static object Get(Type type)
        {
            return _kernel.Get(type);
        }

        public static object TryGet(Type type)
        {
            return _kernel.TryGet(type);
        }

        public static IEnumerable<object> GetAll(Type type)
        {
            return _kernel.GetAll(type);
        }

        public static void Initialize(StandardKernel kernel, params INinjectModule[] modules)
        {
            if (_kernel != null)
            {
                throw new Exception("IoC Kernel already initialized");
            }
            _kernel = kernel;
            if (modules != null && modules.Any())
            {
                kernel.Load(modules);
            }
        }

        public static void ReInitialize(IKernel kernel, params INinjectModule[] modules)
        {
            _kernel = kernel;
            if (modules != null && modules.Any())
            {
                kernel.Load(modules);
            }
        }

        public static void Clear()
        {
            _kernel = null;
        }
    }
}
