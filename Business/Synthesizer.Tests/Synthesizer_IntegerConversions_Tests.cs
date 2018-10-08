using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable once RedundantUsingDirective
using static JJ.Business.Synthesizer.Helpers.SystemPatchNames;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_IntegerConversions_Tests
    {
        private static readonly double[] _xValues = MathHelper.SpreadDoubles(-5, 5, 20);

        // Ceiling

        [TestMethod]
        public void Test_Synthesizer_Ceiling_WithRoslyn() => Test_Synthesizer_Ceiling(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Ceiling_WithCalculatorClasses() => Test_Synthesizer_Ceiling(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Ceiling(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(Ceiling), Math.Ceiling, calculationEngineEnum);

        // Floor

        [TestMethod]
        public void Test_Synthesizer_Floor_WithRoslyn() => Test_Synthesizer_Floor(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Floor_WithCalculatorClasses() => Test_Synthesizer_Floor(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Floor(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(Floor), Math.Floor, calculationEngineEnum);

        // Truncate

        [TestMethod]
        public void Test_Synthesizer_Truncate_WithRoslyn() => Test_Synthesizer_Truncate(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Truncate_WithCalculatorClasses() => Test_Synthesizer_Truncate(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Truncate(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest(nameof(Truncate), Math.Truncate, calculationEngineEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                TestConstants.DEFAULT_DIMENSION_ENUM,
                _xValues,
                calculationEngineEnum);
    }
}