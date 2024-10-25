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
            double nestedAdderResult = nestedAdder.Calculate();
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
            
            double calculatedNestedSum = sumWrapper.Calculate();
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

            double calculatedFlattenedFactors = flattenedFactors.Product(x => x.Calculate());
            AreEqual(1 * 2 * 3 * 4 * 5 * 6 * 7 * 8, () => calculatedFlattenedFactors);
        }

        [TestMethod]
        public void Test_Fluent_Notation1()
        {
            Play(() => Sine(C4).Multiply(0.5).Panbrello(speed: 3, depth: 0.9));
        }

        [TestMethod]
        public void Test_Fluent_Notation2()
        {
            Play(() => Fluent(E4).Sine.Multiply(0.5).Panbrello(speed: 3, depth: 0.9));
        }

        [TestMethod]
        public void Test_Fluent_Notation3()
        {
            Play(() => G4.Sine.Multiply(0.5).Panbrello(speed: 3, depth: 0.9));
        }
        
        [TestMethod]
        public void Test_Fluent_Notation4()
        {
            Play(() => (B4.Sine * 0.5).Panbrello(speed: 3, depth: 0.9));
        }

        [TestMethod]
        public void Test_Fluent_Chaining()
        {
            var freq = C4;

            Play(() => Multiply
                 (
                     Add
                     (
                         freq.Times(1).Sine.Times(0.50).Panbrello(speed: 3.0, depth: 0.9),
                         freq.Times(2).Sine.Times(0.08).Panbrello(speed: 2.0, depth: 0.4),
                         freq.Times(3).Sine.Times(0.04).Panbrello(speed: 2.5, depth: 0.2)
                     ),
                     Curve(@"

                       *
                           *
                                *
                                        *
                     *                              *")
                 ));
        }
    
        [TestMethod]
        public void Test_Fluent_PlayMono()
        {
            var freq = E4;

            Channel = Single;
            
            Multiply
            (
                Add
                (
                    _[freq].Multiply(1).Sine.Multiply(0.50).Tremolo(speed: 3.0, depth: 0.9),
                    _[freq].Multiply(2).Sine.Multiply(0.08).Tremolo(speed: 2.0, depth: 0.4),
                    _[freq].Multiply(3).Sine.Multiply(0.04).Tremolo(speed: 2.5, depth: 0.2)
                ),
                Curve(@"

                   *
                       *
                            *
                                    *
                 *                              *")
            ).PlayMono();
        }
 
        [TestMethod]
        public void Test_Fluent_CSharpOperators()
        {
            FluentOutlet freq = G5;

            Play(() => Multiply
                 (
                     Add
                     (
                         Sine(freq * 1).Times(0.50).Panbrello(3.0, 0.9),
                         Sine(freq * 2).Times(0.08).Panbrello(2.0, 0.4),
                         Sine(freq * 3).Times(0.04).Panbrello(2.5, 0.2)
                     ),
                     Curve(@"

                       *
                           *
                                *
                                        *
                     *                              *")
                 ));
        }
 
        
        [TestMethod]
        public void Test_Fluent_ValueChaining()
        {
            {
                var sine = A4.Sine;
            }
            {
                double freq = 440;
                var    sine = _[freq].Sine;
            }
            {
                Outlet freq = A4;
                var    sine = _[freq].Sine;
            }
            {
                FluentOutlet freq = A4;
                var          sine = freq.Sine;
            }
            {
                var freq = A4;
                var sine = freq.Sine;
            }
        }

        [TestMethod]
        public void Test_Fluent_CurveChaining()
        {
            var chain1 = Sine(G4).Curve(0, 1, 0);
            var chain2 = Sine(A4).Times(Curve(0, 1, 0));
            var chain3 = Sine(B4) * Curve(0, 1, 0).Stretch(2);
             
            PlayMono(() => chain1);
            PlayMono(() => chain2);
            PlayMono(() => chain3, duration: 2);
        }

        [TestMethod]
        public void Test_ParallelAdd()
        {
            var freq = A4;
            var volume = 0.2;

            var added = ParallelAdd
            (
                x => x.Sine(x.Value(freq.Value) * 1) * 1.0 * volume,
                x => x.Sine(x.Value(freq.Value) * 2) * 0.1 * volume,
                x => x.Sine(x.Value(freq.Value) * 3) * 0.5 * volume,
                x => x.Sine(x.Value(freq.Value) * 4) * 0.2 * volume,
                x => x.Sine(x.Value(freq.Value) * 5) * 0.3 * volume
            );

            PlayMono(() => added);
        }
    }
}