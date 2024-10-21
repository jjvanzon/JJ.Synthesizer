using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Tests.Helpers;

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Long")]
    public class LongRunningTests
    {
        [TestMethod]
        public void FM_Jingle() => new FMTests().FM_Jingle_RunTest();
    
        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica_Jingle() => new ModulationTests().Detunica_Jingle_RunTest();
    
        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_Detunica_Jingle_Mono() => new ModulationTests().Detunica_Jingle_RunTest_Mono();

        /// <inheritdoc cref="docs._detunica" />
        [TestMethod]
        public void Test_DetunicaBass() => new ModulationTests().DetunicaBass_RunTest();
    }
}
