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
            
        // Run in Stages of Processing
        
        public void RunAfterRecord(Tape tape)
        {
            _channelActionRunner.InterceptIfNeeded(tape.InterceptChannel);
        }
        
        public void RunForPostProcessing(IList<Tape> normalTapes, IList<Tape> stereoTapes)
        {
            _channelActionRunner.CacheToDiskIfNeeded(normalTapes);
            _monoActionRunner.CacheToDiskIfNeeded(normalTapes);
            _stereoActionRunner.CacheToDiskIfNeeded(stereoTapes);

            // Channel-specific variation is run per tape instead.
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
        protected override bool ExtraCondition(TapeAction action) => action.Tape.Channel != null && action.Name.Contains("Channel");
    }
    
    internal class MonoActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.IsMono;
    }
    
    internal class StereoActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.IsStereo;
    }
    
    internal abstract class ActionRunnerBase
    {
        // Actions Per Item

        public void InterceptIfNeeded(TapeAction action)
        {
            if (CanIntercept(action))
            {
                action.Callback(action.Tape);
            }
        }
       
        public void SaveIfNeeded(TapeAction action)
        {
            if (CanSave(action))
            {
                action.Tape.Save();
            }
        }

        public void PlayIfNeeded(TapeAction action)
        {
            if (CanPlay(action))
            {
                action.Tape.Play();
            }
        }
            
        public void CacheToDiskIfNeeded(TapeAction action)
        {
            if (CanSave(action))
            {
                action.Tape.Save(action.Tape.Descriptor());
            }
        }
        
        public void PlayForAllTapesIfNeeded(TapeAction action)
        {
            if (CanPlay(action))
            {
                action.Tape.Play();
            }
        }
        
        // Condition Checking
        
        protected virtual bool ExtraCondition(TapeAction action) => true;
        
        private bool CanIntercept(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            
            if (!ExtraCondition(action) || !action.On || action.Callback == null) return false;
            
            if (action.Done)
            {
                LogAction(action, "Already Intercepted");
                return false;
            }
            
            LogAction(action);
            
            return action.Done = true;
        }
        
        private bool CanSave(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
                        
            if (!ExtraCondition(action) || !action.On) return false;
            
            if (action.Done)
            {
                LogAction(action, "Already Saved");
                LogOutputFile(action.Tape.FilePathResolved);
                return false;
            }
            
            return action.Done = true;
        }
        
        private bool CanPlay(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            
            if (!ExtraCondition(action) || !action.On) return false;

            if (action.Done)
            {
                LogAction(action, "Already Played");
                return false;
            }
            
            return action.Done = true;
        }
        
        // Run Lists per Action Type
        
        public void InterceptIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                InterceptIfNeeded(tape.Intercept);
                InterceptIfNeeded(tape.InterceptChannel);
            }
        }
        
        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                SaveIfNeeded(tape.Save);
                SaveIfNeeded(tape.SaveChannel);
            }
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                PlayIfNeeded(tape.Play);
                PlayIfNeeded(tape.PlayChannel);
            }
        }
        
        public void CacheToDiskIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                CacheToDiskIfNeeded(tape.DiskCache);
            }
        }
        
        public void PlayForAllTapesIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                PlayForAllTapesIfNeeded(tape.PlayAllTapes);
            }
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
            
            InterceptIfNeeded(tape.Intercept);
            InterceptIfNeeded(tape.InterceptChannel);
            SaveIfNeeded(tape.Save);
            SaveIfNeeded(tape.SaveChannel);
            PlayIfNeeded(tape.Play);
            PlayIfNeeded(tape.PlayChannel);
            CacheToDiskIfNeeded(tape.DiskCache);
            PlayForAllTapesIfNeeded(tape.PlayAllTapes);
        }
    }
}