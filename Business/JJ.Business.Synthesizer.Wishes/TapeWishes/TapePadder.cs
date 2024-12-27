using System;
using System.Linq;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
        /// <inheritdoc cref="docs._tapepadder" />
    internal class TapePadder
    {
        private readonly SynthWishes _synthWishes;
        private readonly TapeCollection _tapes;
        
        /// <inheritdoc cref="docs._tapepadder" />
        public TapePadder(SynthWishes synthWishes, TapeCollection tapes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _tapes = tapes ?? throw new ArgumentNullException(nameof(tapes));
        }
        
        /// <inheritdoc cref="docs._tapepadder" />
        public Tape[] PadTapesIfNeeded(Tape[] tapes)
        {
            if (tapes == null) throw new ArgumentNullException(nameof(tapes));
            Tape[] newTapes = tapes.Select(PadIfNeeded)
                                   .Where(x => x != null)
                                   .ToArray();
            return newTapes;
        }
        
        /// <inheritdoc cref="docs._tapepadder" />
        public Tape PadIfNeeded(Tape tape)
        {
            if (tape == null) throw new NullException(() => tape);
            if (tape.Signal == null) throw new NullException(() => tape.Signal);
            
            // Padding only applies to Play and Save actions.
            if (!tape.Actions.Play.On && 
                !tape.Actions.PlayChannels.On &&
                !tape.Actions.Save.On &&
                !tape.Actions.SaveChannels.On) return null;
            
            // If tape already padded, don't do it again.
            if (tape.IsPadded) return null;

            // Get variables
            double padding = tape.LeadingSilence + tape.TrailingSilence;

            // Don't bother if no padding.
            if (padding == 0) return null;

            Tape paddedTape = ApplyDelayIfNeeded(tape);
            
            // Update duration
            var oldDuration = tape.Duration;
            paddedTape.Duration = oldDuration + padding;
            
            LogAction(paddedTape, "Pad", $"AudioLength = {tape.LeadingSilence} + {oldDuration} + {tape.TrailingSilence} = {paddedTape.Duration}");
            
            // Remove original tape if it has no other purposes.
            bool hasIntercept = tape.Actions.BeforeRecord.On        || tape.Actions.BeforeRecord.Callback != null        ||
                                tape.Actions.AfterRecord.On         || tape.Actions.AfterRecord.Callback != null         ||
                                tape.Actions.BeforeRecordChannel.On || tape.Actions.BeforeRecordChannel.Callback != null ||
                                tape.Actions.AfterRecordChannel.On  || tape.Actions.AfterRecordChannel.Callback != null;
            if (!hasIntercept)
            {
                _tapes.Remove(tape);
            }
            
            return paddedTape;
        }
        
        private Tape ApplyDelayIfNeeded(Tape originalTape)
        {
            // Don't make a new tape if it's only trailed by extra silence.
            if (originalTape.LeadingSilence == 0) return originalTape;
            
            // Apply delay
            FlowNode newNode = _synthWishes.Delay(originalTape.Signal, originalTape.LeadingSilence).SetName(originalTape.GetName() + " Padded");
            
            // Add tape
            Tape paddedTape = _tapes.GetOrCreate(newNode, _synthWishes[originalTape.Duration], null, null, null, null, originalTape.FilePathSuggested);
            
            // Copy data from original
            CloneTape(originalTape, paddedTape);
            
            // Clear Buff
            paddedTape.Bytes = default;
            paddedTape.FilePathResolved = default;
            paddedTape.UnderlyingAudioFileOutput = default;
            
            // Restore Signal
            paddedTape.Signal = newNode;
            paddedTape.Signals = default;

            // Set Actions
            paddedTape.IsPadded = true;
            
            // Clear Intercept Actions (Unpadded Tapes are desired for Interception purposes.)
            paddedTape.Actions.BeforeRecord.On = default;
            paddedTape.Actions.BeforeRecord.Done = default;
            paddedTape.Actions.BeforeRecord.Callback = default;
            paddedTape.Actions.AfterRecord.On = default;
            paddedTape.Actions.AfterRecord.Done = default;
            paddedTape.Actions.AfterRecord.Callback = default;
            paddedTape.Actions.BeforeRecordChannel.On = default;
            paddedTape.Actions.BeforeRecordChannel.Done = default;
            paddedTape.Actions.BeforeRecordChannel.Callback = default;
            paddedTape.Actions.AfterRecordChannel.On = default;
            paddedTape.Actions.AfterRecordChannel.Done = default;
            paddedTape.Actions.AfterRecordChannel.Callback = default;
            
            // Clear Actions from Original Tape (Padding desired for Play and Save actions);
            originalTape.Actions.Play.On = false;
            originalTape.Actions.Save.On = false;
            originalTape.Actions.PlayChannels.On = false;
            originalTape.Actions.SaveChannels.On = false;
            
            LogAction(paddedTape, "Pad", $"Delay + {originalTape.LeadingSilence} s");
            
            return paddedTape;
        }
    }
}
