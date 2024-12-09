using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Reflection;

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

            Buff replacementBuff = tape.ChannelCallback?.Invoke(tape.Buff, tape.Channel.Value);
            if (replacementBuff != null)
            {
                tape.Buff = replacementBuff;
            }
        }
        
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSaveChannel)
            {
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (tape.IsPlayChannel || _synthWishes.GetPlayAllTapes)
            {
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
        
        public void CacheIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.Callback != null)
            {
                Buff replacementBuff = tape.Callback(tape.Buff);
                if (replacementBuff != null)
                {
                    tape.Buff = replacementBuff;
                }
            }
        }
        
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSave)
            {
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsPlay)
            {
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
        
        public void CacheIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
        
            if (tape.Callback != null)
            {
                Buff replacementBuff = tape.Callback(tape.Buff);
                if (replacementBuff != null)
                {
                    tape.Buff = replacementBuff;
                }
            }
        }
        
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsSave)
            {
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (tape.IsPlay)
            {
                _synthWishes.Play(tape.Buff);
            }
        }
    }
}