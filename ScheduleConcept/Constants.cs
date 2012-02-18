using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleConcept
{
    class Constants
    {
        public const int DAYS = 5;
        public const int HOURS_PER_DAY = 16;
        public static TimeSpan MINS_PER_SLOT = new TimeSpan(0, 40, 0);
        public static TimeSpan DAY_BEGIN = new TimeSpan(8, 0, 0);
    }
}
