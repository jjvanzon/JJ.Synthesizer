using JJ.Business.Synthesizer.Tests.Functional;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]      
    [TestCategory("Technical")]
    public class FMTests_Technical
    {
        private readonly FMTests _x = new FMTests();

        [TestMethod]
        public void FM_Trombone_Melody2_Notation_MildEcho_AddsTheDuration() => new FMTests_Technical().FM_Trombone_Melody2_Notation_MildEcho_AddsTheDuration_RunTest();

        void FM_Trombone_Melody2_Notation_MildEcho_AddsTheDuration_RunTest()
        {
            _x.Play(() => _x.MildEcho(_x.TromboneMelody2, volume: 0.75));
        }

        [TestMethod]
        public void FM_Trombone_Melody2_Notation_AddDuration_MildEchoTime()
            => new FMTests_Technical().FM_Trombone_Melody2_Notation_AddDuration_MildEchoTime_RunTest();

        void FM_Trombone_Melody2_Notation_AddDuration_MildEchoTime_RunTest()
        {
            _x.Play(() => _x.MildEcho(_x.TromboneMelody2.AddDuration(_x.MildEchoTime), volume: 0.75));
        }

        [TestMethod]
        public void FM_Trombone_Melody2_Notation_WithDuration_DurationPlusMildEchoTime() 
            => new FMTests_Technical().FM_Trombone_Melody2_Notation_WithDuration_DurationPlusMildEchoTime_RunTest();

        void FM_Trombone_Melody2_Notation_WithDuration_DurationPlusMildEchoTime_RunTest()
        {
            _x.Play(() => _x.MildEcho(_x.TromboneMelody2.WithDuration(_x.Duration + _x.MildEchoTime), volume: 0.75));
        }

        [TestMethod]
        public void FM_Trombone_Melody2_Issue_MelodyOverridesEchoTimeDuration() 
            => new FMTests_Technical().FM_Trombone_Melody2_Issue_MelodyOverridesEchoTimeDuration_RunTest();

        void FM_Trombone_Melody2_Issue_MelodyOverridesEchoTimeDuration_RunTest()
        {
            _x.WithDuration(_x.beats[8] + _x.MildEchoTime);

            _x.Play(() => _x.MildEcho(_x.TromboneMelody2, volume: 0.75));
        }
    }
}
