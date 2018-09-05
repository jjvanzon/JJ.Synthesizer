using System;
using JJ.Business.Synthesizer.Configuration;
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

        [TestMethod]
        public void Test_Synthesizer_Xor_WithRoslyn() => Test_Synthesizer_Xor(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Xor_WithCalculatorClasses() => Test_Synthesizer_Xor(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Xor(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Xor), (x, y) => (x != 0) ^ (y != 0) ? 1 : 0, calculationMethodEnum);

        [TestMethod]
        public void Test_Synthesizer_Nand_WithRoslyn() => Test_Synthesizer_Nand(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Nand_WithCalculatorClasses() => Test_Synthesizer_Nand(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Nand(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Nand), (x, y) => !(x != 0 && y != 0) ? 1 : 0, calculationMethodEnum);

        private void ExecuteTest(string systemPatchName, Func<double, double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test2In1Out(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                func,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationMethodEnum);
    }
}