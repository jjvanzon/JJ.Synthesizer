using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJ.Business.Synthesizer.Tests.ConfigTests;
using JJ.Framework.Reflection;

namespace JJ.Business.Synthesizer.Tests.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        internal static string GetDebuggerDisplay(FrameCountWishesTests.Case testCase)
        {
            return "{Case} " + testCase;
        }
        
        internal static string GetDebuggerDisplay<T>(
            FrameCountWishesTests.CaseProp<T> caseProp) where T : struct
        {
            return "{CaseProp} " + caseProp;
        }

        internal static string GetDebuggerDisplay<T>(
            FrameCountWishesTests.Values<T> values) where T : struct
        {
            return "{Values} " + values;
        }
    }
}
