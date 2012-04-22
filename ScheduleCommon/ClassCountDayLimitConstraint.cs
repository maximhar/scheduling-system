using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    [Serializable]
    public class ClassCountDayLimitConstraint : IConstraint
    {
        private int classLimit;
        public ClassCountDayLimitConstraint(int aClassLimit)
        {
            classLimit = aClassLimit;
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
                    int ClassCount = sched[day][group].Count;
                    if (ClassCount > classLimit)
                    {
                        pass = false;
                        string error = string.Format("Class count exceeds limit of {0}: group {1} has {2} classes on day {3}",
                            classLimit, group.Name, ClassCount, day);
                        errorContainer.AppendLine(error);
                    }
                }
            }
            return new ConstraintResult(pass, errorContainer.ToString());
        }
    }
}
