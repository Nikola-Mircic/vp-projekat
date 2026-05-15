using Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Server));

            host.Open();

            Console.WriteLine("Server is running. Press Enter to stop.");
            Console.ReadLine();

            host.Close();
        }
    }
}
