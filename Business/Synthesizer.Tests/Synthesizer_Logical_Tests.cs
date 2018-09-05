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
            => TestExecutor.Test2In1Out(
                x => x.New(nameof(SystemPatchNames.Xor), x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                (x, y) => (x != 0) ^ (y != 0) ? 1.0 : 0.0,
                DimensionEnum.A,
                _values,
                DimensionEnum.B,
                _values,
                calculationMethodEnum);
    }
}