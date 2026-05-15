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
        StreamWriter warningWriter;

        public FileSessionStorage() {
            try
            {
                Directory.CreateDirectory("Data/");
                warningWriter = new StreamWriter(Path.Combine("Data/", "rejects.csv"), true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool PushSample(EegSample sample)
        {
            try
            {
                writer.WriteLine(sample.ToCsv());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool PushWarning(Warning warning)
        {
            try
            {
                warningWriter.WriteLine(warning.ToCsv());
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
            string warningPath = $"Data/";

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

        public bool EndSession()
        {
            if (writer != null)
                writer.Dispose();

            return true;
        }

        public void Dispose()
        {
            EndSession();

            if(warningWriter != null)
                warningWriter.Dispose();
        }
    }
}
