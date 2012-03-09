using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    class Configuration
    {
        private static Configuration instance;
        private Configuration() { }
        public List<Professor> Professors = new List<Professor>();
        public List<Course> Courses = new List<Course>();
        public List<StudentGroup> Groups = new List<StudentGroup>();
        public List<IConstraint> Constraints = new List<IConstraint>();
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
    
        
    }
}
