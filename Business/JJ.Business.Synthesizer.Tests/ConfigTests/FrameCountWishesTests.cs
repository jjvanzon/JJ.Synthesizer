using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Configuration;
using static System.Array;
using static JJ.Business.Synthesizer.Tests.Helpers.Case;
using static JJ.Business.Synthesizer.Tests.ConfigTests.ConfigEntityEnum;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Testing.AssertHelper_Copied;
using static JJ.Framework.Wishes.Testing.AssertWishes;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

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

        /// <summary> Initializes FrameCount and dependencies, verifies FrameCount values from entities. </summary>
        private static Case[] _initCases = FromTemplate(new Case
            {
                Name = "Init",
                PlusFrames = 3,
                Strict = false
            },
            new Case(  960+3 ),
            new Case(  882+3 ),
            new Case(  480+3 ),
            new Case(  441+3 ),
            new Case(  220+3 ),
            new Case(  110+3 ),
            new Case(    8+3 ),
            new Case(   16+3 ),
            new Case(   19+3 ),
            new Case(   31+3 ),
            new Case(   61+3 ),
            new Case(  100+3 ),
            new Case(  103+3 ), 
            new Case(  123+3 ),
            new Case( 1234+3 )
        );

        /// <summary> Varies FrameCount and checks value consistency across entities. </summary>
        static Case[] _basicCases = FromTemplate(new Case
            {
                Name = "Basic",
                PlusFrames = 3,
                Strict = false
            },
            new Case ( 480+3,  960+3 ),
            new Case ( 480+3,  882+3 ),
            new Case ( 480+3,  441+3 ),
            new Case ( 480+3,  220+3 ),
            new Case ( 480+3,  110+3 ),
            new Case ( 480+3,    1+3 ),
            new Case ( 480+3,    3+3 ),
            new Case ( 480+3,    6+3 ),
            new Case ( 480+3,   10+3 ),
            new Case ( 480+3,  100+3 ),
            new Case ( 480+3,  123+3 ),
            new Case (   8+3,  480+3 ),
            new Case ( 110+3,  441+3 ),
            new Case ( 220+3,  441+3 ),
            new Case ( 441+3,  480+3 ),
            new Case ( 882+3,  441+3 ),
            new Case ( 960+3,  480+3 )
        );

        /// <summary> Cases where AudioLength adjustments should change FrameCount accordingly. </summary>
        static Case[] _audioLengthCases = FromTemplate(new Case
                                                           
            { Name = "AudioLength", Length = 0.01, Hz = DefaultHz, PlusFrames = 3 },
            
            new Case ( 480+3,  480+3 ) { Length = { To =  480.0 / Hz } },
            new Case ( 480+3,  960+3 ) { Length = { To =  960.0 / Hz } },
            new Case ( 480+3,  882+3 ) { Length = { To =  882.0 / Hz } },
            new Case ( 480+3,  441+3 ) { Length = { To =  441.0 / Hz } },
            new Case ( 480+3,  220+3 ) { Length = { To =  220.0 / Hz } },
            new Case ( 480+3,  110+3 ) { Length = { To =  110.0 / Hz } },
            new Case ( 480+3,    8+3 ) { Length = { To =    8.0 / Hz } },
            new Case ( 480+3,   16+3 ) { Length = { To =   16.0 / Hz } },
            new Case ( 480+3,   19+3 ) { Length = { To =   19.0 / Hz } },
            new Case ( 480+3,   31+3 ) { Length = { To =   31.0 / Hz } },
            new Case ( 480+3,   61+3 ) { Length = { To =   61.0 / Hz } },
            new Case ( 480+3,  100+3 ) { Length = { To =  100.0 / Hz } },
            
            new Case (   8+3,  480+3 ) { Length = { From =   8.0 / Hz } },
            new Case ( 441+3,  480+3 ) { Length = { From = 441.0 / Hz } },
            
            new Case ( 110+3,  441+3 ) { Length = { From = 110.0 / Hz, To =  441.0 / Hz } },
            new Case ( 330+3,  441+3 ) { Length = { From = 330.0 / Hz, To =  441.0 / Hz } },
            new Case ( 220+3,  441+3 ) { Length = { From = 220.0 / Hz, To =  441.0 / Hz } }
        );

        /// <summary> SamplingRate varying tests; should adjust FrameCount accordingly. </summary>
        static Case[] _samplingRateCases = FromTemplate(new Case
        
            { Name = "SamplingRate", Hz = 48000, sec = 0.01, Plus = 3 },
            
            new Case ( 480+3,  960+3 ) { Hertz = { To =  96000 } },
            new Case ( 480+3,  882+3 ) { Hertz = { To =  88200 } },
            new Case ( 480+3,  441+3 ) { Hertz = { To =  44100 } },
            new Case ( 480+3,  220+3 ) { Hertz = { To =  22000 } },
            new Case ( 480+3,  110+3 ) { Hertz = { To =  11000 } },
            new Case ( 480+3,    8+3 ) { Hertz = { To =    800 } },
            new Case ( 480+3,   16+3 ) { Hertz = { To =   1600 } },
            new Case ( 480+3,   19+3 ) { Hertz = { To =   1900 } },
            new Case ( 480+3,   31+3 ) { Hertz = { To =   3100 } },
            new Case ( 480+3,   61+3 ) { Hertz = { To =   6100 } },
            new Case ( 480+3,   10+3 ) { Hertz = { To =   1000 } },
            new Case ( 480+3,  100+3 ) { Hertz = { To =  10000 } },
            new Case ( 480+3,  123+3 ) { Hertz = { To =  12300 } },
            new Case (   8+3,  480+3 ) { Hertz = { From =  800 } },
            new Case ( 110+3,  441+3 ) { Hertz = { From = 11000, To = 44100 } },
            new Case ( 220+3,  441+3 ) { Hertz = { From = 22000, To = 44100 } },
            new Case ( 441+3,  480+3 ) { Hertz = { From = 44100 } },
            new Case ( 882+3,  441+3 ) { Hertz = { From = 88200, To = 44100 } },
            new Case ( 960+3,  480+3 ) { Hertz = { From = 96000 } }
        );

        /// <summary> Testing courtesy frames' adjustments effect on FrameCount. </summary>
        static Case[] _courtesyFramesCases = FromTemplate(new Case
        
            { Name = "PlusFrames", SamplingRate = 100 },
            
            new Case(102, 103) { PlusFrames = { From = 2, To =  3 }, sec = 1 },
            new Case(203, 204) { PlusFrames = { From = 3, To =  4 }, sec = 2 },
            new Case(305, 304) { PlusFrames = { From = 5, To =  4 }, sec = 3 },
            new Case(402, 410) { PlusFrames = { From = 2, To = 10 }, sec = 4 }
        );
                
        /// <summary> Ensures null Hertz resolves to 48000 Hz and FrameCounts adjust correctly. </summary>
        static Case[] _nullyAudioLengthCases = FromTemplate(new Case
            
            { Name = "NullyLen", Hz = 480, Plus = 3 },
            
            new Case (480+3)       { Length = { From = (null, 1.0), To = (null, 1.0) } },
            new Case (480+3)       { Length = { From = (null, 1.0), To = 1.0         } },
            new Case (480+3)       { Length = { From = 1.0        , To = (null, 1.0) } },
            new Case (480+3,240+3) { Length = { From = (null, 1.0), To = 0.5         } },
            new Case (240+3,480+3) { Length = { From = 0.5        , To = (null, 1.0) } },
            
            // TODO: This case should fail, but do not. 0 should not coalesce to 1 sec. 0 means 0 seconds.
            new Case (480+3)       { Length = { From = 1.0        , To = (0, 1.0)    } },
            new Case (480+3)       { Length = { From = (null, 1.0), To = (0, 1.0)    } }
            
            // These cases fail. 0 is not nully for AudioLength. 0 means 0 seconds, not to default to 1 second.
            //new Case (480+3)       { Length = { From = (0, 1.0)   , To = 1.0         } }
            //new Case (240+3,480+3) { Length = { From = 0.5        , To = (0, 1.0)    } },
        );        
                
        /// <summary> Ensures null Hertz resolves to 48000 Hz and FrameCounts adjust correctly. </summary>
        static Case[] _nullySamplingRateCases = FromTemplate(new Case
            
            { Name = "NullyHz", AudioLength = 0.01, CourtesyFrames = 3 },
            
            new Case (480+3)       { Hz = { From = (null,48000), To = (null,48000) } },
            new Case (480+3)       { Hz = { From = (null,48000), To = 48000        } },
            new Case (480+3)       { Hz = { From = (0,48000)   , To = 48000        } },
            new Case (480+3)       { Hz = { From = 48000       , To = (null,48000) } },
            new Case (480+3)       { Hz = { From = 48000       , To = (0,48000)    } },
            new Case (480+3)       { Hz = { From = (null,48000), To = (0,48000)    } },
            new Case (480+3,240+3) { Hz = { From = (null,48000), To = 24000        } },
            new Case (240+3,480+3) { Hz = { From = 24000       , To = (0,48000)    } }
        );        

        static Case[] _nullyCourtesyFramesCases = FromTemplate(new Case
        
            { Name = "PlusNullies", SamplingRate = 100 },
            
            new Case(104, 104) { sec = 1, CourtesyFrames = { From = (null,4), To = (null,4) } },
            new Case(203, 204) { sec = 2, CourtesyFrames = { From = 3       , To = (null,4) } },
            new Case(304, 305) { sec = 3, CourtesyFrames = { From = (null,4), To = 5        } }
        );

        /// <summary> Nully FrameCount tests check the behavior of coalescing to default. </summary>
        static Case[] _nullyFrameCountCases = FromTemplate(new Case
            
            { Name = "Nully", sec = 1, Hz = 480, Plus = 3 },
            
            // FrameCount null → AudioLength defaults to 1 sec. Then FrameCount calculates to:
            // 4803 = 1 sec (default) * 4800 Hz (specified sampling rate) + 3 courtesy frames
            
            // Basic case of coalescing FrameCounts
            new Case { From = (null,480+3), To= (null,480+3) },
            new Case { From = (null,480+3), To= 480+3  },
            new Case { From = 480+3, To = (null,480+3) },
            
            // FrameCount adjusts AudioLength
            new Case { From = 2403, To = (null,480+3), sec = { From = 5.0 } },
            new Case { From = (null,480+3), To = 2403, sec = { To = 5.0 } },

            // Edge case: Conflicting null/default and explicit AudioLength
            // Invalid: FrameCount cannot be null/default while AudioLength is explicitly set to non-default.
            //new Case ( from: (null,480+3), to: 480+3 ) { Hz = 48000, sec = 0.01 },
            //new Case ( from: 480+3, to: (null,480+3) ) { Hz = 48000, sec = 0.01 },
            
            // FrameCount 0 is not nully. It means 0 seconds. Sort of, but you can't test it:
            
            // You need 3 courtesy frames to make AudioLength 0.
            // FrameCount 0 would make AudioLength -3 frames, resulting in an exception.
            //new Case ( from: (0,480+3), to: 480+3 ),
            //new Case ( from: 480+3, to: (0,480+3) ),

            // FrameCount 3 (courtesy frames) = AudioLength 0 sec.
            // But here the exception is thrown: "Duration is not above 0."
            //new Case ( from: 480+3, to: 3 ) { sec = { To = 0 } },
            //new Case ( from: 3, to: 480+3 ) { sec = { From = 0 } },
            
            // Attempt to stay just above 0. Nope, exception:
            // "Attempt to initialize FrameCount to 4 is inconsistent with FrameCount 3
            // based on initial values for AudioLength (default 1), SamplingRate (4800) and CourtesyFrames (3)."
            //new Case ( from: 4, to: 480+3 ) { sec = { From = 0 } },
            //new Case ( from: 480+3, to: 4 ) { sec = { To = 0 } },

            // Reference case without nullies
            new Case { From = 480+3, To = 480+3, Hz = 48000, sec = 0.01, Name = "NonNully" }
        );

        Dictionary<string, Case> _caseDictionary
            = Empty<Case>().Concat(_basicCases)
                           .Concat(_audioLengthCases)
                           .Concat(_samplingRateCases)
                           .Concat(_courtesyFramesCases)
                           .Concat(_nullyAudioLengthCases)
                           .Concat(_nullySamplingRateCases)
                           .Concat(_nullyCourtesyFramesCases)
                           .Concat(_nullyFrameCountCases)
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
                            .Concat(_nullyAudioLengthCases)
                            .Concat(_nullySamplingRateCases)
                            .Concat(_nullyCourtesyFramesCases)
                            .Concat(_nullyFrameCountCases)
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
            // Independent after Taping
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.From;
            int value = testCase.To;
         
            void AssertProp(ConfigEntityEnum change, Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_Bound_Getters(x, init);
                
                Assert_Independent_Getters(x.Independent.AudioFileInfo, change == ForAudioFileInfo ? value : init);
                Assert_Independent_Getters(x.Independent.AudioInfoWish, change == ForAudioInfoWish ? value : init);
                Assert_Independent_Getters(x.Independent.Sample,        change == ForSample        ? value : init);
                
                Assert_Immutable_Getters(x, init);

                x.Record();
                Assert_All_Getters(x, init);
            }
            
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.FrameCount(value)));
            AssertProp(ForAudioInfoWish, x =>                                             x.Independent.AudioInfoWish.FrameCount = value);
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.FrameCount(value)));
            AssertProp(ForAudioFileInfo, x =>                                             x.Independent.AudioFileInfo.SampleCount = value);

            if (testCase.AudioLength.From != testCase.AudioLength.To)
            {
                AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, x.Independent.AudioInfoWish.AudioLength(testCase.AudioLength, testCase.CourtesyFrames)));
                AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, x.Independent.AudioFileInfo.AudioLength(testCase.AudioLength, testCase.CourtesyFrames)));
            }
            
            // SamplingRate does not affect FrameCount in this case.
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
    }        
}