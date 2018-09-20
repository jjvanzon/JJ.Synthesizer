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
    public class Synthesizer_Constants_Tests
    {
        [TestMethod]
        public void Test_Synthesizer_ConstantE_WithRoslyn()
            => Test_Synthesizer_ConstantE(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantE_WithCalculatorClasses()
            => Test_Synthesizer_ConstantE(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_ConstantE(CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(x => x.New(nameof(E)), Math.E, calculationEngineEnum);

        // Pi

        [TestMethod]
        public void Test_Synthesizer_ConstantPi_WithRoslyn()
            => Test_Synthesizer_ConstantPi(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantPi_WithCalculatorClasses()
            => Test_Synthesizer_ConstantPi(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_ConstantPi(CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(x => x.New(nameof(Pi)), Math.PI, calculationEngineEnum);

        // TwoPi

        [TestMethod]
        public void Test_Synthesizer_ConstantTwoPi_WithRoslyn()
            => Test_Synthesizer_ConstantTwoPi(CalculationEngineEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantTwoPi_WithCalculatorClasses()
            => Test_Synthesizer_ConstantTwoPi(CalculationEngineEnum.CalculatorClasses);

        private void Test_Synthesizer_ConstantTwoPi(CalculationEngineEnum calculationEngineEnum)
            => TestExecutor.ExecuteTest(x => x.New(nameof(TwoPi)), MathHelper.TWO_PI, calculationEngineEnum);
    }
}