using JJ.Business.SynthesizerPrototype.Roslyn.Calculation;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;

namespace JJ.Business.SynthesizerPrototype.Tests.Helpers.WithCSharpCompilation
{
    internal static class OperatorCalculatorFactory
    {
        public static IOperatorCalculator CreateOperatorCalculatorFromDto(OperatorDtoBase dto)
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
