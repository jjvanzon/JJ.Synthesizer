using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_MiscArithmetic_Tests
    {
        [TestMethod]
        public void Test_Synthesizer_SquareRoot_WithRoslyn()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.SquareRoot), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Sqrt,
                MathHelper.SpreadDoubles(1, 15, 29),
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_SquareRoot_WithCalculatorClasses()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.SquareRoot), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Sqrt,
                MathHelper.SpreadDoubles(1, 15, 29),
                CalculationMethodEnum.Roslyn);
    }
}