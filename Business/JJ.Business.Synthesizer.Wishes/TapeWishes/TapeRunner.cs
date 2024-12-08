using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeRunner
    {
        private readonly SynthWishes _synthWishes;
        private readonly TapeCollection _tapes;
        private readonly StereoTapeMatcher _stereoTapeMatcher;
        private readonly StereoTapeRecombiner _stereoTapeRecombiner;
        private readonly StereoTapeActionRunner _stereoTapeActionRunner;
        private readonly MonoTapeActionRunner _monoTapeActionRunner;
        private readonly ChannelTapeActionRunner _channelTapeActionRunner;

        public TapeRunner(SynthWishes synthWishes, TapeCollection tapes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _tapes = tapes ?? throw new ArgumentNullException(nameof(tapes));
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
                var tapes = RunTapesPerChannel();
                ExecutePostProcessing(tapes);
            }
            finally
            {
                _synthWishes.Config._samplingRate = originalSamplingRate;
            }
        }
        
        private void ExecutePreProcessing()
        {
            foreach (Tape tape in _tapes.ToArray())
            {
                ApplyPadding(tape);
            }
        }
        
        /// <inheritdoc cref="docs._applypaddingtotape" />
        private void ApplyPadding(Tape oldTape)
        {
            // Padding only applies to Play and Save actions.
            if (!oldTape.IsPlay && 
                !oldTape.IsPlayChannel &&
                !oldTape.IsSave &&
                !oldTape.IsSaveChannel) return;
            
            // If tape already padded, don't do it again.
            if (oldTape.IsPadding) return;

            // Get variables
            double leadingSilence = _synthWishes.GetLeadingSilence.Value;
            double trailingSilence = _synthWishes.GetTrailingSilence.Value;
            double padding = leadingSilence + trailingSilence;

            // Don't bother if no padding.
            if (padding == 0) return;

            // Apply delay
            string newName = MemberName() + " " + oldTape.Signal.Name;
            FlowNode newNode = _synthWishes.ApplyPaddingDelay(oldTape.Signal).SetName(newName);

            // Add tape
            //if (oldTape.IsTape || oldTape.IsCache)
            Tape newTape = _tapes.GetOrCreate(newNode, oldTape.Duration, oldTape.FilePath);
            newTape.Channel = oldTape.Channel;
            newTape.IsPlay = oldTape.IsPlay;
            newTape.IsSave = oldTape.IsSave;
            newTape.IsCache = oldTape.IsCache;
            newTape.IsPlayChannel = oldTape.IsPlayChannel;
            newTape.IsSaveChannel = oldTape.IsSaveChannel;
            newTape.IsCacheChannel = oldTape.IsCacheChannel;
            newTape.IsPadding = true;
            newTape.FallBackName = oldTape.FallBackName;
            
            // Remove actions from original tape
            oldTape.IsPlay = false;
            oldTape.IsSave = false;
            oldTape.IsPlayChannel = false;
            oldTape.IsSaveChannel = false;

            // Update duration
            var oldDuration = oldTape.Duration ?? _synthWishes.GetAudioLength;
            newTape.Duration = oldDuration + padding;
            
            Console.WriteLine(
                $"{PrettyTime()} Padding: Tape.Duration = {oldDuration} + " +
                $"{leadingSilence} + {trailingSilence} = {newTape.Duration}");
        }
        
        private IList<Tape> RunTapesPerChannel()
        {
            Tape[] tapes = _tapes.GetAll();
            
            foreach (Tape tape in tapes)
            {
                SetTapeParentChildRelationshipsRecursive(tape.Signal);
            }
            
            _tapes.Clear();

            SetTapeNestingLevelsRecursive(tapes);
            
            string tapeLog = LogWishes.PlotTapeHierarchy(tapes);
            Console.WriteLine(tapeLog);

            var tapeGroups = tapes.GroupBy(x => x.Channel)
                                  .Select(x => x.ToArray())
                                  .ToArray();
            
            var tasks = new Task[tapeGroups.Length];
            for (var i = 0; i < tapeGroups.Length; i++)
            {
                Tape[] tapeGroup = tapeGroups[i];
                tasks[i] = Task.Run(() => RunTapeLeafPipeline(tapeGroup));
            }
            
            Task.WaitAll(tasks);
            
            return tapes;
        }
                    
        private void SetTapeNestingLevelsRecursive(IList<Tape> tapes)
        {
            var roots = tapes.Where(x => x.ParentTape == null).ToArray();
            foreach (Tape root in roots)
            {
                SetTapeNestingLevelsRecursive(root);
            }
        }

        private void SetTapeNestingLevelsRecursive(Tape tape, int level = 1)
        {
            // Don't overwrite in case of multiple usage.
            if (tape.NestingLevel == default) tape.NestingLevel = level++;
            
            foreach (Tape child in tape.ChildTapes)
            {
                if (child == null) continue;
                SetTapeNestingLevelsRecursive(child, level);
            }
        }

        private void SetTapeParentChildRelationshipsRecursive(FlowNode node, Tape parentTape = null)
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
                SetTapeParentChildRelationshipsRecursive(child, parentTape);
            }
        }
        
        private void RunTapeLeafPipeline(IList<Tape> tapeCollection)
        {
            int waitTimeMs = (int)(_synthWishes.GetParallelTaskCheckDelay * 1000);
            
            Console.WriteLine($"{PrettyTime()} Tapes: Leaf check delay = {waitTimeMs} ms");
            
            List<Tape> tapes = tapeCollection.ToList(); // Copy list to keep item removals local.
            List<Task> tasks = new List<Task>(tapes.Count);
            
            long i = 0;
            while (tapes.Count > 0)
            {
                i++;
                Tape leaf = tapes.FirstOrDefault(x => x.ChildTapes.Count == 0);
                if (leaf != null)
                {
                    tapes.Remove(leaf);
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
        
        private static void CleanupParentChildRelationship(Tape leaf)
        {
            leaf.ParentTape?.ChildTapes.Remove(leaf);
            leaf.ParentTape = null;
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
                // Run Actions
                var tapesWithActions = tapes.Where(x => x.IsPlay ||
                                                        x.IsSave ||
                                                        x.Callback != null).ToArray();

                var tapePairs = _stereoTapeMatcher.PairTapes(tapesWithActions);
                var stereoTapes = _stereoTapeRecombiner.RecombineChannelsConcurrent(tapePairs);
                
                foreach (var stereoTape in stereoTapes)
                {
                    _stereoTapeActionRunner.RunActions(stereoTape);
                }
            }
        }
    }
}