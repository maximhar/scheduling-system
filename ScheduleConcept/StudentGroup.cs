using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleConcept
{
    [Serializable]
    public class StudentGroup
    {
        
        public int ID { get; set; }
        public int Size { get; set; }
        public String Name { get; set; }
        public List<CourseClass> Classes { get; set; }
	    public static bool operator ==(StudentGroup gr1, StudentGroup gr2)
        { 
            return gr1.ID == gr2.ID; 
        }
        public static bool operator !=(StudentGroup gr1, StudentGroup gr2)
        {
            return gr1.ID != gr2.ID;
        }
        public StudentGroup(int id, string name, int numberOfStudents)
        {
            ID = id;
            Name = name;
            Size = numberOfStudents;
            Classes = new List<CourseClass>();
        }
    }
}
