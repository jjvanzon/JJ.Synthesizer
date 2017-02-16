using System.Diagnostics;
using JJ.Business.SynthesizerPrototype.Dto;
using JJ.Business.SynthesizerPrototype.Tests.Helpers;
using JJ.Business.SynthesizerPrototype.Tests.Helpers.WithInheritance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.SynthesizerPrototype.WithInheritance.CopiedCode.From_JJ_Business_SynthesizerPrototype;
using JJ.Business.SynthesizerPrototype.WithInheritance.Calculation;

namespace JJ.Business.SynthesizerPrototype.Tests
{
    [TestClass]
    public class Synthesizer_PerformanceTests_WithInheritance
    {
        [TestMethod]
        public void PerformanceTest_SynthesizerPrototype_WithInheritance()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            OperatorCalculatorBase calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

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
        public void Debug_SynthesizerPrototype_WithInheritance()
        {
            var dimensionStack = new DimensionStack();
            dimensionStack.Push(0.0);

            IOperatorDto dto = OperatorDtoFactory.CreateOperatorDto_8Partials();
            OperatorCalculatorBase calculator = OperatorCalculatorFactory.CreateOperatorCalculatorFromDto(dto, dimensionStack);

            double t = 0.0;
            const double dt = 1.0 / 500000.0;

            while (t <= 1.0)
            {
                dimensionStack.Set(t);

                double value = calculator.Calculate();

                t += dt;
            }
        }
    }
}
