using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
// ReSharper disable ArrangeStaticMemberQualifier

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeRunner
    {
        private readonly SynthWishes _synthWishes;
        private readonly TapeCollection _tapes;
        /// <inheritdoc cref="docs._tapePadder" />
        private readonly TapePadder _tapePadder;
        private readonly TapeHierarchyBuilder _tapeHierarchyBuilder;
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
            _tapeHierarchyBuilder = new TapeHierarchyBuilder(tapes);
            _stereoTapeMatcher = new StereoTapeMatcher();
            _stereoTapeRecombiner = new StereoTapeRecombiner(synthWishes);
            _stereoTapeActionRunner = new StereoTapeActionRunner(synthWishes);
            _monoTapeActionRunner = new MonoTapeActionRunner(synthWishes);
            _channelTapeActionRunner = new ChannelTapeActionRunner(synthWishes);
        }
        
        public void RunAllTapes()
        {
            if (_tapes.Count == 0) return;
            
            // HACK: Override sampling rate with currently resolved sampling rate.
            // (Can be customized for long-running tests,
            // but separate threads cannot check the test category.)
            var originalSamplingRate = _synthWishes.Config._samplingRate;
            try
            {
                _synthWishes.WithSamplingRate(_synthWishes.GetSamplingRate);
               
                ExecutePreProcessing();
                var tapes = RunTapeLeavesConcurrent();
                ExecutePostProcessing(tapes);
            }
            finally
            {
                _synthWishes.Config._samplingRate = originalSamplingRate;
            }
        }
        
        private void ExecutePreProcessing()
        {
            var tapes = _tapes.ToArray();
            
            tapes.ForEach(_tapePadder.ApplyPadding);
            
            tapes = _tapes.ToArray();
            
            _tapeHierarchyBuilder.BuildTapeHierarchyRecursive(tapes);
            
            Console.WriteLine(PlotTapeHierarchy(tapes));
        }
        
        private readonly AutoResetEvent _checkForNewLeavesReset = new AutoResetEvent(false);
        
        private IList<Tape> RunTapeLeavesConcurrent()
        {
            Tape[] originalTapeCollection = _tapes.ToArray();
            _tapes.Clear();
            HashSet<Tape> hashSet = originalTapeCollection.ToHashSet();
            List<Task> tasks = new List<Task>(hashSet.Count);
            
            long waitCount = 0;
            while (hashSet.Count > 0)
            {
                Tape leaf = hashSet.FirstOrDefault(x => x.ChildTapes.Count == 0);
                if (leaf != null)
                {
                    LogAction(leaf, "Leaf Found", "Running");
                    
                    hashSet.Remove(leaf);
                    Task task = Task.Run(() => ProcessLeaf(leaf));
                    tasks.Add(task);
                }
                else
                {
                    waitCount++;
                    
                    LogAction(nameof(Tape), "No Leaf", "Wait... " + waitCount);
                    
                    _checkForNewLeavesReset.WaitOne();
                    
                    LogAction(nameof(Tape), "Task Finished", "Continue");
                }
            }
            
            Task.WaitAll(tasks.ToArray());

            LogAction(nameof(Tape), "Total waits for leaves: " + waitCount);

            return originalTapeCollection;
        }
        
        private void ProcessLeaf(Tape leaf)
        {
            try
            {
                // Run tape
                RunTape(leaf);
            }
            finally
            {
                // Don’t let a thread crash cause the while loop for child tapes to retry infinitely.
                // Exceptions will propagate after the while loop (where we wait for all tasks to finish),
                // so let’s make sure the while loop can finish properly.
                CleanupParentChildRelationship(leaf);
                
                _checkForNewLeavesReset.Set();
            }
        }
        
        internal void RunTape(Tape tape)
        {
            LogAction(tape, "Start");
            
            // Cache Buffer
            tape.Buff = tape.Signal.SynthWishes.MaterializeCache(tape.Signal, tape.Duration, tape.GetName);
            
            // Run Actions (that can't wait)
            _channelTapeActionRunner.CacheIfNeeded(tape);
            
            // Wrap in Sample
            FlowNode sample = tape.Signal.SynthWishes.Sample(tape.Buff, name: tape.GetName);
            
            // Replace All References
            IList<Inlet> connectedInlets = tape.Signal.UnderlyingOutlet.ConnectedInlets.ToArray();
            foreach (Inlet inlet in connectedInlets)
            {
                inlet.LinkTo(sample);
            }
            
            LogAction(tape, "Stop");
        }
                
        private static void CleanupParentChildRelationship(Tape leaf)
        {
            leaf.ParentTape?.ChildTapes.Remove(leaf);
            leaf.ParentTape = null;
        }

        private void ExecutePostProcessing(IList<Tape> tapes)
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

            _monoTapeActionRunner.CacheIfNeeded(tapes);
            _stereoTapeActionRunner.CacheIfNeeded(stereoTapes);
            
            _channelTapeActionRunner.SaveIfNeeded(tapes);
            _monoTapeActionRunner.SaveIfNeeded(tapes);
            _stereoTapeActionRunner.SaveIfNeeded(stereoTapes);

            _channelTapeActionRunner.PlayIfNeeded(tapes);
            _monoTapeActionRunner.PlayIfNeeded(tapes);
            _stereoTapeActionRunner.PlayIfNeeded(stereoTapes);
        }
    }
}