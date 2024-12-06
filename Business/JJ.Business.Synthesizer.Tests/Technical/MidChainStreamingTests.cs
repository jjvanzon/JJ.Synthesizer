using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Mathematics_Copied;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.NameHelper;
// ReSharper disable ExplicitCallerInfoArgument
// ReSharper disable ParameterHidesMember
// ReSharper disable AccessToModifiedClosure

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class MidChainStreamingTests : MySynthWishes
    {
        //FlowNode DelayCurve => DelayedPulseCurve.Stretch(GetAudioLength);
        
        double RandomVolume => Randomizer_Copied.GetDouble(0.6, 1.0);
        FlowNode RandomNote => Randomizer_Copied.GetRandomItem(new [] {D4, E4, G4, A4});

        public MidChainStreamingTests()
        {
            //WithShortDuration();
            //WithAudioLength(0.5);
        }
        
        // Problems
        
        [TestMethod]
        public void Stereo_WentPerChannel_Test() => new MidChainStreamingTests().Stereo_WentPerChannel();
        void Stereo_WentPerChannel()
        {
            var pitch = RandomNote;
            
            WithStereo();
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Volume(1.0).Curve(RecorderCurve * RandomVolume).Panning(0.2).PlayChannel(),
                     Sine(pitch * 2).Volume(0.3).Curve(RecorderCurve * RandomVolume).Panning(0.8).PlayChannel()
                 ));

            pitch = RandomNote;
            
            Play(() => Add
                 (
                     1.0 * Sine(pitch * 1).Curve(RecorderCurve * RandomVolume).Panbrello(3.000, 0.2).PlayChannel(),
                     0.2 * Sine(pitch * 2).Curve(RecorderCurve * RandomVolume).Panbrello(5.234, 0.3).PlayChannel(),
                     0.3 * Sine(pitch * 3).Curve(RecorderCurve * RandomVolume).Panbrello(7.000, 0.2).PlayChannel()
                 ));
        }
        
        [TestMethod]
        public void Stereo_RecombineChannelsExplicit_Test() => new MidChainStreamingTests().Stereo_RecombineChannelsExplicit();
        void Stereo_RecombineChannelsExplicit() 
        {
            WithStereo();
            
            var pitch = RandomNote;
            
            var buffs = new Buff[2];
            
            // The delegate creates a non-trivial convergence point.
            
            Save(() => Sine(pitch).Panning(0.05).Curve(RecorderCurve * RandomVolume).CacheChannel((b, i) => buffs[i] = b)).Play();
            
            IsNotNull(() => buffs[0]);
            IsNotNull(() => buffs[1]);
            
            buffs[0].Play();
            buffs[1].Play();
            
            // Recombination can only be done after running all tapes.
           
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
        
        [TestMethod]
        public void Stereo_RecombineChannelsExplicitly_ShortTest() => new MidChainStreamingTests().Stereo_RecombineChannelsExplicitly_Short();
        void Stereo_RecombineChannelsExplicitly_Short() 
        {
            WithStereo();
            
            var pitch = RandomNote;

            var buffs = new Buff[2];
            
            Save(() => Sine(pitch).Panning(0.05).Curve(RecorderCurve * RandomVolume).CacheChannel((b, i) => buffs[i] = b)).Play();
            
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
        
        // Simple Cases
        
        [TestMethod]
        public void Mono_Play_Test() => Run(Mono_Play);
        void Mono_Play()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().Play();
        }
        
        [TestMethod]
        public void Mono_Play_Test_2Calls() => Run(Mono_Play_2Calls);
        void Mono_Play_2Calls()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().Play().SpeedUp(2).Play();
        }
        
        [TestMethod]
        public void Mono_Save_Test() => Run(Mono_Save);
        void Mono_Save()
        { 
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().Save().Play();
        }
        
        [TestMethod]
        public void Mono_Save_Test_2Calls() => Run(Mono_Save_2Calls);
        void Mono_Save_2Calls()
        { 
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().Save().SpeedUp(2).Save().Play();
        }
        
        [TestMethod]
        public void Mono_Cache_Test() => Run(Mono_Cache);
        void Mono_Cache()
        { 
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().Cache().Play();
        }
        
        [TestMethod]
        public void Mono_Cache_Test_2Calls() => Run(Mono_Cache_2Calls);
        void Mono_Cache_2Calls()
        { 
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().Cache().SpeedUp(2).Cache().Play();
        }

        [TestMethod]
        public void Mono_PlayChannel_Test() => Run(Mono_PlayChannel);
        void Mono_PlayChannel()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().PlayChannel();
        }
        
        [TestMethod]
        public void Mono_PlayChannel_Test_2Calls() => Run(Mono_PlayChannel_2Calls);
        void Mono_PlayChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().PlayChannel().SpeedUp(2).Play();
        }
        
        [TestMethod]
        public void Mono_PlayChannel_Test_3Calls() => Run(Mono_PlayChannel_3Calls);
        void Mono_PlayChannel_3Calls()
        {
            WithMono();
            
            var pitch = RandomNote;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Curve(RecorderCurve * RandomVolume).Volume(1.0).PlayChannel(),
                     Sine(pitch * 2).Curve(RecorderCurve * RandomVolume).Volume(0.1),
                     Sine(pitch * 3).Curve(RecorderCurve * RandomVolume).PlayChannel().Volume(0.15),
                     Sine(pitch * 4).Curve(RecorderCurve * RandomVolume).Volume(0.1),
                     Sine(pitch * 5).Curve(RecorderCurve * RandomVolume).Volume(0.05).PlayChannel()
                 ));
        }
        
        [TestMethod]
        public void Mono_SaveChannel_Test() => Run(Mono_SaveChannel);
        void Mono_SaveChannel()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().SaveChannel().Play();
        }
        
        [TestMethod]
        public void Mono_SaveChannel_Test_2Calls() => Run(Mono_SaveChannel_2Calls);
        void Mono_SaveChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().SaveChannel().SpeedUp(2).Save().Play();
        }

        [TestMethod]
        public void Mono_SaveChannel_Test_3Calls() => Run(Mono_SaveChannel_3Calls);
        void Mono_SaveChannel_3Calls()
        {
            WithMono();
            
            var pitch = D4;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Curve(RecorderCurve * RandomVolume).Volume(1.0).SaveChannel(MemberName() + " Partial 1"),
                     Sine(pitch * 2).Curve(RecorderCurve * RandomVolume).Volume(0.1),
                     Sine(pitch * 3).Curve(RecorderCurve * RandomVolume).SaveChannel("FluentSave_UsingTape Partial 2").Volume(0.05),
                     Sine(pitch * 4).Curve(RecorderCurve * RandomVolume).Volume(0.01),
                     Sine(pitch * 5).Curve(RecorderCurve * RandomVolume).Volume(0.02).SetName("FluentSave_UsingTape Partial 3").SaveChannel()
                 )).Save();
        }
        
        [TestMethod]
        public void Mono_CacheChannel_Test() => Run(Mono_CacheChannel);
        void Mono_CacheChannel()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().CacheChannel().Play();
        }
        
        [TestMethod]
        public void Mono_CacheChannel_Test_2Calls() => Run(Mono_CacheChannel_2Calls);
        void Mono_CacheChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Curve(RecorderCurve * RandomVolume).Panbrello().CacheChannel().SpeedUp(2).Cache().Play();
        }

        [TestMethod]
        public void Mono_CacheChannel_Test_3Calls() => Run(Mono_CacheChannel_3Calls);
        void Mono_CacheChannel_3Calls()
        {
            WithMono();
            
            var pitch = E3;
            
            Play(() => Add
                 (
                     Sine(pitch * 1).Curve(RecorderCurve * RandomVolume).Volume(1.0).CacheChannel(),
                     Sine(pitch * 2).Curve(RecorderCurve * RandomVolume).Volume(0.05),
                     Sine(pitch * 3).Curve(RecorderCurve * RandomVolume).CacheChannel().Volume(0.02),
                     Sine(pitch * 4).Curve(RecorderCurve * RandomVolume).Volume(0.03),
                     Sine(pitch * 5).Curve(RecorderCurve * RandomVolume).Volume(0.01).CacheChannel()
                 )).Cache();
        }
        
        [TestMethod]
        public void Stereo_Play_Test() => WithStereo().Run(Stereo_Play);
        void Stereo_Play()
        {
            Sine(D4).Curve(RecorderCurve * RandomVolume).Panbrello().Play();
        }
        
        [TestMethod]
        public void Stereo_Play_Test_2Calls() => Run(Stereo_Play_2Calls);
        void Stereo_Play_2Calls()
        { 
            WithStereo().Sine(E4).Curve(RecorderCurve * RandomVolume).Panbrello().Play().SpeedUp(2).Play();
        }
        
        [TestMethod]
        public void Stereo_Save_Test() => Run(Stereo_Save);
        void Stereo_Save()
        { 
            WithStereo().Sine(G4).Curve(RecorderCurve * RandomVolume).Panbrello().Save().Play();
        }
        
        [TestMethod]
        public void Stereo_Save_Test_2Calls() => Run(Stereo_Save_2Calls);
        void Stereo_Save_2Calls()
        { 
            WithStereo().Sine(A4).Curve(RecorderCurve * RandomVolume).Panbrello().Save().SpeedUp(2).Save().Play();
        }
        
        [TestMethod]
        public void Stereo_Cache_Test() => Run(Stereo_Cache);
        void Stereo_Cache()
        { 
            WithStereo().Sine(D4).Curve(RecorderCurve * RandomVolume).Panbrello().Cache().Play();
        }
        
        [TestMethod]
        public void Stereo_Cache_Test_2Calls() => Run(Stereo_Cache_2Calls);
        void Stereo_Cache_2Calls()
        { 
            WithStereo().Sine(E4).Curve(RecorderCurve * RandomVolume).Panbrello().Cache().SpeedUp(2).Cache().Play();
        }
        
        [TestMethod]
        public void Stereo_PlayChannel_Test() => WithStereo().Run(Stereo_PlayChannel);
        void Stereo_PlayChannel()
        { 
            Sine(B4).Curve(RecorderCurve * RandomVolume).Panbrello().PlayChannel().Play();
        }
        
        [TestMethod]
        public void Stereo_PlayChannel_Test_2Calls() => Run(Stereo_PlayChannel_2Calls);
        void Stereo_PlayChannel_2Calls()
        { 
            WithStereo().Sine(G4).Curve(RecorderCurve * RandomVolume).Panbrello().PlayChannel().SpeedUp(2).PlayChannel();
        }
        
        [TestMethod]
        public void Stereo_SaveChannel_Test() => Run(Stereo_SaveChannel);
        void Stereo_SaveChannel()
        { 
            WithStereo().Sine(A4).Curve(RecorderCurve * RandomVolume).Panbrello().SaveChannel().Play();
        }
        
        [TestMethod]
        public void Stereo_SaveChannel_Test_2Calls() => Run(Stereo_SaveChannel_2Calls);
        void Stereo_SaveChannel_2Calls()
        { 
            WithStereo().Sine(D4).Curve(RecorderCurve * RandomVolume).Panbrello().SaveChannel().SpeedUp(2).SaveChannel().Play();
        }
        
        [TestMethod]
        public void Stereo_CacheChannel_Test() => Run(Stereo_CacheChannel);
        void Stereo_CacheChannel()
        { 
            WithStereo().Sine(E4).Curve(RecorderCurve * RandomVolume).Panbrello().CacheChannel().Play();
        }
        
        [TestMethod]
        public void Stereo_CacheChannel_Test_2Calls() => Run(Stereo_CacheChannel_2Calls);
        void Stereo_CacheChannel_2Calls()
        { 
            WithStereo().Sine(B4).Curve(RecorderCurve * RandomVolume).Panbrello().CacheChannel().SpeedUp(2).CacheChannel().Play();
        }
        
        // Complex Cases
        
        [TestMethod]
        public void Stereo_MultipleActions_Test() => Run(Stereo_MultipleActions);
        void Stereo_MultipleActions() 
        {
            WithStereo();

            var pitch = E4;
            
            Play(() => Add
            (
                Sine(pitch * 1).Curve(RecorderCurve * RandomVolume).Play(),
                Sine(pitch * 2).Curve(RecorderCurve * RandomVolume).Volume(0.2),
                Sine(pitch * 3).Curve(RecorderCurve * RandomVolume).Panning(0.03).Play().Volume(0.1),
                Sine(pitch * 4).Curve(RecorderCurve * RandomVolume).Volume(0.08),
                Sine(pitch * 5).Volume(0.05).Curve(RecorderCurve * RandomVolume).Panning(0.9).PlayChannel((b, i) => b.Save())
            ));
        }
    }
}
