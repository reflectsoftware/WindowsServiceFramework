using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WindowsServiceFramework.BasicSample
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectInstaller"/> class.
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();
            InitializeInstaller();
        }

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.ServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.ServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // ServiceProcessInstaller
            // 
            this.ServiceProcessInstaller.Password = null;
            this.ServiceProcessInstaller.Username = null;
            // 
            // ServiceInstaller
            // 
            this.ServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.ServiceProcessInstaller,
            this.ServiceInstaller});

        }

        /// <summary>
        /// Initializes the installer.
        /// </summary>
        private void InitializeInstaller()
        {
            // 
            // ServiceProcessInstaller
            // 
            ServiceProcessInstaller.Account = ServiceAccount.LocalSystem;
            ServiceProcessInstaller.Password = null;
            ServiceProcessInstaller.Username = null;
            ServiceProcessInstaller.BeforeInstall += ServiceInstaller_BeforeInstallUninstall;
            // 
            // ServiceInstaller
            // 
            ServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            ServiceInstaller.Description = "WindowsServiceFramework.BasicSample";
            ServiceInstaller.DisplayName = "WindowsServiceFramework.BasicSample";
            ServiceInstaller.ServiceName = "WindowsServiceFramework.BasicSample";
            ServiceInstaller.BeforeUninstall += ServiceInstaller_BeforeInstallUninstall;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ServiceInstaller != null)
                {
                    ServiceInstaller.Dispose();
                    ServiceInstaller = null;
                }

                if (ServiceProcessInstaller != null)
                {
                    ServiceProcessInstaller.Dispose();
                    ServiceProcessInstaller = null;
                }

                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Handles the BeforeInstallUninstall event of the ServiceInstaller control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="InstallEventArgs"/> instance containing the event data.</param>
        private void ServiceInstaller_BeforeInstallUninstall(object sender, InstallEventArgs e)
        {
            ServiceInstaller.DisplayName = Context.Parameters["displayname"];
            ServiceInstaller.ServiceName = Context.Parameters["servicename"];
        }

        /// <summary>
        /// Installs the specified state server.
        /// </summary>
        /// <param name="stateServer">The state server.</param>
        public override void Install(IDictionary stateServer)
        {
            // In order to put in a description of the Windows Service, you needed to override the INSTALL method.
            // Make sure you call the BASE.

            base.Install(stateServer);

            Microsoft.Win32.RegistryKey system;
            Microsoft.Win32.RegistryKey currentControlSet;
            Microsoft.Win32.RegistryKey services;
            Microsoft.Win32.RegistryKey service;

            system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
            currentControlSet = system.OpenSubKey("CurrentControlSet");
            services = currentControlSet.OpenSubKey("Services");
            service = services.OpenSubKey(ServiceInstaller.ServiceName, true);
            service.SetValue("Description", ServiceInstaller.Description);
        }

        /// <summary>
        /// Uninstall the specified state server.
        /// </summary>
        /// <param name="stateServer">The state server.</param>
        public override void Uninstall(IDictionary stateServer)
        {
            // In order to put in a description of the Windows Service, you needed to override the INSTALL method.
            // Make sure you call the BASE.

            Microsoft.Win32.RegistryKey system;
            Microsoft.Win32.RegistryKey currentControlSet;
            Microsoft.Win32.RegistryKey services;
            Microsoft.Win32.RegistryKey service;

            try
            {
                //Drill down to the service key and open it with write permission
                system = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("System");
                currentControlSet = system.OpenSubKey("CurrentControlSet");
                services = currentControlSet.OpenSubKey("Services");
                service = services.OpenSubKey(ServiceInstaller.ServiceName, true);
            }
            finally
            {
                base.Uninstall(stateServer);
            }
        }
    }
}
