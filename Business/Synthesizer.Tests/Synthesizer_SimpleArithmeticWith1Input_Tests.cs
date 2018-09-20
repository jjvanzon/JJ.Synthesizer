using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_SimpleArithmeticWith1Input_Tests
    {
        private static readonly double[] _values = { -67, -53.6, -40.2, -26.8, -13.4, 0, 13.4, 26.8, 40.2, 53.6, 67 };

        // Absolute

        [TestMethod]
        public void Test_Synthesizer_Absolute_WithRoslyn() => Test_Synthesizer_Absolute(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Absolute_WithCalculatorClasses() => Test_Synthesizer_Absolute(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Absolute(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Absolute), Math.Abs, calculationEngineEnum);

        // Negative

        [TestMethod]
        public void Test_Synthesizer_Negative_WithRoslyn() => Test_Synthesizer_Negative(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Negative_WithCalculatorClasses() => Test_Synthesizer_Negative(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Negative(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Negative), x => -x, calculationEngineEnum);

        // OneOverX

        [TestMethod]
        public void Test_Synthesizer_OneOverX_WithRoslyn() => Test_Synthesizer_OneOverX(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_OneOverX_WithCalculatorClasses() => Test_Synthesizer_OneOverX(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_OneOverX(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.OneOverX), x => 1.0 / x, calculationEngineEnum);

        // Sign

        [TestMethod]
        public void Test_Synthesizer_Sign_WithRoslyn() => Test_Synthesizer_Sign(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sign_WithCalculatorClasses() => Test_Synthesizer_Sign(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Sign(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(SystemPatchNames.Sign), x => Math.Sign(x), calculationEngineEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                _values,
                calculationEngineEnum);
    }
}