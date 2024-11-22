using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Threading.Tasks.Task;
using static JJ.Business.Synthesizer.Wishes.Helpers.FrameworkStringWishes;
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ParameterHidesMember
// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable ForCanBeConvertedToForeach

namespace JJ.Business.Synthesizer.Wishes
{
    // SynthWishes Parallelization
    
    public partial class SynthWishes
    {
        internal void RunParallelsRecursive(IList<FlowNode> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            if (!GetParallels) return;

            var tasks = new Task[channels.Count];
            for (int i = 0; i < channels.Count; i++)
            {
                int channelIndex = i;
                tasks[channelIndex] = Run(() => RunParallelsRecursive(channels[channelIndex], channelIndex));
            }
            
            WaitAll(tasks);
        }
        
        private void RunParallelsRecursive(FlowNode op, int channelIndex)
        {
            if (!GetParallels) return;
            
            // Gather all tasks with levels
            var tasks = GetParallelTasksRecursive(op, channelIndex, level: 1);
            
            // Group tasks by nesting level
            var levelGroups = tasks.OrderByDescending(x => x.Level).GroupBy(x => x.Level);
            foreach (var levelGroup in levelGroups)
            {
                // Execute each nesting level's task simultaneously.
                Task[] tasksInLevel = levelGroup.Select(x => x.Task).ToArray();
                tasksInLevel.ForEach(x => x.Start());
                WaitAll(tasksInLevel); // Ensure each level completes before moving up
            }
        }
        
        private IList<(Task Task, int Level)> GetParallelTasksRecursive(FlowNode op, int channelIndex, int level)
        {
            if (op == null) throw new ArgumentNullException(nameof(op));

            var tasks = new List<(Task, int)>();
            var operands = op.Operands.ToArray();
            
            // Recursively gather tasks from child nodes
            foreach (var operand in operands)
            {
                if (operand == null) continue;
                tasks.AddRange(GetParallelTasksRecursive(operand, channelIndex,level + 1));
            }
            
            for (var unsafeI = 0; unsafeI < operands.Length; unsafeI++)
            {
                int i = unsafeI;
                var operand = operands[i];
                
                if (operand == null) continue;
                
                // Are we being parallel?
                Tape tape = TryGetTape(operand);
                if (tape != null)
                {
                    RemoveTape(tape);
                    
                    var task = new Task(() =>
                    {
                        Console.WriteLine($"{PrettyTime()} Start Task: {operand.Name} (Level {level})");
                        
                        var cacheResult = Cache(operand, operand.Name);
                        
                        // Actions
                        tape.ResultCallback?.Invoke(cacheResult, channelIndex);
                        if (tape.MustSave) Save(cacheResult, tape.FilePath, operand.Name);
                        if (tape.MustPlay || GetPlayAllTapes) Play(cacheResult);
                        
                        var sampleOutlet = Sample(cacheResult, name: operand.Name);
                        
                        // Replace all references to tape
                        IList<Inlet> connectedInlets = operand.UnderlyingOutlet.ConnectedInlets.ToArray();
                        foreach (Inlet inlet in connectedInlets)
                        {
                            inlet.LinkTo(sampleOutlet);
                        }

                        Console.WriteLine($"{PrettyTime()} End Task: {operand.Name} (Level {level})");
                    });
                    
                    tasks.Add((task, level));
                }
            }

            return tasks;
        }
        
        // Taoes
        
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
        
        // Public Methods
        
        // Tape
        
        public FlowNode Tape(FlowNode signal)
            => Tape(signal, default);
        
        public FlowNode Tape(FlowNode signal, FlowNode duration)
        {
            //duration = duration ?? GetAudioLength ?? _[1];
            AddTape(signal);
            return signal;
        }
        
        // ChannelPlay
        
        public FlowNode ChannelPlay(FlowNode signal)
            => ChannelPlay(signal, default(Action<AudioStreamResult, int>));

        public FlowNode ChannelPlay(FlowNode signal, Action<AudioStreamResult> resultCallback)
            => ChannelPlay(signal, (x, i) => resultCallback(x));
        
        public FlowNode ChannelPlay(FlowNode signal, Action<AudioStreamResult, int> resultCallback)
        {
            Tape tape = AddTape(signal);
            tape.MustPlay = true;
            tape.ResultCallback = resultCallback;
            return signal;
        }
        
        // ChannelSave
        
        public FlowNode ChannelSave(FlowNode signal)
            => ChannelSave(signal, null, default(Action<AudioStreamResult, int>));
        
        public FlowNode ChannelSave(FlowNode signal, string filePath)
            => ChannelSave(signal, filePath, default(Action<AudioStreamResult, int>));
        
        public FlowNode ChannelSave(FlowNode signal, Action<AudioStreamResult> resultCallback)
            => ChannelSave(signal, null, resultCallback);
        
        public FlowNode ChannelSave(FlowNode signal, Action<AudioStreamResult, int> resultCallback)
            => ChannelSave(signal, null, resultCallback);
        
        public FlowNode ChannelSave(FlowNode signal, string filePath, Action<AudioStreamResult> resultCallback)
            => ChannelSave(signal, filePath, (x, y) => resultCallback(x));

        public FlowNode ChannelSave(FlowNode signal, string filePath, Action<AudioStreamResult, int> resultCallback)
        {
            Tape tape = AddTape(signal);
            tape.MustSave = true;
            tape.FilePath = filePath;
            tape.ResultCallback = resultCallback;
            return signal;
        }
        
        // ChannelCache
        
        public FlowNode ChannelCache(FlowNode signal, Action<AudioStreamResult> resultCallback)
            => ChannelCache(signal, (x, i) => resultCallback(x));

        public FlowNode ChannelCache(FlowNode signal, Action<AudioStreamResult, int> resultCallback) 
        {
            Tape tape = AddTape(signal);
            tape.ResultCallback = resultCallback;
            return signal;
        }
        
        // ParallelAdd
        
        /// <inheritdoc cref="docs._paralleladd" />
        public FlowNode ParallelAdd(params FlowNode[] termFuncs)
            => ParallelAdd((IList<FlowNode>)termFuncs);

        /// <inheritdoc cref="docs._paralleladd" />
        public FlowNode ParallelAdd(IList<FlowNode> terms)
        {
            if (terms == null) throw new ArgumentNullException(nameof(terms));
            
            var add = Add(terms);
            
            if (GetParallels)
            {
                foreach (var term in add.Operands)
                {
                    Tape(term);
                }
            }
            
            return add;
        }
        
    }
    
    // FlowNode
    
    public partial class FlowNode
    {
        // Tape
        
        public FlowNode Tape(FlowNode duration = null)
            => _synthWishes.Tape(this, duration);
        
        // ChannelPlay
        
        public FlowNode ChannelPlay()
            => _synthWishes.ChannelPlay(this);
        
        public FlowNode ChannelPlay(Action<AudioStreamResult> resultCallback)
            => _synthWishes.ChannelPlay(this, resultCallback);
        
        public FlowNode ChannelPlay(Action<AudioStreamResult, int> resultCallback)
            => _synthWishes.ChannelPlay(this, resultCallback);
        
        // ChannelSave
        
        public FlowNode ChannelSave()
            => _synthWishes.ChannelSave(this);
        
        public FlowNode ChannelSave(string filePath)
            => _synthWishes.ChannelSave(this, filePath);
        
        public FlowNode ChannelSave(Action<AudioStreamResult> resultCallback)
            => _synthWishes.ChannelSave(this, resultCallback);
        
        public FlowNode ChannelSave(Action<AudioStreamResult, int> resultCallback)
            => _synthWishes.ChannelSave(this, resultCallback);
        
        public FlowNode ChannelSave(string filePath, Action<AudioStreamResult> resultCallback)
            => _synthWishes.ChannelSave(this, filePath, resultCallback);
        
        public FlowNode ChannelSave(string filePath, Action<AudioStreamResult, int> resultCallback)
            => _synthWishes.ChannelSave(this, filePath, resultCallback);
        
        // ChannelCache
        
        public FlowNode ChannelCache(Action<AudioStreamResult> resultCallback)
            => _synthWishes.ChannelCache(this, resultCallback);
        
        public FlowNode ChannelCache(Action<AudioStreamResult, int> resultCallback)
            => _synthWishes.ChannelCache(this, resultCallback);
    }
    
    // Info Type
    
    internal class Tape
    {
        public Outlet Outlet { get; set; }
        public bool MustPlay { get; set; }
        public bool MustSave { get; set; }
        public string FilePath { get; set; }
        public Action<AudioStreamResult, int> ResultCallback { get; set; }
    }
    
    /// <summary>
    /// Proposed TapeInfo with many future properties,
    /// too many to implement all at the same time.
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
        
        public FlowNode Duration { get; set; }
    }
}
