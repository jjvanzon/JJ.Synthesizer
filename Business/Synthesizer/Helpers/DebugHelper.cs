using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Framework.Exceptions;
using DebugHelper_Data = JJ.Data.Synthesizer.Helpers.DebugHelper;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class DebugHelper
    {
        public static string GetDebuggerDisplay(OperatorCalculatorBase operatorCalculatorBase)
        {
            if (operatorCalculatorBase == null) throw new NullException(() => operatorCalculatorBase);

            return operatorCalculatorBase.GetType().Name;
        }

        internal static string GetDebuggerDisplay(OperatorWrapperBase operatorWrapperBase)
        {
            if (operatorWrapperBase == null) throw new NullException(() => operatorWrapperBase);

            string debuggerDisplay = DebugHelper_Data.GetDebuggerDisplay(operatorWrapperBase.WrappedOperator);

            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(OperatorDtoBase operatorDto)
        {
            if (operatorDto == null) throw new NullException(() => operatorDto);

            return operatorDto.GetType().Name;
        }
    }
}
