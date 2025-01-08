using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class ChannelsWishesTests
    {
        [TestMethod] public void Test_Channels_InTandem()
        {
            Test_Channels_InTandem(1, 2);
            Test_Channels_InTandem(2, 1);
        }
        
        void Test_Channels_InTandem(int init, int value)
        {
            // Check Before Change
            { 
                var x = CreateTestEntities(init);
                Assert_All_Channels_Getters(x, init);
            }

            // Synth-Bound Changes
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.Channels(value)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.Channels(value)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Channels(value)));
                
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,        x.SynthBound.SynthWishes.WithChannels(value)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,           x.SynthBound.FlowNode.WithChannels(value)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigWishes,       x.SynthBound.ConfigWishes.WithChannels(value)));
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Mono());
                    if (value == 2) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.Stereo()); });

                AssertProp(x => {
                    if (value == 1) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithMono());
                    if (value == 2) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.WithStereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Mono());
                    if (value == 2) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.Stereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithMono());
                    if (value == 2) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.WithStereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Mono());
                    if (value == 2) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Stereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.WithMono());
                    if (value == 2) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.WithStereo()); });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = CreateTestEntities(init);
                    Assert_All_Channels_Getters(x, init);
                    
                    setter(x);
                    
                    Assert_SynthBound_Channels_Getters(x, value);
                    Assert_TapeBound_Channels_Getters(x, init);
                    Assert_BuffBound_Channels_Getters(x, init);
                    Assert_Independent_Channels_Getters(x, init);
                    Assert_Immutable_Channels_Getters(x, init);
                    
                    x.Record();
                    
                    Assert_All_Channels_Getters(x, value);
                }
            }

            // Tape-Bound Changes
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.Channels(value)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.Channels(value)));
                AssertProp(x =>                               x.TapeBound.TapeConfig.Channels = value);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Channels(value)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.Channels(value)));
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Mono());
                    if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.Stereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Mono());
                    if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Stereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Mono());
                    if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Stereo()); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Mono());
                    if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.Stereo()); });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = CreateTestEntities(init);
                    Assert_All_Channels_Getters(x, init);
                    
                    setter(x);
                    
                    Assert_SynthBound_Channels_Getters(x, init);
                    Assert_TapeBound_Channels_Getters(x, value);
                    Assert_BuffBound_Channels_Getters(x, init);
                    Assert_Independent_Channels_Getters(x, init);
                    Assert_Immutable_Channels_Getters(x, init);
                    
                    x.Record();
                    
                    Assert_All_Channels_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.Channels(value, x.SynthBound.Context)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels(value, x.SynthBound.Context)));
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Mono(x.SynthBound.Context));
                    if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Stereo(x.SynthBound.Context)); });
                
                AssertProp(x => {
                    if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Mono(x.SynthBound.Context));
                    if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Stereo(x.SynthBound.Context)); });

                void AssertProp(Action<TestEntities> setter)
                {    
                    var x = CreateTestEntities(init);
                    Assert_All_Channels_Getters(x, init);
                    
                    setter(x);

                    Assert_SynthBound_Channels_Getters(x, init);
                    Assert_TapeBound_Channels_Getters(x, init);
                    Assert_BuffBound_Channels_Getters(x, value);
                    Assert_Independent_Channels_Getters(x, init);
                    Assert_Immutable_Channels_Getters(x, init);
                    
                    x.Record();
                    Assert_All_Channels_Getters(x, init);
                }
            }
        }
        
        // Channels for Independently Changeable
        
        [TestMethod] public void Test_Channels_IndependentAfterTaping()
        {
            Test_Channels_IndependentAfterTaping(init: 1, value: 2);
            Test_Channels_IndependentAfterTaping(init: 2, value: 1);
        }
        
        void Test_Channels_IndependentAfterTaping(int init, int value)
        {
            // Independent after Taping
            var x = CreateTestEntities(init);

            // Sample
            {
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Channels(value, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Mono(x.SynthBound.Context));
                    if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Stereo(x.SynthBound.Context)); });
                
                void AssertProp(Action setter)
                {
                    Initialize(x, init);
                    Assert_All_Channels_Getters(x, init);
                    
                    setter();
                    
                    Assert_Channels_Getters(x.Independent.Sample, value);
                    
                    Assert_Channels_Getters(x.Independent.AudioInfoWish,init);
                    Assert_Channels_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Channels_Getters(x, init);
                    Assert_Bound_Channels_Getters(x, init);

                    x.Record();
                    Assert_All_Channels_Getters(x, init);
                }
            }
            
            // AudioInfoWish
            {
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Channels(value)));
                AssertProp(() => x.Independent.AudioInfoWish.Channels = value);
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Mono());
                    if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Stereo()); });
                
                void AssertProp(Action setter)
                {
                    Initialize(x, init);
                    Assert_All_Channels_Getters(x, init);
                    
                    setter();
                    
                    Assert_Channels_Getters(x.Independent.AudioInfoWish, value);
                    
                    Assert_Channels_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Channels_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Channels_Getters(x, init);
                    Assert_Bound_Channels_Getters(x, init);

                    x.Record();
                    Assert_All_Channels_Getters(x, init);
                }
            }
                        
            // AudioFileInfo
            {
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Channels(value)));
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Mono());
                    if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Stereo()); });
                
                void AssertProp(Action setter)
                {
                    Initialize(x, init);
                    Assert_All_Channels_Getters(x, init);
                    
                    setter();
                    
                    Assert_Channels_Getters(x.Independent.AudioFileInfo, value);
                    
                    Assert_Channels_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Channels_Getters(x.Independent.Sample, init);
                    Assert_Bound_Channels_Getters(x, init);
                    Assert_Immutable_Channels_Getters(x, init);

                    x.Record();
                    Assert_All_Channels_Getters(x, init);
                }
            }
        }
        
        // Channels for Immutables

        [TestMethod] public void Test_Channels_Immutable()
        {
            Test_Channels_Immutable(init: 1, value: 2);
            Test_Channels_Immutable(init: 2, value: 1);
        }
        
        void Test_Channels_Immutable(int init, int value)
        {
            var x = CreateTestEntities(init);

            var wavHeaders = new List<WavHeaderStruct>();
            {
                AssertProp(() => x.Immutable.WavHeader.Channels(value));
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.Mono() : x.Immutable.WavHeader.Stereo());
                
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Channels_Getters(x.Immutable.WavHeader, init);
                    
                    var wavHeader2 = setter();
                    
                    Assert_Channels_Getters(x.Immutable.WavHeader, init);
                    Assert_Channels_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }
            }
            
            var speakerSetupEnums = new List<SpeakerSetupEnum>();
            {
                AssertProp(() => x.Immutable.SpeakerSetupEnum.Channels(value));
                AssertProp(() => value.ChannelsToEnum());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono() : x.Immutable.SpeakerSetupEnum.Stereo());
                
                void AssertProp(Func<SpeakerSetupEnum> setter)
                {
                    Assert_Channels_Getters(x.Immutable.SpeakerSetupEnum, init);
                    
                    var speakerSetupEnum2 = setter();
                    
                    Assert_Channels_Getters(x.Immutable.SpeakerSetupEnum, init);
                    Assert_Channels_Getters(speakerSetupEnum2, value);
                    
                    speakerSetupEnums.Add(speakerSetupEnum2);
                }
            }
                        
            var speakerSetups = new List<SpeakerSetup>();
            {
                AssertProp(() => x.Immutable.SpeakerSetup.Channels(value, x.SynthBound.Context));
                AssertProp(() => value.ChannelsToEntity(x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.Mono(x.SynthBound.Context) : x.Immutable.SpeakerSetup.Stereo(x.SynthBound.Context));

                void AssertProp(Func<SpeakerSetup> setter)
                {
                    Assert_Channels_Getters(x.Immutable.SpeakerSetup, init);

                    var speakerSetup2 = setter();
                    
                    Assert_Channels_Getters(x.Immutable.SpeakerSetup, init);
                    Assert_Channels_Getters(speakerSetup2, value);
                    
                    speakerSetups.Add(speakerSetup2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Channels_Getters(x, init);
            
            // Except for our variables
            wavHeaders       .ForEach(w => Assert_Channels_Getters(w, value));
            speakerSetupEnums.ForEach(e => Assert_Channels_Getters(e, value));
            speakerSetups    .ForEach(s => Assert_Channels_Getters(s, value));
        }

        // Channels in ConfigSection
        
        [TestMethod] public void Test_Channels_ConfigSection()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = TestEntities.GetConfigSectionAccessor();
            
            AreEqual(DefaultChannels,      () => configSection.Channels);
            AreEqual(DefaultChannels,      () => configSection.Channels());
            AreEqual(DefaultChannels == 1, () => configSection.IsMono());
            AreEqual(DefaultChannels == 2, () => configSection.IsStereo());
        }

        // Helpers
        
        private TestEntities CreateTestEntities(int channels) => new TestEntities(x => x.WithChannels(channels));
        
        private void Initialize(TestEntities x, int channels) => x.Initialize(s => s.WithChannels(channels));

        private void Assert_All_Channels_Getters(TestEntities x, int channels)
        {
            Assert_SynthBound_Channels_Getters(x, channels);
            Assert_TapeBound_Channels_Getters(x, channels);
            Assert_BuffBound_Channels_Getters(x, channels);
            Assert_Independent_Channels_Getters(x, channels);
            Assert_Immutable_Channels_Getters(x, channels);
        }

        private void Assert_Bound_Channels_Getters(TestEntities x, int channels)
        {
            Assert_SynthBound_Channels_Getters(x, channels);
            Assert_TapeBound_Channels_Getters(x, channels);
            Assert_BuffBound_Channels_Getters(x, channels);
        }

        private void Assert_SynthBound_Channels_Getters(TestEntities x, int channels)
        {
            AreEqual(channels, () => x.SynthBound.SynthWishes.Channels());
            AreEqual(channels, () => x.SynthBound.SynthWishes.GetChannels);
            AreEqual(channels, () => x.SynthBound.FlowNode.Channels());
            AreEqual(channels, () => x.SynthBound.FlowNode.GetChannels);
            AreEqual(channels, () => x.SynthBound.ConfigWishes.Channels());
            AreEqual(channels, () => x.SynthBound.ConfigWishes.GetChannels);
            
            AreEqual(channels == 1, () => x.SynthBound.SynthWishes.IsMono());
            AreEqual(channels == 1, () => x.SynthBound.SynthWishes.IsMono);
            AreEqual(channels == 1, () => x.SynthBound.FlowNode.IsMono());
            AreEqual(channels == 1, () => x.SynthBound.FlowNode.IsMono);
            AreEqual(channels == 1, () => x.SynthBound.ConfigWishes.IsMono());
            AreEqual(channels == 1, () => x.SynthBound.ConfigWishes.IsMono);
            
            AreEqual(channels == 2, () => x.SynthBound.SynthWishes.IsStereo());
            AreEqual(channels == 2, () => x.SynthBound.SynthWishes.IsStereo);
            AreEqual(channels == 2, () => x.SynthBound.FlowNode.IsStereo());
            AreEqual(channels == 2, () => x.SynthBound.FlowNode.IsStereo);
            AreEqual(channels == 2, () => x.SynthBound.ConfigWishes.IsStereo());
            AreEqual(channels == 2, () => x.SynthBound.ConfigWishes.IsStereo);
        }
        
        private void Assert_TapeBound_Channels_Getters(TestEntities x, int channels)
        {
            AreEqual(channels, () => x.TapeBound.Tape.Channels());
            AreEqual(channels, () => x.TapeBound.TapeConfig.Channels());
            AreEqual(channels, () => x.TapeBound.TapeConfig.Channels);
            AreEqual(channels, () => x.TapeBound.TapeActions.Channels());
            AreEqual(channels, () => x.TapeBound.TapeAction.Channels());
            
            AreEqual(channels == 1, () => x.TapeBound.Tape.IsMono());
            AreEqual(channels == 1, () => x.TapeBound.TapeConfig.IsMono());
            AreEqual(channels == 1, () => x.TapeBound.TapeActions.IsMono());
            AreEqual(channels == 1, () => x.TapeBound.TapeAction.IsMono());
        
            AreEqual(channels == 2, () => x.TapeBound.Tape.IsStereo());
            AreEqual(channels == 2, () => x.TapeBound.TapeConfig.IsStereo());
            AreEqual(channels == 2, () => x.TapeBound.TapeActions.IsStereo());
            AreEqual(channels == 2, () => x.TapeBound.TapeAction.IsStereo());
        }
        
        private void Assert_BuffBound_Channels_Getters(TestEntities x, int channels)
        {
            AreEqual(channels, () => x.BuffBound.Buff.Channels());
            AreEqual(channels, () => x.BuffBound.AudioFileOutput.Channels());
            
            AreEqual(channels == 1, () => x.BuffBound.Buff.IsMono());
            AreEqual(channels == 1, () => x.BuffBound.AudioFileOutput.IsMono());
            
            AreEqual(channels == 2, () => x.BuffBound.Buff.IsStereo());
            AreEqual(channels == 2, () => x.BuffBound.AudioFileOutput.IsStereo());
        }

        private void Assert_Independent_Channels_Getters(TestEntities x, int channels)
        {
            // Independent after Taping
            Assert_Channels_Getters(x.Independent.Sample, channels);
            Assert_Channels_Getters(x.Independent.AudioInfoWish, channels);
            Assert_Channels_Getters(x.Independent.AudioFileInfo, channels);
        }

        private void Assert_Immutable_Channels_Getters(TestEntities x, int channels)
        {
            Assert_Channels_Getters(x.Immutable.WavHeader, channels);
            Assert_Channels_Getters(x.Immutable.SpeakerSetupEnum, channels);
            Assert_Channels_Getters(x.Immutable.SpeakerSetup, channels);
        }

        private void Assert_Channels_Getters(AudioFileInfo audioFileInfo, int channels)
        {
            AreEqual(channels,      () => audioFileInfo.Channels());
            AreEqual(channels == 1, () => audioFileInfo.IsMono());
            AreEqual(channels == 2, () => audioFileInfo.IsStereo());
        }
        
        private void Assert_Channels_Getters(Sample sample, int channels)
        {
            AreEqual(channels,      () => sample.Channels());
            AreEqual(channels == 1, () => sample.IsMono());
            AreEqual(channels == 2, () => sample.IsStereo());
        }
        
        private void Assert_Channels_Getters(AudioInfoWish audioInfoWish, int channels)
        {
            AreEqual(channels,      () => audioInfoWish.Channels());
            AreEqual(channels,      () => audioInfoWish.Channels);
            AreEqual(channels == 1, () => audioInfoWish.IsMono());
            AreEqual(channels == 2, () => audioInfoWish.IsStereo());
        }

        private void Assert_Channels_Getters(WavHeaderStruct wavHeader, int channels)
        {
            AreEqual(channels,      () => wavHeader.Channels());
            AreEqual(channels,      () => wavHeader.ChannelCount);
            AreEqual(channels == 1, () => wavHeader.IsMono());
            AreEqual(channels == 2, () => wavHeader.IsStereo());
        }
        
        private void Assert_Channels_Getters(SpeakerSetupEnum speakerSetupEnum, int channels)
        {
            AreEqual(channels,      () => speakerSetupEnum.Channels());
            AreEqual(channels,      () => speakerSetupEnum.EnumToChannels());
            AreEqual(channels == 1, () => speakerSetupEnum.IsMono());
            AreEqual(channels == 2, () => speakerSetupEnum.IsStereo());
        }
                
        private void Assert_Channels_Getters(SpeakerSetup speakerSetup, int channels)
        {
            if (speakerSetup == null) throw new NullException(() => speakerSetup);
            AreEqual(channels,      () => speakerSetup.Channels());
            AreEqual(channels,      () => speakerSetup.EntityToChannels());
            AreEqual(channels == 1, () => speakerSetup.IsMono());
            AreEqual(channels == 2, () => speakerSetup.IsStereo());
        }
    } 
}