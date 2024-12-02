using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;

// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Wishes
{
    // Tape Method
    
    public partial class FlowNode
    {
        public FlowNode Tape(FlowNode duration = null, [CallerMemberName] string callerMemberName = null)
            => _synthWishes.Tape(this, duration, callerMemberName);
    }

    public partial class SynthWishes
    {
        public FlowNode Tape(FlowNode signal, FlowNode duration = null, [CallerMemberName] string callerMemberName = null)
        {
            Tape tape = _tapes.AddTape(signal, callerMemberName);
            tape.Duration = duration ?? GetAudioLength;
            return signal;
        }
        
        // Run Tapes
        
        internal void RunAllTapes(IList<FlowNode> channels)
        {
            if (_tapes.Count == 0) return;
            
            // HACK: Override sampling rate with currently resolved sampling rate.
            // (Can be customized for long-running tests, but separate threads cannot check the test category.)
            int storedSamplingRate = _configResolver._samplingRate;
            int resolvedSamplingRate = _configResolver.GetSamplingRate;
            try
            {
                WithSamplingRate(resolvedSamplingRate);
                IList<Tape> tapes = RunTapesPerChannel(channels);
                //ExecutePostProcessing(tapes);
            }
            finally
            {
                WithSamplingRate(storedSamplingRate);
            }
        }
        
        private IList<Tape> RunTapesPerChannel(IList<FlowNode> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            
            channels.ForEach(x => SetTapeNestingLevelsRecursive(x));
            channels.ForEach(x => SetTapeParentChildRelationshipsRecursive(x));
            
            Tape[] tapes = _tapes.GetAllTapes();
            _tapes.ClearTapes();

            var tapeGroups = tapes.GroupBy(x => x.ChannelIndex)
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
        
        private void SetTapeNestingLevelsRecursive(FlowNode node, int level = 1)
        {
            Tape tape = _tapes.TryGetTape(node);
            if (tape != null)
            {
                // Don't overwrite in case of multiple usage.
                if (tape.NestingLevel == default) tape.NestingLevel = level++;
            }
            
            foreach (FlowNode child in node.Operands)
            {
                if (child == null) continue;
                SetTapeNestingLevelsRecursive(child, level);
            }
        }
        
        private void SetTapeParentChildRelationshipsRecursive(FlowNode node, Tape parentTape = null)
        {
            Tape tape = _tapes.TryGetTape(node);
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
            int waitTimeMs = (int)(GetParallelTaskCheckDelay * 1000);
            
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
            Buff cacheBuff = MaterializeCache(tape.Signal, tape.Duration, tape.GetName);
            tape.Buff = cacheBuff;

            // Run Actions
            _channelTapeActionRunner.RunActions(tape);
            
            // Wrap in Sample
            FlowNode sample = Sample(cacheBuff, name: tape.GetName);
            
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
            switch (GetSpeakers)
            {
                case SpeakerSetupEnum.Mono:
                    
                    foreach (Tape tape in tapes)
                    {
                        _monoTapeActionRunner.RunActions(tape);
                    }
                    
                    break;
                
                case SpeakerSetupEnum.Stereo:
                    
                    var tapePairs = _stereoTapeMatcher.PairTapes(tapes);

                    foreach ((Tape Left, Tape Right) tapePair in tapePairs)
                    {
                        Tape stereoTape = _stereoTapeRecombiner.RecombineChannels(tapePair);
                        _stereoTapeActionRunner.RunActions(stereoTape);
                    }
                    
                    break;
                
                default:
                    
                    throw new ValueNotSupportedException(GetSpeakers);
            }
        }
    }
}
