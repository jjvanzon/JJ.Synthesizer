using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Comparative_Tests
    {
        private static readonly double[] _values = MathHelper.SpreadDoubles(-31, 66, 5);

        // Equal

        [TestMethod]
        public void Test_Synthesizer_Equal_WithRoslyn() => Test_Synthesizer_Equal(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Equal_WithCalculatorClasses() => Test_Synthesizer_Equal(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Equal(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Equal), (a, b) => a == b, calculationMethodEnum);

        // GreaterThan

        [TestMethod]
        public void Test_Synthesizer_GreaterThan_WithRoslyn() => Test_Synthesizer_GreaterThan(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_GreaterThan_WithCalculatorClasses() => Test_Synthesizer_GreaterThan(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_GreaterThan(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.GreaterThan), (a, b) => a > b, calculationMethodEnum);

        // GreaterThanOrEqual

        [TestMethod]
        public void Test_Synthesizer_GreaterThanOrEqual_WithRoslyn() => Test_Synthesizer_GreaterThanOrEqual(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_GreaterThanOrEqual_WithCalculatorClasses() => Test_Synthesizer_GreaterThanOrEqual(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_GreaterThanOrEqual(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.GreaterThanOrEqual), (a, b) => a >= b, calculationMethodEnum);

        // LessThan

        [TestMethod]
        public void Test_Synthesizer_LessThan_WithRoslyn() => Test_Synthesizer_LessThan(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_LessThan_WithCalculatorClasses() => Test_Synthesizer_LessThan(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_LessThan(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.LessThan), (a, b) => a < b, calculationMethodEnum);

        // LessThanOrEqual

        [TestMethod]
        public void Test_Synthesizer_LessThanOrEqual_WithRoslyn() => Test_Synthesizer_LessThanOrEqual(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_LessThanOrEqual_WithCalculatorClasses() => Test_Synthesizer_LessThanOrEqual(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_LessThanOrEqual(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.LessThanOrEqual), (a, b) => a <= b, calculationMethodEnum);

        // NotEqual

        [TestMethod]
        public void Test_Synthesizer_NotEqual_WithRoslyn() => Test_Synthesizer_NotEqual(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_NotEqual_WithCalculatorClasses() => Test_Synthesizer_NotEqual(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_NotEqual(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.NotEqual), (a, b) => a != b, calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double, bool> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                (a, b) => func(a, b) ? 1 : 0,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationMethodEnum);
    }
}