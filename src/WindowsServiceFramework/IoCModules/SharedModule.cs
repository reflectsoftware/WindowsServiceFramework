using Autofac;
using Plato.Configuration;
using Plato.Configuration.Interfaces;
using Plato.Core.Interfaces;
using Plato.Core.Logging.Interfaces;
using Plato.Interfaces;
using Plato.Threading.WorkManagement;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsServiceFramework.Interfaces;

namespace WindowsServiceFramework.IoCModules
{
    public class SharedModule : Module
    {
        protected readonly ILogNotification _notifier;
        protected readonly IDisposables _disposables;
        protected readonly ISimpleConfigurationSectionManager _workersConfiguration;
        protected readonly WorkManagerRegistry _workManagerRegistry;
        protected readonly Func<IDependencyFactory> _onDependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="SharedModule" /> class.
        /// </summary>
        /// <param name="workManagerRegistry">The work manager registry.</param>
        /// <param name="notifier">The notifier.</param>
        /// <param name="disposables">The disposables.</param>
        /// <param name="onDependencyFactory">The on dependency factory.</param>
        public SharedModule(
            WorkManagerRegistry workManagerRegistry,
            ILogNotification notifier,
            IDisposables disposables,
            Func<IDependencyFactory> onDependencyFactory)
        {
            _notifier = notifier;
            _disposables = disposables;
            _workManagerRegistry = workManagerRegistry;
            _onDependencyFactory = onDependencyFactory;
            _workersConfiguration = new SimpleConfigurationSectionManager("workerSettings");
        }

        /// <summary>
        /// Loads the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // factories
            builder.Register(ctx => _onDependencyFactory()).SingleInstance();

            // standard instances
            builder.Register(c => _notifier).As<ILogNotification>().SingleInstance();
            builder.Register(c => _disposables).As<IDisposables>().SingleInstance();
            
            // configurations 
            builder.Register(c => new SimpleConfigurationSectionManager("generalSettings")).Named<ISimpleConfigurationSectionManager>("generalSettings").SingleInstance();

            //RegisterDatabaseContainers(builder);
        }


        /// <summary>
        /// Gets the common worker parameters.
        /// </summary>
        /// <returns></returns>
        protected virtual NameValueCollection GetCommonWorkerParameters()
        {
            var commonWorkerParameters = new NameValueCollection();
            commonWorkerParameters["enabled"] = "true";
            commonWorkerParameters["workSleep"] = "0";
            commonWorkerParameters["instances"] = "1";
            commonWorkerParameters["aliveResponseWindow"] = "60";
            commonWorkerParameters["restartNonResponsive"] = "true";
            commonWorkerParameters["abortThreadOnUnhandledException"] = "false";

            return commonWorkerParameters;
        }

        /// <summary>
        /// Extends the worker parameters.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parameters">The parameters.</param>
        protected virtual void ExtendWorkerParameters(string name, NameValueCollection parameters)
        {
        }

        /// <summary>
        /// Registers the worker.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="name">The name.</param>
        protected void RegisterWorker<T>(ContainerBuilder builder, string name)
        {
            var workerParameters = new NameValueCollection(GetCommonWorkerParameters());

            var workerConfigurations = _workersConfiguration.GetAttributes("worker", name);
            foreach (var configKey in workerConfigurations.AllKeys)
            {
                workerParameters[configKey] = workerConfigurations[configKey];
            }

            if (workerParameters["enabled"] != "true")
            {
                return;
            }

            builder.RegisterType<T>().Named<IBaseWorker>(name).ExternallyOwned();
            _workManagerRegistry.Register<IBaseWorker>(name, true, workerParameters);
        }
    }
}
