using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_SimpleArithmeticWith3Inputs_Tests
    {
        private static readonly double[] _aValues = { -31, 0, 17.5, 41.75 };
        private static readonly double[] _bValues = { -2, 0, 3 };
        private static readonly double[] _origins = { -6.75, 0, 17.5 };

        // DivideWithOrigin

        [TestMethod]
        public void Test_Synthesizer_DivideWithOrigin_WithRoslyn() => Test_Synthesizer_DivideWithOrigin(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_DivideWithOrigin_WithCalculatorClasses() => Test_Synthesizer_DivideWithOrigin(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_DivideWithOrigin(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.DivideWithOrigin), (a, b, o) => (a - o) / b + o, calculationMethodEnum);

        // MultiplyWithOrigin

        [TestMethod]
        public void Test_Synthesizer_MultiplyWithOrigin_WithRoslyn() => Test_Synthesizer_MultiplyWithOrigin(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_MultiplyWithOrigin_WithCalculatorClasses() => Test_Synthesizer_MultiplyWithOrigin(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_MultiplyWithOrigin(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(SystemPatchNames.MultiplyWithOrigin), (a, b, o) => (a - o) * b + o, calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double, double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(DimensionEnum.A), x.PatchInlet(DimensionEnum.B), x.PatchInlet(DimensionEnum.Origin)),
                func,
                DimensionEnum.A,
                _aValues,
                DimensionEnum.B,
                _bValues,
                DimensionEnum.Origin,
                _origins,
                calculationMethodEnum);
    }
}