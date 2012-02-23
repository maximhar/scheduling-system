using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleConcept
{
    [Serializable]
    public class Course
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Course(int id, string name)
        {
            ID = id;
            Name = name;
        }
    }
}
