using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using static System.Array;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntityEnum;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Testing.Core.AssertHelperLegacy;
using static JJ.Framework.Testing.Core.AssertHelperCore;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.Core.DeltaDirectionEnum;
using JJ.Framework.Testing.Core;

// ReSharper disable ArrangeStaticMemberQualifier
#pragma warning disable CS0611

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class FrameCountWishesTests
    {
        private const int Tolerance = -1;
        private const int Hz = DefaultSamplingRate;
        private const int DefaultHz = DefaultSamplingRate;

        internal class Case : CaseBase<int>
        {
            public override IList<object> KeyElements 
                => new object[] { Name, "~", FrameCount, "f", "(", SamplingRate, "Hz", "+", CourtesyFrames, (",", AudioLength, "s"),
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
            new Case(  960 ),
            new Case(  882 ),
            new Case(  480 ),
            new Case(  441 ),
            new Case(  220 ),
            new Case(  110 ),
            new Case(    8 ),
            new Case(   16 ),
            new Case(   19 ),
            new Case(   31 ),
            new Case(   61 ),
            new Case(  100 ),
            new Case(  103 ), 
            new Case(  123 ),
            new Case( 1234 )
        );

        /// <summary> Varies FrameCount and checks value consistency across entities. </summary>
        static CaseCollection<Case> BasicCases { get; } = Cases.FromTemplate(new Case
            {
                Name = "Basic",
                PlusFrames = 3,
                Strict = false
            },
            new Case ( 480,  960 ),
            new Case ( 480,  882 ),
            new Case ( 480,  441 ),
            new Case ( 480,  220 ),
            new Case ( 480,  110 ),
            new Case ( 480,    1 ),
            new Case ( 480,    3 ),
            new Case ( 480,    6 ),
            new Case ( 480,   10 ),
            new Case ( 480,  100 ),
            new Case ( 480,  123 ),
            new Case (   8,  480 ),
            new Case ( 110,  441 ),
            new Case ( 220,  441 ),
            new Case ( 441,  480 ),
            new Case ( 882,  441 ),
            new Case ( 960,  480 )
        );

        /// <summary> Cases where AudioLength adjustments should change FrameCount accordingly. </summary>
        static CaseCollection<Case> AudioLengthCases { get; } = Cases.FromTemplate(new Case
                                                           
            { Name = "AudioLength", Length = 0.01, Hz = DefaultHz, PlusFrames = 3 },
            
            new Case ( 480,  480 ) { Length = { To   = 480.0 / Hz } },
            new Case ( 480,  960 ) { Length = { To   = 960.0 / Hz } },
            new Case ( 480,  882 ) { Length = { To   = 882.0 / Hz } },
            new Case ( 480,  441 ) { Length = { To   = 441.0 / Hz } },
            new Case ( 480,  220 ) { Length = { To   = 220.0 / Hz } },
            new Case ( 480,  110 ) { Length = { To   = 110.0 / Hz } },
            new Case ( 480,    8 ) { Length = { To   =   8.0 / Hz } },
            new Case ( 480,   16 ) { Length = { To   =  16.0 / Hz } },
            new Case ( 480,   19 ) { Length = { To   =  19.0 / Hz } },
            new Case ( 480,   31 ) { Length = { To   =  31.0 / Hz } },
            new Case ( 480,   61 ) { Length = { To   =  61.0 / Hz } },
            new Case ( 480,  100 ) { Length = { To   = 100.0 / Hz } },
            new Case (   8,  480 ) { Length = { From =   8.0 / Hz } },
            new Case ( 441,  480 ) { Length = { From = 441.0 / Hz } },
            new Case ( 110,  441 ) { Length = { From = 110.0 / Hz, To =  441.0 / Hz } },
            new Case ( 330,  441 ) { Length = { From = 330.0 / Hz, To =  441.0 / Hz } },
            new Case ( 220,  441 ) { Length = { From = 220.0 / Hz, To =  441.0 / Hz } }
        );

        /// <summary> SamplingRate varying tests; should adjust FrameCount accordingly. </summary>
        static CaseCollection<Case> SamplingRateCases { get; } = Cases.FromTemplate(new Case
        
            { Name = "SamplingRate", Hz = 48000, sec = 0.01, Plus = 3 },
            
            new Case ( 480,  960 ) { Hertz = { To =  96000 } },
            new Case ( 480,  882 ) { Hertz = { To =  88200 } },
            new Case ( 480,  441 ) { Hertz = { To =  44100 } },
            new Case ( 480,  220 ) { Hertz = { To =  22000 } },
            new Case ( 480,  110 ) { Hertz = { To =  11000 } },
            new Case ( 480,    8 ) { Hertz = { To =    800 } },
            new Case ( 480,   16 ) { Hertz = { To =   1600 } },
            new Case ( 480,   19 ) { Hertz = { To =   1900 } },
            new Case ( 480,   31 ) { Hertz = { To =   3100 } },
            new Case ( 480,   61 ) { Hertz = { To =   6100 } },
            new Case ( 480,   10 ) { Hertz = { To =   1000 } },
            new Case ( 480,  100 ) { Hertz = { To =  10000 } },
            new Case ( 480,  123 ) { Hertz = { To =  12300 } },
            new Case (   8,  480 ) { Hertz = { From =  800 } },
            new Case ( 110,  441 ) { Hertz = { From = 11000, To = 44100 } },
            new Case ( 220,  441 ) { Hertz = { From = 22000, To = 44100 } },
            new Case ( 441,  480 ) { Hertz = { From = 44100 } },
            new Case ( 882,  441 ) { Hertz = { From = 88200, To = 44100 } },
            new Case ( 960,  480 ) { Hertz = { From = 96000 } }
        );

        /// <summary> Testing courtesy frames' adjustments effect on FrameCount. </summary>
        static CaseCollection<Case> CourtesyFramesCases { get; } = Cases.FromTemplate(new Case
        
            { Name = "PlusFrames", SamplingRate = 100 },
            
            new Case(100, 100) { PlusFrames = { From = 2, To =  3 }, sec = 1 },
            new Case(200, 200) { PlusFrames = { From = 3, To =  4 }, sec = 2 },
            new Case(300, 300) { PlusFrames = { From = 5, To =  4 }, sec = 3 },
            new Case(400, 400) { PlusFrames = { From = 2, To = 10 }, sec = 4 }
        );
                
        /// <summary> Ensures null Hertz resolves to 48000 Hz and FrameCounts adjust correctly. </summary>
        static CaseCollection<Case> NullyAudioLengthCases { get; }= Cases.FromTemplate(new Case
            
            { Name = "NullyLen", Hz = 480, Plus = 3 },
            
            new Case (480)     { Length = { From = (null, 1.0), To = (null, 1.0) } },
            new Case (480)     { Length = { From = (null, 1.0), To = 1.0         } },
            new Case (480)     { Length = { From = 1.0        , To = (null, 1.0) } },
            new Case (480,240) { Length = { From = (null, 1.0), To = 0.5         } },
            new Case (240,480) { Length = { From = 0.5        , To = (null, 1.0) } }
            
            // These cases fail. 0 should not coalesce to 1 sec. 0 means 0 seconds.
            //new Case (480)     { Length = { From = 1.0        , To = (0, 1.0)    } },
            //new Case (480)     { Length = { From = (null, 1.0), To = (0, 1.0)    } },
            
            // These cases fail. 0 is not nully for AudioLength. 0 means 0 seconds, not to default to 1 second.
            //new Case (480)     { Length = { From = (0, 1.0)   , To = 1.0         } },
            //new Case (240,480) { Length = { From = 0.5        , To = (0, 1.0)    } },
        );        
                
        /// <summary> Ensures null Hertz resolves to 48000 Hz and FrameCounts adjust correctly. </summary>
        static CaseCollection<Case> NullySamplingRateCases { get; } = Cases.FromTemplate(new Case
            
            { Name = "NullyHz", AudioLength = 0.01, CourtesyFrames = 3 },
            
            new Case (480)     { Hz = { From = (null,48000), To = (null,48000) } },
            new Case (480)     { Hz = { From = (null,48000), To = 48000        } },
            new Case (480)     { Hz = { From = (0,48000)   , To = 48000        } },
            new Case (480)     { Hz = { From = 48000       , To = (null,48000) } },
            new Case (480)     { Hz = { From = 48000       , To = (0,48000)    } },
            new Case (480)     { Hz = { From = (null,48000), To = (0,48000)    } },
            new Case (480,240) { Hz = { From = (null,48000), To = 24000        } },
            new Case (240,480) { Hz = { From = 24000       , To = (0,48000)    } }
        );        

        static CaseCollection<Case> NullyCourtesyFramesCases = Cases.FromTemplate(new Case
        
            { Name = "PlusNullies", SamplingRate = 100 },
            
            new Case(100, 100) { sec = 1, CourtesyFrames = { From = (null,4), To = (null,4) } },
            new Case(200, 200) { sec = 2, CourtesyFrames = { From = 3       , To = (null,4) } },
            new Case(300, 300) { sec = 3, CourtesyFrames = { From = (null,4), To = 5        } }
        );

        /// <summary> Nully FrameCount tests check the behavior of coalescing to default. </summary>
        static CaseCollection<Case> NullyFrameCountCases { get; } = Cases.FromTemplate(new Case
            
            { Name = "Nully", sec = 1, Hz = 480, Plus = 3 },
            
            // FrameCount null → AudioLength defaults to 1 sec. Then FrameCount calculates to:
            // 4803 = 1 sec (default) * 4800 Hz (specified sampling rate)
            
            // Basic case of coalescing FrameCounts
            new Case { From = (null,480), To= (null,480) },
            new Case { From = (null,480), To= 480  },
            new Case { From = 480, To = (null,480) },
            
            // FrameCount adjusts AudioLength
            new Case { From = 2400, To = (null,480), sec = { From = 5.0 } },
            new Case { From = (null,480), To = 2400, sec = { To = 5.0 } },

            // Edge case: Conflicting null/default and explicit AudioLength
            // Invalid: FrameCount cannot be null/default while AudioLength is explicitly set to non-default.
            //new Case ( from: (null,480), to: 480 ) { Hz = 48000, sec = 0.01 },
            //new Case ( from: 480, to: (null,480) ) { Hz = 48000, sec = 0.01 },

            // Reference case without nullies
            new Case { From = 480, To = 480, Hz = 48000, sec = 0.01, Name = "NonNully" }
        );
        
        static CaseCollection<Case> ConversionFormulaCases { get; } = Cases.FromTemplate(new Case

            { AudioLength = 0.01, Bits = 32, Channels = 2, FrameSize = 8, Hertz = 50000, Plus = 3, HeaderLength = WavHeaderLength },

            new Case(frameCount:  500) { ByteCount = 4000 + WavHeaderLength },
            new Case(frameCount:  550) { ByteCount = 4400 + WavHeaderLength, AudioLength = 0.011 },
            new Case(frameCount: 1000) { ByteCount = 8000 + WavHeaderLength, Hertz = 100000 },
            new Case(frameCount:  500) { ByteCount = 4000 + WavHeaderLength, Plus = 5 },
            new Case(frameCount:  500) { ByteCount = 2000 + WavHeaderLength, Bits = 16,    FrameSize = 4 },
            new Case(frameCount:  500) { ByteCount = 2000 + WavHeaderLength, Channels = 1, FrameSize = 4 }
            // TODO: Set HeaderLength to 0, but 0 gets overwritten by 44 from the template.
            // Add again when CaseCollection supports clean syntax for multiple templates in a single collection.
        );

        // ncrunch: no coverage end
        
        private TestEntities CreateTestEntities(Case test = default, [CallerMemberName] string name = null)
        {
            test = test ?? new Case();
            
            return new TestEntities(name, x =>
            {
                // Stop tooling configurations for interfering.
                x.IsUnderNCrunch = x.IsUnderAzurePipelines = false;
                
                x.NoLog();
                x.AudioLength(test.AudioLength.Init.Nully);
                x.SamplingRate(test.SamplingRate.Init.Nully);
                x.CourtesyFrames(test.CourtesyFrames.Init.Nully);
                x.Channels(2); // // Sneaky default verifies FrameCount is unaffected.
                
                int frameCountBefore = x.FrameCount();
                x.FrameCount(test.FrameCount.Init.Nully);
                int frameCountAfter = x.FrameCount();
                
                if (test.Strict && frameCountBefore != frameCountAfter)
                {   // ncrunch: no coverage start
                    string formattedFrameCount     = CoalesceFrameCount    (test.FrameCount    .Init.Nully, "default " + DefaultFrameCount    );
                    string formattedAudioLength    = CoalesceAudioLength   (test.AudioLength   .Init.Nully, "default " + DefaultAudioLength   );
                    string formattedSamplingRate   = CoalesceSamplingRate  (test.SamplingRate  .Init.Nully, "default " + DefaultSamplingRate  );
                    string formattedCourtesyFrames = CoalesceCourtesyFrames(test.CourtesyFrames.Init.Nully, "default " + DefaultCourtesyFrames);
                    
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
            Case test = Cases[caseKey];
            var x = CreateTestEntities(test);
            Assert_All_Getters(x, test, test.CourtesyFrames);
        }

        [TestMethod]
        [DynamicData(nameof(ConversionFormulaCases))]
        public void ConversionFormula_FrameCount(string caseKey)
        {
            Case   test         = Cases[caseKey];
            int    frameCount   = test.FrameCount;
            double len          = test.AudioLength;
            int    Hz           = test.SamplingRate;
            int    byteCount    = test.ByteCount;
            int    bits         = test.Bits;
            int    channels     = test.Channels;
            int    frameSize    = FrameSize(bits, channels);
            int    headerLength = test.HeaderLength;

            AreEqual(frameCount, () =>              len.FrameCountFromAudioLength (Hz), Tolerance);
            AreEqual(frameCount, () =>              len.GetFrameCount             (Hz), Tolerance);
            AreEqual(frameCount, () =>              len.ToFrameCount              (Hz), Tolerance);
            AreEqual(frameCount, () =>              len.FrameCount                (Hz), Tolerance);
            AreEqual(frameCount, () =>              FrameCountFromAudioLength(len, Hz), Tolerance);
            AreEqual(frameCount, () =>              GetFrameCount            (len, Hz), Tolerance);
            AreEqual(frameCount, () =>              ToFrameCount             (len, Hz), Tolerance);
            AreEqual(frameCount, () =>              FrameCount               (len, Hz), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCountFromAudioLength(len, Hz), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount            (len, Hz), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.ToFrameCount             (len, Hz), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount               (len, Hz), Tolerance);
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
            Case test = Cases[caseKey];
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(test);
                Assert_All_Getters(x, test.Init.Coalesced, test.PlusFrames.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, test.To.Coalesced);
                Assert_TapeBound_Getters  (x, test.Init.Coalesced, test.PlusFrames.Init);
                Assert_BuffBound_Getters  (x, test.Init.Coalesced, test.PlusFrames.Init);
                Assert_Independent_Getters(x, test.Init.Coalesced, test.PlusFrames.Init);
                Assert_Immutable_Getters  (x, test.Init.Coalesced);
                
                x.Record();
                Assert_All_Getters(x, test.To.Coalesced, test.PlusFrames.To);
            }
                    
            if (test.AudioLength.Changed)
            {
                AssertProp(x => x.SynthBound.SynthWishes   .SetAudioLength(test.AudioLength));
                AssertProp(x => x.SynthBound.FlowNode      .SetAudioLength(test.AudioLength));
                AssertProp(x => x.SynthBound.ConfigResolver.SetAudioLength(test.AudioLength, x.SynthBound.SynthWishes));
                return;
            }
        
            if (test.SamplingRate.Changed)
            {
                AssertProp(x => x.SynthBound.SynthWishes   .SetSamplingRate(test.SamplingRate));
                AssertProp(x => x.SynthBound.FlowNode      .SetSamplingRate(test.SamplingRate));
                AssertProp(x => x.SynthBound.ConfigResolver.SetSamplingRate(test.SamplingRate));
                return;
            }
            
            if (test.CourtesyFrames.Changed)
            {
                AssertProp(x => x.SynthBound.SynthWishes   .SetCourtesyFrames(test.CourtesyFrames));
                AssertProp(x => x.SynthBound.FlowNode      .SetCourtesyFrames(test.CourtesyFrames));
                AssertProp(x => x.SynthBound.ConfigResolver.SetCourtesyFrames(test.CourtesyFrames));
                return;
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameCount    (test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameCount    (test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameCount    (test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithFrameCount(test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithFrameCount(test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithFrameCount(test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetFrameCount (test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetFrameCount (test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetFrameCount (test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    FrameCount    (x.SynthBound.SynthWishes   , test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       FrameCount    (x.SynthBound.FlowNode      , test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, FrameCount    (x.SynthBound.ConfigResolver, test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithFrameCount(x.SynthBound.SynthWishes   , test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithFrameCount(x.SynthBound.FlowNode      , test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithFrameCount(x.SynthBound.ConfigResolver, test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetFrameCount (x.SynthBound.SynthWishes   , test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetFrameCount (x.SynthBound.FlowNode      , test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetFrameCount (x.SynthBound.ConfigResolver, test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .FrameCount    (x.SynthBound.SynthWishes   , test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .FrameCount    (x.SynthBound.FlowNode      , test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.FrameCount    (x.SynthBound.ConfigResolver, test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithFrameCount(x.SynthBound.SynthWishes   , test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithFrameCount(x.SynthBound.FlowNode      , test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithFrameCount(x.SynthBound.ConfigResolver, test, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetFrameCount (x.SynthBound.SynthWishes   , test)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetFrameCount (x.SynthBound.FlowNode      , test)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetFrameCount (x.SynthBound.ConfigResolver, test, x.SynthBound.SynthWishes)));
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
            Case test = Cases[caseKey];
            int init  = test.From;
            int value = test.To;

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(test);
                Assert_All_Getters(x, init, test.PlusFrames.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, init);
                Assert_TapeBound_Getters  (x, init, test.PlusFrames.Init); // By Design: Tape is too buff to change. FrameCount will be based on buff.
                Assert_BuffBound_Getters  (x, init, test.PlusFrames.Init);
                Assert_Independent_Getters(x, init, test.PlusFrames.Init);
                Assert_Immutable_Getters  (x, init);
                
                x.Record();
                Assert_All_Getters(x, init, test.CourtesyFrames.Init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
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
            
            if (test.AudioLength   .Changed) AssertProp(x => x.TapeBound.Tape      .Duration       = test.AudioLength);
            if (test.SamplingRate  .Changed) AssertProp(x => x.TapeBound.TapeConfig.SamplingRate   = test.SamplingRate);
            if (test.CourtesyFrames.Changed) AssertProp(x => x.TapeBound.TapeConfig.CourtesyFrames = test.CourtesyFrames);
        }

        static object BuffBoundCases => Empty<object[]>() // ncrunch: no coverage
            .Concat(BasicCases)
            .Concat(AudioLengthCases)
            .Concat(SamplingRateCases);
        
        [TestMethod] 
        [DynamicData(nameof(BuffBoundCases))]
        public void BuffBound_FrameCount(string caseKey)
        {
            Case test = Cases[caseKey];
            int init = test.From;
            int value = test.To;
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(test);
                Assert_All_Getters(x, init, test.PlusFrames.Init);
                
                setter(x);
                
                Assert_SynthBound_Getters     (x, init );
                Assert_TapeBound_Getters      (x, init, test.PlusFrames.Init);
                Assert_Buff_Getters           (x, init, test.PlusFrames.Init); // By Design: Buff's "too buff" to change! FrameCount will be based on bytes!
                Assert_AudioFileOutput_Getters(x, value); // By Design: "Out" will take on new properties when asked.
                Assert_Independent_Getters    (x, init, test.PlusFrames.Init);
                Assert_Immutable_Getters      (x, init );
                
                x.Record();
                Assert_All_Getters(x, init, test.CourtesyFrames.Init);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .FrameCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.FrameCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithFrameCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetFrameCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .FrameCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.FrameCount    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .WithFrameCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.WithFrameCount(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , x.BuffBound.Buff           .SetFrameCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.SetFrameCount (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , FrameCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, FrameCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithFrameCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithFrameCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetFrameCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetFrameCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , FrameCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, FrameCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , WithFrameCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, WithFrameCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , SetFrameCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, SetFrameCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.FrameCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.FrameCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithFrameCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithFrameCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetFrameCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetFrameCount (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.FrameCount    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.FrameCount    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.WithFrameCount(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.WithFrameCount(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff           , ConfigWishes.SetFrameCount (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, ConfigWishes.SetFrameCount (x.BuffBound.AudioFileOutput, value)));

            if (test.AudioLength .Changed) AssertProp(x => x.BuffBound.AudioFileOutput.Duration     = test.AudioLength);
            if (test.SamplingRate.Changed) AssertProp(x => x.BuffBound.AudioFileOutput.SamplingRate = test.SamplingRate);
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
            Case test = Cases[caseKey];
            int init = test.From;
            int value = test.To;
         
            void AssertProp(TestEntityEnum entity, Action<TestEntities> setter)
            {
                var x = CreateTestEntities(test);
                Assert_All_Getters(x, init, test.PlusFrames.Init);
                
                setter(x);
                
                Assert_Bound_Getters(x, init, test.PlusFrames.Init);
                
                Assert_Independent_Getters(x.Independent.AudioFileInfo, entity == ForAudioFileInfo ? value : init);
                Assert_Independent_Getters(x.Independent.AudioInfoWish, entity == ForAudioInfoWish ? value : init);
                Assert_Independent_Getters(x.Independent.Sample,        entity == ForSample        ? value : init, test.PlusFrames);
                
                Assert_Immutable_Getters(x, init);

                x.Record();
                Assert_All_Getters(x, init, test.PlusFrames.Init);
            }
            
            AssertProp(ForAudioInfoWish, x =>                                             x.Independent.AudioInfoWish.FrameCount = value);
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.FrameCount(value)));
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithFrameCount(value)));
            AssertProp(ForAudioInfoWish, x => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetFrameCount(value)));
            if (test.AudioLength.Changed)
            {
                AssertProp(ForAudioInfoWish, x => x.Independent.AudioInfoWish.SetAudioLength(test.AudioLength));
            }
            
            AssertProp(ForAudioFileInfo, x =>                                             x.Independent.AudioFileInfo.SampleCount = value);
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.FrameCount(value)));
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithFrameCount(value)));
            AssertProp(ForAudioFileInfo, x => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetFrameCount(value)));
            if (test.AudioLength.Changed)
            {
                AssertProp(ForAudioFileInfo, x => x.Independent.AudioFileInfo.SetAudioLength(test.AudioLength));
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
            Case test = Cases[caseKey];
            int init = test.From;
            int value = test.To;
            
            TestEntities x = CreateTestEntities(test);

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

                AssertProp(() => x.Immutable.WavHeader.FrameCount(value));
                AssertProp(() => x.Immutable.WavHeader.WithFrameCount(value));
                AssertProp(() => x.Immutable.WavHeader.SetFrameCount(value));
                if (test.AudioLength.Changed) AssertProp(() => x.Immutable.WavHeader.SetAudioLength(test.AudioLength));
                // SamplingRate does not affect FrameCount in this case.
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init, test.CourtesyFrames.Init);
            
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
            AreEqual(DefaultAudioLength * DefaultSamplingRate, () => DefaultFrameCount);
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
                    new Case(frameCount:  0) { CourtesyFrames = 2, Strict = false }), 
                    "Duration is not above 0.");
            
            // FrameCount does not need to include courtesy frames, so now frame count 2
            // gets a duration > 0 and does not generate the message "Duration is not above 0."
            CreateTestEntities(new Case(frameCount: 2) { CourtesyFrames = 2, AudioLength = 0, Strict = false });
        }

        // Getter Helpers
        
        internal static void Assert_All_Getters(TestEntities x, int frameCount, int courtesyFrames)
        {
            Assert_Bound_Getters      (x, frameCount, courtesyFrames);
            Assert_Independent_Getters(x, frameCount, courtesyFrames);
            Assert_Immutable_Getters  (x, frameCount);
        }

        private static void Assert_Bound_Getters(TestEntities x, int frameCount, int courtesyFrames)
        {
            Assert_SynthBound_Getters(x, frameCount);
            Assert_TapeBound_Getters (x, frameCount, courtesyFrames);
            Assert_BuffBound_Getters (x, frameCount, courtesyFrames);
        }
        
        private static void Assert_Independent_Getters(TestEntities x, int frameCount, int courtesyFrames)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample       , frameCount, courtesyFrames);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, frameCount);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, frameCount);
        }

        private static void Assert_Immutable_Getters(TestEntities x, int frameCount)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, frameCount);
        }

        private static void Assert_SynthBound_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .GetFrameCount,   Tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .GetFrameCount,   Tolerance);
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.GetFrameCount(x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .FrameCount   (), Tolerance);
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.FrameCount   (x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => FrameCount   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .GetFrameCount(x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .GetFrameCount(x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => ConfigWishesAccessor.GetFrameCount(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .FrameCount   (x.SynthBound.SynthWishes), Tolerance);
            AreEqual(frameCount, () => ConfigWishes        .FrameCount   (x.SynthBound.FlowNode   ), Tolerance);
            AreEqual(frameCount, () => ConfigWishesAccessor.FrameCount   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes), Tolerance);
        }
        
        private static void Assert_TapeBound_Getters(TestEntities x, int frameCount, int courtesyFrames)
        {
            AreEqual(frameCount, () => x.TapeBound.Tape       .FrameCount   (), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.TapeConfig .FrameCount   (), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.TapeActions.FrameCount   (), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.TapeAction .FrameCount   (), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.Tape       .GetFrameCount(), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.TapeConfig .GetFrameCount(), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.TapeActions.GetFrameCount(), courtesyFrames, Up);
            AreEqual(frameCount, () => x.TapeBound.TapeAction .GetFrameCount(), courtesyFrames, Up);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.Tape       ), courtesyFrames, Up);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.TapeConfig ), courtesyFrames, Up);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.TapeActions), courtesyFrames, Up);
            AreEqual(frameCount, () => FrameCount   (x.TapeBound.TapeAction ), courtesyFrames, Up);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.Tape       ), courtesyFrames, Up);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.TapeConfig ), courtesyFrames, Up);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.TapeActions), courtesyFrames, Up);
            AreEqual(frameCount, () => GetFrameCount(x.TapeBound.TapeAction ), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.Tape       ), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.TapeConfig ), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.TapeActions), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.TapeBound.TapeAction ), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.Tape       ), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.TapeConfig ), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.TapeActions), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.TapeBound.TapeAction ), courtesyFrames, Up);
        }
        
        private static void Assert_BuffBound_Getters(TestEntities x, int frameCount, int courtesyFrames)
        {
            Assert_Buff_Getters           (x, frameCount, courtesyFrames);
            Assert_AudioFileOutput_Getters(x, frameCount);
        }

        private static void Assert_AudioFileOutput_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.FrameCount               (), Tolerance);
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.GetFrameCount            (), Tolerance);
            AreEqual(frameCount, () =>              FrameCount   (x.BuffBound.AudioFileOutput), Tolerance);
            AreEqual(frameCount, () =>              GetFrameCount(x.BuffBound.AudioFileOutput), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.BuffBound.AudioFileOutput), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.BuffBound.AudioFileOutput), Tolerance);
        }
        
        private static void Assert_Buff_Getters(TestEntities x, int frameCount, int courtesyFrames)
        {
            AreEqual(frameCount, () => x.BuffBound.Buff.FrameCount               (), courtesyFrames, Up);
            AreEqual(frameCount, () => x.BuffBound.Buff.GetFrameCount            (), courtesyFrames, Up);
            AreEqual(frameCount, () =>              FrameCount   (x.BuffBound.Buff), courtesyFrames, Up);
            AreEqual(frameCount, () =>              GetFrameCount(x.BuffBound.Buff), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (x.BuffBound.Buff), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(x.BuffBound.Buff), courtesyFrames, Up);
        }
        
        private static void Assert_Independent_Getters(Sample sample, int frameCount, int courtesyFrames)
        {
            AreEqual(frameCount, () => sample.FrameCount   (), courtesyFrames, Up);
            AreEqual(frameCount, () => sample.GetFrameCount(), courtesyFrames, Up);
            AreEqual(frameCount, () => FrameCount   (sample), courtesyFrames, Up);
            AreEqual(frameCount, () => GetFrameCount(sample), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (sample), courtesyFrames, Up);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(sample), courtesyFrames, Up);
        }
        
        private static void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int frameCount)
        {
            AreEqual(frameCount, () => audioInfoWish.FrameCount     , Tolerance);
            AreEqual(frameCount, () => audioInfoWish.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => audioInfoWish.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (audioInfoWish) , Tolerance);
            AreEqual(frameCount, () => GetFrameCount(audioInfoWish) , Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (audioInfoWish), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(audioInfoWish), Tolerance);
        }

        private static void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int frameCount)
        {
            AreEqual(frameCount, () => audioFileInfo.SampleCount , Tolerance);
            AreEqual(frameCount, () => audioFileInfo.FrameCount   (), Tolerance);
            AreEqual(frameCount, () => audioFileInfo.GetFrameCount(), Tolerance);
            AreEqual(frameCount, () => FrameCount   (audioFileInfo), Tolerance);
            AreEqual(frameCount, () => GetFrameCount(audioFileInfo), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.FrameCount   (audioFileInfo), Tolerance);
            AreEqual(frameCount, () => ConfigWishes.GetFrameCount(audioFileInfo), Tolerance);
        }

        private static void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int frameCount)
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