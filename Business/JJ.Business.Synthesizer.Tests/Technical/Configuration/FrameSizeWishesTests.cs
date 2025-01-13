using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Tests.Technical.Configuration.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

#pragma warning disable CS0611 
#pragma warning disable CS0618 
#pragma warning disable MSTEST0018 

namespace JJ.Business.Synthesizer.Tests.Technical.Configuration
{
    [TestClass]
    [TestCategory("Technical")]
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes .Bits(val.bits).Channels(val.channels)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode    .Bits(val.bits).Channels(val.channels)));
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
            var initEnums = (x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum);
            Assert_Immutable_Getter(initEnums, init.frameSize);
            var enums = (val.bits.BitsToEnum(), val.channels.ChannelsToEnum());
            Assert_Immutable_Getter(initEnums, init.frameSize);
            Assert_Immutable_Getter(enums, val.frameSize);

            // Entities
            var initEntities = (x.Immutable.SampleDataType, x.Immutable.SpeakerSetup);
            Assert_Immutable_Getter(initEntities, init.frameSize);
            var entities = (val.bits.BitsToEntity(x.SynthBound.Context), val.channels.ChannelsToEntity(x.SynthBound.Context));
            Assert_Immutable_Getter(initEntities, init.frameSize);
            Assert_Immutable_Getter(entities, val.frameSize);
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.frameSize);
            
            // Except for our variables
            Assert_Immutable_Getter(wavHeader, val.frameSize);
            Assert_Immutable_Getter(enums, val.frameSize);
            Assert_Immutable_Getter(entities, val.frameSize);
        }

        [TestMethod]
        public void GlobalBound_FrameSize()
        {
            // Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            AreEqual(DefaultBits / 8 * DefaultChannels      , () => DefaultFrameSize);
            AreEqual(FrameSize(DefaultBits, DefaultChannels), () => configSection.FrameSize());
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
            Assert_Immutable_Getter(x.Immutable.WavHeader, frameSize);
            Assert_Immutable_Getter((x.Immutable.SampleDataTypeEnum, x.Immutable.SpeakerSetupEnum), frameSize);
            Assert_Immutable_Getter((x.Immutable.SampleDataType, x.Immutable.SpeakerSetup), frameSize);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int frameSize)
        {
            AreEqual(frameSize, () => x.SynthBound.SynthWishes.FrameSize());
            AreEqual(frameSize, () => x.SynthBound.FlowNode.FrameSize());
            AreEqual(frameSize, () => x.SynthBound.ConfigResolver.FrameSize());
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int frameSize)
        {
            AreEqual(frameSize, () => x.TapeBound.Tape.FrameSize());
            AreEqual(frameSize, () => x.TapeBound.TapeConfig.FrameSize());
            AreEqual(frameSize, () => x.TapeBound.TapeActions.FrameSize());
            AreEqual(frameSize, () => x.TapeBound.TapeAction.FrameSize());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int frameSize)
        {
            AreEqual(frameSize, () => x.BuffBound.Buff.FrameSize());
            AreEqual(frameSize, () => x.BuffBound.AudioFileOutput.FrameSize());
        }

        private void Assert_Independent_Getter(AudioFileInfo audioFileInfo, int frameSize)
        {
            AreEqual(frameSize, () => audioFileInfo.FrameSize());
        }
        
        private void Assert_Independent_Getter(Sample sample, int frameSize)
        {
            AreEqual(frameSize, () => sample.FrameSize());
        }
        
        private void Assert_Independent_Getter(AudioInfoWish audioInfoWish, int frameSize)
        {
            AreEqual(frameSize, () => audioInfoWish.FrameSize());
        }

        private void Assert_Immutable_Getter(WavHeaderStruct wavHeader, int frameSize)
        {
            AreEqual(frameSize, () => wavHeader.FrameSize());
        }
        
        private void Assert_Immutable_Getter((SampleDataTypeEnum, SpeakerSetupEnum) enums, int frameSize)
        {
            AreEqual(frameSize, () => enums.FrameSize());
        }
        
        private void Assert_Immutable_Getter((SampleDataType, SpeakerSetup) entities, int frameSize)
        {
            AreEqual(frameSize, () => entities.FrameSize());
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities((int frameSize, int? bits, int? channels) init) 
            => new TestEntities(x => x.WithBits(init.bits).WithChannels(init.channels));
                 
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
                    int?   frameSize  = FrameSize(CoalesceBits(bits), CoalesceChannels(channels));
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
            string channelsDescriptor = channels == null ? "null-channels" : channels == 0 ? "0-channels" : ChannelDescriptor(channels).ToLower();
            return $"{bitsFormatted}-{channelsDescriptor} ";
        }
       
        // ncrunch: no coverage end
    } 
}