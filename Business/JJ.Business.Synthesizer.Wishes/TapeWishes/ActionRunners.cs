using System;
using System.Collections.Generic;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class VersatileActionRunner
    {
        private readonly MonoActionRunner _monoActionRunner = new MonoActionRunner();
        private readonly StereoActionRunner _stereoActionRunner = new StereoActionRunner();
        private readonly ChannelActionRunner _channelActionRunner = new ChannelActionRunner();
        
        public void RunAfterRecord(Tape tape)
        {
            _channelActionRunner.InterceptIfNeeded(tape);
        }
        
        public void RunForPostProcessing(IList<Tape> normalTapes, IList<Tape> stereoTapes)
        {
            _channelActionRunner.CacheToDiskIfNeeded(normalTapes);
            _monoActionRunner.CacheToDiskIfNeeded(normalTapes);
            _stereoActionRunner.CacheToDiskIfNeeded(stereoTapes);
            
            _monoActionRunner.InterceptIfNeeded(normalTapes);
            _stereoActionRunner.InterceptIfNeeded(stereoTapes);
            
            _channelActionRunner.SaveIfNeeded(normalTapes);
            _monoActionRunner.SaveIfNeeded(normalTapes);
            _stereoActionRunner.SaveIfNeeded(stereoTapes);
            
            _channelActionRunner.PlayForAllTapesIfNeeded(normalTapes);
            _monoActionRunner.PlayForAllTapesIfNeeded(normalTapes);
            _stereoActionRunner.PlayForAllTapesIfNeeded(stereoTapes);
            
            _channelActionRunner.PlayIfNeeded(normalTapes);
            _monoActionRunner.PlayIfNeeded(normalTapes);
            _stereoActionRunner.PlayIfNeeded(stereoTapes);
        }
    }
    
    internal class ChannelActionRunner : ActionRunnerBase
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
    
    internal class MonoActionRunner : ActionRunnerBase
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
    
    internal class StereoActionRunner : ActionRunnerBase
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
    
    internal abstract class ActionRunnerBase
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