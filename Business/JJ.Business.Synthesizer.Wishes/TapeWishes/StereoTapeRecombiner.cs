using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class StereoTapeRecombiner
    {
        private readonly SynthWishes _synthWishes;
        
        public StereoTapeRecombiner(SynthWishes synthWishes)
        {
            _synthWishes = synthWishes ?? throw new ArgumentNullException(nameof(synthWishes));
        }
        
        // Whole List
        
        public IList<Tape> RecombineChannelsConcurrent(IList<(Tape Left, Tape Right)> tapePairs)
        {
            if (tapePairs == null) throw new ArgumentNullException(nameof(tapePairs));
            if (tapePairs.Count == 0) return new List<Tape>();
            
            tapePairs.ForEach(AssertTapePair);
            Tape[] stereoTapes = tapePairs.Select(CreateStereoTape).ToArray();
            
            int count = stereoTapes.Length;

            Parallel.For(0, count, i =>
            {
                LogAction(stereoTapes[i], "Start");
                RecordStereoTape(stereoTapes[i]);
                LogAction(stereoTapes[i], "Stop");
            });
            
            return stereoTapes;
        }
        
        // Per Item

        private void AssertTapePair((Tape Left, Tape Right) tapePair)
        {
            if (tapePair.Left == null) throw new NullException(() => tapePair.Left);
            if (tapePair.Right == null) throw new NullException(() => tapePair.Right);
        }

        private Tape CreateStereoTape((Tape Left, Tape Right) tapePair)
        {
            Tape stereoTape = CloneTape(tapePair.Left);
            stereoTape.Signals = GetChannelSignals(tapePair);
            return stereoTape;
        }
        
        private IList<FlowNode> GetChannelSignals((Tape Left, Tape Right) tapePair) => _synthWishes.GetChannelSignals(
        () =>
        {
            FlowNode signal = _synthWishes.Sample(tapePair.Left).Panning(0) +
                              _synthWishes.Sample(tapePair.Right).Panning(1);
            
            return signal.SetName(tapePair.Left);
        });
        
        private static Tape CloneTape(Tape tapePrototype) => new Tape
        {
            // Names
            FilePathSuggested = tapePrototype.FilePathSuggested,
            FallBackName      = tapePrototype.FallBackName,

            // Durations
            Duration        = tapePrototype.Duration,
            LeadingSilence  = tapePrototype.LeadingSilence,
            TrailingSilence = tapePrototype.TrailingSilence,

            // Audio Properties
            SamplingRate  = tapePrototype.SamplingRate,
            Bits          = tapePrototype.Bits,
            Channels      = tapePrototype.Channels,
            AudioFormat   = tapePrototype.AudioFormat,
            Interpolation = tapePrototype.Interpolation,
            
            // Actions
            IsTape               = tapePrototype.IsTape,
            IsPlay               = tapePrototype.IsPlay,
            IsPlayed             = tapePrototype.IsPlayed,
            IsSave               = tapePrototype.IsSave,
            IsSaved              = tapePrototype.IsSaved,
            IsIntercept          = tapePrototype.IsIntercept,
            IsIntercepted        = tapePrototype.IsIntercepted,
            IsPlayChannel        = tapePrototype.IsPlayChannel,
            ChannelIsPlayed      = tapePrototype.ChannelIsPlayed,
            IsSaveChannel        = tapePrototype.IsSaveChannel,
            ChannelIsSaved       = tapePrototype.ChannelIsSaved,
            IsInterceptChannel   = tapePrototype.IsInterceptChannel,
            ChannelIsIntercepted = tapePrototype.ChannelIsIntercepted,
            IsPadded             = tapePrototype.IsPadded,
            Callback             = tapePrototype.Callback,
            
            // Options
            DiskCache = tapePrototype.DiskCache,
            PlayAllTapes = tapePrototype.PlayAllTapes,
            CourtesyFrames = tapePrototype.CourtesyFrames,
        };
        
        private void RecordStereoTape(Tape stereoTape) 
            => _synthWishes.Record(stereoTape);
    }
}