using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal static class TapeActionCloner
    {
        public static void Clone(TapeAction source, TapeAction dest)
        {
            dest.On = source.On;
            dest.Done = source.Done;
            dest.Callback = source.Callback;
        }
    }
}
