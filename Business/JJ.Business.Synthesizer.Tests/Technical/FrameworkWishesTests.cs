using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.FrameworkWishesAccessor;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class FrameworkWishesTests
    {
        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_100Bytes_ShowsBytes()
            => AreEqual("100 bytes", () => PrettyByteCount(100));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_9kB_ShowsBytes()
            => AreEqual("9216 bytes", () => PrettyByteCount(9 * 1024));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10kBMinus1_ShowsBytes()
            => AreEqual("10239 bytes", () => PrettyByteCount(10 * 1024 - 1));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10kB_ShowsKB()
            => AreEqual("10 kB", () => PrettyByteCount(10 * 1024));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_9MB_ShowsKB()
            => AreEqual("9216 kB", () => PrettyByteCount(9 * 1024 * 1024));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10kBMinus1_ShowsKB()
            => AreEqual("10240 kB", () => PrettyByteCount(10 * 1024 * 1024 - 1));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10MB_ShowsMB()
            => AreEqual("10 MB", () => PrettyByteCount(10 * 1024 * 1024));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_9GB_ShowsMB()
            => AreEqual("9216 MB", () => PrettyByteCount((long)9 * 1024 * 1024 * 1024));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10GBMinus1_ShowsMB()
            => AreEqual("10240 MB", () => PrettyByteCount((long)10 * 1024 * 1024 * 1024 - 1));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10GB_ShowsGB()
            => AreEqual("10 GB", () => PrettyByteCount((long)10 * 1024 * 1024 * 1024));

        [TestMethod]
        public void Test_FrameworkWishes_PrettyByteCount_10000GB_ShowsGB()
            => AreEqual("10000 GB", () => PrettyByteCount((long)10000 * 1024 * 1024 * 1024));
    }
}