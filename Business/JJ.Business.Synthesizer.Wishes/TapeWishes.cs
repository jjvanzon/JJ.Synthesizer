using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Wishes
{
    // Tape Object
    
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class Tape
    {
        public string Name { get; set; }
        public FlowNode Signal { get; set; }
        public FlowNode Duration { get; set; }
        public bool MustPlay { get; set; }
        public bool MustSave { get; set; }
        public string FilePath { get; set; }
        public bool IsCache { [UsedImplicitly] get; set; }
        public Action<Buff, int> Callback { get; set; }
        public int ChannelIndex { get; set; }
        public int NestingLevel { get; set; }
        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
        public Tape ParentTape { get; set; }
        public IList<Tape> ChildTapes { get; } = new List<Tape>();
    }
    
    // Tape Method
    
    public partial class FlowNode
    {
        public FlowNode Tape(FlowNode duration = null)
            => _synthWishes.Tape(this, duration);
    }

    public partial class SynthWishes
    {
        public FlowNode Tape(FlowNode signal, FlowNode duration = null)
        {
            Tape tape = AddTape(signal);
            tape.Duration = duration ?? GetAudioLength;
            return signal;
        }
        
        // Tapes Collection
        
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        internal Tape AddTape(FlowNode signal)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            var tape = new Tape
            {
                Signal = signal,
                Name = signal.Name,
                ChannelIndex = GetChannelIndex
            };
            
            _tapes[signal] = tape;
            
            return tape;
        }
        
        internal bool IsTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return _tapes.ContainsKey(outlet);
        }
        
        private Tape TryGetTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
        
        private Tape[] GetAllTapes() => _tapes.Values.ToArray();
        
        private void ClearTapes() => _tapes.Clear();
        
        // Run Tapes
        
        internal void RunAllTapes(IList<FlowNode> channels)
        {
            RunTapesPerChannel(channels);
        }
        
        private void RunTapesPerChannel(IList<FlowNode> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            
            channels.ForEach(x => SetTapeNestingLevelsRecursive(x));
            channels.ForEach(x => SetTapeParentChildRelationshipsRecursive(x));
            
            Tape[] tapes = GetAllTapes();
            ClearTapes();

            var tapeGroups = tapes.GroupBy(x => x.ChannelIndex)
                                  .Select(x => x.ToArray())
                                  .ToArray();
            
            var tasks = new Task[tapeGroups.Length];
            for (var i = 0; i < tapeGroups.Length; i++)
            {
                Tape[] tapeGroup = tapeGroups[i];
                tasks[i] = Task.Run(() => RunTapesPerLeafBatch(tapeGroup));
            }

            Task.WaitAll(tasks);
        }
        
        
        private void RunLeafBatchPipeline()
        {
            
            
        }

        private void SetTapeNestingLevelsRecursive(FlowNode node, int level = 1)
        {
            Tape tape = TryGetTape(node);
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
            Tape tape = TryGetTape(node);
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
        
        private void RunTapesLeavesPerBatch(Tape[] tapes)
        {
            while (tapes.Length > 0)
            {
                tapes = RunTapeLeafBatch(tapes);
            }
        }

        private Tape[] RunTapeLeafBatch(Tape[] tapes)
        {
            // Get leaves
            Tape[] leaves = tapes.Where(x => x.ChildTapes.Count == 0).ToArray();

            // Execute tasks for the leaves
            Task[] tasks = new Task[leaves.Length];
            for (var i = 0; i < leaves.Length; i++)
            {
                Tape tape = leaves[i];
                tasks[i] = Task.Run(() => RunTape(tape));
            }
            
            // Ensure the leaf batch completes before moving on
            Task.WaitAll(tasks);
            
            // Remove parent-child relationship
            foreach (Tape leaf in leaves)
            {
                leaf.ParentTape?.ChildTapes.Remove(leaf);
                leaf.ParentTape = null;
            }

            // Return remaining tapes
            Tape[] remainingTapes = tapes.Except(leaves).ToArray();
            return remainingTapes;
        }

        internal void RunTape(Tape tape)
        {
            Console.WriteLine($"{PrettyTime()} Start Tape: (Level {tape.NestingLevel}) {tape.Name}");
            
            // Cache Buffer
            Buff cacheBuff = Cache(tape.Signal, tape.Duration, tape.Name);
            
            // Run Actions
            tape.Callback?.Invoke(cacheBuff, tape.ChannelIndex);
            if (tape.MustSave) Save(cacheBuff, tape.FilePath, tape.Name);
            if (tape.MustPlay || GetPlayAllTapes) Play(cacheBuff);
            
            // Wrap in Sample
            var sample = Sample(cacheBuff, name: tape.Name);
            
            // Replace All References
            IList<Inlet> connectedInlets = tape.Signal.UnderlyingOutlet.ConnectedInlets.ToArray();
            foreach (Inlet inlet in connectedInlets)
            {
                inlet.LinkTo(sample);
            }
            
            Console.WriteLine($"{PrettyTime()}  Stop Tape: (Level {tape.NestingLevel}) {tape.Name} ");
        }
    }
}
