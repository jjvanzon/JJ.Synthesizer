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
            if (tapePair.Left.Buff == null) throw new NullException(() => tapePair.Left.Buff);
            if (tapePair.Right == null) throw new NullException(() => tapePair.Right);
            if (tapePair.Right.Buff == null) throw new NullException(() => tapePair.Right.Buff);
        }

        //public Tape RecombineChannels((Tape Left, Tape Right) tapePair)
        //{
        //    AssertTapePair(tapePair);
        //    var channelSignals = GetChannelSignals(tapePair);
        //    var stereoTape = CloneTape(tapePair.Left);
        //    RecordStereoTape(stereoTape, channelSignals);
        //    return stereoTape;
        //}

        private Tape CreateStereoTape((Tape Left, Tape Right) tapePair)
        {
            IList<FlowNode> channelSignals = GetChannelSignals(tapePair);
            Tape stereoTape = CloneTape(tapePair.Left);
            stereoTape.Signals = channelSignals;
            return stereoTape;
        }
        
        private IList<FlowNode> GetChannelSignals((Tape Left, Tape Right) tapePair) => _synthWishes.GetChannelSignals(
        () =>
        {
            FlowNode signal = _synthWishes.Sample(tapePair.Left.Buff).Panning(0) +
                              _synthWishes.Sample(tapePair.Right.Buff).Panning(1);
            
            return signal.SetName(tapePair.Left);
        });
        
        private static Tape CloneTape(Tape tapePrototype) => new Tape
        {
            // Durations
            Duration = tapePrototype.Duration,
            LeadingSilence = tapePrototype.LeadingSilence,
            TrailingSilence = tapePrototype.TrailingSilence,

            // Audio Properties
            SamplingRate = tapePrototype.SamplingRate,
            Bits = tapePrototype.Bits,
            Channels = tapePrototype.Channels,
            AudioFormat = tapePrototype.AudioFormat,
            
            // Names
            FilePath = tapePrototype.FilePath,
            FallBackName = tapePrototype.FallBackName,
            
            // Actions
            IsPlay = tapePrototype.IsPlay,
            IsSave = tapePrototype.IsSave,
            IsIntercept = tapePrototype.IsIntercept,
            IsPadding = tapePrototype.IsPadding,
            Callback = tapePrototype.Callback,
            
            // Options
            CacheToDisk = tapePrototype.CacheToDisk,
            ExtraBufferFrames = tapePrototype.ExtraBufferFrames,
        };
        
        private void RecordStereoTape(Tape stereoTape) 
            => stereoTape.Buff = _synthWishes.Record(stereoTape.Signals, stereoTape.Duration, stereoTape.GetName);
    }
}