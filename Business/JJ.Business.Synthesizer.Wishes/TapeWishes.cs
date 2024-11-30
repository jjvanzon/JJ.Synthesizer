using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

// ReSharper disable MemberCanBePrivate.Global

namespace JJ.Business.Synthesizer.Wishes
{
    // Tape Object
    
    [DebuggerDisplay("{DebuggerDisplay}")]
    internal class Tape
    {
        /// <inheritdoc cref="docs._tapename" />
        public string Name 
        {
            get => Signal?.Name;
            set
            {
                if (Signal == null) throw new NullException(() => Signal);
                Signal.Name = value;
            }
        }
        
        public FlowNode Signal { get; set; }
        public FlowNode Duration { get; set; }
        public int ChannelIndex { get; set; }
        
        public bool MustPlay { get; set; }
        public bool MustSave { get; set; }
        public string FilePath { get; set; }
        public Action<Buff, int> Callback { get; set; }
        
        public Tape ParentTape { get; set; }
        public IList<Tape> ChildTapes { get; } = new List<Tape>();
        
        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    
        // Informational
        
        //public string Name { get; set; }
        public int NestingLevel { get; set; }
        public bool IsCache { [UsedImplicitly] get; set; }
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
            if (_tapes.Count == 0) return;
            
            // HACK: Override sampling rate with currently resolved sampling rate.
            // (Can be customized for long-running tests, but separate threads cannot check the test category.)
            int storedSamplingRate = _configResolver._samplingRate;
            int resolvedSamplingRate = _configResolver.GetSamplingRate;
            try
            {
                WithSamplingRate(resolvedSamplingRate);
                RunTapesPerChannel(channels);
            }
            finally
            {
                WithSamplingRate(storedSamplingRate);
            }
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
                tasks[i] = Task.Run(() => RunTapeLeafPipeline(tapeGroup));
            }

            Task.WaitAll(tasks);
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
        
        private void RunTapeLeafPipeline(IEnumerable<Tape> tapeCollection)
        {
            int waitTimeMs = (int)(GetParallelTaskCheckDelay * 1000);
            
            Console.WriteLine($"{PrettyTime()} Tapes: Leaf check delay = {waitTimeMs} ms");

            List<Tape> tapes = tapeCollection.ToList();
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
            Console.WriteLine($"{PrettyTime()} Start Tape: (Level {tape.NestingLevel}) {tape.Name}");
            
            // Cache Buffer
            Buff cacheBuff = Cache(tape.Signal, tape.Duration);
            
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
