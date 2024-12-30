using System;
using System.Collections.Generic;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class VersatileActionRunner
    {
        private readonly ActionRunner _actionRunner = new ActionRunner();
            
        // Run in Stages of Processing
        
        public void RunBeforeRecord(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            _actionRunner.InterceptIfNeeded(tape.Actions.BeforeRecordChannel);
            _actionRunner.InterceptIfNeeded(tape.Actions.BeforeRecord);
        }
        
        public void RunAfterRecord(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            _actionRunner.InterceptIfNeeded(tape.Actions.AfterRecordChannel);
            _actionRunner.InterceptIfNeeded(tape.Actions.AfterRecord);
        }
        
        public void RunForPostProcessing(IList<Tape> normalTapes, IList<Tape> stereoTapes)
        {
            _actionRunner.CacheToDiskIfNeeded(normalTapes);
            _actionRunner.CacheToDiskIfNeeded(stereoTapes);

            // Mono and channel-specific variations are run per tape instead.
            _actionRunner.RunAfterRecordIfNeeded(stereoTapes);
            
            _actionRunner.SaveIfNeeded(normalTapes);
            _actionRunner.SaveIfNeeded(stereoTapes);
            
            _actionRunner.PlayForAllTapesIfNeeded(normalTapes);
            _actionRunner.PlayForAllTapesIfNeeded(stereoTapes);
            
            _actionRunner.PlayIfNeeded(normalTapes);
            _actionRunner.PlayIfNeeded(stereoTapes);
        }
    }
    
    internal class ActionRunner
    {
        // Actions Per Item

        public void InterceptIfNeeded(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            if (!action.Active) return;
            action.Done = true;
            action.Intercept();
        }
        
        public void SaveIfNeeded(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            if (!action.Active) return;
            action.Done = true;
            action.Save();
        }

        public void PlayIfNeeded(TapeAction action)
        {
            if (action == null) throw new NullException(() => action);
            if (!action.Active) return;
            action.Done = true;
            action.Play();
        }
            
        public void CacheToDiskIfNeeded(TapeAction action)
        {
            if (!action.Active) return;
            action.Done = true;
            action.Save(action.Tape.Descriptor());
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