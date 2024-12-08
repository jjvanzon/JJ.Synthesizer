using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Long")]
    public class LongTests
    {
        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        [TestCategory("Long")]
        public void Test_Detunica_Jingle() => new ModulationTests().Detunica_Jingle_RunTest();
    }
}
