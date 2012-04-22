using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class ConversionServices
    {
        private static List<string> dayNames = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        public static string GetDayNameFromDayNumber(int aDay)
        {
            if (aDay < 0 || aDay >= dayNames.Count)
            {
                throw new ArgumentException("This day is unknown.");
            }
            return dayNames[aDay];
        }
    }
}
