using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Diagnostics;


namespace RestartMappingService
{

    public interface IServiceRestarter
    {
        void Start();
        void Stop();
        int CurrentlyAvailableMemory();
    }

    public class ServiceRestarter : IServiceRestarter 
    {
        
        private int _AvailableMemoryAtWhichToRestart;
        private int _IntervalAtWhichToCheck;
        private Timer timer;
        IService _service;

        public ServiceRestarter(IService service)
        {
            _service = service;
            _AvailableMemoryAtWhichToRestart = 500;
            _IntervalAtWhichToCheck = 10000;
        }

        public ServiceRestarter(IService service, int AvailableMemoryAtWhichToRestart, int IntervalAtWhichToCheck)
        {
            _service = service;
            _AvailableMemoryAtWhichToRestart = AvailableMemoryAtWhichToRestart;
            _IntervalAtWhichToCheck = IntervalAtWhichToCheck;
        }

        public void Start()
        {
            Console.WriteLine("Starting Available Memory testing at " + DateTime.Now.ToString());
            timer = new Timer(_IntervalAtWhichToCheck );
            timer.Elapsed += new ElapsedEventHandler(CheckMemory);
            timer.Start();
        }

        public int CurrentlyAvailableMemory()
        {
            PerformanceCounter free;
            free = new PerformanceCounter("Memory", "Available MBytes");
            return (int)free.NextValue();
        }

        private void CheckMemory(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Available memory at " + DateTime.Now.ToString() + " is " + CurrentlyAvailableMemory().ToString() + "Mb");

            if (CurrentlyAvailableMemory() < _AvailableMemoryAtWhichToRestart)
            {
                Console.WriteLine("Available Memory less than " + _AvailableMemoryAtWhichToRestart.ToString() + "Mb.  Service Restarted");
                _service.Restart();
            }

        }

        public void Stop()
        {
            //stop the memory test loop; 
            Console.WriteLine("Starting Available Memory testing at " + DateTime.Now.ToString());
            timer.Stop();
        }
    }
}
