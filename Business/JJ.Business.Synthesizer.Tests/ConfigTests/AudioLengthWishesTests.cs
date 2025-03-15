using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Business.Synthesizer.Wishes.Logging;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Math;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Testing.Core.AssertWishes;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Nully.Core.FilledInWishes;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using System.Runtime.CompilerServices;
// ReSharper disable ArrangeStaticMemberQualifier

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
            void AssertProp(Action<TestEntities> setter)
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioLength    (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioLength    (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioLength    (value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioLength(value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetAudioLength (value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetAudioLength (value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetAudioLength (value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    AudioLength    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       AudioLength    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, AudioLength    (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithAudioLength(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithAudioLength(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithAudioLength(x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetAudioLength (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetAudioLength (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetAudioLength (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .AudioLength    (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .AudioLength    (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.AudioLength    (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithAudioLength(x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithAudioLength(x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithAudioLength(x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetAudioLength (x.SynthBound.SynthWishes   , value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetAudioLength (x.SynthBound.FlowNode      , value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetAudioLength (x.SynthBound.ConfigResolver, value, x.SynthBound.SynthWishes)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_AudioLength(double init, double value)
        {
            void AssertProp(Action<TestEntities> setter)
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

            AssertProp(x =>                                         x.TapeBound.Tape       .Duration = value);
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetAudioLength(value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => AudioLength    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => AudioLength    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => AudioLength    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => AudioLength    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithAudioLength(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithAudioLength(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithAudioLength(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithAudioLength(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetAudioLength (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetAudioLength (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetAudioLength (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetAudioLength (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AudioLength    (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AudioLength    (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AudioLength    (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AudioLength    (x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithAudioLength(x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithAudioLength(x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithAudioLength(x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithAudioLength(x.TapeBound.TapeAction , value)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetAudioLength (x.TapeBound.Tape       , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetAudioLength (x.TapeBound.TapeConfig , value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetAudioLength (x.TapeBound.TapeActions, value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetAudioLength (x.TapeBound.TapeAction , value)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_AudioLength(double init, double value)
        {
            void AssertProp(Action<TestEntities> setter)
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

            AssertProp(x =>                                             x.BuffBound.AudioFileOutput.Duration = value);
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AudioLength    (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AudioLength    (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithAudioLength(value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithAudioLength(value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetAudioLength (value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetAudioLength (value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => AudioLength    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => AudioLength    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithAudioLength(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithAudioLength(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetAudioLength (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetAudioLength (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AudioLength    (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AudioLength    (x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithAudioLength(x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithAudioLength(x.BuffBound.AudioFileOutput, value)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetAudioLength (x.BuffBound.Buff           , value)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetAudioLength (x.BuffBound.AudioFileOutput, value)));
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
                    LogTolerance(x, init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioLength    (value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithAudioLength(value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetAudioLength (value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AudioLength    (x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithAudioLength(x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetAudioLength (x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AudioLength    (x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithAudioLength(x.Independent.Sample, value)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetAudioLength (x.Independent.Sample, value)));
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    LogTolerance(x, init);
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

                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.AudioLength    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.WithAudioLength(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SetAudioLength (value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => AudioLength    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => WithAudioLength(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => SetAudioLength (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.AudioLength    (x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.WithAudioLength(x.Independent.AudioInfoWish, value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => ConfigWishes.SetAudioLength (x.Independent.AudioInfoWish, value)));
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    LogTolerance(x, init);
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

                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.AudioLength    (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.WithAudioLength(value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SetAudioLength (value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => AudioLength    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => WithAudioLength(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => SetAudioLength (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.AudioLength    (x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.WithAudioLength(x.Independent.AudioFileInfo, value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => ConfigWishes.SetAudioLength (x.Independent.AudioFileInfo, value)));
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
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init);
                    Assert_Immutable_Getters(wavHeader2, value);
                    
                    wavHeaders.Add(wavHeader2);
                }

                AssertProp(() => x.Immutable.WavHeader.AudioLength    (value));
                AssertProp(() => x.Immutable.WavHeader.WithAudioLength(value));
                AssertProp(() => x.Immutable.WavHeader.SetAudioLength (value));
                AssertProp(() => AudioLength    (x.Immutable.WavHeader, value));
                AssertProp(() => WithAudioLength(x.Immutable.WavHeader, value));
                AssertProp(() => SetAudioLength (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.AudioLength    (x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.WithAudioLength(x.Immutable.WavHeader, value));
                AssertProp(() => ConfigWishes.SetAudioLength (x.Immutable.WavHeader, value));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders.ForEach(w => Assert_Immutable_Getters(w, value));
        }

        [TestMethod]
        public void ConfigSection_AudioLength()
        {
            // Get-only.
            var configSection = CreateTestEntities().SynthBound.ConfigSection;
            AreEqual(DefaultAudioLength, () => configSection.AudioLength);
            AreEqual(DefaultAudioLength, () => configSection.AudioLength());
            AreEqual(DefaultAudioLength, () => configSection.GetAudioLength());
            AreEqual(DefaultAudioLength, () => AudioLength(configSection));
            AreEqual(DefaultAudioLength, () => GetAudioLength(configSection));
            AreEqual(DefaultAudioLength, () => ConfigWishesAccessor.AudioLength(configSection));
            AreEqual(DefaultAudioLength, () => ConfigWishesAccessor.GetAudioLength(configSection));
        }

        [TestMethod]
        public void Default_AudioLength()
        {
            AreEqual(1, () => DefaultAudioLength);
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
            Assert_Immutable_Getters(x.Immutable.WavHeader, audioLength);
        }

        private void Assert_SynthBound_Getters(TestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.SynthBound.SynthWishes   .AudioLength   ().Value);
            AreEqual(audioLength, () => x.SynthBound.FlowNode      .AudioLength   ().Value);
            AreEqual(audioLength, () => x.SynthBound.ConfigResolver.AudioLength   (x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => x.SynthBound.SynthWishes   .GetAudioLength().Value);
            AreEqual(audioLength, () => x.SynthBound.FlowNode      .GetAudioLength().Value);
            AreEqual(audioLength, () => x.SynthBound.ConfigResolver.GetAudioLength(x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => AudioLength   (x.SynthBound.SynthWishes   ).Value);
            AreEqual(audioLength, () => AudioLength   (x.SynthBound.FlowNode      ).Value);
            AreEqual(audioLength, () => AudioLength   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => GetAudioLength(x.SynthBound.SynthWishes   ).Value);
            AreEqual(audioLength, () => GetAudioLength(x.SynthBound.FlowNode      ).Value);
            AreEqual(audioLength, () => GetAudioLength(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => ConfigWishes        .AudioLength   (x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => ConfigWishes        .AudioLength   (x.SynthBound.FlowNode   ).Value);
            AreEqual(audioLength, () => ConfigWishesAccessor.AudioLength   (x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => ConfigWishes        .GetAudioLength(x.SynthBound.SynthWishes).Value);
            AreEqual(audioLength, () => ConfigWishes        .GetAudioLength(x.SynthBound.FlowNode   ).Value);
            AreEqual(audioLength, () => ConfigWishesAccessor.GetAudioLength(x.SynthBound.ConfigResolver, x.SynthBound.SynthWishes).Value);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.TapeBound.Tape.Duration);
            AreEqual(audioLength, () => x.TapeBound.Tape       .AudioLength   ());
            AreEqual(audioLength, () => x.TapeBound.TapeConfig .AudioLength   ());
            AreEqual(audioLength, () => x.TapeBound.TapeActions.AudioLength   ());
            AreEqual(audioLength, () => x.TapeBound.TapeAction .AudioLength   ());
            AreEqual(audioLength, () => x.TapeBound.Tape       .GetAudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeConfig .GetAudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeActions.GetAudioLength());
            AreEqual(audioLength, () => x.TapeBound.TapeAction .GetAudioLength());
            AreEqual(audioLength, () => AudioLength   (x.TapeBound.Tape       ));
            AreEqual(audioLength, () => AudioLength   (x.TapeBound.TapeConfig ));
            AreEqual(audioLength, () => AudioLength   (x.TapeBound.TapeActions));
            AreEqual(audioLength, () => AudioLength   (x.TapeBound.TapeAction ));
            AreEqual(audioLength, () => GetAudioLength(x.TapeBound.Tape       ));
            AreEqual(audioLength, () => GetAudioLength(x.TapeBound.TapeConfig ));
            AreEqual(audioLength, () => GetAudioLength(x.TapeBound.TapeActions));
            AreEqual(audioLength, () => GetAudioLength(x.TapeBound.TapeAction ));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (x.TapeBound.Tape       ));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (x.TapeBound.TapeConfig ));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (x.TapeBound.TapeActions));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (x.TapeBound.TapeAction ));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(x.TapeBound.Tape       ));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(x.TapeBound.TapeConfig ));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(x.TapeBound.TapeActions));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(x.TapeBound.TapeAction ));
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, double audioLength)
        {
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.Duration);
            AreEqual(audioLength, () => x.BuffBound.Buff           .AudioLength());
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.AudioLength());
            AreEqual(audioLength, () => x.BuffBound.Buff           .GetAudioLength());
            AreEqual(audioLength, () => x.BuffBound.AudioFileOutput.GetAudioLength());
            AreEqual(audioLength, () => AudioLength   (x.BuffBound.Buff));
            AreEqual(audioLength, () => AudioLength   (x.BuffBound.AudioFileOutput));
            AreEqual(audioLength, () => GetAudioLength(x.BuffBound.Buff));
            AreEqual(audioLength, () => GetAudioLength(x.BuffBound.AudioFileOutput));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (x.BuffBound.Buff));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (x.BuffBound.AudioFileOutput));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(x.BuffBound.Buff));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(x.BuffBound.AudioFileOutput));
        }

        private void Assert_Independent_Getters(Sample sample, double audioLength)
        {
            AreEqual(audioLength, () => sample.AudioLength(),                ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => sample.GetAudioLength(),             ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => AudioLength                (sample), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => GetAudioLength             (sample), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (sample), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(sample), ToleranceByPercent(audioLength, _tolerancePercent));
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, double audioLength)
        {
            AreEqual(audioLength, () => audioFileInfo.AudioLength   (),                ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => audioFileInfo.GetAudioLength(),                ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => AudioLength                 (audioFileInfo), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => GetAudioLength              (audioFileInfo), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.AudioLength    (audioFileInfo), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength (audioFileInfo), ToleranceByPercent(audioLength, _tolerancePercent));
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, double audioLength)
        {
            AreEqual(audioLength, () => audioInfoWish.AudioLength   (),                ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => audioInfoWish.GetAudioLength(),                ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => AudioLength                 (audioInfoWish), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => GetAudioLength              (audioInfoWish), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.AudioLength    (audioInfoWish), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength (audioInfoWish), ToleranceByPercent(audioLength, _tolerancePercent));
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, double audioLength)
        {
            AreEqual(audioLength, () => wavHeader.AudioLength      (),            ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => wavHeader.GetAudioLength   (),            ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => AudioLength                (wavHeader), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => GetAudioLength             (wavHeader), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.AudioLength   (wavHeader), ToleranceByPercent(audioLength, _tolerancePercent));
            AreEqual(audioLength, () => ConfigWishes.GetAudioLength(wavHeader), ToleranceByPercent(audioLength, _tolerancePercent));
        }
         
        // Tolerance Helpers
        
        private const double _tolerancePercent = 0.8;
        
        private double ToleranceByPercent(double value, double percent) => percent / 100 * value;

        private void LogTolerance(TestEntities x, double audioLength, string title = null)
        {
            var synthWishes = x.SynthBound.SynthWishes;
            
            if (Has(title)) x.SynthBound.SynthWishes.LogTitleStrong(title);
            
            double tolerance = ToleranceByPercent(audioLength, _tolerancePercent);
            
            LogTolerance(synthWishes, audioLength, x.Independent.AudioFileInfo.AudioLength(), tolerance, "audioFileInfo.AudioLength()");
            LogTolerance(synthWishes, audioLength, x.Independent.AudioInfoWish.AudioLength(), tolerance, "audioInfoWish.AudioLength()");
            LogTolerance(synthWishes, audioLength, x.Immutable  .WavHeader    .AudioLength(), tolerance,     "wavHeader.AudioLength()");
            LogTolerance(synthWishes, audioLength, x.Independent.Sample       .AudioLength(), tolerance,        "sample.AudioLength()");
        }

        private static void LogTolerance(SynthWishes synthWishes, double expected, double actual, double tolerance, string title)
        {
            double toleranceRequired        = actual - expected;
            double tolerancePercent         = (expected + tolerance) / expected * 100 - 100;
            double tolerancePercentRequired = actual                 / expected * 100 - 100;
            
            synthWishes.LogTitle(title);
            synthWishes.Log();
            synthWishes.Log($"expected = {expected}");
            synthWishes.Log($"  actual = {actual}");
            synthWishes.Log();
            synthWishes.Log("Tolerance:" );
            synthWishes.Log();
            synthWishes.Log($"    used = {tolerance:0.0000####}");
            synthWishes.Log($"required = {toleranceRequired:0.0000####}");
            synthWishes.Log();
            synthWishes.Log();
            synthWishes.Log($"    used = {tolerancePercent:0.###}%");
            synthWishes.Log($"required = {tolerancePercentRequired:0.###}%");
            synthWishes.Log();
        }
 
        // Test Data Helpers
        
        private const int _samplingRate = 2000;
        
        private TestEntities CreateTestEntities(double? audioLength = default, [CallerMemberName] string name = null) 
            => new TestEntities(x => x.NoLog().WithAudioLength(audioLength).WithSamplingRate(_samplingRate), name);
        
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