using Plato.Core.Miscellaneous;
using Plato.Interfaces;
using System;
using System.Threading.Tasks;
using WindowsServiceFramework.Extensions.Interfaces;
using WindowsServiceFramework.Interfaces;

namespace WindowsServiceFramework.Extensions.Workers
{
    public class DelayedWorker : PassiveWorker
    {
        private int _serviceOwnershipTimeWindow;
        private readonly IDependencyFactory _dependencyFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DelayedWorker"/> class.
        /// </summary>
        /// <param name="workPackage">The work package.</param>
        /// <param name="dependencyFactory">The dependency factory.</param>
        /// <param name="referenceRepositoryContainer">The reference repository container.</param>
        public DelayedWorker(
            IWorkPackage workPackage,
            IDependencyFactory dependencyFactory,
            IWorkerRepositoryContainer workerRepositoryContainer) :
            base(workPackage, workerRepositoryContainer)
        {
            Guard.AgainstNull(() => workPackage);
            Guard.AgainstNull(() => dependencyFactory);

            _dependencyFactory = dependencyFactory;
        }

        /// <summary>
        /// Called when [dispose].
        /// </summary>
        protected override void OnDispose()
        {
            base.OnDispose();
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        protected override void OnInitialize()
        {
            base.OnInitialize();

            _serviceOwnershipTimeWindow = int.Parse(WorkPackage.Parameters("serviceOwnershipTimeWindow", "5"));
        }

        /// <summary>
        /// Does the work.
        /// </summary>
        /// <returns></returns>
        private async Task DoWork()
        {
            var schedules = await _workerRepositoryContainer.DelayServiceRepository.GetScheduledRequestsAsync(DateTime.UtcNow);

            try
            {
                foreach (var workItem in schedules)
                {
                    //try
                    //{
                    //    var handler = _dependencyFactory.Create<IDelayServiceHandler>(((DelayServiceType)workItem.DelayTypeId).ToString());
                    //    using (var disposeHandler = new Disposer<IDelayServiceHandler>(handler))
                    //    {
                    //        await disposeHandler.Instance.ProcessAsync(workItem.Data);
                    //        await _workerRepositoryContainer.DelayServiceRepository.DeleteAsync(workItem);
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    workItem.Scheduled = DateTime.UtcNow.AddHours(1);
                    //    Notification.SendError($"Error detected processing DelayHandler: {((DelayServiceType)workItem.DelayTypeId)}. Rescheduling work item until: {workItem.Scheduled}");
                    //    throw;
                    //}
                }
            }
            finally
            {
                //await _workerRepositoryContainer.DbContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Called when [work].
        /// </summary>
        protected override void OnWork()
        {
#if !DEBUG
            if (!HasOwnership(_serviceOwnershipTimeWindow))
            {
                return;
            }
#endif

            DoWork().GetAwaiter().GetResult();
        }
    }
}
