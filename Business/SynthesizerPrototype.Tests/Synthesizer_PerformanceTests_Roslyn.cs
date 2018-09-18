using System;
using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Roslyn.Calculation;
using JJ.Business.SynthesizerPrototype.Roslyn.Helpers;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable UnusedVariable

namespace JJ.Business.SynthesizerPrototype.Tests
{
    [TestClass]
    public class Synthesizer_PerformanceTests_Roslyn
    {
        [TestMethod]
        public void PerformanceTest_SynthesizerPrototype_Roslyn_ByChunk()
        {
            const int framesPerChunk = 5000;
            const double frameDuration = 1.0 / 50000.0;

            IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var compiler = new OperatorDtoCompiler();
            IPatchCalculator calculator = compiler.CompileToPatchCalculator(dto, framesPerChunk);

            var stopWatch = Stopwatch.StartNew();

            for (var i = 0; i < 100; i++)
            {
                calculator.Calculate(0.0, frameDuration);
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Console.WriteLine(message);
        }

        [TestMethod]
        public void PerformanceTest_SynthesizerPrototype_Roslyn_NotByChunk()
        {
            IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var compiler = new OperatorDtoCompiler();
            IOperatorCalculator calculator = compiler.CompileToOperatorCalculator(dto);

            var stopWatch = Stopwatch.StartNew();

            for (var i = 0; i < 500000; i++)
            {
                calculator.Calculate();
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Console.WriteLine(message);
        }

        [TestMethod]
        public void Debug_SynthesizerPrototype_OperatorDtoCompiler_WithoutSymbols()
        {
            IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var compiler = new OperatorDtoCompiler(false);
            IOperatorCalculator calculator = compiler.CompileToOperatorCalculator(dto);
            double value = calculator.Calculate();
        }
    }
}