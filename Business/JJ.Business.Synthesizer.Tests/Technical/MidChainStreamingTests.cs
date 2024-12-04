using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.NameHelper;

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class MidChainStreamingTests : MySynthWishes
    {
        FlowNode Envelope => DelayedPulseCurve.Stretch(GetAudioLength);
        
        public MidChainStreamingTests()
        {
            //WithShortDuration();
        }
        
        // Problems
        
        [TestMethod]
        public void MidChainStreaming_Stereo_WentPerChannel_Test() => new MidChainStreamingTests().MidChainStreaming_Stereo_WentPerChannel();
        void MidChainStreaming_Stereo_WentPerChannel()
        {
            var pitch = A4;
            
            WithStereo();

            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Curve(Envelope).Panning(0.2).PlayChannel(),
                     Sine(pitch * 2).Volume(0.3).Curve(Envelope).Panning(0.8).PlayChannel()
                 ) * Envelope * 1.5);

            Play(() => Add
                 (
                     1.0 * Sine(pitch * 1).Curve(Envelope).Panbrello(3.000, 0.2).PlayChannel(),
                     0.2 * Sine(pitch * 2).Curve(Envelope).Panbrello(5.234, 0.3).PlayChannel(),
                     0.3 * Sine(pitch * 3).Curve(Envelope).Panbrello(7.000, 0.2).PlayChannel()
                 ) * Envelope * 1.5);
        }
        
        [TestMethod]
        public void MidChainStreaming_Stereo_RecombineChannelsExplicit_Test() => new MidChainStreamingTests().MidChainStreaming_Stereo_RecombineChannelsExplicit();
        void MidChainStreaming_Stereo_RecombineChannelsExplicit() 
        {
            WithStereo();
            
            var buffs = new Buff[2];
            
            // The delegate creates a non-trivial convergence point.
            
            Save(() => Sine(A4).Panning(0.1).Curve(Envelope).CacheChannel((b, i) => buffs[i] = b)).Play();
            
            IsNotNull(() => buffs[0]);
            IsNotNull(() => buffs[1]);
            
            buffs[0].Play();
            buffs[1].Play();
            
            // Recombination can only be done after running all tapes.
           
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
        
        [TestMethod]
        public void MidChainStreaming_Stereo_RecombineChannelsExplicitly_ShortTest() => new MidChainStreamingTests().MidChainStreaming_Stereo_RecombineChannelsExplicitly_Short();
        void MidChainStreaming_Stereo_RecombineChannelsExplicitly_Short() 
        {
            WithStereo();
            
            var buffs = new Buff[2];
            
            Save(() => Sine(A4).Panning(0.1).Curve(Envelope).CacheChannel((b, i) => buffs[i] = b)).Play();
            
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
        
        // Simple Cases

        [TestMethod]
        public void MidChainStreaming_Mono_Play_Test() => new MidChainStreamingTests().MidChainStreaming_Mono_Play();
        void MidChainStreaming_Mono_Play()
        { 
            WithMono();
            Run(() => Sine(A4).Curve(Envelope).Volume(0.4).Play().SpeedUp(2).Play());
        }
        
        [TestMethod]
        public void MidChainStreaming_Mono_Save_Test() => new MidChainStreamingTests().MidChainStreaming_Mono_Save();
        void MidChainStreaming_Mono_Save()
        { 
            WithMono();
            Run(() => Sine(A4).Curve(Envelope).Volume(0.4).Save().SpeedUp(2).Save());
        }
        
        [TestMethod]
        public void MidChainStreaming_Mono_Cache_Test() => new MidChainStreamingTests().MidChainStreaming_Mono_Cache();
        void MidChainStreaming_Mono_Cache()
        { 
            WithMono();
            Run(() => Sine(A4).Curve(Envelope).Volume(0.4).Cache().SpeedUp(2).Cache());
        }
        
        [TestMethod]
        public void MidChainStreaming_Mono_PlayChannel_Test() => new MidChainStreamingTests().MidChainStreaming_Mono_PlayChannel();
        void MidChainStreaming_Mono_PlayChannel()
        {
            WithMono();
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Curve(Envelope).Volume(1.0).PlayChannel(),
                     Sine(pitch * 2).Curve(Envelope).Volume(0.2),
                     Sine(pitch * 3).Curve(Envelope).PlayChannel().Volume(0.3),
                     Sine(pitch * 4).Curve(Envelope).Volume(0.4),
                     Sine(pitch * 5).Curve(Envelope).Volume(0.2).PlayChannel()
                 ));
        }
        
        [TestMethod]
        public void MidChainStreaming_Mono_SaveChannel_Test() => new MidChainStreamingTests().MidChainStreaming_Mono_SaveChannel();
        void MidChainStreaming_Mono_SaveChannel()
        {
            WithMono();
            
            var pitch = A4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).SaveChannel(MemberName() + " Partial 1"),
                     Sine(pitch * 2).Volume(0.2),
                     Sine(pitch * 3).SaveChannel("FluentSave_UsingTape Partial 2").Volume(0.3),
                     Sine(pitch * 4).Volume(0.4),
                     Sine(pitch * 5).Volume(0.2).SetName("FluentSave_UsingTape Partial 3").SaveChannel()
                 ) * Envelope).Save();
        }
        
        [TestMethod]
        public void MidChainStreaming_Stereo_Play_Test() => new MidChainStreamingTests().MidChainStreaming_Stereo_Play();
        void MidChainStreaming_Stereo_Play() 
        {
            WithStereo();
            Save(() => Sine(A4).Panning(0.1).Play().Curve(Envelope)).Play();
        }
        
        // Complex Cases
        
        [TestMethod]
        public void MidChainStreaming_Stereo_MultipleActions_Test() => new MidChainStreamingTests().MidChainStreaming_Stereo_MultipleActions();
        void MidChainStreaming_Stereo_MultipleActions() 
        {
            WithStereo();

            var pitch = G4;
            
            Play(() => Add
            (
                Sine(pitch * 1).Curve(Envelope)/*.Play()*/,
                Sine(pitch * 2).Curve(Envelope).Volume(0.2),
                Sine(pitch * 3).Curve(Envelope).Panning(0.03).Play().Volume(0.1),
                Sine(pitch * 4).Curve(Envelope).Volume(0.08),
                Sine(pitch * 5).Volume(0.05).Curve(Envelope).Panning(0.9)/*.PlayChannel((b, i) => b.Save())*/.Curve(Envelope)
            ));
        }

    }
}
