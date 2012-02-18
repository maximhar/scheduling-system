using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleConcept
{
    [Serializable]
    class CourseClass
    {
        static int _nextID = 0;

        public Course ClassCourse { get; set; }
        public Professor ClassProfessor { get; set; }
        public List<StudentGroup> StudentGroups { get; set; }
        public int ID { get; set; }
        public int Length { get; set; }
        public bool RequiresLab { get; set; }
        public int StudentCount
        {
            get
            {
                return StudentGroups.Sum(group => group.Size);
            }
        }
        public CourseClass Clone()
        {
            return (CourseClass)this.MemberwiseClone();
        }
        private CourseClass(Professor professor, Course course, List<StudentGroup> groups, bool requiresLab, int duration, int id)
        {
            ID = id;
            ClassProfessor = professor;
            ClassCourse = course;
            StudentGroups = groups;
            RequiresLab = requiresLab;
            Length = duration;
        }
        public CourseClass(Professor professor, Course course, List<StudentGroup> groups, bool requiresLab, int duration)
        {
            ID = _nextID++;
            ClassProfessor = professor;
            ClassCourse = course;
            StudentGroups = groups;
            RequiresLab = requiresLab;
            Length = duration;
        }
        public bool GroupsOverlap(CourseClass c) 
        {
            return c.StudentGroups.Any(group =>
                {
                    return StudentGroups.Any(innerGroup => innerGroup == group);
                });
        }
        public bool ProfessorOverlaps(CourseClass c ) { return ClassProfessor == c.ClassProfessor; }
        public override string ToString()
        {
            return string.Format("Course {0}, Professor {1}, Length {2}", ClassCourse.Name, ClassProfessor.Name, Length);
        }
        public static bool operator == (CourseClass a, Course b)
        {
            if ((object)a == null)
                if ((object)b == null)
                    return true;
                else
                    return false;
            if ((object)b == null) return false;

            return a.Equals(b);
        }
        public static bool operator !=(CourseClass a, Course b)
        {
            return !(a == b);
        }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (this.GetType() != obj.GetType()) return false;

            CourseClass other = obj as CourseClass;
            return other.ID == ID;
        }
        public override int GetHashCode()
        {
            return ID;
        }

    }
}
