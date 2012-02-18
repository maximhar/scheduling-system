using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ScheduleConcept
{
    delegate void ChromosomeEventHandler(object sender, ChromosomeEventArgs args);
    class Program
    {
        static Schedule LastBest;
        
        static void Main(string[] args)
        {
            Configuration conf = Configuration.GetInstance();
            string path = "info.xml";
            Console.WriteLine("Reading from: {0}", path);
            conf.ReadFromXML(path);
            Algorithm a = Algorithm.GetInstance();
            a.EvolutionStateChanged += new EventHandler(a_EvolutionStateChanged);
            a.StateChanged += new EventHandler(a_StateChanged);
            a.NewBestChromosome += new ChromosomeEventHandler(a_NewBestChromosome);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            a.Start();
            sw.Stop();
            Console.WriteLine("It took: {0}ms.", sw.ElapsedMilliseconds);
            Console.ReadLine();
        }

        static void a_NewBestChromosome(object sender, ChromosomeEventArgs e)
        {
            LastBest = (sender as Algorithm).GetBestChromosome().Clone(false);
            Console.WriteLine("New best chromosome found: fitness {0}", (sender as Algorithm).GetBestChromosome().Fitness);
            DisplaySchedule(e.Chromosome);
            if (e.Chromosome.Fitness > 100.9)
            {
                LastBest = e.Chromosome;
                (sender as Algorithm).Stop();
            }
            //DisplaySchedule((sender as Algorithm).GetBestChromosome());
            //Console.ReadLine();
        }

        static void a_StateChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Algorithm state: {0}", (sender as Algorithm).State);
            if ((sender as Algorithm).State != AlgorithmState.RUNNING)
            {
                Schedule s = LastBest;
                Console.WriteLine("Finished after {0} generations", (sender as Algorithm).CurrentGeneration);
                DisplaySchedule(s);
                s.CalculateFitness();
                Console.WriteLine(s.Fitness);
            }
            
        }
        static void DisplaySchedule(Schedule s)
        {
            int daySize = Constants.HOURS_PER_DAY * Configuration.GetInstance().Rooms.Count;
            var classes = from v in s.Classes
                          let day = v.Value / daySize
                          let time = v.Value % daySize
                          let room = time / Constants.HOURS_PER_DAY
                          let finaltime = time % Constants.HOURS_PER_DAY
                          select new { Day = day, Professor = v.Key.ClassProfessor, Time = finaltime, Course = v.Key.ClassCourse, Duration = v.Key.Length, Room = Configuration.GetInstance().GetRoomByID(room), Group = v.Key.StudentGroups[0] };
            var sortedclasses = from c in classes
                                orderby c.Time ascending
                                orderby c.Day ascending
                                group c by c.Group into a
                                from e in a
                                group e by e.Day;
            Console.WriteLine("       {0, -12} {1, -7} {2, -15} {3, -15} {4,-4} {5, -4}", "Time", "Group", "Course", "Professor", "Room", "Lab");
            foreach (var day in sortedclasses)
            {
                Console.WriteLine("Day {0}:", day.Key);
                foreach(var c in day)
                {
                    string line = string.Format("{0, -12} {1, -7} {2, -15} {3, -15} {4,-4} {5,-4}", TimeSpanToString(GetTime(c.Time)) + "-" + TimeSpanToString(GetTime(c.Time+c.Duration)), c.Group.Name, c.Course.Name, c.Professor.Name, c.Room.Name, c.Room.IsLab ? "Yes":"No");
                    Console.WriteLine("       {0}", line);
                }
            }
        }
        static string TimeSpanToString(TimeSpan t)
        {
            return new DateTime(t.Ticks).ToString("HH:mm");
        }
        static TimeSpan SlotsToSpan(int s)
        {
            return new TimeSpan(0, (int)Constants.MINS_PER_SLOT.TotalMinutes*s,0);
        }
        static TimeSpan GetTime(int s)
        {
            return Constants.DAY_BEGIN + SlotsToSpan(s);
        }
        static void a_EvolutionStateChanged(object sender, EventArgs e)
        {
            if((sender as Algorithm).CurrentGeneration%1000==0)
                Console.WriteLine("Generation: {0}", (sender as Algorithm).CurrentGeneration);
            
        }
    }
}
