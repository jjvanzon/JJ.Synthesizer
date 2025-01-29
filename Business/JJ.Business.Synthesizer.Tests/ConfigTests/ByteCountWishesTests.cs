using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Wishes.Common;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class ByteCountWishesTests
    {
        // Test Data Helpers
        
        class Case : CaseBase<int>
        {
            public CaseProp<int>    ByteCount      => MainProp;
            public CaseProp<double> AudioLength    { get; set; }
            public CaseProp<int>    FrameCount     { get; set; }
            public CaseProp<int>    SamplingRate   { get; set; }
            public CaseProp<int>    Bits           { get; set; }
            public CaseProp<int>    SizeOfBitDepth { get; set; }
            public CaseProp<int>    Channels       { get; set; }
            public CaseProp<int>    HeaderLength   { get; set; }
            public CaseProp<int>    CourtesyFrames { get; set; }
        }

        static CaseCollection<Case> Cases { get; } = new CaseCollection<Case>();
        
        static CaseCollection<Case> SimpleCases { get; } = Cases.Add
        (
            new Case { From = 100, To = 200, SizeOfBitDepth = { From = 4, To = 2 }  },
            new Case { From = 200, To = 100, SizeOfBitDepth = { From = 2, To = 4 }  }
        );
        
        static CaseCollection<Case> DependencyCases { get; } = Cases.FromTemplate(new Case
            {
                Bits = 32,
                Channels = 1,
                SamplingRate = 1000, 
                AudioLength = 0.1, 
                HeaderLength = 0,
                CourtesyFrames = 2,
                ByteCount = { From = 400+8, To = 800+8 }
            },
            new Case { FrameCount = { From = 100+2, To = 200+2 } },
            new Case { AudioLength = { To = 0.2 } },
            new Case { SamplingRate = { To = 2000 } },
            new Case { Channels = { To = 2 }, ByteCount = { To = 800+16 } },
            new Case { Bits = { From = 16, To = 32 }, ByteCount = { From = 200+4, To = 400+8 } },
            new Case { HeaderLength = { To = 44 }, ByteCount = { To = 400+8+44 } },
            new Case { CourtesyFrames = { To = 3 }, ByteCount = { To = 400+12 } }
        );

        static ConfigTestEntities CreateTestEntities(int init, int sizeOfBitDepthInit)
            // Change bit depth first, or it'll change the byte count.
            => new ConfigTestEntities(x => x.SizeOfBitDepth(sizeOfBitDepthInit).ByteCount(init));

        static ConfigTestEntities CreateTestEntities(Case val) 
            => new ConfigTestEntities(synth => 
            {
                // Change primary properties before ByteCount, or they will change the byte count.
                
                int?    bits           = val.Bits          .Init.Nully;
                int?    sizeOfBitDepth = val.SizeOfBitDepth.Init.Nully;
                int?    channels       = val.Channels      .Init.Nully;
                int?    samplingRate   = val.SamplingRate  .Init.Nully;
                int?    headerLength   = val.HeaderLength  .Init.Nully;
                int?    courtesyFrames = val.CourtesyFrames.Init.Nully;
                double? audioLength    = val.AudioLength   .Init.Nully;
                int?    frameCount     = val.FrameCount    .Init.Nully;
                int?    byteCount      = val.ByteCount     .Init.Nully;
                
                if (CoalesceBits(bits) != DefaultBits)
                    synth.Bits(bits);
                
                if (CoalesceSizeOfBitDepth(sizeOfBitDepth) != DefaultSizeOfBitDepth)
                    synth.SizeOfBitDepth(sizeOfBitDepth);

                if (CoalesceChannels(channels) != DefaultChannels)
                    synth.Channels(channels);
                                
                if (CoalesceSamplingRate(samplingRate) != DefaultSamplingRate)
                    synth.SamplingRate(samplingRate);
                
                if (CoalesceHeaderLength(headerLength) != DefaultHeaderLength)
                    synth.HeaderLength(headerLength);
                
                if (CoalesceCourtesyFrames(courtesyFrames) != DefaultCourtesyFrames)
                    synth.CourtesyFrames(courtesyFrames);

                if (CoalesceFrameCount(frameCount) != DefaultFrameCount)
                    synth.FrameCount(frameCount);
                
                if (CoalesceByteCount(byteCount) != DefaultByteCount)
                    synth.ByteCount(byteCount);
                
                if (CoalesceAudioLength(audioLength) != DefaultAudioLength)
                    synth.AudioLength(audioLength);
            });
        
        // Tests
        
        [TestMethod]
        public void ByteCount_Basic_ConversionFormula()
        {
            const double audioLength    = 0.5;
            const int    samplingRate   = 24000;
            const int    bits           = 16;
            const int    channels       = 2;
            const int    headerLength   = 44;
            const int    courtesyFrames = 3;
            
            const int frameCount = (int)(audioLength * samplingRate) + courtesyFrames;
            const int frameSize  = bits / 8 * channels;
            
            const int byteCountExpected = frameCount * frameSize + headerLength;
            
            AreEqual(byteCountExpected, audioLength.ByteCount(samplingRate, bits, channels, headerLength, courtesyFrames));
            AreEqual(byteCountExpected, audioLength.ByteCount(samplingRate, frameSize, headerLength, courtesyFrames));
            AreEqual(byteCountExpected, frameCount.ByteCount(bits, channels, headerLength));
            AreEqual(byteCountExpected, frameCount.ByteCount(frameSize, headerLength));
        }

        [TestMethod]
        public void ByteCount_Basic_Init() => new ConfigTestEntities(x => x.ByteCount(100));
        
        [TestMethod]
        public void ByteCount_Basic_Getters()
        {
            int byteCount = 100;
            var entities = new ConfigTestEntities(x => x.ByteCount(byteCount));
            
            AreEqual(DefaultByteCount, entities.SynthBound.ConfigSection.ByteCount());
            
            AreEqual(byteCount, entities.SynthBound.SynthWishes   .ByteCount());
            AreEqual(byteCount, entities.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, entities.SynthBound.FlowNode2     .ByteCount());
            AreEqual(byteCount, entities.SynthBound.ConfigResolver.ByteCount(entities.SynthBound.SynthWishes));
            
            AreEqual(byteCount, entities.TapeBound.Tape           .ByteCount());
            AreEqual(byteCount, entities.TapeBound.TapeConfig     .ByteCount());
            AreEqual(byteCount, entities.TapeBound.TapeActions    .ByteCount());
            AreEqual(byteCount, entities.TapeBound.TapeAction     .ByteCount());
            
            AreEqual(byteCount, entities.BuffBound.Buff           .ByteCount(DefaultCourtesyFrames));
            AreEqual(byteCount, entities.BuffBound.AudioFileOutput.ByteCount(DefaultCourtesyFrames));
            AreEqual(byteCount, entities.Independent.Sample       .ByteCount());
            AreEqual(byteCount, entities.Immutable.WavHeader      .ByteCount());
            
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.Bits              .ByteCount());
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.Type              .ByteCount());
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.SampleDataType    .ByteCount());
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.SampleDataTypeEnum.ByteCount());
            
            foreach (var channelEntities in entities.ChannelEntities)
            {
                AreEqual(byteCount, channelEntities.TapeBound.Tape           .ByteCount());
                AreEqual(byteCount, channelEntities.TapeBound.TapeConfig     .ByteCount());
                AreEqual(byteCount, channelEntities.TapeBound.TapeActions    .ByteCount());
                AreEqual(byteCount, channelEntities.TapeBound.TapeAction     .ByteCount());
                
                AreEqual(byteCount, channelEntities.BuffBound.Buff           .ByteCount(DefaultCourtesyFrames));
                AreEqual(byteCount, channelEntities.BuffBound.AudioFileOutput.ByteCount(DefaultCourtesyFrames));
                AreEqual(byteCount, channelEntities.Independent.Sample       .ByteCount());
                AreEqual(byteCount, channelEntities.Immutable.WavHeader      .ByteCount());

                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.Bits              .ByteCount());
                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.Type              .ByteCount());
                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.SampleDataType    .ByteCount());
                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.SampleDataTypeEnum.ByteCount());
            }
        }
        
        [TestMethod]
        public void ByteCount_Basic_Setters()
        {
            int init = 100;
            int value = 200;
            int sizeOfBitDepthInit = 4;
            int sizeOfBitDepthValue = 2;

            var x = CreateTestEntities(init, sizeOfBitDepthInit);
            
            x.SynthBound.SynthWishes      .ByteCount(value);
            x.SynthBound.FlowNode         .ByteCount(value);
            x.SynthBound.FlowNode2        .ByteCount(value);
            x.SynthBound.ConfigResolver   .ByteCount(value, x.SynthBound.SynthWishes);
            x.TapeBound.Tape              .ByteCount(value);
            x.TapeBound.TapeConfig        .ByteCount(value);
            x.TapeBound.TapeActions       .ByteCount(value);
            x.TapeBound.TapeAction        .ByteCount(value);
            x.BuffBound.Buff              .ByteCount(value, DefaultCourtesyFrames);
            x.BuffBound.AudioFileOutput   .ByteCount(value, DefaultCourtesyFrames);
            x.Immutable.WavHeader         .ByteCount(value);
            x.Immutable.SampleDataType    .ByteCount(sizeOfBitDepthValue, x.SynthBound.Context);
            x.Immutable.SampleDataTypeEnum.ByteCount(sizeOfBitDepthValue);
            x.Immutable.Bits              .ByteCount(sizeOfBitDepthValue);
            x.Immutable.Type              .ByteCount(sizeOfBitDepthValue);
            
            foreach (var channelEntities in x.ChannelEntities)
            {
                channelEntities.TapeBound.Tape              .ByteCount(value);
                channelEntities.TapeBound.TapeConfig        .ByteCount(value);
                channelEntities.TapeBound.TapeActions       .ByteCount(value);
                channelEntities.TapeBound.TapeAction        .ByteCount(value);
                channelEntities.BuffBound.Buff              .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.BuffBound.AudioFileOutput   .ByteCount(value, DefaultCourtesyFrames);
                channelEntities.Immutable.WavHeader         .ByteCount(value);
                channelEntities.Immutable.Bits              .ByteCount(sizeOfBitDepthValue);
                channelEntities.Immutable.Type              .ByteCount(sizeOfBitDepthValue);
                channelEntities.Immutable.SampleDataType    .ByteCount(sizeOfBitDepthValue, x.SynthBound.Context);
                channelEntities.Immutable.SampleDataTypeEnum.ByteCount(sizeOfBitDepthValue);
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(Cases))]
        public void SynthBound_ByteCount(string caseKey)
        {   
            var testCase = Cases[caseKey];
            int init = testCase.Init;
            int value = testCase.Value;
            var sizeOfBitDepth = testCase.SizeOfBitDepth;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, value);
                Assert_TapeBound_Getters  (x, init );
                Assert_BuffBound_Getters  (x, init );
                Assert_Independent_Getters(x, init );
                Assert_Immutable_Getters  (x, init );
                Assert_Bitness_Getters    (x, sizeOfBitDepth.Init);
                
                x.Record();
                Assert_All_Getters(x, value, sizeOfBitDepth.Init); // By Design: Properties that express bit-ness don't change their ByteCount the same way.
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .ByteCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .ByteCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.ByteCount(value, x.SynthBound.SynthWishes)));
            
            if (testCase.AudioLength.From != testCase.AudioLength.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioLength(testCase.AudioLength.To, x.SynthBound.SynthWishes)));
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioLength(testCase.AudioLength.To, x.SynthBound.SynthWishes)));
            }
                        
            if (testCase.FrameCount.From != testCase.FrameCount.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameCount(testCase.FrameCount.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameCount(testCase.FrameCount.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameCount(testCase.FrameCount.To, x.SynthBound.SynthWishes)));
            }

            if (testCase.SamplingRate.From != testCase.SamplingRate.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SamplingRate(testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SamplingRate(testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SamplingRate(testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithSamplingRate(testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithSamplingRate(testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithSamplingRate(testCase.SamplingRate.To)));
            }
            
            if (testCase.Bits.From != testCase.Bits.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits(testCase.Bits.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits(testCase.Bits.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits(testCase.Bits.To)));
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithBits(testCase.Bits.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithBits(testCase.Bits.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithBits(testCase.Bits.To)));
            }
            
            if (testCase.Channels.From != testCase.Channels.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Channels(testCase.Channels.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Channels(testCase.Channels.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Channels(testCase.Channels.To)));
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithChannels(testCase.Channels.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithChannels(testCase.Channels.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithChannels(testCase.Channels.To)));
            }
            
            if (testCase.HeaderLength.From != testCase.HeaderLength.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .HeaderLength(testCase.HeaderLength.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .HeaderLength(testCase.HeaderLength.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.HeaderLength(testCase.HeaderLength.To)));
            }

            if (testCase.CourtesyFrames.From != testCase.CourtesyFrames.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .CourtesyFrames(testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .CourtesyFrames(testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.CourtesyFrames(testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithCourtesyFrames(testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithCourtesyFrames(testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithCourtesyFrames(testCase.CourtesyFrames.To)));
            }
        }

        [TestMethod] 
        [DynamicData(nameof(Cases))]
        public void TapeBound_ByteCount(string caseKey)
        {
            var testCase = Cases[caseKey];
            int init = testCase.Init;
            int value = testCase.Value;
            var sizeOfBitDepth = testCase.SizeOfBitDepth;

            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters  (x, init); // By Design: Tape is too buff to change. FrameCount will be based on buff.
                Assert_BuffBound_Getters  (x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters  (x, init);
                Assert_Bitness_Getters    (x, sizeOfBitDepth.Init);

                x.Record();
                Assert_All_Getters(x, init, sizeOfBitDepth.Init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ByteCount(value)));

            if (testCase.AudioLength.From != testCase.AudioLength.To)
            {
                AssertProp(x =>                                   x.TapeBound.Tape       .Duration  = testCase.AudioLength.To);
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .AudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .AudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.AudioLength(testCase.AudioLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .AudioLength(testCase.AudioLength.To)));
            }
                        
            if (testCase.FrameCount.From != testCase.FrameCount.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .FrameCount(testCase.FrameCount.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .FrameCount(testCase.FrameCount.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.FrameCount(testCase.FrameCount.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .FrameCount(testCase.FrameCount.To)));
            }

            if (testCase.SamplingRate.From != testCase.SamplingRate.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SamplingRate  (testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SamplingRate  (testCase.SamplingRate.To)));
                AssertProp(x =>                                   x.TapeBound.TapeConfig .SamplingRate = testCase.SamplingRate.To);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SamplingRate  (testCase.SamplingRate.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SamplingRate  (testCase.SamplingRate.To)));
            }
            
            if (testCase.Bits.From != testCase.Bits.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .Bits  (testCase.Bits.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .Bits  (testCase.Bits.To)));
                AssertProp(x =>                                   x.TapeBound.TapeConfig .Bits = testCase.Bits.To);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.Bits  (testCase.Bits.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .Bits  (testCase.Bits.To)));
            }
            
            if (testCase.Channels.From != testCase.Channels.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .Channels  (testCase.Channels.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .Channels  (testCase.Channels.To)));
                AssertProp(x =>                                   x.TapeBound.TapeConfig .Channels = testCase.Channels.To);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.Channels  (testCase.Channels.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .Channels  (testCase.Channels.To)));
            }
            
            if (testCase.HeaderLength.From != testCase.HeaderLength.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .HeaderLength  (testCase.HeaderLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .HeaderLength  (testCase.HeaderLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.HeaderLength  (testCase.HeaderLength.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .HeaderLength  (testCase.HeaderLength.To)));
            }

            if (testCase.CourtesyFrames.From != testCase.CourtesyFrames.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .CourtesyFrames  (testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .CourtesyFrames  (testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.CourtesyFrames  (testCase.CourtesyFrames.To)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .CourtesyFrames  (testCase.CourtesyFrames.To)));
            }
        }

        [TestMethod] 
        [DynamicData(nameof(SimpleCases))]
        public void BuffBound_ByteCount(string caseKey)
        {
            var testCase = Cases[caseKey];
            int init = testCase.Init;
            int value = testCase.Value;
            var sizeOfBitDepth = testCase.SizeOfBitDepth;

            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters     (x, init );
                Assert_TapeBound_Getters      (x, init );
                Assert_Buff_Getters           (x, init ); // By Design: Buff's "too buff" to change! FrameCount will be based on bytes!
                Assert_AudioFileOutput_Getters(x, value); // By Design: "Out" will take on new properties when asked.
                Assert_Independent_Getters    (x, init );
                Assert_Immutable_Getters      (x, init );
                Assert_Bitness_Getters        (x, sizeOfBitDepth.Init);

                x.Record();
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
            }

            // TODO: Why the dependency on CourtesyFrames?
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .ByteCount(value, DefaultCourtesyFrames)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.ByteCount(value, DefaultCourtesyFrames)));
        }
        
        [TestMethod]
        [DynamicData(nameof(SimpleCases))]
        public void Immutable_SizeOfBitDepth(string caseKey)
        {
            var testCase = Cases[caseKey];
            int init = testCase.Init;
            int value = testCase.Value;
            var sizeOfBitDepth = testCase.SizeOfBitDepth;

            var x = CreateTestEntities(testCase);

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
                
                AssertProp(() => x.Immutable.WavHeader.ByteCount(value));
            }

            // SampleDataTypeEnum

            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Init);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Init);
                    Assert_Bitness_Getters(sampleDataTypeEnum2, sizeOfBitDepth.Value);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.ByteCount(sizeOfBitDepth.Value));
            }

            // SampleDataType

            var sampleDataTypes = new List<SampleDataType>();
            {
                void AssertProp(Func<SampleDataType> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.SampleDataType, sizeOfBitDepth.Init);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.SampleDataType, sizeOfBitDepth.Init);
                    Assert_Bitness_Getters(sampleDataType2, sizeOfBitDepth.Value);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            
                AssertProp(() => x.Immutable.SampleDataType.ByteCount(sizeOfBitDepth.Value, x.SynthBound.Context));
            }
            
            // Type

            var types = new List<Type>();
            {
                void AssertProp(Func<Type> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.Type, sizeOfBitDepth.Init);
                    
                    var type2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.Type, sizeOfBitDepth.Init);
                    Assert_Bitness_Getters(type2, sizeOfBitDepth.Value);
                    
                    types.Add(type2);
                }

                AssertProp(() => x.Immutable.Type.ByteCount(sizeOfBitDepth.Value));
            }
                
            // Bits

            var bitsList = new List<int>();
            {
                void AssertProp(Func<int> setter)
                {
                    Assert_Bitness_Getters(x.Immutable.Bits, sizeOfBitDepth.Init);

                    int bits2 = setter();
                    
                    Assert_Bitness_Getters(x.Immutable.Bits, sizeOfBitDepth.Init);
                    Assert_Bitness_Getters(bits2, sizeOfBitDepth.Value);
                    
                    bitsList.Add(bits2);
                }
            
                AssertProp(() => x.Immutable.Bits.ByteCount(sizeOfBitDepth.Value));
            }

            // After-Record
            x.Record();
            
            // All is reset
            Assert_SynthBound_Getters (x, init);
            Assert_TapeBound_Getters  (x, init);
            Assert_BuffBound_Getters  (x, init);
            Assert_Independent_Getters(x, init);
            Assert_Immutable_Getters  (x, init);
            Assert_Bitness_Getters    (x, sizeOfBitDepth.Init);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, value));
            sampleDataTypeEnums.ForEach(e => Assert_Bitness_Getters(e, sizeOfBitDepth.Value));
            sampleDataTypes    .ForEach(s => Assert_Bitness_Getters(s, sizeOfBitDepth.Value));
            types              .ForEach(t => Assert_Bitness_Getters(t, sizeOfBitDepth.Value));
            bitsList           .ForEach(b => Assert_Bitness_Getters(b, sizeOfBitDepth.Value));
        }

        [TestMethod]
        public void ConfigSection_ByteCount()
        {
            // Get-only
            var configSection = new ConfigTestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultByteCount, () => configSection.ByteCount());
        }

        [TestMethod]
        public void Default_ByteCount()
        {
            // ReSharper disable once PossibleLossOfFraction
            double fromPrimitives = 
                (DefaultAudioLength * DefaultSamplingRate + DefaultCourtesyFrames) *
                (DefaultBits / 8 * DefaultChannels) + 
                DefaultHeaderLength;
            
            AreEqual(fromPrimitives, () => DefaultByteCount);
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, int byteCount, int sizeOfBitDepth)
        {
            Assert_SynthBound_Getters    (x, byteCount);
            Assert_TapeBound_Getters     (x, byteCount);
            Assert_BuffBound_Getters     (x, byteCount);
            Assert_Independent_Getters   (x, byteCount);
            Assert_Immutable_Getters     (x, byteCount);
            Assert_Bitness_Getters(x, sizeOfBitDepth);
        }
        
        private void Assert_SynthBound_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);
            AreEqual(byteCount, () => x.SynthBound.SynthWishes   .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.ConfigResolver.ByteCount(x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);
            AreEqual(byteCount, () => x.TapeBound.Tape       .ByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeConfig .ByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeActions.ByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeAction .ByteCount());
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int byteCount)
        {
            Assert_Buff_Getters           (x, byteCount);
            Assert_AudioFileOutput_Getters(x, byteCount);
        }

        private void Assert_AudioFileOutput_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.AudioFileOutput);
            AreEqual(byteCount, () => x.BuffBound.AudioFileOutput.ByteCount(x.Immutable.CourtesyFrames));
        }
        
        private void Assert_Buff_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            AreEqual(byteCount, () => x.BuffBound.Buff.ByteCount(x.Immutable.CourtesyFrames));
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            IsNotNull(() => x.Independent.Sample);
            AreEqual(byteCount, () => x.Independent.Sample.ByteCount());
        }
        
        private void Assert_Immutable_Getters(ConfigTestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, byteCount);
        }
                
        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int byteCount)
        {
            if (wavHeader.IsNully()) return;
            AreEqual(byteCount, () => wavHeader.ByteCount());
        }

        private void Assert_Bitness_Getters(ConfigTestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Bitness_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth);
            Assert_Bitness_Getters(x.Immutable.SampleDataType    , sizeOfBitDepth);
            Assert_Bitness_Getters(x.Immutable.Type              , sizeOfBitDepth);
            Assert_Bitness_Getters(x.Immutable.Bits              , sizeOfBitDepth);
        }
        
        private void Assert_Bitness_Getters(SampleDataTypeEnum sampleDataTypeEnum, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;
            
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.ByteCount());
        }
        
        private void Assert_Bitness_Getters(SampleDataType sampleDataType, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;
            
            IsNotNull(() => sampleDataType);
            AreEqual(sizeOfBitDepth, () => sampleDataType.ByteCount());
        }
        
        private void Assert_Bitness_Getters(Type type, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;

            IsNotNull(() => type);
            AreEqual(sizeOfBitDepth, () => type.ByteCount());
        }
        
        private void Assert_Bitness_Getters(int bits, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;

            AreEqual(sizeOfBitDepth, () => bits.ByteCount());
        }
    }
}
