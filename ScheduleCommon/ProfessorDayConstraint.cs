using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    class ProfessorDayConstraint : IConstraint
    {
        public ProfessorDayConstraint()
        {
        }

        public ConstraintResult Check(Schedule sched)
        {
            return new ConstraintResult(false, "ProfessorDayConstraint not implemented yet!");
        }
    }
}
