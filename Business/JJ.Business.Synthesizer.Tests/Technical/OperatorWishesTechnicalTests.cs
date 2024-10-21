using System.Linq;
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
            var cloackedValue6 = CurveIn(6, 6);
            var cloackedValue7 = CurveIn(7, 7);
            var cloackedValue8 = CurveIn(8, 8);

            IsNotNull(() => cloackedValue1);
            IsNotNull(() => cloackedValue2);
            IsNotNull(() => cloackedValue3);
            IsNotNull(() => cloackedValue4);
            IsNotNull(() => cloackedValue5);
            IsNotNull(() => cloackedValue6);
            IsNotNull(() => cloackedValue7);
            IsNotNull(() => cloackedValue8);

            var nestedSum = 
                Sum
                (
                    Sum
                    (
                        cloackedValue1, 
                        cloackedValue2, 
                        cloackedValue3
                    ),
                    cloackedValue4, 
                    Sum
                    (
                        cloackedValue5, 
                        Sum
                        (
                            cloackedValue6, 
                            Add
                            (
                                cloackedValue7,
                                cloackedValue8
                            )
                        )
                    )
                );

             var flattenSumOperands = OperatorExtensionsWishes.FlattenTerms(nestedSum);

            IsNotNull(() => flattenSumOperands);
            AreEqual(8, () => flattenSumOperands.Count);
            AreEqual(cloackedValue1, () => flattenSumOperands[0]);
            AreEqual(cloackedValue2, () => flattenSumOperands[1]);
            AreEqual(cloackedValue3, () => flattenSumOperands[2]);
            AreEqual(cloackedValue4, () => flattenSumOperands[3]);
            AreEqual(cloackedValue5, () => flattenSumOperands[4]);
            AreEqual(cloackedValue6, () => flattenSumOperands[5]);
            AreEqual(cloackedValue7, () => flattenSumOperands[6]);
            AreEqual(cloackedValue8, () => flattenSumOperands[7]);

            //IsNotNull(() => nestedSum);
            //AreEqual(5, () => nestedSum.Operator.Inlets.Count);
        }
    }
}
