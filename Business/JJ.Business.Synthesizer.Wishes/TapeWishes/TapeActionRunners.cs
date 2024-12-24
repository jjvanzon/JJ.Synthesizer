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
            if (tape == null) throw new NullException(() => tape);
            if (tape.Channel == null) throw new NullException(() => tape.Channel);
            
            if (!tape.InterceptChannel.On || tape.InterceptChannel.Callback == null) return;
            
            if (tape.InterceptChannel.Done)
            {
                LogAction(tape, Intercept, "Already intercepted");
                return;
            }

            tape.InterceptChannel.Done = true;
            
            LogAction(tape, Intercept);
            
            tape.InterceptChannel.Callback(tape);
        }
       
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            if (!tape.SaveChannel.On) return;
            
            if (tape.SaveChannel.Done)
            {
                LogAction(tape, Save, "Already saved");
                LogOutputFile(tape.FilePathResolved);
                return;
            }

            tape.SaveChannel.Done = true;
            
            tape.Save();
        }

        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);

            if (!(tape.PlayChannel.On || tape.PlayAllTapes)) return;
            
            if (tape.PlayChannel.Done)
            {
                LogAction(tape, Play, "Already played");
                return;
            }

            tape.PlayChannel.Done = true;
            
            tape.Play();
        }
    }
    
    internal class MonoTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            if (!tape.IsMono) return;
            if (!tape.Intercept.On || tape.Intercept.Callback == null) return;
            
            if (tape.Intercept.Done)
            {
                LogAction(tape, Intercept, "Already intercepted");
                return;
            }

            tape.Intercept.Done = true;

            LogAction(tape, Intercept);
            
            tape.Intercept.Callback(tape);
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            if (!tape.IsMono) return;
            if (!tape.Save.On) return;
            
            if (tape.Save.Done)
            {
                LogAction(tape, Save, "Already saved");
                LogOutputFile(tape.FilePathResolved);
                return;
            }

            tape.Save.Done = true;

            tape.Save();
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);

            if (!tape.IsMono) return;
            if (!tape.Play.On) return;
            
            // Skip PlayAllTapes check: Channel already played (identical for Mono).
            //if (!(tape.Play.On || tape.PlayAllTapes)) return;
            
            if (tape.Play.Done)
            {
                LogAction(tape, Play, "Already played");
                return;
            }

            tape.Play.Done = true;

            tape.Play();
        }
    }
    
    internal class StereoTapeActionRunner : TapeActionRunnerBase
    {
        public override void InterceptIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            if (!tape.IsStereo) return;
            if (!tape.Intercept.On || tape.Intercept.Callback == null) return;
            
            if (tape.Intercept.Done)
            {
                LogAction(tape, Intercept, "Already intercepted");
                return;
            }

            tape.Intercept.Done = true;
            
            LogAction(tape, Intercept);
            
            tape.Intercept.Callback(tape);
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            if (!tape.IsStereo) return;
            if (!tape.Save.On) return;
            
            if (tape.Save.Done)
            {
                LogAction(tape, Save, "Already saved");
                LogOutputFile(tape.FilePathResolved);
                return;
            }

            tape.Save.Done = true;
            
            tape.Save();
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            
            if (!tape.IsStereo) return;
            if (!(tape.Play.On || tape.PlayAllTapes)) return;
            
            if (tape.Play.Done)
            {
                LogAction(tape, Play, "Already played");
                return;
            }

            tape.Play.Done = true;

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
            if (tapes == null) throw new NullException(() => tapes);
            tapes.ForEach(InterceptIfNeeded);
        }
        
        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            tapes.ForEach(SaveIfNeeded);
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
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