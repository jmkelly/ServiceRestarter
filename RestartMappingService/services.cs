using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using System.Security.Principal;

namespace LocationSolve
{

    public interface IService
    {
        void Restart();
    }

    public class WindowsUpdateService :IService 
    {
        ServiceController service;
        public WindowsUpdateService()
        {
            service = new ServiceController("Windows Update");
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            Console.WriteLine("Running under identity " + identity.Name);
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("Running under Administrator");
            }
            else
            {
                Console.WriteLine("Not Running under Administrator.  Rerun with Administrator privledges!");
            }
        }

        public void Restart()
        {
            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
          
        }
    }


    public class MappingService : IService
    {
        ServiceController service;
        
        public MappingService()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            Console.WriteLine("Running under identity " + identity.Name);
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            if (principal.IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("Running under Administrator");
            }
            else
            {
                Console.WriteLine("Not Running under Administrator.  Rerun with Administrator privledges!");
            }
            service = new ServiceController("Midway.Infrastructure.MappingService");
        }

        public void Restart()
        {
            try
            {
                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped);
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
          
        }

       
            
    }
}
