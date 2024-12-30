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
        
        public void RunBeforeRecord(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            _channelActionRunner.InterceptIfNeeded(tape.Actions.BeforeRecordChannel);
            _monoActionRunner.InterceptIfNeeded(tape.Actions.BeforeRecord);
        }
        
        public void RunAfterRecord(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            _channelActionRunner.InterceptIfNeeded(tape.Actions.AfterRecordChannel);
            _monoActionRunner.InterceptIfNeeded(tape.Actions.AfterRecord);
        }
        
        public void RunForPostProcessing(IList<Tape> normalTapes, IList<Tape> stereoTapes)
        {
            _channelActionRunner.CacheToDiskIfNeeded(normalTapes);
            _monoActionRunner.CacheToDiskIfNeeded(normalTapes);
            _stereoActionRunner.CacheToDiskIfNeeded(stereoTapes);

            // Mono and channel-specific variations are run per tape instead.
            _stereoActionRunner.RunAfterRecordIfNeeded(stereoTapes);
            
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
        protected override bool ExtraCondition(TapeAction action) => action.Tape.Config.Channel != null && action.Type.ToString().Contains("Channel");
    }
    
    internal class MonoActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.Config.IsMono && !action.Type.ToString().Contains("Channel");
    }
    
    internal class StereoActionRunner : ActionRunnerBase
    {
        protected override bool ExtraCondition(TapeAction action) => action.Tape.Config.IsStereo && !action.Type.ToString().Contains("Channel");
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
                action.Save();
            }
        }

        public void PlayIfNeeded(TapeAction action)
        {
            if (CanPlay(action))
            {
                action.Play();
            }
        }
            
        public void CacheToDiskIfNeeded(TapeAction action)
        {
            if (CanSave(action))
            {
                action.Save(action.Tape.Descriptor());
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
        
        public void RunAfterRecordIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                InterceptIfNeeded(tape.Actions.AfterRecord);
                InterceptIfNeeded(tape.Actions.AfterRecordChannel);
            }
        }
        
        public void SaveIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                SaveIfNeeded(tape.Actions.Save);
                SaveIfNeeded(tape.Actions.SaveChannels);
            }
        }
        
        public void PlayIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                PlayIfNeeded(tape.Actions.Play);
                PlayIfNeeded(tape.Actions.PlayChannels);
            }
        }
        
        public void CacheToDiskIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                CacheToDiskIfNeeded(tape.Actions.DiskCache);
            }
        }
        
        public void PlayForAllTapesIfNeeded(IList<Tape> tapes)
        {
            if (tapes == null) throw new NullException(() => tapes);
            foreach (Tape tape in tapes)
            {
                PlayIfNeeded(tape.Actions.PlayAllTapes);
            }
        }
    }
}