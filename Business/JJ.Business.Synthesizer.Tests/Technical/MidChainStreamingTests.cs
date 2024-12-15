using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Threading.Thread;
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
        
        double RandomVolume => RandomNumber(0.8, 1);
        
        FlowNode RandomSlope => Curve(RandomVolume, RandomVolume);

        FlowNode DownwardSlope => Curve(@"
         *   *
                 *
                     *
        *                    *");
        
        FlowNode RandomCurve => RandomSlope * DownwardSlope;
        
        FlowNode StereoDynamics => RandomCurve.Stretch(GetAudioLength)
                                              .Tremolo(
                                                  speed: RandomNumber(2, 4),
                                                  depth: RandomNumber(0.02, 0.1))
                                              .Panbrello(4, 0.9);
        
        FlowNode[] RandomNotes { get; }
        FlowNode RandomNote => GetRandomItem
        (
            C3, D3, E3, G3,
            C4, D4, E4, G4
        );
        
        public MidChainStreamingTests()
        {
            WithAudioLength(0.25);
            
            RandomNotes = new[]
            {
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote,
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote,
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote,
                RandomNote, RandomNote, RandomNote, RandomNote, RandomNote
            };
        }
        
        [TestMethod] public void Mono_Play_Test() => Run(Mono_Play);
        void Mono_Play()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).Play();
        }
        
        [TestMethod] public void Mono_Play_Test_2Calls() => Run(Mono_Play_2Calls);
        void Mono_Play_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).Play().SpeedUp(1.5).Play();
        }
        
        [TestMethod] public void Mono_Save_Test() => Run(Mono_Save);
        void Mono_Save()
        { 
            WithMono().Sine(RandomNote).Volume(StereoDynamics).Save().Play();
        }
        
        [TestMethod] public void Mono_Save_Test_2Calls() => Run(Mono_Save_2Calls);
        void Mono_Save_2Calls()
        { 
            WithMono().Sine(RandomNote).Volume(StereoDynamics).Save().SpeedUp(1.5).Save().Play();
        }
        
        [TestMethod] public void Mono_Intercept_Test() => new MidChainStreamingTests().Mono_Intercept();
        void Mono_Intercept()
        { 
            WithMono();
            
            Buff buff = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics).Intercept(x => buff = x));
            
            IsNotNull(() => buff);
            
            buff.Save().Play();
        }
        
        [TestMethod] public void Mono_Intercept_Test_2Calls() => new MidChainStreamingTests().Mono_Intercept_2Calls();
        void Mono_Intercept_2Calls()
        { 
            WithMono();
            
            Buff buff1 = default;
            Buff buff2 = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics).
                      Intercept(x => buff1 = x).SpeedUp(1.5).Intercept(x => buff2 = x));
                
            IsNotNull(() => buff1);
            IsNotNull(() => buff2);
            
            buff1.Save().Play();
            buff2.Save().Play();
        }

        [TestMethod] public void Mono_PlayChannel_Test() => Run(Mono_PlayChannel);
        void Mono_PlayChannel()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).PlayChannel();
        }
        
        [TestMethod] public void Mono_PlayChannel_Test_2Calls() => Run(Mono_PlayChannel_2Calls);
        void Mono_PlayChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).PlayChannel().SpeedUp(1.5).PlayChannel();
        }
        
        [TestMethod] public void Mono_PlayChannel_Test_3Calls() => Run(Mono_PlayChannel_3Calls);
        void Mono_PlayChannel_3Calls()
        {
            WithMono();
            
            Add
            (
                Sine(RandomNotes[3] * 1).Volume(StereoDynamics).Volume(1.0).PlayChannel(),
                Sine(RandomNotes[3] * 2).Volume(StereoDynamics).Volume(0.1),
                Sine(RandomNotes[3] * 3).Volume(StereoDynamics).Volume(0.15).PlayChannel(),
                Sine(RandomNotes[3] * 4).Volume(StereoDynamics).Volume(0.08),
                Sine(RandomNotes[3] * 5).Volume(StereoDynamics).Volume(0.05).PlayChannel()
            ).Play();
        }
        
        [TestMethod] public void Mono_SaveChannel_Test() => Run(Mono_SaveChannel);
        void Mono_SaveChannel()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).SaveChannel().Play();
        }
        
        [TestMethod] 
        public void Mono_SaveChannel_Test_2Calls() => Run(Mono_SaveChannel_2Calls);
        void Mono_SaveChannel_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).SaveChannel().SpeedUp(1.5).SaveChannel().Play();
        }

        [TestMethod] public void Mono_SaveChannel_Test_3Calls() => Run(Mono_SaveChannel_3Calls);
        void Mono_SaveChannel_3Calls()
        {
            WithMono();
            
            Add
            (
                Sine(RandomNotes[4] * 1).Volume(StereoDynamics).Volume(1.0).SaveChannel(MemberName() + " Partial 1"),
                Sine(RandomNotes[4] * 2).Volume(StereoDynamics).Volume(0.1),
                Sine(RandomNotes[4] * 3).Volume(StereoDynamics).SaveChannel(ResolveName() + " Partial 2").Volume(0.05),
                Sine(RandomNotes[4] * 4).Volume(StereoDynamics).Volume(0.01),
                Sine(RandomNotes[4] * 5).Volume(StereoDynamics).Volume(0.02).SetName(MemberName() + " Partial 3").SaveChannel()
            ).Play();
        }
        
        [TestMethod] public void Mono_InterceptChannel_Test() => new MidChainStreamingTests().Mono_InterceptChannel();
        void Mono_InterceptChannel()
        {
            WithMono();
            
            Buff buff = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics).InterceptChannel((b, i) => buff = b));
            
            IsNotNull(() => buff);
            
            buff.Play();
        }
        
        [TestMethod] public void Mono_InterceptChannel_Test_2Calls() => new MidChainStreamingTests().Mono_InterceptChannel_2Calls();
        void Mono_InterceptChannel_2Calls()
        {
            WithMono();
            
            Buff buff1 = default;
            Buff buff2 = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics).
                      InterceptChannel((b, i) => buff1 = b).
                      SpeedUp(1.5).InterceptChannel((b, i) => buff2 = b));
                
            IsNotNull(() => buff1);
            IsNotNull(() => buff2);
                
            buff1.Play();
            buff2.Play();
        }

        [TestMethod] public void Mono_InterceptChannel_Test_3Calls() => new MidChainStreamingTests().Mono_InterceptChannel_3Calls();
        void Mono_InterceptChannel_3Calls()
        {
            WithMono();
            
            Buff buff1 = default;
            Buff buff2 = default;
            Buff buff3 = default;

            Run(() => Add
            (
                Sine(RandomNotes[5] * 1).Volume(StereoDynamics).Volume(1.0).InterceptChannel((b, i) => buff1 = b),
                Sine(RandomNotes[5] * 2).Volume(StereoDynamics).Volume(0.05),
                Sine(RandomNotes[5] * 3).Volume(StereoDynamics).InterceptChannel((b, i) => buff2 = b).Volume(0.02),
                Sine(RandomNotes[5] * 4).Volume(StereoDynamics).Volume(0.03),
                Sine(RandomNotes[5] * 5).Volume(StereoDynamics).Volume(0.01).InterceptChannel((b, i) => buff3 = b)
            ));
            
            IsNotNull(() => buff1);
            IsNotNull(() => buff2);
            IsNotNull(() => buff3);
        }
        
        [TestMethod] public void Stereo_Play_Test() => WithStereo().Run(Stereo_Play);
        void Stereo_Play()
        {
            Sine(RandomNotes[6]).Curve(StereoDynamics).Play();
        }
        
        [TestMethod] public void Stereo_Play_Test_2Calls() => Run(Stereo_Play_2Calls);
        void Stereo_Play_2Calls()
        { 
            WithStereo().Sine(RandomNotes[7]).Volume(StereoDynamics).Play("Play1").SpeedUp(1.5).Play("Play2");
        }
        
        [TestMethod] public void Stereo_Save_Test() => Run(Stereo_Save);
        void Stereo_Save()
        { 
            WithStereo().Sine(RandomNotes[8]).Volume(StereoDynamics).Save().Play();
        }
        
        [TestMethod] public void Stereo_Save_Test_2Calls() => Run(Stereo_Save_2Calls);
        void Stereo_Save_2Calls()
        { 
            WithStereo().Sine(RandomNotes[9]).Volume(StereoDynamics).Save().SpeedUp(1.5).Save().Play();
        }
        
        [TestMethod] public void Stereo_Intercept_Test() => new MidChainStreamingTests().Stereo_Intercept();
        void Stereo_Intercept()
        { 
            WithStereo();
                
            Buff buff = default;
            
            Run(() => Sine(RandomNotes[10]).Volume(StereoDynamics).Intercept(x => buff = x));
            
            IsNotNull(() => buff);
            
            buff.Save().Play();
        }
        
        [TestMethod] public void Stereo_Intercept_Test_2Calls() => new MidChainStreamingTests().Stereo_Intercept_2Calls();
        void Stereo_Intercept_2Calls()
        { 
            WithStereo();
                
            Buff buff1 = default;
            Buff buff2 = default;
            
            Run(() => Sine(RandomNotes[11]).Volume(StereoDynamics).
                      Intercept(x => buff1 = x).
                      SpeedUp(1.5).Intercept(x => buff2 = x));
            
            IsNotNull(() => buff1);
            IsNotNull(() => buff2);
                
            buff1.Save().Play();
            buff2.Save().Play();
        }
        
        [TestMethod] public void Stereo_PlayChannel_Test() => WithStereo().Run(Stereo_PlayChannel);
        void Stereo_PlayChannel()
        { 
            Sine(RandomNotes[12]).Volume(StereoDynamics).PlayChannel();
        }
        
        [TestMethod] public void Stereo_PlayChannel_Test_2Calls() => Run(Stereo_PlayChannel_2Calls);
        void Stereo_PlayChannel_2Calls()
        { 
            WithStereo().Sine(RandomNotes[12]).Volume(StereoDynamics).PlayChannel().SpeedUp(1.5).PlayChannel();
        }
        
        [TestMethod] public void Stereo_SaveChannel_Test() => Run(Stereo_SaveChannel);
        void Stereo_SaveChannel()
        { 
            WithStereo().Sine(RandomNotes[13]).Volume(StereoDynamics).SaveChannel().PlayChannel();
        }
        
        [TestMethod] public void Stereo_SaveChannel_Test_2Calls() => Run(Stereo_SaveChannel_2Calls);
        void Stereo_SaveChannel_2Calls()
        { 
            WithStereo().Sine(RandomNotes[14]).Volume(StereoDynamics).SaveChannel().SpeedUp(1.5).SaveChannel().PlayChannel();
        }
        
        [TestMethod] public void Stereo_InterceptChannel_Test() => new MidChainStreamingTests().Stereo_InterceptChannel();
        void Stereo_InterceptChannel()
        { 
            WithStereo();
            WithAudioLength(0.5);
            
            var buffs = new Buff[2];
            
            Run(() => Sine(RandomNotes[15]).Volume(StereoDynamics).InterceptChannel((b,i) => buffs[i] = b));
            
            IsNotNull(() => buffs[0]);
            IsNotNull(() => buffs[1]);
            
            buffs[0].Play(); Sleep(1000);
            buffs[1].Play(); Sleep(1000);
            
            Run(() => (Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Save().Play());
        }
        
        [TestMethod] public void Stereo_InterceptChannel_Test_2Calls() => new MidChainStreamingTests().Stereo_InterceptChannel_2Calls();
        void Stereo_InterceptChannel_2Calls()
        {
            WithStereo();
            WithAudioLength(0.5);
            
            var buffs1 = new Buff[2];
            var buffs2 = new Buff[2];
            
            Run(() => Sine(RandomNotes[16]).Volume(StereoDynamics).
                      InterceptChannel((b,i) => buffs1[i] = b).SpeedUp(1.5).
                      InterceptChannel((b,i) => buffs2[i] = b));
            
            IsNotNull(() => buffs1[0]);
            IsNotNull(() => buffs1[1]);
            IsNotNull(() => buffs2[0]);
            IsNotNull(() => buffs2[1]);
        
            buffs1[0].Play(); Sleep(1000);
            buffs1[1].Play(); Sleep(1000);
            buffs2[0].Play(); Sleep(1000);
            buffs2[1].Play(); Sleep(1000);

            Run(() => (Sample(buffs1[0]).Panning(0) +
                       Sample(buffs1[1]).Panning(1)).Save().Play());
            
            Run(() => (Sample(buffs2[0]).Panning(0) +
                       Sample(buffs2[1]).Panning(1)).Save().Play());
        }
        
        // Complex Cases
        
        [TestMethod] public void Stereo_MultipleActions_Test() => new MidChainStreamingTests().Stereo_MultipleActions();
        void Stereo_MultipleActions() 
        {
            WithStereo();
            
            Add
            (
                Sine(RandomNotes[17] * 1).Volume(StereoDynamics).Play("Play1"),
                Sine(RandomNotes[17] * 2).Volume(StereoDynamics).Volume(0.2),
                Sine(RandomNotes[17] * 3).Volume(StereoDynamics).Panning(0.03).Play("Play2").Volume(0.1),
                Sine(RandomNotes[17] * 4).Volume(StereoDynamics).Volume(0.08),
                Sine(RandomNotes[17] * 5).Volume(0.05).Volume(StereoDynamics).Panning(0.9).InterceptChannel((b, i) => b.Play().Save())
            ).Play();
        }

        // Problems
        
        [TestMethod]
        public void Stereo_RecombineChannelsExplicit_Test() => new MidChainStreamingTests().Stereo_RecombineChannelsExplicit();
        void Stereo_RecombineChannelsExplicit() 
        {
            WithStereo();
            WithAudioLength(0.5);
            
            var buffs = new Buff[2];
            
            // The delegate creates a non-trivial convergence point.
            
            Save(() => Sine(RandomNotes[1]).Panning(0.1).Volume(StereoDynamics).InterceptChannel((b, i) => buffs[i] = b)).Play();
            
            IsNotNull(() => buffs[0]);
            IsNotNull(() => buffs[1]);
            
            buffs[0].Save().Play(); Sleep(1000);
            buffs[1].Save().Play();
            
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
            
            Save(() => Sine(RandomNotes[2]).Panning(0.9).Volume(StereoDynamics).InterceptChannel((b, i) => buffs[i] = b)).Play();
            
            Save(() => Sample(buffs[0]).Panning(0) +
                       Sample(buffs[1]).Panning(1)).Play();
        }
    }
}
