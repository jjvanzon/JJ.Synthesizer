using System;
using System.Diagnostics;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation.WithCSharpCompilation;
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
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoToOperatorCalculatorCSharpVisitor()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoToOperatorCalculatorCSharpVisitor();
            string csharp = visitor.Execute(dto, "MyNameSpace", "MyClass");
        }

        [TestMethod]
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoCompiler_CompileToOperatorCalculator()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoCompiler();
            IOperatorCalculator calculator = visitor.CompileToOperatorCalculator(dto);
            double value = calculator.Calculate();
        }

        [TestMethod]
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoCompiler_CompileToPatchCalculator()
        {
            double startTime = 0.0;
            int frameCount = 10;
            double frameDuration = 1.0 / frameCount;

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoCompiler();
            IPatchCalculator calculator = visitor.CompileToPatchCalculator(dto, frameCount);

            calculator.Calculate(startTime, frameDuration);
        }

        [TestMethod]
        public void Debug_Synthesizer_NanoOptimization_OperatorDtoToOperatorCalculatorVisitor_WithoutSymbols()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoCompiler(includeSymbols: false);
            IOperatorCalculator calculator = visitor.CompileToOperatorCalculator(dto);
            double value = calculator.Calculate();
        }

        [TestMethod]
        public void PerformanceTest_Synthesizer_NanoOptimization_WithoutTime_8Partials_50_000_Iterations_WithCSharpCompilation_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
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

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 500000; i++)
            {
                calculator.Calculate();
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void PerformanceTest_Synthesizer_NanoOptimization_WithoutTime_8Partials_500_000_Iterations_WithCSharpCompilation_WithDto_ByChunk()
        {
            int framesPerChunk = 5000;
            double frameDuration = 1.0 / 50000.0;

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            IPatchCalculator calculator = OperatorCalculatorFactory.CreatePatchCalculatorFromDto(dto, framesPerChunk);

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < 100; i++)
            {
                calculator.Calculate(0.0, frameDuration);
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }
    }
}
