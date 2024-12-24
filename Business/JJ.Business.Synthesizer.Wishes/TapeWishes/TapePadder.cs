using System;
using System.Linq;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.TapeWishes.TapeActionCloner;

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
            if (!tape.Play.On && 
                !tape.PlayChannel.On &&
                !tape.Save.On &&
                !tape.SaveChannel.On) return null;
            
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
            bool hasIntercept        = tape.Intercept       .On && tape.Intercept       .Callback != null;
            bool hasInterceptChannel = tape.InterceptChannel.On && tape.InterceptChannel.Callback != null;
            
            if (!hasIntercept && !hasInterceptChannel)
            {
                _tapes.Remove(tape);
            }
            
            return paddedTape;
        }
        
        private Tape ApplyDelayIfNeeded(Tape tape)
        {
            // Don't make a new tape if it's only trailed by extra silence.
            if (tape.LeadingSilence == 0) return tape;
            
            // Apply delay
            FlowNode newNode = _synthWishes.Delay(tape.Signal, tape.LeadingSilence).SetName(tape.GetName() + " Padded");
            
            // Add tape
            Tape paddedTape = _tapes.GetOrCreate(newNode, _synthWishes[tape.Duration], null, null, tape.FilePathSuggested);
            
            // Clone Names
            paddedTape.FallBackName = tape.FallBackName;
            paddedTape.FilePathSuggested = tape.FilePathSuggested;
            
            // Clone Durations
            paddedTape.Duration = tape.Duration;
            paddedTape.LeadingSilence = tape.LeadingSilence;
            paddedTape.TrailingSilence = tape.TrailingSilence;
            
            // Clone Audio Properties
            paddedTape.SamplingRate = tape.SamplingRate;
            paddedTape.Bits = tape.Bits;
            paddedTape.Channel = tape.Channel;
            paddedTape.Channels = tape.Channels;
            paddedTape.AudioFormat = tape.AudioFormat;
            paddedTape.Interpolation = tape.Interpolation;
            
            // Set Actions
            paddedTape.IsTape = tape.IsTape;
            paddedTape.IsPadded = true;
            CloneAction(tape.Play, paddedTape.Play);
            CloneAction(tape.Save, paddedTape.Save);
            CloneAction(tape.PlayChannel, paddedTape.PlayChannel);
            CloneAction(tape.SaveChannel, paddedTape.SaveChannel);
            //paddedTape.Intercept.On = false;
            //paddedTape.Intercept.Done = false;
            //paddedTape.InterceptChannel.On = false;
            //paddedTape.InterceptChannel.Done = false;
            
            // Set Options
            paddedTape.DiskCache = tape.DiskCache;
            paddedTape.PlayAllTapes = tape.PlayAllTapes;
            paddedTape.CourtesyFrames = tape.CourtesyFrames;
            
            // Remove Actions from original Tape
            tape.Play.On = false;
            tape.Save.On = false;
            tape.PlayChannel.On = false;
            tape.SaveChannel.On = false;
            
            LogAction(paddedTape, "Pad", $"Delay + {tape.LeadingSilence} s");
            
            return paddedTape;
        }
    }
}
