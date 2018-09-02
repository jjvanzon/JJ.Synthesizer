using System;
using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Helpers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Data.Synthesizer.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable LocalizableElement

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class TrigonometryTests
    {
        private const DimensionEnum DIMENSION_ENUM = DimensionEnum.Number;

        [TestMethod]
        public void Test_Synthesizer_Sin_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Sin,
                x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_Sin_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Sin,
                x => x.New(nameof(SystemPatchNames.Sin), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_Cos_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Cos,
                x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_Cos_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Cos,
                x => x.New(nameof(SystemPatchNames.Cos), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_Tan_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Tan,
                x => x.New(nameof(SystemPatchNames.Tan), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_Tan_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Tan,
                x => x.New(nameof(SystemPatchNames.Tan), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_SinH_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Sinh,
                x => x.New(nameof(SystemPatchNames.SinH), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_SinH_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Sinh,
                x => x.New(nameof(SystemPatchNames.SinH), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_CosH_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Cosh,
                x => x.New(nameof(SystemPatchNames.CosH), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_CosH_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Cosh,
                x => x.New(nameof(SystemPatchNames.CosH), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_TanH_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Tanh,
                x => x.New(nameof(SystemPatchNames.TanH), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_TanH_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Tanh,
                x => x.New(nameof(SystemPatchNames.TanH), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Asin,
                x => x.New(nameof(SystemPatchNames.ArcSin), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_ArcSin_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Asin,
                x => x.New(nameof(SystemPatchNames.ArcSin), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Acos,
                x => x.New(nameof(SystemPatchNames.ArcCos), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_ArcCos_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Acos,
                x => x.New(nameof(SystemPatchNames.ArcCos), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithRoslyn()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.Roslyn,
                Math.Atan,
                x => x.New(nameof(SystemPatchNames.ArcTan), x.PatchInlet(DIMENSION_ENUM)));

        [TestMethod]
        public void Test_Synthesizer_ArcTan_WithCalculatorClasses()
            => ExecuteTrigonometryTest(
                CalculationMethodEnum.CalculatorClasses,
                Math.Atan,
                x => x.New(nameof(SystemPatchNames.ArcTan), x.PatchInlet(DIMENSION_ENUM)));

        private void ExecuteTrigonometryTest(
            CalculationMethodEnum calculationMethodEnum,
            Func<double, double> func,
            Func<OperatorFactory, Outlet> operatorCreationDelegate)
            => TestHelper.ExecuteTest(
                calculationMethodEnum,
                DIMENSION_ENUM,
                func,
                operatorCreationDelegate,
                new[]
                {
                    Math.PI * 0.00,
                    Math.PI * 0.25,
                    Math.PI * 0.50,
                    Math.PI * 0.75,
                    Math.PI * 1.00,
                    Math.PI * 1.25,
                    Math.PI * 1.50,
                    Math.PI * 1.75,
                    Math.PI * 2.00
                });
    }
}