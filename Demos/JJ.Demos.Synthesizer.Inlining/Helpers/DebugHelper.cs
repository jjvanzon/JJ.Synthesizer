using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithInheritance;
using JJ.Demos.Synthesizer.Inlining.Calculation.Operators.WithStructs;
using JJ.Demos.Synthesizer.Inlining.Dto;
using JJ.Framework.Reflection.Exceptions;

namespace JJ.Demos.Synthesizer.Inlining.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(InletDto inletDto)
        {
            if (inletDto == null) throw new NullException(() => inletDto);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}}", inletDto.GetType().Name);

            if (inletDto.InputOperatorDto == null)
            {
                sb.Append(" no input");
            }
            else
            {
                sb.Append(' ');

                string operatorDebuggerDisplay = GetDebuggerDisplay(inletDto.InputOperatorDto);
                sb.Append(operatorDebuggerDisplay);
            }

            return sb.ToString();
        }

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

        public static string GetDebuggerDisplay(object obj)
        {
            if (obj == null) throw new NullException(() => obj);

            return obj.GetType().Name;
        }
    }
}
