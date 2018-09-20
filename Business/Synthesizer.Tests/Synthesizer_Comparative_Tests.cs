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
        public void Test_Synthesizer_Equal_WithRoslyn() => Test_Synthesizer_Equal(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Equal_WithCalculatorClasses() => Test_Synthesizer_Equal(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Equal(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Equal), (a, b) => a == b, calculationEngineEnum);

        // GreaterThan

        [TestMethod]
        public void Test_Synthesizer_GreaterThan_WithRoslyn() => Test_Synthesizer_GreaterThan(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_GreaterThan_WithCalculatorClasses() => Test_Synthesizer_GreaterThan(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_GreaterThan(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.GreaterThan), (a, b) => a > b, calculationEngineEnum);

        // GreaterThanOrEqual

        [TestMethod]
        public void Test_Synthesizer_GreaterThanOrEqual_WithRoslyn() => Test_Synthesizer_GreaterThanOrEqual(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_GreaterThanOrEqual_WithCalculatorClasses() => Test_Synthesizer_GreaterThanOrEqual(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_GreaterThanOrEqual(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.GreaterThanOrEqual), (a, b) => a >= b, calculationEngineEnum);

        // LessThan

        [TestMethod]
        public void Test_Synthesizer_LessThan_WithRoslyn() => Test_Synthesizer_LessThan(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_LessThan_WithCalculatorClasses() => Test_Synthesizer_LessThan(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_LessThan(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.LessThan), (a, b) => a < b, calculationEngineEnum);

        // LessThanOrEqual

        [TestMethod]
        public void Test_Synthesizer_LessThanOrEqual_WithRoslyn() => Test_Synthesizer_LessThanOrEqual(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_LessThanOrEqual_WithCalculatorClasses() => Test_Synthesizer_LessThanOrEqual(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_LessThanOrEqual(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.LessThanOrEqual), (a, b) => a <= b, calculationEngineEnum);

        // NotEqual

        [TestMethod]
        public void Test_Synthesizer_NotEqual_WithRoslyn() => Test_Synthesizer_NotEqual(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_NotEqual_WithCalculatorClasses() => Test_Synthesizer_NotEqual(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_NotEqual(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.NotEqual), (a, b) => a != b, calculationEngineEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double, bool> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                (a, b) => func(a, b) ? 1 : 0,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly: true);
    }
}