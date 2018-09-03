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
            => TestExecutor.TestOneValue(x => x.New(nameof(E)), Math.E, CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantE_WithCalculatorClasses()
            => TestExecutor.TestOneValue(x => x.New(nameof(E)), Math.E, CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_ConstantPi_WithRoslyn()
            => TestExecutor.TestOneValue(x => x.New(nameof(Pi)), Math.PI, CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantPi_WithCalculatorClasses()
            => TestExecutor.TestOneValue(x => x.New(nameof(Pi)), Math.PI, CalculationMethodEnum.CalculatorClasses);

        [TestMethod]
        public void Test_Synthesizer_ConstantTwoPi_WithRoslyn()
            => TestExecutor.TestOneValue(x => x.New(nameof(TwoPi)), MathHelper.TWO_PI, CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_ConstantTwoPi_WithCalculatorClasses()
            => TestExecutor.TestOneValue(x => x.New(nameof(TwoPi)), MathHelper.TWO_PI, CalculationMethodEnum.CalculatorClasses);
    }
}