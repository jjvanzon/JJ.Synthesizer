using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class OperatorWishes_TechnicalTests : SynthWishes
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
            double nestedAdderResult = nestedAdder.Calculate(0);
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => nestedAdderResult);

            // Check Flattened Terms
            var flattenAdderTerms = OperatorExtensionsWishesAccessor.FlattenTerms(nestedAdder);

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
            
            double calculatedNestedSum = sumWrapper.Calculate(0);
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => calculatedNestedSum);
        }
 
        [TestMethod]
        public void TestNestedMultiplicationOptimization()
        {
            // Arrange
            var const1 = _[1];
            var var2 = CurveIn("Curve2", 2, 2);
            var const3 = _[3];
            var var4 = CurveIn("Curve4", 4, 4);
            var const5 = _[5];
            var var6 = CurveIn("Curve6", 6, 6);
            var var7 = CurveIn("Curve7", 7, 7);
            var var8 = CurveIn("Curve8", 8, 8);

            IsNotNull(() => const1);
            IsNotNull(() => var2);
            IsNotNull(() => const3);
            IsNotNull(() => var4);
            IsNotNull(() => const5);
            IsNotNull(() => var6);
            IsNotNull(() => var7);
            IsNotNull(() => var8);

            // Check Nested Multiplication
            Outlet nestedMultiply =
                Multiply
                (
                    Multiply
                    (
                        Multiply(const1, var2),
                        Multiply
                        (
                            const3,
                            Multiply(var4, const5)
                        )
                    ),
                    Multiply
                    (
                        var6, 
                        Multiply(var7, var8)
                    )
                );

            // Check Optimized Factors
            IList<Outlet> flattenedFactors = OperatorExtensionsWishesAccessor.FlattenFactors(nestedMultiply);
            
            IsNotNull(() => flattenedFactors);
            AreEqual(6, () => flattenedFactors.Count);
            
            // Operator creation reversed the order.
            AreEqual(var2,   () => flattenedFactors[4]);
            AreEqual(var4,   () => flattenedFactors[3]);
            AreEqual(var6,   () => flattenedFactors[2]);
            AreEqual(var7,   () => flattenedFactors[1]);
            AreEqual(var8,   () => flattenedFactors[0]);
            
            double? constant = flattenedFactors[5].AsConst();
            IsNotNull(() => constant);
            AreEqual(1 * 3 * 5, () => constant.Value);
            
            IsNotNull(() => nestedMultiply);
            double multiplyResult = nestedMultiply.Calculate(time: 0);
            AreEqual(1 * 2 * 3 * 4 * 5 * 6 * 7 * 8, () => multiplyResult);

            double calculatedFlattenedFactors = flattenedFactors.Product(x => x.Calculate(0));
            AreEqual(1 * 2 * 3 * 4 * 5 * 6 * 7 * 8, () => calculatedFlattenedFactors);
        }
    }
}