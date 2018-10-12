using Autofac;
using Plato.Threading.WorkManagement;
using Plato.Core.Interfaces;
using Plato.Core.Logging.Interfaces;
using System;
using WindowsServiceFramework.IoCModules;
using WindowsServiceFramework.Interfaces;
using WindowsServiceFramework.BasicSample.Workers;

namespace WindowsServiceFramework.BasicSample
{
    public class ServiceModule : BaseModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceModule" /> class.
        /// </summary>
        /// <param name="workManagerRegistry">The _work manager registry.</param>
        /// <param name="notifier">The notifier.</param>
        /// <param name="disposables">The disposables.</param>
        /// <param name="onDependencyFactory">The on dependency factory.</param>
        public ServiceModule(
            WorkManagerRegistry workManagerRegistry,
            ILogNotification notifier,
            IDisposables disposables,
            Func<IDependencyFactory> onDependencyFactory) : base(workManagerRegistry, notifier, disposables, onDependencyFactory)
        {
        }

        /// <summary>
        /// Loads the specified builder.
        /// </summary>
        /// <param name="builder">The builder.</param>
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            RegisterWorker<BasicWorker>(builder, "Basic.Worker");
        }
    }
}
