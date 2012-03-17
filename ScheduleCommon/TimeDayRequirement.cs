using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class TimeDayRequirement
    {
        public Professor Professor { get; set; }
        public int Day { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }

        public TimeDayRequirement(Professor aProf, int aDay, TimeSpan aStart, TimeSpan aEnd)
        {
            Professor = aProf;
            Day = aDay;
            Start = aStart;
            End = aEnd;
        }

        public override string ToString()
        {
            return string.Format("{0} can't work on day {1} from {2} to {3}", Professor, Day, Start, End);
        }
    }
}
