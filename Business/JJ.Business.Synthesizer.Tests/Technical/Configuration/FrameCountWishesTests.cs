using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
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
        [DynamicData(nameof(TestParametersInit))]
        public void Init_FrameCount(int? init)
        {
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, Coalesce(init));
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpties))]
        public void SynthBound_FrameCount(int? init, int? value)
        {            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, Coalesce(init));
                
                setter(x);
                
                Assert_SynthBound_Getters(x, Coalesce(value));
                Assert_TapeBound_Getters(x, Coalesce(init));
                Assert_BuffBound_Getters(x, Coalesce(init));
                Assert_Independent_Getters(x, Coalesce(init));
                Assert_Immutable_Getters(x, Coalesce(init));
                
                x.Record();
                Assert_All_Getters(x, Coalesce(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .FrameCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .FrameCount(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.FrameCount(value, x.SynthBound.SynthWishes)));
        }

        //[TestMethod] 
        //[DynamicData(nameof(TestParameters))]
        public void TapeBound_FrameCount(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init);
                Assert_TapeBound_Getters(x, value);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FrameCount(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FrameCount(value)));
        }

        //[TestMethod] 
        //[DynamicData(nameof(TestParameters))]
        public void BuffBound_FrameCount(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
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
        
        //[TestMethod]
        //[DynamicData(nameof(TestParameters))]
        public void Independent_FrameCount(int init, int value)
        {
            // Independent after Taping

            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
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
                    x = CreateTestEntities(init);
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
        
        //[TestMethod] 
        //[DynamicData(nameof(TestParameters))]
        public void Immutable_FrameCount(int init, int value)
        {
            TestEntities x = CreateTestEntities(init);

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
            ThrowsException_OrInnerException<Exception>(() => CreateTestEntities(frameCount: -1), "FrameCount -1 below 0.");
            ThrowsException_OrInnerException<Exception>(() => CreateTestEntities(frameCount:  0), "FrameCount = 0 but should be a minimum of 2 CourtesyFrames.");
            ThrowsException_OrInnerException<Exception>(() => CreateTestEntities(frameCount:  2), "Duration is not above 0.");
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
        
        private TestEntities CreateTestEntities(int? frameCount)
        {
            return new TestEntities(x =>
            {
                // Impersonate NCrunch for reliable default SamplingRate of 10 Hz.
                x.IsUnderNCrunch = true;
                x.IsUnderAzurePipelines = false;
                x.FrameCount(frameCount);
            });
        }
        
        private int Coalesce(int? frameCount)
        {
            int defaultValue = 1 /*sec*/ * 10 /*Hz*/ + 2 /*CourtesyFrames*/;
            return CoalesceFrameCount(frameCount, defaultValue);
        }
        
        // ncrunch: no coverage start
        
        static IEnumerable<object[]> TestParametersInit => new[]
        {
            new object[] { 96000 },
            new object[] { 88200 },
            new object[] { 48000 },
            new object[] { 44100 },
            new object[] { 22050 },
            new object[] { 11025 },
            new object[] { 8 },
            new object[] { 16 },
            new object[] { 19 },
            new object[] { 31 },
            new object[] { 61 },
            new object[] { 100 },
            new object[] { 1000 },
            new object[] { 12345 },
            new object[] { 1234567 }
        };
        
        static IEnumerable<object[]> TestParametersWithEmpties => new[]
        {
            new object[] { 1234567 ,  null },
            new object[] {    null , 12345 },
            
        }.Concat(TestParameters);

        static IEnumerable<object[]> TestParameters => new[] 
        {
            new object[] { 48000, 96000 },
            new object[] { 48000, 88200 },
            new object[] { 48000, 48000 },
            new object[] { 48000, 44100 },
            new object[] { 48000, 22050 },
            new object[] { 48000, 11025 },
            new object[] { 48000, 8 },
            new object[] { 96000, 48000 },
            new object[] { 88200, 44100 },
            new object[] { 44100, 48000 },
            new object[] { 22050, 44100 },
            new object[] { 11025, 44100 },
            new object[] { 8, 48000 },
            new object[] { 48000, 16 },
            new object[] { 48000, 19 },
            new object[] { 48000, 31 },
            new object[] { 48000, 61 },
            new object[] { 48000, 100 },
            new object[] { 48000, 1000 },
            new object[] { 48000, 12345 },
            new object[] { 48000, 1234567 },
        };

        class Case
        {
            // FrameCount: the main property tested
            public int? FromFrameCountNully { get; set; }
            public int? FromFrameCountCoalesced { get; set; }
            public int? ToFrameCountNully { get; set; }
            public int? ToFrameCountCoalesced { get; set; }

            // SamplingRate scales FrameCount.
            public int? FromSamplingRateNully { get; set; }
            public int? FromSamplingRateCoalesced { get; set; }
            public int? ToSamplingRateNully { get; set; }
            public int? ToSamplingRateCoalesced { get; set; }
            
            // AudioLength scales FrameCount.
            // + FrameCount setters adjust AudioLength.
            public double? FromAudioLengthNully { get; set; }
            public double? FromAudioLengthCoalesced { get; set; }
            public double? ToAudioLengthNully { get; set; }
            public double? ToAudioLengthCoalesced { get; set; }
            
            // AudioLength does not incorporate CourtesyFrames, but
            // FrameCount does.
            public int? FromCourtesyFramesNully { get;set; }
            public int? FromCourtesyFramesCoalesced { get;set; }
            public int? ToCourtesyFramesNully { get;set; }
            public int? ToCourtesyFramesCoalesced { get;set; }

            // AudioLength vs FrameCount should be invariant under Channels,
            // but was accidentally involved in the formulas.
            public double? FromChannelsNully { get; set; }
            public double? FromChannelsCoalesced { get; set; }
            public double? ToChannelsNully { get; set; }
            public double? ToChannelsCoalesced { get; set; }
        }
        
        // ncrunch: no coverage end
    } 
}