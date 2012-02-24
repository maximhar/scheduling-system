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
using ScheduleShared;
namespace ScheduleWPF
{
    class DaysModel:IDropTarget
    {
        public ObservableCollection<CourseClass> Monday { get; private set; }
        public ObservableCollection<CourseClass> Tuesday { get; private set; }
        public ObservableCollection<CourseClass> Wednesday { get; private set; }
        public ObservableCollection<CourseClass> Thursday { get; private set; }
        public ObservableCollection<CourseClass> Friday { get; private set; }
        public DaysModel()
        {
            var conf = Configuration.GetInstance();
            conf.ReadFromXML("info.xml");
            Monday = new ObservableCollection<CourseClass>(from c in conf.CourseClasses
                                                           where c.ID < 8
                                                           select c);
            Tuesday = new ObservableCollection<CourseClass>(from c in conf.CourseClasses
                                                            where c.ID > 8 && c.ID <= 16
                                                            select c);
            Wednesday = new ObservableCollection<CourseClass>(from c in conf.CourseClasses
                                                              where c.ID > 16 && c.ID <= 24
                                                              select c);
            Thursday = new ObservableCollection<CourseClass>(from c in conf.CourseClasses
                                                             where c.ID > 24 && c.ID <= 32
                                                             select c);
            Friday = new ObservableCollection<CourseClass>(from c in conf.CourseClasses
                                                           where c.ID > 32 && c.ID <= 40
                                                           select c);
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            if (dropInfo.Data is CourseClass)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
                var daydrop = (CourseClass)dropInfo.Data;
                var source = ((IList)dropInfo.DragInfo.SourceCollection);
                var target = ((IList)dropInfo.TargetCollection);
                int indexmodifier = 0;
                if ((source.IndexOf(daydrop) < dropInfo.InsertIndex) && (dropInfo.TargetCollection == dropInfo.DragInfo.SourceCollection)) indexmodifier = -1;
                source.Remove(daydrop);
                target.Insert(dropInfo.InsertIndex+indexmodifier, (CourseClass)daydrop);
        }
    }
}
