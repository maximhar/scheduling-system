using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class ProfessorDayAndTimeConstraint : IConstraint
    {
        public List<Prevent> prevent;
        public ProfessorDayAndTimeConstraint(List<Prevent> aPrevent)
        {
            prevent = aPrevent;
        }

        public ConstraintResult Check(Schedule sched)
        {

            bool pass = true;
            StringBuilder errorContainer = new StringBuilder();
            
            foreach (var prev in prevent)
            {
                int day = prev.day;

                if (sched[day].Count == 0)
                {
                    continue;
                }
                foreach (var group in Configuration.Instance.Groups)
                {
                    foreach (var classs in sched[day][group])
                    {
                        if (classs.Course.Professor == prev.prof)
                        {
                            TimeSpan classStart = sched.GetStartTimeForClass(day, group, classs);
                            TimeSpan classEnd = classStart + classs.Length;
                            if ((classStart >= prev.start && classStart <= prev.end) || (classEnd >= prev.start && classEnd <= prev.end))
                            {
                                pass = false;
                                string error = string.Format("Professor Day&Time conflict: professor {0} conflicts on day {3} between {1}-{2}",
                                    prev.prof, classStart, classEnd, prev.day);
                                errorContainer.AppendLine(error);
                            }
                        }
                    }
                }
            }
            return new ConstraintResult(pass, errorContainer.ToString());
        }
    }
}
