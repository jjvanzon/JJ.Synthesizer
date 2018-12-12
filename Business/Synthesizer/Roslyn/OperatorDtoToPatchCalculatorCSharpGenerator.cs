using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Calculation.Arrays;
using JJ.Business.Synthesizer.Calculation.Patches;
using JJ.Business.Synthesizer.Calculation.Random;
using JJ.Business.Synthesizer.Dto;
using JJ.Business.Synthesizer.Dto.Operators;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Roslyn.Helpers;
using JJ.Framework.Exceptions.Basic;
using JJ.Framework.Mathematics;
using JJ.Framework.Text;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.Synthesizer.Roslyn
{
	internal class OperatorDtoToPatchCalculatorCSharpGenerator
	{
		private const string TAB_STRING = "	";
		private const int RAW_CALCULATION_CODE_INDENT_LEVEL = 4;
		private const int RAW_RESET_CODE_INDENT_LEVEL = 3;

		private readonly int _channelCount;
		private readonly int _channelIndex;

		public OperatorDtoToPatchCalculatorCSharpGenerator(int channelCount, int channelIndex)
		{
			_channelCount = channelCount;
			_channelIndex = channelIndex;
		}

		public OperatorDtoToPatchCalculatorCSharpGeneratorResult Execute(IOperatorDto dto, string generatedNameSpace, string generatedClassName)
		{
			if (string.IsNullOrEmpty(generatedNameSpace)) throw new NullOrEmptyException(() => generatedNameSpace);
			if (string.IsNullOrEmpty(generatedClassName)) throw new NullOrEmptyException(() => generatedClassName);

			// Build up Method Bodies
			var visitor = new OperatorDtoToRawCSharpVisitor(
				RAW_CALCULATION_CODE_INDENT_LEVEL,
				RAW_RESET_CODE_INDENT_LEVEL);
			OperatorDtoToCSharpVisitorResult visitorResult = visitor.Execute(dto);

			// Build up Code File
			var sb = new StringBuilderWithIndentation(TAB_STRING);

			sb.AppendLine("using System;");
			sb.AppendLine("using " + typeof(ArrayCalculatorBase).Namespace + ";");
			sb.AppendLine("using " + typeof(ArrayDto).Namespace + ";");
			sb.AppendLine("using " + typeof(CalculationHelper).Namespace + ";");
			sb.AppendLine("using " + typeof(Dictionary<,>).Namespace + ";");
			sb.AppendLine("using " + typeof(DimensionEnum).Namespace + ";");
			sb.AppendLine("using " + typeof(MathHelper).Namespace + ";");
			sb.AppendLine("using " + typeof(MethodImplAttribute).Namespace + ";");
			sb.AppendLine("using " + typeof(NameHelper).Namespace + ";");
			sb.AppendLine("using " + typeof(NoiseCalculatorHelper).Namespace + ";");
			sb.AppendLine("using " + typeof(PatchCalculatorBase).Namespace + ";");
			sb.AppendLine("using " + typeof(RandomCalculatorHelper).Namespace + ";");
			sb.AppendLine("using " + typeof(SineCalculator).Namespace + ";");
			sb.AppendLine();
			sb.AppendLine($"namespace {generatedNameSpace}");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine($"public class {generatedClassName} : PatchCalculatorBase");
				sb.AppendLine("{");
				sb.Indent();
				{
					// Fields
					sb.AppendLine("// Fields");
					sb.AppendLine();
					AppendFields(sb, visitorResult);
					sb.AppendLine();

					// Constructor
					sb.AppendLine("// Constructor");
					sb.AppendLine();
					AppendConstructor(sb, visitorResult, generatedClassName);
					sb.AppendLine();

					// Calculate Method
					sb.AppendLine("// Calculate");
					sb.AppendLine();
					AppendCalculateMethod(sb, visitorResult);
					sb.AppendLine();

					// Reset Method
					sb.AppendLine("// Reset");
					sb.AppendLine();
					AppendResetMethod(sb, visitorResult);
					sb.AppendLine();

					// Values
					sb.AppendLine("// Values");
					sb.AppendLine();
					AppendSetValue_ByPosition(sb, visitorResult);
					sb.AppendLine();
					AppendSetValue_ByDimensionEnum(sb, visitorResult);
					sb.AppendLine();
					AppendSetValue_ByName(sb, visitorResult);
					sb.AppendLine();
					AppendSetValue_ByDimensionEnumAndPosition(sb, visitorResult);
					sb.AppendLine();
					AppendSetValue_ByNameAndPosition(sb, visitorResult);
					sb.Unindent();
				}
				sb.AppendLine("}");
				sb.Unindent();
			}
			sb.AppendLine("}");

			string generatedCode = sb.ToString();

			var result = new OperatorDtoToPatchCalculatorCSharpGeneratorResult
			{
				GeneratedCode = generatedCode,
				ArrayCalculationInfos = visitorResult.ArrayCalculationInfos
			};

			return result;
		}

		private void AppendFields(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<string> doubleInstanceVariableNamesCamelCase = GetDoubleInstanceVariableNamesCamelCase(visitorResult);

			foreach (string variableName in doubleInstanceVariableNamesCamelCase)
			{
				sb.AppendLine($"private double _{variableName};");
			}

			foreach (ArrayCalculationInfo variableInfo in visitorResult.ArrayCalculationInfos)
			{
				sb.AppendLine($"private readonly {variableInfo.TypeName} _{variableInfo.NameCamelCase};");
			}

			// ReSharper disable once InvertIf
			foreach (DoubleArrayVariableInfo variableInfo in visitorResult.LongLivedDoubleArrayVariableInfos)
			{
				sb.AppendLine($"private readonly double[] _{variableInfo.NameCamelCase} = new double[{variableInfo.ArrayLength}];");
			}
		}

		private void AppendConstructor(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult, string generatedClassName)
		{
			sb.AppendLine($"public {generatedClassName}(");
			sb.Indent();
			{
				sb.AppendLine("int samplingRate,");
				sb.AppendLine("int channelCount,");
				sb.AppendLine("int channelIndex,");
				sb.AppendLine("Dictionary<string, ArrayDto> arrayDtoDictionary)");
				sb.Unindent();
			}
			sb.Indent();
			{
				sb.AppendLine(": base(samplingRate, channelCount, channelIndex)");
				sb.Unindent();
			}
			sb.AppendLine("{");
			sb.Indent();
			{
				if (visitorResult.InputVariableInfos.Any())
				{
					foreach (InputVariableInfo inputVariableInfo in visitorResult.InputVariableInfos)
					{
						sb.AppendLine($"_{inputVariableInfo.VariableNameCamelCase} = {CompilationHelper.FormatValue(inputVariableInfo.Value ?? 0.0)};");
					}
					sb.AppendLine();
				}

				if (visitorResult.ArrayCalculationInfos.Any())
				{
					foreach (ArrayCalculationInfo variableInfo in visitorResult.ArrayCalculationInfos)
					{
						string name = variableInfo.NameCamelCase;
						string type = variableInfo.TypeName;

						sb.AppendLine($"_{name} = ({type})ArrayCalculatorFactory.CreateArrayCalculator(");
						sb.Indent();
						{
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].Array,");
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].Rate,");
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].MinPosition,");
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].ValueBefore,");
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].ValueAfter,");
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].InterpolationTypeEnum,");
							sb.AppendLine($"arrayDtoDictionary[\"{name}\"].IsRotating);");
							sb.Unindent();
						}
					}
					sb.AppendLine();
				}

				sb.AppendLine("Reset(time: 0.0);");
				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendCalculateMethod(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
			sb.AppendLine("public override void Calculate(float[] buffer, int frameCount, double startTime)");
			sb.AppendLine("{");
			sb.Indent();
			{
				// Copy Fields to Local
				AppendCopyFieldsToLocal(sb, visitorResult);

				// Initialize ValueCount variable
				sb.AppendLine($"int valueCount = frameCount * {_channelCount};");

				// Initialize First Time Variable
				sb.AppendLine($"{visitorResult.FirstTimeVariableNameCamelCase} = startTime;");
				sb.AppendLine();

				// Loop
				sb.AppendLine($"for (int i = {_channelIndex}; i < valueCount; i += {_channelCount})"); // Writes values in an interleaved way to the buffer."
				sb.AppendLine("{");
				sb.Indent();
				{
					// Raw Calculation
					sb.Append(visitorResult.RawCalculationCode);

					// Accumulate
					sb.AppendLine("// Accumulate");
					sb.AppendLine($"double value = {visitorResult.ReturnValueLiteral};");
					sb.AppendLine();
					sb.AppendLine("if (double.IsNaN(value))"); // winmm will trip over NaN.
					sb.AppendLine("{");
					sb.Indent();
					{
						sb.AppendLine("value = 0;");
						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.AppendLine();
					sb.AppendLine("float floatValue = (float)value;"); // TODO: This seems unsafe. What happens if the cast to float is invalid?
					sb.AppendLine();
					sb.AppendLine("PatchCalculatorHelper.InterlockedAdd(ref buffer[i], floatValue);");

					sb.AppendLine();
					sb.AppendLine($"{visitorResult.FirstTimeVariableNameCamelCase} += frameDuration;");

					sb.Unindent();
				}
				sb.AppendLine("}");
				sb.AppendLine();

				// Copy Local to Fields
				AppendCopyLocalToFields(sb, visitorResult);

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendResetMethod(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			sb.AppendLine("public override void Reset(double time)");
			sb.AppendLine("{");
			sb.Indent();
			{
				// Copy Fields to Local
				AppendCopyFieldsToLocal(sb, visitorResult);

				// Initialize First Time Variable
				sb.AppendLine($"{visitorResult.FirstTimeVariableNameCamelCase} = time;");
				sb.AppendLine();

				// Raw Reset Code
				sb.Append(visitorResult.RawResetCode);

				// Copy Local to Fields
				AppendCopyLocalToFields(sb, visitorResult);

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendCopyFieldsToLocal(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			// Copy Fields to Local
			sb.AppendLine("double frameDuration = _frameDuration;");
			sb.AppendLine();

			IList<string> doubleInstanceVariableNamesCamelCase = GetDoubleInstanceVariableNamesCamelCase(visitorResult);
			if (doubleInstanceVariableNamesCamelCase.Any())
			{
				foreach (string variableName in doubleInstanceVariableNamesCamelCase)
				{
					sb.AppendLine($"double {variableName} = _{variableName};");
				}
				sb.AppendLine();
			}

			if (visitorResult.ArrayCalculationInfos.Any())
			{
				foreach (ArrayCalculationInfo variableInfo in visitorResult.ArrayCalculationInfos)
				{
					sb.AppendLine($"var {variableInfo.NameCamelCase} = _{variableInfo.NameCamelCase};");
				}
				sb.AppendLine();
			}

			// ReSharper disable once InvertIf
			if (visitorResult.LongLivedDoubleArrayVariableInfos.Any())
			{
				foreach (DoubleArrayVariableInfo variableInfo in visitorResult.LongLivedDoubleArrayVariableInfos)
				{
					sb.AppendLine($"double[] {variableInfo.NameCamelCase} = _{variableInfo.NameCamelCase};");
				}
				sb.AppendLine();
			}
		}

		private void AppendCopyLocalToFields(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<string> doubleInstanceVariableNamesCamelCase = GetDoubleInstanceVariableNamesCamelCase(visitorResult);
			foreach (string variableName in doubleInstanceVariableNamesCamelCase)
			{
				sb.AppendLine($"_{variableName} = {variableName};");
			}
		}

		private void AppendSetValue_ByPosition(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<InputVariableInfo> inputVariableInfos = visitorResult.InputVariableInfos.Sort().ToArray();

			sb.AppendLine("public override void SetValue(int position, double value)");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine("base.SetValue(position, value);");
				sb.AppendLine();

				if (inputVariableInfos.Any())
				{
					sb.AppendLine("switch (position)");
					sb.AppendLine("{");
					sb.Indent();
					{
						var i = 0;
						foreach (string inputVariableName in inputVariableInfos.Select(x => x.VariableNameCamelCase))
						{
							sb.AppendLine($"case {i}:");
							sb.Indent();
							{
								sb.AppendLine($"_{inputVariableName} = value;");
								sb.AppendLine("break;");
								sb.AppendLine();
								sb.Unindent();
							}
							i++;
						}

						sb.Unindent();
					}
					sb.AppendLine("}");
				}
				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendSetValue_ByDimensionEnum(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<InputVariableInfo> inputVariableInfos = visitorResult.InputVariableInfos
																	   .Where(x => x.DimensionEnum != DimensionEnum.Undefined || IsAnonymousDimension(x))
																	   .ToArray();

			sb.AppendLine("public override void SetValue(DimensionEnum dimensionEnum, double value)");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine("base.SetValue(dimensionEnum, value);");
				sb.AppendLine();

				AppendSetValue_FieldAssignments_ByDimensionEnum(sb, inputVariableInfos);

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendSetValue_ByName(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<InputVariableInfo> inputVariableInfos = visitorResult.InputVariableInfos
																	   .Where(x => !string.IsNullOrEmpty(x.CanonicalName) || IsAnonymousDimension(x))
																	   .ToArray();

			sb.AppendLine("public override void SetValue(string name, double value)");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine("base.SetValue(name, value);");
				sb.AppendLine();

				sb.AppendLine("string canonicalName = NameHelper.ToCanonical(name);");
				sb.AppendLine();

				AppendSetValue_FieldAssignments_ByCanonicalName(sb, inputVariableInfos);

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendSetValue_ByDimensionEnumAndPosition(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<InputVariableInfo> inputVariableInfosToInclude =
				visitorResult.InputVariableInfos
							 .Where(x => x.DimensionEnum != DimensionEnum.Undefined || IsAnonymousDimension(x))
							 .Sort()
							 .ToArray();

			sb.AppendLine("public override void SetValue(DimensionEnum dimensionEnum, int position, double value)");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine("base.SetValue(dimensionEnum, position, value);");
				sb.AppendLine();

				// Input Variables
				AppendSetValue_FieldAssignments_ByDimensionEnumAndPosition(sb, inputVariableInfosToInclude);

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		private void AppendSetValue_ByNameAndPosition(StringBuilderWithIndentation sb, OperatorDtoToCSharpVisitorResult visitorResult)
		{
			IList<InputVariableInfo> inputVariableInfosToInclude =
				visitorResult.InputVariableInfos
							 .Where(x => !string.IsNullOrEmpty(x.CanonicalName) || IsAnonymousDimension(x))
							 .Sort()
							 .ToArray();

			sb.AppendLine("public override void SetValue(string name, int position, double value)");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine("base.SetValue(name, position, value);");
				sb.AppendLine();
				sb.AppendLine("string canonicalName = NameHelper.ToCanonical(name);");
				sb.AppendLine();

				// Input Variables
				AppendSetValue_FieldAssignments_ByCanonicalNameAndPosition(sb, inputVariableInfosToInclude);

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		/// <summary> Assumes that the variable dimensionEnum is already declared in the generated code. </summary>
		private void AppendSetValue_FieldAssignments_ByDimensionEnum(StringBuilderWithIndentation sb, IList<InputVariableInfo> variableInfos)
		{
			// ReSharper disable once SuggestVarOrType_Elsewhere
			var groups = variableInfos.GroupBy(x => x.DimensionEnum);

			// ReSharper disable once PossibleMultipleEnumeration
			if (!groups.Any())
			{
				return;
			}

			sb.AppendLine("switch (dimensionEnum)");
			sb.AppendLine("{");
			sb.Indent();
			{
				// ReSharper disable once SuggestVarOrType_Elsewhere
				// ReSharper disable once PossibleMultipleEnumeration
				foreach (var group in groups)
				{
					sb.AppendLine($"case {nameof(DimensionEnum)}.{group.Key}:");
					sb.Indent();
					{
						foreach (InputVariableInfo variableInfo in group)
						{
							sb.AppendLine($"_{variableInfo.VariableNameCamelCase} = value;");
						}
						sb.AppendLine("break;");
						sb.AppendLine();
						sb.Unindent();
					}
				}

				sb.Unindent();
			}
			sb.AppendLine("}");
		}

		/// <summary> 
		/// Assumes that the variable canonicalName is already declared in the generated code.
		/// </summary>
		private void AppendSetValue_FieldAssignments_ByCanonicalName(StringBuilderWithIndentation sb, IList<InputVariableInfo> variableInfos)
		{
			// ReSharper disable once SuggestVarOrType_Elsewhere
			var groups = variableInfos.GroupBy(x => x.CanonicalName);
			// ReSharper disable once PossibleMultipleEnumeration
			if (!groups.Any())
			{
				return;
			}

			// ReSharper disable once SuggestVarOrType_Elsewhere
			// ReSharper disable once PossibleMultipleEnumeration
			foreach (var group in groups)
			{
				sb.AppendLine($"if (String.Equals(canonicalName, \"{group.Key}\", StringComparison.Ordinal))");
				sb.AppendLine("{");
				sb.Indent();
				{
					foreach (InputVariableInfo variableInfo in group)
					{
						sb.AppendLine($"_{variableInfo.VariableNameCamelCase} = value;");
					}
					sb.Unindent();
				}
				sb.AppendLine("}");
				sb.AppendLine();
			}
		}

		/// <summary> 
		/// Assumes that the variable dimensionEnum is already declared in the generated code.
		/// Assumes variableInfos are already order by Position.
		/// </summary>
		private void AppendSetValue_FieldAssignments_ByDimensionEnumAndPosition(StringBuilderWithIndentation sb, IList<InputVariableInfo> variableInfos)
		{
			// ReSharper disable once SuggestVarOrType_Elsewhere
			var groups = variableInfos.GroupBy(x => x.DimensionEnum);
			// ReSharper disable once SuggestVarOrType_Elsewhere
			foreach (var group in groups)
			{
				var i = 0;
				foreach (InputVariableInfo inputVariableInfo in group)
				{
					sb.AppendLine($"if (dimensionEnum == {nameof(DimensionEnum)}.{group.Key} && position == {i})");
					sb.AppendLine("{");
					sb.Indent();
					{
						sb.AppendLine($"_{inputVariableInfo.VariableNameCamelCase} = value;");
						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.AppendLine();

					i++;
				}
			}
		}

		/// <summary> 
		/// Assumes that the variable canonicalName is already declared in the generated code.
		/// Assumes variableInfos are already order by Position.
		/// </summary>
		private void AppendSetValue_FieldAssignments_ByCanonicalNameAndPosition(StringBuilderWithIndentation sb, IList<InputVariableInfo> variableInfos)
		{
			// Input Variables
			// ReSharper disable once SuggestVarOrType_Elsewhere
			var groups = variableInfos.GroupBy(x => x.CanonicalName);
			// ReSharper disable once SuggestVarOrType_Elsewhere
			foreach (var group in groups)
			{
				var i = 0;
				foreach (InputVariableInfo inputVariableInfo in group)
				{
					sb.AppendLine($"if (String.Equals(canonicalName, \"{group.Key}\") && position == {i})");
					sb.AppendLine("{");
					sb.Indent();
					{
						sb.AppendLine($"_{inputVariableInfo.VariableNameCamelCase} = value;");
						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.AppendLine();

					i++;
				}
			}
		}

		private IList<string> GetDoubleInstanceVariableNamesCamelCase(OperatorDtoToCSharpVisitorResult visitorResult) => visitorResult.LongLivedDoubleVariableNamesCamelCase.Concat(visitorResult.InputVariableInfos.Select(x => x.VariableNameCamelCase))
		                                                                                                                              .ToArray();

	    private bool IsAnonymousDimension(InputVariableInfo extendedVariableInfo)
		{
			bool isAnonymousDimension = extendedVariableInfo.DimensionEnum == DimensionEnum.Undefined && string.IsNullOrEmpty(extendedVariableInfo.CanonicalName);
			return isAnonymousDimension;
		}
	}
}
