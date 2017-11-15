using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using System.Web.Http;
using Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using GroceryApp.Services;

namespace GroceryApp
{
    public sealed class UnityConfig
    {
        private readonly static UnityConfig _instance = new UnityConfig();
        static UnityConfig() { }
        private UnityConfig() { }
        public static UnityConfig Instance { get { return _instance; } }
        public void RegisterComponents(HttpConfiguration config)
        {
            UnityContainer container = new UnityContainer();
            //container.RegisterType<IYourInterfaceName, YourConcreteClassName>();
            container.RegisterType<IProductService, ProductService>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            config.DependencyResolver = new UnityResolver(container);
        }
    }
    public class UnityResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        protected IUnityContainer container;
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            this.container = container;
        }
        public object GetService(Type serviceType)
        {
            try { return container.Resolve(serviceType); }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try { return container.ResolveAll(serviceType); }
            catch (ResolutionFailedException) { return new List<object>(); }
        }
        public IDependencyScope BeginScope()
        {
            IUnityContainer child = container.CreateChildContainer();
            return new UnityResolver(child);
        }
        public void Dispose()
        {
            container.Dispose();
        }
    }
}