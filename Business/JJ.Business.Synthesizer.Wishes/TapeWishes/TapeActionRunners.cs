using System;
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
        
        public void CacheIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.Channel == null) throw new NullException(() => tape.Channel);

            Buff replacementBuff = tape.ChannelCallback?.Invoke(tape.Buff, tape.Channel.Value);
            if (replacementBuff != null) tape.Buff = replacementBuff;
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
        public void RunActions(Tape tape)
        {
            SynthWishes synthWishes = SynthWishesResolver.Resolve(tape);
            
            Buff replacementBuff = tape.Callback?.Invoke(tape.Buff);
            if (replacementBuff != null) tape.Buff = replacementBuff;
            
            if (tape.IsSave)
            {
                synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
            
            if (tape.IsPlay)
            {
                synthWishes.Play(tape.Buff);
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

        public void RunActions(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            Buff replacementBuff = tape.Callback?.Invoke(tape.Buff);
            if (replacementBuff != null) tape.Buff = replacementBuff;
            
            if (tape.IsSave)
            {
                _synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
            
            if (tape.IsPlay)
            {
                _synthWishes.Play(tape.Buff);
            }
        }
    }
}