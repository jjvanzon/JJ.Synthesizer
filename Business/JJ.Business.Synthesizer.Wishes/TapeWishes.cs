using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.LinkTo;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Common;
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
        public string GetName => FetchName(Signal, FallBackName, FilePath);
        
        public FlowNode Signal { get; set; }
        public FlowNode Duration { get; set; }
        public int ChannelIndex { get; set; }
        
        public bool WithPlay { get; set; }
        public bool WithSave { get; set; }
        public bool WithCache { get; set; }
        public bool WithPlayChannel { get; set; }
        public bool WithSaveChannel { get; set; }
        public bool WithCacheChannel { get; set; }

        public string FilePath { get; set; }
        public Func<Buff, Buff> Callback { get; set; }
        public Func<Buff, int, Buff> ChannelCallback { get; set; }
        public Buff Buff { get; set; }
        
        public Tape ParentTape { get; set; }
        public IList<Tape> ChildTapes { get; } = new List<Tape>();

        private string DebuggerDisplay => DebuggerDisplayFormatter.GetDebuggerDisplay(this);
    
        // Informational
        public string FallBackName { get; set; }
        public int NestingLevel { get; set; }
    }
    
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
            Tape tape = AddTape(signal, callerMemberName);
            tape.Duration = duration ?? GetAudioLength;
            return signal;
        }
        
        // Tapes Collection
        
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        internal Tape AddTape(FlowNode signal, [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            var tape = new Tape
            {
                Signal = signal,
                ChannelIndex = GetChannelIndex,
                FallBackName = callerMemberName
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
            
            // Here we have the channel buffs.
            // Now we need to associate the left and right channel buffs with each other. 
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
            
            // Run Actions
            Buff replacementBuff = tape.ChannelCallback?.Invoke(cacheBuff, tape.ChannelIndex);
            if (replacementBuff != null) cacheBuff = replacementBuff;
            if (tape.WithSaveChannel) Save(cacheBuff, tape.FilePath, tape.GetName);
            if (tape.WithPlayChannel || GetPlayAllTapes) Play(cacheBuff);
            
            // Wrap in Sample
            FlowNode sample = Sample(cacheBuff, name: tape.GetName);
            
            // Replace All References
            IList<Inlet> connectedInlets = tape.Signal.UnderlyingOutlet.ConnectedInlets.ToArray();
            foreach (Inlet inlet in connectedInlets)
            {
                inlet.LinkTo(sample);
            }

            tape.Buff = cacheBuff;
            
            Console.WriteLine($"{PrettyTime()}  Stop Tape: (Level {tape.NestingLevel}) {tape.GetName} ");
        }
    }
    
    internal class StereoTapeMatcher
    {
        /// <summary>
        /// Heuristically matches the left channel and right channel tapes together,
        /// which is non-trivial because of the rigid separation of the 2 channels in this system,
        /// which is both powerful and tricky all the same.
        /// Only call in case of stereo. In case of mono, the tapes can be played, saved or cached
        /// individually.
        /// </summary>
        public IList<(Tape Left, Tape Right)> PairTapes(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            if (tapes.Count == 1) throw new Exception("Stereo tapes expected, but only 1 channel tape found.");
               
            var unprocessedTapes = tapes.ToHashSet();
            var tapePairs = new List<(Tape, Tape)>();

            // Return single stereo tape
            {
                var pair = TryGetPair(tapes);
                if (pair != default)
                {
                    tapePairs.Add(pair);
                    return tapePairs;
                }
            }
                
            // First grouping:
            // Group by flags, as they definitely should match.
            var tapesGroupedByFlags = tapes.GroupBy(x => new
            {
                x.WithPlay,
                x.WithSave,
                x.WithCache,
                x.WithPlayChannel,
                x.WithSaveChannel,
                x.WithCacheChannel
            });
                
            foreach (var tapesMatchedByFlags in tapesGroupedByFlags)
            {
                {
                    var pairMatchedByFlags = TryGetPair(tapesMatchedByFlags);
                    if (pairMatchedByFlags != default)
                    {
                        tapePairs.Add(pairMatchedByFlags);
                        unprocessedTapes.Remove(pairMatchedByFlags.left);
                        unprocessedTapes.Remove(pairMatchedByFlags.right);
                        continue;
                    }
                }

                // Match by delegate method
                {
                    var tapesGroupedByClosureMethod
                        = tapesMatchedByFlags.Where(x => x.Callback != null)
                                                .GroupBy(x => x.Callback.Method);
                        
                    foreach (var tapesMatchedByClosureMethod in tapesGroupedByClosureMethod)
                    {
                        var pairMatchedByClosureMethod = TryGetPair(tapesMatchedByClosureMethod);
                        if (pairMatchedByClosureMethod != default)
                        {
                            tapePairs.Add(pairMatchedByClosureMethod);
                            unprocessedTapes.Remove(pairMatchedByClosureMethod.left);
                            unprocessedTapes.Remove(pairMatchedByClosureMethod.right);
                        }
                    }
                }
                    
                // Match by delegate type
                {
                    var tapesGroupedByClosureClass
                        = tapesMatchedByFlags.Where(x => x.Callback != null)
                                                .GroupBy(x => x.Callback.Method.DeclaringType);
                        
                    foreach (var tapesMatchedByClosureClass in tapesGroupedByClosureClass)
                    {
                        var pairMatchedByClosureClass = TryGetPair(tapesMatchedByClosureClass);
                        if (pairMatchedByClosureClass != default)
                        {
                            tapePairs.Add(pairMatchedByClosureClass);
                            unprocessedTapes.Remove(pairMatchedByClosureClass.left);
                            unprocessedTapes.Remove(pairMatchedByClosureClass.right);
                        }
                    }
                }
                    
                // Match by name
                {
                    var tapesGroupedByNames
                        = tapesMatchedByFlags.GroupBy(x => new
                        {
                            x.FallBackName,
                            x.FilePath,
                            x.Signal?.Name
                        });
                        
                    foreach (var tapesMatchedByName in tapesGroupedByNames)
                    {
                        var tapePair = TryGetPair(tapesMatchedByName);
                        if (tapePair != default)
                        {
                            tapePairs.Add(tapePair);
                            unprocessedTapes.Remove(tapePair.left);
                            unprocessedTapes.Remove(tapePair.right);
                        }
                    }
                }
            }
            
            if (unprocessedTapes.Count > 0)
            {
                // TODO: Make exception more specific.
                throw new Exception("Some channel tapes could not matched to for a stereo tape.");
            }
                        
            return tapePairs;
        }
            
        private static (Tape left, Tape right) TryGetPair(IEnumerable<Tape> potentialPair)
        {
            if (potentialPair == null) throw new ArgumentNullException(nameof(potentialPair));
                
            var array = potentialPair as Tape[] ?? potentialPair.ToArray();
                
            if (array.Length != 2)
            {
                return default;
            }

            var left = array.FirstOrDefault(x => x.ChannelIndex == 0);
            if (left == null) throw new Exception("There are 2 channel tapes, but none of them are a Left channel (ChannelIndex = 0).");
                
            var right = array.FirstOrDefault(x => x.ChannelIndex == 1);
            if (right == null) throw new Exception("There are 2 channel tapes, but none of them are a Right channel (ChannelIndex = 1).");
                
            var pair = (left, right);
                
            return pair;
        }
    }
}
