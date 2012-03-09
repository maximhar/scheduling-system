using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public enum CourseType
    {
        NormalCourse,
        ComputerCourse
    }
    public class Course
    {
        public string Name { get; set; }
        public Professor Professor { get; set; }
        public CourseType CourseType { get; set; }
    }
}
