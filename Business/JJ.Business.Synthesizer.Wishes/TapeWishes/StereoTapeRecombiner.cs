using System;
using JJ.Business.Synthesizer.Wishes.Helpers;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal class StereoTapeRecombiner
    {
        public Tape RecombineChannels((Tape Left, Tape Right) tapePair)
        {
            if (tapePair.Left == null) throw new NullException(() => tapePair.Left);
            if (tapePair.Left.Buff == null) throw new NullException(() => tapePair.Left.Buff);
            if (tapePair.Right == null) throw new NullException(() => tapePair.Right);
            if (tapePair.Right.Buff == null) throw new NullException(() => tapePair.Right.Buff);
            
            SynthWishes synthWishes = SynthWishesResolver.Resolve(tapePair);
            
            Buff stereoBuff = synthWishes.Cache(
                () => synthWishes.Sample(tapePair.Left.Buff ).Panning(0) +
                      synthWishes.Sample(tapePair.Right.Buff).Panning(1),
                      tapePair.Left.Duration);
            
            var stereoTape = new Tape
            {
                Buff = stereoBuff,
                Duration = tapePair.Left.Duration,
                FilePath = tapePair.Left.FilePath,
                FallBackName = tapePair.Left.FallBackName,
                IsPlay = tapePair.Left.IsPlay,
                IsSave = tapePair.Left.IsSave,
                IsCache = tapePair.Left.IsCache,
                IsPadding = tapePair.Left.IsPadding,
                Callback = tapePair.Left.Callback
            };
            
            return stereoTape;
        }
    }
}
