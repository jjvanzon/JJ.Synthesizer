using JJ.Framework.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class NameWishesTests
    {
        [TestMethod]
        public void TestFileNameWithoutExtension()
        {
            string input = @"C:\Repositories\JJ.Synthesizer\Business\JJ.Business.Synthesizer.Tests\bin\Release\Additive_Metallophone_Jingle.wav";

            string expected = "Additive_Metallophone_Jingle";
            
            AssertHelper.AreEqual(expected, () => Path.GetFileNameWithoutExtension(input));
        }
        
        [TestMethod]
        public void TestPrettifyFileNameExtensionRemoval()
        {
            string input = @"C:\Repositories\JJ.Synthesizer\Business\JJ.Business.Synthesizer.Tests\bin\Release\Additive_Metallophone_Jingle.wav";

            string expected = @"Additive Metallophone Jingle";
            
            AssertHelper.AreEqual(expected, () => NameWishes.PrettifyName(input));
        }
    }
}
