using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleWPF
{
    enum Day
    {
        Monday, 
        Tuesday,
        Wednesday,
        Thursday,
        Friday
    }
    interface IConstraint
    {
        public bool Validate(ScheduleShared.Schedule aSchedule);
    }

    class ConstraintByDay : IConstraint
    {
        private ScheduleShared.Professor professor_;
        private Day day_;
        public ConstraintByDay(Day aDay, ScheduleShared.Professor aProfessor)
        {
            professor_ = aProfessor;
            day_ = aDay;
        }
        bool IConstraint.Validate(ScheduleShared.Schedule aSchedule)
        {
            return false;
        }
    }

}
