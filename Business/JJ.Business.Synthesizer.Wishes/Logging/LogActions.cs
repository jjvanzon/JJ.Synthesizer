using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

namespace JJ.Business.Synthesizer.Wishes.Logging
{
    public static class LogActions
    {
        public static readonly string Start = MemberName();
        public static readonly string Stop = MemberName();
        public static readonly string Create = MemberName();
        public static readonly string Update = MemberName();
        public static readonly string Delete = MemberName();
        public static readonly string Write = MemberName();
    }
}
