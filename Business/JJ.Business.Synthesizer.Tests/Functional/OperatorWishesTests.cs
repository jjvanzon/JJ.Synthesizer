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
    public class OperatorWishesTests : SynthWishes
    {
        // Vibrato/Tremolo Tests

        [TestMethod]
        /// <inheritdoc cref="docs._vibrato" />
        public void Test_Vibrato() => new OperatorWishesTests().Vibrato_RunTest();

        /// <inheritdoc cref="Wishes.Helpers.docs._vibrato" />
        void Vibrato_RunTest()
            => PlayMono(
                () => Sine(VibratoOverPitch(A4)),
                volume: 0.9, duration: 3);

        [TestMethod]
        /// <inheritdoc cref="docs._tremolo" />
        public void Test_Tremolo() => new OperatorWishesTests().Tremolo_RunTest();

        /// <inheritdoc cref="Wishes.Helpers.docs._tremolo" />
        void Tremolo_RunTest()
            => PlayMono(
                () => Tremolo(Sine(A4), (_[4], _[0.5])),
                volume: 0.30, duration: 3);

        // Panning Tests

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsDouble() => new OperatorWishesTests().Panning_ConstSignal_ConstPanningAsDouble_RunTest();

        void Panning_ConstSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            Outlet fixedValues()
            {
                if (Channel == Left) return _[0.8];
                if (Channel == Right) return _[0.6];
                return default;
            }

            var    panning = _[0.5];
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
        public void Test_Panning_ConstSignal_ConstPanningAsOperator() => new OperatorWishesTests().Panning_ConstSignal_ConstPanningAsOperator_RunTest();

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
            Outlet panningOutlet = _[panningValue];

            // Act

            Outlet panned;

            Channel = Left;
            panned  = Panning(TestSignal(), panningOutlet);
            double leftValue = panned.Calculate(time: 0);

            Channel = Right;
            panned  = Panning(TestSignal(), panningOutlet);
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
        public void Test_Panning_SineWaveSignal_ConstPanningAsDouble() => new OperatorWishesTests().Panning_SineWaveSignal_ConstPanningAsDouble_RunTest();

        void Panning_SineWaveSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            var freq    = A4;
            var sine    = Sine(_[freq]);
            var panning = _[0.25];

            // Act

            Outlet panned;

            Channel = Left;
            panned  = Panning(sine, panning);
            double maxValueLeft = panned.Calculate(time: 0.25 / freq);
            double minValueLeft = panned.Calculate(time: 0.75 / freq);

            Channel = Right;
            panned  = Panning(sine, panning);
            double maxValueRight = panned.Calculate(time: 0.25 / freq);
            double minValueRight = panned.Calculate(time: 0.75 / freq);

            Play(() => Panning(sine, panning), duration: 1);

            // Assert
            AssertHelper.AreEqual(0.75,  () => maxValueLeft);
            AssertHelper.AreEqual(-0.75, () => minValueLeft);
            AssertHelper.AreEqual(0.25,  () => maxValueRight);
            AssertHelper.AreEqual(-0.25, () => minValueRight);
        }

        [TestMethod]
        public void Test_Panning_SineWaveSignal_DynamicPanning() => new OperatorWishesTests().Panning_SineWaveSignal_DynamicPanning_RunTest();

        void Panning_SineWaveSignal_DynamicPanning_RunTest()
        {
            var sine = Sine(A4);
            var panning = CurveIn(@"
                                    *
                                *
                            *
                        *
                    *
                *
            *");

            Play(() => Panning(sine, panning), duration: 1);
        }

        // Panbrello Tests

        [TestMethod]
        public void Test_Panbrello_DefaultSpeedAndDepth() => new OperatorWishesTests().Panbrello_DefaultSpeedAndDepth_RunTest();

        void Panbrello_DefaultSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4);
            Play(() => Panbrello(sound));
        }

        [TestMethod]
        public void Test_Panbrello_ConstSpeedAndDepth() => new OperatorWishesTests().Panbrello_ConstSpeedAndDepth_RunTest();

        void Panbrello_ConstSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4);
            Play(() => Panbrello(sound, (speed: _[2.0], depth: _[0.75])));
        }

        [TestMethod]
        public void Test_Panbrello_DynamicSpeedAndDepth() => new OperatorWishesTests().Panbrello_DynamicSpeedAndDepth_RunTest();

        void Panbrello_DynamicSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4);

            var speed = CurveIn(
                "Speed", x: (0, 3), y: (0, 8), @"
                                * *
                            *
                        *
                    *
                *                              ");

            var depth = CurveIn(
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
        public void Test_PitchPan_UsingOperators() => new OperatorWishesTests().PitchPan_UsingOperators_RunTest();

        void PitchPan_UsingOperators_RunTest()
        {
            // Arrange
            double centerFrequency    = A4;
            double referenceFrequency = A5;
            double referencePanning   = 1;
            Console.WriteLine($"Input: {new { centerFrequency, referenceFrequency, referencePanning }}");

            Outlet e4 = E4;
            Outlet g4 = G4;

            // Act
            Outlet panningOpE4 = PitchPan(e4, _[centerFrequency], _[referenceFrequency], _[referencePanning]);
            Outlet panningOpG4 = PitchPan(g4, _[centerFrequency], _[referenceFrequency], _[referencePanning]);

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
        public void Test_PitchPan_DynamicParameters() => new OperatorWishesTests().PitchPan_DynamicParameters_RunTest();

        void PitchPan_DynamicParameters_RunTest()
        {
            // Arrange
            double centerFrequency    = A4;
            double referenceFrequency = A5;
            double referencePanning   = 1;
            Console.WriteLine($"Input: {new { centerFrequency, referenceFrequency, referencePanning }}");

            // Act
            double panningValueE4 = PitchPan(E4, centerFrequency, referenceFrequency, referencePanning);
            double panningValueG4 = PitchPan(G4, centerFrequency, referenceFrequency, referencePanning);
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
        public void Test_Echo_Additive_Old() => new OperatorWishesTests().Echo_Additive_Old_RunTest();

        void Echo_Additive_Old_RunTest()
        {
            Outlet envelope = CurveIn("Envelope", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(G4), envelope);
            Outlet echoes   = EntityFactory.CreateEcho(this, sound, denominator: 1.5, delay: 0.25, count: 16);

            PlayMono(() => sound,  duration: 0.2, fileName: Name() + "_Input.wav");
            PlayMono(() => echoes, duration: 4.0, fileName: Name() + "_Output.wav");

            Console.WriteLine();
            Console.WriteLine(echoes.Stringify());
        }

        [TestMethod]
        public void Test_Echo_Additive_FixedValues() => new OperatorWishesTests().Echo_Additive_FixedValues_RunTest();

        void Echo_Additive_FixedValues_RunTest()
        {
            Outlet envelope = CurveIn("Envelope", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(B4), envelope);
            Outlet echoes   = EchoAdditive(sound, magnitude: _[0.66], delay: _[0.25], count: 16);

            PlayMono(() => sound,  duration: 0.2, fileName: Name() + "_Input.wav");
            PlayMono(() => echoes, duration: 4.0, fileName: Name() + "_Output.wav");

            Console.WriteLine();
            Console.WriteLine(echoes.Stringify());
        }

        [TestMethod]
        public void Test_Echo_Additive_DynamicParameters() => new OperatorWishesTests().Echo_Additive_DynamicParameters_RunTest();

        void Echo_Additive_DynamicParameters_RunTest()
        {
            Outlet envelope = CurveIn("Volume Curve", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(D5), envelope);

            Outlet magnitude = CurveIn("Magnitude Curve",
                                       (0.0, 0.66),
                                       (0.5, 0.90),
                                       (3.0, 1.00),
                                       (4.0, 0.80),
                                       (5.0, 0.25));

            Outlet delay = CurveIn("Delay Curve", (0, 0.25), (4, 0.35));

            Outlet echoes = EchoAdditive(sound, magnitude, delay, count: 16);

            PlayMono(() => sound, duration: 0.2, fileName: Name() + "_Input.wav");
            SaveAudioMono(() => magnitude, duration: 5, fileName: Name() + "_Magnitude.wav");
            SaveAudioMono(() => delay,     duration: 5, fileName: Name() + "_Delay.wav");
            PlayMono(() => echoes, duration: 5, fileName: Name() + "_Output.wav");

            Console.WriteLine();
            Console.WriteLine(echoes.Stringify());
        }

        [TestMethod]
        public void Test_Echo_FeedBack_FixedValues() => new OperatorWishesTests().Echo_FeedBack_FixedValues_RunTest();

        void Echo_FeedBack_FixedValues_RunTest()
        {
            Outlet envelope = CurveIn("Envelope", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(F5), envelope);

            Outlet echoes = EchoFeedBack(sound, magnitude: _[0.66], delay: _[0.25], count: 16);

            PlayMono(() => sound,  duration: 0.2, fileName: Name() + "_Input.wav");
            PlayMono(() => echoes, duration: 4.0, fileName: Name() + "_Output.wav");

            Console.WriteLine();
            Console.WriteLine(echoes.Stringify());
        }

        [TestMethod]
        public void Test_Echo_FeedBack_DynamicParameters() => new OperatorWishesTests().Echo_FeedBack_DynamicParameters_RunTest();

        void Echo_FeedBack_DynamicParameters_RunTest()
        {
            Outlet envelope = CurveIn("Volume Curve", (0, 1), (0.2, 0));
            Outlet sound    = Multiply(Sine(D5), envelope);

            Outlet magnitude = CurveIn("Magnitude Curve",
                                       (0.0, 0.66),
                                       (0.5, 0.90),
                                       (3.0, 1.00),
                                       (4.0, 0.80),
                                       (5.0, 0.25));

            Outlet delay = CurveIn("Delay Curve", (0, 0.25), (4, 0.35));

            Outlet echoes = EchoFeedBack(sound, magnitude, delay, count: 16);

            PlayMono(() => sound, duration: 0.2, fileName: Name() + "_Input.wav");
            SaveAudioMono(() => magnitude, duration: 5, fileName: Name() + "_Magnitude.wav");
            SaveAudioMono(() => delay,     duration: 5, fileName: Name() + "_Delay.wav");
            PlayMono(() => echoes, duration: 5, fileName: Name() + "_Output.wav");

            Console.WriteLine();
            Console.WriteLine(echoes.Stringify());
        }
    }
}