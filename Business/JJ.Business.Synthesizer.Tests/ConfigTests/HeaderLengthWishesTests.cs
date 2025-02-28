using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class HeaderLengthWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_HeaderLength(int headerLength, int? audioFormatInt)
        { 
            var init = (headerLength, audioFormat: (AudioFileFormatEnum?)audioFormatInt);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.headerLength);
        }

        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_HeaderLength(int initHeaderLength, int? initAudioFormatInt, int headerLength, int? audioFormatInt)
        {
            var init = (headerLength: initHeaderLength, audioFormat: (AudioFileFormatEnum?)initAudioFormatInt);
            var val  = (headerLength, audioFormat: (AudioFileFormatEnum?)audioFormatInt);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.headerLength);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, val.headerLength);
                Assert_TapeBound_Getters(x, init.headerLength);
                Assert_BuffBound_Getters(x, init.headerLength);
                Assert_Independent_Getters(x, init.headerLength);
                Assert_Immutable_Getters(x, init.headerLength);
                
                x.Record();
                Assert_All_Getters(x, val.headerLength);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    HeaderLength    (x.SynthBound.SynthWishes   , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       HeaderLength    (x.SynthBound.FlowNode      , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, HeaderLength    (x.SynthBound.ConfigResolver, val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    WithHeaderLength(x.SynthBound.SynthWishes   , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       WithHeaderLength(x.SynthBound.FlowNode      , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, WithHeaderLength(x.SynthBound.ConfigResolver, val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    SetHeaderLength (x.SynthBound.SynthWishes   , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       SetHeaderLength (x.SynthBound.FlowNode      , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, SetHeaderLength (x.SynthBound.ConfigResolver, val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .HeaderLength    (x.SynthBound.SynthWishes   , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .HeaderLength    (x.SynthBound.FlowNode      , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.HeaderLength    (x.SynthBound.ConfigResolver, val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .WithHeaderLength(x.SynthBound.SynthWishes   , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .WithHeaderLength(x.SynthBound.FlowNode      , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.WithHeaderLength(x.SynthBound.ConfigResolver, val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    ConfigWishes        .SetHeaderLength (x.SynthBound.SynthWishes   , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       ConfigWishes        .SetHeaderLength (x.SynthBound.FlowNode      , val.headerLength)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, ConfigWishesAccessor.SetHeaderLength (x.SynthBound.ConfigResolver, val.headerLength)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.SetAudioFormat(val.audioFormat)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_HeaderLength(int initHeaderLength, int initAudioFormatInt, int headerLength, int audioFormatInt)
        {
            var init = (headerLength: initHeaderLength, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (headerLength, audioFormat: (AudioFileFormatEnum)audioFormatInt);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.headerLength);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.headerLength);
                Assert_TapeBound_Getters(x, val.headerLength);
                Assert_BuffBound_Getters(x, init.headerLength);
                Assert_Independent_Getters(x, init.headerLength);
                Assert_Immutable_Getters(x, init.headerLength);
                
                x.Record();
                Assert_All_Getters(x, init.headerLength); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .HeaderLength    (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .WithHeaderLength(val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetHeaderLength (val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        HeaderLength    (x.TapeBound.Tape       , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  HeaderLength    (x.TapeBound.TapeConfig , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, HeaderLength    (x.TapeBound.TapeActions, val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  HeaderLength    (x.TapeBound.TapeAction , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        WithHeaderLength(x.TapeBound.Tape       , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  WithHeaderLength(x.TapeBound.TapeConfig , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, WithHeaderLength(x.TapeBound.TapeActions, val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  WithHeaderLength(x.TapeBound.TapeAction , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        SetHeaderLength (x.TapeBound.Tape       , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  SetHeaderLength (x.TapeBound.TapeConfig , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, SetHeaderLength (x.TapeBound.TapeActions, val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  SetHeaderLength (x.TapeBound.TapeAction , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        ConfigWishes.HeaderLength    (x.TapeBound.Tape       , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  ConfigWishes.HeaderLength    (x.TapeBound.TapeConfig , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, ConfigWishes.HeaderLength    (x.TapeBound.TapeActions, val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  ConfigWishes.HeaderLength    (x.TapeBound.TapeAction , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        ConfigWishes.WithHeaderLength(x.TapeBound.Tape       , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  ConfigWishes.WithHeaderLength(x.TapeBound.TapeConfig , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, ConfigWishes.WithHeaderLength(x.TapeBound.TapeActions, val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  ConfigWishes.WithHeaderLength(x.TapeBound.TapeAction , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        ConfigWishes.SetHeaderLength (x.TapeBound.Tape       , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  ConfigWishes.SetHeaderLength (x.TapeBound.TapeConfig , val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, ConfigWishes.SetHeaderLength (x.TapeBound.TapeActions, val.headerLength)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  ConfigWishes.SetHeaderLength (x.TapeBound.TapeAction , val.headerLength)));

            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape       .SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig .SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction .SetAudioFormat(val.audioFormat)));

        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_HeaderLength(int initHeaderLength, int initAudioFormatInt, int headerLength, int audioFormatInt)
        {
            var init = (headerLength: initHeaderLength, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (headerLength, audioFormat: (AudioFileFormatEnum)audioFormatInt);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.headerLength);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.headerLength);
                Assert_TapeBound_Getters(x, init.headerLength);
                Assert_BuffBound_Getters(x, val.headerLength);
                Assert_Independent_Getters(x, init.headerLength);
                Assert_Immutable_Getters(x, init.headerLength);
                
                x.Record();
                Assert_All_Getters(x, init.headerLength);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .HeaderLength    (val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.HeaderLength    (val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithHeaderLength(val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithHeaderLength(val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetHeaderLength (val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetHeaderLength (val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => HeaderLength    (x.BuffBound.Buff           , val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => HeaderLength    (x.BuffBound.AudioFileOutput, val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithHeaderLength(x.BuffBound.Buff           , val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithHeaderLength(x.BuffBound.AudioFileOutput, val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetHeaderLength (x.BuffBound.Buff           , val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetHeaderLength (x.BuffBound.AudioFileOutput, val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.HeaderLength    (x.BuffBound.Buff           , val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.HeaderLength    (x.BuffBound.AudioFileOutput, val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithHeaderLength(x.BuffBound.Buff           , val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithHeaderLength(x.BuffBound.AudioFileOutput, val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetHeaderLength (x.BuffBound.Buff           , val.headerLength, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetHeaderLength (x.BuffBound.AudioFileOutput, val.headerLength, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetAudioFormat(val.audioFormat, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetAudioFormat(val.audioFormat, x.SynthBound.Context)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_HeaderLength(int initHeaderLength, int initAudioFormatInt, int headerLength, int audioFormatInt)
        {
            // Independent after Taping
            
            var init = (headerLength: initHeaderLength, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (headerLength, audioFormat: (AudioFileFormatEnum)audioFormatInt);

            // Sample
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.headerLength);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.headerLength);
                    Assert_Independent_Getters(x.Independent.Sample, val.headerLength);
                    Assert_Immutable_Getters(x, init.headerLength);

                    x.Record();
                    Assert_All_Getters(x, init.headerLength);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.HeaderLength    (val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithHeaderLength(val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetHeaderLength (val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => HeaderLength    (x.Independent.Sample, val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithHeaderLength(x.Independent.Sample, val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetHeaderLength (x.Independent.Sample, val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.HeaderLength    (x.Independent.Sample, val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithHeaderLength(x.Independent.Sample, val.headerLength, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetHeaderLength (x.Independent.Sample, val.headerLength, x.SynthBound.Context)));
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetAudioFormat(val.audioFormat, x.SynthBound.Context)));
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_HeaderLength(int initHeaderLength, int initAudioFormatInt, int headerLength, int audioFormatInt)
        {
            var init = (headerLength: initHeaderLength, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (headerLength, audioFormat: (AudioFileFormatEnum)audioFormatInt);
            
            var x = CreateTestEntities(init);

            // AudioFileFormatEnum
            
            var audioFormats = new List<AudioFileFormatEnum>();
            {
                void AssertProp(Func<AudioFileFormatEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init.headerLength);
                    
                    AudioFileFormatEnum audioFormat2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init.headerLength);
                    Assert_Immutable_Getters(audioFormat2, val.headerLength);
                    
                    audioFormats.Add(audioFormat2);
                }

                AssertProp(() => x.Immutable.AudioFormat.HeaderLength    (val.headerLength));
                AssertProp(() => x.Immutable.AudioFormat.WithHeaderLength(val.headerLength));
                AssertProp(() => x.Immutable.AudioFormat.SetHeaderLength (val.headerLength));
                AssertProp(() => HeaderLength                 (x.Immutable.AudioFormat, val.headerLength));
                AssertProp(() => WithHeaderLength             (x.Immutable.AudioFormat, val.headerLength));
                AssertProp(() => SetHeaderLength              (x.Immutable.AudioFormat, val.headerLength));
                AssertProp(() => ConfigWishes.HeaderLength    (x.Immutable.AudioFormat, val.headerLength));
                AssertProp(() => ConfigWishes.WithHeaderLength(x.Immutable.AudioFormat, val.headerLength));
                AssertProp(() => ConfigWishes.SetHeaderLength (x.Immutable.AudioFormat, val.headerLength));
                AssertProp(() => val.headerLength.AudioFormat              ());
                AssertProp(() => val.headerLength.ToAudioFormat            ());
                AssertProp(() => val.headerLength.HeaderLengthToAudioFormat());
                AssertProp(() => AudioFormat                           (val.headerLength));
                AssertProp(() => ToAudioFormat                         (val.headerLength));
                AssertProp(() => HeaderLengthToAudioFormat             (val.headerLength));
                AssertProp(() => ConfigWishes.AudioFormat              (val.headerLength));
                AssertProp(() => ConfigWishes.ToAudioFormat            (val.headerLength));
                AssertProp(() => ConfigWishes.HeaderLengthToAudioFormat(val.headerLength));
            }

            // AudioFormat Entity
            
            var audioFormatEntities = new List<AudioFileFormat>();
            {
                void AssertProp(Func<AudioFileFormat> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init.headerLength);

                    AudioFileFormat audioFormatEntity2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init.headerLength);
                    Assert_Immutable_Getters(audioFormatEntity2, val.headerLength);
                    
                    audioFormatEntities.Add(audioFormatEntity2);
                }


                AssertProp(() => x.Immutable.AudioFormatEntity.HeaderLength    (val.headerLength, x.SynthBound.Context));
                AssertProp(() => x.Immutable.AudioFormatEntity.WithHeaderLength(val.headerLength, x.SynthBound.Context));
                AssertProp(() => x.Immutable.AudioFormatEntity.SetHeaderLength (val.headerLength, x.SynthBound.Context));
                AssertProp(() => HeaderLength                 (x.Immutable.AudioFormatEntity, val.headerLength, x.SynthBound.Context));
                AssertProp(() => WithHeaderLength             (x.Immutable.AudioFormatEntity, val.headerLength, x.SynthBound.Context));
                AssertProp(() => SetHeaderLength              (x.Immutable.AudioFormatEntity, val.headerLength, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.HeaderLength    (x.Immutable.AudioFormatEntity, val.headerLength, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithHeaderLength(x.Immutable.AudioFormatEntity, val.headerLength, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetHeaderLength (x.Immutable.AudioFormatEntity, val.headerLength, x.SynthBound.Context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.headerLength);
            
            // Except for our variables
            audioFormats       .ForEach(e => Assert_Immutable_Getters(e, val.headerLength));
            audioFormatEntities.ForEach(s => Assert_Immutable_Getters(s, val.headerLength));
        }

        [TestMethod] 
        public void ConfigSection_HeaderLength()
        {
            // Immutable. Get-only.
            var x = CreateTestEntities(default);
            AreEqual(DefaultHeaderLength, () => x.SynthBound.ConfigSection.HeaderLength());
            AreEqual(DefaultHeaderLength, () => x.SynthBound.ConfigSection.GetHeaderLength());
        }

        [TestMethod] 
        public void Default_HeaderLength()
        {
            AreEqual(44, () => DefaultHeaderLength);
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, int headerLength)
        {
            Assert_Bound_Getters(x, headerLength);
            Assert_Independent_Getters(x, headerLength);
            Assert_Immutable_Getters(x, headerLength);
        }

        private void Assert_Bound_Getters(TestEntities x, int headerLength)
        {
            Assert_SynthBound_Getters(x, headerLength);
            Assert_TapeBound_Getters(x, headerLength);
            Assert_BuffBound_Getters(x, headerLength);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int headerLength)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, headerLength);
        }

        private void Assert_Immutable_Getters(TestEntities x, int headerLength)
        {
            Assert_Immutable_Getters(x.Immutable.AudioFormat, headerLength);
            Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, headerLength);
            Assert_Immutable_Getters(x.Immutable.WavHeader, headerLength);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int headerLength)
        {
            AreEqual(headerLength, () => x.SynthBound.SynthWishes   .GetHeaderLength);
            AreEqual(headerLength, () => x.SynthBound.FlowNode      .GetHeaderLength);
            AreEqual(headerLength, () => x.SynthBound.SynthWishes   .GetHeaderLength());
            AreEqual(headerLength, () => x.SynthBound.FlowNode      .GetHeaderLength());
            AreEqual(headerLength, () => x.SynthBound.ConfigResolver.GetHeaderLength());
            AreEqual(headerLength, () => x.SynthBound.SynthWishes   .HeaderLength   ());
            AreEqual(headerLength, () => x.SynthBound.FlowNode      .HeaderLength   ());
            AreEqual(headerLength, () => x.SynthBound.ConfigResolver.HeaderLength   ());
            AreEqual(headerLength, () => GetHeaderLength(x.SynthBound.SynthWishes   ));
            AreEqual(headerLength, () => GetHeaderLength(x.SynthBound.FlowNode      ));
            AreEqual(headerLength, () => GetHeaderLength(x.SynthBound.ConfigResolver));
            AreEqual(headerLength, () => HeaderLength   (x.SynthBound.SynthWishes   ));
            AreEqual(headerLength, () => HeaderLength   (x.SynthBound.FlowNode      ));
            AreEqual(headerLength, () => HeaderLength   (x.SynthBound.ConfigResolver));
            AreEqual(headerLength, () => ConfigWishes        .GetHeaderLength(x.SynthBound.SynthWishes   ));
            AreEqual(headerLength, () => ConfigWishes        .GetHeaderLength(x.SynthBound.FlowNode      ));
            AreEqual(headerLength, () => ConfigWishesAccessor.GetHeaderLength(x.SynthBound.ConfigResolver));
            AreEqual(headerLength, () => ConfigWishes        .HeaderLength   (x.SynthBound.SynthWishes   ));
            AreEqual(headerLength, () => ConfigWishes        .HeaderLength   (x.SynthBound.FlowNode      ));
            AreEqual(headerLength, () => ConfigWishesAccessor.HeaderLength   (x.SynthBound.ConfigResolver));
        }

        private void Assert_TapeBound_Getters(TestEntities x, int headerLength)
        {
            AreEqual(headerLength, () => x.TapeBound.Tape       .HeaderLength   ());
            AreEqual(headerLength, () => x.TapeBound.TapeConfig .HeaderLength   ());
            AreEqual(headerLength, () => x.TapeBound.TapeActions.HeaderLength   ());
            AreEqual(headerLength, () => x.TapeBound.TapeAction .HeaderLength   ());
            AreEqual(headerLength, () => x.TapeBound.Tape       .GetHeaderLength());
            AreEqual(headerLength, () => x.TapeBound.TapeConfig .GetHeaderLength());
            AreEqual(headerLength, () => x.TapeBound.TapeActions.GetHeaderLength());
            AreEqual(headerLength, () => x.TapeBound.TapeAction .GetHeaderLength());
            AreEqual(headerLength, () => HeaderLength   (x.TapeBound.Tape       ));
            AreEqual(headerLength, () => HeaderLength   (x.TapeBound.TapeConfig ));
            AreEqual(headerLength, () => HeaderLength   (x.TapeBound.TapeActions));
            AreEqual(headerLength, () => HeaderLength   (x.TapeBound.TapeAction ));
            AreEqual(headerLength, () => GetHeaderLength(x.TapeBound.Tape       ));
            AreEqual(headerLength, () => GetHeaderLength(x.TapeBound.TapeConfig ));
            AreEqual(headerLength, () => GetHeaderLength(x.TapeBound.TapeActions));
            AreEqual(headerLength, () => GetHeaderLength(x.TapeBound.TapeAction ));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (x.TapeBound.Tape       ));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (x.TapeBound.TapeConfig ));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (x.TapeBound.TapeActions));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (x.TapeBound.TapeAction ));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(x.TapeBound.Tape       ));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(x.TapeBound.TapeConfig ));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(x.TapeBound.TapeActions));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(x.TapeBound.TapeAction ));
        }
                        
        private void Assert_BuffBound_Getters(TestEntities x, int headerLength)
        {
            AreEqual(headerLength, () => x.BuffBound.Buff           .HeaderLength   ());
            AreEqual(headerLength, () => x.BuffBound.AudioFileOutput.HeaderLength   ());
            AreEqual(headerLength, () => x.BuffBound.Buff           .GetHeaderLength());
            AreEqual(headerLength, () => x.BuffBound.AudioFileOutput.GetHeaderLength());
            AreEqual(headerLength, () => HeaderLength   (x.BuffBound.Buff           ));
            AreEqual(headerLength, () => HeaderLength   (x.BuffBound.AudioFileOutput));
            AreEqual(headerLength, () => GetHeaderLength(x.BuffBound.Buff           ));
            AreEqual(headerLength, () => GetHeaderLength(x.BuffBound.AudioFileOutput));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (x.BuffBound.Buff           ));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (x.BuffBound.AudioFileOutput));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(x.BuffBound.Buff           ));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(x.BuffBound.AudioFileOutput));
        }

        private void Assert_Independent_Getters(Sample sample, int headerLength)
        {
            AreEqual(headerLength, () => sample.HeaderLength   ());
            AreEqual(headerLength, () => sample.GetHeaderLength());
            AreEqual(headerLength, () => HeaderLength   (sample));
            AreEqual(headerLength, () => GetHeaderLength(sample));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength   (sample));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(sample));
        }
        
        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, int headerLength)
        {
            AreEqual(headerLength, () => audioFileFormatEnum.HeaderLength());
            AreEqual(headerLength, () => audioFileFormatEnum.GetHeaderLength());
            AreEqual(headerLength, () => audioFileFormatEnum.ToHeaderLength());
            AreEqual(headerLength, () => audioFileFormatEnum.AudioFormatToHeaderLength());
            AreEqual(headerLength, () => HeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => GetHeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => ToHeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => AudioFormatToHeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => ConfigWishes.ToHeaderLength(audioFileFormatEnum));
            AreEqual(headerLength, () => ConfigWishes.AudioFormatToHeaderLength(audioFileFormatEnum));
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, int headerLength)
        {
            IsNotNull(() => audioFormatEntity);
            AreEqual(headerLength, () => audioFormatEntity.HeaderLength());
            AreEqual(headerLength, () => audioFormatEntity.ToHeaderLength());
            AreEqual(headerLength, () => audioFormatEntity.GetHeaderLength());
            AreEqual(headerLength, () => HeaderLength(audioFormatEntity));
            AreEqual(headerLength, () => ToHeaderLength(audioFormatEntity));
            AreEqual(headerLength, () => GetHeaderLength(audioFormatEntity));
            AreEqual(headerLength, () => ConfigWishes.HeaderLength(audioFormatEntity));
            AreEqual(headerLength, () => ConfigWishes.ToHeaderLength(audioFormatEntity));
            AreEqual(headerLength, () => ConfigWishes.GetHeaderLength(audioFormatEntity));
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int headerLength)
        {
            if (headerLength == 44)
            {
                NotEqual(default, () => wavHeader);
                IsTrue(() => wavHeader.IsWav());
                AreEqual(headerLength, () => wavHeader.HeaderLength());
                AreEqual(headerLength, () => wavHeader.GetHeaderLength());
            }
            else
            {
                AreEqual(default, () => wavHeader);
                IsFalse(() => wavHeader.IsRaw());
                NotEqual(headerLength, () => wavHeader.HeaderLength());
                NotEqual(headerLength, () => wavHeader.GetHeaderLength());
            }
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities((int headerLength, AudioFileFormatEnum? audioFormat) init) 
            => new TestEntities(x => x.WithLoggingDisabled().AudioFormat(init.audioFormat).SamplingRate(HighPerfHz));
        
        // ncrunch: no coverage start
        
        static object TestParametersInit => new[] 
        {
            new object[] { 44,  null    },
            new object[] {  0, (int)Raw },
            new object[] { 44, (int)Wav }
        };
        
        static object TestParametersWithEmpty => new[]
        {
            new object[] { 44,  null   ,  0, (int)Raw },
            new object[] {  0, (int)Raw, 44,  null    },
            new object[] {  0, (int)Raw, 44, (int)Wav },
            new object[] { 44, (int)Wav,  0, (int)Raw }
        };
        
        static object TestParameters => new[]
        {
            new object[] {  0, (int)Raw, 44, (int)Wav },
            new object[] { 44, (int)Wav,  0, (int)Raw }
        };
        
        // ncrunch: no coverage end
    } 
}