using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
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
            var var1 = Curve("Curve1", 1, 1);
            var var2 = Curve("Curve2", 2, 2);
            var var3 = Curve("Curve3", 3, 3);
            var var4 = Curve("Curve4", 4, 4);
            var var5 = Curve("Curve5", 5, 5);
            var var6 = Curve("Curve6", 6, 6);
            var var7 = Curve("Curve7", 7, 7);
            var var8 = Curve("Curve8", 8, 8);
            var const9 = _[9];
            var const10 = _[10];

            IsNotNull(() => var1);
            IsNotNull(() => var2);
            IsNotNull(() => var3);
            IsNotNull(() => var4);
            IsNotNull(() => var5);
            IsNotNull(() => var6);
            IsNotNull(() => var7);
            IsNotNull(() => const9);
            IsNotNull(() => const10);

            // Check Classic Adder
            OperatorFactory x = TestHelper.CreateOperatorFactory(Context);
            Adder nestedAdder =
                x.Adder
                (
                    x.Adder
                    (
                        var1,
                        var2,
                        var3
                    ),
                    var4,
                    x.Adder
                    (
                        var5,
                        x.Adder
                        (
                            var6,
                            x.Adder
                            (
                                var7,
                                var8
                            )
                        )
                    ),
                    x.Adder
                    (
                        const9, 
                        const10
                    )
                );

            IsNotNull(() => nestedAdder);
            AreEqual(4, () => nestedAdder.Operands.Count);
            double nestedAdderResult = nestedAdder.Calculate(0);
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => nestedAdderResult);

            // Check Flattened Terms
            var flattenAdderTerms = new SynthWishesAccessor(this).FlattenTerms(nestedAdder);

            IsNotNull(        () => flattenAdderTerms);
            AreEqual(10,      () => flattenAdderTerms.Count);
            AreEqual(var1,    () => flattenAdderTerms[0]);
            AreEqual(var2,    () => flattenAdderTerms[1]);
            AreEqual(var3,    () => flattenAdderTerms[2]);
            AreEqual(var4,    () => flattenAdderTerms[3]);
            AreEqual(var5,    () => flattenAdderTerms[4]);
            AreEqual(var6,    () => flattenAdderTerms[5]);
            AreEqual(var7,    () => flattenAdderTerms[6]);
            AreEqual(var8,    () => flattenAdderTerms[7]);
            AreEqual(const9,  () => flattenAdderTerms[8]);
            AreEqual(const10, () => flattenAdderTerms[9]);
            
            // Check Nested Sum
            Outlet nestedSumOutlet =
                Add
                (
                    Add
                    (
                        var1,
                        var2,
                        var3
                    ),
                    var4,
                    Add
                    (
                        var5,
                        Add
                        (
                            var6,
                            Add
                            (
                                var7,
                                var8
                            )
                        )
                    ),
                    Add
                    (
                        const9,
                        const10
                    )
                );

            var sumWrapper = new Adder(nestedSumOutlet.Operator);

            AreEqual(9, () => sumWrapper.Operands.Count);

            AreEqual(var1, () => sumWrapper.Operands[0]);
            AreEqual(var2, () => sumWrapper.Operands[1]);
            AreEqual(var3, () => sumWrapper.Operands[2]);
            AreEqual(var4, () => sumWrapper.Operands[3]);
            AreEqual(var5, () => sumWrapper.Operands[4]);
            AreEqual(var6, () => sumWrapper.Operands[5]);
            AreEqual(var7, () => sumWrapper.Operands[6]);
            AreEqual(var8, () => sumWrapper.Operands[7]);
            
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
            var var2 = Curve("Curve2", 2, 2);
            var const3 = _[3];
            var var4 = Curve("Curve4", 4, 4);
            var const5 = _[5];
            var var6 = Curve("Curve6", 6, 6);
            var var7 = Curve("Curve7", 7, 7);
            var var8 = Curve("Curve8", 8, 8);

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
            IList<Outlet> flattenedFactors = new SynthWishesAccessor(this).FlattenFactors(nestedMultiply);
            
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

        [TestMethod]
        public void Test_OperatorChaining()
        {
            Play(() => Sine(A4).Multiply(0.5).Panbrello(speed: 3, depth: 0.5));
        }
    }
}