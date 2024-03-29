﻿using System;
using System.Text;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation.Operators;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Exceptions.Basic;
using DebuggerDisplayFormatter_Data = JJ.Data.Synthesizer.Helpers.DebuggerDisplayFormatter;

namespace JJ.Business.Synthesizer.Helpers
{
    internal static class DebuggerDisplayFormatter
    {
        public static string GetDebuggerDisplay(InputDto inputDto)
        {
            if (inputDto == null) throw new ArgumentNullException(nameof(inputDto));

            string debuggerDisplay = inputDto.Var != null ? GetDebuggerDisplay(inputDto.Var) : inputDto.Const.ToString();
            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(InputVariableInfo variableInfo)
        {
            if (variableInfo == null) throw new NullException(() => variableInfo);

            var sb = new StringBuilder();

            sb.AppendFormat("{{{0}}}", variableInfo.GetType().Name);

            bool nameIsFilledIn = !string.IsNullOrEmpty(variableInfo.VariableNameCamelCase);
            bool valueIsFilledIn = variableInfo.Value.HasValue;
            bool miscPropertiesAreFilledIn = variableInfo.DimensionEnum != DimensionEnum.Undefined ||
                                             variableInfo.Position != 0 ||
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

            // ReSharper disable once InvertIf
            if (miscPropertiesAreFilledIn)
            {
                sb.Append(' ');
                sb.Append(new { variableInfo.DimensionEnum, variableInfo.Position, variableInfo.DefaultValue });
            }

            return sb.ToString();
        }

        public static string GetDebuggerDisplay(MidiMappingDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            string debuggerDisplay =
                $"{{{nameof(MidiMappingDto)}}} " +
                $"{new { dto.DimensionEnum, dto.CanonicalName, dto.Position, dto.FromDimensionValue, dto.TillDimensionValue }} " +
                $"{new { dto.MidiMappingTypeEnum, dto.FromMidiValue, dto.TillMidiValue, dto.MidiControllerCode }}";

            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(OperatorCalculatorBase operatorCalculatorBase)
        {
            if (operatorCalculatorBase == null) throw new NullException(() => operatorCalculatorBase);

            return operatorCalculatorBase.GetType().Name;
        }

        public static string GetDebuggerDisplay(OperatorWrapper operatorWrapperBase)
        {
            if (operatorWrapperBase == null) throw new NullException(() => operatorWrapperBase);

            string debuggerDisplay = DebuggerDisplayFormatter_Data.GetDebuggerDisplay(operatorWrapperBase.WrappedOperator);

            return debuggerDisplay;
        }

        public static string GetDebuggerDisplay(IOperatorDto operatorDto)
        { 
            if (operatorDto == null) throw new NullException(() => operatorDto);
            return operatorDto.GetType().Name;
        }

        public static string GetDebuggerDisplay([NotNull] DataPropertyParser.ParsedKeyValuePair obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            return $"{obj.Key}={obj.Value}";
        }
    }
}
