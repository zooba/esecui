﻿using System;
using System.Collections.Generic;
using System.Threading;
using IronPython.Runtime;

namespace esecui
{
    [PythonType]
    public class Monitor : IDisposable
    {
        private Editor Owner;

        private AutoResetEvent PausedChanged;
        private bool _IsPaused;
        private bool _IsSingleStep;
        public bool IsPaused
        {
            get { return _IsPaused; }
            set { _IsPaused = value; PausedChanged.Set(); }
        }
        public void SingleStep()
        {
            _IsSingleStep = true;
            IsPaused = false;
        }

        public bool IsCancelled { get; private set; }
        public void Cancel()
        {
            IsCancelled = true;
            IsPaused = false;
        }

        public int Iterations { get; private set; }
        public int Evaluations { get; private set; }
        public int Births { get; private set; }
        public DateTime StartTime { get; private set; }

        public int? IterationLimit { get; set; }
        public int? EvaluationLimit { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public double? FitnessLimit { get; set; }

        private dynamic BestSolution;
        private dynamic CurrentBest;
        private dynamic CurrentMean;
        private dynamic CurrentWorst;

        public Monitor(Editor owner)
        {
            Owner = owner;

            PausedChanged = new AutoResetEvent(false);
            IsPaused = false;

            IsCancelled = false;

            MainGroup = null;
            Iterations = 0;
            Evaluations = 0;
            Births = 0;
            StartTime = DateTime.MinValue;

            IterationLimit = null;
            EvaluationLimit = null;
            TimeLimit = null;
            FitnessLimit = null;

            BestSolution = null;
            CurrentBest = null;
            CurrentMean = null;
            CurrentWorst = null;
        }

        private void LogIndividual(dynamic indiv)
        {
            Owner.Log("#{0} - {1}, {2}", Iterations, indiv.birthday, indiv.fitness.__str__());
        }

        private string MainGroup;
        private List<dynamic> CachedMainGroup;

        public void on_yield(dynamic sender, dynamic name, dynamic group_obj)
        {
            if (MainGroup == null) MainGroup = (string)name;

            if (MainGroup == name)
            {
                CachedMainGroup = new List<dynamic>((IList<dynamic>)group_obj);
                Owner.UpdateVisualisation(CachedMainGroup, Iterations <= 1);
            }
        }

        private void CalculateStats()
        {
            var group = CachedMainGroup;
            dynamic best = null, worst = null, sum = null;

            foreach (var indiv in group)
            {
                if (best == null || indiv.fitness > best.fitness) best = indiv;
                if (worst == null || indiv.fitness < worst.fitness) worst = indiv;
                if (sum == null) sum = indiv.fitness; else sum = sum + indiv.fitness;
            }

            if (BestSolution == null || best.fitness > BestSolution.fitness)
            {
                BestSolution = best;
            }
            CurrentBest = best;
            CurrentMean = sum.__div__(group.Count);
            CurrentWorst = worst;
        }

        public void on_notify(dynamic sender, dynamic name, dynamic value)
        {
            if (name == "statistic")
            {
                if (value == "births") Births += 1;
                else if (value == "local_evals+global_evals") Evaluations += 1;
                else Owner.Log("Statistic: " + value.ToString());
            }
            else if (name == "System" || name == "Landscape" || name == "Configuration" || name == "Block")
            { }
            else
            {
                Owner.Log("Notification from {0}\n{0}: {1}", sender, name, value);
            }
        }

        public void _on_notify(dynamic sender, dynamic name, dynamic value)
        {
            on_notify(sender, name, value);
        }

        public void notify(dynamic sender, dynamic name, dynamic value)
        {

        }

        public void on_pre_reset(dynamic sender)
        {
            IsCancelled = false;

            Iterations = 0;
            Evaluations = 0;
            Births = 0;
            StartTime = DateTime.Now;

            BestSolution = null;
            CurrentBest = null;
            CurrentMean = null;
            CurrentWorst = null;
        }

        public void on_post_reset(dynamic sender)
        {
            on_post_breed(sender);
        }

        public void on_pre_breed(dynamic sender)
        {
            Iterations += 1;
        }

        public void on_post_breed(dynamic sender)
        {
            CalculateStats();

            Owner.UpdateStats(Iterations, Evaluations, Births, DateTime.Now.Subtract(StartTime),
                BestSolution,
                BestSolution == null ? null : BestSolution.fitness,
                CurrentBest == null ? null : CurrentBest.fitness,
                CurrentMean == null ? null : CurrentMean,
                CurrentWorst == null ? null : CurrentWorst.fitness);

            while (IsPaused && !IsCancelled)
            {
                PausedChanged.WaitOne();
            }
            if (_IsSingleStep) IsPaused = true;
            _IsSingleStep = false;
        }

        public void on_run_start(dynamic sender)
        {

        }

        public void on_run_end(dynamic sender)
        {
            Owner.UpdateStats(Iterations, Evaluations, Births, DateTime.Now.Subtract(StartTime),
                BestSolution,
                BestSolution == null ? null : BestSolution.fitness,
                CurrentBest == null ? null : CurrentBest.fitness,
                CurrentMean == null ? null : CurrentMean,
                CurrentWorst == null ? null : CurrentWorst.fitness);
        }

        public void on_exception(dynamic sender, dynamic exception_type, dynamic value, dynamic trace)
        {
            Owner.Log("Exception: " + sender.ToString() + " - " + exception_type.ToString() + "\n" + trace.ToString());
        }

        public bool should_terminate(dynamic sender)
        {
            if (IsCancelled) return true;
            if (IterationLimit.HasValue && IterationLimit <= Iterations) return true;
            if (EvaluationLimit.HasValue && EvaluationLimit <= Evaluations) return true;
            if (TimeLimit.HasValue && TimeLimit <= DateTime.Now.Subtract(StartTime)) return true;
            if (FitnessLimit.HasValue && BestSolution.fitness.should_terminate(FitnessLimit.Value)) return true;

            return !(IterationLimit.HasValue ||
                EvaluationLimit.HasValue ||
                TimeLimit.HasValue ||
                FitnessLimit.HasValue);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (PausedChanged != null) PausedChanged.Close();
                PausedChanged = null;
            }
        }
    }
}
