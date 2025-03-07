using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611 

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class FrameSizeWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_FrameSize(string descriptor, int frameSize, int? bits, int? channels)
        { 
            var init = (frameSize, bits, channels);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, frameSize);
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_FrameSize(string descriptor, int initFrameSize, int? initBits, int? initChannels, int frameSize, int? bits, int? channels)
        {            
            var init = (frameSize: initFrameSize, bits: initBits, channels: initChannels);
            var val = (frameSize, bits, channels);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.frameSize);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, val.frameSize);
                Assert_TapeBound_Getters(x, init.frameSize);
                Assert_BuffBound_Getters(x, init.frameSize);
                Assert_Independent_Getters(x, init.frameSize);
                Assert_Immutable_Getters(x, init.frameSize);
                
                x.Record();
                Assert_All_Getters(x, val.frameSize);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits(val.bits).Channels(val.channels)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits(val.bits).Channels(val.channels)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits(val.bits).Channels(val.channels)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_FrameSize(string descriptor, int initFrameSize, int initBits, int initChannels, int frameSize, int bits, int channels)
        {
            var init = (frameSize: initFrameSize, bits: initBits, channels: initChannels);
            var val = (frameSize, bits, channels);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.frameSize);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.frameSize);
                Assert_TapeBound_Getters(x, val.frameSize);
                Assert_BuffBound_Getters(x, init.frameSize);
                Assert_Independent_Getters(x, init.frameSize);
                Assert_Immutable_Getters(x, init.frameSize);
                
                x.Record();
                Assert_All_Getters(x, init.frameSize); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Bits(val.bits).Channels(val.channels)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Bits(val.bits).Channels(val.channels)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits(val.bits).Channels(val.channels)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Bits(val.bits).Channels(val.channels)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_FrameSize(string descriptor, int initFrameSize, int initBits, int initChannels, int frameSize, int bits, int channels)
        {
            var init = (frameSize: initFrameSize, bits: initBits, channels: initChannels);
            var val = (frameSize, bits, channels);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.frameSize);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.frameSize);
                Assert_TapeBound_Getters(x, init.frameSize);
                Assert_BuffBound_Getters(x, val.frameSize);
                Assert_Independent_Getters(x, init.frameSize);
                Assert_Immutable_Getters(x, init.frameSize);
                
                x.Record();
                Assert_All_Getters(x, init.frameSize);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.Bits(val.bits, x.SynthBound.Context)
                                                                             .Channels(val.channels, x.SynthBound.Context)));
            
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Bits(val.bits, x.SynthBound.Context)
                                                                                                   .Channels(val.channels, x.SynthBound.Context)));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_FrameSize(string descriptor, int initFrameSize, int initBits, int initChannels, int frameSize, int bits, int channels)
        {
            // Independent after Taping

            var init = (frameSize: initFrameSize, bits: initBits, channels: initChannels);
            var val = (frameSize, bits, channels);

            // Sample
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.frameSize);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.frameSize);
                    Assert_Independent_Getter(x.Independent.Sample, val.frameSize);
                    Assert_Independent_Getter(x.Independent.AudioInfoWish,init.frameSize);
                    Assert_Independent_Getter(x.Independent.AudioFileInfo, init.frameSize);
                    Assert_Immutable_Getters(x, init.frameSize);

                    x.Record();
                    Assert_All_Getters(x, init.frameSize);
                }
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits(val.bits, x.SynthBound.Context)
                                                                                          .Channels(val.channels, x.SynthBound.Context)));
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.frameSize);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.frameSize);
                    Assert_Independent_Getter(x.Independent.AudioInfoWish, val.frameSize);
                    Assert_Independent_Getter(x.Independent.AudioFileInfo, init.frameSize);
                    Assert_Independent_Getter(x.Independent.Sample, init.frameSize);
                    Assert_Immutable_Getters(x, init.frameSize);

                    x.Record();
                    Assert_All_Getters(x, init.frameSize);
                }

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Bits(val.bits).Channels(val.channels)));
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.frameSize);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.frameSize);
                    Assert_Independent_Getter(x.Independent.AudioFileInfo, val.frameSize);
                    Assert_Independent_Getter(x.Independent.AudioInfoWish, init.frameSize);
                    Assert_Independent_Getter(x.Independent.Sample, init.frameSize);
                    Assert_Immutable_Getters(x, init.frameSize);

                    x.Record();
                    Assert_All_Getters(x, init.frameSize);
                }

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Bits(val.bits).Channels(val.channels)));
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_FrameSize(string descriptor, int initFrameSize, int initBits, int initChannels, int frameSize, int bits, int channels)
        {
            var init = (frameSize: initFrameSize, bits: initBits, channels: initChannels);
            var val = (frameSize, bits, channels);

            var x = CreateTestEntities(init);

            // WavHeader
            Assert_Immutable_Getter(x.Immutable.WavHeader, init.frameSize);
            var wavHeader = x.Immutable.WavHeader.Bits(val.bits).Channels(val.channels);
            Assert_Immutable_Getter(x.Immutable.WavHeader, init.frameSize);
            Assert_Immutable_Getter(wavHeader, val.frameSize);

            // Enums
            Assert_Immutable_Getter(x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, init.frameSize);
            Assert_Immutable_Getter(x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, init.frameSize);
            SampleDataTypeEnum sampleDataTypeEnum = val.bits.BitsToEnum();
            SpeakerSetupEnum speakerSetupEnum   = val.channels.ChannelsToEnum();
            Assert_Immutable_Getter(sampleDataTypeEnum, speakerSetupEnum, val.frameSize);

            // Entities
            Assert_Immutable_Getter(x.Immutable.SampleDataType, x.Immutable.SpeakerSetup, init.frameSize);
            Assert_Immutable_Getter(x.Immutable.SampleDataType, x.Immutable.SpeakerSetup, init.frameSize);
            SampleDataType sampleDataType = val.bits.BitsToEntity(x.SynthBound.Context);
            SpeakerSetup speakerSetup = val.channels.ChannelsToEntity(x.SynthBound.Context);
            Assert_Immutable_Getter(sampleDataType, speakerSetup, val.frameSize);
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.frameSize);
            
            // Except for our variables
            Assert_Immutable_Getter(wavHeader, val.frameSize);
            Assert_Immutable_Getter(sampleDataTypeEnum, speakerSetupEnum, val.frameSize);
            Assert_Immutable_Getter(sampleDataType, speakerSetup, val.frameSize);
        }

        [TestMethod]
        public void ConfigSection_FrameSize()
        {
            var x = CreateTestEntities(default);
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => x.SynthBound.ConfigSection.FrameSize());
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => x.SynthBound.ConfigSection.GetFrameSize());
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => FrameSize(x.SynthBound.ConfigSection));
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => GetFrameSize(x.SynthBound.ConfigSection));
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => ConfigWishesAccessor.FrameSize(x.SynthBound.ConfigSection));
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => ConfigWishesAccessor.GetFrameSize(x.SynthBound.ConfigSection));
        }

        [TestMethod]
        public void Default_FrameSize()
        {
            AreEqual(DefaultBits / 8 * DefaultChannels, () => DefaultFrameSize);
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int frameSize)
        {
            Assert_Bound_Getters(x, frameSize);
            Assert_Independent_Getters(x, frameSize);
            Assert_Immutable_Getters(x, frameSize);
        }

        private void Assert_Bound_Getters(TestEntities x, int frameSize)
        {
            Assert_SynthBound_Getters(x, frameSize);
            Assert_TapeBound_Getters(x, frameSize);
            Assert_BuffBound_Getters(x, frameSize);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int frameSize)
        {
            // Independent after Taping
            Assert_Independent_Getter(x.Independent.Sample, frameSize);
            Assert_Independent_Getter(x.Independent.AudioInfoWish, frameSize);
            Assert_Independent_Getter(x.Independent.AudioFileInfo, frameSize);
        }

        private void Assert_Immutable_Getters(TestEntities x, int frameSize)
        {
            Assert_Immutable_Getter(x.Immutable.WavHeader,                                        frameSize);
            Assert_Immutable_Getter(x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum, frameSize);
            Assert_Immutable_Getter(x.Immutable.SampleDataType,     x.Immutable.SpeakerSetup,     frameSize);
            Assert_Immutable_Getter(x.Immutable.Bits,               x.Immutable.Channels,         frameSize);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int frameSize)
        {
            AreEqual(frameSize, () => x.SynthBound.SynthWishes   .GetFrameSize);
            AreEqual(frameSize, () => x.SynthBound.FlowNode      .GetFrameSize);
            AreEqual(frameSize, () => x.SynthBound.SynthWishes   .GetFrameSize());
            AreEqual(frameSize, () => x.SynthBound.FlowNode      .GetFrameSize());
            AreEqual(frameSize, () => x.SynthBound.ConfigResolver.GetFrameSize());
            AreEqual(frameSize, () => x.SynthBound.SynthWishes   .FrameSize   ());
            AreEqual(frameSize, () => x.SynthBound.FlowNode      .FrameSize   ());
            AreEqual(frameSize, () => x.SynthBound.ConfigResolver.FrameSize   ());
            AreEqual(frameSize, () => GetFrameSize(x.SynthBound.SynthWishes   ));
            AreEqual(frameSize, () => GetFrameSize(x.SynthBound.FlowNode      ));
            AreEqual(frameSize, () => GetFrameSize(x.SynthBound.ConfigResolver));
            AreEqual(frameSize, () => FrameSize   (x.SynthBound.SynthWishes   ));
            AreEqual(frameSize, () => FrameSize   (x.SynthBound.FlowNode      ));
            AreEqual(frameSize, () => FrameSize   (x.SynthBound.ConfigResolver));
            AreEqual(frameSize, () => ConfigWishes        .GetFrameSize(x.SynthBound.SynthWishes   ));
            AreEqual(frameSize, () => ConfigWishes        .GetFrameSize(x.SynthBound.FlowNode      ));
            AreEqual(frameSize, () => ConfigWishesAccessor.GetFrameSize(x.SynthBound.ConfigResolver));
            AreEqual(frameSize, () => ConfigWishes        .FrameSize   (x.SynthBound.SynthWishes   ));
            AreEqual(frameSize, () => ConfigWishes        .FrameSize   (x.SynthBound.FlowNode      ));
            AreEqual(frameSize, () => ConfigWishesAccessor.FrameSize   (x.SynthBound.ConfigResolver));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int frameSize)
        {
            AreEqual(frameSize, () => x.TapeBound.Tape       .FrameSize   ());
            AreEqual(frameSize, () => x.TapeBound.TapeConfig .FrameSize   ());
            AreEqual(frameSize, () => x.TapeBound.TapeActions.FrameSize   ());
            AreEqual(frameSize, () => x.TapeBound.TapeAction .FrameSize   ());
            AreEqual(frameSize, () => x.TapeBound.Tape       .GetFrameSize());
            AreEqual(frameSize, () => x.TapeBound.TapeConfig .GetFrameSize());
            AreEqual(frameSize, () => x.TapeBound.TapeActions.GetFrameSize());
            AreEqual(frameSize, () => x.TapeBound.TapeAction .GetFrameSize());
            AreEqual(frameSize, () => FrameSize   (x.TapeBound.Tape       ));
            AreEqual(frameSize, () => FrameSize   (x.TapeBound.TapeConfig ));
            AreEqual(frameSize, () => FrameSize   (x.TapeBound.TapeActions));
            AreEqual(frameSize, () => FrameSize   (x.TapeBound.TapeAction ));
            AreEqual(frameSize, () => GetFrameSize(x.TapeBound.Tape       ));
            AreEqual(frameSize, () => GetFrameSize(x.TapeBound.TapeConfig ));
            AreEqual(frameSize, () => GetFrameSize(x.TapeBound.TapeActions));
            AreEqual(frameSize, () => GetFrameSize(x.TapeBound.TapeAction ));
            AreEqual(frameSize, () => ConfigWishes.FrameSize   (x.TapeBound.Tape       ));
            AreEqual(frameSize, () => ConfigWishes.FrameSize   (x.TapeBound.TapeConfig ));
            AreEqual(frameSize, () => ConfigWishes.FrameSize   (x.TapeBound.TapeActions));
            AreEqual(frameSize, () => ConfigWishes.FrameSize   (x.TapeBound.TapeAction ));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(x.TapeBound.Tape       ));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(x.TapeBound.TapeConfig ));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(x.TapeBound.TapeActions));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(x.TapeBound.TapeAction ));
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int frameSize)
        {
            AreEqual(frameSize, () => x.BuffBound.Buff           .FrameSize   ());
            AreEqual(frameSize, () => x.BuffBound.AudioFileOutput.FrameSize   ());
            AreEqual(frameSize, () => x.BuffBound.Buff           .GetFrameSize());
            AreEqual(frameSize, () => x.BuffBound.AudioFileOutput.GetFrameSize());
            AreEqual(frameSize, () => FrameSize   (x.BuffBound.Buff           ));
            AreEqual(frameSize, () => FrameSize   (x.BuffBound.AudioFileOutput));
            AreEqual(frameSize, () => GetFrameSize(x.BuffBound.Buff           ));
            AreEqual(frameSize, () => GetFrameSize(x.BuffBound.AudioFileOutput));
            AreEqual(frameSize, () => ConfigWishes.FrameSize   (x.BuffBound.Buff           ));
            AreEqual(frameSize, () => ConfigWishes.FrameSize   (x.BuffBound.AudioFileOutput));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(x.BuffBound.Buff           ));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(x.BuffBound.AudioFileOutput));
        }
        
        private void Assert_Independent_Getter(AudioFileInfo audioFileInfo, int frameSize)
        {
            AreEqual(frameSize, () => audioFileInfo.FrameSize());
            AreEqual(frameSize, () => audioFileInfo.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(audioFileInfo));
            AreEqual(frameSize, () => GetFrameSize(audioFileInfo));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(audioFileInfo));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(audioFileInfo));
        }
        
        private void Assert_Independent_Getter(Sample sample, int frameSize)
        {
            AreEqual(frameSize, () => sample.FrameSize());
            AreEqual(frameSize, () => sample.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(sample));
            AreEqual(frameSize, () => GetFrameSize(sample));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(sample));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(sample));
        }
        
        private void Assert_Independent_Getter(AudioInfoWish audioInfoWish, int frameSize)
        {
            AreEqual(frameSize, () => audioInfoWish.FrameSize());
            AreEqual(frameSize, () => audioInfoWish.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(audioInfoWish));
            AreEqual(frameSize, () => GetFrameSize(audioInfoWish));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(audioInfoWish));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(audioInfoWish));
        }

        private void Assert_Immutable_Getter(WavHeaderStruct wavHeader, int frameSize)
        {
            AreEqual(frameSize, () => wavHeader.FrameSize());
            AreEqual(frameSize, () => wavHeader.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(wavHeader));
            AreEqual(frameSize, () => GetFrameSize(wavHeader));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(wavHeader));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(wavHeader));
        }
        
        private void Assert_Immutable_Getter(SampleDataTypeEnum sampleDataTypeEnum, SpeakerSetupEnum speakerSetupEnum, int frameSize)
        {
            var enums = (sampleDataTypeEnum, speakerSetupEnum);
            AreEqual(frameSize, () => enums.FrameSize());
            AreEqual(frameSize, () => enums.ToFrameSize());
            AreEqual(frameSize, () => enums.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(enums));
            AreEqual(frameSize, () => ToFrameSize(enums));
            AreEqual(frameSize, () => GetFrameSize(enums));
            AreEqual(frameSize, () => FrameSize(sampleDataTypeEnum, speakerSetupEnum));
            AreEqual(frameSize, () => ToFrameSize(sampleDataTypeEnum, speakerSetupEnum));
            AreEqual(frameSize, () => GetFrameSize(sampleDataTypeEnum, speakerSetupEnum));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(enums));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(enums));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(enums));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(sampleDataTypeEnum, speakerSetupEnum));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(sampleDataTypeEnum, speakerSetupEnum));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(sampleDataTypeEnum, speakerSetupEnum));
        }
        
        private void Assert_Immutable_Getter(SampleDataType sampleDataType, SpeakerSetup speakerSetup, int frameSize)
        {
            var entities = (sampleDataType, speakerSetup);
            AreEqual(frameSize, () => entities.FrameSize());
            AreEqual(frameSize, () => entities.ToFrameSize());
            AreEqual(frameSize, () => entities.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(entities));
            AreEqual(frameSize, () => ToFrameSize(entities));
            AreEqual(frameSize, () => GetFrameSize(entities));
            AreEqual(frameSize, () => FrameSize(sampleDataType, speakerSetup));
            AreEqual(frameSize, () => ToFrameSize(sampleDataType, speakerSetup));
            AreEqual(frameSize, () => GetFrameSize(sampleDataType, speakerSetup));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(entities));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(entities));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(entities));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(sampleDataType, speakerSetup));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(sampleDataType, speakerSetup));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(sampleDataType, speakerSetup));
        }
                
        private void Assert_Immutable_Getter(int bits, int channels, int frameSize)
        {
            (int, int) tuple = (bits, channels);
            int? nullyBits = bits; // TODO: Test actual nully values with better Case definition structure
            int? nullyChannels = channels;
            (int?, int?) nullyTuple = (nullyBits, nullyChannels);

            AreEqual(frameSize, () => tuple.FrameSize());
            AreEqual(frameSize, () => tuple.ToFrameSize());
            AreEqual(frameSize, () => tuple.GetFrameSize());
            AreEqual(frameSize, () => nullyTuple.FrameSize());
            AreEqual(frameSize, () => nullyTuple.ToFrameSize());
            AreEqual(frameSize, () => nullyTuple.GetFrameSize());
            AreEqual(frameSize, () => FrameSize(tuple));
            AreEqual(frameSize, () => ToFrameSize(tuple));
            AreEqual(frameSize, () => GetFrameSize(tuple));
            AreEqual(frameSize, () => FrameSize(nullyTuple));
            AreEqual(frameSize, () => ToFrameSize(nullyTuple));
            AreEqual(frameSize, () => GetFrameSize(nullyTuple));
            AreEqual(frameSize, () => FrameSize(bits, channels));
            AreEqual(frameSize, () => ToFrameSize(bits, channels));
            AreEqual(frameSize, () => GetFrameSize(bits, channels));
            AreEqual(frameSize, () => FrameSize(nullyBits, nullyChannels));
            AreEqual(frameSize, () => ToFrameSize(nullyBits, nullyChannels));
            AreEqual(frameSize, () => GetFrameSize(nullyBits, nullyChannels));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(tuple));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(tuple));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(tuple));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(nullyTuple));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(nullyTuple));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(nullyTuple));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(bits, channels));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(bits, channels));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(bits, channels));
            AreEqual(frameSize, () => ConfigWishes.FrameSize(nullyBits, nullyChannels));
            AreEqual(frameSize, () => ConfigWishes.ToFrameSize(nullyBits, nullyChannels));
            AreEqual(frameSize, () => ConfigWishes.GetFrameSize(nullyBits, nullyChannels));
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities((int frameSize, int? bits, int? channels) init) 
            => new TestEntities(x => x.WithLoggingDisabled().WithBits(init.bits).WithChannels(init.channels).SamplingRate(HighPerfHz));
                 
        private static readonly int [] _bitsValues = { 8, 16, 32 };
        private static readonly int?[] _bitsValuesWithEmpty = { null, 0, 8, 16, 32 };
        private static readonly int [] _channelsValues = { 1, 2 };
        private static readonly int?[] _channelsValuesWithEmpty = { null, 0, 1, 2 };

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> TestParametersInit
        {
            get
            {
                foreach (int? bits in _bitsValuesWithEmpty)
                foreach (int? channels in _channelsValuesWithEmpty)
                {
                    int?   frameSize  = FrameSize(bits, channels);
                    string descriptor = GetParametersDescriptor(bits, channels);
                    yield return new object[] { descriptor, frameSize, bits, channels };
                }
            }
        }
        
        static IEnumerable<object[]> TestParametersWithEmpty 
            => GetTestParameters(_bitsValuesWithEmpty, _channelsValuesWithEmpty);
        
        static IEnumerable<object[]> TestParameters 
            => GetTestParameters(_bitsValues.Cast<int?>().ToArray(), _channelsValues.Cast<int?>().ToArray());

        
        static IEnumerable<object[]> GetTestParameters(int?[] bitsValues, int?[] channelsValues)
        {
            foreach (int? initBits in bitsValues)
            foreach (int? initChannels in channelsValues)
            foreach (int? bits in bitsValues)
            foreach (int? channels in channelsValues)
            {
                if (initBits == bits && initChannels == channels) continue;
                
                yield return new object[]
                {
                    GetParametersDescriptor(initBits, initChannels, bits, channels),
                    FrameSize(initBits, initChannels),
                    initBits,
                    initChannels,
                    FrameSize(bits, channels),
                    bits,
                    channels
                };
            }
            
            // Add 1 case where the source and dest values are equal.
            int? lastBits      = bitsValues.Last();
            int? lastChannels  = channelsValues.Last();
            int? lastFrameSize = FrameSize(lastBits, lastChannels);
            
            yield return new object[]
            {
                GetParametersDescriptor(lastBits, lastChannels, lastBits, lastChannels),
                lastFrameSize,
                lastBits,
                lastChannels,
                lastFrameSize,
                lastBits,
                lastChannels
            };
        }
        
        static string GetParametersDescriptor(int? initBits, int? initChannels, int? bits, int? channels)
        {
            string initDescriptor = GetParametersDescriptor(initBits, initChannels);
            string valDescriptor = GetParametersDescriptor(bits, channels);
            return $"{initDescriptor}=> {valDescriptor}";
        }
        
        static string GetParametersDescriptor(int? bits, int? channels)
        {
            string bitsFormatted = bits == null ? "null-bit" : bits == 0 ? "0-bit" : $"{bits}-bit";
            string channelsDescriptor = channels == null ? "null-channels" : channels == 0 ? "0-channels" : channels.ChannelDescriptor().ToLower();
            return $"{bitsFormatted}-{channelsDescriptor} ";
        }
       
        // ncrunch: no coverage end
    } 
}