using System;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable ExplicitCallerInfoArgument

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Functional")]
    public class OperatorWishes_FunctionalTests : SynthWishes
    {
        FluentOutlet Envelope => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));

        public OperatorWishes_FunctionalTests()
        {
            WithMono();
        }
        
        // Vibrato/Tremolo Tests

        /// <inheritdoc cref="docs._vibrato" />
        [TestMethod]
        public void Test_Vibrato() => new OperatorWishes_FunctionalTests().Vibrato_RunTest();

        /// <inheritdoc cref="docs._vibrato" />
        void Vibrato_RunTest()
            => WithAudioLength(2).WithMono().Save(() => VibratoOverPitch(A4).Sine * Envelope.Stretch(2) * 0.9).Play();

        /// <inheritdoc cref="docs._tremolo" />
        [TestMethod]
        public void Test_Tremolo() => new OperatorWishes_FunctionalTests().Tremolo_RunTest();

        /// <inheritdoc cref="docs._tremolo" />
        void Tremolo_RunTest()
            => WithAudioLength(2).WithMono().Save(() => Sine(C5).Tremolo(4, 0.5) * Envelope.Stretch(2) * 0.3).Play();

        // Panning Tests

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsDouble() => new OperatorWishes_FunctionalTests().Panning_ConstSignal_ConstPanningAsDouble_RunTest();

        void Panning_ConstSignal_ConstPanningAsDouble_RunTest()
        {
            WithStereo();
            
            // Arrange
            FluentOutlet fixedValues()
            {
                if (Channel == ChannelEnum.Left) return _[0.8];
                if (Channel == ChannelEnum.Right) return _[0.6];
                return default;
            }

            double panning = 0.5;
            Outlet panned;

            // Act
            WithLeft();
            panned  = Panning(fixedValues(), panning);
            double outputLeftValue = panned.Calculate(time: 0);

            WithRight();
            panned  = Panning(fixedValues(), panning);
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

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsOperator() => new OperatorWishes_FunctionalTests().Panning_ConstSignal_ConstPanningAsOperator_RunTest();

        void Panning_ConstSignal_ConstPanningAsOperator_RunTest()
        {
            WithStereo();
            
            // Arrange
            FluentOutlet TestSignal()
            {
                switch (Channel)
                {
                    case ChannelEnum.Left:  return _[0.8];
                    case ChannelEnum.Right: return _[0.6];
                    default:                return default;
                }
            }

            double panningValue  = 0.5;

            // Act

            FluentOutlet panned;

            WithLeft();
            panned  = Panning(TestSignal(), panningValue);
            double leftValue = panned.Calculate(time: 0);

            WithRight();
            panned  = Panning(TestSignal(), panningValue);
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

        [TestMethod]
        public void Test_Panning_SineWaveSignal_ConstPanningAsDouble() => new OperatorWishes_FunctionalTests().Panning_SineWaveSignal_ConstPanningAsDouble_RunTest();

        void Panning_SineWaveSignal_ConstPanningAsDouble_RunTest()
        {
            WithStereo();
            
            // Arrange
            var freq    = E5;
            var sine    = Sine(freq);
            var panning = 0.25;

            // Act

            Outlet panned;

            WithLeft();
            panned  = Panning(sine, panning);
            double maxValueLeft = panned.Calculate(time: 0.25 / (double)freq);
            double minValueLeft = panned.Calculate(time: 0.75 / (double)freq);

            WithRight();
            panned  = Panning(sine, panning);
            double maxValueRight = panned.Calculate(time: 0.25 / (double)freq);
            double minValueRight = panned.Calculate(time: 0.75 / (double)freq);

            Save(() => Panning(sine, panning) * Envelope).Play();

            // Assert
            AssertHelper.AreEqual(0.75,  () => maxValueLeft);
            AssertHelper.AreEqual(-0.75, () => minValueLeft);
            AssertHelper.AreEqual(0.25,  () => maxValueRight);
            AssertHelper.AreEqual(-0.25, () => minValueRight);
        }

        [TestMethod]
        public void Test_Panning_SineWaveSignal_DynamicPanning() => new OperatorWishes_FunctionalTests().Panning_SineWaveSignal_DynamicPanning_RunTest();

        void Panning_SineWaveSignal_DynamicPanning_RunTest()
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

            WithStereo().Save(() => Panning(sine, panning)).Play();
        }

        // Panbrello Tests

        [TestMethod]
        public void Test_Panbrello_DefaultSpeedAndDepth() => new OperatorWishes_FunctionalTests().Panbrello_DefaultSpeedAndDepth_RunTest();

        void Panbrello_DefaultSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4) * Envelope;
            WithStereo().Save(() => Panbrello(sound)).Play();
        }

        [TestMethod]
        public void Test_Panbrello_ConstSpeedAndDepth() => new OperatorWishes_FunctionalTests().Panbrello_ConstSpeedAndDepth_RunTest();

        void Panbrello_ConstSpeedAndDepth_RunTest()
        {
            var sound = Sine(C5) * Envelope;
            WithStereo().Save(() => Panbrello(sound, (speed: 2.0, depth: 0.75))).Play();
        }

        [TestMethod]
        public void Test_Panbrello_DynamicSpeedAndDepth() => new OperatorWishes_FunctionalTests().Panbrello_DynamicSpeedAndDepth_RunTest();

        void Panbrello_DynamicSpeedAndDepth_RunTest()
        {
            var sound = Sine(E5) * Envelope;

            var speed = WithName("Speed").Curve(
                x: (0, 3), y: (0, 8), @"
                                * *
                            *
                        *
                    *
                *                              ");

            var depth = WithName("Depth").Curve(
                x: (0, 3), y: (0, 1), @"
                *
                    *
                        *
                            *
                                * *            ");

            WithStereo().Save(() => Panbrello(sound, (speed, depth))).Play();
        }

        // PitchPan Tests

        [TestMethod]
        public void Test_PitchPan_UsingOperators() => new OperatorWishes_FunctionalTests().PitchPan_UsingOperators_RunTest();

        void PitchPan_UsingOperators_RunTest()
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

        [TestMethod]
        public void Test_PitchPan_DynamicParameters() => new OperatorWishes_FunctionalTests().PitchPan_DynamicParameters_RunTest();

        void PitchPan_DynamicParameters_RunTest()
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

        [TestMethod]
        public void Test_Echo_Additive_Old() => new OperatorWishes_FunctionalTests().Echo_Additive_Old_RunTest();

        void Echo_Additive_Old_RunTest()
        {
            WithMono();
            
            var envelope = WithName("Envelope").Curve((0, 1), (0.2, 0));
            var sound    = Multiply(Sine(G4), envelope);
            var echoes = EntityFactory.CreateEcho(TestHelper.CreateOperatorFactory(Context), sound, denominator: 1.5, delay: 0.25, count: 16);

            WithAudioLength(0.2).WithName(MemberName() + "_Input.wav").Save(() => sound);
            WithAudioLength(4.0).WithName(MemberName() + "_Output.wav").Save(() => _[echoes]).Play();
        }

        [TestMethod]
        public void Test_Echo_Additive_FixedValues() => new OperatorWishes_FunctionalTests().Echo_Additive_FixedValues_RunTest();

        void Echo_Additive_FixedValues_RunTest()
        {
            WithMono();
        
            var envelope = WithName("Envelope").Curve((0, 1), (0.2, 0));
            var sound    = Multiply(Sine(B4), envelope);
            var echoes = EchoAdditive(sound, count: 16, magnitude: 0.66, delay: 0.25);

            WithAudioLength(0.2).WithName(MemberName() + "_Input.wav").Save(() => sound);
            WithAudioLength(4.0).WithName(MemberName() + "_Output.wav").Save(() => echoes).Play();
        }

        [TestMethod]
        public void Test_Echo_Additive_DynamicParameters() => new OperatorWishes_FunctionalTests().Echo_Additive_DynamicParameters_RunTest();

        void Echo_Additive_DynamicParameters_RunTest()
        {
            WithMono();
            
            var envelope = WithName("Volume Curve").Curve((0, 1), (0.2, 0));
            var sound    = Multiply(Sine(D5), envelope);

            var magnitude = WithName("Magnitude Curve").Curve(
                (0.0, 0.66),
                (0.5, 0.90),
                (3.0, 1.00),
                (4.0, 0.80),
                (5.0, 0.25));

            var delay = WithName("Delay Curve").Curve((0, 0.25), (4, 0.35));

            var echoes = EchoAdditive(sound, count: 16, magnitude, delay);

            WithAudioLength(0.2).WithName(MemberName() + "_Input.wav").Save(() => sound);
            WithAudioLength(4.0).WithName(MemberName() + "_Magnitude.wav").Save(() => magnitude);
            WithAudioLength(4.0).WithName(MemberName() + "_Delay.wav").Save(() => delay);
            WithAudioLength(4.0).WithName(MemberName() + "_Output.wav").Save(() => echoes).Play();
        }

        [TestMethod]
        public void Test_Echo_FeedBack_FixedValues() => new OperatorWishes_FunctionalTests().Echo_FeedBack_FixedValues_RunTest();

        void Echo_FeedBack_FixedValues_RunTest()
        {
            WithMono();

            var envelope = WithName("Envelope").Curve((0, 1), (0.2, 0));
            var sound    = Multiply(Sine(F5), envelope);
            var echoes = EchoFeedBack(sound, count: 16, magnitude: 0.66, delay: 0.25);

            WithAudioLength(0.2).WithName(MemberName() + "_Input.wav").Save(() => sound);
            WithAudioLength(4.0).WithName(MemberName() + "_Output.wav").Save(() => echoes).Play();
        }

        [TestMethod]
        public void Test_Echo_FeedBack_DynamicParameters() => new OperatorWishes_FunctionalTests().Echo_FeedBack_DynamicParameters_RunTest();

        void Echo_FeedBack_DynamicParameters_RunTest()
        {
            WithMono();

            var envelope = WithName("Volume Curve").Curve((0, 1), (0.2, 0));
            var sound    = Multiply(Sine(D5), envelope);

            var magnitude = WithName("Magnitude Curve").Curve(
                (0.0, 0.66),
                (0.5, 0.90),
                (3.0, 1.00),
                (4.0, 0.80),
                (5.0, 0.25));

            var delay = WithName("Delay Curve").Curve((0, 0.25), (4, 0.35));

            var echoes = EchoFeedBack(sound, count: 16, magnitude, delay);

            WithAudioLength(0.2).WithName(MemberName() + "_Input.wav").Save(() => sound);
            WithAudioLength(4.5).WithName(MemberName() + "_Magnitude.wav").Save(() => magnitude);
            WithAudioLength(4.5).WithName(MemberName() + "_Delay.wav").Save(() => delay);
            WithAudioLength(4.5).WithName(MemberName() + "_Output.wav").Save(() => echoes).Play();
        }
    }
}