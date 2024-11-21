using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ParallelWishesTests : SynthWishes
    {
        [TestMethod]
        public void SelectiveTape_InconsistentDelay_BecauseASineIsForever_Test() 
            => new ParallelWishesTests().SelectiveTape_InconsistentDelay_BecauseASineIsForever();
        
        private void SelectiveTape_InconsistentDelay_BecauseASineIsForever()
        {
            Play(() => Sine(A3).Tape() + Sine(A4));
        }
        
        [TestMethod]
        public void PlayAllTapesTest() => new ParallelWishesTests().PlayAllTapes();
        
        private void PlayAllTapes()
        {
            WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0);
            WithPlayAllTapes();
            
            var pitch = G4;
            
            Play(() => ParallelAdd
                 (
                     Sine(pitch * 1).Volume(1.0),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).Volume(0.3)
                 ).Curve(0.4, 0.4));
        }
        
        [TestMethod]
        public void TapeThatPlaysTest() => new ParallelWishesTests().TapeThatPlays();
        
        private void TapeThatPlays()
        {
            WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0);
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Play(),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).Play().Volume(0.3),
                     Sine(pitch * 4).Volume(0.4),
                     Sine(pitch * 5).Volume(0.2).Play()
                 ).Curve(0.4, 0.4));
        }
    }
}
