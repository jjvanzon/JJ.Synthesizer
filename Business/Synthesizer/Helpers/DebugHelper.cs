using System;
using System.Text;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Exceptions;
using JJ.Framework.Reflection;
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

        public static string GetDebuggerDisplay(ValueInfo valueInfo)
        {
            if (valueInfo == null) throw new NullException(() => valueInfo);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}}", valueInfo.GetType().Name);

            bool nameIsFilledIn = !String.IsNullOrEmpty(valueInfo.NameCamelCase);
            bool valueIsFilledIn = valueInfo.Value.HasValue;
            bool miscPropertiesAreFilledIn = valueInfo.DimensionEnum != DimensionEnum.Undefined ||
                                             valueInfo.ListIndex != 0 ||
                                             valueInfo.DefaultValue.HasValue;
            if (nameIsFilledIn)
            {
                sb.Append(' ');
                sb.Append(valueInfo.NameCamelCase);
            }

            if (nameIsFilledIn && valueIsFilledIn)
            {
                sb.Append(" =");
            }

            if (valueIsFilledIn)
            {
                sb.Append(' ');
                sb.Append(valueInfo.Value);
            }

            if (miscPropertiesAreFilledIn)
            {
                sb.Append(' ');
                sb.Append(new { valueInfo.DimensionEnum, valueInfo.ListIndex, valueInfo.DefaultValue });
            }

            return sb.ToString();
        }
    }
}
