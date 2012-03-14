using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class ProfessorTimeOverlapConstraint : IConstraint
    {
        public ProfessorTimeOverlapConstraint()
        {
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
                for (int groupN = 0; groupN < Configuration.Instance.Groups.Count - 1; groupN++)
                {
                    var group = Configuration.Instance.Groups[groupN];

                    for (int classsN = 0; classsN < sched[day][group].Count; classsN++)
                    {
                        int groupN2 = groupN + 1;
                        while (groupN2 < Configuration.Instance.Groups.Count)
                        {
                            var group2 = Configuration.Instance.Groups[groupN2];
                            var classs = sched[day][group][classsN];
                            var classs2 = sched[day][group2][classsN];

                            if (classs.Course.Professor == classs2.Course.Professor)
                            {
                                var start1 = sched.GetStartTimeForClass(day, group, classs);
                                var start2 = sched.GetStartTimeForClass(day, group2, classs2);
                                var end1 = start1 + classs.Length;
                                var end2 = start2 + classs2.Length;
                                if (end1 >= start2 && start2 <= end1 && end1 <= end2)
                                {
                                    pass = false;
                                    string error = string.Format("Professor conflict: professor {0} conflicts between group {1} and group {2} in day {3}",
                                        classs.Course.Professor , group, group2, day);
                                    errorContainer.AppendLine(error);
                                }
                            }
                            groupN2++;
                        }

                    }
                }
            }
            return new ConstraintResult(pass, errorContainer.ToString());
        }
    }
}
