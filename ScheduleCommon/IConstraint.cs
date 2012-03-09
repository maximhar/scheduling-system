using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public interface IConstraint
    {
        ConstraintResult Check(Schedule aSchedule);
    }
}
