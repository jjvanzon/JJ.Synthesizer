using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ParallelWishesTests : SynthWishes
    {
        [TestMethod]
        public void TapeMustPlayTest() => new ParallelWishesTests().TapeMustPlay();
        
        private void TapeMustPlay()
        {
            var freq = G4;
            
            Play(() => 0.8 * Add
                 (
                     Sine(freq * 1).Volume(1.0).Play(),
                     Sine(freq * 2).Volume(0.2),
                     Sine(freq * 3).Volume(0.3).Play(),
                     Sine(freq * 4).Volume(0.4),
                     Sine(freq * 5).Volume(0.2).Play()
                 ));
        }
    }
}
