using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class RoomTimeOverlapConstraint : IConstraint
    {
        public RoomTimeOverlapConstraint()
        {
        }
        public ConstraintResult Check(Schedule aSchedule)
        {
            return new ConstraintResult(false, "Constraint not implemented yet.");
        }
    }
}
