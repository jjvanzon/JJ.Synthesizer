using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_SimpleArithmeticWithNInputs_Tests
    {
        private static readonly double[] _values = { -6.75, 0, 17.5 };

        // Add

        [TestMethod]
        public void Test_Synthesizer_Add_WithRoslyn() => Test_Synthesizer_Add(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Add_WithCalculatorClasses() => Test_Synthesizer_Add(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Add(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Add), (a, b, c) => a + b + c, calculationMethodEnum);

        // Multiply

        [TestMethod]
        public void Test_Synthesizer_Multiply_WithRoslyn() => Test_Synthesizer_Multiply(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Multiply_WithCalculatorClasses() => Test_Synthesizer_Multiply(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Multiply(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Multiply), (a, b, c) => a * b * c, calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double, double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.TestWith3Inputs(
                x => x.NewWithItemInlets(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B), x.PatchInlet(DimensionEnum.Item)),
                func,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                DimensionEnum.Item,
                _values,
                calculationMethodEnum);
    }
}