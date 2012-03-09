using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    public class Class
    {
        public StudentGroup Group { get; set; }
        public Course Course { get; set; }
        public TimeSpan Length { get; set; }
        public Class(StudentGroup aGroup, Course aCourse, TimeSpan aLength)
        {
            Group = aGroup;
            Course = aCourse;
            Length = aLength;
        }
    }
}
