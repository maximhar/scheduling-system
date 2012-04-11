using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ScheduleCommon
{
    [Serializable]
    public class ClassContainer:INotifyPropertyChanged
    {
        readonly Class classPrototype;
        int classCount;
        public int Count
        {
            get { return classCount; }
            private set { classCount = value; OnCountChanged(); }
        }
        public Class Prototype
        {
            get { return classPrototype; }
        }
        public ClassContainer(Class aClass, int aCount)
        {
            classPrototype = (Class)aClass.Clone();
            classCount = aCount;
        }
        
        public Class GetClass()
        {
            if (Count > 0)
            {
                Count--;
                return (Class)classPrototype.Clone();
            }
            else
            {
                throw new InvalidOperationException("Not enough classes left.");
            }
        }
        public void AddClass(Class aClass, IList<Class> aList)
        {
            if (aClass.Course != classPrototype.Course)
            {
                throw new ArgumentException("The class passed as argument isn't compatible with the prototype class.");
            }
            if (aList.Contains(aClass))
            {
                aList.Remove(aClass);
                Count++;
            }
            else
            {
                throw new ArgumentException("The class is not contained in the list.");
            }
        }
        public override string ToString()
        {
            return string.Format("{0}: {1}", classPrototype.Course, classCount);
        }
        void OnCountChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
