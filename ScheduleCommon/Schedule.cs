using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    /// <summary>
    /// A schedule of all classes through all days in a single school.
    /// </summary>
    public class Schedule
    {
        const int DAYCOUNT = 7;
        Dictionary<StudentGroup, List<Class>>[] days = new Dictionary<StudentGroup, List<Class>>[DAYCOUNT];
        Dictionary<StudentGroup, TimeSpan>[] startTimes = new Dictionary<StudentGroup, TimeSpan>[DAYCOUNT];
        public Schedule()
        {
            for (int i = 0; i < days.Length; i++)
            {
                days[i] = new Dictionary<StudentGroup, List<Class>>();
                startTimes[i] = new Dictionary<StudentGroup, TimeSpan>();
            }
        }
        /// <summary>
        /// A dictionary of StudentGroups and corresponding List of Classes
        /// </summary>
        /// <param name="index">The day of the week.</param>
        /// <returns></returns>
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
        public TimeSpan GetStartTimeForClass(int aDay, StudentGroup aGroup, Class aClass)
        {
            if (aDay < 0 || aDay >= days.Length) throw new ArgumentOutOfRangeException("aDay", "Day should be between 0 and 6");
            if (aGroup == null) throw new ArgumentNullException("aGroup");
            if (aClass == null) throw new ArgumentNullException("aClass");
            if (!startTimes[aDay].ContainsKey(aGroup))
                throw new ArgumentException("Student group not found in start times", "aGroup");
            if (!days[aDay].ContainsKey(aGroup))
                throw new ArgumentException("Student group not found", "aGroup");
            TimeSpan startTime = startTimes[aDay][aGroup];
            foreach (var cl in days[aDay][aGroup])
            {
                if (cl == aClass)
                {
                    return startTime;
                }
                startTime += cl.Length;
            }
            throw new ArgumentException(string.Format("{0} does not exist in day {1}, group {2}", aClass, aDay, aGroup));
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
