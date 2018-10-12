using Plato.Threading.WorkManagement;
using System.ServiceProcess;

namespace WindowsServiceFramework.BasicSample
{
    public partial class WindowsService : ServiceBase
    {
        private readonly WorkManager _workManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsService"/> class.
        /// </summary>
        /// <param name="workManager">The work manager.</param>
        public WindowsService(WorkManager workManager)
        {
            InitializeComponent();

            _workManager = workManager;
        }

        /// <summary>
        /// Called when [start].
        /// </summary>
        /// <param name="args">The arguments.</param>
        protected override void OnStart(string[] args)
        {
            _workManager.Start();
        }

        /// <summary>
        /// When implemented in a derived class, executes when a Stop command is sent to the service by the Service Control Manager (SCM). Specifies actions to take when a service stops running.
        /// </summary>
        protected override void OnStop()
        {
            _workManager.Stop();
        }
    }
}
