using System.ComponentModel;
using System.ServiceProcess;

namespace WindowsServiceFramework.BasicSample
{
    partial class ProjectInstaller
    {
        public ServiceInstaller ServiceInstaller = null;
        private ServiceProcessInstaller ServiceProcessInstaller = null;
        private IContainer components = null;
    }
}