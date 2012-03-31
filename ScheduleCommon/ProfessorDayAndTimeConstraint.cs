using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    [Serializable]
    public class ProfessorDayAndTimeConstraint : IConstraint
    {
        private List<TimeDayRequirement> requirements;
        public ProfessorDayAndTimeConstraint(List<TimeDayRequirement> aRequirements)
        {
            requirements = aRequirements;
        }

        public ConstraintResult Check(Schedule sched)
        {

            bool pass = true;
            StringBuilder errorContainer = new StringBuilder();
            
            foreach (var req in requirements)
            {
                int day = req.Day;

                if (sched[day].Count == 0)
                {
                    continue;
                }
                foreach (var group in Configuration.Instance.Groups)
                {
                    foreach (var classs in sched[day][group])
                    {
                        if (classs.Course.Professor == req.Professor)
                        {
                            TimeSpan classStart = sched.GetStartTimeForClass(day, group, classs);
                            TimeSpan classEnd = classStart + classs.Length;
                            if ((classStart >= req.Start && classStart <= req.End) || (classEnd >= req.Start && classEnd <= req.End))
                            {
                                pass = false;
                                string error = string.Format("Professor Day&Time conflict: professor {0} conflicts on day {3} between {1}-{2}",
                                    req.Professor, classStart, classEnd, req.Day);
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
