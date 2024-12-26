using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Business.Synthesizer.Wishes.SynthWishes;

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
            stereoTapes.ForEach(x => Assert(x, "(Stereo Tape)"));

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
            Tape stereoTape = CloneStereoTape(tapePair.Left);
            stereoTape.Signals = RecombineSignals(tapePair);
            LogAction(stereoTape, "Create", "Stereo Recombined");
            return stereoTape;
        }
        
        private IList<FlowNode> RecombineSignals((Tape Left, Tape Right) tapePair) => _synthWishes.GetChannelSignals(
        () =>
        {
            FlowNode signal = _synthWishes.Sample(tapePair.Left).Panning(0) +
                              _synthWishes.Sample(tapePair.Right).Panning(1);
            
            return signal.SetName(tapePair.Left);
        });
        
        private static Tape CloneStereoTape(Tape sourceTape)
        {
            Tape stereoTape = CloneTape(sourceTape);
            
            stereoTape.Bytes = default;
            stereoTape.FilePathResolved = default;
            stereoTape.UnderlyingAudioFileOutput = default;
            stereoTape.Signal = default;
            stereoTape.Signals = default;
            stereoTape.Channel = default;
            stereoTape.ClearHierarchy();
            stereoTape.NestingLevel = default;
            
            return stereoTape;
        }
    }
}