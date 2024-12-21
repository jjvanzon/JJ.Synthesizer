using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Math;
using static JJ.Business.Synthesizer.Wishes.Helpers.FilledInHelper;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapeCollection : IEnumerable<Tape>
    {
        private readonly SynthWishes _synthWishes;
        private readonly Dictionary<Outlet, Tape> _tapes = new Dictionary<Outlet, Tape>();
        
        public TapeCollection(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        public int Count => _tapes.Count;

        public Tape GetOrCreate(
            FlowNode signal, FlowNode duration, 
            Action<Tape> callback, Action<Tape> channelCallback,
            string filePath, [CallerMemberName] string callerMemberName = null)
        {
            if (signal == null) throw new ArgumentNullException(nameof(signal));
            
            if (!_tapes.TryGetValue(signal, out Tape tape))
            {
                _tapes[signal] = tape = new Tape();
                tape.Signal = signal;
            }

            // Durations
            
            tape.LeadingSilence = _synthWishes.GetLeadingSilence.Value;
            tape.TrailingSilence = _synthWishes.GetTrailingSilence.Value;

            // Audio Properties
            
            tape.SamplingRate = _synthWishes.GetSamplingRate;
            tape.Bits = _synthWishes.GetBits;
            tape.Channel = _synthWishes.GetChannel;
            tape.Channels = _synthWishes.GetChannels;
            tape.AudioFormat = _synthWishes.GetAudioFormat;

            // Options
            
            tape.CacheToDisk = _synthWishes.GetCacheToDisk;
            tape.PlayAllTapes = _synthWishes.GetPlayAllTapes;
            tape.ExtraBufferFrames = _synthWishes.GetExtraBufferFrames;
            
            // From Parameters

            
            if (Has(filePath)) tape.FilePathSuggested = filePath;
            if (Has(callerMemberName)) tape.FallBackName = callerMemberName;

            double newDuration = (duration ?? _synthWishes.GetAudioLength).Value;
            tape.Duration = Max(tape.Duration, newDuration);

            tape.Callback = tape.Callback ?? callback;
            tape.ChannelCallback = tape.ChannelCallback ?? channelCallback; 

            // Detect conflicting callback
            if (callback != null &&
                tape.Callback != null &&
                tape.Callback != callback)
            {
                throw new Exception("Different " + nameof(tape.Callback) + " passed than already assigned to the " + nameof(Tape) + "!");
            }
            
            if (channelCallback != null &&
                tape.ChannelCallback != null &&
                tape.ChannelCallback != channelCallback)
            {
                throw new Exception("Different " + nameof(tape.ChannelCallback) + " passed than already assigned to the " + nameof(Tape) + "!");
            }

            return tape;
        }
        
        public bool IsTape(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            return _tapes.ContainsKey(outlet);
        }
        
        public Tape TryGet(Outlet outlet)
        {
            if (outlet == null) throw new ArgumentNullException(nameof(outlet));
            _tapes.TryGetValue(outlet, out Tape tape);
            return tape;
        }
        
        public void Remove(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            if (tape.Signal == null) throw new NullException(() => tape.Signal);
            
            _tapes.Remove(tape.Signal);
        }
        
        public void Clear() => _tapes.Clear();

        public Tape[] ToArray() => _tapes.Values.ToArray(); // For availability in debugger.

        public IEnumerator<Tape> GetEnumerator()
        {
            foreach (Tape tape in _tapes.Values)
            {
                yield return tape;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}