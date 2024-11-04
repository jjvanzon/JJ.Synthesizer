using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Long")]
    public class LongRunningTests
    {
        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica_Jingle() => new ModulationTests().Detunica_Jingle_RunTest();
    }
}
