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
    public class Synthesizer_LogN_Tests
    {
        private static readonly double[] _numberValues = MathHelper.SpreadDoubles(1, 100, 10);
        private static readonly double[] _baseValues = { 1.1, 2, Math.E, 3, 5, 10 };

        [TestMethod]
        public void Test_Synthesizer_LogN_WithRoslyn() => Test_Synthesizer_LogN(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_LogN_WithCalculatorClasses() => Test_Synthesizer_LogN(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_LogN(CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(nameof(SystemPatchNames.LogN), x.PatchInlet(DimensionEnum.Number), x.PatchInlet(DimensionEnum.Base)),
                Math.Log,
                DimensionEnum.Number,
                _numberValues,
                DimensionEnum.Base,
                _baseValues,
                calculationEngineEnum);
    }
}