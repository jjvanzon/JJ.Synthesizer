using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_SimpleArithmetic1In1Out_Tests
    {
        private static readonly double[] _values = { -67, -53.6, -40.2, -26.8, -13.4, 0, 13.4, 26.8, 40.2, 53.6, 67 };

        // Absolute

        [TestMethod]
        public void Test_Synthesizer_Absolute_WithRoslyn() => Test_Synthesizer_Absolute(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Absolute_WithCalculatorClasses() => Test_Synthesizer_Absolute(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Absolute(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Absolute), Math.Abs, calculationMethodEnum);

        // Negative

        [TestMethod]
        public void Test_Synthesizer_Negative_WithRoslyn() => Test_Synthesizer_Negative(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Negative_WithCalculatorClasses() => Test_Synthesizer_Negative(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Negative(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Negative), x => -x, calculationMethodEnum);

        // OneOverX

        [TestMethod]
        public void Test_Synthesizer_OneOverX_WithRoslyn() => Test_Synthesizer_OneOverX(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_OneOverX_WithCalculatorClasses() => Test_Synthesizer_OneOverX(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_OneOverX(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.OneOverX), x => 1.0 / x, calculationMethodEnum);

        // Sign

        [TestMethod]
        public void Test_Synthesizer_Sign_WithRoslyn() => Test_Synthesizer_Sign(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sign_WithCalculatorClasses() => Test_Synthesizer_Sign(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Sign(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.Sign), x => Math.Sign(x), calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test1In1Out(
                x => x.New(systemPatchName, x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                func,
                _values,
                calculationMethodEnum);
    }
}