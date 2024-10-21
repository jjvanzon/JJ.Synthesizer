using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class OperatorWishesTechnicalTests : SynthWishes
    {
        [TestMethod]
        public void TestNestedSumFlattening()
        {
            var cloackedValue1 = CurveIn(1, 1);
            var cloackedValue2 = CurveIn(2, 2);
            var cloackedValue3 = CurveIn(3, 3);
            var cloackedValue4 = CurveIn(4, 4);
            var cloackedValue5 = CurveIn(5, 5);

            IsNotNull(() => cloackedValue1);
            IsNotNull(() => cloackedValue2);
            IsNotNull(() => cloackedValue3);
            IsNotNull(() => cloackedValue4);
            IsNotNull(() => cloackedValue5);

            var nestedSum = Sum(Sum(cloackedValue1, cloackedValue2), cloackedValue3);

            IsNotNull(() => nestedSum);
            AreEqual(3, () => nestedSum.Operator.Inlets.Count);
        }
    }
}
