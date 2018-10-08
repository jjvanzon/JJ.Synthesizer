using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_SimpleArithmeticWith2Inputs_Tests
    {
        private static readonly double[] _values = { -31, -6.75, 0, 17.5, 41.75, 66 };

        // Divide

        [TestMethod]
        public void Test_Synthesizer_Divide_WithRoslyn() => Test_Synthesizer_Divide(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Divide_WithCalculatorClasses() => Test_Synthesizer_Divide(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Divide(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Divide), (a, b) => a / b, calculationEngineEnum);

        // Remainder

        [TestMethod]
        public void Test_Synthesizer_Remainder_WithRoslyn() => Test_Synthesizer_Remainder(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Remainder_WithCalculatorClasses() => Test_Synthesizer_Remainder(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Remainder(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Remainder), (a, b) => a % b, calculationEngineEnum);

        // Subtract

        [TestMethod]
        public void Test_Synthesizer_Subtract_WithRoslyn() => Test_Synthesizer_Subtract(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Subtract_WithCalculatorClasses() => Test_Synthesizer_Subtract(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Subtract(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationEngineEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                func,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationEngineEnum);
    }
}