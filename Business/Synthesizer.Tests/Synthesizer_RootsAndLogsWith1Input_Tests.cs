using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_RootsAndLogsWith1Input_Tests
    {
        private static readonly double[] _values = MathHelper.SpreadDoubles(1, 15, 29);

        // SquareRoot

        [TestMethod]
        public void Test_Synthesizer_SquareRoot_WithRoslyn() => Test_Synthesizer_SquareRoot(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_SquareRoot_WithCalculatorClasses() 
            => Test_Synthesizer_SquareRoot(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_SquareRoot(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.SquareRoot), Math.Sqrt, calculationMethodEnum);

        // CubeRoot

        [TestMethod]
        public void Test_Synthesizer_CubeRoot_WithRoslyn() => Test_Synthesizer_CubeRoot(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_CubeRoot_WithCalculatorClasses() => Test_Synthesizer_CubeRoot(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_CubeRoot(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.CubeRoot), x => Math.Pow(x, 1.0 / 3.0), calculationMethodEnum);

        // Ln

        [TestMethod]
        public void Test_Synthesizer_Ln_WithRoslyn() => Test_Synthesizer_Ln(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Ln_WithCalculatorClasses() => Test_Synthesizer_Ln(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Ln(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Ln), Math.Log, calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestHelper.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                _values,
                calculationMethodEnum);
    }
}