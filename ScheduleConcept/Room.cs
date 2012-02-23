using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScheduleConcept
{
    [Serializable]
    public class Room
    {
        public int ID { get; set; }
        public int Capacity { get; set; }
        public string Name { get; set; }
        public bool IsLab { get; set; }
        static int _nextRoomID;
        public Room(string name, bool lab, int size) 
        {
            Name = name;
            ID = _nextRoomID++;
            IsLab = lab;
            Capacity = size;
        }
        static void ResetIDs() { _nextRoomID = 0; }
    }
}
