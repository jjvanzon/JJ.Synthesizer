using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Wishes.Common;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Reflection;
using static JJ.Business.Synthesizer.Tests.ConfigTests.FrameCountWishesTests.Cases;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Testing.AssertHelper_Copied;
using static JJ.Framework.Wishes.Testing.AssertWishes;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Business.Synthesizer.Tests.Helpers.DebuggerDisplayFormatter;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class FrameCountWishesTests
    {
        private const int Tolerance = -1;
        private const int DefaultHz = DefaultSamplingRate;
        private const int DefaultHertz = DefaultSamplingRate;

        // ncrunch: no coverage start
        
        private static Case[] _initCases = FromTemplate(new Case
            {
                Name = "Init",
                PlusFrames = 3
            },
            new Case(  9600+3 ),
            new Case(  8820+3 ),
            new Case(  4800+3 ),
            new Case(  4410+3 ),
            new Case(  2205+3 ),
            new Case(  1102+3 ),
            new Case(     8+3 ),
            new Case(    16+3 ),
            new Case(    19+3 ),
            new Case(    31+3 ),
            new Case(    61+3 ),
            new Case(   100+3 ),
            new Case(  1000+3 ),
            new Case(  1003+3 ), 
            new Case(  1234+3 ),
            new Case( 12345+3 ) 
        );

        static Case[] _basicCases = FromTemplate(new Case
            {
                Name = "Basic",
                PlusFrames = 3
            },
            new Case ( 4800+3,  4800+3 ),
            new Case ( 4800+3,  9600+3 ),
            new Case ( 4800+3,  8820+3 ),
            new Case ( 4800+3,  4410+3 ),
            new Case ( 4800+3,  2205+3 ),
            new Case ( 4800+3,  1102+3 ),
            new Case ( 4800+3,     8+3 ),
            new Case ( 4800+3,    16+3 ),
            new Case ( 4800+3,    19+3 ),
            new Case ( 4800+3,    31+3 ),
            new Case ( 4800+3,    61+3 ),
            new Case ( 4800+3,   100+3 ),
            new Case ( 4800+3,  1000+3 ),
            new Case ( 4800+3,  1234+3 ),
            new Case ( 4800+3, 12345+3 ),
            new Case (    8+3,  4800+3 ),
            new Case ( 1102+3,  4410+3 ),
            new Case ( 2205+3,  4410+3 ),
            new Case ( 4410+3,  4800+3 ),
            new Case ( 8820+3,  4410+3 ),
            new Case ( 9600+3,  4800+3 )
        );

        static Case[] _audioLengthCases = FromTemplate(new Case
            {
                Name = "AudioLength",
                sec = { From = 0.1 }, 
                Hz = DefaultHz, 
                PlusFrames = 3
            },
            new Case ( 4800+3,  4800+3 ) { sec = { To =  4800.0 / DefaultHz } },
            new Case ( 4800+3,  9600+3 ) { sec = { To =  9600.0 / DefaultHz } },
            new Case ( 4800+3,  8820+3 ) { sec = { To =  8820.0 / DefaultHz } },
            new Case ( 4800+3,  4410+3 ) { sec = { To =  4410.0 / DefaultHz } },
            new Case ( 4800+3,  2205+3 ) { sec = { To =  2205.0 / DefaultHz } },
            new Case ( 4800+3,  1102+3 ) { sec = { To =  1102.0 / DefaultHz } },
            new Case ( 4800+3,     8+3 ) { sec = { To =     8.0 / DefaultHz } },
            new Case ( 4800+3,    16+3 ) { sec = { To =    16.0 / DefaultHz } },
            new Case ( 4800+3,    19+3 ) { sec = { To =    19.0 / DefaultHz } },
            new Case ( 4800+3,    31+3 ) { sec = { To =    31.0 / DefaultHz } },
            new Case ( 4800+3,    61+3 ) { sec = { To =    61.0 / DefaultHz } },
            new Case ( 4800+3,   100+3 ) { sec = { To =   100.0 / DefaultHz } },
            new Case ( 4800+3,  1000+3 ) { sec = { To =  1000.0 / DefaultHz } },
            new Case ( 4800+3,  1234+3 ) { sec = { To =  1234.0 / DefaultHz } },
            new Case ( 4800+3, 12345+3 ) { sec = { To = 12345.0 / DefaultHz } },
            new Case (    8+3,  4800+3 ) { sec = { To =  4800.0 / DefaultHz } },
            new Case ( 1102+3,  4410+3 ) { sec = { To =  4410.0 / DefaultHz } },
            new Case ( 2205+3,  4410+3 ) { sec = { To =  4410.0 / DefaultHz } },
            new Case ( 4410+3,  4800+3 ) { sec = { To =  4800.0 / DefaultHz } },
            new Case ( 8820+3,  4410+3 ) { sec = { To =  4410.0 / DefaultHz } },
            new Case ( 9600+3,  4800+3 ) { sec = { To =  4800.0 / DefaultHz } }
        );

        static Case[] _samplingRateCases = FromTemplate(new Case
            {
                Name = "SamplingRate",
                sec = 0.1,
                PlusFrames = 3,
                Hertz = { From = 48000 }
            },
            new Case ( 4800+3,  4800+3 ) { Hertz = { To =  48000 } },
            new Case ( 4800+3,  9600+3 ) { Hertz = { To =  96000 } },
            new Case ( 4800+3,  8820+3 ) { Hertz = { To =  88200 } },
            new Case ( 4800+3,  4410+3 ) { Hertz = { To =  44100 } },
            new Case ( 4800+3,  2205+3 ) { Hertz = { To =  22050 } },
            new Case ( 4800+3,  1102+3 ) { Hertz = { To =  11020 } },
            new Case ( 4800+3,     8+3 ) { Hertz = { To =     80 } },
            new Case ( 4800+3,    16+3 ) { Hertz = { To =    160 } },
            new Case ( 4800+3,    19+3 ) { Hertz = { To =    190 } },
            new Case ( 4800+3,    31+3 ) { Hertz = { To =    310 } },
            new Case ( 4800+3,    61+3 ) { Hertz = { To =    610 } },
            new Case ( 4800+3,   100+3 ) { Hertz = { To =   1000 } },
            new Case ( 4800+3,  1000+3 ) { Hertz = { To =  10000 } },
            new Case ( 4800+3,  1234+3 ) { Hertz = { To =  12340 } },
            new Case ( 4800+3, 12345+3 ) { Hertz = { To = 123450 } },
            new Case (    8+3,  4800+3 ) { Hertz = { From =     8, To =   4800 } },
            new Case ( 1102+3,  4410+3 ) { Hertz = { From =  1102, To =   4410 } },
            new Case ( 2205+3,  4410+3 ) { Hertz = { From = 48000, To =  96000 } },
            new Case ( 4410+3,  4800+3 ) { Hertz = { From =  4410, To =   4800 } },
            new Case ( 8820+3,  4410+3 ) { Hertz = { From = 48000, To =  24000 } },
            new Case ( 9600+3,  4800+3 ) { Hertz = { From = 48000, To =  24000 } }
        );

        static Case[] _nullyCases = FromTemplate(
            template: new Case
            {
                Name = "Nully",
                SamplingRate = 48000,
                CourtesyFrames = 3
            },
            new Case(       48000+3 , (null,48000+3) ),
            new Case( (null,48000+3),       48000+3  ) 
        );

        static object[][] InitCaseKeys => _initCases.Select(x => new object[] { x.Descriptor }).ToArray();
        
        static object[][] CaseKeys => _basicCases.Concat(_audioLengthCases)
                                                 .Concat(_samplingRateCases)
                                                 .Select(x => new object[] { x.Descriptor }).ToArray();
        
        static object[][] NullyCaseKeys => _nullyCases.Select(x => new object[] { x.Descriptor }).ToArray();
        
        static object[][] CaseKeysWithNullies => CaseKeys.Concat(NullyCaseKeys).ToArray();
        
        Dictionary<string, Case> _caseDictionary = _basicCases.Concat(_audioLengthCases)
                                                              .Concat(_samplingRateCases)
                                                              .Concat(_nullyCases)
                                                              .Concat(_initCases)
                                                              //.Distinct(x => x.Descriptor)
                                                              .ToDictionary(x => x.Descriptor);
        // ncrunch: no coverage end
        
        [DataTestMethod]
        [DynamicData(nameof(InitCaseKeys))]
        public void Init_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var x = CreateTestEntities(testCase);
            Assert_All_Getters(x, testCase);
        }

        [TestMethod] 
        [DynamicData(nameof(CaseKeysWithNullies))]
        public void SynthBound_FrameCount(string caseKey)
        {            
            Case testCase = _caseDictionary[caseKey];
            var init  = testCase.From;
            var value = testCase.To;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init.Coalesced);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, value.Coalesced);
                Assert_TapeBound_Getters  (x, init .Coalesced);
                Assert_BuffBound_Getters  (x, init .Coalesced);
                Assert_Independent_Getters(x, init .Coalesced);
                Assert_Immutable_Getters  (x, init .Coalesced);
                
                x.Record();
                Assert_All_Getters(x, value.Coalesced);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameCount(value, x.SynthBound.SynthWishes)));
        
            if (testCase.AudioLength.From != testCase.AudioLength.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioLength(testCase.AudioLength, x.SynthBound.SynthWishes)));
                
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioLength(testCase.AudioLength, x.SynthBound.SynthWishes)));
            }
        
            if (testCase.SamplingRate.From != testCase.SamplingRate.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SamplingRate(testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SamplingRate(testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SamplingRate(testCase.SamplingRate)));

                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithSamplingRate(testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithSamplingRate(testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithSamplingRate(testCase.SamplingRate)));
            }
        }

        [TestMethod] 
        [DynamicData(nameof(CaseKeys))]
        public void TapeBound_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init  = testCase.From;
            int value = testCase.To;

            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters  (x, init); // By Design: Tape is too buff to change. FrameCount will be based on buff.
                Assert_BuffBound_Getters  (x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters  (x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FrameCount(value)));

            if (testCase.AudioLength.From != testCase.AudioLength.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,              x.TapeBound.Tape       .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,        x.TapeBound.TapeConfig .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.TapeBound.TapeActions,       x.TapeBound.TapeActions.AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,        x.TapeBound.TapeAction .AudioLength(testCase.AudioLength)));
                AssertProp(x =>                                         x.TapeBound.Tape       .Duration =  testCase.AudioLength);
            }
                    
            if (testCase.SamplingRate.From != testCase.SamplingRate.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,              x.TapeBound.Tape       .SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,        x.TapeBound.TapeConfig .SamplingRate  (testCase.SamplingRate)));
                AssertProp(x =>                                         x.TapeBound.TapeConfig .SamplingRate = testCase.SamplingRate);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions,       x.TapeBound.TapeActions.SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,        x.TapeBound.TapeAction .SamplingRate  (testCase.SamplingRate)));
            }
        }

        [TestMethod] 
        [DynamicData(nameof(CaseKeys))]
        public void BuffBound_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.From;
            int value = testCase.To;
            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters     (x, init );
                Assert_TapeBound_Getters      (x, init );
                Assert_Buff_Getters           (x, init ); // By Design: Buff's "too buff" to change! FrameCount will be based on bytes!
                Assert_AudioFileOutput_Getters(x, value); // By Design: "Out" will take on new properties when asked.
                Assert_Independent_Getters    (x, init );
                Assert_Immutable_Getters      (x, init );
                
                x.Record();
                Assert_All_Getters(x, init);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .FrameCount(value, testCase.CourtesyFrames)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.FrameCount(value, testCase.CourtesyFrames)));

            if (testCase.AudioLength.From != testCase.AudioLength.To)
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .AudioLength(testCase.AudioLength)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.AudioLength(testCase.AudioLength)));
                AssertProp(x =>                                       x.BuffBound.AudioFileOutput.Duration =  testCase.AudioLength);
            }

            if (testCase.SamplingRate.From != testCase.SamplingRate.To)
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SamplingRate  (testCase.SamplingRate)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SamplingRate  (testCase.SamplingRate)));
                AssertProp(x =>                                       x.BuffBound.AudioFileOutput.SamplingRate = testCase.SamplingRate);
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(CaseKeys))]
        public void Independent_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.From;
            int value = testCase.To;
         
            // Independent after Taping

            // AudioInfoWish
            {
                ConfigTestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(testCase);
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

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.FrameCount(value)));
                AssertProp(() =>                                             x.Independent.AudioInfoWish.FrameCount = value);
            
                if (testCase.AudioLength.From != testCase.AudioLength.To)
                {
                    AssertProp(() => AreEqual(x.Independent.AudioInfoWish, x.Independent.AudioInfoWish.AudioLength(testCase.AudioLength, testCase.CourtesyFrames)));
                }
                
                // SamplingRate does not affect FrameCount in this case.
            }
                        
            // AudioFileInfo
            {
                ConfigTestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(testCase);
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

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.FrameCount(value)));
                AssertProp(() =>                                             x.Independent.AudioFileInfo.SampleCount = value);

                if (testCase.AudioLength.From != testCase.AudioLength.To)
                {
                    AssertProp(() => AreEqual(x.Independent.AudioFileInfo, x.Independent.AudioFileInfo.AudioLength(testCase.AudioLength, testCase.CourtesyFrames)));
                }
                
                // SamplingRate does not affect FrameCount in this case.
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(CaseKeys))]
        public void Immutable_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.From;
            int value = testCase.To;
            
            ConfigTestEntities x = CreateTestEntities(testCase);

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

                AssertProp(() => x.Immutable.WavHeader.FrameCount (value, testCase.CourtesyFrames));
                
                if (testCase.AudioLength.From != testCase.AudioLength.To)
                {            
                    AssertProp(() => x.Immutable.WavHeader.AudioLength(testCase.AudioLength, testCase.CourtesyFrames));
                }

                // SamplingRate does not affect FrameCount in this case.
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders.ForEach(w => Assert_Immutable_Getters(w, value));
        }

        [TestMethod]
        public void ConfigSection_FrameCount()
        {
            // Get-only
            var configSection = CreateTestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultFrameCount, () => configSection.FrameCount());
        }

        [TestMethod]
        public void Default_FrameCount()
        {
            AreEqual(DefaultAudioLength * DefaultSamplingRate + DefaultCourtesyFrames, () => DefaultFrameCount);
        }
        
        [TestMethod]
        public void FrameCount_EdgeCases()
        {
            ThrowsException_OrInnerException<Exception>(
                () => CreateTestEntities(
                    new Case(frameCount: -1)), 
                    "FrameCount -1 below 0.");
            
            ThrowsException_OrInnerException<Exception>(
                () => CreateTestEntities(
                    new Case(frameCount:  0) { CourtesyFrames = 2 }), 
                    "FrameCount = 0 but should be a minimum of 2 CourtesyFrames.");
            
            ThrowsException_OrInnerException<Exception>(
                () => CreateTestEntities(
                    new Case(frameCount:  2) { CourtesyFrames = 2 }), 
                    "Duration is not above 0.");
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, int frameCount)
        {
            Assert_Bound_Getters      (x, frameCount);
            Assert_Independent_Getters(x, frameCount);
            Assert_Immutable_Getters  (x, frameCount);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, int frameCount)
        {
            Assert_SynthBound_Getters(x, frameCount);
            Assert_TapeBound_Getters (x, frameCount);
            Assert_BuffBound_Getters (x, frameCount);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, int frameCount)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample       , frameCount);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, frameCount);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, frameCount);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, int frameCount)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, frameCount);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .FrameCount(), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .FrameCount(), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.FrameCount(x.SynthBound.SynthWishes), Tolerance);
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.TapeBound.Tape       .FrameCount(), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeConfig .FrameCount(), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeActions.FrameCount(), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeAction .FrameCount(), Tolerance);
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int frameCount)
        {
            Assert_Buff_Getters           (x, frameCount);
            Assert_AudioFileOutput_Getters(x, frameCount);
        }

        private void Assert_AudioFileOutput_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.FrameCount(x.Immutable.CourtesyFrames), Tolerance);
        }
        
        private void Assert_Buff_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.Buff.FrameCount(x.Immutable.CourtesyFrames), Tolerance);
        }
        
        private void Assert_Independent_Getters(Sample sample, int frameCount)
        {
            AreEqual(frameCount, () => sample.FrameCount(), Tolerance);
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int frameCount)
        {
            AreEqual(frameCount, () => audioInfoWish.FrameCount(), Tolerance);
            AreEqual(frameCount, () => audioInfoWish.FrameCount  , Tolerance);
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int frameCount)
        {
            AreEqual(frameCount, () => audioFileInfo.FrameCount(), Tolerance);
            AreEqual(frameCount, () => audioFileInfo.SampleCount , Tolerance);
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int frameCount)
        {
            AreEqual(frameCount, () => wavHeader.FrameCount(), Tolerance);
        }
 
        // Test Data Helpers
        
        private ConfigTestEntities CreateTestEntities(Case testCase = default)
        {
            testCase = testCase ?? new Case();
            
            return new ConfigTestEntities(x =>
            {
                x.AudioLength(testCase.AudioLength.Init);
                x.SamplingRate(testCase.SamplingRate.Init);
                x.CourtesyFrames(testCase.CourtesyFrames.Init);
                x.Channels(2); // // Sneaky default verifies formula is unaffected.
                x.FrameCount(testCase.FrameCount.Init);
            });
        }
        
        // ncrunch: no coverage start
        
        internal static class Cases
        {
            public static Case[] FromTemplate(Case template, params Case[] cases)
            {
                if (template == null) throw new NullException(() => template);
                return template.CloneTo(cases);
            }
        }
            
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class Case : CaseProp<int>
        {
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;

            public string Name { get; set; }
            
            // FrameCount: The main property being tested, adjusted directly or via dependencies.
            public CaseProp<int> FrameCount => this;
            
            // SamplingRate: Scales FrameCount
            public CaseProp<int> SamplingRate { get; set; } = new CaseProp<int>();
            public CaseProp<int> Hertz { get => SamplingRate; set => SamplingRate = value; }
            public CaseProp<int> Hz    { get => SamplingRate; set => SamplingRate = value; }

            // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
            public CaseProp<double> AudioLength { get; set; } = new CaseProp<double>();
            public CaseProp<double> seconds { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> sec     { get => AudioLength; set => AudioLength = value; }
            
            // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
            public CaseProp<int> CourtesyFrames { get; set; } = new CaseProp<int>();
            public CaseProp<int> PlusFrames { get => CourtesyFrames; set => CourtesyFrames = value; }

            public override string Descriptor
            {
                get 
                {
                    string nameDescriptor = Has(Name) ? Name + " ~ " : "";
                    return $"{nameDescriptor}{base.Descriptor} f ({SamplingRate} Hz + {CourtesyFrames} , {AudioLength} s)"; 
                }
            }
            
            public Case(
                int?    frameCount     = null,
                int?    samplingRate   = null,
                double? audioLength    = null,
                int?    courtesyFrames = null)
            {
                if (frameCount     != null) From = To      = frameCount.Value;
                if (samplingRate   != null) SamplingRate   = samplingRate.Value;
                if (audioLength    != null) AudioLength    = audioLength.Value;
                if (courtesyFrames != null) CourtesyFrames = courtesyFrames.Value;
            }
            
            public Case(int frameCount) : this() => From = To = frameCount;
            public Case(int from, int to) : this() { From = from; To = to; }
             
            public Case(int from, (int? nully, int coalesced) to) : this()
            {
                From         = from;
                To.Nully     = to.nully;
                To.Coalesced = to.coalesced;
            }
            
            public Case((int? nully, int coalesced) from, int to) : this()
            {
                From.Nully     = from.nully;
                From.Coalesced = from.coalesced;
                To             = to;
            }
             
            public Case(int? from, int? to) : this() { From.Nully = from; To.Nully = to; }
            public Case(int? from, int  to) : this() { From.Nully = from; To       = to; }
            public Case(int  from, int? to) : this() { From       = from; To.Nully = to; }

            public Case[] CloneTo(params Case[] cases)
            {
                if (cases == null) throw new NullException(() => cases);
                for (int i = 0; i < cases.Length; i++)
                {
                    if (cases[i] == null) throw new NullException(() => cases[i]);
                    var testCase = cases[i];
                    testCase.Name = Coalesce(testCase.Name, Name);
                    testCase.FrameCount    .CloneFrom(FrameCount    );
                    testCase.SamplingRate  .CloneFrom(SamplingRate  );
                    testCase.AudioLength   .CloneFrom(AudioLength   );
                    testCase.CourtesyFrames.CloneFrom(CourtesyFrames);
                }
                return cases;
            }
        }
        
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class CaseProp<T> where T : struct
        {
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;
            
            public static implicit operator T (CaseProp<T> prop) => prop.To;
            public static implicit operator T?(CaseProp<T> prop) => prop.To;
            public static implicit operator CaseProp<T>(T  value) => new CaseProp<T>(value);
            public static implicit operator CaseProp<T>(T? value) => new CaseProp<T>(value);
            public static implicit operator CaseProp<T>((T from, T to) values) => new CaseProp<T> (values);

            public CaseProp() { }
            public CaseProp(T  value             ) { From = value      ; To = value    ; }
            public CaseProp(T? value             ) { From = value      ; To = value    ; }
            public CaseProp((T from, T to) values) { From = values.from; To = values.to; }
            public CaseProp( T from, T to        ) { From =        from; To =        to; }
                
            /// <inheritdoc cref="docs._from" />
            public NullyPair<T> From   { get; set; } = new NullyPair<T>();
            /// <inheritdoc cref="docs._from" />
            public NullyPair<T> Init   { get => From; set => From = value; }
            /// <inheritdoc cref="docs._from" />
            public NullyPair<T> Source { get => From; set => From = value; }
            
            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> To    { get; set; } = new NullyPair<T>();
            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> Value { get => To; set => To = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> Val { get => To; set => To = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> Dest  { get => To; set => To = value; }

            public T? Nully
            {
                get => To.Nully;
                set => From.Nully = To.Nully = value;
            }
            
            public T Coalesced
            {
                get => To.Coalesced;
                set => From.Coalesced = To.Coalesced = value;
            }

            public virtual string Descriptor
            {
                get
                {
                    string from = $"{From}";
                    string to = $"{To}";
                    
                    // None Filled In
                    if (from.IsNully() && to.IsNully()) return default;
                    
                    // All Equal
                    if (from.Is(to)) return from;
                    
                    // No Change
                    if (Equals(From.Nully, To.Nully) && Equals(From.Coalesced, To.Coalesced))
                    {
                        return $"({Nully},{Coalesced})";
                    }
                    
                    // Mutation
                    return $"{from} => {to}";
                }

            }
        
            public void CloneFrom(CaseProp<T> template)
            {
                if (template == null) throw new NullException(() => template);
                From.Nully     = Coalesce(From.Nully,     template.From.Nully);
                From.Coalesced = Coalesce(From.Coalesced, template.From.Coalesced);
                To.Nully       = Coalesce(To.Nully,       template.To.Nully);
                To.Coalesced   = Coalesce(To.Coalesced,   template.To.Coalesced);
            }
        }
        
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class NullyPair<T> where T : struct
        {
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;

            public T? Nully { get; set; }
            public T Coalesced { get; set; }
            
            public static implicit operator T?(NullyPair<T> pair) => pair.Nully;
            public static implicit operator T (NullyPair<T> pair) => pair.Coalesced;
            public static implicit operator NullyPair<T>(T? value) => new NullyPair<T> { Nully = value };
            public static implicit operator NullyPair<T>(T  value) => new NullyPair<T> { Nully = value, Coalesced = value };
            public static bool operator ==(NullyPair<T> a, NullyPair<T> b) => Equals(a?.Coalesced, b?.Coalesced);
            public static bool operator !=(NullyPair<T> a, NullyPair<T> b) => !Equals(a?.Coalesced, b?.Coalesced);
            public override bool Equals(object obj) => obj is NullyPair<T> other && Equals(Coalesced, other.Coalesced);

            public string Descriptor
            {
                get
                {
                    string nully     = Coalesce(Nully, "");
                    string coalesced = Coalesce(Coalesced, "");
                    if (!Has(nully) && !Has(coalesced)) return "_";
                    if (nully.Is(coalesced)) return nully;
                    if (Has(nully) && !Has(coalesced)) return nully;
                    return $"({nully},{coalesced})";
                }
            }
        }
        
        // ReSharper disable once UnusedMember.Local
        static Case[] _caseExamples = 
        {
            // Example with all values specified
            new Case 
            { 
                FrameCount     = { From = { Nully = 3 * 22050, Coalesced = 3 * 22050 }, To = { Nully = 5 * 22050, Coalesced = 5 * 22050 }}, 
                SamplingRate   = { From = { Nully =     22050, Coalesced =     22050 }, To = { Nully =     22050, Coalesced =     22050 }}, 
                AudioLength    = { From = { Nully = 3        , Coalesced = 3         }, To = { Nully = 5        , Coalesced = 5         }}, 
                CourtesyFrames = { From = { Nully = 4        , Coalesced = 4         }, To = { Nully = 4        , Coalesced = 4         }}, 
            },
            
            // Example with same value for Nully and Coalesced
            new Case 
            { 
                FrameCount     = { From = 22050 * 3 , To = 22050 * 5 },
                SamplingRate   = { From = 22050     , To = 22050     },
                AudioLength    = { From =         3 , To =         5 },
                CourtesyFrames = { From = 4         , To = 4         },
            },
            
            // Example with single mentioning of values that don't change.
            new Case
            { 
                SamplingRate = 22050, CourtesyFrames = 4, 
                AudioLength  = { From = 3         , To = 5         },
                FrameCount   = { From = 3 * 22050 , To = 5 * 22050 }
            },
            
            // Using inherited properties From and To to set main property FrameCount.
            new Case
            { 
                From = 3 * 22050 , To = 5 * 22050,
                AudioLength = { From = 3, To = 5 },
                SamplingRate = 22050, CourtesyFrames = 4, 
            },
            
            // Example using constructor parameters for side-issues
            new Case(courtesyFrames: 4)
            { 
                FrameCount   = { From = 3 * 22050, To = 5 * 22050 },
                AudioLength  = { From = 3        , To = 5         },
                SamplingRate = 22050
            },
            
            // Examples initializing main property in constructor.
            new Case(    48000           ),
            new Case(    22050,     96000),
            new Case(3 * 22050, 5 * 22050) { SamplingRate = 22050, AudioLength = { From = 3, To = 5 } },
            new Case(from: 3 * 22050, to: 5 * 22050) { SamplingRate = 22050, AudioLength = { From = 3, To = 5 } }
        };

        // ncrunch: no coverage end
    } 
}