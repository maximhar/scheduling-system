using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScheduleShared;
namespace ScheduleConcept
{
    public enum AlgorithmState
    {
        USER_STOPPED,
        CRITERIA_STOPPED,
        RUNNING
    }
    class ChromosomeEventArgs : EventArgs
    {
        private Schedule _chromosome;
        public Schedule Chromosome { get { return _chromosome; } }
        public ChromosomeEventArgs(Schedule s)
            : base()
        {
            _chromosome = s;
        }
    }
    class Algorithm
    {
        Random _random = new Random(DateTime.Now.Millisecond);
        public event ChromosomeEventHandler NewBestChromosome;
        public event EventHandler StateChanged;
        public event EventHandler EvolutionStateChanged;
        void OnNewBestChromosome(Schedule s)
        {
            NewBestChromosome(this, new ChromosomeEventArgs(s));
        }
        void OnStateChanged(EventArgs args)
        {
            StateChanged(this, args);
        }
        void OnEvolutionStateChanged(EventArgs args)
        {
            EvolutionStateChanged(this, args);
        }
        List<Schedule> _chromosomes;

        bool[] _bestFlags;
        int[] _bestChromosomes;
        int _currentBestSize;
        int _replaceByGeneration;
        int _numberOfChromosomes;
        Schedule _prototype;
        AlgorithmState _state;
        public AlgorithmState State
        {
            get { return _state; }
            set
            {
                _state = value; OnStateChanged(EventArgs.Empty);
            }
        }
        int _currentGeneration;
        static Algorithm _instance;
        public static Algorithm GetInstance() 
        {
            if (_instance == null)
            {
                Schedule prototype = new Schedule(2, 2, 40, 2);
                
                _instance = new Algorithm(100, 5, 5, prototype);
            }
            return _instance;
        }
        public void Start()
        {
            if (_prototype==null) return;
            State = AlgorithmState.RUNNING;
            ClearBest();
            for (int i = 0; i < _numberOfChromosomes; i++)
            {
                _chromosomes.Add(_prototype.CreateNewFromPrototype());
                AddToBest(i);
            }
            _currentGeneration = 0;

            while (true)
            {
                if (_state != AlgorithmState.RUNNING)
                {
                    break;
                }
                Schedule best = GetBestChromosome();
                if (best.Fitness >= 120)
                {
                    State = AlgorithmState.CRITERIA_STOPPED;
                    break;
                }
                
                Schedule[] offspring = new Schedule[_replaceByGeneration];
                for (int j = 0; j < _replaceByGeneration; j++)
                {
                    Schedule p1 = _chromosomes[_random.Next(_chromosomes.Count)];
                    Schedule p2 = _chromosomes[_random.Next(_chromosomes.Count)];
                    offspring[j] = p1.CrossOver(p2);
                    offspring[j].Mutation();
                    offspring[j].Repair();
                }
                for (int j = 0; j < _replaceByGeneration; j++)
                {
                    int ci=0;
                    do
                    {
                        ci = _random.Next(_chromosomes.Count);
                    }
                    while (IsInBest(ci));
                    _chromosomes[ci] = offspring[j];
                    AddToBest(ci);
                }
                var newbest = GetBestChromosome();

                if (best != newbest)
                {
                    OnNewBestChromosome(newbest);
                }
                _currentGeneration++;
                
                OnEvolutionStateChanged(EventArgs.Empty);
            }
        }
        public void Stop() 
        {
            if (_state == AlgorithmState.RUNNING)
                State = AlgorithmState.USER_STOPPED;
        }
        public int CurrentGeneration
        {
            get
            {
                return _currentGeneration;
            }
        }
        public Algorithm(int numberOfChromosomes, int replaceByGeneration, int trackBest, Schedule prototype) 
        {
            _replaceByGeneration = replaceByGeneration; 
            _prototype = prototype;
            
            if (numberOfChromosomes < 2)
                numberOfChromosomes = 2;
            if (trackBest < 1)
                trackBest = 1;
            if (_replaceByGeneration < 1)
                _replaceByGeneration = 1;
            else if (_replaceByGeneration > numberOfChromosomes - trackBest)
                _replaceByGeneration = numberOfChromosomes - trackBest;
            _numberOfChromosomes = numberOfChromosomes;
            _chromosomes = new List<Schedule>(numberOfChromosomes);
            _bestFlags = new bool[numberOfChromosomes];
            _bestChromosomes = new int[trackBest];
        }
        public Schedule GetBestChromosome()
        {
            return _chromosomes[_bestChromosomes[0]];
        }
        void AddToBest(int chromosomeIndex)
        {
            if ((_currentBestSize == _bestChromosomes.Length && _chromosomes[_bestChromosomes[_currentBestSize - 1]].Fitness >=
                _chromosomes[chromosomeIndex].Fitness) || _bestFlags[chromosomeIndex])
                return;
            int i = _currentBestSize;
            for (; i > 0; i--)
            {
                if (i < _bestChromosomes.Length)
                {
                    if (_chromosomes[_bestChromosomes[i - 1]].Fitness > _chromosomes[chromosomeIndex].Fitness) break;
                    _bestChromosomes[i] = _bestChromosomes[i - 1];
                }
                else
                {
                    _bestFlags[_bestChromosomes[i - 1]] = false;
                }
            }
            _bestChromosomes[i] = chromosomeIndex;
            _bestFlags[chromosomeIndex] = true;
            if (_currentBestSize < _bestChromosomes.Length) _currentBestSize++;
        }
        bool IsInBest(int chromosomeIndex)
        {
            return _bestFlags[chromosomeIndex];
        }
        void ClearBest()
        {
            for (int i = _bestFlags.Length - 1; i >= 0; --i)
                _bestFlags[i] = false;

            _currentBestSize = 0;
        }
    }
}
