using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    class ProfessorDayAndTimeConstraint : IConstraint
    {
        public ProfessorDayAndTimeConstraint()
        {
        }

        public ConstraintResult Check(Schedule sched)
        {
            return new ConstraintResult(false, "ProfessorDayAndTimeConstraint not implemented yet!");
        }
    }
}
