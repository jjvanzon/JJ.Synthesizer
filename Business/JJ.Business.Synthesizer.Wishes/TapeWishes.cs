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
    // SynthWishes Parallelization
    
    public partial class SynthWishes
    {
        public FlowNode Tape(FlowNode signal, FlowNode duration = null)
        {
            Tape tape = AddTape(signal);
            tape.Duration = duration ?? GetAudioLength;
            return signal;
        }
        
        internal void RunAllTapes(IList<FlowNode> channels)
        {
            RunTapesPerChannel(channels);
        }
        
        private void RunTapesPerChannel(IList<FlowNode> channels) 
        {
            if (channels == null) throw new ArgumentNullException(nameof(channels));
            if (channels.Contains(null)) throw new Exception("channels.Contains(null)");
            
            channels.ForEach(x => SetTapeNestingLevelsRecursive(x));
            
            var tapes = GetTapes();
            ClearTapes();

            // Future replacement
            var tapeGroups = tapes.GroupBy(x => x.ChannelIndex)
                                  .Select(x => x.ToArray())
                                  .ToArray();
            
            var tasks = new Task[tapeGroups.Length];
            for (var i = 0; i < tapeGroups.Length; i++)
            {
                Tape[] tapeGroup = tapeGroups[i];
                tasks[i] = Task.Run(() => RunTapesPerNestingLevel(tapeGroup));
            }

            Task.WaitAll(tasks);
        }
        
        private void SetTapeNestingLevelsRecursive(FlowNode node, int level = 1)
        {
            Tape tape = TryGetTape(node);
            if (tape != null)
            {
                // Don't overwrite in case of multiple usage.
                if (tape.NestingLevel == default) tape.NestingLevel = level; 
            }
            
            foreach (FlowNode child in node.Operands)
            {
                if (child == null) continue;
                SetTapeNestingLevelsRecursive(child, level + 1);
            }
        }
        
        private void RunTapesPerNestingLevel(IList<Tape> tapes)
        {
            // Group tasks by nesting level
            var tapeGroups = tapes.OrderByDescending(x => x.NestingLevel)
                                  .GroupBy(x => x.NestingLevel)
                                  .Select(x => x.ToArray())
                                  .ToArray();
            
            // Execute each nesting level's task simultaneously.
            foreach (Tape[] tapeGroup in tapeGroups)
            {
                Task[] tasks = new Task[tapeGroup.Length];
                for (var i = 0; i < tapeGroup.Length; i++)
                {
                    Tape tape = tapeGroup[i];
                    tasks[i] = Task.Run(() => RunTape(tape));
                }
                Task.WaitAll(tasks); // Ensure each level completes before moving up
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
        
        private void ClearTapes()
        {
            _tapes.Clear();
        }
        
        private IList<Tape> GetTapes() => _tapes.Values.ToArray();
        
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
        public Action<Buff, int> Callback { get; set; }
        
        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    }
}
