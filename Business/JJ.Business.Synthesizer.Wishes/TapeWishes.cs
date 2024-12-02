using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Enums;
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
            
            return tapes;
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
                        _stereoTapeActionRunner.RunStereoActions(tapePair);
                    }
                    
                    break;
                
                default:
                    
                    throw new ValueNotSupportedException(GetSpeakers);
            }
        }
    }
    
    internal class StereoTapeMatcher
    {
        private HashSet<Tape> _unprocessedTapes;
        private IList<(Tape, Tape)> _matchedPairs;
        
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
               
            _unprocessedTapes = tapes.ToHashSet();
            _matchedPairs = new List<(Tape, Tape)>();

            // Return single stereo tape
            {
                if (TryAddPair(tapes))
                {
                    return _matchedPairs;
                }
            }
                
            // First grouping:
            // Group by flags, as they definitely should match.
            var groupedByFlags = tapes.GroupBy(x => new
            {
                x.WithPlay,
                x.WithSave,
                x.WithCache,
                x.WithPlayChannel,
                x.WithSaveChannel,
                x.WithCacheChannel
            });
                
            foreach (var group in groupedByFlags)
            {
                TryAddPair(group);

                // Match by delegate method
                var groupedByMethod = group.Where(x => x.Callback != null).GroupBy(x => x.Callback.Method);
                foreach (var subGroup in groupedByMethod)
                {
                    TryAddPair(subGroup);
                }
                    
                // Match by delegate type
                var groupedByClass = group.Where(x => x.Callback != null).GroupBy(x => x.Callback.Method.DeclaringType);
                foreach (var subGroup in groupedByClass)
                {
                    TryAddPair(subGroup);
                }
                    
                // Match by name
                {
                    var groupedByName = group.GroupBy(x => new { x.FallBackName, x.FilePath, x.Signal?.Name });
                    foreach (var subGroup in groupedByName)
                    {
                        TryAddPair(subGroup);
                    }
                }
            }
            
            if (_unprocessedTapes.Count > 0)
            {
                // TODO: Make exception more specific.
                throw new Exception("Some channel tapes could not matched to for a stereo tape.");
            }
                        
            return _matchedPairs;
        }
        
        private bool TryAddPair(IEnumerable<Tape> potentialPair)
        {
            (Tape left, Tape right) tapePair = TryGetTapePair(potentialPair);
            if (tapePair != default)
            {
                _matchedPairs.Add(tapePair);
                _unprocessedTapes.Remove(tapePair.left);
                _unprocessedTapes.Remove(tapePair.right);
                return true;
            }
            
            return false;
        }
        
        private static (Tape left, Tape right) TryGetTapePair(IEnumerable<Tape> potentialPair)
        {
            if (potentialPair == null) throw new ArgumentNullException(nameof(potentialPair));
                
            var array = potentialPair as Tape[] ?? potentialPair.ToArray();
            if (array.Length != 2) return default;
            
            var left = array.FirstOrDefault(x => x.ChannelIndex == 0);
            if (left == null) throw new Exception("There are 2 channel tapes, but none of them are a Left channel (ChannelIndex = 0).");
            
            var right = array.FirstOrDefault(x => x.ChannelIndex == 1);
            if (right == null) throw new Exception("There are 2 channel tapes, but none of them are a Right channel (ChannelIndex = 1).");
            
            var pair = (left, right);
                
            return pair;
        }
    }

    internal class StereoTapeActionRunner
    {
        private SynthWishes _synthWishes;

        public StereoTapeActionRunner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }

        public void RunStereoActions((Tape left, Tape right) tapePair)
        {
            throw new NotImplementedException();
        }
    }
    
    internal class MonoTapeActionRunner
    {
        private readonly SynthWishes _synthWishes;
        
        public MonoTapeActionRunner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public void RunActions(Tape tape)
        {
            Buff replacementBuff = tape.Callback?.Invoke(tape.Buff);
            if (replacementBuff != null) tape.Buff = replacementBuff;

            if (tape.WithSave)
            {
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }

            if (tape.WithPlay)
            {
                _synthWishes.Play(tape.Buff);
            }
        }
    }
    
    internal class ChannelTapeActionRunner
    {
        private readonly SynthWishes _synthWishes;
        
        public ChannelTapeActionRunner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public void RunActions(Tape tape)
        {
            Buff replacementBuff = tape.ChannelCallback?.Invoke(tape.Buff, tape.ChannelIndex);
            if (replacementBuff != null) tape.Buff = replacementBuff;

            if (tape.WithSaveChannel)
            {
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }

            if (tape.WithPlayChannel || _synthWishes.GetPlayAllTapes)
            {
                _synthWishes.Play(tape.Buff);
            }
        }
    }
        
    //internal class TapeActionRunner
    //{
    //    private readonly SpeakerSetupEnum _speakers;
    //   
    //    public TapeActionRunner(SpeakerSetupEnum speakers)
    //    {
    //        _speakers = speakers;
    //        _stereoTapeMatcher = new StereoTapeMatcher();
    //    }
    //   
    //    public void RunActions(IList<Tape> tapes)
    //    {
    //        // Here we have the channel buffs.
    //        // Now we need to associate the left and right channel buffs with each other. 
    //   
    //        switch (_speakers)
    //        {
    //            case SpeakerSetupEnum.Mono:
    //                RunMonoActions(tapes);
    //                return;
    //   
    //            case SpeakerSetupEnum.Stereo:
    //                RunStereoActions(tapes);
    //                break;
    //   
    //            default:
    //                throw new ValueNotSupportedException(_speakers);
    //        }
    //    }
    //}
}
