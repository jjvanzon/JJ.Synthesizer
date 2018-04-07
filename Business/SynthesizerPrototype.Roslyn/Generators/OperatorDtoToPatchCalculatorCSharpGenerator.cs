using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Roslyn.Calculation;
using JJ.Business.SynthesizerPrototype.Roslyn.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;
using JJ.Business.SynthesizerPrototype.Roslyn.Visitors;
using JJ.Framework.Text;

// ReSharper disable RedundantArgumentDefaultValue

namespace JJ.Business.SynthesizerPrototype.Roslyn.Generators
{
	internal class OperatorDtoToPatchCalculatorCSharpGenerator
	{
		private const int RAW_CALCULATION_INDENT_LEVEL = 4;
		private const string TAB_STRING = "	";

		public string Execute(IOperatorDto dto, string generatedNameSpace, string generatedClassName)
		{
			// Build up Method Body
			var visitor = new OperatorDtoToCSharpVisitor();
			OperatorDtoToCSharpVisitorResult visitorResult = visitor.Execute(dto, RAW_CALCULATION_INDENT_LEVEL);

			IList<string> instanceVariableNames = visitorResult.PhaseVariableNamesCamelCase.Concat(visitorResult.PreviousPositionVariableNamesCamelCase)
																						   .Concat(visitorResult.VariableInputValueInfos.Select(x => x.NameCamelCase))
																						   .ToArray();
			// Build up Code File
			var sb = new StringBuilderWithIndentation(TAB_STRING);

			sb.AppendLine("using System;");
			sb.AppendLine("using System.Runtime.CompilerServices;");
			sb.AppendLine("using " + typeof(IOperatorCalculator).Namespace + ";");
			sb.AppendLine("using " + typeof(SineCalculator).Namespace + ";");
			sb.AppendLine();
			sb.AppendLine($"namespace {generatedNameSpace}");
			sb.AppendLine("{");
			sb.Indent();
			{
				sb.AppendLine($"public class {generatedClassName} : IPatchCalculator");
				sb.AppendLine("{");
				sb.Indent();
				{
					// Fields

					// NOTE:
					// Array access is excluded from the test, 
					// because otherwise it would not be a fair performance comparison.

					//_sb.AppendLine("private double[] _values;");
					sb.AppendLine("private int _framesPerChunk;");
					sb.AppendLine();

					foreach (string instanceVariableName in instanceVariableNames)
					{
						sb.AppendLine($"private double _{instanceVariableName};");
					}

					// Constructor
					sb.AppendLine();
					sb.AppendLine($"public {generatedClassName}(int framesPerChunk)");
					sb.AppendLine("{");
					sb.Indent();
					{
						sb.AppendLine("_framesPerChunk = framesPerChunk;");
						//_sb.AppendLine("_values = new double[_framesPerChunk];");
						sb.AppendLine("");

						foreach (VariableInputInfo variableInputValueInfo in visitorResult.VariableInputValueInfos)
						{
							sb.AppendLine($"_{variableInputValueInfo.NameCamelCase} = {CompilationHelper.FormatValue(variableInputValueInfo.Value)};");
						}
						sb.AppendLine("");

						sb.AppendLine("Reset();");
						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.AppendLine();

					// Reset Method
					sb.AppendLine("public void Reset()");
					sb.AppendLine("{");
					sb.Indent();
					{
						foreach (string variableName in visitorResult.PhaseVariableNamesCamelCase.Concat(visitorResult.PreviousPositionVariableNamesCamelCase))
						{
							sb.AppendLine($"_{variableName} = 0.0;");
						}

						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.AppendLine();

					// SetInput Method
					sb.AppendLine("public void SetInput(int listIndex, double input)");
					sb.AppendLine("{");
					sb.Indent();
					{
						sb.AppendLine("switch (listIndex)");
						sb.AppendLine("{");
						sb.Indent();
						{
							int i = 0;
							foreach (string inputVariableName in visitorResult.VariableInputValueInfos.Select(x => x.NameCamelCase))
							{
								sb.AppendLine($"case {i}:");
								sb.Indent();
								{
									sb.AppendLine($"_{inputVariableName} = input;");
									sb.AppendLine("break;");
									sb.Unindent();
								}
								i++;
							}

							sb.Unindent();
						}
						sb.AppendLine("}");
						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.AppendLine();

					// Calculate Method
					sb.AppendLine("[MethodImpl(MethodImplOptions.AggressiveInlining)]");
					sb.AppendLine("public double[] Calculate(double startTime, double frameDuration)");
					sb.AppendLine("{");
					sb.Indent();
					{
						// Copy Fields to Local

						//_sb.AppendLine("double[] values = _values;");
						sb.AppendLine("int framesPerChunk = _framesPerChunk;");
						sb.AppendLine();
						foreach (string instanceVariableName in instanceVariableNames)
						{
							sb.AppendLine($"double {instanceVariableName} = _{instanceVariableName};");
						}
						sb.AppendLine();

						// Position Variables
						string firstPositionVariableName = visitorResult.PositionVariableNamesCamelCase.FirstOrDefault();
						if (firstPositionVariableName != null)
						{
							sb.AppendLine($"double {firstPositionVariableName} = startTime;");
						}

						foreach (string positionVariableName in visitorResult.PositionVariableNamesCamelCase.Skip(1))
						{
							sb.AppendLine($"double {positionVariableName};");
						}
						sb.AppendLine();

						// Loop
						sb.AppendLine("for (int i = 0; i < framesPerChunk; i++)");
						sb.AppendLine("{");
						sb.Indent();
						{
							// Raw Calculation
							sb.Append(visitorResult.RawCalculationCode);
							sb.AppendLine();

							// Accumulate
							sb.AppendLine($"double value = {visitorResult.ReturnValueLiteral};");
							sb.AppendLine();
							//_sb.AppendLine("values[i] = value;");

							if (firstPositionVariableName != null)
							{
								sb.AppendLine();
								sb.AppendLine($"{firstPositionVariableName} += frameDuration;");
							}

							sb.Unindent();
						}
						sb.AppendLine("}");
						sb.AppendLine();

						// Copy Local to Fields
						foreach (string variableName in instanceVariableNames)
						{
							sb.AppendLine($"_{variableName} = {variableName};");
						}

						// Return statement
						sb.AppendLine();
						//_sb.AppendLine($"return values;");
						sb.AppendLine("return null;");

						sb.Unindent();
					}
					sb.AppendLine("}");
					sb.Unindent();
				}
				sb.AppendLine("}");
				sb.Unindent();
			}
			sb.AppendLine("}");

			string csharp = sb.ToString();
			return csharp;
		}
	}
}
