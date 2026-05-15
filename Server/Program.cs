using Server.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(AppConfig.BatteryLowThreshold);
            Console.WriteLine(AppConfig.ContactQualityMin);
            Console.WriteLine(AppConfig.StressSpikeThreshold);
            Console.WriteLine(AppConfig.RelaxationDropThreshold);
            Console.WriteLine(AppConfig.TimestampSkewMaxMs);
        }
    }
}
