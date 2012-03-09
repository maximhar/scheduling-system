using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;

namespace ScheduleShared
{
    [Serializable]
    public class ScheduleOld
    {
        Random _random = new Random(DateTime.Now.Millisecond);
        List<CourseClass>[] _timeslots;
        public List<CourseClass>[] Timeslots
        {
            get
            {
                return _timeslots;
            }
        }
        public Dictionary<CourseClass, int> Classes
        {
            get
            {
                return _classes;
            }
        }
        Dictionary<CourseClass, int> _classes;
        float _fitness;
        public float Fitness
        {
            get
            {
                return _fitness;
            }
        }
        bool[] _criteria;
        int _numberOfCrossoverPoints;
        int _mutationSize;
        int _crossoverProbability;
        int _mutationProbability;
        int _daySize = Constants.HOURS_PER_DAY * Configuration.GetInstance().Rooms.Count;

        public void SetMutationProbability(int p)
        {
            _mutationProbability = p;
        }
        public void SetMutationSize(int p)
        {
            _mutationSize = p;
        }
        void InitializeListCapacities()
        {
            _timeslots = new List<CourseClass>[Constants.DAYS * Constants.HOURS_PER_DAY * Configuration.GetInstance().Rooms.Count];
            for (int i = 0; i < _timeslots.Length; i++)
            {
                _timeslots[i] = new List<CourseClass>();
            }
            _classes = new Dictionary<CourseClass, int>();
            _criteria = new bool[Configuration.GetInstance().Courses.Count * 5];
        }
        public ScheduleOld(int numberOfCrossoverPoints, int mutationSize, int crossoverProbability, int mutationProbability)
        {
            _numberOfCrossoverPoints = numberOfCrossoverPoints;
            _mutationSize = mutationSize;
            _crossoverProbability = crossoverProbability;
            _mutationProbability = mutationProbability;

            InitializeListCapacities();
        }
        
        public ScheduleOld(ScheduleOld c, bool shallow)
        {
            InitializeListCapacities();
            if (!shallow)
            {
                for (int i = 0; i < c._timeslots.Length; i++)
                {
                    foreach (var v in c._timeslots[i])
                    {
                        _timeslots[i].Add(v.Clone());
                    }
                }
                foreach (var k in c._classes)
                {
                    _classes.Add(k.Key.Clone(), k.Value); 
                }
                _criteria = c._criteria;
                _fitness = c._fitness;
            }
            _numberOfCrossoverPoints = c._numberOfCrossoverPoints;
            _mutationSize = c._mutationSize;
            _crossoverProbability = c._crossoverProbability;
            _mutationProbability = c._mutationProbability;
        }

        public ScheduleOld()
        {
            _timeslots = new List<CourseClass>[0];
            _classes = new Dictionary<CourseClass, int>();
            _criteria = new bool[0];
        }
        public ScheduleOld Clone(bool shallow)
        {
            return new ScheduleOld(this, shallow);
        }
        public ScheduleOld CreateNewFromPrototype() 
        {
            int size = _timeslots.Length;
            ScheduleOld newChromosome = new ScheduleOld(this, true);

            List<CourseClass> classes = Configuration.GetInstance().CourseClasses;
            int roomCount = Configuration.GetInstance().Rooms.Count;
            foreach (CourseClass c in classes)
            {
                int duration = c.Length;
                int day = _random.Next(Constants.DAYS);
                int room = c.RequiresLab ? Configuration.GetInstance().GetLabRoom().ID : Configuration.GetInstance().GetNonLabRoom().ID;
                int time = _random.Next(Constants.HOURS_PER_DAY + 1 - duration);
                int pos = day * roomCount * Constants.HOURS_PER_DAY + room * Constants.HOURS_PER_DAY + time;
                for(int i = duration-1;i>=0;i--)
                {
                    newChromosome._timeslots[pos+i].Add(c);
                }
                newChromosome._classes.Add(c, pos);
            }
            newChromosome.CalculateFitness();
            return newChromosome;
        }
        public ScheduleOld CrossOver(ScheduleOld parent2) 
        {
            if (_random.Next(100) > _crossoverProbability)
                return new ScheduleOld(this, false);

            ScheduleOld s = new ScheduleOld(this, true);
            int numberOfClasses = _classes.Count;
            bool[] cp = new bool[numberOfClasses];
            for (int i = _numberOfCrossoverPoints; i > 0; i--)
            {
                while (true)
                {
                    int p = _random.Next(numberOfClasses);
                    if (!cp[p])
                    {
                        cp[p] = true;
                        break;
                    }
                }
            }
            var classes1 = _classes.GetEnumerator();
            var classes2 = parent2._classes.GetEnumerator();
            bool first = _random.Next(0,2)==0;
            for (int i = 0; i < numberOfClasses; i++)
            {
                classes1.MoveNext();
                classes2.MoveNext();
                if (first)
                {
                    s._classes.Add(classes1.Current.Key, classes1.Current.Value);
                    for (int j = classes1.Current.Key.Length - 1; j >= 0; j--)
                    {
                        s._timeslots[classes1.Current.Value + j].Add(classes1.Current.Key);
                    }
                }
                else
                {
                    s._classes.Add(classes2.Current.Key, classes2.Current.Value);
                    for (int j = classes2.Current.Key.Length - 1; j >= 0; j--)
                    {
                        s._timeslots[classes2.Current.Value + j].Add(classes2.Current.Key);
                    }
                }
                if (cp[i]) first = !first;
                
            }
            s.CalculateFitness();
            return s;
        }
        public void Repair()
        {
            if (_random.Next(100) > 100)
                return;
            FixLabDependencies();
            FixRoomOccupancy();
            
            FixMultipleTimeslotOccupants();
            foreach (var c in _classes.ToList())
            {
                foreach(var c2 in _classes.ToList())
                {
                    if (c.Value == c2.Value) continue;
                    if (c.Key.StudentGroups[0] == c2.Key.StudentGroups[0])
                    {
                        if ((GetClassTime(c.Value) == GetClassTime(c2.Value)) && (GetClassDay(c.Value) == GetClassDay(c2.Value)))
                        {
                            CourseClass toMove = c2.Key;
                            int source = c2.Value;
                            int dest = GetRandomEmptyTimeslot(toMove.RequiresLab);
                            MoveSingleClass(source, dest, toMove);
                        }
                    }
                    
                }
            }
            FixClustering();
            CalculateFitness();
        }

        private void FixMultipleTimeslotOccupants()
        {
            int slotindex = 0;
            foreach (var c in _timeslots)
            {
                while (c.Count > 1)
                {
                    CourseClass toMove = c.Last();
                    int source = slotindex;
                    int dest = GetRandomEmptyTimeslot(toMove.RequiresLab);
                    MoveSingleClass(source, dest, toMove);
                }
                slotindex++;
            }
        }
        private int GetRandomEmptyTimeslot(bool lab)
        {
            while (true)
            {
                int rand = _random.Next(_timeslots.Length);
                if (Configuration.GetInstance().GetRoomByID(GetClassRoom(rand)).IsLab == lab)
                    return rand;
            }
            return -1;
        }
        private void FixClustering()
        {
            int roomCount = Configuration.GetInstance().Rooms.Count;
            foreach (var c1 in _classes.ToList())
            {
                foreach (var c2 in _classes.ToList())
                {
                    if (c2.Key == c1.Key) continue;
                    if (c2.Key.ClassCourse == c1.Key.ClassCourse && c2.Key.ClassProfessor == c1.Key.ClassProfessor)
                    {
                        int day1 = GetClassDay(c1.Value);
                        int room1 = GetClassRoom(c1.Value);
                        int time1 = GetClassTime(c1.Value);
                        int pos1 = c2.Value;
                        if (time1 + 1 < Constants.HOURS_PER_DAY)
                        {
                            int dest = day1 * roomCount * Constants.HOURS_PER_DAY + room1 * Constants.HOURS_PER_DAY + (time1 + 1) % Constants.HOURS_PER_DAY;
                            int source = pos1;
                            if (!IsRoomFree(day1, time1 + 1, room1))
                            {
                                CourseClass old = _timeslots[dest].Last();
                                if (old.ClassCourse == c2.Key.ClassCourse) continue;
                                MoveSingleClass(dest, source, old);
                            }
                            MoveSingleClass(source, dest, c2.Key);
                        }
                    }
                }

            }
        }
        private void MoveSingleClass(int source, int dest, CourseClass moving)
        {
            _timeslots[source].Remove(moving);
            _timeslots[dest].Add(moving);
            _classes[moving] = dest;
        }
        private void FixRoomOccupancy()
        {
            int roomCount = Configuration.GetInstance().Rooms.Count;
            foreach (var cl in _classes.ToList())
            {
                int room = GetClassRoom(cl.Value);
                int day = GetClassDay(cl.Value);
                int time = GetClassTime(cl.Value);
                Room r = Configuration.GetInstance().GetRoomByID(room);
                if (GetOccupiedCount(day, time, room) > 1)
                {
                    int newroom = room;
                    while (!IsRoomFree(day, time, newroom))
                    {
                        if (cl.Key.RequiresLab)
                            newroom = Configuration.GetInstance().GetLabRoom().ID;
                        else
                            newroom = Configuration.GetInstance().GetNonLabRoom().ID;
                    }
                    int duration = cl.Key.Length;
                    int pos1 = cl.Value;
                    int pos2 = day * roomCount * Constants.HOURS_PER_DAY + newroom * Constants.HOURS_PER_DAY + time % Constants.HOURS_PER_DAY;
                    for (int j = duration - 1; j >= 0; j--)
                    {
                        List<CourseClass> c = _timeslots[pos1 + j];
                        c.Remove(cl.Key);
                        _timeslots[pos2 + j].Add(cl.Key);
                    }
                    _classes[cl.Key] = pos2;
                }
            }
        }
        private void FixLabDependencies()
        {
            int roomCount = Configuration.GetInstance().Rooms.Count;
            foreach (var cl in _classes.ToList())
            {
                int room = GetClassRoom(cl.Value);
                int day = GetClassDay(cl.Value);
                int time = GetClassTime(cl.Value);
                Room r = Configuration.GetInstance().GetRoomByID(room);
                int newroom = room;
                while (cl.Key.RequiresLab && !r.IsLab && !IsRoomFree(day, time, newroom))
                {
                    newroom = Configuration.GetInstance().GetLabRoom().ID;
                }
                while (cl.Key.RequiresLab == false && r.IsLab && !IsRoomFree(day, time, newroom))
                {
                    newroom = Configuration.GetInstance().GetNonLabRoom().ID;
                }
                int duration = cl.Key.Length;
                int pos1 = cl.Value;
                int pos2 = day * roomCount * Constants.HOURS_PER_DAY + newroom * Constants.HOURS_PER_DAY + time % Constants.HOURS_PER_DAY;
                for (int j = duration - 1; j >= 0; j--)
                {
                    List<CourseClass> c = _timeslots[pos1 + j];
                    c.Remove(cl.Key);
                    _timeslots[pos2 + j].Add(cl.Key);
                }
                _classes[cl.Key] = pos2;
            }
        }
        public void Mutation() 
        {
            if (_random.Next(100) > -1)
                return;
            
            int numberOfClasses = _classes.Count;
            int size = _timeslots.Length;
            for (int i = _mutationSize; i > 0; i--)
            {
                int mpos = _random.Next(numberOfClasses);
                int pos1 = 0;
                var c = _classes.ElementAt(mpos);
                pos1 = c.Value;
                CourseClass cc1 = c.Key;
                
                int roomCount = Configuration.GetInstance().Rooms.Count;
                int duration = cc1.Length;
                int day = _random.Next(Constants.DAYS);
                int room = _random.Next(roomCount);

                int time = _random.Next(Constants.HOURS_PER_DAY + 1 - duration);
                
                int pos2 = day * roomCount * Constants.HOURS_PER_DAY + room * Constants.HOURS_PER_DAY + time;

                MoveSingleClass(pos1, pos2, cc1);
            }
            //CalculateFitness();
        }
        public void CalculateFitness()
        {
            int score = 0;
            float secondaryscore = 0;
            int numberOfRooms = Configuration.GetInstance().Rooms.Count;
            int daySize = numberOfRooms * Constants.HOURS_PER_DAY;

            int ci = 0;
            CourseClass last=null;
            float step = 0.25f/_classes.Count;
            int counter = 0;
            foreach (var item in _timeslots)
            {
                if(item == null && last == null)
                    secondaryscore += step;
                if (last != null)
                {
                    if (item.Count > 0)
                    {
                        if (item.Any(x=>x.ClassCourse==last.ClassCourse))
                        {
                            if (GetClassRoom(counter) == GetClassRoom(counter - 1))
                            {
                                secondaryscore += step;
                            }
                            secondaryscore += step;
                        }
                        
                    }
                }
                if (item.Count > 0) last = item.Last();
                else last = null;
                if (counter % numberOfRooms == 0) last = null;
                counter++;
            }
            foreach (var item in _classes)
            {
                int p = item.Value;
                int day = p / daySize;
                int time = p % daySize;
                int room = time / Constants.HOURS_PER_DAY;
                time = time % Constants.HOURS_PER_DAY;
                int duration = item.Key.Length;
                //check for room overlap
                bool roomOverlap = DoesRoomOverlap(p, duration);
                if (!roomOverlap) score++;
                _criteria[ci + 0] = !roomOverlap;
                //check for necessary room capacity
                CourseClass cc = item.Key;
                Room r = Configuration.GetInstance().GetRoomByID(room);
                _criteria[ci + 1] = r.Capacity >= cc.StudentCount;
                if (_criteria[ci + 1]) score++;
                //check for computers
                _criteria[ci + 2] = cc.RequiresLab ? r.IsLab : !r.IsLab;
                if (_criteria[ci + 2]) score++;
                //check for overlapping of classes for professors and student groups
                bool po = false, go = false;
                for (int i = numberOfRooms, t = day * daySize + time; i > 0; i--, t += Constants.HOURS_PER_DAY)
                {
                    for (int j = duration - 1; j >= 0; j--)
                    {
                            List<CourseClass> cl = _timeslots[t + j];
                            foreach (var c in cl)
                            {
                                if (cc != c)
                                {
                                    if (!po && cc.ProfessorOverlaps(c)) po = true;
                                    if (!go && cc.GroupsOverlap(c)) go = true;
                                    if (po && go) goto total_overlap;
                                }
                            }
                    }
                }
total_overlap:
                if (!po) score++;
                _criteria[ci + 3] = !po;
                if (!go) score++;
                _criteria[ci + 4] = !go;
                
            }
            //foreach (var c in _classes.ToList())
            //{
            //    foreach (var c2 in _classes.ToList())
            //    {
            //        if (c.Value == c2.Value) continue;
            //        if (c.Key.StudentGroups[0] == c2.Key.StudentGroups[0])
            //        {
            //            if ((GetClassTime(c.Value) == GetClassTime(c2.Value)) && (GetClassDay(c.Value) == GetClassDay(c2.Value)))
            //            {
            //                score--;
            //            }
            //        }

            //    }
            //}
            _fitness = (float)(score*100) /  (Configuration.GetInstance().CourseClasses.Count * Constants.DAYS) + secondaryscore;
        }
        private bool DoesRoomOverlap(int p, int duration)
        {
            bool roomOverlap = false;
            for (int i = duration - 1; i >= 0; i--)
            {
                if (_timeslots[p + i].Count > 1)
                {
                    roomOverlap = true;
                    break;
                }
            }
            return roomOverlap;
        }
        private int GetClassRoom(int position)
        {
            int time = position % _daySize;
            int room = time / Constants.HOURS_PER_DAY;
            return room;
        }
        private int GetClassDay(int position)
        {
            int day = position / _daySize;
            return day;
        }
        private int GetClassTime(int position)
        {
            int time = position % _daySize;
            time = time % Constants.HOURS_PER_DAY;
            return time;
        }
        private bool IsRoomFree(int day, int time, int room)
        {
            if(GetOccupiedCount(day,time,room) == 0) return true;
            return false;
        }
        private int GetOccupiedCount(int day, int time, int room)
        {
            int pos = day * Configuration.GetInstance().Rooms.Count * Constants.HOURS_PER_DAY + room * Constants.HOURS_PER_DAY + time;
            return _timeslots[pos].Count;
        }
		
    }
}
