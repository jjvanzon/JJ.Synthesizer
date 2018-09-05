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
        private static readonly double[] _xValues = MathHelper.SpreadDoubles(1, 15, 29);

        [TestMethod]
        public void Test_Synthesizer_SquareRoot_WithRoslyn()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.SquareRoot), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Sqrt,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_SquareRoot_WithCalculatorClasses()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.SquareRoot), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Sqrt,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_CubeRoot_WithRoslyn()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.CubeRoot), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                x => Math.Pow(x, 1.0 / 3.0),
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_CubeRoot_WithCalculatorClasses()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.CubeRoot), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                x => Math.Pow(x, 1.0 / 3.0),
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Ln_WithRoslyn()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Ln), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Log,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Ln_WithCalculatorClasses()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Ln), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Log,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sign_WithRoslyn()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Sign), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Log,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sign_WithCalculatorClasses()
            => TestExecutor.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Sign), x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                Math.Log,
                _xValues,
                CalculationMethodEnum.Roslyn);
    }
}