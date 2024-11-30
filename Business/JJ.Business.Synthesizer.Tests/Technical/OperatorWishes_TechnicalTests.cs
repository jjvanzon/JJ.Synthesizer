using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Helpers.TestHelper;
using static JJ.Framework.Testing.AssertHelper;
// ReSharper disable UnusedVariable
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class OperatorWishes_TechnicalTests : MySynthWishes
    {
        FlowNode Envelope => DelayedPulseCurve.Stretch(GetAudioLength) * 0.5;

        [TestMethod]
        public void NestedSumFlatteningTest() => new OperatorWishes_TechnicalTests().NestedSumFlattening();

        private void NestedSumFlattening()
        {
            WithMathBoost();

            // Arrange
            var var1    = Curve(1, 1).SetName("Curve1");
            var var2    = Curve(2, 2).SetName("Curve2");
            var var3    = Curve(3, 3).SetName("Curve3");
            var var4    = Curve(4, 4).SetName("Curve4");
            var var5    = Curve(5, 5).SetName("Curve5");
            var var6    = Curve(6, 6).SetName("Curve6");
            var var7    = Curve(7, 7).SetName("Curve7");
            var var8    = Curve(8, 8).SetName("Curve8");
            var const9  = _[9];
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

            IsNotNull(() => flattenAdderTerms);
            AreEqual(10,                 () => flattenAdderTerms.Count);
            AreEqual(var1.UnderlyingOutlet, () => flattenAdderTerms[0].UnderlyingOutlet);
            AreEqual(var2.UnderlyingOutlet, () => flattenAdderTerms[1].UnderlyingOutlet);
            AreEqual(var3.UnderlyingOutlet, () => flattenAdderTerms[2].UnderlyingOutlet);
            AreEqual(var4.UnderlyingOutlet, () => flattenAdderTerms[3].UnderlyingOutlet);
            AreEqual(var5.UnderlyingOutlet, () => flattenAdderTerms[4].UnderlyingOutlet);
            AreEqual(var6.UnderlyingOutlet, () => flattenAdderTerms[5].UnderlyingOutlet);
            AreEqual(var7.UnderlyingOutlet, () => flattenAdderTerms[6].UnderlyingOutlet);
            AreEqual(var8.UnderlyingOutlet, () => flattenAdderTerms[7].UnderlyingOutlet);
            AreEqual(const9,             () => flattenAdderTerms[8].UnderlyingOutlet);
            AreEqual(const10,            () => flattenAdderTerms[9].UnderlyingOutlet);

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
            WithMathBoost();
            
            // Arrange
            var const1 = _[1];
            var var2   = Curve(2, 2).SetName("Curve2");
            var const3 = _[3];
            var var4   = Curve(4, 4).SetName("Curve4");
            var const5 = _[5];
            var var6   = Curve(6, 6).SetName("Curve6");
            var var7   = Curve(7, 7).SetName("Curve7");
            var var8   = Curve(8, 8).SetName("Curve8");

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
            IList<FlowNode> flattenedFactors = new SynthWishesAccessor(this).FlattenFactors(_[nestedMultiply]);

            IsNotNull(() => flattenedFactors);
            AreEqual(6, () => flattenedFactors.Count);

            // Operator creation reversed the order.
            AreEqual(var2.UnderlyingOutlet, () => flattenedFactors[4].UnderlyingOutlet);
            AreEqual(var4.UnderlyingOutlet, () => flattenedFactors[3].UnderlyingOutlet);
            AreEqual(var6.UnderlyingOutlet, () => flattenedFactors[2].UnderlyingOutlet);
            AreEqual(var7.UnderlyingOutlet, () => flattenedFactors[1].UnderlyingOutlet);
            AreEqual(var8.UnderlyingOutlet, () => flattenedFactors[0].UnderlyingOutlet);

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
            WithShortDuration();
            
            Save(() => Sine(C4).Multiply(0.5).Panbrello(speed: 3, depth: 0.9).Multiply(Envelope)).Play();
        }

        [TestMethod]
        public void FluentNotationTest2() => new OperatorWishes_TechnicalTests().FluentNotation2();

        private void FluentNotation2()
        {
            WithShortDuration();
            
            Save(() => Fluent(E4).Sine().Multiply(0.5).Panbrello(speed: 3, depth: 0.9).Volume(Envelope)).Play();
        }

        [TestMethod]
        public void FluentNotationTest3() => new OperatorWishes_TechnicalTests().FluentNotation3();

        private void FluentNotation3()
        {
            WithShortDuration();
            
            Save(() => G4.Sine().Multiply(0.5).Panbrello(speed: 3, depth: 0.9).Curve(Envelope)).Play();
        }

        [TestMethod]
        public void FluentNotationTest4() => new OperatorWishes_TechnicalTests().FluentNotation4();

        private void FluentNotation4()
        {
            WithShortDuration();
            
            Save(() => (B4.Sine() * 0.5).Panbrello(speed: 3, depth: 0.9) * Envelope).Play();
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
                         freq.Times(1).Sine().Times(0.50).Panbrello(speed: 3.0, depth: 0.9),
                         freq.Times(2).Sine().Times(0.08).Panbrello(speed: 2.0, depth: 0.4),
                         freq.Times(3).Sine().Times(0.04).Panbrello(speed: 2.5, depth: 0.2)
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
            
            Cache(
                () =>
                    Multiply
                    (
                        Add
                        (
                            _[freq].Multiply(1).Sine().Multiply(0.50).Tremolo(speed: 3.0, depth: 0.9),
                            _[freq].Multiply(2).Sine().Multiply(0.08).Tremolo(speed: 2.0, depth: 0.4),
                            _[freq].Multiply(3).Sine().Multiply(0.04).Tremolo(speed: 2.5, depth: 0.2)
                        ),
                        Curve(@"

                           *
                               *
                                    *
                                            *
                         *                              *")
                    ).ChannelPlay()
            );
        }

        [TestMethod]
        public void FluentCSharpOperatorsTest() => new OperatorWishes_TechnicalTests().FluentCSharpOperators();

        private void FluentCSharpOperators()
        {
            FlowNode freq = G5;

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
                var sine = A4.Sine();
            }
            {
                double freq = 440;
                var    sine = _[freq].Sine();
            }
            {
                Outlet freq = A4;
                var    sine = _[freq].Sine();
            }
            {
                FlowNode freq = A4;
                var      sine = freq.Sine();
            }
            {
                var freq = A4;
                var sine = freq.Sine();
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
        public void MonoSampleInStereoContextTest() => new OperatorWishes_TechnicalTests().MonoSampleInStereoContext();

        private void MonoSampleInStereoContext()
        {
            var sample = Sample(GetViolin16BitMono44100WavStream(), bytesToSkip: 64).Stretch(3).Curve(1, 0.9, 0.8, 0.2, 0.1, 0);

            WithStereo().Play(() => sample);
        }

        [TestMethod]
        public void ComplexityTest() => new OperatorWishes_TechnicalTests().TestComplexity();

        private void TestComplexity()
        {
            WithCacheToDisk(false);
            
            {
                var accessor = new AdditiveTestsAccessor(new AdditiveTests());
                TestComplexity(accessor.Metallophone(A4));
                TestComplexity(accessor.MetallophoneJingle);
            }
            {
                var accessor = new FMTestsAccessor(new FMTests());
                TestComplexity(accessor.Flute1());
                TestComplexity(accessor.Flute2());
                TestComplexity(accessor.Flute3());
                TestComplexity(accessor.Flute4());
                TestComplexity(accessor.Organ());
                TestComplexity(accessor.Pad());
                TestComplexity(accessor.Horn());
                TestComplexity(accessor.Trombone());
                TestComplexity(accessor.ElectricNote());
                TestComplexity(accessor.RippleBass());
                TestComplexity(accessor.RippleNote_SharpMetallic());
                TestComplexity(accessor.RippleSound_Clean());
                TestComplexity(accessor.RippleSound_FantasyEffect());
                TestComplexity(accessor.RippleSound_CoolDouble());
                TestComplexity(accessor.Create_FM_Noise_Beating());
                TestComplexity(accessor.FluteMelody1);
                TestComplexity(accessor.FluteMelody2);
                TestComplexity(accessor.OrganChords);
                TestComplexity(accessor.PadChords());
                TestComplexity(accessor.PadChords2());
                TestComplexity(accessor.HornMelody1);
                TestComplexity(accessor.HornMelody2);
                TestComplexity(accessor.TromboneMelody1);
                TestComplexity(accessor.TromboneMelody2);
                TestComplexity(accessor.RippleBassMelody2);
                TestComplexity(accessor.Jingle());
            }
            {
                var accessor = new ModulationTestsAccessor(new ModulationTests());
                WithStereo();
                TestComplexity(accessor.Detunica1(A4));
                TestComplexity(accessor.Detunica2());
                TestComplexity(accessor.Detunica3());
                TestComplexity(accessor.Detunica4());
                TestComplexity(accessor.Detunica5());
                TestComplexity(accessor.DetunicaBass());
                TestComplexity(accessor.DetunicaJingle);
                TestComplexity(accessor.Vibraphase());
                TestComplexity(accessor.VibraphaseChord);
            }
        }

        private void TestComplexity(FlowNode flowNode)
        {
            IsNotNull(() => flowNode);
            {
                string stringify = flowNode.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = flowNode.Complexity;
                AreEqual(complexityOld, () => complexity);
            }

            IsNotNull(() => flowNode.UnderlyingOutlet);
            Outlet outlet = flowNode.UnderlyingOutlet;
            {
                string stringify = outlet.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = outlet.Complexity();
                AreEqual(complexityOld, () => complexity);
            }

            IsNotNull(() => outlet.Operator);
            Operator op = outlet.Operator;
            {
                string stringify = op.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = op.Complexity();
                AreEqual(complexityOld, () => complexity);
            }

            // For the Inlet case: Complexity requires detouring through another operator.
            var add = Add(flowNode, 1);

            // Ensure the Add operation and its associated Operator and Inlets are initialized correctly.
            IsNotNull(() => add);
            IsNotNull(() => add.UnderlyingOperator);
            IsNotNull(() => add.UnderlyingOperator.Inlets);
            Assert.IsTrue(add.UnderlyingOperator.Inlets.Count > 0);

            // Access the first Inlet to evaluate its complexity.
            Inlet inlet = add.UnderlyingOperator.Inlets[0];
            {
                string stringify = inlet.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = inlet.Complexity();
                // The expected complexity excludes 1 line,
                // as the old method counted an extra line for the added nesting level.
                int expectedComplexity = complexityOld - 1;
                AreEqual(expectedComplexity, () => complexity);
            }

            Buff result = Cache(flowNode);
            IsNotNull(() => result);
            {
                string stringify = result.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = result.Complexity();
                AreEqual(complexityOld, () => complexity);
            }

            AudioFileOutput audioFileOutput = result.UnderlyingAudioFileOutput;
            IsNotNull(() => audioFileOutput);
            {
                string stringify = audioFileOutput.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = audioFileOutput.Complexity();
                AreEqual(complexityOld, () => complexity);
            }

            IsNotNull(() => audioFileOutput.AudioFileOutputChannels);
            foreach (var audioFileOutputChannel in audioFileOutput.AudioFileOutputChannels)
            {
                IsNotNull(() => audioFileOutputChannel);
                {
                    string stringify     = audioFileOutputChannel.Stringify();
                    int    complexityOld = stringify.CountLines();
                    int    complexity    = audioFileOutputChannel.Complexity();
                    AreEqual(complexityOld, () => complexity);
                }
            }
        }
    }
}