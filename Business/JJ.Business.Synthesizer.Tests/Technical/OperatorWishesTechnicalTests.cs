using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
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
            // Arrange
            var cloackedValue1 = CurveIn("Curve1", 1, 1);
            var cloackedValue2 = CurveIn("Curve2", 2, 2);
            var cloackedValue3 = CurveIn("Curve3", 3, 3);
            var cloackedValue4 = CurveIn("Curve4", 4, 4);
            var cloackedValue5 = CurveIn("Curve5", 5, 5);
            var cloackedValue6 = CurveIn("Curve6", 6, 6);
            var cloackedValue7 = CurveIn("Curve7", 7, 7);
            var cloackedValue8 = CurveIn("Curve8", 8, 8);
            var constValue9 = _[9];
            var constValue10 = _[10];

            IsNotNull(() => cloackedValue1);
            IsNotNull(() => cloackedValue2);
            IsNotNull(() => cloackedValue3);
            IsNotNull(() => cloackedValue4);
            IsNotNull(() => cloackedValue5);
            IsNotNull(() => cloackedValue6);
            IsNotNull(() => cloackedValue7);
            IsNotNull(() => constValue9);
            IsNotNull(() => constValue10);

            // Check Classic Adder
            Adder nestedAdder =
                Adder
                (
                    Adder
                    (
                        cloackedValue1,
                        cloackedValue2,
                        cloackedValue3
                    ),
                    cloackedValue4,
                    Adder
                    (
                        cloackedValue5,
                        Adder
                        (
                            cloackedValue6,
                            Adder
                            (
                                cloackedValue7,
                                cloackedValue8
                            )
                        )
                    ),
                    Adder
                    (
                        constValue9, 
                        constValue10
                    )
                );

            IsNotNull(() => nestedAdder);
            AreEqual(4, () => nestedAdder.Operands.Count);
            double nestedAdderResult = nestedAdder.Result.Calculate(0);
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => nestedAdderResult);

            // Check Flattened Terms
            var flattenAdderTerms = OperatorExtensionsWishes.FlattenTerms(nestedAdder);

            IsNotNull(() => flattenAdderTerms);
            AreEqual(10,             () => flattenAdderTerms.Count);
            AreEqual(cloackedValue1, () => flattenAdderTerms[0]);
            AreEqual(cloackedValue2, () => flattenAdderTerms[1]);
            AreEqual(cloackedValue3, () => flattenAdderTerms[2]);
            AreEqual(cloackedValue4, () => flattenAdderTerms[3]);
            AreEqual(cloackedValue5, () => flattenAdderTerms[4]);
            AreEqual(cloackedValue6, () => flattenAdderTerms[5]);
            AreEqual(cloackedValue7, () => flattenAdderTerms[6]);
            AreEqual(cloackedValue8, () => flattenAdderTerms[7]);
            AreEqual(constValue9, () => flattenAdderTerms[8]);
            AreEqual(constValue10, () => flattenAdderTerms[9]);
            
            // Check Nested Sum
            Outlet nestedSumOutlet =
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
                    ),
                    Add
                    (
                        constValue9,
                        constValue10
                    )
                );

            var sumWrapper = new Adder(nestedSumOutlet.Operator);

            AreEqual(9, () => sumWrapper.Operands.Count);

            AreEqual(cloackedValue1, () => sumWrapper.Operands[0]);
            AreEqual(cloackedValue2, () => sumWrapper.Operands[1]);
            AreEqual(cloackedValue3, () => sumWrapper.Operands[2]);
            AreEqual(cloackedValue4, () => sumWrapper.Operands[3]);
            AreEqual(cloackedValue5, () => sumWrapper.Operands[4]);
            AreEqual(cloackedValue6, () => sumWrapper.Operands[5]);
            AreEqual(cloackedValue7, () => sumWrapper.Operands[6]);
            AreEqual(cloackedValue8, () => sumWrapper.Operands[7]);
            
            double? constant = sumWrapper.Operands[8].AsConst();
            IsNotNull(() => constant);
            AreEqual(9 + 10, () => constant.Value);
            
            double calculatedNestedSum = sumWrapper.Result.Calculate(0);
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => calculatedNestedSum);
        }
    }
}