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
using static System.Array;
using static JJ.Business.Synthesizer.Tests.ConfigTests.FrameCountWishesTests.Case;
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
        private const int Hz = DefaultSamplingRate;
        private const int DefaultHz = DefaultSamplingRate;
        private const int DefaultHertz = DefaultSamplingRate;

        // ncrunch: no coverage start
        
        private static Case[] _initCases = FromTemplate(new Case
            {
                Name = "Init",
                PlusFrames = 3,
                Strict = false
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
                PlusFrames = 3,
                Strict = false
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
                                                           
            { Name = "AudioLength", Length = 0.1, Hz = DefaultHz, PlusFrames = 3 },
            
            new Case ( 4800+3,  4800+3 ) { Length = { To =  4800.0 / Hz } },
            new Case ( 4800+3,  9600+3 ) { Length = { To =  9600.0 / Hz } },
            new Case ( 4800+3,  8820+3 ) { Length = { To =  8820.0 / Hz } },
            new Case ( 4800+3,  4410+3 ) { Length = { To =  4410.0 / Hz } },
            new Case ( 4800+3,  2205+3 ) { Length = { To =  2205.0 / Hz } },
            new Case ( 4800+3,  1102+3 ) { Length = { To =  1102.0 / Hz } },
            new Case ( 4800+3,     8+3 ) { Length = { To =     8.0 / Hz } },
            new Case ( 4800+3,    16+3 ) { Length = { To =    16.0 / Hz } },
            new Case ( 4800+3,    19+3 ) { Length = { To =    19.0 / Hz } },
            new Case ( 4800+3,    31+3 ) { Length = { To =    31.0 / Hz } },
            new Case ( 4800+3,    61+3 ) { Length = { To =    61.0 / Hz } },
            new Case ( 4800+3,   100+3 ) { Length = { To =   100.0 / Hz } },
            new Case ( 4800+3,  1000+3 ) { Length = { To =  1000.0 / Hz } },
            new Case ( 4800+3,  1234+3 ) { Length = { To =  1234.0 / Hz } },
            new Case ( 4800+3, 12345+3 ) { Length = { To = 12345.0 / Hz } },
            
            new Case (    8+3,  4800+3 ) { Length = { From =    8.0 / Hz } },
            new Case ( 4410+3,  4800+3 ) { Length = { From = 4410.0 / Hz } },
            new Case ( 9600+3,  4800+3 ) { Length = { From = 9600.0 / Hz } },
            
            new Case ( 1102+3,  4410+3 ) { Length = { From = 1102.0 / Hz, To =  4410.0 / Hz } },
            new Case ( 2205+3,  4410+3 ) { Length = { From = 2205.0 / Hz, To =  4410.0 / Hz } },
            new Case ( 8820+3,  4410+3 ) { Length = { From = 8820.0 / Hz, To =  4410.0 / Hz } }
        );

        static Case[] _samplingRateCases = FromTemplate(new Case
        
            { Name = "SamplingRate", sec = 0.1, PlusFrames = 3, Hertz = 48000 },
            
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
            new Case (    8+3,  4800+3 ) { Hertz = { From =   80 } },
            new Case ( 1102+3,  4410+3 ) { Hertz = { From = 11020, To = 44100 } },
            new Case ( 2205+3,  4410+3 ) { Hertz = { From = 22050, To = 44100 } },
            new Case ( 4410+3,  4800+3 ) { Hertz = { From = 44100 } },
            new Case ( 8820+3,  4410+3 ) { Hertz = { From = 88200, To = 44100 } },
            new Case ( 9600+3,  4800+3 ) { Hertz = { From = 96000 } }
        );
        
        static Case[] _courtesyFramesCases = FromTemplate(new Case
            {
                Name = "PlusFrames",
                SamplingRate = 1000
            },
            new Case(1002, 1003) { PlusFrames = { From = 2, To =  3 }, sec = 1 },
            new Case(2003, 2004) { PlusFrames = { From = 3, To =  4 }, sec = 2 },
            new Case(3005, 3004) { PlusFrames = { From = 5, To =  4 }, sec = 3 },
            new Case(4002, 4010) { PlusFrames = { From = 2, To = 10 }, sec = 4 }
        );
        
        static Case[] _nullyCases = FromTemplate(new Case
            
            { Name = "Nully", sec = 1, Hz = 4800, Plus = 3 },
            
            // FrameCount null → AudioLength defaults to 1 sec. Then FrameCount calculates to:
            // 4803 = 1 sec (default) * 4800 Hz (specified sampling rate) + 3 courtesy frames
            
            // Basic case of coalescing FrameCounts
            new Case { From = (null,4803), To= 4803  },
            new Case { From = 4803, To = (null,4803) },
            
            // FrameCount adjusts AudioLength
            new Case { From = 48003, To = (null,4803), sec = { From = 10.0 } },
            new Case { From = (null,4803), To = 48003, sec = { To = 10.0 } },

            // Edge case: Conflicting null/default and explicit AudioLength
            // Invalid: FrameCount cannot be null/default while AudioLength is explicitly set to non-default.
            //new Case ( from: (null,4800+3), to: 4800+3 ) { Hz = 48000, sec = 0.1 },
            //new Case ( from: 4800+3, to: (null,4800+3) ) { Hz = 48000, sec = 0.1 },
            
            // FrameCount 0 is not nully. It means 0 seconds. Sort of, but you can't test it:
            
            // You need 3 courtesy frames to make AudioLength 0.
            // FrameCount 0 would make AudioLength -3 frames, resulting in an exception.
            //new Case ( from: (0,4803), to: 4803 ),
            //new Case ( from: 4803, to: (0,4803) ),

            // FrameCount 3 (courtesy frames) = AudioLength 0 sec.
            // But here the exception is thrown: "Duration is not above 0."
            //new Case ( from: 4803, to: 3 ) { sec = { To = 0 } },
            //new Case ( from: 3, to: 4803 ) { sec = { From = 0 } },
            
            // Attempt to stay just above 0. Nope, exception:
            // "Attempt to initialize FrameCount to 4 is inconsistent with FrameCount 3
            // based on initial values for AudioLength (default 1), SamplingRate (4800) and CourtesyFrames (3)."
            //new Case ( from: 4, to: 4803 ) { sec = { From = 0 } },
            //new Case ( from: 4803, to: 4 ) { sec = { To = 0 } },

            // Reference case without nullies
            new Case { From = 4803, To = 4803, Hz = 48000, sec = 0.1, Name = "NonNully" }
        );
        
        static Case[] _nullyHertzCases = FromTemplate(new Case
            
            { Name = "NullyHz", AudioLength = 0.1, CourtesyFrames = 3 },
            
            new Case (4803)      { Hz = { From = (null,48000), To = 48000        } },
            new Case (4803)      { Hz = { From = (0,48000)   , To = 48000        } },
            new Case (4803)      { Hz = { From = 48000       , To = (null,48000) } },
            new Case (4803)      { Hz = { From = 48000       , To = (0,48000)    } },
            new Case (4803)      { Hz = { From = (null,48000), To = (0,48000)    } },
            new Case (4803,2403) { Hz = { From = (null,48000), To = 24000        } },
            new Case (2403,4803) { Hz = { From = 24000       , To = (0,48000)    } }
        );        

        Dictionary<string, Case> _caseDictionary
            = Empty<Case>().Concat(_basicCases)
                           .Concat(_audioLengthCases)
                           .Concat(_samplingRateCases)
                           .Concat(_courtesyFramesCases)
                           .Concat(_nullyCases)
                           .Concat(_nullyHertzCases)
                           .Concat(_initCases)
                           //.Distinct(x => x.Descriptor)
                           .ToDictionary(x => x.Descriptor);
        
        // ncrunch: no coverage end
        
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
                    new Case(frameCount:  2) { CourtesyFrames = 2, AudioLength = 0 }), 
                    "Duration is not above 0.");
        }
        
        static object InitCases => _initCases.Select(x => x.DynamicData);
        
        [TestMethod]
        [DynamicData(nameof(InitCases))]
        public void Init_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var x = CreateTestEntities(testCase);
            Assert_All_Getters(x, testCase);
        }

        static object SynthBoundCases
            => Empty<Case>().Concat(_basicCases)
                            .Concat(_audioLengthCases)
                            .Concat(_samplingRateCases)
                            .Concat(_courtesyFramesCases)
                            .Concat(_nullyCases)
                            .Concat(_nullyHertzCases)
                            .Select(x => x.DynamicData);
        [TestMethod] 
        [DynamicData(nameof(SynthBoundCases))]
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
            
            if (testCase.CourtesyFrames.From != testCase.CourtesyFrames.To)
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .CourtesyFrames(testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .CourtesyFrames(testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.CourtesyFrames(testCase.CourtesyFrames)));

                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithCourtesyFrames(testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithCourtesyFrames(testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithCourtesyFrames(testCase.CourtesyFrames)));
            }
        }

        static object TapeBoundCases
            => Empty<Case>().Concat(_basicCases)
                            .Concat(_audioLengthCases)
                            .Concat(_samplingRateCases)
                            .Concat(_courtesyFramesCases)
                            .Select(x => x.DynamicData);
        [TestMethod] 
        [DynamicData(nameof(TapeBoundCases))]
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
            
            if (testCase.CourtesyFrames.From != testCase.CourtesyFrames.To)
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,              x.TapeBound.Tape       .CourtesyFrames  (testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,        x.TapeBound.TapeConfig .CourtesyFrames  (testCase.CourtesyFrames)));
                AssertProp(x =>                                         x.TapeBound.TapeConfig .CourtesyFrames = testCase.CourtesyFrames);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions,       x.TapeBound.TapeActions.CourtesyFrames  (testCase.CourtesyFrames)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,        x.TapeBound.TapeAction .CourtesyFrames  (testCase.CourtesyFrames)));
            }
        }

        static object BuffBoundCases
            => Empty<Case>().Concat(_basicCases)
                            .Concat(_audioLengthCases)
                            .Concat(_samplingRateCases)
                            .Select(x => x.DynamicData);
        [TestMethod] 
        [DynamicData(nameof(BuffBoundCases))]
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
        
        static object IndependentCases
            => Empty<Case>().Concat(_basicCases)
                            .Concat(_audioLengthCases)
                            .Concat(_samplingRateCases)
                            .Select(x => x.DynamicData);
        [TestMethod] 
        [DynamicData(nameof(IndependentCases))]
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
        
        static object ImmutableCases
            => Empty<Case>().Concat(_basicCases)
                            .Concat(_audioLengthCases)
                            .Concat(_samplingRateCases)
                            .Select(x => x.DynamicData);
        [TestMethod] 
        [DynamicData(nameof(ImmutableCases))]
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
                // Stop tooling configurations for interfering.
                x.IsUnderNCrunch = x.IsUnderAzurePipelines = false;
                
                x.AudioLength(testCase.AudioLength.Init.Nully);
                x.SamplingRate(testCase.SamplingRate.Init.Nully);
                x.CourtesyFrames(testCase.CourtesyFrames.Init.Nully);
                x.Channels(2); // // Sneaky default verifies formula is unaffected.
                
                int frameCountBefore = x.FrameCount();
                x.FrameCount(testCase.FrameCount.Init.Nully);
                int frameCountAfter = x.FrameCount();
                
                if (testCase.Strict && frameCountBefore != frameCountAfter)
                {
                    string formattedFrameCount     = Coalesce(testCase.FrameCount    .Init.Nully, "default " + DefaultFrameCount    );
                    string formattedAudioLength    = Coalesce(testCase.AudioLength   .Init.Nully, "default " + DefaultAudioLength   );
                    string formattedSamplingRate   = Coalesce(testCase.SamplingRate  .Init.Nully, "default " + DefaultSamplingRate  );
                    string formattedCourtesyFrames = Coalesce(testCase.CourtesyFrames.Init.Nully, "default " + DefaultCourtesyFrames);
                    
                    throw new Exception(
                        $"Attempt to initialize {nameof(FrameCount)} to {formattedFrameCount} " +
                        $"is inconsistent with {nameof(FrameCount)} {frameCountBefore} " +
                        $"based on initial values for {nameof(AudioLength)} ({formattedAudioLength}), " +
                        $"SamplingRate ({formattedSamplingRate}) " +
                        $"and {nameof(CourtesyFrames)} ({formattedCourtesyFrames}). " +
                        $"(This restriction can be relaxed by setting {nameof(Case.Strict)} = false in the test {nameof(Case)}.)");
                }
            });
        }
        
        // ncrunch: no coverage start
            
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class Case
        {
            /// <inheritdoc cref="docs._strict />
            public bool Strict { get; set; } = true;
            
            // FrameCount: The main property being tested, adjusted directly or via dependencies.
            public CaseProp<int> FrameCount { get; set; } = new CaseProp<int>();
            public CaseProp<int> Frames { get => FrameCount; set => FrameCount = value; }
            
            // SamplingRate: Scales FrameCount
            public CaseProp<int> SamplingRate { get; set; } = new CaseProp<int>();
            public CaseProp<int> Hertz { get => SamplingRate; set => SamplingRate = value; }
            public CaseProp<int> Hz    { get => SamplingRate; set => SamplingRate = value; }

            // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
            public CaseProp<double> AudioLength { get; set; } = new CaseProp<double>();
            public CaseProp<double> Length   { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> Duration { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> seconds  { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> sec      { get => AudioLength; set => AudioLength = value; }
            
            // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
            public CaseProp<int> CourtesyFrames { get; set; } = new CaseProp<int>();
            public CaseProp<int> PlusFrames { get => CourtesyFrames; set => CourtesyFrames = value; }
            public CaseProp<int> Plus       { get => CourtesyFrames; set => CourtesyFrames = value; }

            // Quasi-Inherited Properties
            
            /// <inheritdoc cref="docs._from" />
            public NullyPair<int> From { get => FrameCount.From; set => FrameCount.From = value; } 
            /// <inheritdoc cref="docs._from" />
            public NullyPair<int> Init { get => FrameCount.Init; set => FrameCount.Init = value; }
            /// <inheritdoc cref="docs._from" />
            public NullyPair<int> Source { get => FrameCount.Source; set => FrameCount.Source = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<int> To { get => FrameCount.To; set => FrameCount.To = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<int> Value { get => FrameCount.Value; set => FrameCount.Value = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<int> Val { get => FrameCount.Val; set => FrameCount.Val = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<int> Dest  { get => FrameCount.Dest; set => FrameCount.Dest = value; }
            public int? Nully { get => FrameCount.Nully; set => FrameCount.Nully = value; }
            public int Coalesced { get => FrameCount.Coalesced; set => FrameCount.Coalesced = value; }

            // Descriptions            
            
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;
            public string Name { get; set; }
            public object[] DynamicData => new object[] { Descriptor };

            public string Descriptor
            {
                get 
                {
                    string name = Has(Name) ? $"{Name} ~ " : "";
                    
                    string frameCount = $"{FrameCount}";
                    frameCount = Has(frameCount) ? $"{frameCount} f " : "";
                    
                    string samplingRate = $"{SamplingRate}";
                    samplingRate = Has(samplingRate) ? $"{samplingRate} Hz " : "";
                    
                    string plusFrames = $"{PlusFrames}";
                    plusFrames = Has(plusFrames) ? $"+ {plusFrames} " : "";
                    
                    string audioLength = $"{AudioLength}";
                    audioLength = Has(audioLength) ? $", {audioLength} s" : "";
                    
                    string braced = samplingRate + plusFrames + audioLength;
                    braced = Has(braced) ? $"({braced.TrimStart(',').Trim()})" : "";
                    
                    return $"{name}{frameCount}{braced}"; 
                }
            }

            // Constructors
            
            public Case() { }
            
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
            
            public Case(int frameCount) => FrameCount = new CaseProp<int>(frameCount);
            public Case(int? frameCount) => FrameCount = new CaseProp<int>(frameCount);
            public Case(int  from, int  to) => FrameCount = new CaseProp<int>(from, to);
            public Case(int  from, int? to) => FrameCount = new CaseProp<int>(from, to);
            public Case(int? from, int  to) => FrameCount = new CaseProp<int>(from, to);
            public Case(int? from, int? to) => FrameCount = new CaseProp<int>(from, to);
            public Case((int  from, int  to) values) => FrameCount = new CaseProp<int>(values);
            public Case((int? from, int  to) values) => FrameCount = new CaseProp<int>(values);
            public Case((int  from, int? to) values) => FrameCount = new CaseProp<int>(values);
            public Case((int? from, int? to) values) => FrameCount = new CaseProp<int>(values);
            public Case(int from, (int? nully, int coalesced) to) => FrameCount = new CaseProp<int>(from, to);
            public Case((int? nully, int coalesced) from, int to) => FrameCount = new CaseProp<int>(from, to);
            public Case((int? nully, int coalesced) from, (int? nully, int coalesced) to) => FrameCount = new CaseProp<int>(from, to);

            // Conversion Operators
            
            public static implicit operator int (Case testCase) => testCase.FrameCount;
            public static implicit operator int?(Case testCase) => testCase.FrameCount;
            
            public static implicit operator Case(int  value) => new Case(value);
            public static implicit operator Case(int? value) => new Case(value);
            public static implicit operator Case((int  from, int  to) values) => new Case(values);
            public static implicit operator Case((int? from, int  to) values) => new Case(values);
            public static implicit operator Case((int  from, int? to) values) => new Case(values);
            public static implicit operator Case((int? from, int? to) values) => new Case(values);
            public static implicit operator Case((int from, (int? nully, int coalesced) to) x) => new Case(x.from, x.to);
            public static implicit operator Case(((int? nully, int coalesced) from, int to) x) =>  new Case(x.from, x.to);
            public static implicit operator Case(((int? nully, int coalesced) from, (int? nully, int coalesced) to) x) => new Case(x.from, x.to);

            // Templating
            
            public static Case[] FromTemplate(Case template, params Case[] cases)
            {
                if (template == null) throw new NullException(() => template);
                return template.CloneTo(cases);
            }

            public Case[] CloneTo(params Case[] cases)
            {
                if (cases == null) throw new NullException(() => cases);
                for (int i = 0; i < cases.Length; i++)
                {
                    if (cases[i] == null) throw new NullException(() => cases[i]);
                    var testCase = cases[i];
                    testCase.Name = Coalesce(testCase.Name, Name);
                    if (Strict == false) testCase.Strict = false; // Yield over alleviation from strictness.
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
            // Properties
            
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
            public NullyPair<T> Val   { get => To; set => To = value; }
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

            // Constructors
            
            public CaseProp() { }
            public CaseProp( T  value      ) { From = value     ; To = value   ; }
            public CaseProp( T? value      ) { From = value     ; To = value   ; }
            public CaseProp( T  from, T  to) { From       = from; To =       to; }
            public CaseProp( T? from, T  to) { From.Nully = from; To       = to; }
            public CaseProp( T  from, T? to) { From       = from; To.Nully = to; }
            public CaseProp( T? from, T? to) { From.Nully = from; To.Nully = to; }
            public CaseProp((T  from, T  to) values) { From = values.from; To = values.to; }
            public CaseProp((T? from, T  to) values) { From = values.from; To = values.to; }
            public CaseProp((T  from, T? to) values) { From = values.from; To = values.to; }
            public CaseProp((T? from, T? to) values) { From = values.from; To = values.to; }
            public CaseProp( T  from, (T? nully, T coalesced) to) { From = from; To = to; }
            public CaseProp((T? nully, T coalesced) from, T to) { To = to; From = from; }
            public CaseProp((T? nully, T coalesced) from, (T? nully, T coalesced) to) { To = to; From = from; }
                        
            // Conversion Operators
            
            public static implicit operator T (CaseProp<T> prop) => prop.To;
            public static implicit operator T?(CaseProp<T> prop) => prop.To;
            public static implicit operator CaseProp<T>(T  value) => new CaseProp<T>(value);
            public static implicit operator CaseProp<T>(T? value) => new CaseProp<T>(value);
            public static implicit operator CaseProp<T>((T  from, T  to) values) => new CaseProp<T>(values);
            public static implicit operator CaseProp<T>((T? from, T  to) values) => new CaseProp<T>(values);
            public static implicit operator CaseProp<T>((T  from, T? to) values) => new CaseProp<T>(values);
            public static implicit operator CaseProp<T>((T? from, T? to) values) => new CaseProp<T>(values);
            public static implicit operator CaseProp<T>((T from, (T? nully, T coalesced) to) x) => new CaseProp<T>(x.from, x.to);
            public static implicit operator CaseProp<T>(((T? nully, T coalesced) from, T to) x) =>  new CaseProp<T>(x.from, x.to);
            public static implicit operator CaseProp<T>(((T? nully, T coalesced) from, (T? nully, T coalesced) to) x) => new CaseProp<T>(x.from, x.to);

            // Descriptions
            
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;

            public string Descriptor
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

            // Templating
        
            public void CloneFrom(CaseProp<T> template)
            {
                if (template == null) throw new NullException(() => template);
                
                // Favor specifically specified values over template values,
                // even though that gives 2 competing meanings to Nully values:
                // either not filled in in the test case or use default value in the API.
                
                From.Nully     = Coalesce(From.Nully,     template.From.Nully);
                From.Coalesced = Coalesce(From.Coalesced, template.From.Coalesced);
                To.Nully       = Coalesce(To.Nully,       template.To.Nully);
                To.Coalesced   = Coalesce(To.Coalesced,   template.To.Coalesced);
            }
        }
        
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class NullyPair<T> where T : struct
        {
            // Properties

            public T? Nully { get; set; }
            public T Coalesced { get; set; }
            
            // Conversion Operators
            
            public static implicit operator T?(NullyPair<T> pair) => pair.Nully;
            public static implicit operator T (NullyPair<T> pair) => pair.Coalesced;
            public static implicit operator NullyPair<T>(T? value) => new NullyPair<T> { Nully = value };
            public static implicit operator NullyPair<T>(T  value) => new NullyPair<T> { Nully = value, Coalesced = value };
            public static implicit operator NullyPair<T>((T? nully, T coalesced) x) => new NullyPair<T> { Nully = x.nully, Coalesced = x.coalesced };

            public static bool operator ==(NullyPair<T> a, NullyPair<T> b) => Equals(a?.Coalesced, b?.Coalesced);
            public static bool operator !=(NullyPair<T> a, NullyPair<T> b) => !Equals(a?.Coalesced, b?.Coalesced);
            public override bool Equals(object obj) => obj is NullyPair<T> other && Equals(Coalesced, other.Coalesced);

            // Descriptions
            
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;

            public string Descriptor
            {
                get
                {
                    if (!Has(Nully) && !Has(Coalesced)) return "";
                    if (Equals(Nully, Coalesced)) return $"{Nully}";
                    if (Has(Nully) && !Has(Coalesced)) return $"{Nully}";
                    return $"({Nully},{Coalesced})";
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