using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
using JJ.Framework.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Wishes.Notes;

namespace JJ.Business.Synthesizer.Tests
{
    [TestClass]
    public class Synthesizer_OperatorWishesTests : SynthSugarBase
    {
        [UsedImplicitly]
        public Synthesizer_OperatorWishesTests()
        { }

        Synthesizer_OperatorWishesTests(IContext context)
            : base(context)
        { }


        [TestMethod]
        public void Test_Synthesizer_OperatorWishes_Panning_WithDoubles_AssertSimpleValues()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_OperatorWishesTests(context).Test_OperatorWishes_Panning_WithDoubles_AssertSimpleValues();
        }

        [TestMethod]
        public void Test_OperatorWishes_Panning_WithDoubles_AssertSimpleValues()
        {
            // Arrange
            var input = (left: _[0.8], right: _[0.6]);
            double panning = 0.5;

            // Act
            var output = Panning(input, panning);
            var calculator = new OperatorCalculator(default);
            double outputLeftValue = calculator.CalculateValue(output.left, time: 0);
            double outputRightValue = calculator.CalculateValue(output.right, time: 0);

            // Assert
            double expectedLeft = 0.8 * (1 - panning); // 0.8 * 0.5 = 0.4
            double expectedRight = 0.6 * panning;      // 0.6 * 0.5 = 0.3
            AssertHelper.AreEqual(expectedLeft, () => outputLeftValue);
            AssertHelper.AreEqual(expectedRight, () => outputRightValue);
        }

        [TestMethod]
        public void Test_Synthesizer_OperatorWishes_PitchPan_UsingOperators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_OperatorWishesTests(context).Test_OperatorWishes_PitchPan_UsingOperators();
        }

        void Test_OperatorWishes_PitchPan_UsingOperators()
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
        public void Test_Synthesizer_OperatorWishes_PitchPan_UsingDoubles()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_OperatorWishesTests(context).Test_OperatorWishes_PitchPan_UsingDoubles();
        }

        void Test_OperatorWishes_PitchPan_UsingDoubles()
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