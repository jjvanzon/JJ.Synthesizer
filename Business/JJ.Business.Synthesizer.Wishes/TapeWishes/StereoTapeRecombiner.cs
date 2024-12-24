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
                IsTape = sourceTape.IsTape,
                IsPadded = sourceTape.IsPadded
            };
            
            CloneAction(sourceTape.Play, destTape.Play);
            CloneAction(sourceTape.Save, destTape.Save);
            CloneAction(sourceTape.Intercept, destTape.Intercept);
            CloneAction(sourceTape.PlayChannel, destTape.PlayChannel);
            CloneAction(sourceTape.SaveChannel, destTape.SaveChannel);
            CloneAction(sourceTape.InterceptChannel, destTape.InterceptChannel);
            CloneAction(sourceTape.DiskCache, destTape.DiskCache);
            CloneAction(sourceTape.PlayAllTapes, destTape.PlayAllTapes);
            
            return destTape;
        }
        
        private void RecordStereoTape(Tape stereoTape) 
            => _synthWishes.Record(stereoTape);
    }
}