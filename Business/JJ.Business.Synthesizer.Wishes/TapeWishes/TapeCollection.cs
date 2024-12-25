using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Math;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Common_Wishes.FilledInWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Framework.Reflection.ExpressionHelper;

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
            
            bool isNew = false;
            
            if (!_tapes.TryGetValue(signal, out Tape tape))
            {
                isNew = true;
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
            tape.Interpolation = _synthWishes.GetInterpolation;

            // Options
            
            tape.Actions.DiskCache.On = _synthWishes.GetDiskCache;
            tape.Actions.PlayAllTapes.On = _synthWishes.GetPlayAllTapes;
            tape.CourtesyFrames = _synthWishes.GetCourtesyFrames;
            
            // From Parameters
            
            if (Has(filePath)) tape.FilePathSuggested = filePath;
            if (Has(callerMemberName)) tape.FallBackName = callerMemberName;

            double newDuration = (duration ?? _synthWishes.GetAudioLength).Value;
            tape.Duration = Max(tape.Duration, newDuration);

            tape.Actions.Intercept.Callback = tape.Actions.Intercept.Callback ?? callback;
            tape.Actions.InterceptChannel.Callback = tape.Actions.InterceptChannel.Callback ?? channelCallback; 

            // Detect conflicting callback
            if (callback != null &&
                tape.Actions.Intercept.Callback != null &&
                tape.Actions.Intercept.Callback != callback)
            {
                throw new Exception(
                    "Different " + GetText(() => tape.Actions.Intercept.Callback) + 
                    " passed than already assigned to the " + nameof(Tape) + "!");
            }
            
            if (channelCallback != null &&
                tape.Actions.InterceptChannel.Callback != null &&
                tape.Actions.InterceptChannel.Callback != channelCallback)
            {
                throw new Exception(
                    "Different " + GetText(() => tape.Actions.InterceptChannel.Callback) +
                    " passed than already assigned to the " + nameof(Tape) + "!");
            }

            if (isNew)
            {
                LogAction(tape, "Create");
            }
            else
            {
                LogAction(tape, "Update");
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
            
            LogAction(tape, "Delete", "Replaced by padded");
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