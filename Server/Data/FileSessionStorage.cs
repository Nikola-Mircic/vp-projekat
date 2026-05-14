using Common;
using Server.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    internal class FileSessionStorage : ISessionStorage
    {
        StreamWriter writer;

        public bool PushSample(EegSample sample)
        {
            try
            {
                writer.WriteLine(sample.ToString());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool StartSession(EegMeta meta)
        {
            string path = $"Data/{meta.ParticipantId}/{DateTime.Now:yyyy-MM-dd}";

            try
            {
                Directory.CreateDirectory(path);
                writer = new StreamWriter(Path.Combine(path, "session.csv"), true);

                return true;
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public void Dispose()
        {
            if(writer != null)
                writer.Dispose();
        }
    }
}
