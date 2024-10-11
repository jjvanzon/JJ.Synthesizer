using System;
using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
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
        public void Test_Synthesizer_OperatorWishes_PitchPan_UsingOperators()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_OperatorWishesTests(context).Test_OperatorWishes_PitchPan_UsingOperators();
        }

        void Test_OperatorWishes_PitchPan_UsingOperators()
        {
            // Arrange
            Outlet centerFrequency = _[A4];
            Outlet referenceFrequency = _[A5];
            Outlet referencePanning = _[1];

            // Act
            Outlet panningOpE4 = PitchPan(_[E4], centerFrequency, referenceFrequency, referencePanning);
            Outlet panningOpG4 = PitchPan(_[G4], centerFrequency, referenceFrequency, referencePanning);

            var calculator = new OperatorCalculator(default);
            
            double panningValueE4 = calculator.CalculateValue(panningOpE4, default);
            double panningValueG4 = calculator.CalculateValue(panningOpG4, default);

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