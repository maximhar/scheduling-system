using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    class ProfessorTimeConstraint : IConstraint
    {
        public ProfessorTimeConstraint()
        {
        }

        public ConstraintResult Check(Schedule sched)
        {
            return new ConstraintResult(false, "ProfessorTimeConstraint not implemented yet!");
        }
    }
}
