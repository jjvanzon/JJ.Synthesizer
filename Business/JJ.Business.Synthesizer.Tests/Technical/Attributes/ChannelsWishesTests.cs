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

#pragma warning disable CS0611 
#pragma warning disable MSTEST0018 

namespace JJ.Business.Synthesizer.Tests.Technical.Attributes
{
    [TestClass]
    [TestCategory("Technical")]
    public class ChannelsWishesTests
    {
        [TestMethod, DataRow(1) ,DataRow(2)]
        public void Init_Channels(int init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_Channels(int init, int value)
        {            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, value);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, value);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.Channels(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.Channels(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Channels(value)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes.WithChannels(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode.WithChannels(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.WithChannels(value)));
            
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
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Channels(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,       () => x.TapeBound.Tape.Channels(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.Channels(value)));
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
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Channels(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.Channels(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Channels(value, x.SynthBound.Context)));
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Mono(x.SynthBound.Context));
                if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Stereo(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Mono(x.SynthBound.Context));
                if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Stereo(x.SynthBound.Context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_Channels(int init, int value)
        {
            // Independent after Taping

            // Sample
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish,init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Channels(value, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Mono(x.SynthBound.Context));
                    if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.Stereo(x.SynthBound.Context)); });
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, value);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Channels(value)));
                AssertProp(() => x.Independent.AudioInfoWish.Channels = value);
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Mono());
                    if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Stereo()); });
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Channels(value)));
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Mono());
                    if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Stereo()); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Channels(int init, int value)
        {
            TestEntities x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    Assert_Immutable_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }

                AssertProp(() => x.Immutable.WavHeader.Channels(value));
                AssertProp(() => value == 1 ? x.Immutable.WavHeader.Mono() : x.Immutable.WavHeader.Stereo());
            }

            // SpeakerSetupEnum
            
            var speakerSetupEnums = new List<SpeakerSetupEnum>();
            {
                void AssertProp(Func<SpeakerSetupEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, init);
                    
                    SpeakerSetupEnum speakerSetupEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, init);
                    Assert_Immutable_Getters(speakerSetupEnum2, value);
                    
                    speakerSetupEnums.Add(speakerSetupEnum2);
                }

                AssertProp(() => x.Immutable.SpeakerSetupEnum.Channels(value));
                AssertProp(() => value.ChannelsToEnum());
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetupEnum.Mono() : x.Immutable.SpeakerSetupEnum.Stereo());
            }

            // SpeakerSetup Entity
            
            var speakerSetups = new List<SpeakerSetup>();
            {
                void AssertProp(Func<SpeakerSetup> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetup, init);

                    SpeakerSetup speakerSetup2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SpeakerSetup, init);
                    Assert_Immutable_Getters(speakerSetup2, value);
                    
                    speakerSetups.Add(speakerSetup2);
                }
                
                AssertProp(() => x.Immutable.SpeakerSetup.Channels(value, x.SynthBound.Context));
                AssertProp(() => value.ChannelsToEntity(x.SynthBound.Context));
                AssertProp(() => value == 1 ? x.Immutable.SpeakerSetup.Mono(x.SynthBound.Context) : x.Immutable.SpeakerSetup.Stereo(x.SynthBound.Context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders       .ForEach(w => Assert_Immutable_Getters(w, value));
            speakerSetupEnums.ForEach(e => Assert_Immutable_Getters(e, value));
            speakerSetups    .ForEach(s => Assert_Immutable_Getters(s, value));
        }

        [TestMethod] public void ConfigSections_Channels()
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

        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { 1, 2 },
            new object[] { 2, 1 }
        };

        private void Assert_All_Getters(TestEntities x, int channels)
        {
            Assert_Bound_Getters(x, channels);
            Assert_Independent_Getters(x, channels);
            Assert_Immutable_Getters(x, channels);
        }

        private void Assert_Bound_Getters(TestEntities x, int channels)
        {
            Assert_SynthBound_Getters(x, channels);
            Assert_TapeBound_Getters(x, channels);
            Assert_BuffBound_Getters(x, channels);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int channels)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, channels);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, channels);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, channels);
        }

        private void Assert_Immutable_Getters(TestEntities x, int channels)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, channels);
            Assert_Immutable_Getters(x.Immutable.SpeakerSetupEnum, channels);
            Assert_Immutable_Getters(x.Immutable.SpeakerSetup, channels);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int channels)
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
        
        private void Assert_TapeBound_Getters(TestEntities x, int channels)
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
        
        private void Assert_BuffBound_Getters(TestEntities x, int channels)
        {
            AreEqual(channels, () => x.BuffBound.Buff.Channels());
            AreEqual(channels, () => x.BuffBound.AudioFileOutput.Channels());
            
            AreEqual(channels == 1, () => x.BuffBound.Buff.IsMono());
            AreEqual(channels == 1, () => x.BuffBound.AudioFileOutput.IsMono());
            
            AreEqual(channels == 2, () => x.BuffBound.Buff.IsStereo());
            AreEqual(channels == 2, () => x.BuffBound.AudioFileOutput.IsStereo());
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int channels)
        {
            AreEqual(channels,      () => audioFileInfo.Channels());
            AreEqual(channels == 1, () => audioFileInfo.IsMono());
            AreEqual(channels == 2, () => audioFileInfo.IsStereo());
        }
        
        private void Assert_Independent_Getters(Sample sample, int channels)
        {
            AreEqual(channels,      () => sample.Channels());
            AreEqual(channels == 1, () => sample.IsMono());
            AreEqual(channels == 2, () => sample.IsStereo());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int channels)
        {
            AreEqual(channels,      () => audioInfoWish.Channels());
            AreEqual(channels,      () => audioInfoWish.Channels);
            AreEqual(channels == 1, () => audioInfoWish.IsMono());
            AreEqual(channels == 2, () => audioInfoWish.IsStereo());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int channels)
        {
            AreEqual(channels,      () => wavHeader.Channels());
            AreEqual(channels,      () => wavHeader.ChannelCount);
            AreEqual(channels == 1, () => wavHeader.IsMono());
            AreEqual(channels == 2, () => wavHeader.IsStereo());
        }
        
        private void Assert_Immutable_Getters(SpeakerSetupEnum speakerSetupEnum, int channels)
        {
            AreEqual(channels,      () => speakerSetupEnum.Channels());
            AreEqual(channels,      () => speakerSetupEnum.EnumToChannels());
            AreEqual(channels == 1, () => speakerSetupEnum.IsMono());
            AreEqual(channels == 2, () => speakerSetupEnum.IsStereo());
        }
                
        private void Assert_Immutable_Getters(SpeakerSetup speakerSetup, int channels)
        {
            if (speakerSetup == null) throw new NullException(() => speakerSetup);
            AreEqual(channels,      () => speakerSetup.Channels());
            AreEqual(channels,      () => speakerSetup.EntityToChannels());
            AreEqual(channels == 1, () => speakerSetup.IsMono());
            AreEqual(channels == 2, () => speakerSetup.IsStereo());
        }
    } 
}