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
            if (tape.IsPadding || tape.IsPadded) return null;

            // Get variables
            // TODO: Get Silence variables from Tape
            double leadingSilence = _synthWishes.GetLeadingSilence.Value;
            double trailingSilence = _synthWishes.GetTrailingSilence.Value;
            double padding = leadingSilence + trailingSilence;

            // Don't bother if no padding.
            if (padding == 0) return null;

            Tape paddedTape = tape;

            // Don't make a new tape if it's only trailed by extra silence.
            if (leadingSilence != 0)
            {
                // Apply delay
                FlowNode newNode = _synthWishes.Delay(tape.Signal, leadingSilence).SetName(tape.GetName + " Padded");
                
                // Add tape
                //if (oldTape.IsTape || oldTape.IsIntercept)
                paddedTape = _tapes.GetOrCreate(newNode, tape.Duration, null, null, tape.FilePath);

                // Clone Names
                paddedTape.FallBackName = tape.FallBackName;
                paddedTape.FilePath = tape.FilePath;
                
                // Clone Audio Properties
                paddedTape.SamplingRate = tape.SamplingRate;
                paddedTape.Bits = tape.Bits;
                paddedTape.Channel = tape.Channel;
                paddedTape.Channels = tape.Channels;
                paddedTape.AudioFormat = tape.AudioFormat;

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
                paddedTape.IsPadding = true;
                paddedTape.IsPadded = true;
                
                // Set Options
                paddedTape.CacheToDisk = tape.CacheToDisk;
                paddedTape.ExtraBufferFrames = tape.ExtraBufferFrames;
                
                // Remove Actions from original Tape
                tape.IsPlay = false;
                tape.IsSave = false;
                tape.IsPlayChannel = false;
                tape.IsSaveChannel = false;
                tape.IsPadding = false;
            
                LogAction(paddedTape, "Padding", $"Delay + {leadingSilence} s");
            }

            // Update duration
            var oldDuration = tape.Duration;
            paddedTape.Duration = oldDuration + padding;
            
            LogAction(paddedTape, "Padding", $"AudioLength = {leadingSilence} + {oldDuration} + {trailingSilence} = {paddedTape.Duration}");
            
            return paddedTape;
        }
    }
}
