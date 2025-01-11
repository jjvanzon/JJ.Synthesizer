using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611 // Type or member is obsolete
#pragma warning disable MSTEST0018 // DynamicData members should be IEnumerable<object[]>

namespace JJ.Business.Synthesizer.Tests.Technical.AttributeWishes
{
    [TestClass]
    [TestCategory("Technical")]
    public class SamplingRateWishesTests
    {
        [DataTestMethod]
        [DataRow(96000)]
        [DataRow(88200)]
        [DataRow(48000)]
        [DataRow(44100)]
        [DataRow(22050)]
        [DataRow(11025)]
        [DataRow(1)]
        [DataRow(8)]
        [DataRow(16)]
        [DataRow(32)]
        [DataRow(64)]
        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(12345)]
        [DataRow(1234567)]
        public void Init_SamplingRate(int init)
        {
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_SamplingRate(int init, int value)
        {            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, value);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, value);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.SamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.SamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.SamplingRate(value)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes.WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode.WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.WithSamplingRate(value)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_SamplingRate(int init, int value)
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,       () => x.TapeBound.Tape.SamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.SamplingRate(value)));
            AssertProp(x =>                                        x.TapeBound.TapeConfig.SamplingRate = value);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.SamplingRate(value)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_SamplingRate(int init, int value)
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

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.SamplingRate(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SamplingRate(value)));
            AssertProp(x =>                                             x.BuffBound.AudioFileOutput.SamplingRate =value);
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_SamplingRate(int init, int value)
        {
            // Independent after Taping

            // Sample
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish,init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SamplingRate(value)));
                AssertProp(() =>                                      x.Independent.Sample.SamplingRate = value);
            }
            
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

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SamplingRate(value)));
                AssertProp(() =>                                             x.Independent.AudioInfoWish.SamplingRate = value);
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

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SamplingRate(value)));
                AssertProp(() =>                                             x.Independent.AudioFileInfo.SamplingRate = value);
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_SamplingRate(int init, int value)
        {
            TestEntities x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Independent_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Independent_Getters(x.Immutable.WavHeader, init);
                    Assert_Independent_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }

                AssertProp(() => x.Immutable.WavHeader.SamplingRate(value));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders.ForEach(w => Assert_Independent_Getters(w, value));
        }

        [TestMethod] public void ConfigSections_SamplingRate()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = TestEntities.GetConfigSectionAccessor();
            
            AreEqual(DefaultSamplingRate, () => configSection.SamplingRate);
            AreEqual(DefaultSamplingRate, () => configSection.SamplingRate());
        }

        // Helpers
        
        private TestEntities CreateTestEntities(int samplingRate)
        {
            double audioLength = DefaultAudioLength;
            if (samplingRate > 100) audioLength = 0.001; // Tape audio length in case of larger sampling rates for performance.
            return new TestEntities(x => x.WithSamplingRate(samplingRate).WithAudioLength(audioLength));
        }
        
        static object TestParameters => new[]
        {
            new object[] { 48000, 96000 },
            new object[] { 48000, 88200 },
            new object[] { 48000, 48000 },
            new object[] { 48000, 44100 },
            new object[] { 48000, 22050 },
            new object[] { 48000, 11025 },
            new object[] { 48000, 1 },
            new object[] { 48000, 8 },
            new object[] { 96000, 48000 },
            new object[] { 88200, 44100 },
            new object[] { 44100, 48000 },
            new object[] { 22050, 44100 },
            new object[] { 11025, 44100 },
            new object[] { 1, 48000 },
            new object[] { 8, 48000 },
            new object[] { 48000, 16 },
            new object[] { 48000, 32 },
            new object[] { 48000, 64 },
            new object[] { 48000, 100 },
            new object[] { 48000, 1000 },
            new object[] { 48000, 12345 },
            new object[] { 48000, 1234567 },
        };

        private void Assert_All_Getters(TestEntities x, int samplingRate)
        {
            Assert_Bound_Getters(x, samplingRate);
            Assert_Independent_Getters(x, samplingRate);
            Assert_Immutable_Getters(x, samplingRate);
        }

        private void Assert_Bound_Getters(TestEntities x, int samplingRate)
        {
            Assert_SynthBound_Getters(x, samplingRate);
            Assert_TapeBound_Getters(x, samplingRate);
            Assert_BuffBound_Getters(x, samplingRate);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int samplingRate)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, samplingRate);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, samplingRate);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, samplingRate);
        }

        private void Assert_Immutable_Getters(TestEntities x, int samplingRate)
        {
            Assert_Independent_Getters(x.Immutable.WavHeader, samplingRate);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int samplingRate)
        {
            AreEqual(samplingRate, () => x.SynthBound.SynthWishes.SamplingRate());
            AreEqual(samplingRate, () => x.SynthBound.SynthWishes.GetSamplingRate);
            AreEqual(samplingRate, () => x.SynthBound.FlowNode.SamplingRate());
            AreEqual(samplingRate, () => x.SynthBound.FlowNode.GetSamplingRate);
            AreEqual(samplingRate, () => x.SynthBound.ConfigWishes.SamplingRate());
            AreEqual(samplingRate, () => x.SynthBound.ConfigWishes.GetSamplingRate);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int samplingRate)
        {
            AreEqual(samplingRate, () => x.TapeBound.Tape.SamplingRate());
            AreEqual(samplingRate, () => x.TapeBound.TapeConfig.SamplingRate());
            AreEqual(samplingRate, () => x.TapeBound.TapeConfig.SamplingRate);
            AreEqual(samplingRate, () => x.TapeBound.TapeActions.SamplingRate());
            AreEqual(samplingRate, () => x.TapeBound.TapeAction.SamplingRate());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int samplingRate)
        {
            AreEqual(samplingRate, () => x.BuffBound.Buff.SamplingRate());
            AreEqual(samplingRate, () => x.BuffBound.AudioFileOutput.SamplingRate());
            AreEqual(samplingRate, () => x.BuffBound.AudioFileOutput.SamplingRate);
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int samplingRate)
        {
            AreEqual(samplingRate, () => audioFileInfo.SamplingRate());
            AreEqual(samplingRate, () => audioFileInfo.SamplingRate);
        }
        
        private void Assert_Independent_Getters(Sample sample, int samplingRate)
        {
            AreEqual(samplingRate, () => sample.SamplingRate());
            AreEqual(samplingRate, () => sample.SamplingRate);
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int samplingRate)
        {
            AreEqual(samplingRate, () => audioInfoWish.SamplingRate());
            AreEqual(samplingRate, () => audioInfoWish.SamplingRate);
        }

        private void Assert_Independent_Getters(WavHeaderStruct wavHeader, int samplingRate)
        {
            AreEqual(samplingRate, () => wavHeader.SamplingRate());
            AreEqual(samplingRate, () => wavHeader.SamplingRate);
        }
    } 
}