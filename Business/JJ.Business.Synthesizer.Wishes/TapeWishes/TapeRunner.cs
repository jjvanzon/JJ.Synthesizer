using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
// ReSharper disable ArrangeStaticMemberQualifier

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeRunner
    {
        private readonly SynthWishes _synthWishes;
        private readonly TapeCollection _tapes;
        private readonly TapePadder _tapePadder;
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
            _stereoTapeMatcher = new StereoTapeMatcher();
            _stereoTapeRecombiner = new StereoTapeRecombiner(synthWishes);
            _stereoTapeActionRunner = new StereoTapeActionRunner(synthWishes);
            _monoTapeActionRunner = new MonoTapeActionRunner();
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
            Tape[] tapes = _tapes.ToArray();
            
            tapes.ForEach(_tapePadder.ApplyPadding);
            
            tapes.ForEach(BuildTapeHierarchyRecursive);
            
            SetTapeNestingLevelsRecursive(tapes);
            
            Console.WriteLine(PlotTapeHierarchy(tapes));
        }
        

        private void BuildTapeHierarchyRecursive(Tape tape)
        {
            BuildTapeHierarchyRecursive(tape.Signal, null);
        }
        
        private void BuildTapeHierarchyRecursive(FlowNode node, Tape parentTape)
        {
            Tape tape = _tapes.TryGet(node);
            if (tape != null)
            {
                if (parentTape != null && tape.ParentTape == null)
                {
                    tape.ParentTape = parentTape;
                    parentTape.ChildTapes.Add(tape);
                }
                
                parentTape = tape;
            }
            
            foreach (FlowNode child in node.Operands)
            {
                if (child == null) continue;
                BuildTapeHierarchyRecursive(child, parentTape);
            }
        }
        
        private static void SetTapeNestingLevelsRecursive(IList<Tape> tapes)
        {
            var roots = tapes.Where(x => x.ParentTape == null).ToArray();
            foreach (Tape root in roots)
            {
                SetTapeNestingLevelsRecursive(root);
            }
        }

        private static void SetTapeNestingLevelsRecursive(Tape tape, int level = 1)
        {
            // Don't overwrite in case of multiple usage.
            if (tape.NestingLevel == default) tape.NestingLevel = level++;
            
            foreach (Tape child in tape.ChildTapes)
            {
                if (child == null) continue;
                SetTapeNestingLevelsRecursive(child, level);
            }
        }
        
        private IList<Tape> RunTapeLeavesConcurrent()
        {
            Tape[] originalTapeCollection = _tapes.ToArray();
            _tapes.Clear();
            HashSet<Tape> hashSet = originalTapeCollection.ToHashSet();

            int waitTimeMs = (int)(_synthWishes.GetParallelTaskCheckDelay * 1000);
            
            Console.WriteLine($"{PrettyTime()} Tapes: Leaf check delay = {waitTimeMs} ms");
            
            List<Task> tasks = new List<Task>(hashSet.Count);
            
            long i = 0;
            while (hashSet.Count > 0)
            {
                i++;
                Tape leaf = hashSet.FirstOrDefault(x => x.ChildTapes.Count == 0);
                if (leaf != null)
                {
                    hashSet.Remove(leaf);
                    Task task = Task.Run(() => ProcessLeaf(leaf));
                    tasks.Add(task);
                }
                else
                {
                    Thread.Sleep(waitTimeMs);
                }
            }
            
            Console.WriteLine($"{PrettyTime()} Tapes: {i} times checked for leaves.");
            
            Task.WaitAll(tasks.ToArray());
            
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
            }
        }
        
        internal void RunTape(Tape tape)
        {
            Console.WriteLine($"{PrettyTime()} Start Tape: (Level {tape.NestingLevel}) {tape.GetName}");
            
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
            
            Console.WriteLine($"{PrettyTime()}  Stop Tape: (Level {tape.NestingLevel}) {tape.GetName} ");
        }
                
        private static void CleanupParentChildRelationship(Tape leaf)
        {
            leaf.ParentTape?.ChildTapes.Remove(leaf);
            leaf.ParentTape = null;
        }
        
        private void ExecutePostProcessing(IList<Tape> tapes)
        {
            // Run Channel Actions (in post-processing, that might otherwise hold up the taping tasks)
            foreach (Tape tape in tapes)
            {
                _channelTapeActionRunner.SaveIfNeeded(tape);
                _channelTapeActionRunner.PlayIfNeeded(tape);
            }
            
            if (_synthWishes.IsMono)
            {
                foreach (Tape tape in tapes)
                {
                    _monoTapeActionRunner.RunActions(tape);
                }
            }
            
            if (_synthWishes.IsStereo)
            {
                var tapesWithActions = tapes.Where(x => x.IsPlay ||
                                                        x.IsSave ||
                                                        x.Callback != null).ToArray();

                var tapePairs = _stereoTapeMatcher.PairTapes(tapesWithActions);
                var stereoTapes = _stereoTapeRecombiner.RecombineChannelsConcurrent(tapePairs);
                
                foreach (Tape stereoTape in stereoTapes)
                {
                    _stereoTapeActionRunner.RunActions(stereoTape);
                }
            }
        }
    }
}