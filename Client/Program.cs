using Common;
using Common.Faults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChannelFactory<IService> factory = new ChannelFactory<IService>("Server");

            IService proxy = factory.CreateChannel();

            Console.WriteLine("Enter participant ID (1-20):\n");
            int target = int.Parse(Console.ReadLine());

            EegMeta meta = new EegMeta
            {
                ParticipantId = $"{target}",
                FileName = $"subject_{target}_results.csv",
                TotalRows = 10,
                SchemaVersion = "1.0"
            };

            Console.WriteLine("Starting session...");
            proxy.StartSession(meta);

            StreamReader reader = new StreamReader($"EEG/{meta.FileName}");
            string line = reader.ReadLine();
            Console.WriteLine($"For participant {target} reading:\n{line}");
            for (int i = 0; i < meta.TotalRows; i++) { 
                line = reader.ReadLine();
                try
                {
                    if (!proxy.PushSample($"{line},{i + 1}"))
                    {
                        Console.WriteLine("Failed to push sample, aborting.");
                        break;
                    }
                } catch (FaultException<ValidationFault> e) 
                {
                    Console.WriteLine(e.Detail.Message);
                }
                
            }

            Console.WriteLine("Ending session...");

            proxy.EndSession();

            Console.WriteLine("Session ended. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
