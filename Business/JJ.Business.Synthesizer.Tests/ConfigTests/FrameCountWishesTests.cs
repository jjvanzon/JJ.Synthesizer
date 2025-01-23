using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Framework.Wishes.Collections;
using JJ.Framework.Wishes.Common;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
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
        private int _tolerance = -1;

        // ncrunch: no coverage start
        
        private static Case[] _casesInit = 
        {
            new Case(  96000+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(  88200+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(  48000+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(  44100+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(  22050+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(  11025+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(      8+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(     16+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(     19+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(     31+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(     61+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(    100+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(   1000+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(   1003+3 ) { SamplingRate = 48000, CourtesyFrames = 3 }, 
            new Case(  12345+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 123456+3 ) { SamplingRate = 48000, CourtesyFrames = 3 }
        };
        static object[][] CaseKeysInit => _casesInit.Select(x => new object[] { x.Descriptor }).ToArray();

        // ncrunch: no coverage end
        
        [DataTestMethod]
        [DynamicData(nameof(CaseKeysInit))]
        public void Init_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var x = CreateTestEntities(testCase);
            Assert_All_Getters(x, testCase);
        }
        
        // ncrunch: no coverage start
        
        static Case[] _cases =
        {
            new Case( 4800+3,  4800+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  9600+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  8820+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  4410+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  2205+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  1102+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,     8+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 9600+3,  4800+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 8820+3,  4410+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4410+3,  4800+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 2205+3,  4410+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 1102+3,  4410+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case(    8+3,  4800+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,    16+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,    19+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,    31+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,    61+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,   100+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  1000+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3,  1234+3 ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( 4800+3, 12345+3 ) { SamplingRate = 48000, CourtesyFrames = 3 }
        };
        static object[][] CaseKeys => _cases.Select(x => new object[] { x.Descriptor }).ToArray();

        static Case[] _nullyCases =  
        {
            new Case(       48000+3 , (null,48000+3) ) { SamplingRate = 48000, CourtesyFrames = 3 },
            new Case( (null,48000+3),       48000+3  ) { SamplingRate = 48000, CourtesyFrames = 3 }
        };
        static object[][] NullyCaseKeys => _nullyCases.Select(x => new object[] { x.Descriptor }).ToArray();
        
        static object[][] CaseKeysWithNullies => CaseKeys.Concat(NullyCaseKeys).ToArray();
        
        Dictionary<string, Case> _caseDictionary = _cases.Concat(_nullyCases)
                                                         .Concat(_casesInit)
                                                         .Distinct(x => x.Descriptor)
                                                         .ToDictionary(x => x.Descriptor);
        // ncrunch: no coverage end
         
        [TestMethod] 
        //[DynamicData(nameof(CaseKeys))]
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
        }

        [TestMethod] 
        [DynamicData(nameof(CaseKeys))]
        public void TapeBound_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.From;
            int val  = testCase.To;

            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, init); // By Design: Tape is too buff to change. FrameCount will be based on buff.
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FrameCount(val)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FrameCount(val)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FrameCount(val)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FrameCount(val)));
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

                AssertProp(() => x.Immutable.WavHeader.FrameCount(value, x.Immutable.CourtesyFrames));
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
            AreEqual(frameCount, () => x.SynthBound.SynthWishes   .FrameCount(), _tolerance);
            AreEqual(frameCount, () => x.SynthBound.FlowNode      .FrameCount(), _tolerance);
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.FrameCount(x.SynthBound.SynthWishes), _tolerance);
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.TapeBound.Tape       .FrameCount(), _tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeConfig .FrameCount(), _tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeActions.FrameCount(), _tolerance);
            AreEqual(frameCount, () => x.TapeBound.TapeAction .FrameCount(), _tolerance);
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, int frameCount)
        {
            Assert_Buff_Getters           (x, frameCount);
            Assert_AudioFileOutput_Getters(x, frameCount);
        }

        private void Assert_AudioFileOutput_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.FrameCount(x.Immutable.CourtesyFrames), _tolerance);
        }
        
        private void Assert_Buff_Getters(ConfigTestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.Buff.FrameCount(x.Immutable.CourtesyFrames), _tolerance);
        }
        
        private void Assert_Independent_Getters(Sample sample, int frameCount)
        {
            AreEqual(frameCount, () => sample.FrameCount(), _tolerance);
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int frameCount)
        {
            AreEqual(frameCount, () => audioInfoWish.FrameCount(), _tolerance);
            AreEqual(frameCount, () => audioInfoWish.FrameCount  , _tolerance);
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int frameCount)
        {
            AreEqual(frameCount, () => audioFileInfo.FrameCount(), _tolerance);
            AreEqual(frameCount, () => audioFileInfo.SampleCount , _tolerance);
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int frameCount)
        {
            AreEqual(frameCount, () => wavHeader.FrameCount(), _tolerance);
        }
 
        // Test Data Helpers
        
        private ConfigTestEntities CreateTestEntities(Case testCase = default)
        {
            testCase = testCase ?? new Case();
            
            return new ConfigTestEntities(x =>
            {
                // Impersonate NCrunch for reliable default SamplingRate of 10 Hz.
                x.IsUnderNCrunch = true;
                x.IsUnderAzurePipelines = false;
                x.SamplingRate(testCase.SamplingRate.Init);
                x.Channels(testCase.Channels.Init);
                x.CourtesyFrames(testCase.CourtesyFrames.Init);
                x.FrameCount(testCase.Init);
            });
        }
        
        // ncrunch: no coverage start
        
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class Case : CaseProp<int>
        {
            string DebuggerDisplay => GetDebuggerDisplay(this);
            
            // Test defaults not quite the same as the regular defaults.
            public const int    SamplingRateTestDefault   = 44100;
            public const double AudioLengthTestDefault    = 0.1;
            public const int    CourtesyFramesTestDefault = 3;
            public const int    ChannelsTestDefault       = 2;

            // FrameCount: The main property being tested, adjusted directly or via dependencies.
            public CaseProp<int> FrameCount => this;

            // SamplingRate: Scales FrameCount
            public CaseProp<int> SamplingRate { get; set; } = SamplingRateTestDefault;

            // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
            public CaseProp<double> AudioLength { get; set; } = AudioLengthTestDefault;
            
            // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
            public CaseProp<int> CourtesyFrames { get; set; } = CourtesyFramesTestDefault;

            // Channels: AudioLength vs FrameCount is invariant under Channels, but accidentally involved in formulas.
            public CaseProp<int> Channels { get; set; } = ChannelsTestDefault;

            public override string Descriptor
            {
                get
                {
                    string descriptor = base.Descriptor;
                    string samplingRateDescriptor = SamplingRate.Descriptor;
                    string courtesyFramesDescriptor = CourtesyFrames.Descriptor;
                    if (Has(samplingRateDescriptor)) descriptor += $" ({samplingRateDescriptor}Hz+{courtesyFramesDescriptor})";
                    return descriptor;
                }
            }

            public Case(
                int    samplingRate   = SamplingRateTestDefault,
                double audioLength    = AudioLengthTestDefault,
                int    courtesyFrames = CourtesyFramesTestDefault,
                int    channels       = ChannelsTestDefault)
            {
                SamplingRate   = samplingRate;
                AudioLength    = audioLength;
                CourtesyFrames = courtesyFrames;
                Channels       = channels;
            }
            
            public Case(int frameCount) => From = To = frameCount;
            public Case(int from, int to) { From = from; To = to; }
             
            public Case(int from, (int? nully, int coalesced) to)
            {
                From         = from;
                To.Nully     = to.nully;
                To.Coalesced = to.coalesced;
            }
            public Case((int? nully, int coalesced) from, int to)
            {
                From.Nully     = from.nully;
                From.Coalesced = from.coalesced;
                To             = to;
            }
             
            public Case(int? from, int to)
            {
                From.Nully = from;
                To         = to;
            }
            
            public Case(int from, int? to)
            {
                From     = from;
                To.Nully = to;
            }
            
            public Case(int? from, int? to)
            { 
                From.Nully = from; 
                To.Nully   = to;
            }
        }
        
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class CaseProp<T> where T : struct
        {
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;
            
            public static implicit operator T (CaseProp<T> prop) => prop.To;
            public static implicit operator T?(CaseProp<T> prop) => prop.To;
            public static implicit operator CaseProp<T>(T  val) => new CaseProp<T> { To = val, From = val };
            public static implicit operator CaseProp<T>(T? val) => new CaseProp<T> { To = val, From = val };
            
            /// <inheritdoc cref="docs._from" />
            public NullyPair<T> From   { get; set; } = new NullyPair<T>();
            /// <inheritdoc cref="docs._from" />
            public NullyPair<T> Init   { get => From; set => From = value; }
            ///// <inheritdoc cref="docs._from" />
            //public NullyPair<T> Source { get => From; set => From = value; }

            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> To    { get; set; } = new NullyPair<T>();
            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> Value { get => To; set => To = value; }
            /// <inheritdoc cref="docs._to" />
            public NullyPair<T> Val { get => To; set => To = value; }
            ///// <inheritdoc cref="docs._to" />
            //public NullyPair<T> Dest  { get => To; set => To = value; }
            
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
                    string from = From.Descriptor;
                    string to = To.Descriptor;
                    
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
        }
        
        [DebuggerDisplay("{DebuggerDisplay}")]
        internal class NullyPair<T> where T : struct
        {
            string DebuggerDisplay => DebuggerDisplay(this);
            public override string ToString() => Descriptor;

            public T? Nully { get; set; }
            public T Coalesced { get; set; }
            
            public static implicit operator T?(NullyPair<T> values) => values.Nully;
            public static implicit operator T (NullyPair<T> values) => values.Coalesced;
            public static implicit operator NullyPair<T>(T? value) => new NullyPair<T> { Nully = value };
            public static implicit operator NullyPair<T>(T  value) => new NullyPair<T> { Nully = value, Coalesced = value };
            
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
                Channels       = { From = { Nully = 2        , Coalesced = 2         }, To = { Nully = 2        , Coalesced = 2         }}
            },
            
            // Example with same value for Nully and Coalesced
            new Case 
            { 
                FrameCount     = { From = 22050 * 3 , To = 22050 * 5 },
                SamplingRate   = { From = 22050     , To = 22050     },
                AudioLength    = { From =         3 , To =         5 },
                CourtesyFrames = { From = 4         , To = 4         },
                Channels       = { From = 2         , To = 2         }
            },
            
            // Example with single mentioning of values that don't change.
            new Case
            { 
                SamplingRate = 22050, Channels = 2, CourtesyFrames = 4, 
                AudioLength  = { From = 3         , To = 5         },
                FrameCount   = { From = 3 * 22050 , To = 5 * 22050 }
            },
            
            // Using inherited properties From and To to set main property FrameCount.
            new Case
            { 
                From = 3 * 22050 , To = 5 * 22050,
                AudioLength = { From = 3, To = 5 },
                SamplingRate = 22050, Channels = 2, CourtesyFrames = 4, 
            },
            
            // Example using constructor parameters for side-issues
            new Case(channels: 2, courtesyFrames: 4)
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