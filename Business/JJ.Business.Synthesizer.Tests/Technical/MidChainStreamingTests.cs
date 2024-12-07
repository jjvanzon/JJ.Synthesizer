using System.Collections.Generic;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Mathematics_Wishes.RandomizerWishes;
using static JJ.Business.Synthesizer.Wishes.Helpers.JJ_Framework_Mathematics_Copied.Randomizer_Copied;
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
        double RandomNumber(double min, double max) => GetDouble(min, max);
        
        double RandomVolume => RandomNumber(0.6, 1);
        FlowNode RandomVolumeSlope => Curve(RandomVolume, RandomVolume);
        
        FlowNode DynamicCurve 
            => RecorderCurve.Multiply(RandomVolumeSlope)
                            .Stretch(GetAudioLength)
                            .Tremolo(
                                speed: RandomNumber(2, 4), 
                                depth: RandomNumber(0.02, 0.1));

        FlowNode RandomNote => GetRandomItem
        (
            A3, B3, Cs4, E4, Fs4,
            A4, B4, Cs5, E5, Fs5
        );
        IList<FlowNode> RandomNotes { get; }
        
        public MidChainStreamingTests()
        {
            RandomNotes = new[]
            {
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote,
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote,
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote,
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote
            };
            
            WithAudioLength(0.4);
        }
        
        // Problems
        
        [TestMethod]
        public void Stereo_WentPerChannel_Test() => new MidChainStreamingTests().Stereo_WentPerChannel();
        void Stereo_WentPerChannel()
        {
            WithStereo();
            
            Play(() => Add
            (
                Sine(RandomNotes[0] * 1).Volume(1.0).Volume(DynamicCurve).Panning(0.2).PlayChannel(),
                Sine(RandomNotes[0] * 2).Volume(0.3).Volume(DynamicCurve).Panning(0.8).PlayChannel()
            ));

            Play(() => Add
            (
                1.0 * Sine(RandomNotes[1] * 1).Volume(DynamicCurve).Panbrello(3.000, 0.2).PlayChannel(),
                0.2 * Sine(RandomNotes[1] * 2).Volume(DynamicCurve).Panbrello(5.234, 0.3).PlayChannel(),
                0.3 * Sine(RandomNotes[1] * 3).Volume(DynamicCurve).Panbrello(7.000, 0.2).PlayChannel()
            ));
        }
        
        [TestMethod]
        public void Stereo_RecombineChannelsExplicit_Test() => new MidChainStreamingTests().Stereo_RecombineChannelsExplicit();
        void Stereo_RecombineChannelsExplicit() 
        {
            WithStereo();
            
            var buffs = new Buff[2];
            
            // The delegate creates a non-trivial convergence point.
            
            Save(() => Sine(RandomNotes[1]).Panning(0.1).Volume(DynamicCurve).CacheChannel((b, i) => buffs[i] = b)).Play();
            
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
            
            var buffs = new Buff[2];
            
            Save(() => Sine(RandomNotes[2]).Panning(0.9).Volume(DynamicCurve).CacheChannel((b, i) => buffs[i] = b)).Play();
            
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
        
        // Simple Cases
        
        [TestMethod]
        public void Mono_Play_Test() => Run(Mono_Play);
        void Mono_Play()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().Play();
        }
        
        [TestMethod]
        public void Mono_Play_Test_2Calls() => Run(Mono_Play_2Calls);
        void Mono_Play_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().Play().SpeedUp(1.5).Play();
        }
        
        [TestMethod]
        public void Mono_Save_Test() => Run(Mono_Save);
        void Mono_Save()
        { 
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().Save().Play();
        }
        
        [TestMethod]
        public void Mono_Save_Test_2Calls() => Run(Mono_Save_2Calls);
        void Mono_Save_2Calls()
        { 
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().Save().SpeedUp(1.5).Save().Play();
        }
        
        [TestMethod]
        public void Mono_Cache_Test() => Run(Mono_Cache);
        void Mono_Cache()
        { 
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().Cache().Play();
        }
        
        [TestMethod]
        public void Mono_Cache_Test_2Calls() => Run(Mono_Cache_2Calls);
        void Mono_Cache_2Calls()
        { 
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().Cache().SpeedUp(1.5).Cache().Play();
        }

        [TestMethod]
        public void Mono_PlayChannel_Test() => Run(Mono_PlayChannel);
        void Mono_PlayChannel()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().PlayChannel();
        }
        
        [TestMethod]
        public void Mono_PlayChannel_Test_2Calls() => Run(Mono_PlayChannel_2Calls);
        void Mono_PlayChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().PlayChannel().SpeedUp(1.5).PlayChannel();
        }
        
        [TestMethod]
        public void Mono_PlayChannel_Test_3Calls() => Run(Mono_PlayChannel_3Calls);
        void Mono_PlayChannel_3Calls()
        {
            WithMono();
            
            Play(() => Add
            (
                Sine(RandomNotes[3] * 1).Volume(DynamicCurve).Volume(1.0).PlayChannel(),
                Sine(RandomNotes[3] * 2).Volume(DynamicCurve).Volume(0.1),
                Sine(RandomNotes[3] * 3).Volume(DynamicCurve).PlayChannel().Volume(0.15),
                Sine(RandomNotes[3] * 4).Volume(DynamicCurve).Volume(0.08),
                Sine(RandomNotes[3] * 5).Volume(DynamicCurve).Volume(0.05).PlayChannel()
            ));
        }
        
        [TestMethod]
        public void Mono_SaveChannel_Test() => Run(Mono_SaveChannel);
        void Mono_SaveChannel()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().SaveChannel().Play();
        }
        
        [TestMethod]
        public void Mono_SaveChannel_Test_2Calls() => Run(Mono_SaveChannel_2Calls);
        void Mono_SaveChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().SaveChannel().SpeedUp(1.5).SaveChannel().Play();
        }

        [TestMethod]
        public void Mono_SaveChannel_Test_3Calls() => Run(Mono_SaveChannel_3Calls);
        void Mono_SaveChannel_3Calls()
        {
            WithMono();
            
            Add
            (
                Sine(RandomNotes[4] * 1).Volume(DynamicCurve).Volume(1.0).SaveChannel(MemberName() + " Partial 1"),
                Sine(RandomNotes[4] * 2).Volume(DynamicCurve).Volume(0.1),
                Sine(RandomNotes[4] * 3).Volume(DynamicCurve).SaveChannel(FetchName() + " Partial 2").Volume(0.05),
                Sine(RandomNotes[4] * 4).Volume(DynamicCurve).Volume(0.01),
                Sine(RandomNotes[4] * 5).Volume(DynamicCurve).Volume(0.02).SetName(MemberName() + " Partial 3").SaveChannel()
            ).Play();
        }
        
        [TestMethod]
        public void Mono_CacheChannel_Test() => Run(Mono_CacheChannel);
        void Mono_CacheChannel()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().CacheChannel().Play();
        }
        
        [TestMethod]
        public void Mono_CacheChannel_Test_2Calls() => Run(Mono_CacheChannel_2Calls);
        void Mono_CacheChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(DynamicCurve).Panbrello().CacheChannel().SpeedUp(1.5).CacheChannel().Play();
        }

        [TestMethod]
        public void Mono_CacheChannel_Test_3Calls() => Run(Mono_CacheChannel_3Calls);
        void Mono_CacheChannel_3Calls()
        {
            WithMono();
            
            Play(() => Add
            (
                Sine(RandomNotes[5] * 1).Volume(DynamicCurve).Volume(1.0).CacheChannel(),
                Sine(RandomNotes[5] * 2).Volume(DynamicCurve).Volume(0.05),
                Sine(RandomNotes[5] * 3).Volume(DynamicCurve).CacheChannel().Volume(0.02),
                Sine(RandomNotes[5] * 4).Volume(DynamicCurve).Volume(0.03),
                Sine(RandomNotes[5] * 5).Volume(DynamicCurve).Volume(0.01).CacheChannel()
            )).Cache();
        }
        
        [TestMethod]
        public void Stereo_Play_Test() => WithStereo().Run(Stereo_Play);
        void Stereo_Play()
        {
            Sine(RandomNotes[6]).Curve(DynamicCurve).Panbrello().Play();
        }
        
        [TestMethod]
        public void Stereo_Play_Test_2Calls() => Run(Stereo_Play_2Calls);
        void Stereo_Play_2Calls()
        { 
            WithStereo().Sine(RandomNotes[7]).Volume(DynamicCurve).Panbrello().Play().SpeedUp(1.5).Play();
        }
        
        [TestMethod]
        public void Stereo_Save_Test() => Run(Stereo_Save);
        void Stereo_Save()
        { 
            WithStereo().Sine(RandomNotes[8]).Volume(DynamicCurve).Panbrello().Save().Play();
        }
        
        [TestMethod]
        public void Stereo_Save_Test_2Calls() => Run(Stereo_Save_2Calls);
        void Stereo_Save_2Calls()
        { 
            WithStereo().Sine(RandomNotes[9]).Volume(DynamicCurve).Panbrello().Save().SpeedUp(1.5).Save().Play();
        }
        
        [TestMethod]
        public void Stereo_Cache_Test() => Run(Stereo_Cache);
        void Stereo_Cache()
        { 
            WithStereo().Sine(RandomNotes[10]).Volume(DynamicCurve).Panbrello().Cache().Play();
        }
        
        [TestMethod]
        public void Stereo_Cache_Test_2Calls() => Run(Stereo_Cache_2Calls);
        void Stereo_Cache_2Calls()
        { 
            WithStereo().Sine(RandomNotes[11]).Volume(DynamicCurve).Panbrello().Cache().SpeedUp(1.5).Cache().Play();
        }
        
        [TestMethod]
        public void Stereo_PlayChannel_Test() => WithStereo().Run(Stereo_PlayChannel);
        void Stereo_PlayChannel()
        { 
            Sine(B4).Volume(DynamicCurve).Panbrello().PlayChannel();
        }
        
        [TestMethod]
        public void Stereo_PlayChannel_Test_2Calls() => Run(Stereo_PlayChannel_2Calls);
        void Stereo_PlayChannel_2Calls()
        { 
            WithStereo().Sine(RandomNotes[12]).Volume(DynamicCurve).Panbrello().PlayChannel().SpeedUp(1.5).PlayChannel();
        }
        
        [TestMethod]
        public void Stereo_SaveChannel_Test() => Run(Stereo_SaveChannel);
        void Stereo_SaveChannel()
        { 
            WithStereo().Sine(RandomNotes[13]).Volume(DynamicCurve).Panbrello().SaveChannel().PlayChannel();
        }
        
        [TestMethod]
        public void Stereo_SaveChannel_Test_2Calls() => Run(Stereo_SaveChannel_2Calls);
        void Stereo_SaveChannel_2Calls()
        { 
            WithStereo().Sine(RandomNotes[14]).Volume(DynamicCurve).Panbrello().SaveChannel().SpeedUp(1.5).SaveChannel().PlayChannel();
        }
        
        [TestMethod]
        public void Stereo_CacheChannel_Test() => Run(Stereo_CacheChannel);
        void Stereo_CacheChannel()
        { 
            WithStereo().Sine(RandomNotes[15]).Volume(DynamicCurve).Panbrello().CacheChannel().PlayChannel();
        }
        
        [TestMethod]
        public void Stereo_CacheChannel_Test_2Calls() => Run(Stereo_CacheChannel_2Calls);
        void Stereo_CacheChannel_2Calls()
        { 
            WithStereo().Sine(RandomNotes[16]).Volume(DynamicCurve).Panbrello().CacheChannel().SpeedUp(1.5).CacheChannel().PlayChannel();
        }
        
        // Complex Cases
        
        [TestMethod]
        public void Stereo_MultipleActions_Test() => Run(Stereo_MultipleActions);
        void Stereo_MultipleActions() 
        {
            WithStereo();
            
            Play(() => Add
            (
                Sine(RandomNotes[17] * 1).Volume(DynamicCurve).Play(),
                Sine(RandomNotes[17] * 2).Volume(DynamicCurve).Volume(0.2),
                Sine(RandomNotes[17] * 3).Volume(DynamicCurve).Panning(0.03).Play().Volume(0.1),
                Sine(RandomNotes[17] * 4).Volume(DynamicCurve).Volume(0.08),
                Sine(RandomNotes[17] * 5).Volume(0.05).Volume(DynamicCurve).Panning(0.9).PlayChannel((b, i) => b.Save())
            ));
        }
    }
}
