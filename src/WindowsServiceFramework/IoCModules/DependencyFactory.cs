using Autofac;
using System.Linq;
using WindowsServiceFramework.Interfaces;
using WindowsServiceFramework.Models;

namespace WindowsServiceFramework.IoCModules
{
    public class DependencyFactory : IDependencyFactory
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyFactory" /> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public DependencyFactory(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T Resolve<T>(string name, params ResolveParameter[] parameters)
        {
            var typedParameters = parameters.Select(param => new TypedParameter(param.ParameterType, param.Parameter));
            return _container.ResolveNamed<T>(name, typedParameters);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public T Resolve<T>(params ResolveParameter[] parameters)
        {
            var typedParameters = parameters.Select(param => new TypedParameter(param.ParameterType, param.Parameter));
            return _container.Resolve<T>(typedParameters);
        }

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public T Resolve<T>(string name)
        {
            return _container.ResolveNamed<T>(name);
        }

        /// <summary>
        /// Resolves this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
