using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class SynthesizerTrigonometryTests
    {
        private static readonly double[] _valuesFrom0To2Pi = MathHelper.SpreadDoubles(0, MathHelper.TWO_PI, 9);
        private static readonly double[] _valuesFromMinus1To1 = MathHelper.SpreadDoubles(-1, 1, 9);

        [TestMethod]
        public void Test_Synthesizer_Sin_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Sin,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sin_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Sin,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_Cos_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Cos,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Cos_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Cos,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_Tan_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Tan), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Tan,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Tan_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.Tan), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Tan,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_SinH_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.SinH), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Sinh,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_SinH_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.SinH), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Sinh,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_CosH_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.CosH), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Cosh,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_CosH_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.CosH), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Cosh,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_TanH_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.TanH), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Tanh,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_TanH_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.TanH), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Tanh,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.ArcSin), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Asin,
                _valuesFromMinus1To1,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.ArcSin), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Asin,
                _valuesFromMinus1To1,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.ArcCos), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Acos,
                _valuesFromMinus1To1,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.ArcCos), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Acos,
                _valuesFromMinus1To1,
                CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithRoslyn()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.ArcTan), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Atan,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithCalculatorClasses()
            => TestHelper.TestMultipleValues(
                x => x.New(nameof(SystemPatchNames.ArcTan), x.PatchInlet(TestHelper.DEFAULT_DIMENSION_ENUM)),
                Math.Atan,
                _valuesFrom0To2Pi,
                CalculationMethodEnum.CalculatorClasses);
    }
}