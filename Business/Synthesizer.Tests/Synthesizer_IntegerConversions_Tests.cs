// ReSharper disable once RedundantUsingDirective

using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Helpers.SystemPatchNames;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_IntegerConversions_Tests
    {
        private static readonly double[] _xValues = MathHelper.SpreadDoubles(-5, 5, 20);

        // Ceiling

        [TestMethod]
        public void Test_Synthesizer_Ceiling_WithRoslyn() => Test_Synthesizer_Ceiling(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Ceiling_WithCalculatorClasses() => Test_Synthesizer_Ceiling(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Ceiling(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(Ceiling), Math.Ceiling, calculationMethodEnum);

        // Floor

        [TestMethod]
        public void Test_Synthesizer_Floor_WithRoslyn() => Test_Synthesizer_Floor(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Floor_WithCalculatorClasses() => Test_Synthesizer_Floor(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Floor(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(Floor), Math.Floor, calculationMethodEnum);

        // Truncate

        [TestMethod]
        public void Test_Synthesizer_Truncate_WithRoslyn() => Test_Synthesizer_Truncate(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Truncate_WithCalculatorClasses() => Test_Synthesizer_Truncate(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Truncate(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest(nameof(Truncate), Math.Truncate, calculationMethodEnum);

        // Generalized Method

        private void ExecuteTest(string systemPatchName, Func<double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test1In1Out(
                x => x.New(systemPatchName, x.PatchInlet(TestExecutor.DEFAULT_DIMENSION_ENUM)),
                func,
                _xValues,
                calculationMethodEnum);
    }
}