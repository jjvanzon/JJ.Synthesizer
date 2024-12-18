using System;
using System.Collections.Generic;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class ChannelTapeActionRunner
    {
        private readonly SynthWishes _synthWishes;
        
        public ChannelTapeActionRunner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        // Run All Action Types
        
        private void RunActions(Tape tape)
        {
            InterceptIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
        }
        
        // Run Lists per Action Type
        
        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(SaveIfNeeded);
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(PlayIfNeeded);
        }
        
        // Actions Per Item
        
        public void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.Channel == null) throw new NullException(() => tape.Channel);
            
            if (tape.ChannelCallback == null) return;
            if (tape.ChannelIsIntercepted) return;

            tape.ChannelIsIntercepted = true;
            
            LogAction(tape, nameof(SynthWishes.InterceptChannel));
            
            Buff replacementBuff = tape.ChannelCallback(tape.Buff, tape.Channel.Value);
            if (replacementBuff != null) tape.Buff = replacementBuff;
        }
        
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsSaveChannel) return;
            if (tape.ChannelsIsSaved) return;

            tape.ChannelsIsSaved = true;

            LogAction(tape, nameof(SynthWishes.SaveChannel));
            
            _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            bool playAllTapes = _synthWishes.GetPlayAllTapes;
            
            if (!tape.IsPlayChannel && !playAllTapes) return;
            if (tape.ChannelIsPlayed) return;
            tape.ChannelIsPlayed = true;

            LogAction(tape, nameof(SynthWishes.PlayChannel) + (playAllTapes ? " (" + nameof(SynthWishes.WithPlayAllTapes) + ")"  : null));
            
            _synthWishes.Play(tape.Buff);
            
        }
    }
    
    internal class MonoTapeActionRunner
    {
        private readonly SynthWishes _synthWishes;
        
        public MonoTapeActionRunner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        // Run All Action Types
        
        private void RunActions(IList<Tape> tapes)
        {
            tapes.ForEach(RunActions);
        }

        public void RunActions(Tape tape)
        {
            InterceptIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
        }
        
        // Run Lists per Action Type
        
        public void InterceptIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));

            if (!_synthWishes.IsMono) return;
            
            tapes.ForEach(InterceptIfNeeded);
        }

        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));

            if (!_synthWishes.IsMono) return;
            
            tapes.ForEach(SaveIfNeeded);
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));

            if (!_synthWishes.IsMono) return;
            
            tapes.ForEach(PlayIfNeeded);
        }
        
        // Actions Per Item
        
        public void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!_synthWishes.IsMono) return;
            
            if (tape.Callback == null) return;
            if (tape.IsIntercepted) return;
            tape.IsIntercepted = true;

            LogAction(tape, nameof(SynthWishes.Intercept));
            
            Buff replacementBuff = tape.Callback(tape.Buff);
            if (replacementBuff != null) tape.Buff = replacementBuff;
            
        }
        
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!_synthWishes.IsMono) return;
            
            if (!tape.IsSave) return;
            if (tape.IsSaved) return;
            tape.IsSaved = true;

            LogAction(tape, nameof(SynthWishes.Save));
            
            _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!_synthWishes.IsMono) return;
            
            if (!tape.IsPlay) return;
            if (tape.IsPlayed) return;
            tape.IsPlayed = true;

            LogAction(tape, nameof(SynthWishes.Play));
            
            _synthWishes.Play(tape.Buff);
            
        }
    }
    
    internal class StereoTapeActionRunner
    {
        private readonly SynthWishes _synthWishes;

        public StereoTapeActionRunner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        // Run All Action Types
        
        private void RunActions(IList<Tape> tapes)
        {
            tapes.ForEach(RunActions);
        }
        
        private void RunActions(Tape tape)
        {
            InterceptIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
        }
        
        // Run Lists per Action Type
        
        public void InterceptIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(InterceptIfNeeded);
        }

        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(SaveIfNeeded);
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(PlayIfNeeded);
        }
        
        // Actions Per Item
        
        private void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.Callback == null) return;
            if (tape.IsIntercepted) return;
            tape.IsIntercepted = true;
            
            LogAction(tape, nameof(SynthWishes.Intercept));
            
            Buff replacementBuff = tape.Callback(tape.Buff);
            if (replacementBuff != null) tape.Buff = replacementBuff;
        }
        
        private void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsSave) return;
            if (tape.IsSaved) return;
            tape.IsSaved = true;

            LogAction(tape, nameof(SynthWishes.Save));
            
            _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
        }
        
        private void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsPlay) return;
            if (tape.IsPlayed) return;

            LogAction(tape, nameof(SynthWishes.Play));
            
            _synthWishes.Play(tape.Buff);
        }
    }
}