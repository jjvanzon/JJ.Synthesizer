using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Threading.Tasks.Task;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;
// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization
    
    public partial class SynthWishes
    {
        public FlowNode Tape(FlowNode signal, FlowNode duration = null)
        {
            Tape tape = AddTape(signal);
            tape.Duration = duration ?? GetAudioLength;
            return signal;
        }

        internal void RunParallelsRecursive(IList<FlowNode> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            
            SetTapeNestingLevelsRecursive(channels);
            
            var tasks = new Task[channels.Count];
            for (int unsafeIndex = 0; unsafeIndex < channels.Count; unsafeIndex++)
            {
                int i = unsafeIndex;
                var channel = channels[i];
                var tasks2 = CreateTapeTasksRecursive(channel, i);
                tasks[i] = Run(() => RunTapes(tasks2));
            }
            
            WaitAll(tasks);
        }
        
        private void SetTapeNestingLevelsRecursive(IList<FlowNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node == null) continue;
                SetTapeNestingLevelsRecursive(node, 1);
            }
        }
        
        private void SetTapeNestingLevelsRecursive(FlowNode node, int level)
        {
            Tape tape = TryGetTape(node);
            if (tape != null)
            {
                // Don't overwrite in case of multiple usage.
                if (tape.NestingLevel == default) tape.NestingLevel = level; 
            }
            
            foreach (var child in node.Operands)
            {
                if (child == null) continue;
                SetTapeNestingLevelsRecursive(child, level + 1);
            }
        }

        private IList<(Task Task, int Level)> CreateTapeTasksRecursive(FlowNode op, int channelIndex)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var tasks = new List<(Task, int)>();
            var operands = op.Operands.ToArray();
            
            // Recursively gather tasks from child nodes
            foreach (FlowNode operand in operands)
            {
                if (operand == null) continue;
                tasks.AddRange(CreateTapeTasksRecursive(operand, channelIndex));
            }
            
            for (var unsafeIndex = 0; unsafeIndex < operands.Length; unsafeIndex++)
            {
                int i = unsafeIndex;
                FlowNode operand = operands[i];
                if (operand == null) continue;
                
                // Are we being parallel?
                Tape tape = TryGetTape(operand);
                if (tape != null)
                {
                    RemoveTape(tape);
                    
                    // Preliminary assignment of variables. Will have been filled in already later.
                    tape.ChannelIndex = channelIndex;

                    var task = new Task(() => RunTape(tape));
                    
                    tasks.Add((task, tape.NestingLevel));
                }
            }

            return tasks;
        }
        
        private void RunTapes(IList<(Task Task, int Level)> tasks)
        {
            // Group tasks by nesting level
            var groups = tasks.OrderByDescending(x => x.Level).GroupBy(x => x.Level);
            foreach (var group in groups)
            {
                // Execute each nesting level's task simultaneously.
                Task[] tasks2 = group.Select(x => x.Task).ToArray();
                tasks2.ForEach(x => x.Start());
                WaitAll(tasks2); // Ensure each level completes before moving up
            }
        }

        private void RunTape(Tape tape)
        {
            Console.WriteLine($"{PrettyTime()} Start Task: (Level {tape.NestingLevel}) {tape.Name}");
            
            // Cache Buffer
            Buff cacheBuff = Cache(tape.Signal, tape.Duration, tape.Name);
            
            // Actions
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
            
            Console.WriteLine($"{PrettyTime()}   End Task: (Level {tape.NestingLevel}) {tape.Name} ");
        }
        
        // Tapes
        
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        private Tape AddTape(FlowNode signal)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            var tape = new Tape { Signal = signal, Name = signal.Name };
            _tapes[signal] = tape;
            return tape;
        }
        
        private bool IsTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return _tapes.ContainsKey(outlet);
        }

        private void RemoveTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.Remove(outlet);
        }

        private void RemoveTape(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            if (tape.Signal == null) throw new NullException(() => tape.Signal);
            _tapes.Remove(tape.Signal);
        }
        
        private Tape TryGetTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
    }
    
    // FlowNode
    
    public partial class FlowNode
    {
        public FlowNode Tape(FlowNode duration = null)
            => _synthWishes.Tape(this, duration);
    }
    
    // Info Type
    
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class Tape
    {
        public string Name { get; set; }
        public FlowNode Signal { get; set; }
        public FlowNode Duration { get; set; }
        public bool MustPlay { get; set; }
        public bool MustSave { get; set; }
        /// <summary> Purely informational </summary>
        public bool IsCache { [UsedImplicitly] get; set; }
        public string FilePath { get; set; }
        public int NestingLevel { get; set; }
        public int ChannelIndex { get; set; }
        public Task Task { get; set; }
        public Action<Buff, int> Callback { get; set; }
        
        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
    
    /// <summary>
    /// Proposed TapeInfo with many future properties,
    /// too many to implement all at the same time.
    /// Outcommented properties are done.
    /// </summary>
    [Obsolete]
    internal class TapeInfoPrototype
    {
        //public Outlet Outlet { get; set; }
        //public int NestingLevel { get; set; }
        public Task Task { get; set; }
        
        //public bool MustPlay { get; set; }
        //public bool MustSave { get; set; }
        //public string FilePath { get; set; }
        //public bool MustCache { get; set; }
        
        //public FlowNode Duration { get; set; }
    }
}
