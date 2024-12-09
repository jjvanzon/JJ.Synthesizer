using System;
using System.Collections.Generic;
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
        
        public void RunActions(Tape tape)
        {
            CacheIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
        }
        
        public void SaveIfNeeded(IList<Tape> tapes)
        {
            foreach (Tape tape in tapes)
            {
                SaveIfNeeded(tape);
            }
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            foreach (Tape tape in tapes)
            {
                PlayIfNeeded(tape);
            }
        }
        
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

        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSaveChannel)
            {
                LogAction(tape, nameof(SynthWishes.SaveChannel));
                
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (tape.IsPlayChannel || _synthWishes.GetPlayAllTapes)
            {
                LogAction(tape, nameof(SynthWishes.PlayChannel));
             
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
        
        public void RunActions(IList<Tape> tapes)
        {
            foreach (Tape stereoTape in tapes)
            {
                RunActions(stereoTape);
            }
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
            if (!_synthWishes.IsMono) return;
            
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            
            foreach (Tape tape in tapes)
            {
                CacheIfNeeded(tape);
            }
        }

        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (!_synthWishes.IsMono) return;
                
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            
            foreach (Tape tape in tapes)
            {
                SaveIfNeeded(tape);
            }
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (!_synthWishes.IsMono) return;
            
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            
            foreach (Tape tape in tapes)
            {
                PlayIfNeeded(tape);
            }
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
        
        public void RunActions(IList<Tape> tapes)
        {
            foreach (Tape stereoTape in tapes)
            {
                RunActions(stereoTape);
            }
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
        
            foreach (Tape tape in tapes)
            {
                CacheIfNeeded(tape);
            }
        }

        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            
            foreach (Tape tape in tapes)
            {
                SaveIfNeeded(tape);
            }
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            
            foreach (Tape tape in tapes)
            {
                PlayIfNeeded(tape);
            }
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