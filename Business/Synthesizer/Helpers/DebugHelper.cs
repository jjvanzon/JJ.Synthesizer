using System;
using System.Text;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Roslyn.Helpers;
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

        public static string GetDebuggerDisplay(ExtendedVariableInfo variableInfo)
        {
            if (variableInfo == null) throw new NullException(() => variableInfo);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}}", variableInfo.GetType().Name);

            bool nameIsFilledIn = !string.IsNullOrEmpty(variableInfo.VariableNameCamelCase);
            bool valueIsFilledIn = variableInfo.Value.HasValue;
            bool miscPropertiesAreFilledIn = variableInfo.DimensionEnum != DimensionEnum.Undefined ||
                                             variableInfo.ListIndex != 0 ||
                                             variableInfo.DefaultValue.HasValue;
            if (nameIsFilledIn)
            {
                sb.Append(' ');
                sb.Append(variableInfo.VariableNameCamelCase);
            }

            if (nameIsFilledIn && valueIsFilledIn)
            {
                sb.Append(" =");
            }

            if (valueIsFilledIn)
            {
                sb.Append(' ');
                sb.Append(variableInfo.Value);
            }

            if (miscPropertiesAreFilledIn)
            {
                sb.Append(' ');
                sb.Append(new { variableInfo.DimensionEnum, variableInfo.ListIndex, variableInfo.DefaultValue });
            }

            return sb.ToString();
        }
    }
}
