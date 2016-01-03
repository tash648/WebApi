using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web.Http.Dependencies;

namespace QuickErrandsWebApi.DependencyResolver
{
    public class MefDependencyResolver : IDependencyResolver
    {
        private bool isDisposed;
        private readonly CompositionContainer container;        

        public MefDependencyResolver(CompositionContainer container)
        {
            this.container = container;
        }

        public IDependencyScope BeginScope()
        {
            var newContainer = new CompositionContainer(container.Catalog);

            return new MefDependencyResolver(newContainer);
        }

        public object GetService(Type serviceType)
        {
            var export = container.GetExports(serviceType, null, null).SingleOrDefault();

            return null != export ? export.Value : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return container.GetExports(serviceType, null, null).Select(p => p.Value);
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            container.Dispose();

            isDisposed = true;
        }
    }
}