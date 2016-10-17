using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(OperatorDto operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            return operatorDto.GetType().Name;
        }

        public static string GetDebuggerDisplay(OperatorCalculatorBase operatorCalculator)
        {
            if (operatorCalculator == null) throw new NullException(() => operatorCalculator);

            return operatorCalculator.GetType().Name;
        }
    }
}
