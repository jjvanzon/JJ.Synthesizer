using System;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Text_Wishes.StringExtensionWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class TapePadder
    {
        private readonly SynthWishes _synthWishes;
        private readonly TapeCollection _tapes;
        
        public TapePadder(SynthWishes synthWishes, TapeCollection tapes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
            _tapes = tapes ?? throw new ArgumentNullException(nameof(tapes));
        }
        
        /// <inheritdoc cref="docs._applypaddingtotape" />
        public void ApplyPadding(Tape oldTape)
        {
            // Padding only applies to Play and Save actions.
            if (!oldTape.IsPlay && 
                !oldTape.IsPlayChannel &&
                !oldTape.IsSave &&
                !oldTape.IsSaveChannel) return;
            
            // If tape already padded, don't do it again.
            if (oldTape.IsPadding) return;

            // Get variables
            double leadingSilence = _synthWishes.GetLeadingSilence.Value;
            double trailingSilence = _synthWishes.GetTrailingSilence.Value;
            double padding = leadingSilence + trailingSilence;

            // Don't bother if no padding.
            if (padding == 0) return;

            // Apply delay
            string newName = MemberName() + " " + oldTape.Signal.Name;
            FlowNode newNode = _synthWishes.ApplyPaddingDelay(oldTape.Signal).SetName(newName);

            // Add tape
            //if (oldTape.IsTape || oldTape.IsCache)
            Tape newTape = _tapes.GetOrCreate(newNode, oldTape.Duration, oldTape.FilePath);
            newTape.Channel = oldTape.Channel;
            newTape.IsPlay = oldTape.IsPlay;
            newTape.IsSave = oldTape.IsSave;
            newTape.IsCache = oldTape.IsCache;
            newTape.IsPlayChannel = oldTape.IsPlayChannel;
            newTape.IsSaveChannel = oldTape.IsSaveChannel;
            newTape.IsCacheChannel = oldTape.IsCacheChannel;
            newTape.IsPadding = true;
            newTape.FallBackName = oldTape.FallBackName;
            
            // Remove actions from original tape
            oldTape.IsPlay = false;
            oldTape.IsSave = false;
            oldTape.IsPlayChannel = false;
            oldTape.IsSaveChannel = false;

            // Update duration
            FlowNode oldDuration = oldTape.Duration ?? _synthWishes.GetAudioLength;
            newTape.Duration = oldDuration + padding;
            
            Console.WriteLine(
                $"{PrettyTime()} Padding: Tape.Duration = {oldDuration} + " +
                $"{leadingSilence} + {trailingSilence} = {newTape.Duration}");
        }

    }
}
