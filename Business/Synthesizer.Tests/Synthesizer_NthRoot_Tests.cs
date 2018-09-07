using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_NthRoot_Tests
    {
        private readonly double[] _numbers = MathHelper.SpreadDoubles(0, 10, 21);
        private readonly double[] _indices = { 2, Math.E, 3, Math.PI, 5, 12 };

        [TestMethod]
        public void Test_Synthesizer_NthRoot_WithRoslyn() => Test_Synthesizer_NthRoot(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_NthRoot_WithCalculatorClasses() => Test_Synthesizer_NthRoot(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_NthRoot(CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.TestWith2Inputs(
                x => x.New(nameof(SystemPatchNames.NthRoot), x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B)),
                (x, y) => Math.Pow(x, 1.0 / y),
                DimensionEnum.A,
                _numbers,
                DimensionEnum.B,
                _indices,
                calculationMethodEnum);
    }
}