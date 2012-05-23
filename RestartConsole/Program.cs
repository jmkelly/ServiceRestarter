using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceProcess;
using RestartMappingService;


namespace RestartConsole
{
    class Program
    {

        protected static void myHandler(object sender, ConsoleCancelEventArgs args)
        {
           
        }

        static void Main(string[] args)
        {

            ConsoleKeyInfo cki;
            Console.Clear();
            MappingService service = new MappingService();
            ServiceRestarter ping = new ServiceRestarter(service);
            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);
            while (true)
            {

   
               
                ping.Start();
                Console.WriteLine("Press 'X' to quit ");
                // Start a console read operation. Do not display the input.
                cki = Console.ReadKey(true);

                // Exit if the user pressed the 'X' key.
                if (cki.Key == ConsoleKey.X)
                {
                    ping.Stop();
                    break;
                } 
            }

           
        }
    }
}
