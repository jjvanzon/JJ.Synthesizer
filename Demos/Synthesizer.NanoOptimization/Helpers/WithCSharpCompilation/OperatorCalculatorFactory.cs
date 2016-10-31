using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation;

namespace JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithCSharpCompilation
{
    internal static class OperatorCalculatorFactory
    {
        public static IOperatorCalculator CreateOperatorCalculatorFromDto(OperatorDto dto, DimensionStack dimensionStack)
        {
            var visitor = new OperatorDtoToOperatorCalculatorVisitor();
            IOperatorCalculator calculator = visitor.Execute(dto);

            return calculator;
        }
    }
}
