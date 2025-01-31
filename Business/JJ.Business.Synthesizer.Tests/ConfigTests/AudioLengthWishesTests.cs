using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Testing.AssertWishes;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Wishes.LogWishes;
using static JJ.Framework.Wishes.Common.FilledInWishes;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class AudioLengthWishesTests
    {
        
        [DataTestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_AudioLength(double? init)
        {
            var x = CreateTestEntities(init);
            LogTolerance(x, Coalesce(init));
            Assert_All_Getters(x, Coalesce(init));
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_AudioLength(double? init, double? value)
        {            
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                LogTolerance(x, Coalesce(init));
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
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                LogTolerance(x, init);
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
            void AssertProp(Action<ConfigTestEntities> setter)
            {
                var x = CreateTestEntities(init);
                LogTolerance(x, init);
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
                ConfigTestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    LogTolerance(x, init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init, x.Immutable.CourtesyFrames);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init, x.Immutable.CourtesyFrames);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioLength(value)));
            }
            
            // AudioInfoWish
            {
                ConfigTestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    LogTolerance(x, init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, value, x.Immutable.CourtesyFrames);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init, x.Immutable.CourtesyFrames);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AudioLength(value, x.Immutable.CourtesyFrames)));
            }
                        
            // AudioFileInfo
            {
                ConfigTestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    LogTolerance(x, init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value, x.Immutable.CourtesyFrames);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init, x.Immutable.CourtesyFrames);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AudioLength(value, x.Immutable.CourtesyFrames)));
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_AudioLength(double init, double value)
        {
            ConfigTestEntities x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init, x.Immutable.CourtesyFrames);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init, x.Immutable.CourtesyFrames);
                    Assert_Immutable_Getters(wavHeader2, value, x.Immutable.CourtesyFrames);
                    
                    wavHeaders.Add(wavHeader2);
                }

                AssertProp(() => x.Immutable.WavHeader.AudioLength(value, x.Immutable.CourtesyFrames));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders.ForEach(w => Assert_Immutable_Getters(w, value, x.Immutable.CourtesyFrames));
        }

        [TestMethod]
        public void ConfigSection_AudioLength()
        {
            // Get-only.
            var configSection = CreateTestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultAudioLength, () => configSection.AudioLength);
            AreEqual(DefaultAudioLength, () => configSection.AudioLength());
        }

        [TestMethod]
        public void Default_AudioLength()
        {
            AreEqual(1, () => DefaultAudioLength);
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, double audioLength)
        {
            Assert_Bound_Getters(x, audioLength);
            Assert_Independent_Getters(x, audioLength);
            Assert_Immutable_Getters(x, audioLength);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, double audioLength)
        {
            Assert_SynthBound_Getters(x, audioLength);
            Assert_TapeBound_Getters(x, audioLength);
            Assert_BuffBound_Getters(x, audioLength);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, double audioLength)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, audioLength);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, audioLength, x.Immutable.CourtesyFrames);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, audioLength, x.Immutable.CourtesyFrames);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, double audioLength)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, audioLength, x.Immutable.CourtesyFrames);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.SynthBound.SynthWishes.AudioLength());
            AreEqual(audioLength, () => x.SynthBound.FlowNode.AudioLength());
            AreEqual(audioLength, () => x.SynthBound.ConfigResolver.AudioLength(x.SynthBound.SynthWishes));
        }
        
        private void Assert_TapeBound_Getters(ConfigTestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.TapeBound.Tape.AudioLength());
            AreEqual(audioLength, () => x.TapeBound.Tape.Duration);
            AreEqual(audioLength, () => x.TapeBound.TapeConfig.AudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeActions.AudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeAction.AudioLength());
        }
        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.BuffBound.Buff.AudioLength());
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.AudioLength());
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.Duration);
        }

        private void Assert_Independent_Getters(Sample sample, double audioLength)
        {
            AreEqual(audioLength, () => sample.AudioLength(), ToleranceByPercent(audioLength, _tolerancePercent));
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, double audioLength, int courtesyFrames)
        {
            AreEqual(audioLength, () => audioFileInfo.AudioLength(courtesyFrames), ToleranceByPercent(audioLength, _tolerancePercent));
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, double audioLength, int courtesyFrames)
        {
            AreEqual(audioLength, () => audioInfoWish.AudioLength(courtesyFrames), ToleranceByPercent(audioLength, _tolerancePercent));
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, double audioLength, int courtesyFrames)
        {
            AreEqual(audioLength, () => wavHeader.AudioLength(courtesyFrames), ToleranceByPercent(audioLength, _tolerancePercent));
        }
         
        // Tolerance Helpers
        
        private const double _tolerancePercent = 0.8;
        
        private double ToleranceByPercent(double value, double percent) => percent / 100 * value;

        private void LogTolerance(ConfigTestEntities x, double audioLength, string title = null)
        {
            if (Has(title)) LogTitleStrong(title);
            
            double tolerance = ToleranceByPercent(audioLength, _tolerancePercent);
            
            LogTolerance(audioLength, x.Independent.AudioFileInfo.AudioLength(x.Immutable.CourtesyFrames), tolerance, "audioFileInfo.AudioLength()");
            LogTolerance(audioLength, x.Independent.AudioInfoWish.AudioLength(x.Immutable.CourtesyFrames), tolerance, "audioInfoWish.AudioLength()");
            LogTolerance(audioLength, x.Immutable  .WavHeader    .AudioLength(x.Immutable.CourtesyFrames), tolerance,     "wavHeader.AudioLength()");
            LogTolerance(audioLength, x.Independent.Sample       .AudioLength(                          ), tolerance,        "sample.AudioLength()");
        }

        private static void LogTolerance(double expected, double actual, double tolerance, string title)
        {
            double toleranceRequired        = actual - expected;
            double tolerancePercent         = (expected + tolerance) / expected * 100 - 100;
            double tolerancePercentRequired = actual                 / expected * 100 - 100;
                
            LogTitle(title);
            Log();
            Log($"expected = {expected}");
            Log($"  actual = {actual}");
            Log();
            Log("Tolerance:" );
            Log();
            Log($"    used = {tolerance:0.0000####}");
            Log($"required = {toleranceRequired:0.0000####}");
            Log();
            Log();
            Log($"    used = {tolerancePercent:0.###}%");
            Log($"required = {tolerancePercentRequired:0.###}%");
            Log();
        }
 
        // Test Data Helpers
        
        private const int _samplingRate = 2000;
        
        private ConfigTestEntities CreateTestEntities(double? audioLength = default) 
            => new ConfigTestEntities(x => x.WithAudioLength(audioLength).WithSamplingRate(_samplingRate));
        
        double Coalesce(double? audioLength) => CoalesceAudioLength(audioLength);

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> TestParametersInit => new[]
        {
            new object[] { null },
            //new object[] {  0.0 }, // Yields "Duration is not above 0." exception.
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
            //new object[] {  0.0, null }, // Yields "Duration is not above 0." exception.
            //new object[] { null, 0.0  }, // Yields "Duration is not above 0." exception.
            new object[] { null, 1.6  },
            new object[] {  1.6, null },
            //new object[] {  0.0, 1.6  }, // Yields "Duration is not above 0." exception.
            //new object[] {  1.6, 0.0  }, // Yields "Duration is not above 0." exception.
            
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