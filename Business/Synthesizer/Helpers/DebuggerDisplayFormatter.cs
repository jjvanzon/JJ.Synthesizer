using System;
using System.Text;
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

			if (inputDto.Var != null)
			{
				return GetDebuggerDisplay(inputDto.Var);
			}
			else
			{
				return inputDto.Const.ToString();
			}
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

		/// <summary>
		/// Solves the ambiguity with GetDebuggerDisplay(InputDto) that was introduced due to the
		/// implicit conversion from OperatorDtoBase to InputDto.
		/// </summary>
		public static string GetDebuggerDisplay(OperatorDtoBase operatorDto) => GetDebuggerDisplay((IOperatorDto)operatorDto);

		public static string GetDebuggerDisplay(IOperatorDto operatorDto)
		{
			if (operatorDto == null) throw new NullException(() => operatorDto);

			return operatorDto.GetType().Name;
		}

		public static string GetDebuggerDisplay(MidiMappingElementDto dto)
		{
			if (dto == null) throw new ArgumentNullException(nameof(dto));

			var sb = new StringBuilder();

			sb.Append($"{{{nameof(MidiMappingElementDto)}}} ");

			if (dto.StandardDimensionEnum != default ||
			    !string.IsNullOrEmpty(dto.CustomDimensionName) ||
			    dto.FromDimensionValue.HasValue ||
			    dto.TillDimensionValue.HasValue)
			{
				sb.Append($"{new { StandardDimension = dto.StandardDimensionEnum, dto.CustomDimensionName, dto.FromDimensionValue, dto.TillDimensionValue }} ");
			}

			if (dto.MidiControllerCode.HasValue || dto.FromMidiControllerValue.HasValue || dto.TillMidiControllerValue.HasValue)
			{
				sb.Append($"{new { dto.MidiControllerCode, dto.FromMidiControllerValue, dto.TillMidiControllerValue }} ");
			}

			if (dto.FromMidiVelocity.HasValue || dto.TillMidiVelocity.HasValue)
			{
				sb.Append($"{new { dto.FromMidiVelocity, dto.TillMidiVelocity }} ");
			}

			if (dto.FromToneNumber.HasValue || dto.TillToneNumber.HasValue)
			{
				sb.Append($"{new { dto.FromToneNumber, dto.TillToneNumber }} ");
			}

			if (dto.FromMidiNoteNumber.HasValue || dto.TillMidiNoteNumber.HasValue)
			{
				sb.Append($"{new { dto.FromMidiNoteNumber, dto.TillMidiNoteNumber }} ");
			}

			if (dto.FromPosition.HasValue || dto.TillPosition.HasValue)
			{
				sb.Append($"{new { dto.FromPosition, dto.TillPosition }} ");
			}

			return sb.ToString().TrimEnd();
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
	}
}
