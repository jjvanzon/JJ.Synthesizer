using System;
using JJ.Business.Synthesizer.Wishes.Helpers;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class ChannelTapeActionRunner
    {
        public void RunActions(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            SynthWishes synthWishes = SynthWishesResolver.Resolve(tape);
            
            Buff replacementBuff = tape.ChannelCallback?.Invoke(tape.Buff, tape.ChannelIndex);
            if (replacementBuff != null) tape.Buff = replacementBuff;
            
            if (tape.WithSaveChannel)
            {
                synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
            
            if (tape.WithPlayChannel || synthWishes.GetPlayAllTapes)
            {
                synthWishes.Play(tape.Buff);
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
            
            if (tape.WithSave)
            {
                synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
            
            if (tape.WithPlay)
            {
                synthWishes.Play(tape.Buff);
            }
        }
    }
    
    internal class StereoTapeActionRunner
    {
        public void RunActions(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            SynthWishes synthWishes = SynthWishesResolver.Resolve(tape);
            
            Buff replacementBuff = tape.Callback?.Invoke(tape.Buff);
            if (replacementBuff != null) tape.Buff = replacementBuff;
            
            if (tape.WithSave)
            {
                synthWishes.Save(tape.Buff, tape.FilePath, tape.GetName);
            }
            
            if (tape.WithPlay)
            {
                synthWishes.Play(tape.Buff);
            }
        }
    }
}