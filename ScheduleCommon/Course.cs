using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    [Serializable]
    public enum CourseType
    {
        NormalCourse,
        ComputerCourse
    }
    [Serializable]
    public class Course
    {
        public string Name { get; set; }
        public Professor Professor { get; set; }
        public CourseType CourseType { get; set; }
        public Course(string aName, Professor aProf, CourseType aType)
        {
            Name = aName;
            Professor = aProf;
            CourseType = aType;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
