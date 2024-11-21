using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    public class ParallelWishesTests : SynthWishes
    {
        [TestMethod]
        public void SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot_Test()
            => new ParallelWishesTests().SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot();
        
        private void SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot()
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
                 ) * Envelope);
        }
        
        [TestMethod]
        public void FluentPlay_UsingTape_Test() => new ParallelWishesTests().FluentPlay_UsingTape();
        
        private void FluentPlay_UsingTape()
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
                 ) * Envelope);
        }
        
        [TestMethod]
        public void FluentSave_UsingTape_Test() => new ParallelWishesTests().FluentSave_UsingTape();
        
        private void FluentSave_UsingTape()
        {
            WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0);
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Save(MemberName() + " Partial 1"),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).Save("TapeThatIsSaved Partial 2").Volume(0.3),
                     Sine(pitch * 4).Volume(0.4),
                     Sine(pitch * 5).Volume(0.2).SetName("TapeThatIsSaved Partial 3").Save()
                 ) * Envelope).Save();
        }
        
        [TestMethod]
        public void Tape_Streaming_GoesPerChannel_Test() => new ParallelWishesTests().Tape_Streaming_GoesPerChannel();
        
        private void Tape_Streaming_GoesPerChannel()
        {
            WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0);
            WithStereo();
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Panning(0.2).Play(),
                     Sine(pitch * 2).Volume(0.2).Panning(0.4).Play(),
                     Sine(pitch * 3).Volume(0.3).Panning(0.9).Play()
                 ) * Envelope * 1.5);
            
            Play(() => Add
                 (
                     1.0 * Sine(pitch * 1).Panbrello(3.000, 0.2).Play(),
                     0.2 * Sine(pitch * 2).Panbrello(5.234, 0.3).Play(),
                     0.3 * Sine(pitch * 3).Panbrello(7.000, 0.2).Play()
                 ) * Envelope * 1.5);
        }
        
        FlowNode Envelope => Curve(0.4, 0.4);
    }
}