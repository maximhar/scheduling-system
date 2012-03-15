using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class Prevent
    {
        public Professor prof { get; set; }
        public int day { get; set; }
        public TimeSpan start { get; set; }
        public TimeSpan end { get; set; }

        public Prevent(Professor aProf, int aDay, TimeSpan aStart, TimeSpan aEnd)
        {
            prof = aProf;
            day = aDay;
            start = aStart;
            end = aEnd;
        }

        public override string ToString()
        {
            return string.Format("Professor {0} can't work on {1} form {2} to {3}", prof, day, start, end);
        }
    }
}
