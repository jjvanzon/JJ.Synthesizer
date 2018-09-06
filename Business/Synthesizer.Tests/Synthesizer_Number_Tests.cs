using JJ.Business.Synthesizer.Configuration;
using JJ.Business.Synthesizer.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_Number_Tests
    {
        [TestMethod]
        public void Test_Synthesizer_Number_WithRoslyn() => Test_Synthesizer_Number(CalculationMethodEnum.Roslyn);

        [TestMethod]
        public void Test_Synthesizer_Number_WithCalculatorClasses() => Test_Synthesizer_Number(CalculationMethodEnum.CalculatorClasses);

        private void Test_Synthesizer_Number(CalculationMethodEnum calculationMethodEnum)
            => TestExecutor.Test0In1Out(x => x.Number(123.456), 123.456, calculationMethodEnum);
    }
}