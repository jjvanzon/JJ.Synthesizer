using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.ChannelEnum;

// ReSharper disable JoinDeclarationAndInitializer

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class OperatorWishesTests : SynthesizerSugar
    {
        [UsedImplicitly]
        public OperatorWishesTests()
        { }

        OperatorWishesTests(IContext context)
            : base(context)
        { }

        // Vibrato/Tremolo Tests

        [TestMethod]
        /// <inheritdoc cref="docs._vibrato" />
        public void Test_Vibrato()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Vibrato_RunTest();
        }

        /// <inheritdoc cref="docs._vibrato" />
        void Vibrato_RunTest()
            => SaveWavMono(
                () => Sine(VibratoOverPitch(A4)),
                volume: 0.9, duration: 3);

        [TestMethod]
        /// <inheritdoc cref="docs._tremolo" />
        public void Test_Tremolo()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Tremolo_RunTest();
        }

        /// <inheritdoc cref="docs._tremolo" />
        void Tremolo_RunTest()
            => SaveWavMono(
                () => Tremolo(Sine(A4), (_[4], _[0.5])),
                volume: 0.30, duration: 3);

        // Panning Tests

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_ConstSignal_ConstPanningAsDouble_RunTest();
        }

        void Panning_ConstSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            Outlet signal()
            {
                if (Channel == Left) return _[0.8];
                if (Channel == Right) return _[0.6];
                return default;
            }

            double panning = 0.5;
            Outlet panned;

            // Act
            var calculator = new OperatorCalculator(default);

            Channel = Left;
            panned  = Panning(signal(), panning);
            double outputLeftValue = calculator.CalculateValue(panned, time: 0);

            Channel = Right;
            panned  = Panning(signal(), panning);
            double outputRightValue = calculator.CalculateValue(panned, time: 0);

            // Assert
            double expectedLeft  = 0.8 * (1 - panning); // 0.8 * 0.5 = 0.4
            double expectedRight = 0.6 * panning; // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeft,  () => outputLeftValue);
            AssertHelper.AreEqual(expectedRight, () => outputRightValue);
        }

        [TestMethod]
        public void Test_Panning_ConstSignal_ConstPanningAsOperator()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_ConstSignal_ConstPanningAsOperator_RunTest();
        }

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

            var calculator = new OperatorCalculator(default);

            // Act

            Outlet panned;

            Channel = Left;
            panned  = Panning(TestSignal(), panningOutlet);
            double leftValue = calculator.CalculateValue(panned, time: 0);

            Channel = Right;
            panned  = Panning(TestSignal(), panningOutlet);
            double rightValue = calculator.CalculateValue(panned, time: 0);

            // Assert
            double expectedLeftValue  = 0.8 * (1 - panningValue); // 0.8 * 0.5 = 0.4
            double expectedRightValue = 0.6 * panningValue; // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeftValue,  () => leftValue);
            AssertHelper.AreEqual(expectedRightValue, () => rightValue);
        }

        [TestMethod]
        public void Test_Panning_SineWaveSignal_ConstPanningAsDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_SineWaveSignal_ConstPanningAsDouble_RunTest();
        }

        void Panning_SineWaveSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            var    freq       = A4;
            var    sine       = Sine(_[freq]);
            double panning    = 0.25;
            var    calculator = new OperatorCalculator(default);

            // Act

            Outlet panned;

            Channel    = Left;
            panned = Panning(sine, panning);
            double maxValueLeft = calculator.CalculateValue(panned, time: 0.25 / freq);
            double minValueLeft = calculator.CalculateValue(panned, time: 0.75 / freq);

            Channel    = Right;
            panned = Panning(sine, panning);
            double maxValueRight = calculator.CalculateValue(panned, time: 0.25 / freq);
            double minValueRight = calculator.CalculateValue(panned, time: 0.75 / freq);

            SaveWav(() => Panning(sine, panning), duration: 1, volume: 1);

            // Assert
            AssertHelper.AreEqual(0.75,  () => maxValueLeft);
            AssertHelper.AreEqual(-0.75, () => minValueLeft);
            AssertHelper.AreEqual(0.25,  () => maxValueRight);
            AssertHelper.AreEqual(-0.25, () => minValueRight);
        }

        [TestMethod]
        public void Test_Panning_SineWaveSignal_DynamicPanning()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_SineWaveSignal_DynamicPanning_RunTest();
        }

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

            SaveWav(() => Panning(sine, panning), duration: 1, volume: 1);
        }

        // Panbrello Tests

        [TestMethod]
        public void Test_Panbrello_DefaultSpeedAndDepth()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panbrello_DefaultSpeedAndDepth_RunTest();
        }

        void Panbrello_DefaultSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4);
            SaveWav(() => Panbrello(sound), volume: 1);
        }

        [TestMethod]
        public void Test_Panbrello_ConstSpeedAndDepth()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panbrello_ConstSpeedAndDepth_RunTest();
        }

        void Panbrello_ConstSpeedAndDepth_RunTest()
        {
            var sound = Sine(A4);
            SaveWav(() => Panbrello(sound, (speed: _[2.0], depth: _[0.75])), volume: 1);
        }

        [TestMethod]
        public void Test_Panbrello_DynamicSpeedAndDepth()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panbrello_DynamicSpeedAndDepth_RunTest();
        }

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

            SaveWav(() => Panbrello(sound, (speed, depth)), volume: 1);
        }

        // PitchPan Tests

        [TestMethod]
        public void Test_PitchPan_UsingOperators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).PitchPan_UsingOperators_RunTest();
        }

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

            var calculator = new OperatorCalculator(default);

            double panningValueE4 = calculator.CalculateValue(panningOpE4, time: 0);
            double panningValueG4 = calculator.CalculateValue(panningOpG4, time: 0);

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
        public void Test_PitchPan_UsingDoubles()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).PitchPan_UsingDoubles_RunTest();
        }

        void PitchPan_UsingDoubles_RunTest()
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
    }
}