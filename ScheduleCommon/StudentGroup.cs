using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class StudentGroup
    {
        public string Name { get; set; }
        public StudentGroup(string aName)
        {
            Name = aName;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
