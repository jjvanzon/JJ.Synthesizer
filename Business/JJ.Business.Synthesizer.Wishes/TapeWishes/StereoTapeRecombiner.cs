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
            var channelSignals = tapePairs.Select(GetChannelSignals).ToArray();
            var stereoTapes = tapePairs.Select(x => CloneTape(x.Left)).ToArray();
            int count = stereoTapes.Length;

            Parallel.For(0, count, i =>
            {
                LogAction(stereoTapes[i], "Start");
                MaterializeStereoTape(stereoTapes[i], channelSignals[i]);
                LogAction(stereoTapes[i], "Stop");
            });
            
            return stereoTapes;
        }
        
        // Per Item

        //public Tape RecombineChannels((Tape Left, Tape Right) tapePair)
        //{
        //    AssertTapePair(tapePair);
        //    var channelSignals = GetChannelSignals(tapePair);
        //    var stereoTape = CloneTape(tapePair.Left);
        //    MaterializeStereoTape(stereoTape, channelSignals);
        //    return stereoTape;
        //}

        private void AssertTapePair((Tape Left, Tape Right) tapePair)
        {
            if (tapePair.Left == null) throw new NullException(() => tapePair.Left);
            if (tapePair.Left.Buff == null) throw new NullException(() => tapePair.Left.Buff);
            if (tapePair.Right == null) throw new NullException(() => tapePair.Right);
            if (tapePair.Right.Buff == null) throw new NullException(() => tapePair.Right.Buff);
        }
        
        private IList<FlowNode> GetChannelSignals((Tape Left, Tape Right) tapePair)
            => _synthWishes.GetChannelSignals(() => _synthWishes.Sample(tapePair.Left .Buff).Panning(0) +
                                                    _synthWishes.Sample(tapePair.Right.Buff).Panning(1));

        private static Tape CloneTape(Tape tapePrototype) => new Tape
        {
            Duration = tapePrototype.Duration,
            FilePath = tapePrototype.FilePath,
            FallBackName = tapePrototype.FallBackName,
            IsPlay = tapePrototype.IsPlay,
            IsSave = tapePrototype.IsSave,
            IsCache = tapePrototype.IsCache,
            IsPadding = tapePrototype.IsPadding,
            Callback = tapePrototype.Callback
        };
        
        //private void MaterializeStereoTape(Tape stereoTape, IList<FlowNode> channelSignals) 
        //    => stereoTape.Buff = _synthWishes.MakeBuff(
        //        channelSignals, stereoTape.Duration,
        //        inMemory: !_synthWishes.GetCacheToDisk, default, null, null, null);
        
        private void MaterializeStereoTape(Tape stereoTape, IList<FlowNode> channelSignals) 
            => stereoTape.Buff = _synthWishes.MaterializeCache(
                channelSignals, stereoTape.Duration, stereoTape.GetName);
    }
}