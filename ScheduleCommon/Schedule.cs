using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class Schedule
    {
        Dictionary<StudentGroup, List<Class>>[] days = new Dictionary<StudentGroup, List<Class>>[7];
        Dictionary<StudentGroup, TimeSpan>[] startTimes = new Dictionary<StudentGroup, TimeSpan>[7];
        public Schedule()
        {
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = new Dictionary<StudentGroup, List<Class>>();
                startTimes[i] = new Dictionary<StudentGroup, TimeSpan>();
            }
        }
        public Dictionary<StudentGroup, List<Class>> this[int index]
        {
            get
            {
                return days[index];
            }
            set
            {
                days[index] = value;
            }
        }
        public TimeSpan GetStartTime(int aDay, StudentGroup aGroup)
        {
            if (aDay >= startTimes.Length || aDay < 0)
                throw new ArgumentOutOfRangeException("aDay", "Day should be between 0 and 6");
            if (!startTimes[aDay].ContainsKey(aGroup))
                throw new ArgumentException("Student group not found", "aGroup");
            return startTimes[aDay][aGroup];
        }
        public void SetStartTime(int aDay, StudentGroup aGroup, TimeSpan aStartTime)
        {
            if (aDay >= startTimes.Length || aDay < 0)
                throw new ArgumentOutOfRangeException("aDay", "Day should be between 0 and 6");
            if (startTimes[aDay].ContainsKey(aGroup))
            {
                startTimes[aDay][aGroup] = aStartTime;
            }
            else
            {
                startTimes[aDay].Add(aGroup, aStartTime);
            }
        }
    }
}
