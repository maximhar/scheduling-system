using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    class ProfessorDayConstraint : IConstraint
    {
        private Professor prof;
        private List<int> off;
        public ProfessorDayConstraint(Professor professor, List<int> daysOff)
        {
            this.prof = professor;
            this.off = daysOff;
        }

        public ConstraintResult Check(Schedule sched)
        {
            bool pass = true;
            StringBuilder errorContainer = new StringBuilder();

            foreach (int day in off)
            {
                foreach(var group in Configuration.Instance.Groups)
                {
                    for (int classs = 0; classs < sched[day][group].Count; classs++ )
                    {
                        if (sched[day][group][classs].Course.Professor == prof)
                        {
                            pass = false;
                            string error = string.Format("Professor Days Off conflict: professor {0} conflicts in day {1}",
                                sched[day][group][classs].Course.Professor, day);
                            errorContainer.AppendLine(error);
                        }
                    }
                }
            }
            return new ConstraintResult(pass, errorContainer.ToString());
        }
    }
}
