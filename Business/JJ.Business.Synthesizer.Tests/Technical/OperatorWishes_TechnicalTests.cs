using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Framework.Testing.AssertHelper;
// ReSharper disable UnusedVariable
// ReSharper disable ExplicitCallerInfoArgument

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
            var var1 = Curve(1, 1).SetName("Curve1");
            var var2 = Curve(2, 2).SetName("Curve2");
            var var3 = Curve(3, 3).SetName("Curve3");
            var var4 = Curve(4, 4).SetName("Curve4");
            var var5 = Curve(5, 5).SetName("Curve5");
            var var6 = Curve(6, 6).SetName("Curve6");
            var var7 = Curve(7, 7).SetName("Curve7");
            var var8 = Curve(8, 8).SetName("Curve8");
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
            double nestedAdderResult = _[nestedAdder].Calculate();
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => nestedAdderResult);

            // Check Flattened Terms
            var flattenAdderTerms = new SynthWishesAccessor(this).FlattenTerms(_[nestedAdder]);

            IsNotNull(        () => flattenAdderTerms);
            AreEqual(10,          () => flattenAdderTerms.Count);
            AreEqual(var1.WrappedOutlet, () => flattenAdderTerms[0].WrappedOutlet);
            AreEqual(var2.WrappedOutlet, () => flattenAdderTerms[1].WrappedOutlet);
            AreEqual(var3.WrappedOutlet, () => flattenAdderTerms[2].WrappedOutlet);
            AreEqual(var4.WrappedOutlet, () => flattenAdderTerms[3].WrappedOutlet);
            AreEqual(var5.WrappedOutlet, () => flattenAdderTerms[4].WrappedOutlet);
            AreEqual(var6.WrappedOutlet, () => flattenAdderTerms[5].WrappedOutlet);
            AreEqual(var7.WrappedOutlet, () => flattenAdderTerms[6].WrappedOutlet);
            AreEqual(var8.WrappedOutlet, () => flattenAdderTerms[7].WrappedOutlet);
            AreEqual(const9,      () => flattenAdderTerms[8].WrappedOutlet);
            AreEqual(const10,     () => flattenAdderTerms[9].WrappedOutlet);
            
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
            
            double calculatedNestedSum = _[sumWrapper].Calculate();
            AreEqual(1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10, () => calculatedNestedSum);
        }
 
        [TestMethod]
        public void TestNestedMultiplicationOptimization()
        {
            // Arrange
            var const1 = _[1];
            var var2 = WithName("Curve2").Curve(2, 2).SetName("Curve2");
            var const3 = _[3];
            var var4 = WithName("Curve4").Curve(4, 4);
            var const5 = _[5];
            var var6 = WithName("Curve6").Curve(6, 6);
            var var7 = WithName("Curve7").Curve(7, 7);
            var var8 = WithName("Curve8").Curve(8, 8);

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
            IList<FluentOutlet> flattenedFactors = new SynthWishesAccessor(this).FlattenFactors(_[nestedMultiply]);
            
            IsNotNull(() => flattenedFactors);
            AreEqual(6, () => flattenedFactors.Count);
            
            // Operator creation reversed the order.
            AreEqual(var2.WrappedOutlet, () => flattenedFactors[4].WrappedOutlet);
            AreEqual(var4.WrappedOutlet, () => flattenedFactors[3].WrappedOutlet);
            AreEqual(var6.WrappedOutlet, () => flattenedFactors[2].WrappedOutlet);
            AreEqual(var7.WrappedOutlet, () => flattenedFactors[1].WrappedOutlet);
            AreEqual(var8.WrappedOutlet, () => flattenedFactors[0].WrappedOutlet);
            
            double? constant = flattenedFactors[5].AsConst;
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
            SaveAndPlay(() => Sine(C4).Multiply(0.5).Panbrello(speed: 3, depth: 0.9));
        }

        [TestMethod]
        public void Test_Fluent_Notation2()
        {
            SaveAndPlay(() => Fluent(E4).Sine.Multiply(0.5).Panbrello(speed: 3, depth: 0.9));
        }

        [TestMethod]
        public void Test_Fluent_Notation3()
        {
            SaveAndPlay(() => G4.Sine.Multiply(0.5).Panbrello(speed: 3, depth: 0.9));
        }
        
        [TestMethod]
        public void Test_Fluent_Notation4()
        {
            SaveAndPlay(() => (B4.Sine * 0.5).Panbrello(speed: 3, depth: 0.9));
        }

        [TestMethod]
        public void Test_Fluent_Chaining()
        {
            var freq = C4;

            SaveAndPlay(() => Multiply
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

            Center();
            
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

            SaveAndPlay(() => Multiply
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
             
            Mono().SaveAndPlay(() => chain1);
            Mono().SaveAndPlay(() => chain2);
            Mono().WithAudioLength(2).SaveAndPlay(() => chain3);
        }
        
        [TestMethod]
        public void Test_ParallelAdd_NormalAdd_ForComparison()
        {
            WithParallelEnabled();
            
            var duration = 0.1;

            var add = Add
            (
                Curve(0.1, 0.1).SetName("Const Curve 0.1"),
                Curve(0.2, 0.2).SetName("Const Curve 0.2"),
                Curve(0.3, 0.3).SetName("Const Curve 0.3")
            );

            double addedValue = add.Calculate(duration / 2);
            
            AreEqual(0.1 + 0.2 + 0.3, () => addedValue);
            
            Mono().WithAudioLength(duration).Save(() => add);
        }

        [TestMethod]
        public void Test_ParallelAdd_WithConstSignal()
        {
            WithParallelEnabled();
            
            // Arrange
            var duration = 0.1;
            var tolerance = 0.001;

            // Create Entities
            var adder = WithAudioLength(duration).ParallelAdd
            (
                // Values higher than 1 seem to be clipped.
                () => WithName("Const Curve 0.1").Curve(0.1, 0.1),
                () => WithName("Const Curve 0.2").Curve(0.2, 0.2),
                () => WithName("Const Curve 0.3").Curve(0.3, 0.3)
            );

            // Assert Entities
            IsNotNull(() => adder);
            IsNotNull(() => adder.WrappedOutlet);
            IsNotNull(() => adder.WrappedOutlet.Operator);
            IsTrue(() => adder.WrappedOutlet.IsAdder());
            IsTrue(() => adder.WrappedOutlet.Operator.IsAdder());
            AreEqual("Adder", () => adder.WrappedOutlet.Operator.OperatorTypeName);

            IsNotNull(() => adder.WrappedOutlet.Operator.Inlets);
            var addOperands = adder.WrappedOutlet.Operator.Inlets.Select(x => x.Input).ToList();
            AreEqual(3, () => addOperands.Count);

            foreach (var addOperand in addOperands)
            {
                IsNotNull(() => addOperand);
                IsNotNull(() => addOperand.Operator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample.Bytes);
                IsTrue(() => addOperand.IsSample());
                IsTrue(() => addOperand.Operator.IsSample());
                AreEqual("SampleOperator", () => addOperand.Operator.OperatorTypeName);
            }

            NotEqual(addOperands[0], () => addOperands[1]);
            NotEqual(addOperands[1], () => addOperands[2]);
            
            // Check Bytes Array, Read as Int16 Values
            foreach (var addOperand in addOperands)
            {
                Sample sample = addOperand.Operator.AsSampleOperator.Sample;

                AreEqual(Wav, () => sample.GetAudioFileFormatEnum());
                AreEqual(SampleDataTypeEnum.Int16, () => sample.GetSampleDataTypeEnum());
                AreEqual(SpeakerSetupEnum.Mono, () => sample.GetSpeakerSetupEnum());
                AreEqual(44,  () => sample.GetHeaderLength());

                using (var stream = new MemoryStream(sample.Bytes))
                {
                    stream.Position = 44; // Skip header
                    
                    using (var reader = new BinaryReader(stream))
                    {
                        short firstValue = reader.ReadInt16();
                        
                        while (stream.Position < stream.Length)
                        {
                            short nextValue = reader.ReadInt16();
                            AreEqual(firstValue, () => nextValue);
                        }
                    }
                }
            }

            // Calculate Values
            double adderResult = adder.Calculate(duration / 2);
            
            double operandValue1 = addOperands[0].Calculate(duration / 2);
            double operandValue2 = addOperands[1].Calculate(duration / 2);
            double operandValue3 = addOperands[2].Calculate(duration / 2);

            var operandValuesSorted = new [] { operandValue1, operandValue2, operandValue3 }.OrderBy(x => x).ToArray();

            Console.WriteLine($"{new { operandValue1, operandValue2, operandValue3 }}");
            
            // Assert Values
            Assert.AreEqual(0.1 + 0.2 + 0.3, operandValue1 + operandValue2 + operandValue3, tolerance);
            Assert.AreEqual(0.1, operandValuesSorted[0], tolerance);
            Assert.AreEqual(0.2, operandValuesSorted[1], tolerance);
            Assert.AreEqual(0.3, operandValuesSorted[2], tolerance);
            Assert.AreEqual(0.1 + 0.2 + 0.3, adderResult, tolerance);
        }

        [TestMethod]
        public void Test_ParallelAdd_WithConstSignal_WithPreviewPartials()
        {
            // Arrange
            WithParallelEnabled();
            WithPreviewParallels();

            var duration = 0.1;

            // Act
            var adder = WithAudioLength(duration).ParallelAdd
            (
                // Values higher than 1 seem to be clipped.
                () => WithName("Const Curve 0.1").Curve(0.1, 0.1),
                () => WithName("Const Curve 0.2").Curve(0.2, 0.2),
                () => WithName("Const Curve 0.3").Curve(0.3, 0.3)
            );

            // Assert
            IsNotNull(() => adder);
            IsNotNull(() => adder.WrappedOutlet);
            IsNotNull(() => adder.WrappedOutlet.Operator);
            IsTrue(() => adder.WrappedOutlet.IsAdder());
            IsTrue(() => adder.WrappedOutlet.Operator.IsAdder());
            AreEqual("Adder", () => adder.WrappedOutlet.Operator.OperatorTypeName);

            IsNotNull(() => adder.WrappedOutlet.Operator.Inlets);
            var addOperands = adder.WrappedOutlet.Operator.Inlets.Select(x => x.Input).ToList();
            AreEqual(3, () => addOperands.Count);

            foreach (var addOperand in addOperands)
            {
                IsNotNull(() => addOperand);
                IsNotNull(() => addOperand.Operator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample);
                IsNotNull(() => addOperands[0].Operator.AsSampleOperator.Sample.Bytes);
                IsTrue(() => addOperand.IsSample());
                IsTrue(() => addOperand.Operator.IsSample());
                AreEqual("SampleOperator", () => addOperand.Operator.OperatorTypeName);
            }

            NotEqual(addOperands[0], () => addOperands[1]);
            NotEqual(addOperands[1], () => addOperands[2]);
            
            // Don't assert values. A setting can insert a delay, messing with the test values.
        }
        
        [TestMethod]
        public void Test_ParallelAdd_WithSinePartials()
        {
            WithParallelEnabled();
            
            var freq = A4;

            WithAudioLength(0.6);
            
            var added = ParallelAdd
            (
                volume: 1 / 1.5,
                () => Sine(freq * 1) * 1.0,
                () => Sine(freq * 2) * 0.2,
                () => Sine(freq * 3) * 0.7
            );

            Mono().SaveAndPlay(() => added);
            
        }
        
        [TestMethod]
        public void Test_ParallelAdd_SinePartials_PreviewParallels()
        {
            var freq     = A4;
            var volume   = 1 / 1.5;
            var duration = 0.6;

            WithParallelEnabled();
            WithPreviewParallels();
            WithAudioLength(duration);
            WithName();
            
            var added = ParallelAdd
            (
                volume,
                () => Sine(freq * 1) * 1.0,
                () => Sine(freq * 2) * 0.2,
                () => Sine(freq * 3) * 0.7
            );

            Mono().SaveAndPlay(() => added);
        }
    }
}