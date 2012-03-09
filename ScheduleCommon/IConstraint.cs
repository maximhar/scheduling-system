using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    /// <summary>
    /// A schedule constraint.
    /// </summary>
    public interface IConstraint
    {
        ConstraintResult Check(Schedule aSchedule);
    }
}
