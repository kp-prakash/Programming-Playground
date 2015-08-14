using System.ServiceProcess;

namespace ServiceHost
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object.
            var servicesToRun = new ServiceBase[] { new MyServiceHost() };
            ServiceBase.Run(servicesToRun);
        }
    }
}