using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Threading.Thread;
using static JJ.Framework.Wishes.Mathematics.RandomizerWishes;
using static JJ.Framework.Wishes.Mathematics.Randomizer_Copied;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.NameWishes;
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
        
        FlowNode DelayedPulse => DelayedPulseCurve.Stretch(GetAudioLength);
        
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
            WithAudioLength(0.7);
            
            Tape tape = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics * DelayedPulse).AfterRecord(x => tape = x));
            
            IsNotNull(() => tape);
            
            tape.Save().Play();
        }
        
        [TestMethod] public void Mono_Intercept_Test_2Calls() => new MidChainStreamingTests().Mono_Intercept_2Calls();
        void Mono_Intercept_2Calls()
        { 
            WithMono();
            WithAudioLength(0.7);

            Tape tape1 = default;
            Tape tape2 = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics * DelayedPulse).
                      AfterRecord(x => tape1 = x).SpeedUp(1.5).AfterRecord(x => tape2 = x));
            
            IsNotNull(() => tape1);
            IsNotNull(() => tape2);
            
            tape1.Save().Play();
            tape2.Save().Play();
        }

        [TestMethod] public void Mono_PlayChannels_Test() => Run(Mono_PlayChannels);
        void Mono_PlayChannels()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).PlayChannels();
        }
        
        [TestMethod] public void Mono_PlayChannels_Test_2Calls() => Run(Mono_PlayChannels_2Calls);
        void Mono_PlayChannels_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).PlayChannels().SpeedUp(1.5).PlayChannels();
        }
        
        [TestMethod] public void Mono_PlayChannels_Test_3Calls() => Run(Mono_PlayChannels_3Calls);
        void Mono_PlayChannels_3Calls()
        {
            WithMono();
            
            Add
            (
                Sine(RandomNotes[3] * 1).Volume(StereoDynamics).Volume(1.0).PlayChannels(),
                Sine(RandomNotes[3] * 2).Volume(StereoDynamics).Volume(0.1),
                Sine(RandomNotes[3] * 3).Volume(StereoDynamics).Volume(0.15).PlayChannels(),
                Sine(RandomNotes[3] * 4).Volume(StereoDynamics).Volume(0.08),
                Sine(RandomNotes[3] * 5).Volume(StereoDynamics).Volume(0.05).PlayChannels()
            ).Play();
        }
        
        [TestMethod] public void Mono_SaveChannels_Test() => Run(Mono_SaveChannels);
        void Mono_SaveChannels()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).SaveChannels().Play();
        }
        
        [TestMethod] 
        public void Mono_SaveChannels_Test_2Calls() => Run(Mono_SaveChannels_2Calls);
        void Mono_SaveChannels_2Calls()
        {
            WithMono().Sine(RandomNote).Volume(StereoDynamics).SaveChannels().SpeedUp(1.5).SaveChannels().Play();
        }

        [TestMethod] public void Mono_SaveChannels_Test_3Calls() => Run(Mono_SaveChannels_3Calls);
        void Mono_SaveChannels_3Calls()
        {
            WithMono();
            
            Add
            (
                Sine(RandomNotes[4] * 1).Volume(StereoDynamics).Volume(1.0).SaveChannels(MemberName() + " Partial 1"),
                Sine(RandomNotes[4] * 2).Volume(StereoDynamics).Volume(0.1),
                Sine(RandomNotes[4] * 3).Volume(StereoDynamics).SaveChannels(ResolveName() + " Partial 2").Volume(0.05),
                Sine(RandomNotes[4] * 4).Volume(StereoDynamics).Volume(0.01),
                Sine(RandomNotes[4] * 5).Volume(StereoDynamics).Volume(0.02).SetName(MemberName() + " Partial 3").SaveChannels()
            ).Play();
        }
        
        [TestMethod] public void Mono_InterceptChannel_Test() => new MidChainStreamingTests().Mono_InterceptChannel();
        void Mono_InterceptChannel()
        {
            WithMono();
            WithAudioLength(0.7);
            
            Tape tape = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics * DelayedPulse).AfterRecordChannel(x => tape = x));
            
            IsNotNull(() => tape);
            
            tape.Play();
        }
        
        [TestMethod] public void Mono_InterceptChannel_Test_2Calls() => new MidChainStreamingTests().Mono_InterceptChannel_2Calls();
        void Mono_InterceptChannel_2Calls()
        {
            WithMono();
            WithAudioLength(0.7);
            
            Tape tape1 = default;
            Tape tape2 = default;
            
            Run(() => Sine(RandomNote).Volume(StereoDynamics * DelayedPulse).
                      AfterRecordChannel(x => tape1 = x).
                      SpeedUp(1.5).AfterRecordChannel(x => tape2 = x));
                
            IsNotNull(() => tape1);
            IsNotNull(() => tape2);
                
            tape1.Play();
            tape2.Play();
        }

        [TestMethod] public void Mono_InterceptChannel_Test_3Calls() => new MidChainStreamingTests().Mono_InterceptChannel_3Calls();
        void Mono_InterceptChannel_3Calls()
        {
            WithMono();
            WithAudioLength(0.7);
            
            Tape tape1 = default;
            Tape tape2 = default;
            Tape tape3 = default;

            Run(() => Add
            (
                Sine(RandomNotes[5] * 1).Volume(StereoDynamics * DelayedPulse).Volume(1.0).AfterRecordChannel(x => tape1 = x),
                Sine(RandomNotes[5] * 2).Volume(StereoDynamics * DelayedPulse).Volume(0.05),
                Sine(RandomNotes[5] * 3).Volume(StereoDynamics * DelayedPulse).AfterRecordChannel(x => tape2 = x).Volume(0.02),
                Sine(RandomNotes[5] * 4).Volume(StereoDynamics * DelayedPulse).Volume(0.03),
                Sine(RandomNotes[5] * 5).Volume(StereoDynamics * DelayedPulse).Volume(0.01).AfterRecordChannel(x => tape3 = x)
            ));
            
            IsNotNull(() => tape1);
            IsNotNull(() => tape2);
            IsNotNull(() => tape3);
        
            tape1.Play();
            tape2.Play();
            tape3.Play();
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
            WithAudioLength(0.7);
                
            Tape tape = default;
            
            Run(() => Sine(RandomNotes[10]).Volume(StereoDynamics * DelayedPulse).AfterRecord(x => tape = x));
            
            IsNotNull(() => tape);
            
            tape.Save().Play();
        }
        
        [TestMethod] public void Stereo_Intercept_Test_2Calls() => new MidChainStreamingTests().Stereo_Intercept_2Calls();
        void Stereo_Intercept_2Calls()
        { 
            WithStereo();
            WithAudioLength(0.7);
                
            Tape tape1 = default;
            Tape tape2 = default;
            
            Run(() => Sine(RandomNotes[11]).Volume(StereoDynamics * DelayedPulse).
                      AfterRecord(x => tape1 = x).
                      SpeedUp(1.5).AfterRecord(x => tape2 = x));
            
            IsNotNull(() => tape1);
            IsNotNull(() => tape2);
                
            tape1.Save().Play();
            tape2.Save().Play();
        }
        
        [TestMethod] public void Stereo_PlayChannels_Test() => WithStereo().Run(Stereo_PlayChannels);
        void Stereo_PlayChannels()
        { 
            Sine(RandomNotes[12]).Volume(StereoDynamics).PlayChannels();
        }
        
        [TestMethod] public void Stereo_PlayChannels_Test_2Calls() => Run(Stereo_PlayChannels_2Calls);
        void Stereo_PlayChannels_2Calls()
        { 
            WithStereo().Sine(RandomNotes[12]).Volume(StereoDynamics).PlayChannels().SpeedUp(1.5).PlayChannels();
        }
        
        [TestMethod] public void Stereo_SaveChannels_Test() => Run(Stereo_SaveChannels);
        void Stereo_SaveChannels()
        { 
            WithStereo().Sine(RandomNotes[13]).Volume(StereoDynamics).SaveChannels().PlayChannels();
        }
        
        [TestMethod] public void Stereo_SaveChannels_Test_2Calls() => Run(Stereo_SaveChannels_2Calls);
        void Stereo_SaveChannels_2Calls()
        { 
            WithStereo().Sine(RandomNotes[14]).Volume(StereoDynamics).SaveChannels().SpeedUp(1.5).SaveChannels().PlayChannels();
        }
        
        [TestMethod] public void Stereo_InterceptChannel_Test() => new MidChainStreamingTests().Stereo_InterceptChannel();
        void Stereo_InterceptChannel()
        { 
            WithStereo();
            WithAudioLength(0.7);
            
            var tapes = new Tape[2];
            
            Run(() => Sine(RandomNotes[15]).Volume(StereoDynamics * DelayedPulse).AfterRecordChannel(x => tapes[x.i] = x));
            
            IsNotNull(() => tapes[0]);
            IsNotNull(() => tapes[1]);
            
            tapes[0].Save().Play(); Sleep(1000);
            tapes[1].Save().Play(); Sleep(1000);
            
            Run(() => (Sample(tapes[0]).Panning(0) +
                       Sample(tapes[1]).Panning(1)).Save().Play());
        }
        
        [TestMethod] public void Stereo_InterceptChannel_Test_2Calls() => new MidChainStreamingTests().Stereo_InterceptChannel_2Calls();
        void Stereo_InterceptChannel_2Calls()
        {
            WithStereo();
            WithAudioLength(0.7);
            
            var tapes1 = new Tape[2];
            var tapes2 = new Tape[2];
            
            Run(() => Sine(RandomNotes[16]).Volume(StereoDynamics * DelayedPulse).
                      AfterRecordChannel(x => tapes1[x.i] = x).SpeedUp(1.5).
                      AfterRecordChannel(x => tapes2[x.i] = x));
            
            IsNotNull(() => tapes1[0]);
            IsNotNull(() => tapes1[1]);
            IsNotNull(() => tapes2[0]);
            IsNotNull(() => tapes2[1]);
        
            tapes1[0].Save().Play(); Sleep(1000);
            tapes1[1].Save().Play(); Sleep(1000);
            tapes2[0].Save().Play(); Sleep(1000);
            tapes2[1].Save().Play(); Sleep(1000);

            Run(() => (Sample(tapes1[0]).Panning(0) +
                       Sample(tapes1[1]).Panning(1)).Save().Play());
            
            Run(() => (Sample(tapes2[0]).Panning(0) +
                       Sample(tapes2[1]).Panning(1)).Save().Play());
        }
        
        // Complex Cases
        
        [TestMethod] public void Stereo_MultipleActions_Test() => Run(Stereo_MultipleActions);
        void Stereo_MultipleActions() 
        {
            WithStereo();
            
            Add
            (
                Sine(RandomNotes[17] * 1).Volume(StereoDynamics * DelayedPulse).Play("Play1"),
                Sine(RandomNotes[17] * 2).Volume(StereoDynamics * DelayedPulse).Volume(0.2),
                Sine(RandomNotes[17] * 3).Volume(StereoDynamics * DelayedPulse).Panning(0.03).Play("Play2").Volume(0.1),
                Sine(RandomNotes[17] * 4).Volume(StereoDynamics * DelayedPulse).Volume(0.08),
                Sine(RandomNotes[17] * 5).Volume(0.05).Volume(StereoDynamics * DelayedPulse).Panning(0.9).AfterRecordChannel(x => x.Play().Save())
            ).Play();
        }
    }
}
