using System;
using System.Collections.Generic;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class VersatileTapeActionRunner
    {
        private readonly MonoTapeActionRunner _monoTapeActionRunner = new MonoTapeActionRunner();
        private readonly StereoTapeActionRunner _stereoTapeActionRunner = new StereoTapeActionRunner();
        private readonly ChannelTapeActionRunner _channelTapeActionRunner = new ChannelTapeActionRunner();
        
        public void RunAfterRecord(Tape tape)
        {
            _channelTapeActionRunner.InterceptIfNeeded(tape);
        }
        
        public void RunForPostProcessing(IList<Tape> normalTapes, IList<Tape> stereoTapes)
        {
            _channelTapeActionRunner.CacheToDiskIfNeeded(normalTapes);
            _monoTapeActionRunner.CacheToDiskIfNeeded(normalTapes);
            _stereoTapeActionRunner.CacheToDiskIfNeeded(stereoTapes);
            
            _monoTapeActionRunner.InterceptIfNeeded(normalTapes);
            _stereoTapeActionRunner.InterceptIfNeeded(stereoTapes);
            
            _channelTapeActionRunner.SaveIfNeeded(normalTapes);
            _monoTapeActionRunner.SaveIfNeeded(normalTapes);
            _stereoTapeActionRunner.SaveIfNeeded(stereoTapes);
            
            _channelTapeActionRunner.PlayForAllTapesIfNeeded(normalTapes);
            _monoTapeActionRunner.PlayForAllTapesIfNeeded(normalTapes);
            _stereoTapeActionRunner.PlayForAllTapesIfNeeded(stereoTapes);
            
            _channelTapeActionRunner.PlayIfNeeded(normalTapes);
            _monoTapeActionRunner.PlayIfNeeded(normalTapes);
            _stereoTapeActionRunner.PlayIfNeeded(stereoTapes);
        }

    }
    
    internal class ChannelTapeActionRunner : TapeActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.Channel != null;
        
        public override void InterceptIfNeeded(Tape tape)
        {
            if (CanIntercept(tape.InterceptChannel))
            {
                tape.InterceptChannel.Callback(tape);
            }
        }
       
        public override void SaveIfNeeded(Tape tape)
        {
            if (CanSave(tape.SaveChannel))
            {
                tape.Save();
            }
        }

        public override void PlayIfNeeded(Tape tape)
        {
            if (CanPlay(tape.PlayChannel))
            {
                tape.Play();
            }
        }
            
        public override void CacheToDiskIfNeeded(Tape tape)
        {
            if (CanSave(tape.DiskCache))
            {
                tape.Save();
            }
        }
        
        public override void PlayForAllTapesIfNeeded(Tape tape)
        {
            if (CanPlay(tape.PlayAllTapes))
            {
                tape.Save();
            }
        }
    }
    
    internal class MonoTapeActionRunner : TapeActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.IsMono;
        
        public override void InterceptIfNeeded(Tape tape)
        {
            if (CanIntercept(tape.Intercept))
            {
                tape.Intercept.Callback(tape);
            }
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (CanSave(tape.Save))
            {
                tape.Save();
            }
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (CanPlay(tape.Play))
            {
                tape.Play();
            }
        }
        
        public override void CacheToDiskIfNeeded(Tape tape)
        {
            if (CanSave(tape.DiskCache))
            {
                tape.Save();
            }
        }
        
        public override void PlayForAllTapesIfNeeded(Tape tape)
        {
            if (CanPlay(tape.PlayAllTapes))
            {
                tape.Save();
            }
        }
    }
    
    internal class StereoTapeActionRunner : TapeActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.IsStereo;
        
        public override void InterceptIfNeeded(Tape tape)
        {
            if (CanIntercept(tape.Intercept)) 
            {
                tape.Intercept.Callback(tape);
            }
        }
        
        public override void SaveIfNeeded(Tape tape)
        {
            if (CanSave(tape.Save))
            {
                tape.Save();
            }
        }
        
        public override void PlayIfNeeded(Tape tape)
        {
            if (CanPlay(tape.Play))
            {
                tape.Play();
            }
        }
    
        public override void CacheToDiskIfNeeded(Tape tape)
        {
            if (CanSave(tape.DiskCache))
            {
                tape.Save();
            }
        }
        
        public override void PlayForAllTapesIfNeeded(Tape tape)
        {
            if (CanPlay(tape.PlayAllTapes))
            {
                tape.Save();
            }
        }
    }
    
    internal abstract class TapeActionRunnerBase
    {
        // Actions Per Item
        
        public abstract void InterceptIfNeeded(Tape tape);
        public abstract void SaveIfNeeded(Tape tape);
        public abstract void PlayIfNeeded(Tape tape);
        public abstract void CacheToDiskIfNeeded(Tape tape);
        public abstract void PlayForAllTapesIfNeeded(Tape tape);

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
        
        public void CacheToDiskIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            tapes.ForEach(CacheToDiskIfNeeded);
        }
        
        public void PlayForAllTapesIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            tapes.ForEach(PlayForAllTapesIfNeeded);
        }
        
        // Run All Action Types
        
        public void RunActions(IList<Tape> tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            tapes.ForEach(RunActions);
        }
        
        public void RunActions(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            InterceptIfNeeded(tape);
            SaveIfNeeded(tape);
            PlayIfNeeded(tape);
            CacheToDiskIfNeeded(tape);
            PlayForAllTapesIfNeeded(tape);
        }
        
        // Condition Checking
        
        // ReSharper disable once UnusedParameter.Global
        protected virtual bool ExtraCondition(TapeAction action) => true;
            
        protected bool CanIntercept(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            
            if (ExtraCondition(action) == false)
            {
                return false;
            }

            if (!action.On)
            {
                return false;
            }

            if (action.Callback == null)
            {
                return false;
            }

            if (action.Done)
            {
                LogAction(action, "Already intercepted");
                return false;
            }
            
            LogAction(action);
            
            return action.Done = true;
        }

        protected bool CanSave(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
                        
            if (ExtraCondition(action) == false)
            {
                return false;
            }

            if (!action.On)
            {
                return false;
            }

            if (action.Done)
            {
                LogAction(action, "Already saved");
                LogOutputFile(action.Tape.FilePathResolved);
                return false;
            }
            
            return action.Done = true;
        }
        
        protected bool CanPlay(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            
            if (ExtraCondition(action) == false)
            {
                return false;
            }

            if (!action.On)
            {
                return false;
            }

            if (action.Done)
            {
                LogAction(action, "Already played");
                return false;
            }
            
            return action.Done = true;
        }
    }
}