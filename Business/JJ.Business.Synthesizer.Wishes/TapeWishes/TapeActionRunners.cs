using System;
using System.Collections.Generic;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class ChannelTapeActionRunner
    {
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
            
            Buff newBuff = tape.ChannelCallback(tape.Buff, tape.Channel.Value);
            tape.Buff = tape.Buff ?? newBuff;
        }
       
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsSaveChannel) return;
            if (tape.ChannelIsSaved) return;
            tape.ChannelIsSaved = true;

            LogAction(tape, nameof(SynthWishes.SaveChannel));
            
            Save(tape);
        }

        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (!(tape.IsPlayChannel || tape.PlayAllTapes)) return;
            if (tape.ChannelIsPlayed) return;
            tape.ChannelIsPlayed = true;

            LogAction(tape, nameof(SynthWishes.PlayChannel) + (tape.PlayAllTapes ? " (" + nameof(Tape.PlayAllTapes) + ")"  : null));
            
            Play(tape);
            
        }
    }
    
    internal class MonoTapeActionRunner
    {
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
        
        public void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsMono) return;
            if (tape.Callback == null) return;
            if (tape.IsIntercepted) return;
            tape.IsIntercepted = true;

            LogAction(tape, nameof(SynthWishes.Intercept));
            
            Buff newBuff = tape.Callback(tape.Buff);
            if (newBuff != null) tape.Buff = newBuff;
        }
        
        public void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsMono) return;
            if (!tape.IsSave) return;
            if (tape.IsSaved) return;
            tape.IsSaved = true;

            LogAction(tape, nameof(SynthWishes.Save));
            
            Save(tape);
            
        }
        
        public void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (!tape.IsMono) return;
            if (!(tape.IsPlay || tape.PlayAllTapes)) return;
            if (tape.IsPlayed) return;
            tape.IsPlayed = true;

            LogAction(tape, nameof(SynthWishes.Play));
            
            Play(tape);
            
        }
    }
    
    internal class StereoTapeActionRunner
    {
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
            
            if (!tape.IsStereo) return;;
            if (tape.Callback == null) return;
            if (tape.IsIntercepted) return;
            tape.IsIntercepted = true;
            
            LogAction(tape, nameof(SynthWishes.Intercept));
            
            Buff newBuff = tape.Callback(tape.Buff);
            if (newBuff != null) tape.Buff = newBuff;
        }
        
        private void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;;
            if (!tape.IsSave) return;
            if (tape.IsSaved) return;
            tape.IsSaved = true;

            LogAction(tape, nameof(SynthWishes.Save));
            
            Save(tape);
        }
        
        private void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;;
            if (!(tape.IsPlay || tape.PlayAllTapes)) return;
            if (tape.IsPlayed) return;
            tape.IsPlayed = true;

            LogAction(tape, nameof(SynthWishes.Play));
            
            Play(tape);
        }
    }
}