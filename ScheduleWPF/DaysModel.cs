using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using GongSolutions.Wpf.DragDrop;
using System.Windows;
using System.Collections;
using ScheduleCommon;
namespace ScheduleWPF
{
    class DaysModel:IDropTarget
    {
        public Schedule CurrentSchedule { get; set; }
        public ObservableCollection<ConstraintResult> Conflicts { get; set; }
        void InitializeSchedule()
        {
            Conflicts = new ObservableCollection<ConstraintResult>();
            
            Configuration.Instance.Groups = new List<StudentGroup>{new StudentGroup("11A"),
                new StudentGroup("11B"), new StudentGroup("11V"), new StudentGroup("11G")};
            Configuration.Instance.Professors = new List<Professor>{new Professor("Abramowitch"),
                new Professor("Mitova"), new Professor("Mitov")};
            Configuration.Instance.Courses = new List<Course>
                {new Course("Maths", Configuration.Instance.Professors[0], CourseType.NormalCourse),
                new Course("BEL", Configuration.Instance.Professors[1], CourseType.NormalCourse), 
                new Course("TP", Configuration.Instance.Professors[2], CourseType.ComputerCourse)};
            Configuration.Instance.Rooms = new List<Room>{new Room("42", CourseType.NormalCourse), new Room("21", CourseType.NormalCourse),
                new Room("34", CourseType.ComputerCourse)};
            Configuration.Instance.Constraints.Add(new ProfessorDayConstraint(Configuration.Instance.Professors[0], new List<int> { 0, 1 }));
            var groups = Configuration.Instance.Groups;
            var courses = Configuration.Instance.Courses;
            var rooms = Configuration.Instance.Rooms;
            var profs = Configuration.Instance.Professors;
            for(int i = 0;i<7;i++)
            {
                CurrentSchedule[i][groups[0]] = new ObservableCollection<Class>();
                CurrentSchedule[i][groups[1]] = new ObservableCollection<Class>();
                CurrentSchedule[i][groups[2]] = new ObservableCollection<Class>();
                CurrentSchedule[i][groups[3]] = new ObservableCollection<Class>();
            }
            CurrentSchedule[0][groups[0]].Add(new Class(groups[0], courses[0], TimeSpan.FromMinutes(80), rooms[1]));
            CurrentSchedule[0][groups[0]].Add(new Class(groups[0], courses[1], TimeSpan.FromMinutes(80), rooms[0]));
            CurrentSchedule[0][groups[0]].Add(new Class(groups[0], courses[2], TimeSpan.FromMinutes(80), rooms[2]));
        }
        public DaysModel()
        {
            CurrentSchedule = new Schedule();
            var conf = Configuration.Instance;
            InitializeSchedule();
            EvaluateConstraints();
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            if (dropInfo.Data is Class)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
                var daydrop = (Class)dropInfo.Data;
                var source = ((IList)dropInfo.DragInfo.SourceCollection);
                var target = ((IList)dropInfo.TargetCollection);
                int indexmodifier = 0;
                if ((source.IndexOf(daydrop) < dropInfo.InsertIndex) && (dropInfo.TargetCollection == dropInfo.DragInfo.SourceCollection)) indexmodifier = -1;
                source.Remove(daydrop);
                if (target.Count > 0)
                {
                    target.Insert(dropInfo.InsertIndex + indexmodifier, (Class)daydrop);
                }
                else
                {
                    target.Add((Class)daydrop);
                }
                EvaluateConstraints();
        }
        void EvaluateConstraints()
        {
            Conflicts.Clear();
            foreach (var constraint in Configuration.Instance.Constraints)
            {
                var result = constraint.Check(CurrentSchedule);
                if (!result.ConstraintFulfilled) Conflicts.Add(result);
            }
        }
    }
}
