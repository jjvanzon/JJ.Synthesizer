using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Logging.LogWishes;
using static JJ.Framework.Wishes.Text.StringWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;
using static JJ.Business.Synthesizer.Wishes.Config.TimeOutActionEnum;

// ReSharper disable ArrangeStaticMemberQualifier

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeRunner
    {
        private readonly SynthWishes _synthWishes;
        private readonly TapeCollection _tapes;
        /// <inheritdoc cref="docs._tapepadder" />
        private readonly TapePadder _tapePadder;
        private readonly TapeTreeBuilder _tapeTreeBuilder;
        private readonly StereoTapeMatcher _stereoTapeMatcher;
        private readonly StereoTapeRecombiner _stereoTapeRecombiner;
        private readonly VersatileActionRunner _versatileActionRunner;

        public TapeRunner(SynthWishes synthWishes, TapeCollection tapes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _tapes = tapes ?? throw new ArgumentNullException(nameof(tapes));
            _tapePadder = new TapePadder(synthWishes, tapes);
            _tapeTreeBuilder = new TapeTreeBuilder(synthWishes, tapes);
            _stereoTapeMatcher = new StereoTapeMatcher();
            _stereoTapeRecombiner = new StereoTapeRecombiner(synthWishes);
            _versatileActionRunner = new VersatileActionRunner();
        }
        
        public void RunAllTapes()
        {
            if (_tapes.Count == 0) return;
            ExecutePreProcessing();
            Tape[] tapes = RunTapeLeavesConcurrent();
            ExecutePostProcessing(tapes);
        }
        
        private void ExecutePreProcessing()
        {
            var tapes = _tapes.ToArray();
            
            _tapePadder.PadTapesIfNeeded(tapes);
            
            tapes = _tapes.ToArray();
            
            _tapeTreeBuilder.BuildTapeHierarchyRecursive(tapes);

            tapes.Where(x => x.IsRoot).ForEach(x => Assert(x, "(Root)"));

            _synthWishes.Log(Static.GetTapeTree(tapes));
        }
        
        private readonly AutoResetEvent _checkForNewLeavesReset = new AutoResetEvent(false);
        
        private Tape[] RunTapeLeavesConcurrent()
        {
            // Prep settings
            int timeOutInMs = (int)(_synthWishes.GetLeafCheckTimeOut * 1000);
            if (timeOutInMs < 0) timeOutInMs = -1;
            TimeOutActionEnum timeOutAction = _synthWishes.GetTimeOutAction;
            
            // Prep collections
            Tape[] originalTapeCollection = _tapes.ToArray();
            _tapes.Clear();
            Tape[] tapesTODO = originalTapeCollection.ToArray();
            int count = tapesTODO.Length;
            Task[] tasks = new Task[count];
            
            // Loop!
            int waitCount = 0;
            int todoCount = count;
            while (todoCount > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Tape tape = tapesTODO[i];
                    
                    if (tape == null) continue;
                    if (tape.ChildTapes.Count != 0) continue;
                    tapesTODO[i] = null;
                    
                    tasks[i] = Task.Run(() => RunTape(tape));
                    
                    todoCount--;
                }
                
                if (todoCount > 0)
                {
                    waitCount++;

                    _synthWishes.LogAction(nameof(Tape), "No Leaf", "Wait ... " + waitCount);
                    bool triggered = _checkForNewLeavesReset.WaitOne(timeOutInMs);
                    if (!triggered)
                    {
                        HandleTimeOut(timeOutAction, timeOutInMs, todoCount, tapesTODO);
                    }
                }
            } 
            
            Task.WaitAll(tasks);

            _synthWishes.LogAction(nameof(Tape), "Total Leaf Waits: " + waitCount);
            return originalTapeCollection;
        }
        
        private readonly object _hierarchyLock = new object();
        
        internal void RunTape(Tape tape)
        {
            try
            {
                tape.LogAction("Start", "Running ...");
                
                _versatileActionRunner.RunBeforeRecord(tape);
                
                _synthWishes.Record(tape);
                
                _versatileActionRunner.RunAfterRecord(tape);
                
                // Replace All References
                //lock (_hierarchyLock)
                {
                    IList<Inlet> connectedInlets = tape.Outlet.ConnectedInlets.ToArray();
                    foreach (Inlet inlet in connectedInlets)
                    {
                        inlet.LinkTo(tape.Sample);
                    }
                }
                
                tape.LogAction("Stop", "Checking Leaves ...");
            }
            finally
            {
                // Don’t let a thread crash cause the while loop for child tapes to retry infinitely.
                // Exceptions will propagate after the while loop (where we wait for all tasks to finish),
                // so let’s make sure the while loop can finish properly.
                
                lock (_hierarchyLock)
                {
                    tape.ClearHierarchy();
                }
                
                _checkForNewLeavesReset.Set();
            }
        }
        
        private void HandleTimeOut(TimeOutActionEnum timeOutAction, int timeOutInMs, int todoCount, Tape[] tapesTODO)
        {
            double timeOutInSec = timeOutInMs / 1000.0;
            string formattedTimeOut = PrettyDuration(timeOutInSec);
            
            string actionMessage = Static.ActionMessage(
                nameof(Tape),
                "Check for Leaves",
                "", $"Time-out after {formattedTimeOut} waiting for a leaf to finish.");
            
            switch (timeOutAction)
            {
                case Continue:
                    _synthWishes.Log(actionMessage);
                    break;
                    
                case Log:
                    _synthWishes.Log(actionMessage + " " + Static.TapesLeftMessage(todoCount, tapesTODO));
                    break;
                
                case Stop:
                    throw new Exception(actionMessage + " " + Static.TapesLeftMessage(todoCount, tapesTODO));
            }
        }
        
        private void ExecutePostProcessing(Tape[] tapes)
        {
            _synthWishes.LogTitle("Post-Processing");
            
            IList<Tape> relevantStereoChannelTapes
                = tapes.Where(x => x.Config.IsStereo && x.Config.Channel.HasValue)
                       .Where(x => x.Actions.Play.On || x.Actions.Save.On ||
                                  (x.Actions.BeforeRecord.On && x.Actions.BeforeRecord.Callback != null) ||
                                  (x.Actions.AfterRecord.On  && x.Actions.AfterRecord.Callback != null))
                       .ToArray();

            IList<Tape> stereoTapes = Array.Empty<Tape>();
            if (relevantStereoChannelTapes.Count > 0)
            {
                IList<(Tape Left, Tape Right)> tapePairs = _stereoTapeMatcher.PairTapes(relevantStereoChannelTapes);
                stereoTapes = _stereoTapeRecombiner.RecombineChannelsConcurrent(tapePairs);
            }

            _versatileActionRunner.RunForPostProcessing(tapes, stereoTapes);
        }
    }
}