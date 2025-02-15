using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Testing;
using JJ.Framework.Wishes.Testing;
using static System.Array;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.ConfigTests.TestEntityEnum;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Testing.AssertHelper_Copied;
using static JJ.Framework.Wishes.Testing.AssertWishes;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Tests.ConfigTests.TestEntities;
// ReSharper disable ArrangeStaticMemberQualifier
#pragma warning disable CS0611
#pragma warning disable MSTEST0018
#pragma warning disable IDE0002

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

        internal class Case : CaseBase<int>
        {
            public override IList<object> KeyElements 
                => new object[] { Name, "~", Descriptor, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, (",", AudioLength, "s"),
                                  ByteCount, Bits, Channels, FrameSize, HeaderLength, ")" };

            // FrameCount: The main property being tested, adjusted directly or via dependencies.
            public CaseProp<int> FrameCount => this;
            
            // SamplingRate: Scales FrameCount
            public CaseProp<int> SamplingRate { get; set; }
            public CaseProp<int> Hertz        { get => SamplingRate; set => SamplingRate = value; }
            public CaseProp<int> Hz           { get => SamplingRate; set => SamplingRate = value; }
            
            // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
            public CaseProp<int> CourtesyFrames { get; set; }
            public CaseProp<int> PlusFrames     { get => CourtesyFrames; set => CourtesyFrames = value; }
            public CaseProp<int> Plus           { get => CourtesyFrames; set => CourtesyFrames = value; }

            // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
            public CaseProp<double> AudioLength { get; set; }
            public CaseProp<double> Length      { get => AudioLength; set => AudioLength = value; }
            public CaseProp<double> sec         { get => AudioLength; set => AudioLength = value; }

            // Additional properties, used in conversion formulae
            public CaseProp<int> ByteCount { get; set; }
            public CaseProp<int> Bits { get; set; }
            public CaseProp<int> Channels { get; set; }
            public CaseProp<int> FrameSize { get; set; }
            public CaseProp<int> HeaderLength { get; set; }

            // Constructors
            
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
            
            public Case() { }
            public Case(int frameCount) : base(frameCount) { }
            public Case(int from, int to) : base(from, to) { }
        }

        // ncrunch: no coverage start

        static CaseCollection<Case> Cases { get; } = new CaseCollection<Case>();
        
        /// <summary> Initializes FrameCount and dependencies, verifies FrameCount values from entities. </summary>
        private static CaseCollection<Case> InitCases { get; } = Cases.FromTemplate(new Case
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
        static CaseCollection<Case> BasicCases { get; } = Cases.FromTemplate(new Case
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
        static CaseCollection<Case> AudioLengthCases { get; } = Cases.FromTemplate(new Case
                                                           
            { Name = "AudioLength", Length = 0.01, Hz = DefaultHz, PlusFrames = 3 },
            
            new Case ( 480+3,  480+3 ) { Length = { To   = 480.0 / Hz } },
            new Case ( 480+3,  960+3 ) { Length = { To   = 960.0 / Hz } },
            new Case ( 480+3,  882+3 ) { Length = { To   = 882.0 / Hz } },
            new Case ( 480+3,  441+3 ) { Length = { To   = 441.0 / Hz } },
            new Case ( 480+3,  220+3 ) { Length = { To   = 220.0 / Hz } },
            new Case ( 480+3,  110+3 ) { Length = { To   = 110.0 / Hz } },
            new Case ( 480+3,    8+3 ) { Length = { To   =   8.0 / Hz } },
            new Case ( 480+3,   16+3 ) { Length = { To   =  16.0 / Hz } },
            new Case ( 480+3,   19+3 ) { Length = { To   =  19.0 / Hz } },
            new Case ( 480+3,   31+3 ) { Length = { To   =  31.0 / Hz } },
            new Case ( 480+3,   61+3 ) { Length = { To   =  61.0 / Hz } },
            new Case ( 480+3,  100+3 ) { Length = { To   = 100.0 / Hz } },
            new Case (   8+3,  480+3 ) { Length = { From =   8.0 / Hz } },
            new Case ( 441+3,  480+3 ) { Length = { From = 441.0 / Hz } },
            new Case ( 110+3,  441+3 ) { Length = { From = 110.0 / Hz, To =  441.0 / Hz } },
            new Case ( 330+3,  441+3 ) { Length = { From = 330.0 / Hz, To =  441.0 / Hz } },
            new Case ( 220+3,  441+3 ) { Length = { From = 220.0 / Hz, To =  441.0 / Hz } }
        );

        /// <summary> SamplingRate varying tests; should adjust FrameCount accordingly. </summary>
        static CaseCollection<Case> SamplingRateCases { get; } = Cases.FromTemplate(new Case
        
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
        static CaseCollection<Case> CourtesyFramesCases { get; } = Cases.FromTemplate(new Case
        
            { Name = "PlusFrames", SamplingRate = 100 },
            
            new Case(102, 103) { PlusFrames = { From = 2, To =  3 }, sec = 1 },
            new Case(203, 204) { PlusFrames = { From = 3, To =  4 }, sec = 2 },
            new Case(305, 304) { PlusFrames = { From = 5, To =  4 }, sec = 3 },
            new Case(402, 410) { PlusFrames = { From = 2, To = 10 }, sec = 4 }
        );
                
        /// <summary> Ensures null Hertz resolves to 48000 Hz and FrameCounts adjust correctly. </summary>
        static CaseCollection<Case> NullyAudioLengthCases { get; }= Cases.FromTemplate(new Case
            
            { Name = "NullyLen", Hz = 480, Plus = 3 },
            
            new Case (480+3)       { Length = { From = (null, 1.0), To = (null, 1.0) } },
            new Case (480+3)       { Length = { From = (null, 1.0), To = 1.0         } },
            new Case (480+3)       { Length = { From = 1.0        , To = (null, 1.0) } },
            new Case (480+3,240+3) { Length = { From = (null, 1.0), To = 0.5         } },
            new Case (240+3,480+3) { Length = { From = 0.5        , To = (null, 1.0) } }
            
            // These cases fail. 0 should not coalesce to 1 sec. 0 means 0 seconds.
            //new Case (480+3)       { Length = { From = 1.0        , To = (0, 1.0)    } },
            //new Case (480+3)       { Length = { From = (null, 1.0), To = (0, 1.0)    } },
            
            // These cases fail. 0 is not nully for AudioLength. 0 means 0 seconds, not to default to 1 second.
            //new Case (480+3)       { Length = { From = (0, 1.0)   , To = 1.0         } },
            //new Case (240+3,480+3) { Length = { From = 0.5        , To = (0, 1.0)    } },
        );        
                
        /// <summary> Ensures null Hertz resolves to 48000 Hz and FrameCounts adjust correctly. </summary>
        static CaseCollection<Case> NullySamplingRateCases { get; } = Cases.FromTemplate(new Case
            
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

        static CaseCollection<Case> NullyCourtesyFramesCases = Cases.FromTemplate(new Case
        
            { Name = "PlusNullies", SamplingRate = 100 },
            
            new Case(104, 104) { sec = 1, CourtesyFrames = { From = (null,4), To = (null,4) } },
            new Case(203, 204) { sec = 2, CourtesyFrames = { From = 3       , To = (null,4) } },
            new Case(304, 305) { sec = 3, CourtesyFrames = { From = (null,4), To = 5        } }
        );

        /// <summary> Nully FrameCount tests check the behavior of coalescing to default. </summary>
        static CaseCollection<Case> NullyFrameCountCases { get; } = Cases.FromTemplate(new Case
            
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
        
        static CaseCollection<Case> ConversionFormulaCases { get; } = Cases.FromTemplate(new Case

            { AudioLength = 0.01, Bits = 32, Channels = 2, FrameSize = 8, Hertz = 50000, Plus = 3, HeaderLength = WavHeaderLength },

            new Case(frameCount:  500+3) { ByteCount = 4000+24 + WavHeaderLength },
            new Case(frameCount:  550+3) { ByteCount = 4400+24 + WavHeaderLength, AudioLength = 0.011 },
            new Case(frameCount: 1000+3) { ByteCount = 8000+24 + WavHeaderLength, Hertz = 100000 },
            new Case(frameCount:  500+5) { ByteCount = 4000+40 + WavHeaderLength, Plus = 5 },
            new Case(frameCount:  500+3) { ByteCount = 2000+12 + WavHeaderLength, Bits = 16,    FrameSize = 4 },
            new Case(frameCount:  500+3) { ByteCount = 2000+12 + WavHeaderLength, Channels = 1, FrameSize = 4 }
            // TODO: Set HeaderLength to 0, but 0 gets overwritten by 44 from the template.
            // Add again when CaseCollection supports clean syntax for multiple templates in a single collection.
        );

        // ncrunch: no coverage end
        
        private TestEntities CreateTestEntities(Case testCase = default)
        {
            testCase = testCase ?? new Case();
            
            return new TestEntities(x =>
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
                {   // ncrunch: no coverage start
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
                    // ncrunch: no coverage end
                }
            });
        }
        
        [TestMethod]
        [DynamicData(nameof(InitCases))]
        public void Init_FrameCount(string caseKey)
        {
            Case testCase = Cases[caseKey];
            var x = CreateTestEntities(testCase);
            Assert_All_Getters(x, testCase);
        }

        [TestMethod]
        [DynamicData(nameof(ConversionFormulaCases))]
        public void ConversionFormula_FrameCount(string caseKey)
        {
            Case   test         = Cases[caseKey];
            int    frameCount   = test.FrameCount;
            double len          = test.AudioLength;
            int    Hz           = test.SamplingRate;
            int    plus         = test.CourtesyFrames;
            int    byteCount    = test.ByteCount;
            int    bits         = test.Bits;
            int    channels     = test.Channels;
            int    frameSize    = FrameSize(bits, channels);
            int    headerLength = test.HeaderLength;

            AreEqual(frameCount, () =>              len.FrameCountFromAudioLength (Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              len.GetFrameCount             (Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              len.ToFrameCount              (Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              len.FrameCount                (Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              FrameCountFromAudioLength(len, Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              GetFrameCount            (len, Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              ToFrameCount             (len, Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              FrameCount               (len, Hz, plus), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCountFromAudioLength(len, Hz, plus), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount            (len, Hz, plus), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.ToFrameCount             (len, Hz, plus), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount               (len, Hz, plus), Tolerance);
            AreEqual(frameCount, () =>              byteCount.FrameCountFromByteCount (bits, channels, headerLength));
            AreEqual(frameCount, () =>              byteCount.GetFrameCount           (bits, channels, headerLength));
            AreEqual(frameCount, () =>              byteCount.ToFrameCount            (bits, channels, headerLength));
            AreEqual(frameCount, () =>              byteCount.FrameCount              (bits, channels, headerLength));
            AreEqual(frameCount, () =>              FrameCountFromByteCount(byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () =>              GetFrameCount          (byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () =>              ToFrameCount           (byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () =>              FrameCount             (byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () => ConfigWishes.FrameCountFromByteCount(byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount          (byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () => ConfigWishes.ToFrameCount           (byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () => ConfigWishes.FrameCount             (byteCount, bits, channels, headerLength));
            AreEqual(frameCount, () =>              byteCount.FrameCountFromByteCount (frameSize,      headerLength));
            AreEqual(frameCount, () =>              byteCount.GetFrameCount           (frameSize,      headerLength));
            AreEqual(frameCount, () =>              byteCount.ToFrameCount            (frameSize,      headerLength));
            AreEqual(frameCount, () =>              byteCount.FrameCount              (frameSize,      headerLength));
            AreEqual(frameCount, () =>              FrameCountFromByteCount(byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () =>              GetFrameCount          (byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () =>              ToFrameCount           (byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () =>              FrameCount             (byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () => ConfigWishes.FrameCountFromByteCount(byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount          (byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () => ConfigWishes.ToFrameCount           (byteCount, frameSize,      headerLength));
            AreEqual(frameCount, () => ConfigWishes.FrameCount             (byteCount, frameSize,      headerLength));
        }


        static object SynthBoundCases => 
            BasicCases // ncrunch: no coverage
            .Concat(AudioLengthCases)
            .Concat(SamplingRateCases)
            .Concat(CourtesyFramesCases)
            .Concat(NullyAudioLengthCases)
            .Concat(NullySamplingRateCases)
            .Concat(NullyCourtesyFramesCases)
            .Concat(NullyFrameCountCases);
        
        [TestMethod] 
        [DynamicData(nameof(SynthBoundCases))]
        public void SynthBound_FrameCount(string caseKey)
        {            
            Case testCase = Cases[caseKey];
            var init  = testCase.From;
            var value = testCase.To;
            
            void AssertProp(Action<TestEntities> setter)
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameCount    (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameCount    (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameCount    (value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithFrameCount(value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetFrameCount (value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    FrameCount    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       FrameCount    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, FrameCount    (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithFrameCount(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithFrameCount(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithFrameCount(x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetFrameCount (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetFrameCount (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetFrameCount (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .FrameCount    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .FrameCount    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.FrameCount    (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithFrameCount(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithFrameCount(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithFrameCount(x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetFrameCount (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetFrameCount (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetFrameCount (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
        
            if (testCase.AudioLength.Changed)
            {
                AssertProp(x => x.SynthBound.SynthWishes   .SetAudioLength(testCase.AudioLength));
                AssertProp(x => x.SynthBound.FlowNode      .SetAudioLength(testCase.AudioLength));
                AssertProp(x => x.SynthBound.ConfigResolver.SetAudioLength(testCase.AudioLength, x.SynthBound.SynthWishes));
            }
        
            if (testCase.SamplingRate.Changed)
            {
                AssertProp(x => x.SynthBound.SynthWishes   .SetSamplingRate(testCase.SamplingRate));
                AssertProp(x => x.SynthBound.FlowNode      .SetSamplingRate(testCase.SamplingRate));
                AssertProp(x => x.SynthBound.ConfigResolver.SetSamplingRate(testCase.SamplingRate));
            }
            
            if (testCase.CourtesyFrames.Changed)
            {
                AssertProp(x => x.SynthBound.SynthWishes   .SetCourtesyFrames(testCase.CourtesyFrames));
                AssertProp(x => x.SynthBound.FlowNode      .SetCourtesyFrames(testCase.CourtesyFrames));
                AssertProp(x => x.SynthBound.ConfigResolver.SetCourtesyFrames(testCase.CourtesyFrames));
            }
        }

        static object TapeBoundCases => Empty<object[]>() // ncrunch: no coverage
            .Concat(BasicCases)
            .Concat(AudioLengthCases)
            .Concat(SamplingRateCases)
            .Concat(CourtesyFramesCases);
        
        [TestMethod] 
        [DynamicData(nameof(TapeBoundCases))]
        public void TapeBound_FrameCount(string caseKey)
        {
            Case testCase = Cases[caseKey];
            int init  = testCase.From;
            int value = testCase.To;

            void AssertProp(Action<TestEntities> setter)
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FrameCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FrameCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FrameCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FrameCount    (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithFrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetFrameCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => FrameCount    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => FrameCount    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => FrameCount    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => FrameCount    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithFrameCount(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithFrameCount(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithFrameCount(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithFrameCount(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetFrameCount (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetFrameCount (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetFrameCount (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetFrameCount (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.FrameCount    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.FrameCount    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.FrameCount    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.FrameCount    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithFrameCount(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithFrameCount(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithFrameCount(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithFrameCount(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetFrameCount (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetFrameCount (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetFrameCount (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetFrameCount (x.TapeBound.TapeAction , value)));
            
            if (testCase.AudioLength   .Changed) AssertProp(x => x.TapeBound.Tape      .Duration       = testCase.AudioLength);
            if (testCase.SamplingRate  .Changed) AssertProp(x => x.TapeBound.TapeConfig.SamplingRate   = testCase.SamplingRate);
            if (testCase.CourtesyFrames.Changed) AssertProp(x => x.TapeBound.TapeConfig.CourtesyFrames = testCase.CourtesyFrames);
        }

        static object BuffBoundCases => Empty<object[]>() // ncrunch: no coverage
            .Concat(BasicCases)
            .Concat(AudioLengthCases)
            .Concat(SamplingRateCases);
        
        [TestMethod] 
        [DynamicData(nameof(BuffBoundCases))]
        public void BuffBound_FrameCount(string caseKey)
        {
            Case testCase = Cases[caseKey];
            int init = testCase.From;
            int value = testCase.To;
            
            void AssertProp(Action<TestEntities> setter)
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

            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .FrameCount    (value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.FrameCount    (value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithFrameCount(value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithFrameCount(value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetFrameCount (value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetFrameCount (value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .FrameCount    (value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.FrameCount    (value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithFrameCount(value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithFrameCount(value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetFrameCount (value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetFrameCount (value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , FrameCount    (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, FrameCount    (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithFrameCount(x.BuffBound.Buff           , value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithFrameCount(x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetFrameCount (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetFrameCount (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , FrameCount    (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, FrameCount    (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithFrameCount(x.BuffBound.Buff           , value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithFrameCount(x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetFrameCount (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetFrameCount (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.FrameCount    (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.FrameCount    (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithFrameCount(x.BuffBound.Buff           , value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithFrameCount(x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetFrameCount (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetFrameCount (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Nully)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.FrameCount    (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.FrameCount    (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithFrameCount(x.BuffBound.Buff           , value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithFrameCount(x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetFrameCount (x.BuffBound.Buff           , value, testCase.CourtesyFrames.Coalesced)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetFrameCount (x.BuffBound.AudioFileOutput, value, testCase.CourtesyFrames.Coalesced)));

            if (testCase.AudioLength .Changed) AssertProp(x => x.BuffBound.AudioFileOutput.Duration     = testCase.AudioLength);
            if (testCase.SamplingRate.Changed) AssertProp(x => x.BuffBound.AudioFileOutput.SamplingRate = testCase.SamplingRate);
        }
        
        static object IndependentCases => Empty<object[]>() // ncrunch: no coverage
            .Concat(BasicCases)
            .Concat(AudioLengthCases)
            .Concat(SamplingRateCases);
        
        [TestMethod] 
        [DynamicData(nameof(IndependentCases))]
        public void Independent_FrameCount(string caseKey)
        {
            // Independent after Taping
            Case testCase = Cases[caseKey];
            int init = testCase.From;
            int value = testCase.To;
         
            void AssertProp(TestEntityEnum change, Action<TestEntities> setter)
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
            
            AssertProp(ForAudioInfoWish, x =>                                             x.Independent.AudioInfoWish.FrameCount = value);
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.FrameCount(value)));
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithFrameCount(value)));
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetFrameCount(value)));
            if (testCase.AudioLength.Changed)
            {
                AssertProp(ForAudioInfoWish, x => x.Independent.AudioInfoWish.SetAudioLength(testCase.AudioLength, testCase.CourtesyFrames));
            }
            
            AssertProp(ForAudioFileInfo, x =>                                             x.Independent.AudioFileInfo.SampleCount = value);
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.FrameCount(value)));
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithFrameCount(value)));
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetFrameCount(value)));
            if (testCase.AudioLength.Changed)
            {
                AssertProp(ForAudioFileInfo, x => x.Independent.AudioFileInfo.SetAudioLength(testCase.AudioLength, testCase.CourtesyFrames));
            }
            
            // SamplingRate does not affect FrameCount in this case.
        }
        
        static object ImmutableCases => // ncrunch: no coverage
            BasicCases
            .Concat(AudioLengthCases)
            .Concat(SamplingRateCases);
        
        [TestMethod] 
        [DynamicData(nameof(ImmutableCases))]
        public void Immutable_FrameCount(string caseKey)
        {
            Case testCase = Cases[caseKey];
            int init = testCase.From;
            int value = testCase.To;
            
            TestEntities x = CreateTestEntities(testCase);

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

                AssertProp(() => x.Immutable.WavHeader.FrameCount(value, testCase.CourtesyFrames));
                AssertProp(() => x.Immutable.WavHeader.WithFrameCount(value, testCase.CourtesyFrames));
                AssertProp(() => x.Immutable.WavHeader.SetFrameCount(value, testCase.CourtesyFrames));
                if (testCase.AudioLength.Changed) AssertProp(() => x.Immutable.WavHeader.SetAudioLength(testCase.AudioLength, testCase.CourtesyFrames));
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
            AreEqual(DefaultFrameCount, () => configSection.GetFrameCount());
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
                    new Case(frameCount:  2) { CourtesyFrames = 2, AudioLength = 0 }), 
                    "Duration is not above 0.");
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int frameCount)
        {
            Assert_Bound_Getters      (x, frameCount);
            Assert_Independent_Getters(x, frameCount);
            Assert_Immutable_Getters  (x, frameCount);
        }

        private void Assert_Bound_Getters(TestEntities x, int frameCount)
        {
            Assert_SynthBound_Getters(x, frameCount);
            Assert_TapeBound_Getters (x, frameCount);
            Assert_BuffBound_Getters (x, frameCount);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int frameCount)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample       , frameCount);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, frameCount);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, frameCount);
        }

        private void Assert_Immutable_Getters(TestEntities x, int frameCount)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, frameCount);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.FrameCount   (x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.GetFrameCount(x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .FrameCount   (x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .FrameCount   (x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => ConfigWishesAccessor.FrameCount   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .GetFrameCount(x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .GetFrameCount(x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => ConfigWishesAccessor.GetFrameCount(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.TapeBound.Tape       .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeConfig .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeActions.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeAction .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.Tape       .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeConfig .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeActions.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeAction .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.Tape       ), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.TapeConfig ), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.TapeActions), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.TapeAction ), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.Tape       ), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.TapeConfig ), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.TapeActions), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.TapeAction ), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.Tape       ), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.TapeConfig ), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.TapeActions), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.TapeAction ), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.Tape       ), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.TapeConfig ), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.TapeActions), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.TapeAction ), Tolerance);
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int frameCount)
        {
            Assert_Buff_Getters           (x, frameCount);
            Assert_AudioFileOutput_Getters(x, frameCount);
        }

        private void Assert_AudioFileOutput_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.FrameCount                 (x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.GetFrameCount              (x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () =>              FrameCount   (x.BuffBound.AudioFileOutput, x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () =>              GetFrameCount(x.BuffBound.AudioFileOutput, x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.BuffBound.AudioFileOutput, x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.BuffBound.AudioFileOutput, x.Immutable.CourtesyFrames), Tolerance);
        }
        
        private void Assert_Buff_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.Buff.FrameCount                 (x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () => x.BuffBound.Buff.GetFrameCount              (x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () =>              FrameCount   (x.BuffBound.Buff, x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () =>              GetFrameCount(x.BuffBound.Buff, x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.BuffBound.Buff, x.Immutable.CourtesyFrames), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.BuffBound.Buff, x.Immutable.CourtesyFrames), Tolerance);
        }
        
        private void Assert_Independent_Getters(Sample sample, int frameCount)
        {
            AreEqual(frameCount, () => sample.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => sample.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (sample), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(sample), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (sample), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(sample), Tolerance);
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int frameCount)
        {
            AreEqual(frameCount, () => audioInfoWish.FrameCount  , Tolerance);
            AreEqual(frameCount, () => audioInfoWish.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => audioInfoWish.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (audioInfoWish), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(audioInfoWish), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (audioInfoWish), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(audioInfoWish), Tolerance);
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int frameCount)
        {
            AreEqual(frameCount, () => audioFileInfo.SampleCount , Tolerance);
            AreEqual(frameCount, () => audioFileInfo.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => audioFileInfo.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (audioFileInfo), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(audioFileInfo), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (audioFileInfo), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(audioFileInfo), Tolerance);
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int frameCount)
        {
            AreEqual(frameCount, () => wavHeader.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => wavHeader.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (wavHeader), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(wavHeader), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (wavHeader), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(wavHeader), Tolerance);
        }
    }        
}