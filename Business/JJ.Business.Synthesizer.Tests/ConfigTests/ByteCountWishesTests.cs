using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Common;
using JJ.Framework.Wishes.Testing;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
// ReSharper disable ArrangeStaticMemberQualifier

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
            public CaseProp<int>    Channels       { get; set; }
            public CaseProp<int>    Bits           { get; set; }
            public CaseProp<int>    SizeOfBitDepth { get; set; }
            public CaseProp<int>    HeaderLength   { get; set; }
            public CaseProp<int>    CourtesyFrames { get; set; }
        }

        static CaseCollection<Case> Cases { get; } = new CaseCollection<Case>();
        
        static CaseCollection<Case> BasicCases { get; } = Cases.FromTemplate(new Case
        
            { CourtesyFrames = { Nully = null, Coalesced = DefaultCourtesyFrames } }, // CourtesyFrames needed in BuffBound tests where methods take it as nullable or non-nullable parameter.
            
            new Case { From = 100, To = 200, SizeOfBitDepth = { From = 4, To = 2 }  },
            new Case { From = 200, To = 100, SizeOfBitDepth = { From = 2, To = 4 }  }
        );
        
        static CaseCollection<Case> DependencyCases { get; } = Cases.FromTemplate(new Case
            {
                Name = "Dependency",
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
            new Case { Bits = { To = 16 }, ByteCount = { To = 200+4 } },
            new Case { HeaderLength = { To = WavHeaderLength }, ByteCount = { To = 400+8 + WavHeaderLength } },
            new Case { CourtesyFrames = { To = 3 }, ByteCount = { To = 400+12 } }
        );
        
        static CaseCollection<Case> WavDependencyCases { get; } = Cases.FromTemplate(new Case
            {
                Name = "Wav",
                Bits = 32,
                Channels = 1,
                SamplingRate = 1000, 
                AudioLength = 0.1, 
                HeaderLength = WavHeaderLength,
                CourtesyFrames = 2,
                ByteCount = { From = 400+8 + WavHeaderLength, To = 800+8 + WavHeaderLength }
            },
            new Case { FrameCount = { From = 100+2, To = 200+2 } },
            new Case { AudioLength = { To = 0.2 } },
            new Case { SamplingRate = { To = 2000 } },
            new Case { Channels = { To = 2 }, ByteCount = { To = 800 + 16 + WavHeaderLength } },
            new Case { Bits = { To = 16 }, ByteCount = { To = 200 + 4 + WavHeaderLength } },
            // TODO: { To = 0 } becomes (0,44). Separate test for case definition?
            //new Case { HeaderLength = { To = 0 }, ByteCount = { To = 400 + 8 } }, 
            new Case { CourtesyFrames = { To = 3 }, ByteCount = { To = 400 + 12 + WavHeaderLength } }
        );

        static TestEntities CreateTestEntities(int init, int sizeOfBitDepthInit)
            // Change bit depth first, or it'll change the byte count.
            => new TestEntities(x => x.SizeOfBitDepth(sizeOfBitDepthInit).ByteCount(init).SamplingRate(HighPerfHz));

        static TestEntities CreateTestEntities(Case val) 
            => new TestEntities(synth => 
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
            
            AreEqual(byteCountExpected, audioLength .ByteCount               (             samplingRate, bits, channels, headerLength));
            AreEqual(byteCountExpected, audioLength .ByteCountFromAudioLength(             samplingRate, bits, channels, headerLength));
            AreEqual(byteCountExpected,              ByteCount               (audioLength, samplingRate, bits, channels, headerLength));
            AreEqual(byteCountExpected,              ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCount               (audioLength, samplingRate, bits, channels, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, bits, channels, headerLength));
            
            AreEqual(byteCountExpected, audioLength .ByteCount               (             samplingRate, frameSize, headerLength));
            AreEqual(byteCountExpected, audioLength .ByteCountFromAudioLength(             samplingRate, frameSize, headerLength));
            AreEqual(byteCountExpected,              ByteCount               (audioLength, samplingRate, frameSize, headerLength));
            AreEqual(byteCountExpected,              ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCount               (audioLength, samplingRate, frameSize, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCountFromAudioLength(audioLength, samplingRate, frameSize, headerLength));
            
            AreEqual(byteCountExpected, frameCount  .ByteCount              (            bits, channels, headerLength));
            AreEqual(byteCountExpected, frameCount  .ByteCountFromFrameCount(            bits, channels, headerLength));
            AreEqual(byteCountExpected,              ByteCount              (frameCount, bits, channels, headerLength));
            AreEqual(byteCountExpected,              ByteCountFromFrameCount(frameCount, bits, channels, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCount              (frameCount, bits, channels, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCountFromFrameCount(frameCount, bits, channels, headerLength));
            
            AreEqual(byteCountExpected, frameCount  .ByteCount              (frameSize, headerLength));
            AreEqual(byteCountExpected, frameCount  .ByteCountFromFrameCount(frameSize, headerLength));
            AreEqual(byteCountExpected,              ByteCount              (frameCount, frameSize, headerLength));
            AreEqual(byteCountExpected,              ByteCountFromFrameCount(frameCount, frameSize, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCount              (frameCount, frameSize, headerLength));
            AreEqual(byteCountExpected, ConfigWishes.ByteCountFromFrameCount(frameCount, frameSize, headerLength));
        }

        [TestMethod]
        public void ByteCount_Basic_Init() => new TestEntities(x => x.ByteCount(100));
        
        [TestMethod]
        public void ByteCount_Basic_Getters()
        {
            int byteCount = 100;
            var entities = new TestEntities(x => x.ByteCount(byteCount));
            
            AreEqual(DefaultByteCount, entities.SynthBound.ConfigSection.GetByteCount());
            
            AreEqual(byteCount, entities.SynthBound.SynthWishes   .GetByteCount());
            AreEqual(byteCount, entities.SynthBound.FlowNode      .GetByteCount());
            AreEqual(byteCount, entities.SynthBound.FlowNode2     .GetByteCount());
            AreEqual(byteCount, entities.SynthBound.ConfigResolver.GetByteCount(entities.SynthBound.SynthWishes));
            
            AreEqual(byteCount, entities.TapeBound.Tape           .GetByteCount());
            AreEqual(byteCount, entities.TapeBound.TapeConfig     .GetByteCount());
            AreEqual(byteCount, entities.TapeBound.TapeActions    .GetByteCount());
            AreEqual(byteCount, entities.TapeBound.TapeAction     .GetByteCount());
            
            AreEqual(byteCount, entities.BuffBound.Buff           .GetByteCount());
            AreEqual(byteCount, entities.BuffBound.AudioFileOutput.GetByteCount());
            AreEqual(byteCount, entities.Independent.Sample       .GetByteCount());
            AreEqual(byteCount, entities.Immutable.WavHeader      .GetByteCount());
            
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.Bits              .GetByteCount());
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.Type              .GetByteCount());
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.SampleDataType    .GetByteCount());
            AreEqual(DefaultSizeOfBitDepth, entities.Immutable.SampleDataTypeEnum.GetByteCount());
            
            foreach (var channelEntities in entities.ChannelEntities)
            {
                AreEqual(byteCount, channelEntities.TapeBound.Tape           .GetByteCount());
                AreEqual(byteCount, channelEntities.TapeBound.TapeConfig     .GetByteCount());
                AreEqual(byteCount, channelEntities.TapeBound.TapeActions    .GetByteCount());
                AreEqual(byteCount, channelEntities.TapeBound.TapeAction     .GetByteCount());
                
                AreEqual(byteCount, channelEntities.BuffBound.Buff           .GetByteCount());
                AreEqual(byteCount, channelEntities.BuffBound.AudioFileOutput.GetByteCount());
                AreEqual(byteCount, channelEntities.Independent.Sample       .GetByteCount());
                AreEqual(byteCount, channelEntities.Immutable.WavHeader      .GetByteCount());

                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.Bits              .GetByteCount());
                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.Type              .GetByteCount());
                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.SampleDataType    .GetByteCount());
                AreEqual(DefaultSizeOfBitDepth, channelEntities.Immutable.SampleDataTypeEnum.GetByteCount());
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
            
            x.SynthBound.SynthWishes      .SetByteCount(value);
            x.SynthBound.FlowNode         .SetByteCount(value);
            x.SynthBound.FlowNode2        .SetByteCount(value);
            x.SynthBound.ConfigResolver   .SetByteCount(value, x.SynthBound.SynthWishes);
            x.TapeBound.Tape              .SetByteCount(value);
            x.TapeBound.TapeConfig        .SetByteCount(value);
            x.TapeBound.TapeActions       .SetByteCount(value);
            x.TapeBound.TapeAction        .SetByteCount(value);
            x.BuffBound.Buff              .SetByteCount(value);
            x.BuffBound.AudioFileOutput   .SetByteCount(value);
            x.Immutable.WavHeader         .SetByteCount(value);
            x.Immutable.SampleDataType    .SetByteCount(sizeOfBitDepthValue, x.SynthBound.Context);
            x.Immutable.SampleDataTypeEnum.SetByteCount(sizeOfBitDepthValue);
            x.Immutable.Bits              .SetByteCount(sizeOfBitDepthValue);
            x.Immutable.Type              .SetByteCount(sizeOfBitDepthValue);
            
            foreach (var channelEntities in x.ChannelEntities)
            {
                channelEntities.TapeBound.Tape              .SetByteCount(value);
                channelEntities.TapeBound.TapeConfig        .SetByteCount(value);
                channelEntities.TapeBound.TapeActions       .SetByteCount(value);
                channelEntities.TapeBound.TapeAction        .SetByteCount(value);
                channelEntities.BuffBound.Buff              .SetByteCount(value);
                channelEntities.BuffBound.AudioFileOutput   .SetByteCount(value);
                channelEntities.Immutable.WavHeader         .SetByteCount(value);
                channelEntities.Immutable.Bits              .SetByteCount(sizeOfBitDepthValue);
                channelEntities.Immutable.Type              .SetByteCount(sizeOfBitDepthValue);
                channelEntities.Immutable.SampleDataType    .SetByteCount(sizeOfBitDepthValue, x.SynthBound.Context);
                channelEntities.Immutable.SampleDataTypeEnum.SetByteCount(sizeOfBitDepthValue);
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
            var courtesyFrames = testCase.CourtesyFrames;
            
            void AssertProp(Action<SynthBoundEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
                
                setter(x.SynthBound);
                
                Assert_SynthBound_Getters (x, value);
                Assert_TapeBound_Getters  (x, init );
                Assert_BuffBound_Getters  (x, init );
                Assert_Independent_Getters(x, init );
                Assert_Immutable_Getters  (x, init );
                Assert_Bitness_Getters    (x, sizeOfBitDepth.Init);
                
                x.Record();
                Assert_All_Getters(x, value, sizeOfBitDepth.Init); // By Design: Entities that represent bit-ness don't change their ByteCount in these tests.
            }

            // Usually ByteCount or SizeOfBitDepth are asserted.
            // But CourtesyFrames is inadvertently being asserted too.
            // Therefore must be kept in sync.
            // (For the other tests CourtesyFrames is immutable.)
            AssertProp(x => { x.SynthWishes   .CourtesyFrames    (courtesyFrames); AreEqual(x.SynthWishes,    x.SynthWishes   .ByteCount    (value)); });
            AssertProp(x => { x.FlowNode      .CourtesyFrames    (courtesyFrames); AreEqual(x.FlowNode,       x.FlowNode      .ByteCount    (value)); });
            AssertProp(x => { x.ConfigResolver.CourtesyFrames    (courtesyFrames); AreEqual(x.ConfigResolver, x.ConfigResolver.ByteCount    (value, x.SynthWishes)); });
            AssertProp(x => { x.SynthWishes   .WithCourtesyFrames(courtesyFrames); AreEqual(x.SynthWishes,    x.SynthWishes   .WithByteCount(value)); });
            AssertProp(x => { x.FlowNode      .WithCourtesyFrames(courtesyFrames); AreEqual(x.FlowNode,       x.FlowNode      .WithByteCount(value)); });
            AssertProp(x => { x.ConfigResolver.WithCourtesyFrames(courtesyFrames); AreEqual(x.ConfigResolver, x.ConfigResolver.WithByteCount(value, x.SynthWishes)); });
            AssertProp(x => { x.SynthWishes   .SetCourtesyFrames (courtesyFrames); AreEqual(x.SynthWishes,    x.SynthWishes   .SetByteCount (value)); });
            AssertProp(x => { x.FlowNode      .SetCourtesyFrames (courtesyFrames); AreEqual(x.FlowNode,       x.FlowNode      .SetByteCount (value)); });
            AssertProp(x => { x.ConfigResolver.SetCourtesyFrames (courtesyFrames); AreEqual(x.ConfigResolver, x.ConfigResolver.SetByteCount (value, x.SynthWishes)); });
            AssertProp(x => { CourtesyFrames    (x.SynthWishes   , courtesyFrames); AreEqual(x.SynthWishes,    ByteCount    (x.SynthWishes   , value)); });
            AssertProp(x => { CourtesyFrames    (x.FlowNode      , courtesyFrames); AreEqual(x.FlowNode,       ByteCount    (x.FlowNode      , value)); });
            AssertProp(x => { CourtesyFrames    (x.ConfigResolver, courtesyFrames); AreEqual(x.ConfigResolver, ByteCount    (x.ConfigResolver, value, x.SynthWishes)); });
            AssertProp(x => { WithCourtesyFrames(x.SynthWishes   , courtesyFrames); AreEqual(x.SynthWishes,    WithByteCount(x.SynthWishes   , value)); });
            AssertProp(x => { WithCourtesyFrames(x.FlowNode      , courtesyFrames); AreEqual(x.FlowNode,       WithByteCount(x.FlowNode      , value)); });
            AssertProp(x => { WithCourtesyFrames(x.ConfigResolver, courtesyFrames); AreEqual(x.ConfigResolver, WithByteCount(x.ConfigResolver, value, x.SynthWishes)); });
            AssertProp(x => { SetCourtesyFrames (x.SynthWishes   , courtesyFrames); AreEqual(x.SynthWishes,    SetByteCount (x.SynthWishes   , value)); });
            AssertProp(x => { SetCourtesyFrames (x.FlowNode      , courtesyFrames); AreEqual(x.FlowNode,       SetByteCount (x.FlowNode      , value)); });
            AssertProp(x => { SetCourtesyFrames (x.ConfigResolver, courtesyFrames); AreEqual(x.ConfigResolver, SetByteCount (x.ConfigResolver, value, x.SynthWishes)); });
            AssertProp(x => { ConfigWishes        .CourtesyFrames    (x.SynthWishes   , courtesyFrames); AreEqual(x.SynthWishes,    ConfigWishes        .ByteCount    (x.SynthWishes   , value)); });
            AssertProp(x => { ConfigWishes        .CourtesyFrames    (x.FlowNode      , courtesyFrames); AreEqual(x.FlowNode,       ConfigWishes        .ByteCount    (x.FlowNode      , value)); });
            AssertProp(x => { ConfigWishesAccessor.CourtesyFrames    (x.ConfigResolver, courtesyFrames); AreEqual(x.ConfigResolver, ConfigWishesAccessor.ByteCount    (x.ConfigResolver, value, x.SynthWishes)); });
            AssertProp(x => { ConfigWishes        .WithCourtesyFrames(x.SynthWishes   , courtesyFrames); AreEqual(x.SynthWishes,    ConfigWishes        .WithByteCount(x.SynthWishes   , value)); });
            AssertProp(x => { ConfigWishes        .WithCourtesyFrames(x.FlowNode      , courtesyFrames); AreEqual(x.FlowNode,       ConfigWishes        .WithByteCount(x.FlowNode      , value)); });
            AssertProp(x => { ConfigWishesAccessor.WithCourtesyFrames(x.ConfigResolver, courtesyFrames); AreEqual(x.ConfigResolver, ConfigWishesAccessor.WithByteCount(x.ConfigResolver, value, x.SynthWishes)); });
            AssertProp(x => { ConfigWishes        .SetCourtesyFrames (x.SynthWishes   , courtesyFrames); AreEqual(x.SynthWishes,    ConfigWishes        .SetByteCount (x.SynthWishes   , value)); });
            AssertProp(x => { ConfigWishes        .SetCourtesyFrames (x.FlowNode      , courtesyFrames); AreEqual(x.FlowNode,       ConfigWishes        .SetByteCount (x.FlowNode      , value)); });
            AssertProp(x => { ConfigWishesAccessor.SetCourtesyFrames (x.ConfigResolver, courtesyFrames); AreEqual(x.ConfigResolver, ConfigWishesAccessor.SetByteCount (x.ConfigResolver, value, x.SynthWishes)); });
            
            if (testCase.AudioLength.Changed)
            {
                AssertProp(x => x.SynthWishes.SetAudioLength(testCase.AudioLength));
            }
            
            if (testCase.FrameCount.Changed)
            {
                AssertProp(x => x.SynthWishes.SetFrameCount(testCase.FrameCount));
            }
            
            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x => x.SynthWishes.SetSamplingRate(testCase.SamplingRate));
            }
            
            if (testCase.Bits.Changed)
            {
                AssertProp(x => x.SynthWishes.SetBits(testCase.Bits));
            }
            
            if (testCase.Channels.Changed)
            {
                AssertProp(x => x.SynthWishes.SetChannels(testCase.Channels));
            }
            
            if (testCase.HeaderLength.Changed)
            {
                AssertProp(x => x.SynthWishes.SetHeaderLength(testCase.HeaderLength));
            }
            
            if (testCase.CourtesyFrames.Changed)
            {
                AssertProp(x => x.SynthWishes.SetCourtesyFrames(testCase.CourtesyFrames));
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

            void AssertProp(Action<TestEntities> setter)
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

            // In theory I'd need set CourtesyFrames too. (See SynthBound_ByteCount comments.)
            // But because everything stays init, it doesn't matter.
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .ByteCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .ByteCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.ByteCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .ByteCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithByteCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetByteCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetByteCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetByteCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetByteCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ByteCount    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ByteCount    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ByteCount    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ByteCount    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithByteCount(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithByteCount(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithByteCount(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithByteCount(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetByteCount (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetByteCount (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetByteCount (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetByteCount (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.ByteCount    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.ByteCount    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.ByteCount    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.ByteCount    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithByteCount(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithByteCount(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithByteCount(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithByteCount(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetByteCount (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetByteCount (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetByteCount (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetByteCount (x.TapeBound.TapeAction , value)));
            
            if (testCase.AudioLength.Changed)
            {
                AssertProp(x => x.TapeBound.Tape.Duration = testCase.AudioLength);
            }
            
            if (testCase.FrameCount.Changed)
            {
                AssertProp(x => x.TapeBound.Tape.SetFrameCount(testCase.FrameCount));
            }
            
            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x => x.TapeBound.TapeConfig.SamplingRate = testCase.SamplingRate);
            }
            
            if (testCase.Bits.Changed)
            {
                AssertProp(x => x.TapeBound.TapeConfig.Bits = testCase.Bits);
            }
            
            if (testCase.Channels.Changed)
            {
                AssertProp(x => x.TapeBound.TapeConfig.Channels = testCase.Channels);
            }
            
            if (testCase.HeaderLength.Changed)
            {
                AssertProp(x => x.TapeBound.TapeConfig.SetHeaderLength(testCase.HeaderLength));
            }
            
            if (testCase.CourtesyFrames.Changed)
            {
                AssertProp(x => x.TapeBound.TapeConfig.CourtesyFrames = testCase.CourtesyFrames);
            }
        }

        [TestMethod] 
        [DynamicData(nameof(Cases))]
        public void BuffBound_ByteCount(string caseKey)
        {
            var testCase = Cases[caseKey];
            int init = testCase.Init;
            int value = testCase.Value;
            var sizeOfBitDepth = testCase.SizeOfBitDepth;

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters     (x, init);
                Assert_TapeBound_Getters      (x, init);
                Assert_Buff_Getters           (x, init); // By Design: Buff's "too buff" to change! FrameCount will be based on bytes!
                Assert_AudioFileOutput_Getters(x, value); // By Design: "Out" will take on new properties when asked.
                Assert_Independent_Getters    (x, init);
                Assert_Immutable_Getters      (x, init);
                Assert_Bitness_Getters        (x, sizeOfBitDepth.Init);

                x.Record();
                Assert_All_Getters(x, init, sizeOfBitDepth.Init);
            }

            // Buff and AudioFileOutput do not have CourtesyFrames,  
            // but ByteCount is determined through AudioLength adjustments.  
            // Since converting AudioLength to ByteCount requires CourtesyFrames, it is passed as a parameter.
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .ByteCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.ByteCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetByteCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetByteCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithByteCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithByteCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ByteCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ByteCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetByteCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetByteCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithByteCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithByteCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.ByteCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.ByteCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetByteCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetByteCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithByteCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithByteCount(x.BuffBound.AudioFileOutput, value)));
            
            // Test nullable and non-nullable courtesyFrames separately.
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .ByteCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.ByteCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetByteCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetByteCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithByteCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithByteCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ByteCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ByteCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetByteCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetByteCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithByteCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithByteCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.ByteCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.ByteCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetByteCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetByteCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithByteCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithByteCount(x.BuffBound.AudioFileOutput, value)));

            
            if (testCase.AudioLength.Changed)
            {
                AssertProp(x => x.BuffBound.Buff           .SetAudioLength(testCase.AudioLength));
                AssertProp(x => x.BuffBound.AudioFileOutput.SetAudioLength(testCase.AudioLength));
            }
            
            if (testCase.FrameCount.Changed)
            {
                AssertProp(x => x.BuffBound.Buff           .SetFrameCount(testCase.FrameCount));
                AssertProp(x => x.BuffBound.AudioFileOutput.SetFrameCount(testCase.FrameCount));
            }

            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x => x.BuffBound.Buff           .SetSamplingRate(testCase.SamplingRate));
                AssertProp(x => x.BuffBound.AudioFileOutput.SetSamplingRate(testCase.SamplingRate));
            }
            
            if (testCase.Bits.Changed)
            {
                AssertProp(x => x.BuffBound.Buff           .Bits(testCase.Bits, x.SynthBound.Context));
                AssertProp(x => x.BuffBound.AudioFileOutput.Bits(testCase.Bits, x.SynthBound.Context));
            }
            
            if (testCase.Channels.Changed)
            {
                AssertProp(x => x.BuffBound.Buff           .Channels(testCase.Channels, x.SynthBound.Context));
                AssertProp(x => x.BuffBound.AudioFileOutput.Channels(testCase.Channels, x.SynthBound.Context));
            }
            
            if (testCase.HeaderLength.Changed)
            {
                AssertProp(x => x.BuffBound.Buff           .HeaderLength(testCase.HeaderLength, x.SynthBound.Context));
                AssertProp(x => x.BuffBound.AudioFileOutput.HeaderLength(testCase.HeaderLength, x.SynthBound.Context));
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(Cases))]
        public void Immutable_ByteCount(string caseKey)
        {
            var testCase = Cases[caseKey];
            int init = testCase.Init;
            int value = testCase.Value;
            var sizeOfBitDepth = testCase.SizeOfBitDepth;

            var x = CreateTestEntities(testCase);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            bool caseHasHeader = testCase.HeaderLength.From.FilledIn() && testCase.HeaderLength.To.FilledIn();
            if (caseHasHeader)
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    Assert_Immutable_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }
                
                AssertProp(() => x.Immutable.WavHeader.ByteCount    (value));
                AssertProp(() => x.Immutable.WavHeader.WithByteCount(value));
                AssertProp(() => x.Immutable.WavHeader.SetByteCount (value));
                AssertProp(() => ByteCount    (x.Immutable.WavHeader, value));
                AssertProp(() => WithByteCount(x.Immutable.WavHeader, value));
                AssertProp(() => SetByteCount (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.ByteCount    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithByteCount(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetByteCount (x.Immutable.WavHeader, value));
                
                if (testCase.FrameCount .Changed) AssertProp(() => x.Immutable.WavHeader.SetFrameCount (testCase.FrameCount));
                if (testCase.AudioLength.Changed) AssertProp(() => x.Immutable.WavHeader.SetAudioLength(testCase.AudioLength));
                if (testCase.Channels   .Changed) AssertProp(() => x.Immutable.WavHeader.SetChannels   (testCase.Channels));
                if (testCase.Bits       .Changed) AssertProp(() => x.Immutable.WavHeader.SetBits       (testCase.Bits));
                
                // Invariant under other property changes.
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
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.ByteCount    (sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.WithByteCount(sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.SetByteCount (sizeOfBitDepth.Value));
                AssertProp(() => ByteCount    (x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Value));
                AssertProp(() => WithByteCount(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Value));
                AssertProp(() => SetByteCount (x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.ByteCount    (x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.WithByteCount(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.SetByteCount (x.Immutable.SampleDataTypeEnum, sizeOfBitDepth.Value));
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
            
                AssertProp(() => x.Immutable.SampleDataType.ByteCount    (sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.WithByteCount(sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.SetByteCount (sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => ByteCount    (x.Immutable.SampleDataType, sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => WithByteCount(x.Immutable.SampleDataType, sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => SetByteCount (x.Immutable.SampleDataType, sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.ByteCount    (x.Immutable.SampleDataType, sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithByteCount(x.Immutable.SampleDataType, sizeOfBitDepth.Value, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetByteCount (x.Immutable.SampleDataType, sizeOfBitDepth.Value, x.SynthBound.Context));
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

                AssertProp(() => x.Immutable.Type.ByteCount    (sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.Type.WithByteCount(sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.Type.SetByteCount (sizeOfBitDepth.Value));
                AssertProp(() => ByteCount    (x.Immutable.Type, sizeOfBitDepth.Value));
                AssertProp(() => WithByteCount(x.Immutable.Type, sizeOfBitDepth.Value));
                AssertProp(() => SetByteCount (x.Immutable.Type, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.ByteCount    (x.Immutable.Type, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.WithByteCount(x.Immutable.Type, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.SetByteCount (x.Immutable.Type, sizeOfBitDepth.Value));
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
            
                AssertProp(() => x.Immutable.Bits.ByteCount      (sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.Bits.WithByteCount  (sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.Bits.SetByteCount   (sizeOfBitDepth.Value));
                AssertProp(() => x.Immutable.Bits.ByteCountToBits(sizeOfBitDepth.Value));
                AssertProp(() => sizeOfBitDepth.Value.Coalesced.ByteCountToBits());
                AssertProp(() => ByteCount      (x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => WithByteCount  (x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => SetByteCount   (x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => ByteCountToBits(x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => ByteCountToBits(sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.ByteCount      (x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.WithByteCount  (x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.SetByteCount   (x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.ByteCountToBits(x.Immutable.Bits, sizeOfBitDepth.Value));
                AssertProp(() => ConfigWishes.ByteCountToBits(sizeOfBitDepth.Value));
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
            var configSection = new TestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultByteCount, () => configSection.ByteCount());
            AreEqual(DefaultByteCount, () => configSection.GetByteCount());
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

        private void Assert_All_Getters(TestEntities x, int byteCount, int sizeOfBitDepth)
        {
            Assert_SynthBound_Getters (x, byteCount);
            Assert_TapeBound_Getters  (x, byteCount);
            Assert_BuffBound_Getters  (x, byteCount);
            Assert_Independent_Getters(x, byteCount);
            Assert_Immutable_Getters  (x, byteCount);
            Assert_Bitness_Getters    (x, sizeOfBitDepth);
        }
        
        private void Assert_SynthBound_Getters(TestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);

            AreEqual(byteCount, () => x.SynthBound.SynthWishes   .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.FlowNode      .ByteCount());
            AreEqual(byteCount, () => x.SynthBound.ConfigResolver.ByteCount(x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => x.SynthBound.SynthWishes   .GetByteCount());
            AreEqual(byteCount, () => x.SynthBound.FlowNode      .GetByteCount());
            AreEqual(byteCount, () => x.SynthBound.ConfigResolver.GetByteCount(x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => ByteCount   (x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => ByteCount   (x.SynthBound.FlowNode));
            AreEqual(byteCount, () => ByteCount   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => GetByteCount(x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => GetByteCount(x.SynthBound.FlowNode));
            AreEqual(byteCount, () => GetByteCount(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => ConfigWishes        .ByteCount   (x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => ConfigWishes        .ByteCount   (x.SynthBound.FlowNode));
            AreEqual(byteCount, () => ConfigWishesAccessor.ByteCount   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => ConfigWishes        .GetByteCount(x.SynthBound.SynthWishes));
            AreEqual(byteCount, () => ConfigWishes        .GetByteCount(x.SynthBound.FlowNode));
            AreEqual(byteCount, () => ConfigWishesAccessor.GetByteCount(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int byteCount)
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
            AreEqual(byteCount, () => x.TapeBound.Tape       .GetByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeConfig .GetByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeActions.GetByteCount());
            AreEqual(byteCount, () => x.TapeBound.TapeAction .GetByteCount());
            AreEqual(byteCount, () => ByteCount   (x.TapeBound.Tape       ));
            AreEqual(byteCount, () => ByteCount   (x.TapeBound.TapeConfig ));
            AreEqual(byteCount, () => ByteCount   (x.TapeBound.TapeActions));
            AreEqual(byteCount, () => ByteCount   (x.TapeBound.TapeAction ));
            AreEqual(byteCount, () => GetByteCount(x.TapeBound.Tape       ));
            AreEqual(byteCount, () => GetByteCount(x.TapeBound.TapeConfig ));
            AreEqual(byteCount, () => GetByteCount(x.TapeBound.TapeActions));
            AreEqual(byteCount, () => GetByteCount(x.TapeBound.TapeAction ));
            AreEqual(byteCount, () => ConfigWishes.ByteCount   (x.TapeBound.Tape       ));
            AreEqual(byteCount, () => ConfigWishes.ByteCount   (x.TapeBound.TapeConfig ));
            AreEqual(byteCount, () => ConfigWishes.ByteCount   (x.TapeBound.TapeActions));
            AreEqual(byteCount, () => ConfigWishes.ByteCount   (x.TapeBound.TapeAction ));
            AreEqual(byteCount, () => ConfigWishes.GetByteCount(x.TapeBound.Tape       ));
            AreEqual(byteCount, () => ConfigWishes.GetByteCount(x.TapeBound.TapeConfig ));
            AreEqual(byteCount, () => ConfigWishes.GetByteCount(x.TapeBound.TapeActions));
            AreEqual(byteCount, () => ConfigWishes.GetByteCount(x.TapeBound.TapeAction ));
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int byteCount)
        {
            Assert_Buff_Getters           (x, byteCount);
            Assert_AudioFileOutput_Getters(x, byteCount);
        }

        private void Assert_AudioFileOutput_Getters(TestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.AudioFileOutput);
            
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.ByteCount());
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.GetByteCount());
            AreEqual(byteCount, ByteCount   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, GetByteCount(x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.ByteCount   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.GetByteCount(x.BuffBound.AudioFileOutput));

            AreEqual(byteCount, x.BuffBound.AudioFileOutput.ByteCount());
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.GetByteCount());
            AreEqual(byteCount, ByteCount   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, GetByteCount(x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.ByteCount   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.GetByteCount(x.BuffBound.AudioFileOutput));
            
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.BytesNeeded());
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.GetBytesNeeded());
            AreEqual(byteCount, BytesNeeded   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, GetBytesNeeded(x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.BytesNeeded   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.GetBytesNeeded(x.BuffBound.AudioFileOutput));
            
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.BytesNeeded());
            AreEqual(byteCount, x.BuffBound.AudioFileOutput.GetBytesNeeded());
            AreEqual(byteCount, BytesNeeded   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, GetBytesNeeded(x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.BytesNeeded   (x.BuffBound.AudioFileOutput));
            AreEqual(byteCount, ConfigWishes.GetBytesNeeded(x.BuffBound.AudioFileOutput));

        }
        
        private void Assert_Buff_Getters(TestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            
            AreEqual(byteCount, x.BuffBound.Buff.ByteCount());
            AreEqual(byteCount, x.BuffBound.Buff.GetByteCount());
            AreEqual(byteCount, ByteCount   (x.BuffBound.Buff));
            AreEqual(byteCount, GetByteCount(x.BuffBound.Buff));
            AreEqual(byteCount, ConfigWishes.ByteCount   (x.BuffBound.Buff));
            AreEqual(byteCount, ConfigWishes.GetByteCount(x.BuffBound.Buff));
            
            AreEqual(byteCount, x.BuffBound.Buff.ByteCount());
            AreEqual(byteCount, x.BuffBound.Buff.GetByteCount());
            AreEqual(byteCount, ByteCount   (x.BuffBound.Buff));
            AreEqual(byteCount, GetByteCount(x.BuffBound.Buff));
            AreEqual(byteCount, ConfigWishes.ByteCount   (x.BuffBound.Buff));
            AreEqual(byteCount, ConfigWishes.GetByteCount(x.BuffBound.Buff));
        }
        
        private void Assert_Independent_Getters(TestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            IsNotNull(() => x.Independent.Sample);
            AreEqual(byteCount, () => x.Independent.Sample.ByteCount   ());
            AreEqual(byteCount, () => x.Independent.Sample.GetByteCount());
            AreEqual(byteCount, () => ByteCount   (x.Independent.Sample));
            AreEqual(byteCount, () => GetByteCount(x.Independent.Sample));
            AreEqual(byteCount, () => ConfigWishes.ByteCount   (x.Independent.Sample));
            AreEqual(byteCount, () => ConfigWishes.GetByteCount(x.Independent.Sample));
        }
        
        private void Assert_Immutable_Getters(TestEntities x, int byteCount)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, byteCount);
        }
                
        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int byteCount)
        {
            if (wavHeader.IsNully()) return;
            AreEqual(byteCount, () => wavHeader.ByteCount   ());
            AreEqual(byteCount, () => wavHeader.GetByteCount());
            AreEqual(byteCount, () => ByteCount   (wavHeader));
            AreEqual(byteCount, () => GetByteCount(wavHeader));
            AreEqual(byteCount, () => ConfigWishes.ByteCount   (wavHeader));
            AreEqual(byteCount, () => ConfigWishes.GetByteCount(wavHeader));
        }

        private void Assert_Bitness_Getters(TestEntities x, int sizeOfBitDepth)
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
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.ByteCount   ());
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.GetByteCount());
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.AsByteCount ());
            AreEqual(sizeOfBitDepth, () => sampleDataTypeEnum.ToByteCount ());
            AreEqual(sizeOfBitDepth, () => ByteCount   (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => GetByteCount(sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => AsByteCount (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ToByteCount (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ByteCount   (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetByteCount(sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsByteCount (sampleDataTypeEnum));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToByteCount (sampleDataTypeEnum));
        }
        
        private void Assert_Bitness_Getters(SampleDataType sampleDataType, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;
            
            IsNotNull(() => sampleDataType);
            AreEqual(sizeOfBitDepth, () => sampleDataType.ByteCount   ());
            AreEqual(sizeOfBitDepth, () => sampleDataType.GetByteCount());
            AreEqual(sizeOfBitDepth, () => sampleDataType.AsByteCount ());
            AreEqual(sizeOfBitDepth, () => sampleDataType.ToByteCount ());
            AreEqual(sizeOfBitDepth, () => ByteCount   (sampleDataType));
            AreEqual(sizeOfBitDepth, () => GetByteCount(sampleDataType));
            AreEqual(sizeOfBitDepth, () => AsByteCount (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ToByteCount (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ByteCount   (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetByteCount(sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsByteCount (sampleDataType));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToByteCount (sampleDataType));
        }
        
        private void Assert_Bitness_Getters(Type type, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;
            IsNotNull(() => type);
            AreEqual(sizeOfBitDepth, () => type.ByteCount      ());
            AreEqual(sizeOfBitDepth, () => type.GetByteCount   ());
            AreEqual(sizeOfBitDepth, () => type.AsByteCount    ());
            AreEqual(sizeOfBitDepth, () => type.ToByteCount    ());
            AreEqual(sizeOfBitDepth, () => type.TypeToByteCount());
            AreEqual(sizeOfBitDepth, () => ByteCount      (type));
            AreEqual(sizeOfBitDepth, () => GetByteCount   (type));
            AreEqual(sizeOfBitDepth, () => AsByteCount    (type));
            AreEqual(sizeOfBitDepth, () => ToByteCount    (type));
            AreEqual(sizeOfBitDepth, () => TypeToByteCount(type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ByteCount      (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetByteCount   (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsByteCount    (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToByteCount    (type));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.TypeToByteCount(type));
        }
        
        private void Assert_Bitness_Getters(int bits, int sizeOfBitDepth)
        {
            if (sizeOfBitDepth.IsNully()) return;
            AreEqual(sizeOfBitDepth, () => bits.ByteCount      ());
            AreEqual(sizeOfBitDepth, () => bits.GetByteCount   ());
            AreEqual(sizeOfBitDepth, () => bits.AsByteCount    ());
            AreEqual(sizeOfBitDepth, () => bits.ToByteCount    ());
            AreEqual(sizeOfBitDepth, () => bits.BitsToByteCount());
            AreEqual(sizeOfBitDepth, () => ByteCount      (bits));
            AreEqual(sizeOfBitDepth, () => GetByteCount   (bits));
            AreEqual(sizeOfBitDepth, () => AsByteCount    (bits));
            AreEqual(sizeOfBitDepth, () => ToByteCount    (bits));
            AreEqual(sizeOfBitDepth, () => BitsToByteCount(bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ByteCount      (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.GetByteCount   (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.AsByteCount    (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.ToByteCount    (bits));
            AreEqual(sizeOfBitDepth, () => ConfigWishes.BitsToByteCount(bits));
        }
    }
}
