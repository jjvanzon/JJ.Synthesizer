using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.SynthesizerPrototype.Tests.Calculation;
using JJ.Business.SynthesizerPrototype.Tests.Calculation.WithCSharpCompilation;
using JJ.Business.SynthesizerPrototype.Tests.Dto;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers.WithCSharpCompilation
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
