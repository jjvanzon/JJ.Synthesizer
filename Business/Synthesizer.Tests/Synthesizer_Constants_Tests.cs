using System;
using JJ.Business.Synthesizer.Configuration;
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
            => Test_Synthesizer_ConstantE(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantE_WithCalculatorClasses()
            => Test_Synthesizer_ConstantE(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_ConstantE(CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test0In1Out(x => x.New(nameof(E)), Math.E, calculationMethodEnum);

        // Pi

        [TestMethod]
        public void Test_Synthesizer_ConstantPi_WithRoslyn()
            => Test_Synthesizer_ConstantPi(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantPi_WithCalculatorClasses()
            => Test_Synthesizer_ConstantPi(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_ConstantPi(CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test0In1Out(x => x.New(nameof(Pi)), Math.PI, calculationMethodEnum);

        // TwoPi

        [TestMethod]
        public void Test_Synthesizer_ConstantTwoPi_WithRoslyn()
            => Test_Synthesizer_ConstantTwoPi(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantTwoPi_WithCalculatorClasses()
            => Test_Synthesizer_ConstantTwoPi(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_ConstantTwoPi(CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test0In1Out(x => x.New(nameof(TwoPi)), MathHelper.TWO_PI, calculationMethodEnum);
    }
}