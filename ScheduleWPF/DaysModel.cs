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
namespace ScheduleWPF
{
    class DaysModel:IDropTarget
    {
        public ObservableCollection<string> Monday { get; private set; }
        public ObservableCollection<string> Tuesday { get; private set; }
        public ObservableCollection<string> Wednesday { get; private set; }
        public ObservableCollection<string> Thursday { get; private set; }
        public ObservableCollection<string> Friday { get; private set; }
        public DaysModel()
        {
            ObservableCollection<string> monday = new ObservableCollection<string> { "BEL", "Maths", "History" };
            ObservableCollection<string> tuesday = new ObservableCollection<string> { "Maths", "PPS", "TP" };
            ObservableCollection<string> wednesday = new ObservableCollection<string> { "OOP", "SUBD", "WEBD" };
            ObservableCollection<string> thursday = new ObservableCollection<string> { "Philo", "AE", "RE" };
            ObservableCollection<string> friday = new ObservableCollection<string> { "TP", "AE", "OS" };
            Monday = monday;
            Tuesday = tuesday;
            Wednesday = wednesday;
            Thursday = thursday;
            Friday = friday;
        }

        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            if (dropInfo.Data is string)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {
            var dayon = (string)dropInfo.TargetItem;
            var daydrop = (string)dropInfo.Data;
            
            ((IList)dropInfo.TargetCollection).Insert(dropInfo.InsertIndex, daydrop);
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(daydrop);
        }
    }
}
