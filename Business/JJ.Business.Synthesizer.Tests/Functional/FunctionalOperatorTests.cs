using System;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.NameWishes;

// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Functional")]
    public class FunctionalOperatorTests : MySynthWishes
    {
        FlowNode Envelope => RecorderCurve;

        public FunctionalOperatorTests()
        {
            WithMono();
        }

        // Vibrato/Tremolo Tests

        /// <inheritdoc cref="docs._vibrato" />
        [TestMethod] public void Vibrato_Test() => Run(Vibrato);
        /// <inheritdoc cref="docs._vibrato" />
        void Vibrato() => WithMono().VibratoFreq(A4).Sine().Volume(Envelope * 0.9).Save().Play();
        
        /// <inheritdoc cref="docs._tremolo" />
        [TestMethod] public void Tremolo_Test() => Run(Tremolo);
        /// <inheritdoc cref="docs._tremolo" />
        void Tremolo() => WithMono().Sine(C5).Tremolo(4, 0.5).Curve(Envelope).Volume(0.3).Save().Play();

        // Panning Tests

        [TestMethod] public void Panning_ConstSignal_ConstPanningAsDouble_Test() => Run(Panning_ConstSignal_ConstPanningAsDouble);
        void Panning_ConstSignal_ConstPanningAsDouble()
        {
            WithStereo();

            // Arrange
            FlowNode fixedValues()
            {
                if (IsLeft) return _[0.8];
                if (IsRight) return _[0.6];
                return default;
            }

            double panning = 0.5;
            Outlet panned;

            // Act
            WithLeft();
            panned = Panning(fixedValues(), panning);
            double outputLeftValue = panned.Calculate(time: 0);

            WithRight();
            panned = Panning(fixedValues(), panning);
            double outputRightValue = panned.Calculate(time: 0);

            // Assert
            double expectedLeft  = 0.8 * (1 - panning); // 0.8 * 0.5 = 0.4
            double expectedRight = 0.6 * panning; // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeft,  () => outputLeftValue);
            AssertHelper.AreEqual(expectedRight, () => outputRightValue);

            // Diagnostics (get code coverage)
            WithCenter();
            Assert.IsNull(fixedValues());
        }

        [TestMethod] public void Panning_ConstSignal_ConstPanningAsOperator_Test() => Run(Panning_ConstSignal_ConstPanningAsOperator);
        void Panning_ConstSignal_ConstPanningAsOperator()
        {
            WithStereo();

            // Arrange
            FlowNode TestSignal()
            {
                if (IsLeft ) return _[0.8];
                if (IsRight) return _[0.6];
                return default;
            }

            double panningValue = 0.5;

            // Act

            FlowNode panned;

            WithLeft();
            panned = Panning(TestSignal(), panningValue);
            double leftValue = panned.Calculate(time: 0);

            WithRight();
            panned = Panning(TestSignal(), panningValue);
            double rightValue = panned.Calculate(time: 0);

            // Assert
            double expectedLeftValue  = 0.8 * (1 - panningValue); // 0.8 * 0.5 = 0.4
            double expectedRightValue = 0.6 * panningValue; // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeftValue,  () => leftValue);
            AssertHelper.AreEqual(expectedRightValue, () => rightValue);

            // Diagnostics (get code coverage)
            WithCenter();
            Assert.IsNull(TestSignal());
        }

        [TestMethod] public void Panning_SineWaveSignal_ConstPanningAsDouble_Test() => Run(Panning_SineWaveSignal_ConstPanningAsDouble);
        void Panning_SineWaveSignal_ConstPanningAsDouble()
        {
            WithStereo();

            // Arrange
            var freq    = E5;
            var sine    = Sine(freq).Curve(1, 1, 0);
            var panning = 0.25;

            // Act
            var originalChannel = GetChannel;
            try
            {
                Outlet panned;

                WithLeft();
                panned = Panning(sine, panning);
                double maxAmplitudeLeft = panned.Calculate(time: 0.25 / (double)freq);
                double minValueLeft = panned.Calculate(time: 0.75 / (double)freq);

                WithRight();
                panned = Panning(sine, panning);
                double maxAmplitudeRight = panned.Calculate(time: 0.25 / (double)freq);
                double minValueRight = panned.Calculate(time: 0.75 / (double)freq);
                
                // Assert
                AssertHelper.AreEqual(0.75,  () => maxAmplitudeLeft);
                AssertHelper.AreEqual(-0.75, () => minValueLeft);
                AssertHelper.AreEqual(0.25,  () => maxAmplitudeRight);
                AssertHelper.AreEqual(-0.25, () => minValueRight);
            }
            finally
            {
                WithChannel(originalChannel);
            }
            
            sine.Panning(panning).Curve(Envelope).Save().Play();
        }

        [TestMethod] public void Panning_SineWaveSignal_DynamicPanning_Test() => Run(Panning_SineWaveSignal_DynamicPanning);
        void Panning_SineWaveSignal_DynamicPanning()
        {
            var sine = Sine(G5) * Envelope;
            var panning = Curve(@"
                                    *
                                *
                            *
                        *
                    *
                *
            *");

            WithStereo().Panning(sine, panning).Save().Play();
        }

        // Panbrello Tests

        [TestMethod] public void Panbrello_DefaultSpeedAndDepth_Test() => Run(Panbrello_DefaultSpeedAndDepth);
        void Panbrello_DefaultSpeedAndDepth()
        {
            var sound = Sine(A4) * Envelope;
            WithStereo().Save(Panbrello(sound)).Play();
        }

        [TestMethod] public void Panbrello_ConstSpeedAndDepth_Test() => Run(Panbrello_ConstSpeedAndDepth);
        void Panbrello_ConstSpeedAndDepth()
        {
            WithStereo().Sine(C5).Times(Envelope).Panbrello(speed: 2.0, depth: 0.75).Save().Play();
        }

        [TestMethod] public void Panbrello_DynamicSpeedAndDepth_Test() => Run(Panbrello_DynamicSpeedAndDepth);
        void Panbrello_DynamicSpeedAndDepth()
        {
            var sound = Sine(E5) * Envelope;

            var speed = Curve(
                x: (0, 3), y: (0, 8), @"
                                * *
                            *
                        *
                    *
                *                      ").SetName("Speed");

            var depth = Curve(
                x: (0, 3), y: (0, 1), @"
                *
                    *
                        *
                            *
                                * *    ").SetName("Depth");

            WithStereo().Save(Panbrello(sound, speed, depth)).Play();
        }

        // PitchPan Tests

        [TestMethod] public void PitchPan_UsingOperators_Test() => Run(PitchPan_UsingOperators);
        void PitchPan_UsingOperators()
        {
            // Arrange
            var centerFrequency    = A4;
            var referenceFrequency = A5;
            var referencePanning   = _[1];
            Console.WriteLine($"Input: {new { centerFrequency, referenceFrequency, referencePanning }}");

            var e4 = E4;
            var g4 = G4;

            // Act
            var panningOpE4 = PitchPan(e4, centerFrequency, referenceFrequency, referencePanning);
            var panningOpG4 = PitchPan(g4, centerFrequency, referenceFrequency, referencePanning);

            double panningValueE4 = panningOpE4.Calculate(time: 0);
            double panningValueG4 = panningOpG4.Calculate(time: 0);

            Console.WriteLine($"Output: {new { panningValueE4, panningValueG4 }}");

            // Assert
            Assert.IsNotNull(panningOpE4);
            Assert.IsTrue(panningValueE4 > 0.5);
            Assert.IsTrue(panningValueE4 < 1.0);

            Assert.IsNotNull(panningOpG4);
            Assert.IsTrue(panningValueG4 > 0.5);
            Assert.IsTrue(panningValueG4 < 1.0);

            Assert.IsTrue(panningValueE4 < panningValueG4);
        }

        [TestMethod] public void PitchPan_DynamicParameters_Test() => Run(PitchPan_DynamicParameters);
        void PitchPan_DynamicParameters()
        {
            // Arrange
            double centerFrequency    = A4.Value;
            double referenceFrequency = A5.Value;
            double referencePanning   = 1;
            Console.WriteLine($"Input: {new { centerFrequency, referenceFrequency, referencePanning }}");

            // Act
            double panningValueE4 = PitchPan(E4.Value, centerFrequency, referenceFrequency, referencePanning);
            double panningValueG4 = PitchPan(G4.Value, centerFrequency, referenceFrequency, referencePanning);
            Console.WriteLine($"Output: {new { panningValueE4, panningValueG4 }}");

            // Assert
            Assert.IsTrue(panningValueE4 > 0.5);
            Assert.IsTrue(panningValueE4 < 1.0);
            Assert.IsTrue(panningValueG4 > 0.5);
            Assert.IsTrue(panningValueG4 < 1.0);
            Assert.IsTrue(panningValueE4 < panningValueG4);
        }

        // Echo Tests

        [TestMethod] public void Echo_Additive_Old_Test() => Run(Echo_Additive_Old);
        void Echo_Additive_Old()
        {
            WithMono();

            var envelope     = Curve((0, 1), (0.2, 0));
            var sound        = Multiply(Sine(G4), envelope);
            var echoes       = _[EntityFactory.CreateEcho(TestHelper.CreateOperatorFactory(Context), sound, denominator: 1.5, delay: 0.25, count: 16)];
            var echoDuration = EchoDuration(count: 16, _[0.25]);

            envelope.SetName("Envelope");
            sound.SetName(MemberName() + "_Input.wav");
            echoes.SetName(MemberName() + "_Output.wav");

            WithAudioLength(0.2).Save(sound);
            WithAudioLength(0.2 + echoDuration).Save(echoes).Play();
        }

        [TestMethod] public void Echo_Additive_FixedValues_Test() => Run(Echo_Additive_FixedValues);
        void Echo_Additive_FixedValues()
        {
            var accessor = new SynthWishesAccessor(this);

            WithMono();

            var envelope     = Curve((0, 1), (0.2, 0));
            var sound        = Multiply(Sine(B4), envelope);
            var echoes       = accessor.EchoAdditive(sound, count: 16, magnitude: 0.66, delay: 0.25);
            var echoDuration = EchoDuration(count: 16, _[0.25]);

            envelope.SetName("Envelope");
            sound.SetName(MemberName() + " Input");
            echoes.SetName(MemberName() + " Output");

            WithAudioLength(0.2).Save(sound);
            WithAudioLength(0.2 + echoDuration).Save(echoes).Play();
        }

        [TestMethod] public void Echo_Additive_DynamicParameters_Test() => Run(Echo_Additive_DynamicParameters);
        void Echo_Additive_DynamicParameters()
        {
            var accessor = new SynthWishesAccessor(this);

            WithMono();

            var envelope = Curve((0, 1), (0.2, 0));
            var sound    = Multiply(Sine(D5), envelope);
            var magnitude = Curve(
                (0.0, 0.66),
                (0.5, 0.90),
                (3.0, 1.00),
                (4.0, 0.80),
                (5.0, 0.25));
            var delay = Curve((0, 0.25), (4, 0.35));
            var echoes = accessor.EchoAdditive(sound, count: 16, magnitude, delay);
            var echoDuration = EchoDuration(count: 16, delay);

            sound.SetName(MemberName() + " Input");
            envelope.SetName(MemberName() + " Volume");
            magnitude.SetName(MemberName() + " Magnitude");
            delay.SetName(MemberName() + " Delay");
            echoes.SetName(MemberName() + " Output");
            
            WithAudioLength(0.2).Save(sound);
            WithAudioLength(0.2 + echoDuration).Save(magnitude);
            WithAudioLength(0.2 + echoDuration).Save(delay);
            WithAudioLength(0.2 + echoDuration).Save(echoes).Play();
        }

        [TestMethod] public void Echo_FeedBack_FixedValues_Test() => Run(Echo_FeedBack_FixedValues);
        void Echo_FeedBack_FixedValues()
        {
            var accessor = new SynthWishesAccessor(this);
            
            WithMono();

            var envelope     = Curve((0, 1), (0.2, 0));
            var sound        = Multiply(Sine(F5), envelope);
            var echoes       = accessor.EchoFeedBack(sound, count: 16, magnitude: 0.66, delay: 0.25);
            var echoDuration = EchoDuration(count: 16, delay: _[0.25]);

            envelope.SetName(MemberName() + " Envelope");
            sound.SetName(MemberName() + " Input");
            echoes.SetName(MemberName() + " Output");
            
            WithAudioLength(0.2).Save(sound);
            WithAudioLength(echoDuration + 4.0).Save(echoes).Play();
        }

        [TestMethod] public void Echo_FeedBack_DynamicParameters_Test() => Run(Echo_FeedBack_DynamicParameters);
        void Echo_FeedBack_DynamicParameters()
        {
            var accessor = new SynthWishesAccessor(this);
            
            WithMono();

            var envelope     = Curve((0, 1), (0.2, 0)).SetName(MemberName() + " Volume");
            var sound        = Multiply(Sine(D5), envelope);
            var magnitude    = Curve((0.0, 0.66),
                                     (0.5, 0.90),
                                     (3.0, 1.00),
                                     (4.0, 0.80),
                                     (5.0, 0.25));
            var delay        = Curve((0, 0.25), (4, 0.35));
            var echoes       = accessor.EchoFeedBack(sound, count: 16, magnitude, delay);
            var echoDuration = EchoDuration(count: 16, delay);

            sound    .WithAudioLength(0.2)         .SetName(MemberName() + " Input"    ).Save();
            magnitude.WithAudioLength(4.5)         .SetName(MemberName() + " Magnitude").Save();
            delay    .WithAudioLength(4.5)         .SetName(MemberName() + " Delay"    ).Save();
            echoes   .WithAudioLength(echoDuration).SetName(MemberName() + " Output"   ).Save().Play();
        }
    }
}