using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Framework.Mathematics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable once RedundantUsingDirective
using static JJ.Business.Synthesizer.Helpers.SystemPatchNames;
// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Trigonometry_Tests
    {
        private static readonly double[] _valuesFrom0To2Pi = MathHelper.SpreadDoubles(0, MathHelper.TWO_PI, 35);
        private static readonly double[] _valuesFromMinus1To1 = MathHelper.SpreadDoubles(-1, 1, 9);

        // Sin

        [TestMethod]
        public void Test_Synthesizer_Sin_WithRoslyn() => Test_Synthesizer_Sin(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sin_WithCalculatorClasses() => Test_Synthesizer_Sin(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Sin(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(Sin), Math.Sin, calculationEngineEnum);

        // Cos

        [TestMethod]
        public void Test_Synthesizer_Cos_WithRoslyn() => Test_Synthesizer_Cos(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Cos_WithCalculatorClasses() => Test_Synthesizer_Cos(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Cos(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(Cos), Math.Cos, calculationEngineEnum);

        // Tan

        [TestMethod]
        public void Test_Synthesizer_Tan_WithRoslyn() => Test_Synthesizer_Tan(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Tan_WithCalculatorClasses() => Test_Synthesizer_Tan(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_Tan(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(Tan), Math.Tan, calculationEngineEnum);

        // SinH

        [TestMethod]
        public void Test_Synthesizer_SinH_WithRoslyn() => Test_Synthesizer_SinH(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_SinH_WithCalculatorClasses() => Test_Synthesizer_SinH(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_SinH(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(SinH), Math.Sinh, calculationEngineEnum);

        // CosH

        [TestMethod]
        public void Test_Synthesizer_CosH_WithRoslyn() => Test_Synthesizer_CosH(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_CosH_WithCalculatorClasses() => Test_Synthesizer_CosH(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_CosH(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(CosH), Math.Cosh, calculationEngineEnum);

        // TanH

        [TestMethod]
        public void Test_Synthesizer_TanH_WithRoslyn() => Test_Synthesizer_TanH(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_TanH_WithCalculatorClasses() => Test_Synthesizer_TanH(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_TanH(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(TanH), Math.Tanh, calculationEngineEnum);

        // ArcSin

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithRoslyn() => Test_Synthesizer_ArcSin(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithCalculatorClasses() => Test_Synthesizer_ArcSin(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_ArcSin(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_FromMinus1To1(nameof(ArcSin), Math.Asin, calculationEngineEnum);

        // ArcCos

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithRoslyn() => Test_Synthesizer_ArcCos(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithCalculatorClasses() => Test_Synthesizer_ArcCos(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_ArcCos(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_FromMinus1To1(nameof(ArcCos), Math.Acos, calculationEngineEnum);

        // ArcTan

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithRoslyn() => Test_Synthesizer_ArcTan(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithCalculatorClasses() => Test_Synthesizer_ArcTan(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_ArcTan(CalculationEngineEnum calculationEngineEnum)
            => ExecuteTest_From0To2Pi(nameof(ArcTan), Math.Atan, calculationEngineEnum);

        // Generalized Methods

        private void ExecuteTest_From0To2Pi(string systemPatchName, Func<double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                TestConstants.DEFAULT_DIMENSION_ENUM,
                _valuesFrom0To2Pi,
                calculationEngineEnum,
                new TestOptions(mustPlot: true, plotLineCount: 9));

        private void ExecuteTest_FromMinus1To1(string systemPatchName, Func<double, double> func, CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                TestConstants.DEFAULT_DIMENSION_ENUM,
                _valuesFromMinus1To1,
                calculationEngineEnum,
                new TestOptions(mustPlot: true));
    }
}