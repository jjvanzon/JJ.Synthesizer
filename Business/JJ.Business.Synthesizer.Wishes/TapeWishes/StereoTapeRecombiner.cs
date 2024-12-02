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
            
            Buff stereoBuff = synthWishes.Cache(() => synthWishes.Sample(tapePair.Left.Buff ).Panning(0) +
                                                      synthWishes.Sample(tapePair.Right.Buff).Panning(1));
            var stereoTape = new Tape
            {
                Buff = stereoBuff,
                Duration = tapePair.Left.Duration,
                WithPlay = tapePair.Left.WithPlay,
                WithSave = tapePair.Left.WithSave,
                WithCache = tapePair.Left.WithCache,
                FilePath = tapePair.Left.FilePath,
                Callback = tapePair.Left.Callback,
                FallBackName = tapePair.Left.FallBackName
            };
            
            return stereoTape;
        }
    }
}
