using System;
using System.Collections.Generic;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.Helpers.PropertyNameWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class ChannelTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            if (tape.Channel == null) throw new NullException(() => tape.Channel);
            
            if (tape.ChannelCallback == null) return;
            if (tape.ChannelIsIntercepted)
            {
                LogAction(tape, Intercept, "Already intercepted");
                return;
            }

            tape.ChannelIsIntercepted = true;
            
            LogAction(tape, Intercept);
            
            tape.ChannelCallback(tape);
        }
       
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsSaveChannel) return;
            
            if (tape.ChannelIsSaved)
            {
                LogAction(tape, Save, "Already saved");
                LogOutputFile(tape.FilePathResolved);
                return;
            }

            tape.ChannelIsSaved = true;
            
            tape.Save();
        }

        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (!(tape.IsPlayChannel || tape.PlayAllTapes)) return;
            
            if (tape.ChannelIsPlayed)
            {
                LogAction(tape, Play, "Already played");
                return;
            }

            tape.ChannelIsPlayed = true;
            
            tape.Play();
        }
    }
    
    internal class MonoTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsMono) return;
            if (tape.Callback == null) return;
            
            if (tape.IsIntercepted)
            {
                LogAction(tape, Intercept, "Already intercepted");
                return;
            }

            tape.IsIntercepted = true;

            LogAction(tape, Intercept);
            
            tape.Callback(tape);
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsMono) return;
            if (!tape.IsSave) return;
            
            if (tape.IsSaved)
            {
                LogAction(tape, Save, "Already saved");
                LogOutputFile(tape.FilePathResolved);
                return;
            }

            tape.IsSaved = true;

            tape.Save();
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));

            if (!tape.IsMono) return;
            if (!tape.IsPlay) return;
            
            // Skip PlayAllTapes check: Channel already played (identical for Mono).
            //if (!(tape.IsPlay || tape.PlayAllTapes)) return;
            
            if (tape.IsPlayed)
            {
                LogAction(tape, Play, "Already played");
                return;
            }

            tape.IsPlayed = true;

            tape.Play();
        }
    }
    
    internal class StereoTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;
            if (tape.Callback == null) return;
            
            if (tape.IsIntercepted)
            {
                LogAction(tape, Intercept, "Already intercepted");
                return;
            }

            tape.IsIntercepted = true;
            
            LogAction(tape, Intercept);
            
            tape.Callback(tape);
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;
            if (!tape.IsSave) return;
            
            if (tape.IsSaved)
            {
                LogAction(tape, Save, "Already saved");
                LogOutputFile(tape.FilePathResolved);
                return;
            }

            tape.IsSaved = true;
            
            tape.Save();
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            if (!tape.IsStereo) return;
            if (!(tape.IsPlay || tape.PlayAllTapes)) return;
            
            if (tape.IsPlayed)
            {
                LogAction(tape, Play, "Already played");
                return;
            }

            tape.IsPlayed = true;

            tape.Play();
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