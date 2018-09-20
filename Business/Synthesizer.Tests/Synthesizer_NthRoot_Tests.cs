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
        private readonly double[] _bases = MathHelper.SpreadDoubles(0, 10, 21);
        private readonly double[] _exponents = { 0, 2, Math.E, 12 };

        [TestMethod]
        public void Test_Synthesizer_NthRoot_WithRoslyn() => Test_Synthesizer_NthRoot(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_NthRoot_WithCalculatorClasses() => Test_Synthesizer_NthRoot(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_NthRoot(CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(nameof(SystemPatchNames.NthRoot), x.PatchInlet(DimensionEnum.Base), x.PatchInlet(DimensionEnum.Exponent)),
                (x, y) => Math.Pow(x, 1.0 / y),
                DimensionEnum.Base,
                _bases,
                DimensionEnum.Exponent,
                _exponents,
                calculationEngineEnum);
    }
}