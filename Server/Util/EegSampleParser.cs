using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Util
{
    public class EegSampleParser
    {
        public static EegSample TryParse(string sample)
        {
            string[] parts = sample.Split(',');

            EegSample res = new EegSample();
            res.Timestamp = DateTime.Parse(parts[0]);
            res.AF3 = double.Parse(parts[1]);
            res.T7 = double.Parse(parts[2]);
            res.Pz = double.Parse(parts[3]);
            res.T8 = double.Parse(parts[4]);
            res.AF4 = double.Parse(parts[5]);
            res.Attention = double.Parse(parts[6]);
            res.Engagement = double.Parse(parts[7]);
            res.Excitement = double.Parse(parts[8]);
            res.Interest = double.Parse(parts[9]);
            res.Relaxation = double.Parse(parts[10]);
            res.Stress = double.Parse(parts[11]);
            res.Battery = int.Parse(parts[12]);
            res.ContactQuality = int.Parse(parts[13]);
            res.SlideIndex = int.Parse(parts[14]);
            res.SetIndex = int.Parse(parts[15]);
            res.RowIndex = int.Parse(parts[16]);

            return res;
        }
    }
}
