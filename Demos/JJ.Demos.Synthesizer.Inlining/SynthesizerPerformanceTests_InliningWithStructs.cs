using System;
using System.Diagnostics;
using JJ.Demos.Synthesizer.Inlining.Helpers;
using JJ.Demos.Synthesizer.Inlining.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Demos.Synthesizer.Inlining
{
    [TestClass]
    public class SynthesizerPerformanceTests_InliningWithStructs
    {
        [TestMethod]
        public void Test_SynthesizerPerformance_InliningWithStructs_WithoutTime()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory_WithGenericMutableStructsAndHelpers.CreateOperatorCalculatorStructure(dimensionStack);

            var stopWatch = Stopwatch.StartNew();

            for (int i = 0; i < TestHelper.ITERATION_COUNT; i++)
            {
                calculator.Calculate();
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(TestHelper.ITERATION_COUNT, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void Test_SynthesizerPerformance_InliningWithStructs_WithTime()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory_WithGenericMutableStructsAndHelpers.CreateOperatorCalculatorStructure(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / TestHelper.SAMPLING_RATE;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 20)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(TestHelper.ITERATION_COUNT, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }
    }
}
