using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.TimeOutActionEnum;

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
        private readonly StereoTapeActionRunner _stereoTapeActionRunner;
        private readonly MonoTapeActionRunner _monoTapeActionRunner;
        private readonly ChannelTapeActionRunner _channelTapeActionRunner;

        public TapeRunner(SynthWishes synthWishes, TapeCollection tapes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _tapes = tapes ?? throw new ArgumentNullException(nameof(tapes));
            _tapePadder = new TapePadder(synthWishes, tapes);
            _tapeTreeBuilder = new TapeTreeBuilder(tapes);
            _stereoTapeMatcher = new StereoTapeMatcher();
            _stereoTapeRecombiner = new StereoTapeRecombiner(synthWishes);
            _stereoTapeActionRunner = new StereoTapeActionRunner();
            _monoTapeActionRunner = new MonoTapeActionRunner();
            _channelTapeActionRunner = new ChannelTapeActionRunner();
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
            
            Console.WriteLine(PlotTapeTree(tapes));
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

                    LogAction(nameof(Tape), "No Leaf", "Wait... " + waitCount);
                    bool triggered = _checkForNewLeavesReset.WaitOne(timeOutInMs);
                    if (!triggered)
                    {
                        HandleTimeOut(timeOutAction, timeOutInMs, todoCount, tapesTODO);
                    }
                }
            } 
            
            Task.WaitAll(tasks);

            LogAction(nameof(Tape), "Total Waits for Leaves: " + waitCount);
            return originalTapeCollection;
        }
        
        private readonly object _hierarchyLock = new object();
        
        internal void RunTape(Tape tape)
        {
            try
            {
                LogAction(tape, "Leaf Found", "Running");
                LogAction(tape, "Start");
                
                // Cache Buffer
                _synthWishes.Record(tape);
                
                // Run Actions (that can't wait)
                _channelTapeActionRunner.InterceptIfNeeded(tape);
                
                // Wrap in Sample
                FlowNode sample = _synthWishes.Sample(tape);
                
                // Replace All References
                //lock (_hierarchyLock)
                {
                    IList<Inlet> connectedInlets = tape.Signal.UnderlyingOutlet.ConnectedInlets.ToArray();
                    foreach (Inlet inlet in connectedInlets)
                    {
                        inlet.LinkTo(sample);
                    }
                }
                
                LogAction(tape, "Stop");
                LogAction(tape, "Task Finished", "Check for Leaves");
}
            finally
            {
                // Don’t let a thread crash cause the while loop for child tapes to retry infinitely.
                // Exceptions will propagate after the while loop (where we wait for all tasks to finish),
                // so let’s make sure the while loop can finish properly.
                
                lock (_hierarchyLock)
                {
                    tape.ClearRelationships();
                }
                
                _checkForNewLeavesReset.Set();
            }
        }
        
        private void HandleTimeOut(TimeOutActionEnum timeOutAction, int timeOutInMs, int todoCount, Tape[] tapesTODO)
        {
            double timeOutInSec = timeOutInMs / 1000.0;
            string formattedTimeOut = PrettyDuration(timeOutInSec);
            
            string actionMessage = GetActionMessage(
                nameof(Tape),
                "Check for Leaves",
                $"Time-out after {formattedTimeOut} waiting for a leaf to finish.");
            
            switch (timeOutAction)
            {
                case Continue:
                    Console.WriteLine(actionMessage);
                    break;
                    
                case Log:
                    Console.WriteLine(actionMessage + " " + GetTapesLeftMessage(todoCount, tapesTODO));
                    break;
                
                case Stop:
                    throw new Exception(actionMessage + " " + GetTapesLeftMessage(todoCount, tapesTODO));
            }
        }
        
        
        private void ExecutePostProcessing(Tape[] tapes)
        {
            IList<Tape> stereoTapes = Array.Empty<Tape>();
            if (_synthWishes.IsStereo)
            {
                var tapesWithActions = tapes.Where(x => x.IsPlay ||
                                                        x.IsSave ||
                                                        x.Callback != null).ToArray();
                var tapePairs = _stereoTapeMatcher.PairTapes(tapesWithActions);
                stereoTapes = _stereoTapeRecombiner.RecombineChannelsConcurrent(tapePairs);
            }

            _monoTapeActionRunner.InterceptIfNeeded(tapes);
            _stereoTapeActionRunner.InterceptIfNeeded(stereoTapes);
            
            _channelTapeActionRunner.SaveIfNeeded(tapes);
            _monoTapeActionRunner.SaveIfNeeded(tapes);
            _stereoTapeActionRunner.SaveIfNeeded(stereoTapes);

            _channelTapeActionRunner.PlayIfNeeded(tapes);
            _monoTapeActionRunner.PlayIfNeeded(tapes);
            _stereoTapeActionRunner.PlayIfNeeded(stereoTapes);
        }
    }
}