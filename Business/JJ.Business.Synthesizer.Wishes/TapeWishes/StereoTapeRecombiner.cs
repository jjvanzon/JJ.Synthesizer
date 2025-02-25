using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JJ.Framework.Common;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Wishes.Helpers.CloneWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
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
            stereoTapes.ForEach(Assert);

            int count = stereoTapes.Length;

            Parallel.For(0, count, i =>
            {
                stereoTapes[i].LogAction("Start");
                _synthWishes.Record(stereoTapes[i]);
                stereoTapes[i].LogAction("Stop");
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
            stereoTape.SetSignals(RecombineSignals(tapePair));
            stereoTape.LogAction("Create", "Stereo Recombined");
            return stereoTape;
        }
        
        private IList<FlowNode> RecombineSignals((Tape Left, Tape Right) tapePair)
        {
            string name = tapePair.Left.GetName();
            return new [] { tapePair.Left.Sample.SetName(name), tapePair.Right.Sample.SetName(name) };
        }

        private static Tape CloneStereoTape(Tape sourceTape)
        {
            Tape stereoTape = DeepClone(sourceTape);

            stereoTape.ClearSignals();
            stereoTape.ClearBuff();
            stereoTape.ClearHierarchy();
            stereoTape.ClearChannelActions();
            stereoTape.Config.Channel = default;
            stereoTape.NestingLevel = default;
            
            // HACK: FilePathSuggested could be channel-specific.
            // Replace with SaveAction's path if available.
            if (Has(stereoTape.FilePathSuggested) && 
                Has(stereoTape.Actions.Save.FilePathSuggested))
            {
                stereoTape.FilePathSuggested = stereoTape.Actions.Save.FilePathSuggested;
            }

            return stereoTape;
        }
    }
}