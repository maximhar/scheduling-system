using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScheduleCommon;
using System.Diagnostics;

namespace Tests
{
    class Tester
    {
        Configuration config = Configuration.Instance;
        static void Main(string[] args)
        {
            Tester p = new Tester();
            p.DoTests();
            Console.ReadLine();
        }
        private void DoTests()
        {
            TestRoomTimeOverlap();
            TestRoomTimeOverlap2();
            TestRoomTimeOverlap3();

            TestProfessorTimeOverlap();
            TestProfessorTimeOverlap2();
        }

        private void TestRoomTimeOverlap()
        {
            config.Clear();

            Stopwatch s = new Stopwatch();

            var mitov = new Professor("Kiril Mitov");
            var abrama = new Professor("Janet Abramowitch");

            var r12 = new Room("12", CourseType.NormalCourse);
            var r24 = new Room("24", CourseType.NormalCourse);
            var r32 = new Room("32", CourseType.ComputerCourse);

            var g11a = new StudentGroup("11A");
            var g11g = new StudentGroup("11G");

            var tp = new Course("TP", mitov, CourseType.ComputerCourse);
            var maths = new Course("Maths", abrama, CourseType.NormalCourse);

            var tp11a = new Class(g11a, tp, new TimeSpan(1, 20, 0), r32);
            var math11g = new Class(g11g, maths, new TimeSpan(2, 0, 0), r32);

            config.Rooms.Add(r12);
            config.Rooms.Add(r24);
            config.Rooms.Add(r32);

            config.Groups.Add(g11a);
            config.Groups.Add(g11g);

            
            config.Professors.Add(mitov);
            config.Professors.Add(abrama);

            config.Courses.Add(tp);
            config.Courses.Add(maths);

            Schedule sched = new Schedule();
            for (int i = 0; i < 7; i++)
            {
                sched.SetStartTime(i, g11a, new TimeSpan(8, 0, 0));
                sched.SetStartTime(i, g11g, new TimeSpan(8, 0, 0));
            }
            sched[0][g11a] = new List<Class>();
            sched[0][g11g] = new List<Class>();

            sched[0][g11a].Add(tp11a);//11A class has TP from 8AM to 9:20AM in room 32
            sched[0][g11g].Add(math11g);//11G class has Maths from 8AM to 10AM in room 32
            //result should be false as rooms conflict
            s.Start();
            IConstraint c = new RoomTimeOverlapConstraint();
            var result = c.Check(sched);
            s.Stop();

            string pass = result.ConstraintFulfilled == false ? "succeeded" : "failed";
            Console.WriteLine("TestRoomTimeOverlap() " + pass);
            Console.WriteLine(result.ErrorMessage);
            Console.WriteLine("{0} ms.", s.ElapsedMilliseconds);
        }
        private void TestRoomTimeOverlap2()
        {
            config.Clear();

            Stopwatch s = new Stopwatch();

            var mitov = new Professor("Kiril Mitov");
            var abrama = new Professor("Janet Abramowitch");

            var r12 = new Room("12", CourseType.NormalCourse);
            var r24 = new Room("24", CourseType.NormalCourse);
            var r32 = new Room("32", CourseType.ComputerCourse);

            var g11a = new StudentGroup("11A");
            var g11g = new StudentGroup("11G");

            var tp = new Course("TP", mitov, CourseType.ComputerCourse);
            var maths = new Course("Maths", abrama, CourseType.NormalCourse);

            var tp11a = new Class(g11a, tp, new TimeSpan(1, 20, 0), r32);
            var math11g = new Class(g11g, maths, new TimeSpan(2, 0, 0), r24);

            config.Rooms.Add(r12);
            config.Rooms.Add(r24);
            config.Rooms.Add(r32);

            config.Groups.Add(g11a);
            config.Groups.Add(g11g);


            config.Professors.Add(mitov);
            config.Professors.Add(abrama);

            config.Courses.Add(tp);
            config.Courses.Add(maths);

            Schedule sched = new Schedule();
            for (int i = 0; i < 7; i++)
            {
                sched.SetStartTime(i, g11a, new TimeSpan(8, 0, 0));
                sched.SetStartTime(i, g11g, new TimeSpan(8, 0, 0));
            }
            sched[0][g11a] = new List<Class>();
            sched[0][g11g] = new List<Class>();

            sched[0][g11a].Add(tp11a);//11A class has TP from 8AM to 9:20AM in room 32
            sched[0][g11g].Add(math11g);//11G class has Maths from 8AM to 10AM in room 24
            //result should be true, rooms do not conflict
            s.Start();
            IConstraint c = new RoomTimeOverlapConstraint();
            var result = c.Check(sched);
            s.Stop();

            string pass = result.ConstraintFulfilled == true ? "succeeded" : "failed";
            Console.WriteLine("TestRoomTimeOverlap2() " + pass);
            Console.WriteLine(result.ErrorMessage);
            Console.WriteLine("{0} ms.", s.ElapsedMilliseconds);
        }
        private void TestRoomTimeOverlap3()
        {
            config.Clear();

            Stopwatch s = new Stopwatch();

            var mitov = new Professor("Kiril Mitov");
            var abrama = new Professor("Janet Abramowitch");

            var r12 = new Room("12", CourseType.NormalCourse);
            var r24 = new Room("24", CourseType.NormalCourse);
            var r32 = new Room("32", CourseType.ComputerCourse);

            var g11a = new StudentGroup("11A");
            var g11g = new StudentGroup("11G");

            var tp = new Course("TP", mitov, CourseType.ComputerCourse);
            var maths = new Course("Maths", abrama, CourseType.NormalCourse);

            var tp11a = new Class(g11a, tp, new TimeSpan(1, 20, 0), r32);
            var math11g = new Class(g11g, maths, new TimeSpan(2, 0, 0), r32);

            config.Rooms.Add(r12);
            config.Rooms.Add(r24);
            config.Rooms.Add(r32);

            config.Groups.Add(g11a);
            config.Groups.Add(g11g);


            config.Professors.Add(mitov);
            config.Professors.Add(abrama);

            config.Courses.Add(tp);
            config.Courses.Add(maths);

            Schedule sched = new Schedule();
            for (int i = 0; i < 7; i++)
            {
                sched.SetStartTime(i, g11a, new TimeSpan(8, 0, 0));
                sched.SetStartTime(i, g11g, new TimeSpan(10, 0, 0));
            }
            sched[0][g11a] = new List<Class>();
            sched[0][g11g] = new List<Class>();

            sched[0][g11a].Add(tp11a);//11A class has TP from 8AM to 9:20AM in room 32
            sched[0][g11g].Add(math11g);//11G class has Maths from 10AM to 12AM in room 32
            //result should be true, rooms do not conflict
            s.Start();
            IConstraint c = new RoomTimeOverlapConstraint();
            var result = c.Check(sched);
            s.Stop();

            string pass = result.ConstraintFulfilled == true ? "succeeded" : "failed";
            Console.WriteLine("TestRoomTimeOverlap3() " + pass);
            Console.WriteLine(result.ErrorMessage);
            Console.WriteLine("{0} ms.", s.ElapsedMilliseconds);
        }

        private void TestProfessorTimeOverlap()
        {
            config.Clear();

            Stopwatch s = new Stopwatch();

            var mitov = new Professor("Kiril Mitov");
            var abrama = new Professor("Janet Abramowitch");

            var r12 = new Room("12", CourseType.NormalCourse);
            var r24 = new Room("24", CourseType.NormalCourse);
            var r32 = new Room("32", CourseType.ComputerCourse);

            var g11a = new StudentGroup("11A");
            var g11g = new StudentGroup("11G");

            var tp = new Course("TP", mitov, CourseType.ComputerCourse);
            var maths = new Course("Maths", abrama, CourseType.NormalCourse);

            var tp11a = new Class(g11a, tp, new TimeSpan(1, 20, 0), r32);
            var tp11g = new Class(g11g, tp, new TimeSpan(1, 20, 0), r24);

            config.Rooms.Add(r12);
            config.Rooms.Add(r24);
            config.Rooms.Add(r32);

            config.Groups.Add(g11a);
            config.Groups.Add(g11g);


            config.Professors.Add(mitov);
            config.Professors.Add(abrama);

            config.Courses.Add(tp);
            config.Courses.Add(maths);

            Schedule sched = new Schedule();
            for (int i = 0; i < 7; i++)
            {
                sched.SetStartTime(i, g11a, new TimeSpan(8, 0, 0));
                sched.SetStartTime(i, g11g, new TimeSpan(8, 0, 0));
            }
            sched[0][g11a] = new List<Class>();
            sched[0][g11g] = new List<Class>();

            sched[0][g11a].Add(tp11a);//11A class has TP from 8AM to 9:20AM in room 32
            sched[0][g11g].Add(tp11g);//11G class has TP from 8AM to 9:20AM in room 24
            //result should be false as professors conflict
            s.Start();
            IConstraint c = new ProfessorTimeOverlapConstraint();
            var result = c.Check(sched);
            s.Stop();

            string pass = result.ConstraintFulfilled == false ? "succeeded" : "failed";
            Console.WriteLine("TestProfessorTimeOverlap() " + pass);
            Console.WriteLine(result.ErrorMessage);
            Console.WriteLine("{0} ms.", s.ElapsedMilliseconds);
        }
        private void TestProfessorTimeOverlap2()
        {
            config.Clear();

            Stopwatch s = new Stopwatch();

            var mitov = new Professor("Kiril Mitov");
            var abrama = new Professor("Janet Abramowitch");

            var r12 = new Room("12", CourseType.NormalCourse);
            var r24 = new Room("24", CourseType.NormalCourse);
            var r32 = new Room("32", CourseType.ComputerCourse);

            var g11a = new StudentGroup("11A");
            var g11g = new StudentGroup("11G");

            var tp = new Course("TP", mitov, CourseType.ComputerCourse);
            var maths = new Course("Maths", abrama, CourseType.NormalCourse);

            var tp11a = new Class(g11a, tp, new TimeSpan(1, 20, 0), r32);
            var maths11g = new Class(g11g, maths, new TimeSpan(1, 20, 0), r24);
            var tp11g = new Class(g11g, tp, new TimeSpan(1, 20, 0), r24);

            config.Rooms.Add(r12);
            config.Rooms.Add(r24);
            config.Rooms.Add(r32);

            config.Groups.Add(g11a);
            config.Groups.Add(g11g);


            config.Professors.Add(mitov);
            config.Professors.Add(abrama);

            config.Courses.Add(tp);
            config.Courses.Add(maths);

            Schedule sched = new Schedule();
            for (int i = 0; i < 7; i++)
            {
                sched.SetStartTime(i, g11a, new TimeSpan(8, 0, 0));
                sched.SetStartTime(i, g11g, new TimeSpan(8, 0, 0));
            }
            sched[0][g11a] = new List<Class>();
            sched[0][g11g] = new List<Class>();

            sched[0][g11a].Add(tp11a);//11A class has TP from 8AM to 9:20AM in room 32
            sched[0][g11g].Add(maths11g);//11G class has Maths from 8AM to 9:20AM in room 24
            sched[0][g11g].Add(tp11g);//11G class has TP from 9:20AM to 10:40AM in room 24
            //result should be true as professors do not conflict
            s.Start();
            IConstraint c = new ProfessorTimeOverlapConstraint();
            var result = c.Check(sched);
            s.Stop();

            string pass = result.ConstraintFulfilled == true ? "succeeded" : "failed";
            Console.WriteLine("TestProfessorTimeOverlap2() " + pass);
            Console.WriteLine(result.ErrorMessage);
            Console.WriteLine("{0} ms.", s.ElapsedMilliseconds);
        }
    }
}
