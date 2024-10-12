using System;
using System.Reflection;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Reflection.MethodBase;
using static JJ.Business.Synthesizer.Tests.Wishes.Notes;
// ReSharper disable PossibleNullReferenceException

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class OperatorWishesTests : SynthSugarBase
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
            => SaveWav(
                Sine(VibratoOverPitch(_[A4])),
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
            => SaveWav(
                Tremolo(Sine(_[A4]), tremolo: (_[4], _[0.5])),
                volume: 0.30, duration: 3);

        // Panning Tests

        [TestMethod]
        public void Test_Panning_ConstantSignal_ConstantPanningAsDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_ConstantSignal_ConstPanningAsDouble_RunTest();
        }

        void Panning_ConstantSignal_ConstPanningAsDouble_RunTest()
        {
            // Arrange
            var input = (left: _[0.8], right: _[0.6]);
            double panning = 0.5;

            // Act
            var output = Panning(input, panning);
            var calculator = new OperatorCalculator(default);
            double outputLeftValue = calculator.CalculateValue(output.Left, time: 0);
            double outputRightValue = calculator.CalculateValue(output.Right, time: 0);

            // Assert
            double expectedLeft = 0.8 * (1 - panning); // 0.8 * 0.5 = 0.4
            double expectedRight = 0.6 * panning;      // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeft, () => outputLeftValue);
            AssertHelper.AreEqual(expectedRight, () => outputRightValue);
        }

        [TestMethod]
        public void Test_Panning_ConstantSignal_ConstantPanningAsOperator()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_ConstantSignal_ConstantPanningAsOperator_RunTest();
        }

        void Panning_ConstantSignal_ConstantPanningAsOperator_RunTest()
        {
            // Arrange
            var input = (left: _[0.8], right: _[0.6]);
            double panning = 0.5;
            Outlet panningOutlet = _[panning];

            // Act
            var output = Panning(input, panningOutlet);
            var calculator = new OperatorCalculator(default);
            double outputLeftValue = calculator.CalculateValue(output.Left, time: 0);
            double outputRightValue = calculator.CalculateValue(output.Right, time: 0);

            // Assert
            double expectedLeft = 0.8 * (1 - panning); // 0.8 * 0.5 = 0.4
            double expectedRight = 0.6 * panning;      // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeft, () => outputLeftValue);
            AssertHelper.AreEqual(expectedRight, () => outputRightValue);
        }

        [TestMethod]
        public void Test_Panning_SineWaveSignal_ConstantPanningAsDouble()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panning_SineWaveSignal_ConstantPanningAsDouble_RunTest();
        }

        void Panning_SineWaveSignal_ConstantPanningAsDouble_RunTest()
        {
            // Arrange
            var sine = Sine(pitch: _[1]);
            var stereoInput = (sine, sine);
            double panning = 0.25;

            // Act
            var pannedSine = Panning(stereoInput, panning);
            var calculator = new OperatorCalculator(default);
            double maxValueLeft = calculator.CalculateValue(pannedSine.Left, time: 0.25);
            double minValueLeft = calculator.CalculateValue(pannedSine.Left, time: 0.75);
            double maxValueRight = calculator.CalculateValue(pannedSine.Right, time: 0.25);
            double minValueRight = calculator.CalculateValue(pannedSine.Right, time: 0.75);

            SaveWav(pannedSine.Left, duration:1, volume:1, fileName: $"{GetCurrentMethod().Name}.left.wav");
            SaveWav(pannedSine.Right, duration:1, volume:1, fileName: $"{GetCurrentMethod().Name}.right.wav");

            // Assert
            AssertHelper.AreEqual(0.75, () => maxValueLeft);
            AssertHelper.AreEqual(-0.75, () => minValueLeft);
            AssertHelper.AreEqual(0.25, () => maxValueRight);
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
            // Arrange
            var sine = Sine(_[A4]);
            var stereoInput = (sine, sine);
            Outlet panningOutlet = CurveIn(@"
                                            *
                                        *
                                    *
                                *
                            *
                        *
                    *
                *
            *");

            // Act
            var pannedSine = Panning(stereoInput, panningOutlet);

            SaveWav(pannedSine.Left, duration: 1, volume: 1, fileName: $"{GetCurrentMethod().Name}.Left.wav");
            SaveWav(pannedSine.Right, duration: 1, volume: 1, fileName: $"{GetCurrentMethod().Name}.Right.wav");
        }

        // Panbrello Tests

        [TestMethod]
        public void Test_Panbrello_DefaultSpeedAndDepth()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panbrello_RunTest_DefaultSpeedAndDepth();
        }

        void Panbrello_RunTest_DefaultSpeedAndDepth()
        {
            var sound = Sine(_[A4]);
            var signal = (sound, sound);
            var panbrello = Panbrello(signal);

            SaveWav(panbrello.Left, volume: 1, fileName: $"{GetCurrentMethod().Name}.Left.wav");
            SaveWav(panbrello.Right, volume: 1, fileName: $"{GetCurrentMethod().Name}.Right.wav");
        }

        [TestMethod]
        public void Test_Panbrello_ConstSpeedAndDepth()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panbrello_RunTest_ConstSpeedAndDepth();
        }

        void Panbrello_RunTest_ConstSpeedAndDepth()
        {
            var sound = Sine(_[A4]);
            var signal = (sound, sound);
            var panbrello = Panbrello(signal, (speed: 2.0, depth: 0.75));

            SaveWav(panbrello.Left, volume: 1, fileName: $"{GetCurrentMethod().Name}.Left.wav");
            SaveWav(panbrello.Right, volume: 1, fileName: $"{GetCurrentMethod().Name}.Right.wav");
        }

        [TestMethod]
        public void Test_Panbrello_DynamicSpeedAndDepth()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new OperatorWishesTests(context).Panbrello_RunTest_DynamicSpeedAndDepth();
        }

        void Panbrello_RunTest_DynamicSpeedAndDepth()
        {
            var sound = Sine(_[A4]);
            var signal = (sound, sound);

            var speed = CurveIn(
            "Speed", x:(0, 3), y:(0, 8), @"
                            * *
                        *
                    *
                *
            *                            ");

            var depth = CurveIn(
            "Depth", x:(0, 3), y:(0, 1), @"
            *
                *
                    *
                        *
                            * *          ");

            var panbrello = Panbrello(signal, (speed, depth));

            SaveWav(panbrello.Left, volume: 1, fileName: $"{GetCurrentMethod().Name}.Left.wav");
            SaveWav(panbrello.Right, volume: 1, fileName: $"{GetCurrentMethod().Name}.Right.wav");
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
            double centerFrequency = A4;
            double referenceFrequency = A5;
            double referencePanning = 1;
            Console.WriteLine($"Input: {new { centerFrequency, referenceFrequency, referencePanning }}");
            
            Outlet e4 = _[E4];
            Outlet g4 = _[G4];

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
            double centerFrequency = A4;
            double referenceFrequency = A5;
            double referencePanning = 1;
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