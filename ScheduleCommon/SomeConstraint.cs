using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    class SomeConstraint:IConstraint
    {

        public ConstraintResult Check(Schedule aSchedule)
        {
            return new ConstraintResult(false, "Nothing to do here!");
        }
    }
}
