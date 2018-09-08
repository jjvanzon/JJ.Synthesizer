using System;
using JJ.Business.Synthesizer.Configuration;
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
        public void Test_Synthesizer_Divide_WithRoslyn() => Test_Synthesizer_Divide(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Divide_WithCalculatorClasses() => Test_Synthesizer_Divide(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Divide(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Divide), (a, b) => a / b, calculationMethodEnum);

        // Remainder

        [TestMethod]
        public void Test_Synthesizer_Remainder_WithRoslyn() => Test_Synthesizer_Remainder(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Remainder_WithCalculatorClasses() => Test_Synthesizer_Remainder(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Remainder(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Remainder), (a, b) => a % b, calculationMethodEnum);

        // Subtract

        [TestMethod]
        public void Test_Synthesizer_Subtract_WithRoslyn() => Test_Synthesizer_Subtract(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Subtract_WithCalculatorClasses() => Test_Synthesizer_Subtract(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Subtract(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Subtract), (a, b) => a - b, calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.TestWith2Inputs(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                func,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationMethodEnum);
    }
}