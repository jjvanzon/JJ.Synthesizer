using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Testing.Core.AssertHelperCore;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class SamplingRateWishesTests
    {
        
        [DataTestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_SamplingRate(int? init)
        {
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, Coalesce(init));
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_SamplingRate(int? init, int? value)
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SamplingRate    (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SamplingRate    (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SamplingRate    (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetSamplingRate (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetSamplingRate (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetSamplingRate (value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SamplingRate    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SamplingRate    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SamplingRate    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithSamplingRate(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithSamplingRate(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithSamplingRate(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetSamplingRate (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetSamplingRate (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetSamplingRate (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SamplingRate    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SamplingRate    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SamplingRate    (x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithSamplingRate(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithSamplingRate(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithSamplingRate(x.SynthBound.ConfigResolver, value)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetSamplingRate (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetSamplingRate (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetSamplingRate (x.SynthBound.ConfigResolver, value)));
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

            AssertProp(x =>                                         x.TapeBound.TapeConfig.SamplingRate = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetSamplingRate(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SamplingRate    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SamplingRate    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SamplingRate    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SamplingRate    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithSamplingRate(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithSamplingRate(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithSamplingRate(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithSamplingRate(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetSamplingRate (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetSamplingRate (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetSamplingRate (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetSamplingRate (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SamplingRate    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SamplingRate    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SamplingRate    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SamplingRate    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithSamplingRate(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithSamplingRate(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithSamplingRate(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithSamplingRate(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetSamplingRate (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetSamplingRate (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetSamplingRate (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetSamplingRate (x.TapeBound.TapeAction , value)));
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

            AssertProp(x =>                                             x.BuffBound.AudioFileOutput.SamplingRate = value);
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SamplingRate    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SamplingRate    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithSamplingRate(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetSamplingRate (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetSamplingRate (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SamplingRate    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SamplingRate    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithSamplingRate(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithSamplingRate(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetSamplingRate (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetSamplingRate (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SamplingRate    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SamplingRate    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithSamplingRate(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithSamplingRate(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetSamplingRate (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetSamplingRate (x.BuffBound.AudioFileOutput, value)));
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

                AssertProp(() =>                                      x.Independent.Sample.SamplingRate = value);
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SamplingRate    (value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithSamplingRate(value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetSamplingRate (value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SamplingRate    (x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithSamplingRate(x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetSamplingRate (x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SamplingRate    (x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithSamplingRate(x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetSamplingRate (x.Independent.Sample, value)));
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

                AssertProp(() =>                                             x.Independent.AudioInfoWish.SamplingRate = value);
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SamplingRate    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithSamplingRate(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetSamplingRate (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SamplingRate    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithSamplingRate(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetSamplingRate (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SamplingRate    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithSamplingRate(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetSamplingRate (x.Independent.AudioInfoWish, value)));
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

                AssertProp(() =>                                             x.Independent.AudioFileInfo.SamplingRate = value);
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SamplingRate    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithSamplingRate(value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetSamplingRate (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SamplingRate    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithSamplingRate(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetSamplingRate (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SamplingRate    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithSamplingRate(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetSamplingRate (x.Independent.AudioFileInfo, value)));
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
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    Assert_Immutable_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }

                AssertProp(() => x.Immutable.WavHeader.SamplingRate    (value));
                AssertProp(() => x.Immutable.WavHeader.WithSamplingRate(value));
                AssertProp(() => x.Immutable.WavHeader.SetSamplingRate (value));
                AssertProp(() => SamplingRate    (x.Immutable.WavHeader, value));
                AssertProp(() => WithSamplingRate(x.Immutable.WavHeader, value));
                AssertProp(() => SetSamplingRate (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SamplingRate    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithSamplingRate(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetSamplingRate (x.Immutable.WavHeader, value));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders.ForEach(w => Assert_Immutable_Getters(w, value));
        }

        [TestMethod]
        public void ConfigSection_SamplingRate()
        {
            // Immutable. Get-only.
            var x = CreateTestEntities(default);

            AreEqual(DefaultSamplingRate, () => x.SynthBound.ConfigSection.SamplingRate);
            AreEqual(DefaultSamplingRate, () => x.SynthBound.ConfigSection.SamplingRate   ());
            AreEqual(DefaultSamplingRate, () => x.SynthBound.ConfigSection.GetSamplingRate());
            AreEqual(DefaultSamplingRate, () => SamplingRate   (x.SynthBound.ConfigSection));
            AreEqual(DefaultSamplingRate, () => GetSamplingRate(x.SynthBound.ConfigSection));
            AreEqual(DefaultSamplingRate, () => ConfigWishesAccessor.SamplingRate   (x.SynthBound.ConfigSection));
            AreEqual(DefaultSamplingRate, () => ConfigWishesAccessor.GetSamplingRate(x.SynthBound.ConfigSection));
        }

        [TestMethod]
        public void Default_SamplingRate()
        {
            AreEqual(48000, () => DefaultSamplingRate);
        }

        // Getter Helpers
        
        internal static void Assert_All_Getters(TestEntities x, int samplingRate)
        {
            Assert_Bound_Getters(x, samplingRate);
            Assert_Independent_Getters(x, samplingRate);
            Assert_Immutable_Getters(x, samplingRate);
        }

        private static void Assert_Bound_Getters(TestEntities x, int samplingRate)
        {
            Assert_SynthBound_Getters(x, samplingRate);
            Assert_TapeBound_Getters(x, samplingRate);
            Assert_BuffBound_Getters(x, samplingRate);
        }
        
        private static void Assert_Independent_Getters(TestEntities x, int samplingRate)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, samplingRate);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, samplingRate);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, samplingRate);
        }

        private static void Assert_Immutable_Getters(TestEntities x, int samplingRate)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, samplingRate);
        }

        private static void Assert_SynthBound_Getters(TestEntities x, int samplingRate)
        {
            AreEqual(samplingRate, () => x.SynthBound.SynthWishes   .GetSamplingRate);
            AreEqual(samplingRate, () => x.SynthBound.FlowNode      .GetSamplingRate);
            AreEqual(samplingRate, () => x.SynthBound.ConfigResolver.GetSamplingRate);
            AreEqual(samplingRate, () => x.SynthBound.SynthWishes   .SamplingRate   ());
            AreEqual(samplingRate, () => x.SynthBound.FlowNode      .SamplingRate   ());
            AreEqual(samplingRate, () => x.SynthBound.ConfigResolver.SamplingRate   ());
            AreEqual(samplingRate, () => x.SynthBound.SynthWishes   .GetSamplingRate());
            AreEqual(samplingRate, () => x.SynthBound.FlowNode      .GetSamplingRate());
            AreEqual(samplingRate, () => x.SynthBound.ConfigResolver.GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (x.SynthBound.SynthWishes   ));
            AreEqual(samplingRate, () => SamplingRate   (x.SynthBound.FlowNode      ));
            AreEqual(samplingRate, () => SamplingRate   (x.SynthBound.ConfigResolver));
            AreEqual(samplingRate, () => GetSamplingRate(x.SynthBound.SynthWishes   ));
            AreEqual(samplingRate, () => GetSamplingRate(x.SynthBound.FlowNode      ));
            AreEqual(samplingRate, () => GetSamplingRate(x.SynthBound.ConfigResolver));
            AreEqual(samplingRate, () => ConfigWishes        .SamplingRate   (x.SynthBound.SynthWishes   ));
            AreEqual(samplingRate, () => ConfigWishes        .SamplingRate   (x.SynthBound.FlowNode      ));
            AreEqual(samplingRate, () => ConfigWishesAccessor.SamplingRate   (x.SynthBound.ConfigResolver));
            AreEqual(samplingRate, () => ConfigWishes        .GetSamplingRate(x.SynthBound.SynthWishes   ));
            AreEqual(samplingRate, () => ConfigWishes        .GetSamplingRate(x.SynthBound.FlowNode      ));
            AreEqual(samplingRate, () => ConfigWishesAccessor.GetSamplingRate(x.SynthBound.ConfigResolver));
        }
        
        private static void Assert_TapeBound_Getters(TestEntities x, int samplingRate)
        {
            AreEqual(samplingRate, () => x.TapeBound.TapeConfig .SamplingRate);
            AreEqual(samplingRate, () => x.TapeBound.Tape       .SamplingRate   ());
            AreEqual(samplingRate, () => x.TapeBound.TapeConfig .SamplingRate   ());
            AreEqual(samplingRate, () => x.TapeBound.TapeActions.SamplingRate   ());
            AreEqual(samplingRate, () => x.TapeBound.TapeAction .SamplingRate   ());
            AreEqual(samplingRate, () => x.TapeBound.Tape       .GetSamplingRate());
            AreEqual(samplingRate, () => x.TapeBound.TapeConfig .GetSamplingRate());
            AreEqual(samplingRate, () => x.TapeBound.TapeActions.GetSamplingRate());
            AreEqual(samplingRate, () => x.TapeBound.TapeAction .GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (x.TapeBound.Tape       ));
            AreEqual(samplingRate, () => SamplingRate   (x.TapeBound.TapeConfig ));
            AreEqual(samplingRate, () => SamplingRate   (x.TapeBound.TapeActions));
            AreEqual(samplingRate, () => SamplingRate   (x.TapeBound.TapeAction ));
            AreEqual(samplingRate, () => GetSamplingRate(x.TapeBound.Tape       ));
            AreEqual(samplingRate, () => GetSamplingRate(x.TapeBound.TapeConfig ));
            AreEqual(samplingRate, () => GetSamplingRate(x.TapeBound.TapeActions));
            AreEqual(samplingRate, () => GetSamplingRate(x.TapeBound.TapeAction ));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (x.TapeBound.Tape       ));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (x.TapeBound.TapeConfig ));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (x.TapeBound.TapeActions));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (x.TapeBound.TapeAction ));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(x.TapeBound.Tape       ));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(x.TapeBound.TapeConfig ));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(x.TapeBound.TapeActions));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(x.TapeBound.TapeAction ));
        }
        
        private static void Assert_BuffBound_Getters(TestEntities x, int samplingRate)
        {
            AreEqual(samplingRate, () => x.BuffBound.AudioFileOutput.SamplingRate);
            AreEqual(samplingRate, () => x.BuffBound.Buff           .SamplingRate   ());
            AreEqual(samplingRate, () => x.BuffBound.AudioFileOutput.SamplingRate   ());
            AreEqual(samplingRate, () => x.BuffBound.Buff           .GetSamplingRate());
            AreEqual(samplingRate, () => x.BuffBound.AudioFileOutput.GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (x.BuffBound.Buff           ));
            AreEqual(samplingRate, () => SamplingRate   (x.BuffBound.AudioFileOutput));
            AreEqual(samplingRate, () => GetSamplingRate(x.BuffBound.Buff           ));
            AreEqual(samplingRate, () => GetSamplingRate(x.BuffBound.AudioFileOutput));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (x.BuffBound.Buff           ));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (x.BuffBound.AudioFileOutput));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(x.BuffBound.Buff           ));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(x.BuffBound.AudioFileOutput));
        }
        
        private static void Assert_Independent_Getters(Sample sample, int samplingRate)
        {
            AreEqual(samplingRate, () => sample.SamplingRate);
            AreEqual(samplingRate, () => sample.SamplingRate   ());
            AreEqual(samplingRate, () => sample.GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (sample));
            AreEqual(samplingRate, () => GetSamplingRate(sample));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (sample));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(sample));
        }
        
        private static void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int samplingRate)
        {
            AreEqual(samplingRate, () => audioFileInfo.SamplingRate);
            AreEqual(samplingRate, () => audioFileInfo.SamplingRate   ());
            AreEqual(samplingRate, () => audioFileInfo.GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (audioFileInfo));
            AreEqual(samplingRate, () => GetSamplingRate(audioFileInfo));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (audioFileInfo));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(audioFileInfo));
        }
        private static void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int samplingRate)
        {
            AreEqual(samplingRate, () => audioInfoWish.SamplingRate);
            AreEqual(samplingRate, () => audioInfoWish.SamplingRate   ());
            AreEqual(samplingRate, () => audioInfoWish.GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (audioInfoWish));
            AreEqual(samplingRate, () => GetSamplingRate(audioInfoWish));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (audioInfoWish));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(audioInfoWish));
        }

        private static void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int samplingRate)
        {
            AreEqual(samplingRate, () => wavHeader.SamplingRate);
            AreEqual(samplingRate, () => wavHeader.SamplingRate   ());
            AreEqual(samplingRate, () => wavHeader.GetSamplingRate());
            AreEqual(samplingRate, () => SamplingRate   (wavHeader));
            AreEqual(samplingRate, () => GetSamplingRate(wavHeader));
            AreEqual(samplingRate, () => ConfigWishes.SamplingRate   (wavHeader));
            AreEqual(samplingRate, () => ConfigWishes.GetSamplingRate(wavHeader));
        }
 
        // Test Data Helpers
        
        private TestEntities CreateTestEntities(int? samplingRate, [CallerMemberName] string name = null)
        {
            double audioLength = DefaultAudioLength;
            if (samplingRate > 100) audioLength = 0.001; // Tame audio length in case of larger sampling rates for performance.
            return new TestEntities(name, x =>
            {
                x.NoLog();
                x.WithSamplingRate(samplingRate);
                x.WithAudioLength(audioLength);
                x.IsUnderNCrunch = true;
                x.IsUnderAzurePipelines = false;
            });
        }
                
        private int Coalesce(int? samplingRate) => CoalesceSamplingRate(samplingRate, defaultValue: 10);

        // ncrunch: no coverage start
        
        static IEnumerable<object[]> TestParametersInit => new[]
        {
            new object[] { 0 },
            new object[] { null },
            new object[] { 96000 },
            new object[] { 88200 },
            new object[] { 48000 },
            new object[] { 44100 },
            new object[] { 22050 },
            new object[] { 11025 },
            new object[] { 1 },
            new object[] { 8 },
            new object[] { 16 },
            new object[] { 32 },
            new object[] { 64 },
            new object[] { 100 },
            new object[] { 1000 },
            new object[] { 12345 },
            new object[] { 1234567 } 
        };
        
        static IEnumerable<object[]> TestParametersWithEmpty => new[]
        {
            new object[] {   22050 ,     0 },
            new object[] {       0 , 11025 },
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

        // ncrunch: no coverage end
    } 
}