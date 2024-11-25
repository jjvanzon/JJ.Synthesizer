using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Framework.Testing.AssertHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class TapeWishesTests : SynthWishes
    {
        public TapeWishesTests()
        {
            WithShortDuration();
        }
        
        [TestMethod]
        public void SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot_Test()
            => new TapeWishesTests().SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot();
        
        private void SelectiveTape_InconsistentDelay_BecauseASineIsForever_AndATapeIsNot()
        {
            Play(() => Sine(A3).Tape() + Sine(A4));
        }
        
        [TestMethod]
        public void PlayAllTapesTest() => new TapeWishesTests().PlayAllTapes();
        
        private void PlayAllTapes()
        {
            WithPlayAllTapes();
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Curve(Envelope).Tape(),
                     Sine(pitch * 2).Volume(0.2).Curve(Envelope).Tape(),
                     Sine(pitch * 3).Volume(0.3).Curve(Envelope).Tape()
                 ));
        }
        
        [TestMethod]
        public void FluentPlay_UsingTape_Test() => new TapeWishesTests().FluentPlay_UsingTape();
        
        private void FluentPlay_UsingTape()
        {
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Curve(Envelope).Volume(1.0).ChannelPlay(),
                     Sine(pitch * 2).Curve(Envelope).Volume(0.2),
                     Sine(pitch * 3).Curve(Envelope).ChannelPlay().Volume(0.3),
                     Sine(pitch * 4).Curve(Envelope).Volume(0.4),
                     Sine(pitch * 5).Curve(Envelope).Volume(0.2).ChannelPlay()
                 ));
        }
        
        [TestMethod]
        public void FluentSave_UsingTape_Test() => new TapeWishesTests().FluentSave_UsingTape();
        
        private void FluentSave_UsingTape()
        {
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).ChannelSave(MemberName() + " Partial 1"),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).ChannelSave("FluentSave_UsingTape Partial 2").Volume(0.3),
                     Sine(pitch * 4).Volume(0.4),
                     Sine(pitch * 5).Volume(0.2).SetName("FluentSave_UsingTape Partial 3").ChannelSave()
                 ) * Envelope).Save();
        }
        
        [TestMethod]
        public void Tape_Streaming_GoesPerChannel_Test() => new TapeWishesTests().Tape_Streaming_GoesPerChannel();
        
        private void Tape_Streaming_GoesPerChannel()
        {
            var pitch = A4;
            
            WithStereo();

            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Curve(Envelope).Panning(0.2).ChannelPlay(),
                     Sine(pitch * 2).Volume(0.3).Curve(Envelope).Panning(0.8).ChannelPlay()
                 ) * Envelope * 1.5);

            Play(() => Add
                 (
                     1.0 * Sine(pitch * 1).Curve(Envelope).Panbrello(3.000, 0.2).ChannelPlay(),
                     0.2 * Sine(pitch * 2).Curve(Envelope).Panbrello(5.234, 0.3).ChannelPlay(),
                     0.3 * Sine(pitch * 3).Curve(Envelope).Panbrello(7.000, 0.2).ChannelPlay()
                 ) * Envelope * 1.5);
        }
        
        // ReSharper disable ParameterHidesMember
        [TestMethod]
        public void FluentCache_UsingTape_Test() => new TapeWishesTests().FluentCache_UsingTape();
        
        void FluentCache_UsingTape() 
        {
            WithStereo();

            var bufs = new Buff[2];
            
            Save(() => Sine(A4).Panning(0.1).ChannelCache((b, i) => bufs[i] = b)).Play();
            
            IsNotNull(() => bufs[0]);
            IsNotNull(() => bufs[1]);

            bufs[0].Play();
            bufs[1].Play();

            Save(() => Sample(bufs[0]).Panning(0) +
                       Sample(bufs[1]).Panning(1)).Play();
        }

        // Helpers
        
        void WithShortDuration() => WithAudioLength(0.5).WithLeadingSilence(0).WithTrailingSilence(0);
        
        FlowNode BaseEnvelope => Curve((0, 0), (0.2, 0), (0.3, 1), (0.7, 1), (0.8, 0), (1.0, 0));
        FlowNode Envelope => BaseEnvelope.Stretch(GetAudioLength) * 0.4;
    }
}