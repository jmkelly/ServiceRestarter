using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Shouldly;
using RestartMappingService;
using System.Threading;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class MappingServiceRestartTest
    {
        [Test]
        public void StartMemoryTest()
        {
            var service = Substitute.For<IService>();
            ServiceRestarter tester = new ServiceRestarter(service);
            tester.Start();
            Thread.Sleep(30000);
            tester.Stop();
            
        }


        [Test]
        public void CurrentlyAvailableMemory()
        {
            var service = Substitute.For<IService>();
            ServiceRestarter tester = new ServiceRestarter(service);
            tester.CurrentlyAvailableMemory().ShouldBeGreaterThan(0);
        }


        [Test]
        public void MakeSureAServiceCanRestart()
        {
            WindowsUpdateService service = new WindowsUpdateService();
            service.Restart();
            //ServiceRestarter restarter = new ServiceRestarter(service,3000,1000);
            

        }
    }
}
