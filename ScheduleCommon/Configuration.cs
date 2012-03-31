using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ScheduleCommon
{
    /// <summary>
    /// Contains common data that applies to all schedules.
    /// </summary>
    public class Configuration
    {
        private static Configuration instance;
        public ObservableCollection<Professor> Professors { get; set; }
        public ObservableCollection<Course> Courses { get; set; }
        public ObservableCollection<StudentGroup> Groups { get; set; }
        public ObservableCollection<IConstraint> Constraints { get; set; }
        public ObservableCollection<Room> Rooms { get; set; }
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
        }
        public void Clear()
        {
            Professors.Clear();
            Courses.Clear();
            Groups.Clear();
            Constraints.Clear();
            Rooms.Clear();
        }
    }
}
