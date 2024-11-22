using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.LinkTo;
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
            
            // New prep steps (not yet used in processing)
            SetTapeLevelsRecursive(channels);
            
            var tasks = new Task[channels.Count];
            for (int unsafeI = 0; unsafeI < channels.Count; unsafeI++)
            {
                int i = unsafeI;
                
                tasks[i] = Run(() => RunParallelsRecursive(channels[i], i));
            }
            
            WaitAll(tasks);
        }
        
        private void SetTapeLevelsRecursive(IList<FlowNode> nodes)
        {
            foreach (var node in nodes)
            {
                if (node == null) continue;
                SetTapeLevelsRecursive(node, 1);
            }
        }
        
        private void SetTapeLevelsRecursive(FlowNode node, int level)
        {
            Tape tape = TryGetTape(node);
            if (tape != null) tape.Level = level;
            
            foreach (var child in node.Operands)
            {
                if (child == null) continue;
                SetTapeLevelsRecursive(child, level + 1);
            }
        }

        private void RunParallelsRecursive(FlowNode op, int channelIndex)
        {
            // Gather all tasks with levels
            var tasks = GetParallelTasksRecursive(op, channelIndex, level: 1);
            
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

        private IList<(Task Task, int Level)> GetParallelTasksRecursive(FlowNode op, int channelIndex, int level)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var tasks = new List<(Task, int)>();
            var operands = op.Operands.ToArray();
            
            // Recursively gather tasks from child nodes
            foreach (FlowNode operand in operands)
            {
                if (operand == null) continue;
                tasks.AddRange(GetParallelTasksRecursive(operand, channelIndex,level + 1));
            }
            
            for (var unsafeI = 0; unsafeI < operands.Length; unsafeI++)
            {
                int i = unsafeI;
                FlowNode operand = operands[i];
                if (operand == null) continue;
                
                // Are we being parallel?
                Tape tape = TryGetTape(operand);
                if (tape != null)
                {
                    RemoveTape(tape);
                    
                    var task = new Task(() =>
                    {
                        string name = operand.Name;
                        
                        Console.WriteLine($"{PrettyTime()} Start Task: (Level {level}) {name}");

                        // Cache Buffer
                        Buff cacheBuff = Cache(operand, tape.Duration, name);
                        
                        // Actions
                        tape.Callback?.Invoke(cacheBuff, channelIndex);
                        if (tape.MustSave) Save(cacheBuff, tape.FilePath, name);
                        if (tape.MustPlay || GetPlayAllTapes) Play(cacheBuff);
                        
                        // Wrap in Sample
                        var sample = Sample(cacheBuff, name: name);
                        
                        // Replace All References
                        IList<Inlet> connectedInlets = operand.UnderlyingOutlet.ConnectedInlets.ToArray();
                        foreach (Inlet inlet in connectedInlets)
                        {
                            inlet.LinkTo(sample);
                        }

                        Console.WriteLine($"{PrettyTime()}   End Task: (Level {level}) {name} ");
                    });
                    
                    tasks.Add((task, level));
                }
            }

            return tasks;
        }
        
        // Tapes
        
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        private Tape AddTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            var tape = new Tape { Outlet = outlet };
            _tapes[outlet] = tape;
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
            if (tape.Outlet == null) throw new NullException(() => tape.Outlet);
            _tapes.Remove(tape.Outlet);
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
    
    internal class Tape
    {
        public Outlet Outlet { get; set; }
        public FlowNode Duration { get; set; }
        public bool MustPlay { get; set; }
        public bool MustSave { get; set; }
        /// <summary> Purely informational </summary>
        public bool IsCache { [UsedImplicitly] get; set; }
        public string FilePath { get; set; }
        public int Level { get; set; }
        public Task Task { get; set; }
        public Action<Buff, int> Callback { get; set; }
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
        public int Level { get; set; }
        public Task Task { get; set; }
        
        //public bool MustPlay { get; set; }
        //public bool MustSave { get; set; }
        //public string FilePath { get; set; }
        //public bool MustCache { get; set; }
        
        //public FlowNode Duration { get; set; }
    }
}
