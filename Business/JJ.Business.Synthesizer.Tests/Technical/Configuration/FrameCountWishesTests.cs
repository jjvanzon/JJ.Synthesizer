using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJ.Persistence.Synthesizer;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Wishes.Collections_Copied;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static JJ.Framework.Wishes.Testing.AssertHelper_Copied;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Configuration
{
    [TestClass]
    [TestCategory("Technical")]
    public class FrameCountWishesTests
    {
        
        [DataTestMethod]
        [DynamicData(nameof(CaseKeysInit))]
        public void Init_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            var  init = testCase.init;
            
            var x = CreateTestEntities(testCase);
            Assert_All_Getters(x, init.coalesce);
        }
        
        [TestMethod] 
        [DynamicData(nameof(CaseKeysWithEmpties))]
        public void SynthBound_FrameCount(string caseKey)
        {            
            Case testCase = _caseDictionary[caseKey];
            var init = testCase.init;
            var val = testCase.val;
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, Coalesce(init.coalesce));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, val .coalesce);
                Assert_TapeBound_Getters  (x, init.coalesce);
                Assert_BuffBound_Getters  (x, init.coalesce);
                Assert_Independent_Getters(x, init.coalesce);
                Assert_Immutable_Getters  (x, init.coalesce);
                
                x.Record();
                Assert_All_Getters(x, val.coalesce);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameCount(val.nully)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameCount(val.nully)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameCount(val.nully, x.SynthBound.SynthWishes)));
        }

        //[TestMethod] 
        //[DynamicData(nameof(CaseKeys))]
        public void TapeBound_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.init.coalesce;
            int val  = testCase.val.coalesce;

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, val);
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

        //[TestMethod] 
        //[DynamicData(nameof(CaseKeys))]
        public void BuffBound_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.CoalescedFrom;
            int value = testCase.CoalescedTo;
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(testCase);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, value);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init);
            }

            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.FrameCount(value, courtesyFrames: 2))); // TODO: Replace hard coded value for courtesyFrames.
        }
        
        [TestMethod] 
        [DynamicData(nameof(CaseKeys))]
        public void Independent_FrameCount(string caseKey)
        {
            Case testCase = _caseDictionary[caseKey];
            int init = testCase.CoalescedFrom;
            int value = testCase.CoalescedTo;
         
            // Independent after Taping

            // AudioInfoWish
            {
                TestEntities x = default;

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
                TestEntities x = default;
                
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
            int init = testCase.CoalescedFrom;
            int value = testCase.CoalescedTo;
            
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
        public void GlobalBound_FrameCount()
        {
            // Immutable. Get-only.
            
            // Config
            var configSection = TestEntities.GetConfigSectionAccessor();
            int configCourtesyFrames = 2;
            AreEqual(DefaultFrameCount - DefaultCourtesyFrames + configCourtesyFrames, () => configSection.FrameCount());
            
            // Default
            AreEqual( 1 /*sec*/ * 48000 /*Hz*/ + 4 /*CourtesyFrames*/, () => DefaultFrameCount);
        }
        
        [TestMethod]
        public void FrameCount_EdgeCases()
        {
            ThrowsException_OrInnerException<Exception>(() => CreateTestEntities(new Case(frameCount: -1)), "FrameCount -1 below 0.");
            ThrowsException_OrInnerException<Exception>(() => CreateTestEntities(new Case(frameCount:  0)), "FrameCount = 0 but should be a minimum of 2 CourtesyFrames.");
            ThrowsException_OrInnerException<Exception>(() => CreateTestEntities(new Case(frameCount:  2)), "Duration is not above 0.");
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int frameCount)
        {
            Assert_Bound_Getters(x, frameCount);
            Assert_Independent_Getters(x, frameCount);
            Assert_Immutable_Getters(x, frameCount);
        }

        private void Assert_Bound_Getters(TestEntities x, int frameCount)
        {
            Assert_SynthBound_Getters(x, frameCount);
            Assert_TapeBound_Getters(x, frameCount);
            Assert_BuffBound_Getters(x, frameCount);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int frameCount)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, frameCount);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, frameCount);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, frameCount);
        }

        private void Assert_Immutable_Getters(TestEntities x, int frameCount)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, frameCount);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.SynthBound.SynthWishes.FrameCount());
            AreEqual(frameCount, () => x.SynthBound.FlowNode.FrameCount());
            AreEqual(frameCount, () => x.SynthBound.ConfigResolver.FrameCount(x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.TapeBound.Tape.FrameCount());
            AreEqual(frameCount, () => x.TapeBound.TapeConfig.FrameCount());
            AreEqual(frameCount, () => x.TapeBound.TapeActions.FrameCount());
            AreEqual(frameCount, () => x.TapeBound.TapeAction.FrameCount());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int frameCount)
        {
            AreEqual(frameCount, () => x.BuffBound.Buff.FrameCount(x.Immutable.CourtesyFrames));
            AreEqual(frameCount, () => x.BuffBound.AudioFileOutput.FrameCount(x.Immutable.CourtesyFrames));
        }
        
        private void Assert_Independent_Getters(Sample sample, int frameCount)
        {
            AreEqual(frameCount, () => sample.FrameCount());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int frameCount)
        {
            AreEqual(frameCount, () => audioInfoWish.FrameCount());
            AreEqual(frameCount, () => audioInfoWish.FrameCount);
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int frameCount)
        {
            AreEqual(frameCount, () => audioFileInfo.FrameCount());
            AreEqual(frameCount, () => audioFileInfo.SampleCount);
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int frameCount)
        {
            AreEqual(frameCount, () => wavHeader.FrameCount());
        }
 
        // Test Data Helpers
        
        private TestEntities CreateTestEntities(Case testCase)
        {
            return new TestEntities(x =>
            {
                // Impersonate NCrunch for reliable default SamplingRate of 10 Hz.
                x.IsUnderNCrunch = true;
                x.IsUnderAzurePipelines = false;
                x.FrameCount(testCase.FrameCount.NullyFrom);
            });
        }
        
        private static int Coalesce(int? frameCount)
        {
            int defaultValue = 1 /*sec*/ * 10 /*Hz*/ + 2 /*CourtesyFrames*/;
            return CoalesceFrameCount(frameCount, defaultValue);
        }
        
        // ncrunch: no coverage start
        
        static object CaseKeysInit        => _casesInit       .Select(x => new object[] { x.Descriptor }).ToArray();
        static object CaseKeys            => _cases           .Select(x => new object[] { x.Descriptor }).ToArray();
        static object CaseKeysWithEmpties => _casesWithEmpties.Select(x => new object[] { x.Descriptor }).ToArray();

        static Case[] _casesInit =
        {
            new Case(   96000 ),
            new Case(   88200 ),
            new Case(   48000 ),
            new Case(   44100 ),
            new Case(   22050 ),
            new Case(   11025 ),
            new Case(       8 ),
            new Case(      16 ),
            new Case(      19 ),
            new Case(      31 ),
            new Case(      61 ),
            new Case(     100 ),
            new Case(    1000 ),
            new Case(   12345 ),
            new Case( 1234567 )
        };
        
        static Case[] _cases =
        {
            new Case( 48000,   96000 ),
            new Case( 48000,   88200 ),
            new Case( 48000,   48000 ),
            new Case( 48000,   44100 ),
            new Case( 48000,   22050 ),
            new Case( 48000,   11025 ),
            new Case( 48000,       8 ),
            new Case( 96000,   48000 ),
            new Case( 88200,   44100 ),
            new Case( 44100,   48000 ),
            new Case( 22050,   44100 ),
            new Case( 11025,   44100 ),
            new Case(     8,   48000 ),
            new Case( 48000,      16 ),
            new Case( 48000,      19 ),
            new Case( 48000,      31 ),
            new Case( 48000,      61 ),
            new Case( 48000,     100 ),
            new Case( 48000,    1000 ),
            new Case( 48000,   12345 ),
            new Case( 48000, 1234567 ),
        };

        static Case[] _casesWithEmpties = new[]
        {
            new Case( 1234567,  null ),
            new Case(    null, 12345 )
        }
        .Concat(_cases).ToArray();

        static Dictionary<string, Case> _caseDictionary = _casesWithEmpties.Concat(_casesInit)
                                                                           .Distinct(x => x.Descriptor)
                                                                           .ToDictionary(x => x.Descriptor);
        private class Case : CaseProp<int>
        {
            // FrameCount: The main property being tested, adjusted directly or via dependencies.
            public CaseProp<int> FrameCount => this;

            // SamplingRate: Scales FrameCount
            public CaseProp<int> SamplingRate { get; set; } = new CaseProp<int>();

            // AudioLength: Scales FrameCount + FrameCount setters adjust AudioLength.
            public CaseProp<double> AudioLength { get; set; } = new CaseProp<double>();
            
            // CourtesyFrames: AudioLength does not incorporate CourtesyFrames, but FrameCount does.
            public CaseProp<int> CourtesyFrames { get; set; } = new CaseProp<int>();

            // Channels: AudioLength vs FrameCount should be invariant under Channels, but was accidentally involved in the formulas.
            public CaseProp<int> Channels { get; set; } = new CaseProp<int>();

            public override string Descriptor
            {
                get
                {
                    string descriptor = base.Descriptor;
                    string samplingRateDescriptor = SamplingRate.Descriptor;
                    if (Has(samplingRateDescriptor)) descriptor += $" ({samplingRateDescriptor})";
                    return descriptor;
                }
            }
            
            public const int    SamplingRateTestDefault   = 44100;
            public const double AudioLengthTestDefault    = 1.6;
            public const int    CourtesyFramesTestDefault = 3;
            public const int    ChannelsTestDefault       = 2;

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
            
            public Case(int frameCount) => Value = frameCount;
            public Case(int from, int to) { From = from; To = to; }
             
            public Case(int? from, int to)
            {
                NullyFrom     = from;
                CoalescedFrom = Coalesce(from); 
                To            = to;
            }
            
            public Case(int from, int? to)
            {
                From        = from;
                NullyTo     = to;
                CoalescedTo = Coalesce(to);
            }
            
            public Case(int? from, int? to)
            { 
                NullyFrom     = from; 
                CoalescedFrom = Coalesce(from);
                NullyTo       = to;
                CoalescedTo   = Coalesce(to);
            }
        }
                  
        private struct Parameters<T> where T : struct
        {
            public T? nully;
            public T coalesce;
        }
        
        private class CaseProp<T> where T : struct
        {
            public Parameters<T> init => new Parameters<T> { nully = NullyFrom, coalesce = CoalescedFrom };
            public Parameters<T> val  => new Parameters<T> { nully = NullyTo  , coalesce = CoalescedTo   };

            public T? NullyFrom     { get; set; }
            public T  CoalescedFrom { get; set; }
            public T? NullyTo       { get; set; }
            public T  CoalescedTo   { get; set; }

            public T Value
            {
                get => Equals(From, To) ? To : default;
                set => From = To = value;
            }
            
            public T From
            {
                get => Equals(NullyFrom, CoalescedFrom) ? CoalescedFrom : default;
                set => NullyFrom = CoalescedFrom = value;
            }
                        
            public T To
            {
                get => Equals(NullyTo, CoalescedTo) ? CoalescedTo : default;
                set => NullyTo = CoalescedTo = value;
            }
            
            public T? Nully
            {
                get => Equals(NullyFrom, NullyTo) ? NullyTo : default;
                set => NullyFrom= NullyTo = value;
            }
            
            public T Coalesced
            {
                get => Equals(CoalescedFrom, CoalescedTo) ? CoalescedTo : default;
                set => CoalescedFrom = CoalescedTo = value;
            }

            public virtual string Descriptor
            {
                get
                {
                    if (Has(Value))
                    {
                        return $"{Value}";
                    }
                    
                    if (Has(From) && Has(To))
                    {
                        return $"{From} => {To}";
                    }
                    
                    if (Has(Nully) && Has(Coalesced))
                    {
                        return $"({Nully},{Coalesced})";
                    }
                    
                    if (!Has(NullyFrom) && !Has(CoalescedFrom) && !Has(NullyTo) && !Has(CoalescedTo))
                    {
                        return default;
                    }
                    
                    return $"({NullyFrom},{CoalescedFrom}) => ({NullyTo},{CoalescedTo})";
                }
            }

            public static implicit operator T(CaseProp<T> prop) => prop.Value;
            public static implicit operator CaseProp<T>(T val) => new CaseProp<T> { Value = val };
        }
        
        static Case[] _caseExamples = 
        {
            // Example with all values specified
            new Case 
            { 
                FrameCount     = { NullyFrom = 22050 * 3 , CoalescedFrom = 22050 * 3 , NullyTo = 22050 * 5 , CoalescedTo = 22050 * 5 }, 
                SamplingRate   = { NullyFrom = 22050     , CoalescedFrom = 22050     , NullyTo = 22050     , CoalescedTo = 22050     }, 
                AudioLength    = { NullyFrom =         3 , CoalescedFrom =         3 , NullyTo =         5 , CoalescedTo =         5 }, 
                CourtesyFrames = { NullyFrom = 4         , CoalescedFrom = 4         , NullyTo = 4         , CoalescedTo = 4         }, 
                Channels       = { NullyFrom = 2         , CoalescedFrom = 2         , NullyTo = 2         , CoalescedTo = 2         }
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