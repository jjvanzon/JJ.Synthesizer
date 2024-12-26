using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JJ.Business.Synthesizer.Wishes.TapeWishes
{
    internal static class ActionCloner
    {
        public static void CloneAction(TapeAction source, TapeAction dest)
        {
            dest.On = source.On;
            dest.Done = source.Done;
            dest.Callback = source.Callback;
        }
    }
}
