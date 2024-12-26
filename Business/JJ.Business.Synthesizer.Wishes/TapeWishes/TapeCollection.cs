﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using static System.Math;
using static JJ.Business.Synthesizer.Wishes.JJ_Framework_Common_Wishes.FilledInWishes;
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
            Action<Tape> beforeRecordCallback, Action<Tape> afterRecordCallback, 
            Action<Tape> beforeRecordChannelCallback, Action<Tape> afterRecordChannelCallback,
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
            
            tape.Channel = _synthWishes.GetChannel;
            tape.Config.SamplingRate = _synthWishes.GetSamplingRate;
            tape.Config.Bits = _synthWishes.GetBits;
            tape.Config.Channels = _synthWishes.GetChannels;
            tape.Config.AudioFormat = _synthWishes.GetAudioFormat;
            tape.Config.Interpolation = _synthWishes.GetInterpolation;
            tape.Config.CourtesyFrames = _synthWishes.GetCourtesyFrames;
            
            // Actions
            
            tape.Actions.DiskCache.On = _synthWishes.GetDiskCache;
            tape.Actions.PlayAllTapes.On = _synthWishes.GetPlayAllTapes;
            
            // From Parameters
            
            if (Has(filePath)) tape.FilePathSuggested = filePath;
            if (Has(callerMemberName)) tape.FallBackName = callerMemberName;

            double newDuration = (duration ?? _synthWishes.GetAudioLength).Value;
            tape.Duration = Max(tape.Duration, newDuration);

            tape.Actions.BeforeRecord.Callback = tape.Actions.BeforeRecord.Callback ?? beforeRecordCallback;
            tape.Actions.AfterRecord.Callback = tape.Actions.AfterRecord.Callback ?? afterRecordCallback;
            tape.Actions.BeforeRecordChannel.Callback = tape.Actions.BeforeRecordChannel.Callback ?? beforeRecordChannelCallback; 
            tape.Actions.AfterRecordChannel.Callback = tape.Actions.AfterRecordChannel.Callback ?? afterRecordChannelCallback; 
            
            AssertCallback(tape.Actions.BeforeRecord, beforeRecordCallback);
            AssertCallback(tape.Actions.AfterRecord, afterRecordCallback);
            AssertCallback(tape.Actions.BeforeRecordChannel, beforeRecordChannelCallback);
            AssertCallback(tape.Actions.AfterRecordChannel, afterRecordChannelCallback);

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
        
        /// <summary>
        /// Detect conflicting callback
        /// </summary>
        private void AssertCallback(TapeAction action, Action<Tape> callback)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            
            if (callback != null &&
                action.Callback != null &&
                action.Callback != callback)
            {
                throw new Exception($"Different {action.Name} {nameof(callback)} passed than already assigned to the {nameof(Tape)}!");
            }
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