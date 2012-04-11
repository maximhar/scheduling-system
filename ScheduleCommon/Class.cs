using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScheduleCommon
{
    [Serializable]
    public class Class:INotifyPropertyChanged,ICloneable
    {
        StudentGroup group;
        Course course;
        TimeSpan length;
        Room room;
        public StudentGroup Group
        {
            get { return group; }
            set
            {
                group = value;
                OnPropertyChanged("Group");
            }
        }
        public Course Course
        {
            get { return course; }
            set
            {
                course = value;
                OnPropertyChanged("Course");
            }
        }
        public TimeSpan Length
        {
            get { return length; }
            set
            {
                length = value;
                OnPropertyChanged("Length");
            }
        }
        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged("Room");
            }
        }
        public Class(StudentGroup aGroup, Course aCourse, TimeSpan aLength, Room aRoom)
        {
            group = aGroup;
            course = aCourse;
            length = aLength;
            room = aRoom;
        }
        public override string ToString()
        {
            return string.Format("{0}, group {1}, length {2}, room {3}", Course, Group, Length, Room);
        }
        void OnPropertyChanged(string aPropertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
