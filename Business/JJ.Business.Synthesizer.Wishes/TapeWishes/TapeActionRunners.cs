using System;
using System.Collections.Generic;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class ChannelTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.Channel == null) throw new NullException(() => tape.Channel);
            
            if (tape.ChannelCallback == null) return;
            if (tape.ChannelIsIntercepted) return;
            tape.ChannelIsIntercepted = true;
            
            LogAction(tape, nameof(SynthWishes.InterceptChannel));
            
            tape.ChannelCallback(tape);
        }
       
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsSaveChannel) return;
            if (tape.ChannelIsSaved) return;
            tape.ChannelIsSaved = true;

            LogAction(tape, nameof(SynthWishes.SaveChannel));
            
            Save(tape);
        }

        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (!(tape.IsPlayChannel || tape.PlayAllTapes)) return;
            if (tape.ChannelIsPlayed) return;
            tape.ChannelIsPlayed = true;
            
            LogPlayAction(tape, nameof(SynthWishes.PlayChannel));
            
            Play(tape);
        }
    }
    
    internal class MonoTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsMono) return;
            if (tape.Callback == null) return;
            if (tape.IsIntercepted) return;
            tape.IsIntercepted = true;

            LogAction(tape, nameof(SynthWishes.Intercept));
            
            tape.Callback(tape);
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsMono) return;
            if (!tape.IsSave) return;
            if (tape.IsSaved) return;
            tape.IsSaved = true;

            LogAction(tape, nameof(Save));
            
            Save(tape);
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (!tape.IsMono) return;
            if (!tape.IsPlay) return;
            // Skip PlayAllTapes check: Channel already played (identical for Mono).
            //if (!(tape.IsPlay || tape.PlayAllTapes)) return;
            if (tape.IsPlayed) return;
            tape.IsPlayed = true;

            LogPlayAction(tape, nameof(Play));
            
            Play(tape);
        }
    }
    
    internal class StereoTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;;
            if (tape.Callback == null) return;
            if (tape.IsIntercepted) return;
            tape.IsIntercepted = true;
            
            LogAction(tape, nameof(SynthWishes.Intercept));
            
            tape.Callback(tape);
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;;
            if (!tape.IsSave) return;
            if (tape.IsSaved) return;
            tape.IsSaved = true;

            LogAction(tape, nameof(Save));
            
            Save(tape);
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;;
            if (!(tape.IsPlay || tape.PlayAllTapes)) return;
            if (tape.IsPlayed) return;
            tape.IsPlayed = true;

            LogPlayAction(tape, nameof(Play));
            
            Play(tape);
        }
    }
    
    
    internal abstract class TapeActionRunnerBase
    {
        // Actions Per Item
        
        public abstract void InterceptIfNeeded(Tape obj);
        public abstract void SaveIfNeeded(Tape obj);
        public abstract void PlayIfNeeded(Tape obj);
        
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
    }
}