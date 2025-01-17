using System;
using System.Collections.Generic;
using System.Linq;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static JJ.Business.Synthesizer.Tests.Technical.Configuration.TestEntities;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Configuration
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioLengthWishesTests
    {
        
        [DataTestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_AudioLength(double? init)
        {
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, Coalesce(init));
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_AudioLength(double? init, double? value)
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioLength(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioLength(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioLength(value, x.SynthBound.SynthWishes)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioLength(value, x.SynthBound.SynthWishes)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_AudioLength(double init, double value)
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.AudioLength(value)));
            AssertProp(x =>                                         x.TapeBound.Tape.Duration = value);
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.AudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.AudioLength(value)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_AudioLength(double init, double value)
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

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.AudioLength(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AudioLength(value)));
            AssertProp(x =>                                             x.BuffBound.AudioFileOutput.Duration = value);
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_AudioLength(double init, double value)
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

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioLength(value)));
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

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AudioLength(value)));
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

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AudioLength(value)));
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_AudioLength(double init, double value)
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

                AssertProp(() => x.Immutable.WavHeader.AudioLength(value));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders.ForEach(w => Assert_Independent_Getters(w, value));
        }

        [TestMethod] public void ConfigSections_AudioLength()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            
            AreEqual(DefaultAudioLength, () => configSection.AudioLength);
            AreEqual(DefaultAudioLength, () => configSection.AudioLength());
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, double audioLength)
        {
            Assert_Bound_Getters(x, audioLength);
            Assert_Independent_Getters(x, audioLength);
            Assert_Immutable_Getters(x, audioLength);
        }

        private void Assert_Bound_Getters(TestEntities x, double audioLength)
        {
            Assert_SynthBound_Getters(x, audioLength);
            Assert_TapeBound_Getters(x, audioLength);
            Assert_BuffBound_Getters(x, audioLength);
        }
        
        private void Assert_Independent_Getters(TestEntities x, double audioLength)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, audioLength);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, audioLength);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, audioLength);
        }

        private void Assert_Immutable_Getters(TestEntities x, double audioLength)
        {
            Assert_Independent_Getters(x.Immutable.WavHeader, audioLength);
        }

        private void Assert_SynthBound_Getters(TestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.SynthBound.SynthWishes.AudioLength());
            AreEqual(audioLength, () => x.SynthBound.FlowNode.AudioLength());
            AreEqual(audioLength, () => x.SynthBound.ConfigResolver.AudioLength(x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.TapeBound.Tape.AudioLength());
            AreEqual(audioLength, () => x.TapeBound.Tape.Duration);
            AreEqual(audioLength, () => x.TapeBound.TapeConfig.AudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeActions.AudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeAction.AudioLength());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.BuffBound.Buff.AudioLength());
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.AudioLength());
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.Duration);
        }
        
        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, double audioLength)
        {
            AreEqual(audioLength, audioFileInfo.AudioLength(), tolerance);
        }

        private void Assert_Independent_Getters(Sample sample, double audioLength)
        {
            AreEqual(audioLength, sample.AudioLength(), tolerance);
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, double audioLength)
        {
            AreEqual(audioLength, audioInfoWish.AudioLength(), tolerance);
        }

        private void Assert_Independent_Getters(WavHeaderStruct wavHeader, double audioLength)
        {
            AreEqual(audioLength, wavHeader.AudioLength(), tolerance);
        }
 
        // Test Data Helpers
        
        // TODO: Needed tolerance is a bit much for the sampling rate.
        int samplingRate = 2000;
        double tolerance = 0.03; 
        
        TestEntities CreateTestEntities(double? audioLength) 
            => new TestEntities(x => x.WithAudioLength(audioLength).WithSamplingRate(samplingRate));
        
        double Coalesce(double? audioLength) => CoalesceAudioLength(audioLength, 1);

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> TestParametersInit => new[]
        {
            new object[] { null },
            new object[] {  0.0 },
            new object[] {  1.6 },
            new object[] {  2.0 },
            new object[] {    E },
            new object[] {   PI },
            new object[] {  5.0 },
            new object[] { 17.0 }
        };
        
        static IEnumerable<object[]> TestParametersWithEmpty => new[]
        {
            new object[] { null, null },
            new object[] {  0.0, null },
            new object[] { null, 0.0  },
            new object[] { null, 1.6  },
            new object[] {  1.6, null },
            new object[] {  0.0, 1.6  },
            new object[] {  1.6, 0.0  },
            
        }.Concat(TestParameters);
        
        static IEnumerable<object[]> TestParameters => new[]
        {
            new object[] {  1.6, 1.6  },
            new object[] {  1.6, E    },
            new object[] {  1.6, PI   },
            new object[] {  1.6, 5.0  },
            new object[] {  1.6, 17.0 },
            new object[] {    E, 1.6  },
            new object[] {    E, PI   },
            new object[] {    E, 5.0  },
            new object[] {    E, 17.0 },
            new object[] {   PI, 1.6  },
            new object[] {   PI, E    },
            new object[] {   PI, 5.0  },
            new object[] {   PI, 17.0 },
            new object[] {  5.0, 1.6  },
            new object[] {  5.0, E    },
            new object[] {  5.0, PI   },
            new object[] {  5.0, 17.0 },
            new object[] { 17.0, 1.6  },
            new object[] { 17.0, E    },
            new object[] { 17.0, PI   },
            new object[] { 17.0, 5.0  },
        };
        
        // ncrunch: no coverage end
    } 
}