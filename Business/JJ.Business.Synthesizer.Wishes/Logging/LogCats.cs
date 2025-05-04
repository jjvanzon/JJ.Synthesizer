using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static class LogCats
    {
        public static readonly string Math      = Name();
        public static readonly string SynthLog  = Name();
        public static readonly string TapeTree  = Name();
        public static readonly string Config    = Name();
        public static readonly string Actions   = Name();
        public static readonly string Operator  = Name();
        public static readonly string Tape      = Name();
        public static readonly string Buff      = Name();
        public static readonly string Out       = Name();
        public static readonly string Sample    = Name();
        public static readonly string File      = Name();
        public static readonly string Memory    = Name();
        public static readonly string Misc      = Name();
    }
}
