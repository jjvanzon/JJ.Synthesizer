using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(OperatorCalculatorBase obj)
        {
            if (obj == null) throw new NullException(() => obj);

            return obj.GetType().Name;
        }
    }
}
