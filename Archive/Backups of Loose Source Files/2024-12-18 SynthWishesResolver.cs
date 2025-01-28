using JJ.Business.Synthesizer.Wishes.TapeWishes;
using System;
namespace JJ.Business.Synthesizer.Wishes.Helpers
{
    internal static class SynthWishesResolver
    {
        public static SynthWishes Resolve(Tape tape)
        {
            if (tape == null) throw new ArgumentNullException(nameof(tape));
            
            SynthWishes synthWishes = tape.Signal?.SynthWishes;
            if (synthWishes == null)
            {
                throw new Exception("SynthWishes was null in tape.Signal.SynthWishes.");
            }
            
            return synthWishes;
        }
                
        public static SynthWishes Resolve((Tape Left, Tape Right) tapePair)
        {
            SynthWishes synthWishes = tapePair.Left.Signal?.SynthWishes ?? tapePair.Right.Signal?.SynthWishes;
            if (synthWishes == null)
            {
                throw new Exception(
                    "SynthWishes was expected to be filled in tapePair.Left.Signal.SynthWishes or tapePair.Right.Signal.SynthWishes.");
            }
            return synthWishes;
        }

    }
}
