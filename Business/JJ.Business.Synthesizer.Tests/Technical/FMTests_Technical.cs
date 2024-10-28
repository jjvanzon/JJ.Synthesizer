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
        public void FM_Trombone_Melody2_Notation_MildEcho_AddsTheAudioLength() => new FMTests_Technical().FM_Trombone_Melody2_Notation_MildEcho_AddsTheAudioLength_RunTest();

        void FM_Trombone_Melody2_Notation_MildEcho_AddsTheAudioLength_RunTest()
        {
            _x.Play(() => _x.MildEcho(_x.TromboneMelody2, volume: 0.75));
        }

        [TestMethod]
        public void FM_Trombone_Melody2_Notation_AddAudioLength_MildEchoTime()
            => new FMTests_Technical().FM_Trombone_Melody2_Notation_AddAudioLength_MildEchoTime_RunTest();

        void FM_Trombone_Melody2_Notation_AddAudioLength_MildEchoTime_RunTest()
        {
            _x.Play(() => _x.MildEcho(_x.TromboneMelody2, volume: 0.75));
        }

        [TestMethod]
        public void FM_Trombone_Melody2_Notation_WithAudioLength_AudioLengthPlusMildEchoTime() 
            => new FMTests_Technical().FM_Trombone_Melody2_Notation_WithAudioLength_AudioLengthPlusMildEchoTime_RunTest();

        void FM_Trombone_Melody2_Notation_WithAudioLength_AudioLengthPlusMildEchoTime_RunTest()
        {
            _x.Play(() => _x.MildEcho(_x.TromboneMelody2, volume: 0.75));
        }

        [TestMethod]
        public void FM_Trombone_Melody2_Issue_MelodyOverridesEchoTimeAudioLength()
            => new FMTests_Technical().FM_Trombone_Melody2_Issue_MelodyOverridesEchoTimeAudioLength_RunTest();

        void FM_Trombone_Melody2_Issue_MelodyOverridesEchoTimeAudioLength_RunTest()
        {
            var mildEchoTime = _x._[0.75];
            
            _x.WithAudioLength(_x.beats[8] + mildEchoTime);

            _x.Play(() => _x.MildEcho(_x.TromboneMelody2, volume: 0.75));
        }
    }
}
