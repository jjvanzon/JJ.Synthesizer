using System;
using System.Diagnostics;
using JJ.Demos.Synthesizer.NanoOptimization.Calculation;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers;
using JJ.Demos.Synthesizer.NanoOptimization.Helpers.WithStructs;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Demos.Synthesizer.NanoOptimization
{
    [TestClass]
    public class SynthesizerPerformanceTests_InliningWithStructs
    {
        [TestMethod]
        public void Test_SynthesizerPerformance_WithoutTime_8Partials_50_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_8Partials(dimensionStack);

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
        public void Test_SynthesizerPerformance_WithoutTime_8Partials_500_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_8Partials(dimensionStack);

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
        public void Test_SynthesizerPerformance_WithoutTime_SinglePartial_50_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

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
        public void Test_SynthesizerPerformance_WithoutTime_SinglePartial_50_000_Iterations_InliningWithStructs_FromDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial_FromDto(dimensionStack);

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
        public void Test_SynthesizerPerformance_WithoutTime_SinglePartial_500_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

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
        public void Test_SynthesizerPerformance_WithoutTime_SinglePartial_500_000_Iterations_InliningWithStructs_FromDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial_FromDto(dimensionStack);

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
        public void Test_SynthesizerPerformance_WithTime_8Partials_50_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_8Partials(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / 50000.0;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(50000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void Test_SynthesizerPerformance_WithTime_8Partials_500_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_8Partials(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / 500000.0;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void Test_SynthesizerPerformance_WithTime_SinglePartials_50_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / 50000.0;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(50000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void Test_SynthesizerPerformance_WithTime_SinglePartials_50_000_Iterations_InliningWithStructs_FromDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial_FromDto(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / 50000.0;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(50000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void Test_SynthesizerPerformance_WithTime_SinglePartials_500_000_Iterations_InliningWithStructs()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / 500000.0;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }

        [TestMethod]
        public void Test_SynthesizerPerformance_WithTime_SinglePartials_500_000_Iterations_InliningWithStructs_FromDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial_FromDto(dimensionStack);

            double t = 0.0;
            double dt = 1.0 / 500000.0;

            var stopWatch = Stopwatch.StartNew();

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }

            stopWatch.Stop();

            string message = TestHelper.GetPerformanceInfoMessage(500000, stopWatch.Elapsed);

            Assert.Inconclusive(message);
        }
    }
}
