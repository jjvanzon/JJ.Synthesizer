using JJ.Business.Synthesizer.Tests.ConfigTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class CaseTests : SynthWishes
    {
        [TestMethod]
        public void Test_Case_Key()
        { 
            var    testCase = new FrameCountWishesTests.Case();
            string key      = testCase.Key;
            Log(key);
            NotNullOrEmpty(() => key);
        }
    }
}
