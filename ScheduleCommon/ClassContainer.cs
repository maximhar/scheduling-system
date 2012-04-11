using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    [Serializable]
    public class ClassContainer
    {
        Class classPrototype;
        int classCount;
        public int Count
        {
            get { return classCount; }
        }
        public ClassContainer(Class aClass, int aCount)
        {
            classPrototype = (Class)aClass.Clone();
            classCount = aCount;
        }
        
        public Class GetClass()
        {
            if (classCount > 0)
            {
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
                classCount++;
            }
            else
            {
                throw new ArgumentException("The class is not contained in the list.");
            }
        }
    }
}
