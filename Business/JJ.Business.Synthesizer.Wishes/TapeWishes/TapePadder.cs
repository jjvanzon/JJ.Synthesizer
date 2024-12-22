using System;
using System.Linq;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

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
            if (!tape.IsPlay && 
                !tape.IsPlayChannel &&
                !tape.IsSave &&
                !tape.IsSaveChannel) return null;
            
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
            
            LogAction(paddedTape, "Padding", $"AudioLength = {tape.LeadingSilence} + {oldDuration} + {tape.TrailingSilence} = {paddedTape.Duration}");
            
            // Remove original tape if it has no other purposes.
            if (!tape.IsIntercept && !tape.IsInterceptChannel && tape.Callback == null && tape.ChannelCallback == null)
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
            paddedTape.IsPlay = tape.IsPlay;
            paddedTape.IsPlayed = tape.IsPlayed;
            paddedTape.IsSave = tape.IsSave;
            paddedTape.IsSaved = tape.IsSaved;
            paddedTape.IsIntercept = false;
            paddedTape.IsIntercepted = false;
            paddedTape.IsPlayChannel = tape.IsPlayChannel;
            paddedTape.ChannelIsPlayed = tape.ChannelIsPlayed;
            paddedTape.IsSaveChannel = tape.IsSaveChannel;
            paddedTape.ChannelIsSaved = tape.ChannelIsSaved;
            paddedTape.IsInterceptChannel = false;
            paddedTape.ChannelIsIntercepted = false;
            paddedTape.IsPadded = true;
            
            // Set Options
            paddedTape.DiskCache = tape.DiskCache;
            paddedTape.PlayAllTapes = tape.PlayAllTapes;
            paddedTape.CourtesyFrames = tape.CourtesyFrames;
            
            // Remove Actions from original Tape
            tape.IsPlay = false;
            tape.IsSave = false;
            tape.IsPlayChannel = false;
            tape.IsSaveChannel = false;
            
            LogAction(paddedTape, "Padding", $"Delay + {tape.LeadingSilence} s");
            
            return paddedTape;
        }
    }
}
