using System;
using JJ.Business.Synthesizer.Configuration;
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
        private static readonly double[] _valuesFrom0To2Pi = MathHelper.SpreadDoubles(0, MathHelper.TWO_PI, 9);
        private static readonly double[] _valuesFromMinus1To1 = MathHelper.SpreadDoubles(-1, 1, 9);

        // Sin

        [TestMethod]
        public void Test_Synthesizer_Sin_WithRoslyn() => Test_Synthesizer_Sin(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Sin_WithCalculatorClasses() => Test_Synthesizer_Sin(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Sin(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(Sin), Math.Sin, calculationMethodEnum);

        // Cos

        [TestMethod]
        public void Test_Synthesizer_Cos_WithRoslyn() => Test_Synthesizer_Cos(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Cos_WithCalculatorClasses() => Test_Synthesizer_Cos(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Cos(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(Cos), Math.Cos, calculationMethodEnum);

        // Tan

        [TestMethod]
        public void Test_Synthesizer_Tan_WithRoslyn() => Test_Synthesizer_Tan(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Tan_WithCalculatorClasses() => Test_Synthesizer_Tan(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Tan(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(Tan), Math.Tan, calculationMethodEnum);

        // SinH

        [TestMethod]
        public void Test_Synthesizer_SinH_WithRoslyn() => Test_Synthesizer_SinH(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_SinH_WithCalculatorClasses() => Test_Synthesizer_SinH(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_SinH(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(SinH), Math.Sinh, calculationMethodEnum);

        // CosH

        [TestMethod]
        public void Test_Synthesizer_CosH_WithRoslyn() => Test_Synthesizer_CosH(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_CosH_WithCalculatorClasses() => Test_Synthesizer_CosH(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_CosH(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(CosH), Math.Cosh, calculationMethodEnum);

        // TanH

        [TestMethod]
        public void Test_Synthesizer_TanH_WithRoslyn() => Test_Synthesizer_TanH(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_TanH_WithCalculatorClasses() => Test_Synthesizer_TanH(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_TanH(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(TanH), Math.Tanh, calculationMethodEnum);

        // ArcSin

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithRoslyn() => Test_Synthesizer_ArcSin(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithCalculatorClasses() => Test_Synthesizer_ArcSin(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_ArcSin(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_FromMinus1To1(nameof(ArcSin), Math.Asin, calculationMethodEnum);

        // ArcCos

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithRoslyn() => Test_Synthesizer_ArcCos(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithCalculatorClasses() => Test_Synthesizer_ArcCos(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_ArcCos(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_FromMinus1To1(nameof(ArcCos), Math.Acos, calculationMethodEnum);

        // ArcTan

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithRoslyn() => Test_Synthesizer_ArcTan(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithCalculatorClasses() => Test_Synthesizer_ArcTan(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_ArcTan(CalculationMethodEnum calculationMethodEnum)
            => ExecuteTest_From0To2Pi(nameof(ArcTan), Math.Atan, calculationMethodEnum);

        // Generalized Methods

        private void ExecuteTest_From0To2Pi(string systemPatchName, Func<double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestHelper.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                _valuesFrom0To2Pi,
                calculationMethodEnum);

        private void ExecuteTest_FromMinus1To1(string systemPatchName, Func<double, double> func, CalculationMethodEnum calculationMethodEnum)
            => TestHelper.ExecuteTest(
                x => x.New(systemPatchName, x.PatchInlet(TestConstants.DEFAULT_DIMENSION_ENUM)),
                func,
                _valuesFromMinus1To1,
                calculationMethodEnum);
    }
}