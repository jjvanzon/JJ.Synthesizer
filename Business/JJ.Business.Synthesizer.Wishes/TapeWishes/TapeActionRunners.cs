using System;

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
}