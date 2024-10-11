using JetBrains.Annotations;
using JJ.Business.Synthesizer.Calculation;
using JJ.Business.Synthesizer.EntityWrappers;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Tests.Wishes;
using JJ.Framework.Persistence;
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
        public void Test_Synthesizer_OperatorWishes_PitchPan()
        {
            using (IContext context = PersistenceHelper.CreateContext())
                new Synthesizer_OperatorWishesTests(context).Test_OperatorWishes_PitchPan();
        }

        void Test_OperatorWishes_PitchPan()
        {
            // Arrange
            var centerFrequency = _[A4];
            var referenceFrequency = _[A5];
            var referencePanning = _[1.0];

            // Act
            var panningOpE4 = PitchPan(_[E4], centerFrequency, referenceFrequency, referencePanning);
            var panningOpG4 = PitchPan(_[G4], centerFrequency, referenceFrequency, referencePanning);

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
    }
}