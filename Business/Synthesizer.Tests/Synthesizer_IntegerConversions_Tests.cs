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

        [TestMethod]
        public void Test_Synthesizer_Ceiling_Roslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(Ceiling), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Ceiling,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Ceiling_CalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(Ceiling), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Ceiling,
                _xValues,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_Floor_Roslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(Floor), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Floor,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Floor_CalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(Floor), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Floor,
                _xValues,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_Truncate_Roslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(Truncate), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Truncate,
                _xValues,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Truncate_CalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(Truncate), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Truncate,
                _xValues,
                CalculationMethodEnum.CalculatorClasses);
    }
}