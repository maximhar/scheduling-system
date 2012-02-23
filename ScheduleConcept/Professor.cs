using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleConcept
{
    [Serializable]
    public class Professor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<CourseClass> Classes { get; set; }
        public static bool operator ==(Professor p1, Professor p2)
        {
            return p1.ID == p2.ID;
        }
        public static bool operator !=(Professor p1, Professor p2)
        {
            return p1.ID != p2.ID;
        }
        public Professor(int id, string name)
        {
            ID = id;
            Name = name;
            Classes = new List<CourseClass>();
        }
        public void AddCourseClass(CourseClass courseClass)
        {
            courseClass.ClassProfessor = this;
            Classes.Add(courseClass);
        }
    }
}
