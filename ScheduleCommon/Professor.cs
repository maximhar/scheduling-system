using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleCommon
{
    [Serializable]
    public class Professor
    {
        public string Name { get; set; }
        //private bool empty;
        public static readonly Professor Empty = new Professor();
        private Professor()
        {
            Name = string.Empty;
        }
        public Professor(string aName)
        {
            Name = aName;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
