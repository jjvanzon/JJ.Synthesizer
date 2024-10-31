using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.docs;

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Long")]
    public class LongRunningTests
    {
        /// <inheritdoc cref="_detunica" />
        [TestMethod]
        public void Test_Detunica_Jingle() => new ModulationTests().Detunica_Jingle_RunTest();
    }
}
