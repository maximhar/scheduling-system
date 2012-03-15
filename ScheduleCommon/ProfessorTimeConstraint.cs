using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class ProfessorTimeConstraint : IConstraint
    {
        private Professor prof;
        private TimeSpan start;
        private TimeSpan end;
        public ProfessorTimeConstraint(Professor professor, TimeSpan start, TimeSpan end)
        {
            this.prof = professor;
            this.start = start;
            this.end = end;
        }

        public ConstraintResult Check(Schedule sched)
        {
            bool pass = true;
            StringBuilder errorContainer = new StringBuilder();

            for (int day = 0; day < 6; day++)
            {
                if (sched[day].Count == 0)
                {
                    continue;
                }
                foreach (var group in Configuration.Instance.Groups)
                {
                    foreach (var classs in sched[day][group])
                    {
                        if (classs.Course.Professor == prof)
                        {
                            TimeSpan classStart = sched.GetStartTimeForClass(day, group, classs);
                            TimeSpan classEnd = classStart + classs.Length;
                            if( (classStart>=start && classStart<=end) || (classEnd>=start && classEnd<=end) ){
                                pass = false;
                                string error = string.Format("Professor Time Off conflict: professor {0} conflicts between {1}-{2}",
                                    prof, classStart, classEnd);
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
