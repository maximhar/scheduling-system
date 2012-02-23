using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
namespace ScheduleConcept
{
    public class Configuration
    {
        static Configuration _instance;
        Random _random = new Random(DateTime.Now.Millisecond);
        static public Configuration GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Configuration();
            }
            return _instance;
        }
        
        public Room GetLabRoom()
        {
            int labrooms = Rooms.Sum(r =>
                {
                    if (r.Value.IsLab) return 1;
                    return 0;
                });
            int lind = 0;
            int rand = _random.Next(labrooms);
            foreach (var r in Rooms)
            {
                if (lind == rand && r.Value.IsLab) return r.Value;
                if(r.Value.IsLab)lind++;
            }
            return null;
        }
        public Room GetNonLabRoom()
        {
            int nlabrooms = Rooms.Sum(r =>
            {
                if (!r.Value.IsLab) return 1;
                return 0;
            });
            int lind = 0;
            int rand = _random.Next(nlabrooms);
            foreach (var r in Rooms)
            {
                if (lind == rand) return r.Value;
                if(!r.Value.IsLab) lind++;
            }
            return null;
        }
        public Configuration()
        {
            Empty = true;
            Professors = new Dictionary<int, Professor>();
            StudentGroups = new Dictionary<int, StudentGroup>();
            Courses = new Dictionary<int, Course>();
            Rooms = new Dictionary<int, Room>();
            CourseClasses = new List<CourseClass>();
        }
        public void ReadFromXML(string path)
        {
            XElement doc = XElement.Load(path);
            var professors = from el in doc.Elements()
                             where el.Name == "prof"
                             select el;
            var courses = from el in doc.Elements()
                         where el.Name == "course"
                         select el;
            var rooms = from el in doc.Elements()
                        where el.Name == "room"
                        select el;
            var groups = from el in doc.Elements()
                         where el.Name == "group"
                         select el;
            var classes = from el in doc.Elements()
                          where el.Name == "class"
                          select el;
            foreach (var prof in professors)
            {
                string name = prof.Attribute("name").Value;
                int id = int.Parse(prof.Attribute("id").Value);
                Professor p = new Professor(id, name);
                Professors.Add(id, p);
            }
            foreach (var course in courses)
            {
                string name = course.Attribute("name").Value;
                int id = int.Parse(course.Attribute("id").Value);
                Course c = new Course(id, name);
                Courses.Add(id, c);
            }
            foreach (var room in rooms)
            {
                string name = room.Attribute("name").Value;
                int cap = int.Parse(room.Attribute("capacity").Value);
                bool lab = bool.Parse(room.Attribute("lab").Value);
                Room r = new Room(name, lab, cap);
                Rooms.Add(r.ID, r);
            }
            foreach (var group in groups)
            {
                string name = group.Attribute("name").Value;
                int id = int.Parse(group.Attribute("id").Value);
                int size = int.Parse(group.Attribute("size").Value);
                StudentGroup g = new StudentGroup(id, name, size);
                StudentGroups.Add(id, g);
            }
            foreach (var cclass in classes)
            {
                int dur = int.Parse(cclass.Attribute("dur").Value);
                bool requireslab = bool.Parse(cclass.Attribute("requireslab").Value);
                var prof = GetProfessorByID(int.Parse(cclass.Attribute("professor").Value));
                var group = GetStudentGroupByID(int.Parse(cclass.Attribute("group").Value));
                var course = GetCourseByID(int.Parse(cclass.Attribute("course").Value));
                CourseClass c = new CourseClass(prof, course, new List<StudentGroup> { group }, requireslab, dur);
                CourseClasses.Add(c);
                group.Classes.Add(c);
                prof.Classes.Add(c);
            }
        }
        public Dictionary<int, Professor> Professors { get; set; }
        public Dictionary<int, StudentGroup> StudentGroups { get; set; }
        public Dictionary<int, Course> Courses { get; set; }
        public Dictionary<int, Room> Rooms { get; set; }
        public List<CourseClass> CourseClasses { get; set; }
        public bool Empty { get; set; }
        public Course GetCourseByID(int id)
        {
            Course result = null;
            Courses.TryGetValue(id, out result);
            return result;
        }
        public Room GetRoomByID(int id)
        {
            Room result = null;
            Rooms.TryGetValue(id, out result);
            return result;
        }
        public StudentGroup GetStudentGroupByID(int id)
        {
            StudentGroup result = null;
            StudentGroups.TryGetValue(id, out result);
            return result;
        }
        public Professor GetProfessorByID(int id)
        {
            Professor result = null;
            Professors.TryGetValue(id, out result);
            return result;
        }

    }
}
