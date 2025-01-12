using System.Collections.Generic;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Factories;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Functional;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Obsolete;
using JJ.Framework.Wishes.JJ_Framework_Text_Wishes;
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
    public class TechnicalOperatorTests : MySynthWishes
    {
        FlowNode Envelope => DelayedPulseCurve.Stretch(GetAudioLength) * 0.6;
        
        public TechnicalOperatorTests()
        {
            WithStereo();
        }

        // Fluent Notation
        
        [TestMethod] public void FluentNotationTest1() => Run(FluentNotation1);
        private void FluentNotation1()
        {
            Sine(C4).Multiply(0.5).Panbrello(3, 0.9).Multiply(Envelope).Save().Play();
        }
        
        [TestMethod] public void FluentNotationTest2() => Run(FluentNotation2);
        private void FluentNotation2()
        {
            E4.Sine().Multiply(0.5).Panbrello(3, 0.9).Volume(Envelope).Save().Play();
        }
        
        [TestMethod] public void FluentNotationTest3() => Run(FluentNotation3);
        private void FluentNotation3()
        {
            G4.Sine().Multiply(0.5).Panbrello(3, 0.9).Curve(Envelope).Save().Play();
        }
        
        [TestMethod] public void FluentNotationTest4() => Run(FluentNotation4);
        private void FluentNotation4()
        {
            Save((A4.Sine() * 0.5).Panbrello(3, 0.9) * Envelope).Play();
        }
        
        [TestMethod] public void FluentChainingTest() => Run(FluentChaining);
        private void FluentChaining()
        {
            var freq = C4;

            Multiply
            (
                Add
                (
                    freq.Times(1).Sine().Times(0.50).Panbrello(3.0, 0.9),
                    freq.Times(2).Sine().Times(0.08).Panbrello(2.0, 0.4),
                    freq.Times(3).Sine().Times(0.04).Panbrello(2.5, 0.2)
                ),
                Curve(@"

                   *
                       *
                            *
                                    *
                 *                              *")
            ).Save().Play();
        }

        [TestMethod] public void FluentPlayChannelsTest() => Run(FluentPlayChannels);
        private void FluentPlayChannels()
        {
            var freq = E4;

            Multiply
            (
                Add
                (
                    _[freq].Multiply(1).Sine().Multiply(0.80).Panbrello(3.0, 0.9),
                    _[freq].Multiply(2).Sine().Multiply(0.08).Panbrello(2.0, 0.4),
                    _[freq].Multiply(3).Sine().Multiply(0.04).Panbrello(2.5, 0.2)
                ),
                Curve(@"

                   *
                       *
                            *
                                    *
                 *                              *")
            ).PlayChannels();
        }

        [TestMethod] public void FluentCSharpOperatorsTest() => Run(FluentCSharpOperators);
        private void FluentCSharpOperators()
        {
            var freq = G4;

            Multiply
            (
                Add
                (
                    (Sine(freq * 1) * 0.50).Panbrello(3.0, 0.9),
                    (Sine(freq * 2) * 0.08).Panbrello(2.0, 0.4),
                    (Sine(freq * 3) * 0.04).Panbrello(2.5, 0.2)
                ),
                Curve(@"

                  *
                      *
                           *
                                   *
                *                              *")
            ).Play().Save();
        }

        [TestMethod] public void FluentValueChainingTest() => Run(FluentValueChaining);
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

        [TestMethod] public void FluentCurveChainingTest() => Run(FluentCurveChaining);
        private void FluentCurveChaining()
        {
            var chain1 = Sine(G4).Curve(0, 1, 0).Panbrello();
            var chain2 = Sine(A4).Times(Curve(0, 1, 0)).Panbrello();
            var chain3 = Sine(C5) * Curve(0, 1, 0).Times(1.2).Panbrello();

            Save(chain1).Play("Play1");
            Save(chain2).Play("Play2");
            Save(chain3).Play("Play3");
        }

        // Regression (used to error)
        
        [TestMethod] public void MonoSampleInStereoContextTest() => Run(MonoSampleInStereoContext);
        private void MonoSampleInStereoContext()
        {
            WithAudioLength(3);
            WithStereo();
            
            var sample = Sample(GetViolin16BitMono44100WavStream(), bytesToSkip: 64);
            
            var shaped = sample.Stretch(3.4)
                               .Curve(1, 0.9, 0.8, 0.2, 0.1, 0)
                               .Panbrello(7, 0.3)
                               .Echo(count: 8, magnitude: 0.5, delay: 0.2)
                               .AddEchoDuration(8, 0.2);

            Play(shaped).Save();
        }
                
        // Optimization
        
        [TestMethod] public void NestedSumFlatteningTest() => Run(NestedSumFlattening);
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

        [TestMethod] public void NestedMultiplicationOptimizationTest() => Run(NestedMultiplicationOptimization);
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

        // Complexity
        
        [TestMethod] public void ComplexityTest() => Run(TestComplexity); 
        void TestComplexity()
        {
            WithDiskCache(false);
            
            {
                var accessor = new AdditiveTestsAccessor(new AdditiveTests());
                TestComplexity(accessor.Metallophone(A4));
                TestComplexity(accessor.MetallophoneChord);
                TestComplexity(accessor.MetallophoneJingle());
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
                
                accessor.WithLeft();
                TestComplexity(accessor.Detunica1(A4));
                TestComplexity(accessor.Detunica2());
                TestComplexity(accessor.Detunica3());
                TestComplexity(accessor.Detunica4());
                TestComplexity(accessor.Detunica5());
                TestComplexity(accessor.DetunicaBass());
                TestComplexity(accessor.DetunicaJingle);
                TestComplexity(accessor.Vibraphase());
                TestComplexity(accessor.VibraphaseChord);
                
                accessor.WithRight();
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

        void TestComplexity(FlowNode flowNode)
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
        }
        
        [TestMethod] public void TestComplexityOnBuff()
        {
            // Test on Buff only once for efficiency, because it requires materialization.
            {
                var  accessor = new FMTestsAccessor(new FMTests());
                Buff buff     = null;
                accessor.Run(() => accessor.Jingle().AfterRecord(x => buff = x.Buff));
                TestBuffComplexity(buff);
            }
        }
        
        void TestBuffComplexity(Buff buff)
        {
            IsNotNull(() => buff);
            {
                string stringify = buff.Stringify();
                IsNotNull(() => stringify);
                int complexityOld = stringify.CountLines();
                int complexity    = buff.Complexity();
                AreEqual(complexityOld, () => complexity);
            }
            
            AudioFileOutput audioFileOutput = buff.UnderlyingAudioFileOutput;
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