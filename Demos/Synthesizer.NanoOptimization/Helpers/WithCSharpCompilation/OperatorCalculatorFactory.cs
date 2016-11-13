using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithCSharpCompilation
{
    internal static class OperatorCalculatorFactory
    {
        public static IOperatorCalculator CreateOperatorCalculatorFromDto(OperatorDtoBase dto, DimensionStack dimensionStack)
        {
            var visitor = new OperatorDtoCompiler();
            IOperatorCalculator calculator = visitor.CompileToOperatorCalculator(dto);

            return calculator;
        }

        public static IPatchCalculator CreatePatchCalculatorFromDto(OperatorDtoBase dto, int framesPerChunk)
        {
            var visitor = new OperatorDtoCompiler();
            IPatchCalculator calculator = visitor.CompileToPatchCalculator(dto, framesPerChunk);

            return calculator;
        }
    }
}
