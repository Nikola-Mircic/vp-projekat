using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Config
{
    public class AppConfig
    {
        public static int BatteryLowThreshold =>
            int.Parse(ConfigurationManager.AppSettings["BatteryLowThreshold"]);

        public static int ContactQualityMin =>
            int.Parse(ConfigurationManager.AppSettings["ContactQualityMin"]);

        public static double StressSpikeThreshold =>
            double.Parse(ConfigurationManager.AppSettings["StressSpikeThreshold"]);

        public static double RelaxationDropThreshold =>
            double.Parse(ConfigurationManager.AppSettings["RelaxationDropThreshold"]);

        public static int TimestampSkewMaxMs =>
            int.Parse(ConfigurationManager.AppSettings["TimestampSkewMaxMs"]);
    }
}
