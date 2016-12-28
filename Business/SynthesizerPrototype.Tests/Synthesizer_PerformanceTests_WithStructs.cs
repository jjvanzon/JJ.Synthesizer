using System;
using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using JJ.Business.SynthesizerPrototype.Tests.Helpers.WithStructs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.SynthesizerPrototype.WithStructs.CopiedCode.From_JJ_Business_SynthesizerPrototype;

namespace JJ.Business.SynthesizerPrototype.Tests
{
    [TestClass]
    public class Synthesizer_PerformanceTests_WithStructs
    {
        [TestMethod]
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_50_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_50_000_Iterations_InliningWithStructs_WithDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_500_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_8Partials_500_000_Iterations_InliningWithStructs_WithDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_SinglePartial_50_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_SinglePartial_50_000_Iterations_InliningWithStructs_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_SinglePartial_500_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithoutTime_SinglePartial_500_000_Iterations_InliningWithStructs_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
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
        public void PerformanceTest_SynthesizerPrototype_WithTime_8Partials_50_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithTime_8Partials_50_000_Iterations_InliningWithStructs_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

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
        public void PerformanceTest_SynthesizerPrototype_WithTime_8Partials_500_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithTime_8Partials_500_000_Iterations_InliningWithStructs_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

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
        public void PerformanceTest_SynthesizerPrototype_WithTime_SinglePartial_50_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithTime_SinglePartial_50_000_Iterations_InliningWithStructs_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

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
        public void PerformanceTest_SynthesizerPrototype_WithTime_SinglePartial_500_000_Iterations_InliningWithStructs_NoDto()
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
        public void PerformanceTest_SynthesizerPrototype_WithTime_SinglePartial_500_000_Iterations_InliningWithStructs_WithDto()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
            var calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

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
        public void Debug_SynthesizerPrototype_RuntimeGeneric_DoesNotInline_ButEquates_CompileTimeGeneric_WhichDoesInline()
        {
            var dimensionStack = new DimensionStack();

            dimensionStack.Push(0.123456789);

            var compileTimeStruct = OperatorCalculatorFactory.CreateOperatorCalculatorStructure_SinglePartial(dimensionStack);

            OperatorDtoBase dto = OperatorDtoFactory.CreateOperatorDto_SinglePartial();
            var runTimeStruct = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

            Type compileTimeStructType = compileTimeStruct.GetType();
            Type runTimeStructType = runTimeStruct.GetType();
            bool typesAreEqual = compileTimeStructType == runTimeStructType;
            Assert.IsTrue(typesAreEqual, "designTimeStruct is not equal to runTimeStruct.");

            double compileTimeStructValue = compileTimeStruct.Calculate();
            double runTimeStructValue = runTimeStruct.Calculate();
        }
    }
}
