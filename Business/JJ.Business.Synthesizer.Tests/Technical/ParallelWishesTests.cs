using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Wishes;
using JJ.Framework.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
using static JJ.Framework.Testing.AssertHelper;

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
            WithShortDuration();
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
            WithShortDuration();
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).ChannelPlay(),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).ChannelPlay().Volume(0.3),
                     Sine(pitch * 4).Volume(0.4),
                     Sine(pitch * 5).Volume(0.2).ChannelPlay()
                 ) * Envelope);
        }
        
        [TestMethod]
        public void FluentSave_UsingTape_Test() => new ParallelWishesTests().FluentSave_UsingTape();
        
        private void FluentSave_UsingTape()
        {
            WithShortDuration();
            
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
        public void Tape_Streaming_GoesPerChannel_Test() => new ParallelWishesTests().Tape_Streaming_GoesPerChannel();
        
        private void Tape_Streaming_GoesPerChannel()
        {
            var pitch = A4;
            
            WithShortDuration();
            WithStereo();

            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Panning(0.2).ChannelPlay(),
                     Sine(pitch * 2).Volume(0.3).Panning(0.8).ChannelPlay()
                 ) * Envelope * 1.5);

            Play(() => Add
                 (
                     1.0 * Sine(pitch * 1).Panbrello(3.000, 0.2).ChannelPlay(),
                     0.2 * Sine(pitch * 2).Panbrello(5.234, 0.3).ChannelPlay(),
                     0.3 * Sine(pitch * 3).Panbrello(7.000, 0.2).ChannelPlay()
                 ) * Envelope * 1.5);
        }
        
        // ReSharper disable ParameterHidesMember
        [TestMethod]
        public void FluentCache_UsingTape_Test() => new ParallelWishesTests().FluentCache_UsingTape();
        
        void FluentCache_UsingTape() 
        {
            WithStereo();
            WithShortDuration();

            var bufs = new AudioStreamResult[2];
            
            Save(() => Sine(G4).Panning(0.1).ChannelCache((b, i) => bufs[i] = b)).Play();

            bufs.ForEach(x => x.Play());

            Save(() => Sample(bufs[0]).Panning(0) +
                       Sample(bufs[1]).Panning(1)).Play();
        }

        // Helper
        
        void WithShortDuration() => WithAudioLength(0.3).WithLeadingSilence(0.2).WithTrailingSilence(0.2);
        FlowNode Envelope => Curve(0.4, 0.4);
    }
}