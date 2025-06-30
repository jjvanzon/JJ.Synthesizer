using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Text.Core.StringHelperCore;
using static JJ.Framework.Testing.Legacy.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class FrameworkWishesTests
    {
        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_100Bytes_ShowsBytes()
            => AreEqual("100 bytes", () => PrettyByteCount(100));
        
        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_1000Bytes_ShowsBytes()
            => AreEqual("1000 bytes", () => PrettyByteCount(1000));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_5kBMinus1_ShowsBytes()
            => AreEqual("5119 bytes", () => PrettyByteCount(5 * 1024 - 1));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_5kBPlus1_ShowskB()
            => AreEqual("5 kB", () => PrettyByteCount(5 * 1024 + 1));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_10kB_ShowskB()
            => AreEqual("10 kB", () => PrettyByteCount(10 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_100kB_ShowskB()
            => AreEqual("100 kB", () => PrettyByteCount(100 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_1000kB_ShowskB()
            => AreEqual("1000 kB", () => PrettyByteCount(1000 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_5MBMinus1_ShowskB()
            => AreEqual("5120 kB", () => PrettyByteCount(5 * 1024 * 1024 - 1));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_5MBPlus1_ShowsMB()
            => AreEqual("5 MB", () => PrettyByteCount(5 * 1024 * 1024 + 1));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_100MB_ShowsMB()
            => AreEqual("100 MB", () => PrettyByteCount(100 * 1024 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_1000MB_ShowsMB()
            => AreEqual("1000 MB", () => PrettyByteCount(1000 * 1024 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_5GBMinus1_ShowsMB()
            => AreEqual("5120 MB", () => PrettyByteCount((long)5 * 1024 * 1024 * 1024 - 1));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_5GBPlus1_ShowsGB()
            => AreEqual("5 GB", () => PrettyByteCount((long)5 * 1024 * 1024 * 1024 + 1));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_10GB_ShowsGB()
            => AreEqual("10 GB", () => PrettyByteCount((long)10 * 1024 * 1024 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_100GB_ShowsGB()
            => AreEqual("100 GB", () => PrettyByteCount((long)100 * 1024 * 1024 * 1024));
        
        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_1000GB_ShowsGB()
            => AreEqual("1000 GB", () => PrettyByteCount((long)1000 * 1024 * 1024 * 1024));

        [TestMethod]
        public void Test_StringWishes_PrettyByteCount_10000GB_ShowsGB()
            => AreEqual("10000 GB", () => PrettyByteCount((long)10000 * 1024 * 1024 * 1024));
    }
}