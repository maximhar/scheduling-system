using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    /// <summary>
    /// Contains common data that applies to all schedules.
    /// </summary>
    class Configuration
    {
        private static Configuration instance;
        public List<Professor> Professors { get; set; }
        public List<Course> Courses { get; set; }
        public List<StudentGroup> Groups { get; set; }
        public List<IConstraint> Constraints { get; set; }
        public List<Room> Rooms { get; set; }
        public static Configuration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Configuration();
                }
                return instance;
            }
        }
        private Configuration() 
        {
            Professors = new List<Professor>();
            Courses = new List<Course>();
            Groups = new List<StudentGroup>();
            Constraints = new List<IConstraint>();
            Rooms = new List<Room>();
        }
        
    }
}
