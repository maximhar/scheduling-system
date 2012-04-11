using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;

namespace ScheduleCommon
{
    /// <summary>
    /// Contains common data that applies to all schedules.
    /// </summary>
    [Serializable]
    public class Configuration
    {
        private static Configuration instance;
        public ObservableCollection<Professor> Professors { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<StudentGroup> Groups { get; set; }
        public ObservableCollection<IConstraint> Constraints { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
        public Dictionary<StudentGroup, TrulyObservableCollection<ClassContainer>> Classes { get; set; }
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
            Professors = new ObservableCollection<Professor>();
            Courses = new ObservableCollection<Course>();
            Groups = new ObservableCollection<StudentGroup>();
            Constraints = new ObservableCollection<IConstraint>();
            Rooms = new ObservableCollection<Room>();
            Classes = new Dictionary<StudentGroup, TrulyObservableCollection<ClassContainer>>();
        }

        public void Clear()
        {
            Professors.Clear();
            Courses.Clear();
            Groups.Clear();
            Constraints.Clear();
            Rooms.Clear();
            Classes.Clear();
        }

        public void SaveToFile(string aPath)
        {
            using (Stream stream = File.Open(aPath, FileMode.Create))
            {
                BinaryFormatter bin = new BinaryFormatter();
                bin.Serialize(stream, this);
            }
        }

        public void LoadFromFile(string aPath)
        {
            using (Stream stream = File.Open(aPath, FileMode.Open))
            {
                Clear();
                BinaryFormatter bin = new BinaryFormatter();
                Configuration deserialized = bin.Deserialize(stream) as Configuration;
                foreach (var prof in deserialized.Professors)
                {
                    Professors.Add(prof);
                }
                foreach (var room in deserialized.Rooms)
                {
                    Rooms.Add(room);
                }
                foreach (var group in deserialized.Groups)
                {
                    Groups.Add(group);
                }
                foreach (var course in deserialized.Courses)
                {
                    Courses.Add(course);
                }
                foreach (var constraint in deserialized.Constraints)
                {
                    Constraints.Add(constraint);
                }
                foreach (var classcont in deserialized.Classes)
                {
                    Classes.Add(classcont.Key, classcont.Value);
                }
            }
        }
    }
}
