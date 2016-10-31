using System;
using System.Diagnostics;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.Operators.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Dto;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithCSharpCompilation;
using JJ.Demos.Synthesizer.NanoOptimization.Visitors.WithCSharpCompilation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Demos.Synthesizer.NanoOptimization
{
    [TestClass]
    public class Synthesizer_PerformanceTests_WithCSharpCompilation
    {
        [TestMethod]
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoToCSharpVisitor()
        {
            OperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoToCSharpVisitor();
            string csharp = visitor.Execute(dto);
        }

        [TestMethod]
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoToOperatorCalculatorVisitor()
        {
            OperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoToOperatorCalculatorVisitor();
            IOperatorCalculator calculator = visitor.Execute(dto);
            double value = calculator.Calculate();
        }

        [TestMethod]
        public void PerformanceTest_Synthesizer_NanoOptimization_WithoutTime_8Partials_50_000_Iterations_WithCSharpCompilation_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 50000; i++)
            {
                calculator.Calculate();
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(50000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void PerformanceTest_Synthesizer_NanoOptimization_WithoutTime_8Partials_500_000_Iterations_WithCSharpCompilation_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 500000; i++)
            {
                calculator.Calculate();
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(50000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }
    }
}
