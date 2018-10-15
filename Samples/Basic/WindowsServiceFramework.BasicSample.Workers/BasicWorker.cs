using Plato.Interfaces;
using Plato.Threading.WorkManagement;
using System.Diagnostics;
using System.Threading.Tasks;

namespace WindowsServiceFramework.BasicSample.Workers
{
    public class BasicWorker : BaseWorker
    {
        private int _serviceOwnershipTimeWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicWorker" /> class.
        /// </summary>
        /// <param name="workPackage">The work package.</param>
        public BasicWorker(
            IWorkPackage workPackage) : base(workPackage)
        {
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
        private async Task OnWorkAsync()
        {
            var sw = new Stopwatch();
            sw.Start();

            Notification.SendInformation($"Starting process for '{WorkPackage.NameInstance}'");
            try
            {
                // Do something
                Notification.SendMessage("Processing some work!", Plato.Core.Logging.Enums.NotificationType.Message);
            }
            finally
            {
                sw.Stop();
                Notification.SendInformation($"Ending process for '{WorkPackage.NameInstance}'. Took {sw.ElapsedMilliseconds:N0} msec");
            }
        }

        /// <summary>
        /// Called when [work].
        /// </summary>
        protected override void OnWork()
        {
            OnWorkAsync().GetAwaiter().GetResult();
        }
    }
}
