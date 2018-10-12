using Autofac;
using Plato.Threading.Enums;
using Plato.Threading.WorkManagement;
using Plato.Core.Miscellaneous;
using System;
using System.ServiceProcess;
using WindowsServiceFramework.Notifications;
using WindowsServiceFramework.IoCModules;

namespace WindowsServiceFramework.BasicSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var container = (IContainer)null;
            var workRegistry = new WorkManagerRegistry();
            var notifier = new ServiceLogNotification();
            var disposables = new Disposables();
            var builder = new ContainerBuilder();

            builder.RegisterModule(new ServiceModule(workRegistry, notifier, disposables, () => new DependencyFactory(container)));
            container = builder.Build();

            using (var workManager = new WorkManager(workRegistry, new WorkManagerDependencyResolver(container), notifier))
            {
                workManager.OnRuntimeState += (state) =>
                {
                    switch (state)
                    {
                        case ManagerRuntimeStates.Stopped:
                            disposables.Dispose();
                            break;
                    }
                };


                if (!Environment.UserInteractive)
                {
                    var servicesToRun = new ServiceBase[]
                    {
                        new WindowsService(workManager)
                    };

                    ServiceBase.Run(servicesToRun);
                }
                else
                {
                    workManager.Start();

                    Console.WriteLine($"WorkManager '{WorkManagerConfig.ApplicationName}' has started.");
                    Console.WriteLine("Press any key to terminate...");
                    Console.ReadKey();

                    workManager.Stop();
                }
            }
        }
    }
}
