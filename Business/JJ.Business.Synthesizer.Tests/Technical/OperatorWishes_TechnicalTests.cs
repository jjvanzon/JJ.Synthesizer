using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
using static JJ.Framework.Testing.AssertHelper;
// ReSharper disable UnusedVariable
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class OperatorWishes_TechnicalTests : SynthWishes
    {
        FluentOutlet Envelope => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));

        [TestMethod]
        public void NestedSumFlatteningTest() => new OperatorWishes_TechnicalTests().NestedSumFlattening();

        private void NestedSumFlattening()
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
            OperatorFactory x = CreateOperatorFactory(Context);
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
        public void NestedMultiplicationOptimizationTest() => new OperatorWishes_TechnicalTests().NestedMultiplicationOptimization();

        private void NestedMultiplicationOptimization()
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
        public void FluentNotationTest1() => new OperatorWishes_TechnicalTests().FluentNotation1();

        private void FluentNotation1()
        {
            Save(() => Sine(C4).Multiply(0.5).Panbrello(speed: 3, depth: 0.9).Multiply(Envelope)).Play();
        }

        [TestMethod]
        public void FluentNotationTest2() => new OperatorWishes_TechnicalTests().FluentNotation2();

        private void FluentNotation2()
        {
            Save(() => Fluent(E4).Sine.Multiply(0.5).Panbrello(speed: 3, depth: 0.9).Volume(Envelope)).Play();
        }

        [TestMethod]
        public void FluentNotationTest3() => new OperatorWishes_TechnicalTests().FluentNotation3();

        private void FluentNotation3()
        {
            Save(() => G4.Sine.Multiply(0.5).Panbrello(speed: 3, depth: 0.9).Curve(Envelope)).Play();
        }

        [TestMethod]
        public void FluentNotationTest4() => new OperatorWishes_TechnicalTests().FluentNotation4();

        private void FluentNotation4()
        {
            Save(() => (B4.Sine * 0.5).Panbrello(speed: 3, depth: 0.9) * Envelope).Play();
        }

        [TestMethod]
        public void FluentChainingTest() => new OperatorWishes_TechnicalTests().FluentChaining();

        private void FluentChaining()
        {
            var freq = C4;

            Save(() => Multiply
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
                 )).Play();
        }

        [TestMethod]
        public void FluentPlayMonoTest() => new OperatorWishes_TechnicalTests().FluentPlayMono();

        private void FluentPlayMono()
        {
            var freq = E4;

            WithCenter();

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
        public void FluentCSharpOperatorsTest() => new OperatorWishes_TechnicalTests().FluentCSharpOperators();

        private void FluentCSharpOperators()
        {
            FluentOutlet freq = G5;

            Save(() => Multiply
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
                 )).Play();
        }

        [TestMethod]
        public void FluentValueChainingTest() => new OperatorWishes_TechnicalTests().FluentValueChaining();

        private void FluentValueChaining()
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
        public void FluentCurveChainingTest() => new OperatorWishes_TechnicalTests().FluentCurveChaining();

        private void FluentCurveChaining()
        {
            var chain1 = Sine(G4).Curve(0, 1, 0);
            var chain2 = Sine(A4).Times(Curve(0, 1, 0));
            var chain3 = Sine(B4) * Curve(0, 1, 0).Stretch(2);

            WithMono().Save(() => chain1).Play();
            WithMono().Save(() => chain2).Play();
            WithMono().WithAudioLength(2).Save(() => chain3).Play();
        }

        [TestMethod]
        public void ParallelAdd_NormalAdd_ForComparison_Test() => new OperatorWishes_TechnicalTests().ParallelAdd_NormalAdd_ForComparison();

        private void ParallelAdd_NormalAdd_ForComparison()
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

            WithMono().WithAudioLength(duration).Save(() => add);
        }

        [TestMethod]
        public void ParallelAdd_WithConstSignal_Test() => new OperatorWishes_TechnicalTests().ParallelAdd_WithConstSignal();

        private void ParallelAdd_WithConstSignal()
        {
            var accessor = new SynthWishesAccessor(this);

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

            accessor.RunParallelsRecursive(adder);

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
                AreEqual(SampleDataTypeEnum.Float32, () => sample.GetSampleDataTypeEnum());
                AreEqual(SpeakerSetupEnum.Mono, () => sample.GetSpeakerSetupEnum());
                AreEqual(44,  () => sample.GetHeaderLength());

                using (var stream = new MemoryStream(sample.Bytes))
                {
                    stream.Position = 44; // Skip header

                    using (var reader = new BinaryReader(stream))
                    {
                        float firstValue = reader.ReadSingle();

                        while (stream.Position < stream.Length)
                        {
                            float nextValue = reader.ReadSingle();
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
        public void ParallelAdd_WithConstSignal_WithPreviewPartials_Test() => new OperatorWishes_TechnicalTests().ParallelAdd_WithConstSignal_WithPreviewPartials();

        private void ParallelAdd_WithConstSignal_WithPreviewPartials()
        {
            var accessor = new SynthWishesAccessor(this);

            // Arrange
            WithParallelEnabled();
            WithPlayParallels();

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

            accessor.RunParallelsRecursive(adder);

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
        public void ParallelAdd_WithSinePartials_Test() => new OperatorWishes_TechnicalTests().ParallelAdd_WithSinePartials();

        private void ParallelAdd_WithSinePartials()
        {
            WithParallelEnabled();

            var freq = A4;

            var added = Envelope * WithName().ParallelAdd
            (
                () => Sine(freq * 1) * 1.0,
                () => Sine(freq * 2) * 0.2,
                () => Sine(freq * 3) * 0.7
            );

            WithMono().Play(() => added);
        }

        [TestMethod]
        public void ParallelAdd_SinePartials_PreviewParallels_Test() => new OperatorWishes_TechnicalTests().ParallelAdd_SinePartials_PreviewParallels();

        private void ParallelAdd_SinePartials_PreviewParallels()
        {
            var freq = A4;

            WithParallelEnabled();
            WithPlayParallels();
            WithName();

            var added = Envelope * ParallelAdd
            (
                () => Sine(freq * 1) * 1.0,
                () => Sine(freq * 2) * 0.2,
                () => Sine(freq * 3) * 0.7
            );

            WithMono().Play(() => added);
        }

        [TestMethod]
        public void MonoSampleInStereoContextTest() => new OperatorWishes_TechnicalTests().MonoSampleInStereoContext();

        private void MonoSampleInStereoContext()
        {
            var sample = Sample(GetViolin16BitMono44100WavStream(), bytesToSkip: 64).Stretch(3).Curve(1, 0.9, 0.8, 0.4, 0.2, 0);

            WithStereo().Play(() => sample);
        }

        [TestMethod]
        public void ComplexityTest() => new OperatorWishes_TechnicalTests().Complexity();

        /// <summary>
        /// NOTE: Outcommented code lines still fail.
        /// </summary>
        private void Complexity()
        {
            var accessor = new FMTestsAccessor(new FMTests());
            
            Complexity(accessor.Flute1());
            Complexity(accessor.Flute2());
            Complexity(accessor.Flute3());
            Complexity(accessor.Flute4());
            //Complexity(accessor.Pad());
            Complexity(accessor.Horn());
            Complexity(accessor.Trombone());
            //Complexity(accessor.ElectricNote());
            Complexity(accessor.RippleBass());
            Complexity(accessor.FluteMelody1);
            Complexity(accessor.FluteMelody2);
            //Complexity(accessor.PadChords());
            Complexity(accessor.HornMelody1);
            Complexity(accessor.HornMelody2);
            Complexity(accessor.TromboneMelody1);
            Complexity(accessor.TromboneMelody2);
            Complexity(accessor.RippleBassMelody2);
            //Complexity(accessor.Jingle());
        }

        private void Complexity(FluentOutlet fluentOutlet)
        {
            IsNotNull(() => fluentOutlet);
            {
                string fluentOutletStringify     = fluentOutlet.Stringify();
                int    fluentOutletComplexityOld = fluentOutletStringify.CountLines();
                int    fluentOutletComplexity    = fluentOutlet.Complexity;
                AreEqual(fluentOutletComplexityOld, () => fluentOutletComplexity);
            }

            IsNotNull(() => fluentOutlet.WrappedOutlet);
            Outlet outlet = fluentOutlet.WrappedOutlet;
            {
                string outletStringify     = outlet.Stringify();
                int    outletComplexityOld = outletStringify.CountLines();
                int    outletComplexity    = outlet.Complexity();
                AreEqual(outletComplexityOld, () => outletComplexity);
            }

            IsNotNull(() => outlet.Operator);
            Operator op = outlet.Operator;
            {
                string operatorStringify     = op.Stringify();
                int    operatorComplexityOld = operatorStringify.CountLines();
                int    operatorComplexity    = fluentOutlet.Operator.Complexity();
                AreEqual(operatorComplexityOld, () => operatorComplexity);
            }
        }

    }
}