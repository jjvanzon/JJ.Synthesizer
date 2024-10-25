using System;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;
using static JJ.Business.Synthesizer.Wishes.Helpers.NameHelper;
// ReSharper disable JoinDeclarationAndInitializer

namespace JJ.Business.Synthesizer.Tests.Functional
{
    [TestClass]
    [TestCategory("Functional")]
    public class OperatorWishes_FunctionalTests : SynthWishes
    {
        FluentOutlet Envelope => Curve((0, 0), (0.05, 1), (0.95, 1), (1.00, 0));

        // Vibrato/Tremolo Tests

        [TestMethod]
        /// <inheritdoc cref="docs._vibrato" />
        public void Test_Vibrato() => new OperatorWishes_FunctionalTests().Vibrato_RunTest();

        /// <inheritdoc cref="Wishes.Helpers.docs._vibrato" />
        void Vibrato_RunTest()
            => PlayMono(
                () => VibratoOverPitch(A4).Sine * Envelope.Stretch(2),
                volume: 0.9, duration: 2);

        [TestMethod]
        /// <inheritdoc cref="docs._tremolo" />
        public void Test_Tremolo() => new OperatorWishes_FunctionalTests().Tremolo_RunTest();

        /// <inheritdoc cref="Wishes.Helpers.docs._tremolo" />
        void Tremolo_RunTest()
            => PlayMono(
                () => Sine(C5).Tremolo(4, 0.5) * Envelope.Stretch(2),
                volume: 0.30, duration: 2);

        // Panning Tests

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsDouble() => new OperatorWishes_FunctionalTests().Panning_ConstSignal_ConstPanningAsDouble_RunTest();

        void Panning_ConstSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            Outlet fixedValues()
            {
                if (Channel == Left) return _[0.8];
                if (Channel == Right) return _[0.6];
                return default;
            }

            double panning = 0.5;
            Outlet panned;

            // Act
            Channel = Left;
            panned  = Panning(fixedValues(), panning);
            double outputLeftValue = panned.Calculate(time: 0);

            Channel = Right;
            panned  = Panning(fixedValues(), panning);
            double outputRightValue = panned.Calculate(time: 0);

            // Assert
            double expectedLeft  = 0.8 * (1 - panning); // 0.8 * 0.5 = 0.4
            double expectedRight = 0.6 * panning; // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeft,  () => outputLeftValue);
            AssertHelper.AreEqual(expectedRight, () => outputRightValue);

            // Diagnostics (get code coverage)
            Channel = Single;
            Assert.IsNull(fixedValues());
        }

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsOperator() => new OperatorWishes_FunctionalTests().Panning_ConstSignal_ConstPanningAsOperator_RunTest();

        void Panning_ConstSignal_ConstPanningAsOperator_RunTest()
        {
            // Arrange
            Outlet TestSignal()
            {
                switch (Channel)
                {
                    case Left:  return _[0.8];
                    case Right: return _[0.6];
                    default:    return default;
                }
            }

            double panningValue  = 0.5;

            // Act

            Outlet panned;

            Channel = Left;
            panned  = Panning(TestSignal(), panningValue);
            double leftValue = panned.Calculate(time: 0);

            Channel = Right;
            panned  = Panning(TestSignal(), panningValue);
            double rightValue = panned.Calculate(time: 0);

            // Assert
            double expectedLeftValue  = 0.8 * (1 - panningValue); // 0.8 * 0.5 = 0.4
            double expectedRightValue = 0.6 * panningValue; // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeftValue,  () => leftValue);
            AssertHelper.AreEqual(expectedRightValue, () => rightValue);

            // Diagnostics (get code coverage)
            Channel = Single;
            Assert.IsNull(TestSignal());
        }

        [TestMethod]
        public void Test_Panning_SineWaveSignal_ConstPanningAsDouble() => new OperatorWishes_FunctionalTests().Panning_SineWaveSignal_ConstPanningAsDouble_RunTest();

        void Panning_SineWaveSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            var freq    = E5;
            var sine    = Sine(freq);
            var panning = 0.25;

            // Act

            Outlet panned;

            Channel = Left;
            panned  = Panning(sine, panning);
            double maxValueLeft = panned.Calculate(time: 0.25 / (double)freq);
            double minValueLeft = panned.Calculate(time: 0.75 / (double)freq);

            Channel = Right;
            panned  = Panning(sine, panning);
            double maxValueRight = panned.Calculate(time: 0.25 / (double)freq);
            double minValueRight = panned.Calculate(time: 0.75 / (double)freq);

            Play(() => Panning(sine, panning) * Envelope);

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

            Play(() => Panning(sine, panning));
        }

        // Panbrello Tests

        [TestMethod]
        public void Test_Panbrello_DefaultSpeedAndDepth() => new OperatorWishes_FunctionalTests().Panbrello_DefaultSpeedAndDepth_RunTest();

        void Panbrello_DefaultSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4) * Envelope;
            Play(() => Panbrello(sound));
        }

        [TestMethod]
        public void Test_Panbrello_ConstSpeedAndDepth() => new OperatorWishes_FunctionalTests().Panbrello_ConstSpeedAndDepth_RunTest();

        void Panbrello_ConstSpeedAndDepth_RunTest()
        {
            var sound = Sine(C5) * Envelope;
            Play(() => Panbrello(sound, (speed: 2.0, depth: 0.75)));
        }

        [TestMethod]
        public void Test_Panbrello_DynamicSpeedAndDepth() => new OperatorWishes_FunctionalTests().Panbrello_DynamicSpeedAndDepth_RunTest();

        void Panbrello_DynamicSpeedAndDepth_RunTest()
        {
            var sound = Sine(E5) * Envelope;

            var speed = Curve(
                "Speed", x: (0, 3), y: (0, 8), @"
                                * *
                            *
                        *
                    *
                *                              ");

            var depth = Curve(
                "Depth", x: (0, 3), y: (0, 1), @"
                *
                    *
                        *
                            *
                                * *            ");

            Play(() => Panbrello(sound, (speed, depth)));
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

            Outlet e4 = E4;
            Outlet g4 = G4;

            // Act
            Outlet panningOpE4 = PitchPan(e4, centerFrequency, referenceFrequency, referencePanning);
            Outlet panningOpG4 = PitchPan(g4, centerFrequency, referenceFrequency, referencePanning);

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
            Outlet envelope = Curve("Envelope", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(G4), envelope);
            Outlet echoes   = EntityFactory.CreateEcho(TestHelper.CreateOperatorFactory(Context), sound, denominator: 1.5, delay: 0.25, count: 16);

            SaveAudioMono(() => sound,  duration: 0.2, fileName: Name() + "_Input.wav");
            PlayMono     (() => echoes, duration: 4.0, fileName: Name() + "_Output.wav");

        }

        [TestMethod]
        public void Test_Echo_Additive_FixedValues() => new OperatorWishes_FunctionalTests().Echo_Additive_FixedValues_RunTest();

        void Echo_Additive_FixedValues_RunTest()
        {
            Outlet envelope = Curve("Envelope", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(B4), envelope);
            Outlet echoes   = EchoAdditive(sound, count: 16, magnitude: 0.66, delay: 0.25);

            SaveAudioMono(() => sound,  duration: 0.2, fileName: Name() + "_Input.wav");
            PlayMono     (() => echoes, duration: 4.0, fileName: Name() + "_Output.wav");
        }

        [TestMethod]
        public void Test_Echo_Additive_DynamicParameters() => new OperatorWishes_FunctionalTests().Echo_Additive_DynamicParameters_RunTest();

        void Echo_Additive_DynamicParameters_RunTest()
        {
            Outlet envelope = Curve("Volume Curve", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(D5), envelope);

            Outlet magnitude = Curve("Magnitude Curve",
                                       (0.0, 0.66),
                                       (0.5, 0.90),
                                       (3.0, 1.00),
                                       (4.0, 0.80),
                                       (5.0, 0.25));

            Outlet delay = Curve("Delay Curve", (0, 0.25), (4, 0.35));

            Outlet echoes = EchoAdditive(sound, count: 16, magnitude, delay);

            SaveAudioMono(() => sound,     duration: 0.2, fileName: Name() + "_Input.wav");
            SaveAudioMono(() => magnitude, duration: 4,   fileName: Name() + "_Magnitude.wav");
            SaveAudioMono(() => delay,     duration: 4,   fileName: Name() + "_Delay.wav");
            PlayMono(     () => echoes,    duration: 4,   fileName: Name() + "_Output.wav");
        }

        [TestMethod]
        public void Test_Echo_FeedBack_FixedValues() => new OperatorWishes_FunctionalTests().Echo_FeedBack_FixedValues_RunTest();

        void Echo_FeedBack_FixedValues_RunTest()
        {
            Outlet envelope = Curve("Envelope", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(F5), envelope);

            Outlet echoes = EchoFeedBack(sound, count: 16, magnitude: 0.66, delay: 0.25);

            SaveAudioMono(() => sound,  duration: 0.2, fileName: Name() + "_Input.wav");
            PlayMono(() => echoes, duration: 4.0, fileName: Name() + "_Output.wav");
        }

        [TestMethod]
        public void Test_Echo_FeedBack_DynamicParameters() => new OperatorWishes_FunctionalTests().Echo_FeedBack_DynamicParameters_RunTest();

        void Echo_FeedBack_DynamicParameters_RunTest()
        {
            Outlet envelope = Curve("Volume Curve", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(D5), envelope);

            Outlet magnitude = Curve("Magnitude Curve",
                                       (0.0, 0.66),
                                       (0.5, 0.90),
                                       (3.0, 1.00),
                                       (4.0, 0.80),
                                       (5.0, 0.25));

            Outlet delay = Curve("Delay Curve", (0, 0.25), (4, 0.35));

            Outlet echoes = EchoFeedBack(sound, count: 16, magnitude, delay);

            SaveAudioMono(() => sound,     duration: 0.2, fileName: Name() + "_Input.wav"    );
            SaveAudioMono(() => magnitude, duration: 4.5, fileName: Name() + "_Magnitude.wav");
            SaveAudioMono(() => delay,     duration: 4.5, fileName: Name() + "_Delay.wav"    );
            PlayMono(     () => echoes,    duration: 4.5, fileName: Name() + "_Output.wav"   );
        }
    }
}