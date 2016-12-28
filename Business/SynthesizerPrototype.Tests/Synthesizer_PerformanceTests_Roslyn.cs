using System;
using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using JJ.Business.SynthesizerPrototype.Tests.Helpers.WithCSharpCompilation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.SynthesizerPrototype.Roslyn.Calculation;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;
using JJ.Business.SynthesizerPrototype.Roslyn.Visitors;

namespace JJ.Business.SynthesizerPrototype.Tests
{
    [TestClass]
    public class Synthesizer_PerformanceTests_Roslyn
    {
        [TestMethod]
        public void Debug_SynthesizerPrototype_OperatorDtoToOperatorCalculatorCSharpVisitor()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoToOperatorCalculatorCSharpVisitor();
            string csharp = visitor.Execute(dto, "MyNameSpace", "MyClass");
        }

        [TestMethod]
        public void Debug_SynthesizerPrototype_OperatorDtoCompiler_CompileToOperatorCalculator()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoCompiler();
            IOperatorCalculator calculator = visitor.CompileToOperatorCalculator(dto);
            double value = calculator.Calculate();
        }

        [TestMethod]
        public void Debug_SynthesizerPrototype_OperatorDtoCompiler_CompileToPatchCalculator()
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
        public void Debug_SynthesizerPrototype_OperatorDtoToOperatorCalculatorVisitor_WithoutSymbols()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var visitor = new OperatorDtoCompiler(includeSymbols: false);
            IOperatorCalculator calculator = visitor.CompileToOperatorCalculator(dto);
            double value = calculator.Calculate();
        }

        [TestMethod]
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_50_000_Iterations_Roslyn_WithDto_NotByChunk()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto);

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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_500_000_Iterations_Roslyn_WithDto_NotByChunk()
        {
            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto);

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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_500_000_Iterations_Roslyn_WithDto_ByChunk()
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
