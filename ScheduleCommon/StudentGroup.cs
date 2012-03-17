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
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return ((StudentGroup)obj).Name == Name;
        }
    }
}
