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
            CacheIfNeeded(tape);
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
        
        public void CacheIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.Channel == null) throw new NullException(() => tape.Channel);

            if (tape.ChannelCallback != null)
            {
                LogAction(tape, nameof(SynthWishes.CacheChannel));
                
                Buff replacementBuff = tape.ChannelCallback(tape.Buff, tape.Channel.Value);
                if (replacementBuff != null)
                {
                    tape.Buff = replacementBuff;
                }
            }
        }
        
        private void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSaveChannel)
            {
                LogAction(tape, nameof(SynthWishes.SaveChannel));
                
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        private void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            bool playAllTapes = _synthWishes.GetPlayAllTapes;
            
            if (tape.IsPlayChannel || playAllTapes)
            {
                LogAction(tape, nameof(SynthWishes.PlayChannel) + (playAllTapes ? " (" + nameof(SynthWishes.WithPlayAllTapes) + ")"  : null));

                _synthWishes.Play(tape.Buff);
            }
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
            CacheIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
        }
        
        // Run Lists per Action Type
        
        public void CacheIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));

            if (!_synthWishes.IsMono) return;
            
            tapes.ForEach(CacheIfNeeded);
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
        
        private void CacheIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.Callback != null)
            {
                LogAction(tape, nameof(SynthWishes.Cache));

                Buff replacementBuff = tape.Callback(tape.Buff);
                if (replacementBuff != null)
                {
                    tape.Buff = replacementBuff;
                }
            }
        }
        
        private void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSave)
            {
                LogAction(tape, nameof(SynthWishes.Save));

                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        private void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsPlay)
            {
                LogAction(tape, nameof(SynthWishes.Play));
                
                _synthWishes.Play(tape.Buff);
            }
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
            CacheIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
        }
        
        // Run Lists per Action Type
        
        public void CacheIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(CacheIfNeeded);
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
        
        private void CacheIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
        
            if (tape.Callback != null)
            {
                LogAction(tape, nameof(SynthWishes.Cache));
                
                Buff replacementBuff = tape.Callback(tape.Buff);
                if (replacementBuff != null)
                {
                    tape.Buff = replacementBuff;
                }
            }
        }
        
        private void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSave)
            {
                LogAction(tape, nameof(SynthWishes.Save));
                
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        private void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsPlay)
            {
                LogAction(tape, nameof(SynthWishes.Play));
                
                _synthWishes.Play(tape.Buff);
            }
        }
    }
}