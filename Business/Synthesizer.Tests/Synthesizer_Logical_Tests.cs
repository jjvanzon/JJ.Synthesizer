using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Logical_Tests
    {
        private static readonly double[] _values = { 0.0, 1.0, -1.0, Math.PI };

        // And

        [TestMethod]
        public void Test_Synthesizer_And_WithRoslyn() => Test_Synthesizer_And(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_And_WithCalculatorClasses() => Test_Synthesizer_And(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_And(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.And), (x, y) => x != 0 && y != 0 ? 1 : 0, calculationEngineEnum);

        // Nand

        [TestMethod]
        public void Test_Synthesizer_Nand_WithRoslyn() => Test_Synthesizer_Nand(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Nand_WithCalculatorClasses() => Test_Synthesizer_Nand(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Nand(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Nand), (x, y) => !(x != 0 && y != 0) ? 1 : 0, calculationEngineEnum);

        // Not

        [TestMethod]
        public void Test_Synthesizer_Not_WithRoslyn() => Test_Synthesizer_Not(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Not_WithCalculatorClasses() => Test_Synthesizer_Not(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Not(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Not), x => x == 0 ? 1 : 0, calculationEngineEnum);

        // Or

        [TestMethod]
        public void Test_Synthesizer_Or_WithRoslyn() => Test_Synthesizer_Or(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Or_WithCalculatorClasses() => Test_Synthesizer_Or(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Or(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Or), (x, y) => x != 0 || y != 0 ? 1 : 0, calculationEngineEnum);

        // Xor

        [TestMethod]
        public void Test_Synthesizer_Xor_WithRoslyn() => Test_Synthesizer_Xor(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Xor_WithCalculatorClasses() => Test_Synthesizer_Xor(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Xor(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Xor), (x, y) => (x != 0) ^ (y != 0) ? 1 : 0, calculationEngineEnum);

        // Generalized Methods

        private void ExecuteTest(string systemPatchName, Func<double, double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                func,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly: true);

        private void ExecuteTest(string systemPatchName, Func<double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.Number)),
                func,
                DimensionEnum.Number,
                _values,
                calculationEngineEnum,
                mustCompareZeroAndNonZeroOnly: true);
    }
}