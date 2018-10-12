using Autofac;
using Plato.Interfaces;
using System;

namespace WindowsServiceFramework.IoCModules
{
    public class WorkManagerDependencyResolver : IWorkManagerDependencyResolver
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WorkManagerDependencyResolver"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WorkManagerDependencyResolver(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="package">The package.</param>
        /// <returns></returns>
        public T Resolve<T>(string name, Type type, IWorkPackage package)
        {
            return _container.ResolveNamed<T>(name, new TypedParameter(typeof(IWorkPackage), package));
        }
    }
}
