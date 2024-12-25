using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.TapeWishes.ActionCloner;

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
                _synthWishes.Record(stereoTapes[i]);
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
        
        private static Tape CloneTape(Tape sourceTape)
        {
            var destTape = new Tape
            {
                // Names
                FilePathSuggested = sourceTape.FilePathSuggested,
                FallBackName = sourceTape.FallBackName,

                // Durations
                Duration = sourceTape.Duration,
                LeadingSilence = sourceTape.LeadingSilence,
                TrailingSilence = sourceTape.TrailingSilence,

                // Audio Properties
                SamplingRate = sourceTape.SamplingRate,
                Bits = sourceTape.Bits,
                Channels = sourceTape.Channels,
                AudioFormat = sourceTape.AudioFormat,
                Interpolation = sourceTape.Interpolation,

                // Options
                CourtesyFrames = sourceTape.CourtesyFrames,

                // Actions
                IsPadded = sourceTape.IsPadded,
                IsTape = sourceTape.IsTape
            };

            CloneAction(sourceTape.Actions.Play, destTape.Actions.Play);
            CloneAction(sourceTape.Actions.Save, destTape.Actions.Save);
            CloneAction(sourceTape.Actions.BeforeRecord, destTape.Actions.BeforeRecord);
            CloneAction(sourceTape.Actions.AfterRecord, destTape.Actions.AfterRecord);
            CloneAction(sourceTape.Actions.PlayChannels, destTape.Actions.PlayChannels);
            CloneAction(sourceTape.Actions.SaveChannels, destTape.Actions.SaveChannels);
            CloneAction(sourceTape.Actions.BeforeRecordChannel, destTape.Actions.BeforeRecordChannel);
            CloneAction(sourceTape.Actions.AfterRecordChannel, destTape.Actions.AfterRecordChannel);
            CloneAction(sourceTape.Actions.DiskCache, destTape.Actions.DiskCache);
            CloneAction(sourceTape.Actions.PlayAllTapes, destTape.Actions.PlayAllTapes);
            
            LogAction(destTape, "Create", "Stereo Recombined");
            
            return destTape;
        }
    }
}