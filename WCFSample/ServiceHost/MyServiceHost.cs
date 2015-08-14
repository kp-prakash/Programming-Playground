using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using WCFSample;

namespace ServiceHost
{
    public partial class MyServiceHost : ServiceBase
    {
        public MyServiceHost()
        {
            InitializeComponent();
            ServiceName = "MyService";
        }

        public System.ServiceModel.ServiceHost ServiceHost = null; 

        protected override void OnStart(string[] args)
        {
            try
            {
                if (ServiceHost != null)
                {
                    ServiceHost.Close();
                }
                ServiceHost = new System.ServiceModel.ServiceHost(typeof(Service1));
                ServiceHost.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (ServiceHost != null)
                {
                    ServiceHost.Close();
                    ServiceHost = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller process;
        private ServiceInstaller service;

        public ProjectInstaller()
        {
            process = new ServiceProcessInstaller();
            process.Account = ServiceAccount.LocalSystem;
            service = new ServiceInstaller();
            service.ServiceName = "MyService";
            service.StartType = ServiceStartMode.Automatic;
            Installers.Add(process);
            Installers.Add(service);
        }
    }
}
