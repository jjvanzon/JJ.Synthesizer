using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Tests.Helpers;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Common;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Accessors.ConfigWishesAccessor;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Tests.Helpers.TestEntities;
// ReSharper disable ArrangeStaticMemberQualifier
#pragma warning disable CS0611

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
    public class FileExtensionWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_FileExtension(string fileExtension, int? audioFormatInt)
        { 
            var init = (fileExtension, audioFormat: (AudioFileFormatEnum?)audioFormatInt);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceFileExtension(init.fileExtension));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_FileExtension(string initFileExtension, int? initAudioFormatInt, string fileExtension, int? audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum?)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum?)audioFormatInt);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceFileExtension(init.fileExtension));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceFileExtension(val .fileExtension));
                Assert_TapeBound_Getters  (x, CoalesceFileExtension(init.fileExtension));
                Assert_BuffBound_Getters  (x, CoalesceFileExtension(init.fileExtension));
                Assert_Independent_Getters(x, CoalesceFileExtension(init.fileExtension));
                Assert_Immutable_Getters  (x, CoalesceFileExtension(init.fileExtension));
                
                x.Record();
                Assert_All_Getters(x, CoalesceFileExtension(val.fileExtension));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => FileExtension    (x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => FileExtension    (x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => FileExtension    (x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => WithFileExtension(x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => WithFileExtension(x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => WithFileExtension(x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => AsFileExtension  (x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => AsFileExtension  (x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => AsFileExtension  (x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => SetFileExtension (x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => SetFileExtension (x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => SetFileExtension (x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .FileExtension    (x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .FileExtension    (x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.FileExtension    (x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .WithFileExtension(x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .WithFileExtension(x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.WithFileExtension(x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .AsFileExtension  (x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .AsFileExtension  (x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.AsFileExtension  (x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => ConfigWishes        .SetFileExtension (x.SynthBound.SynthWishes   , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => ConfigWishes        .SetFileExtension (x.SynthBound.FlowNode      , val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => ConfigWishesAccessor.SetFileExtension (x.SynthBound.ConfigResolver, val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .SetAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.SetAudioFormat(val.audioFormat)));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.fileExtension);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.fileExtension);
                Assert_TapeBound_Getters(x, val.fileExtension);
                Assert_BuffBound_Getters(x, init.fileExtension);
                Assert_Independent_Getters(x, init.fileExtension);
                Assert_Immutable_Getters(x, init.fileExtension);
                
                x.Record();
                Assert_All_Getters(x, init.fileExtension); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .FileExtension    (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .AsFileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .WithFileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .SetFileExtension (val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => FileExtension    (x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => FileExtension    (x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => FileExtension    (x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => FileExtension    (x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => AsFileExtension  (x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => AsFileExtension  (x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => AsFileExtension  (x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => AsFileExtension  (x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => WithFileExtension(x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => WithFileExtension(x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => WithFileExtension(x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => WithFileExtension(x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => SetFileExtension (x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => SetFileExtension (x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => SetFileExtension (x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => SetFileExtension (x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.FileExtension    (x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.FileExtension    (x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.FileExtension    (x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.FileExtension    (x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.AsFileExtension  (x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.AsFileExtension  (x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.AsFileExtension  (x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.AsFileExtension  (x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.WithFileExtension(x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.WithFileExtension(x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.WithFileExtension(x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.WithFileExtension(x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => ConfigWishes.SetFileExtension (x.TapeBound.Tape       , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => ConfigWishes.SetFileExtension (x.TapeBound.TapeConfig , val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => ConfigWishes.SetFileExtension (x.TapeBound.TapeActions, val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => ConfigWishes.SetFileExtension (x.TapeBound.TapeAction , val.fileExtension)));
            AssertProp(x => x.TapeBound.TapeConfig.AudioFormat = val.audioFormat);
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.fileExtension);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.fileExtension);
                Assert_TapeBound_Getters(x, init.fileExtension);
                Assert_BuffBound_Getters(x, val.fileExtension);
                Assert_Independent_Getters(x, init.fileExtension);
                Assert_Immutable_Getters(x, init.fileExtension);
                
                x.Record();
                Assert_All_Getters(x, init.fileExtension);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .FileExtension    (val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.FileExtension    (val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .WithFileExtension(val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.WithFileExtension(val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .AsFileExtension  (val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsFileExtension  (val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetFileExtension (val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetFileExtension (val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => FileExtension    (x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => FileExtension    (x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => WithFileExtension(x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => WithFileExtension(x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => AsFileExtension  (x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => AsFileExtension  (x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => SetFileExtension (x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => SetFileExtension (x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.FileExtension    (x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.FileExtension    (x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.WithFileExtension(x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.WithFileExtension(x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.AsFileExtension  (x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.AsFileExtension  (x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => ConfigWishes.SetFileExtension (x.BuffBound.Buff           , val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => ConfigWishes.SetFileExtension (x.BuffBound.AudioFileOutput, val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .SetAudioFormat(val.audioFormat, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SetAudioFormat(val.audioFormat, x.SynthBound.Context)));
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            // Independent after Taping
            
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);

            // Sample
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.fileExtension);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.fileExtension);
                    Assert_Independent_Getters(x.Independent.Sample, val.fileExtension);
                    Assert_Immutable_Getters(x, init.fileExtension);

                    x.Record();
                    Assert_All_Getters(x, init.fileExtension);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.FileExtension    (val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.WithFileExtension(val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsFileExtension  (val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetFileExtension (val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => FileExtension    (x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => WithFileExtension(x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => AsFileExtension  (x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => SetFileExtension (x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.FileExtension    (x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.WithFileExtension(x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.AsFileExtension  (x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => ConfigWishes.SetFileExtension (x.Independent.Sample, val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SetAudioFormat(val.audioFormat, x.SynthBound.Context)));
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);
            
            var x = CreateTestEntities(init);

            // AudioFileFormatEnum
            
            var audioFormats = new List<AudioFileFormatEnum>();
            {
                void AssertProp(Func<AudioFileFormatEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init.fileExtension);
                    
                    AudioFileFormatEnum audioFormat2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init.fileExtension);
                    Assert_Immutable_Getters(audioFormat2, val.fileExtension);
                    
                    audioFormats.Add(audioFormat2);
                }

                AssertProp(() => x.Immutable.AudioFormat.FileExtension    (val.fileExtension));
                AssertProp(() => x.Immutable.AudioFormat.WithFileExtension(val.fileExtension));
                AssertProp(() => x.Immutable.AudioFormat.AsFileExtension  (val.fileExtension));
                AssertProp(() => x.Immutable.AudioFormat.SetFileExtension (val.fileExtension));
                AssertProp(() => x.Immutable.AudioFormat.ToFileExtension  (val.fileExtension));
                AssertProp(() => val.fileExtension.FileExtensionToAudioFormat());
                AssertProp(() => FileExtension    (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => WithFileExtension(x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => AsFileExtension  (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => SetFileExtension (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => ToFileExtension  (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => FileExtensionToAudioFormat(val.fileExtension));
                AssertProp(() => ConfigWishes.FileExtension    (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => ConfigWishes.WithFileExtension(x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => ConfigWishes.AsFileExtension  (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => ConfigWishes.SetFileExtension (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => ConfigWishes.ToFileExtension (x.Immutable.AudioFormat, val.fileExtension));
                AssertProp(() => ConfigWishes.FileExtensionToAudioFormat(val.fileExtension));
                
                AssertProp(() => x.Immutable.AudioFormat.SetAudioFormat(val.audioFormat));
            }

            // AudioFormat Entity
            
            var audioFormatEntities = new List<AudioFileFormat>();
            {
                void AssertProp(Func<AudioFileFormat> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init.fileExtension);

                    AudioFileFormat audioFormatEntity2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init.fileExtension);
                    Assert_Immutable_Getters(audioFormatEntity2, val.fileExtension);
                    
                    audioFormatEntities.Add(audioFormatEntity2);
                }
                
                AssertProp(() => x.Immutable.AudioFormatEntity.FileExtension    (val.fileExtension, x.SynthBound.Context));
                AssertProp(() => x.Immutable.AudioFormatEntity.WithFileExtension(val.fileExtension, x.SynthBound.Context));
                AssertProp(() => x.Immutable.AudioFormatEntity.AsFileExtension  (val.fileExtension, x.SynthBound.Context));
                AssertProp(() => x.Immutable.AudioFormatEntity.ToFileExtension  (val.fileExtension, x.SynthBound.Context));
                AssertProp(() => x.Immutable.AudioFormatEntity.SetFileExtension (val.fileExtension, x.SynthBound.Context));
                AssertProp(() => FileExtension    (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => WithFileExtension(x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => AsFileExtension  (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => ToFileExtension  (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => SetFileExtension (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.FileExtension    (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.WithFileExtension(x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.AsFileExtension  (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.ToFileExtension  (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                AssertProp(() => ConfigWishes.SetFileExtension (x.Immutable.AudioFormatEntity, val.fileExtension, x.SynthBound.Context));
                
                AssertProp(() => x.Immutable.AudioFormatEntity.SetAudioFormat(val.audioFormat, x.SynthBound.Context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.fileExtension);
            
            // Except for our variables
            audioFormats.ForEach(e => Assert_Immutable_Getters(e, val.fileExtension));
            audioFormatEntities.ForEach(s => Assert_Immutable_Getters(s, val.fileExtension));
        }

        [TestMethod] 
        public void Default_FileExtension()
        {
            AreEqual(".wav", () => DefaultAudioFormat.FileExtension   ());
            AreEqual(".wav", () => DefaultAudioFormat.GetFileExtension());
            AreEqual(".wav", () => FileExtension   (DefaultAudioFormat));
            AreEqual(".wav", () => GetFileExtension(DefaultAudioFormat));
            AreEqual(".wav", () => ConfigWishes.FileExtension   (DefaultAudioFormat));
            AreEqual(".wav", () => ConfigWishes.GetFileExtension(DefaultAudioFormat));
        }
        
        [TestMethod] 
        public void ConfigSection_FileExtension()
        {
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(DefaultAudioFormat.FileExtension(), () => configSection.FileExtension   ());
            AreEqual(DefaultAudioFormat.FileExtension(), () => configSection.GetFileExtension());
            AreEqual(DefaultAudioFormat.FileExtension(), () => FileExtension   (configSection));
            AreEqual(DefaultAudioFormat.FileExtension(), () => GetFileExtension(configSection));
            AreEqual(DefaultAudioFormat.FileExtension(), () => ConfigWishesAccessor.FileExtension   (configSection));
            AreEqual(DefaultAudioFormat.FileExtension(), () => ConfigWishesAccessor.GetFileExtension(configSection));
        }
        
        [TestMethod]
        public void FileExtension_EdgeCases()
        {
            ThrowsException(() => ".abc".FileExtensionToAudioFormat());
            ThrowsException(() => AudioFormatToFileExtension((AudioFileFormatEnum)(-1)));
            IsNull(() => AudioFormatToFileExtension(Undefined));
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, string fileExtension)
        {
            Assert_Bound_Getters(x, fileExtension);
            Assert_Independent_Getters(x, fileExtension);
            Assert_Immutable_Getters(x, fileExtension);
        }

        private void Assert_Bound_Getters(TestEntities x, string fileExtension)
        {
            Assert_SynthBound_Getters(x, fileExtension);
            Assert_TapeBound_Getters(x, fileExtension);
            Assert_BuffBound_Getters(x, fileExtension);
        }
        
        private void Assert_Independent_Getters(TestEntities x, string fileExtension)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, fileExtension);
        }

        private void Assert_Immutable_Getters(TestEntities x, string fileExtension)
        {
            Assert_Immutable_Getters(x.Immutable.AudioFormat, fileExtension);
            Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, fileExtension);
            Assert_Immutable_Getters(x.Immutable.WavHeader, fileExtension);
        }

        private void Assert_SynthBound_Getters(TestEntities x, string fileExtension)
        {
            IsTrue(() => x.SynthBound.SynthWishes   .FileExtension   ().Is(fileExtension));
            IsTrue(() => x.SynthBound.FlowNode      .FileExtension   ().Is(fileExtension));
            IsTrue(() => x.SynthBound.ConfigResolver.FileExtension   ().Is(fileExtension));
            IsTrue(() => x.SynthBound.SynthWishes   .GetFileExtension().Is(fileExtension));
            IsTrue(() => x.SynthBound.FlowNode      .GetFileExtension().Is(fileExtension));
            IsTrue(() => x.SynthBound.ConfigResolver.GetFileExtension().Is(fileExtension));
            IsTrue(() => FileExtension   (x.SynthBound.SynthWishes   ).Is(fileExtension));
            IsTrue(() => FileExtension   (x.SynthBound.FlowNode      ).Is(fileExtension));
            IsTrue(() => FileExtension   (x.SynthBound.ConfigResolver).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.SynthBound.SynthWishes   ).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.SynthBound.FlowNode      ).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.SynthBound.ConfigResolver).Is(fileExtension));
            IsTrue(() => ConfigWishes        .FileExtension   (x.SynthBound.SynthWishes   ).Is(fileExtension));
            IsTrue(() => ConfigWishes        .FileExtension   (x.SynthBound.FlowNode      ).Is(fileExtension));
            IsTrue(() => ConfigWishesAccessor.FileExtension   (x.SynthBound.ConfigResolver).Is(fileExtension));
            IsTrue(() => ConfigWishes        .GetFileExtension(x.SynthBound.SynthWishes   ).Is(fileExtension));
            IsTrue(() => ConfigWishes        .GetFileExtension(x.SynthBound.FlowNode      ).Is(fileExtension));
            IsTrue(() => ConfigWishesAccessor.GetFileExtension(x.SynthBound.ConfigResolver).Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.SynthWishes   .IsRaw);
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.FlowNode      .IsRaw);
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.ConfigResolver.IsRaw);
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.SynthWishes   .IsWav);
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.FlowNode      .IsWav);
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.ConfigResolver.IsWav);
        }

        private void Assert_TapeBound_Getters(TestEntities x, string fileExtension)
        {
            IsTrue(() => x.TapeBound.Tape       .FileExtension   ().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeConfig .FileExtension   ().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeActions.FileExtension   ().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeAction .FileExtension   ().Is(fileExtension));
            IsTrue(() => x.TapeBound.Tape       .GetFileExtension().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeConfig .GetFileExtension().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeActions.GetFileExtension().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeAction .GetFileExtension().Is(fileExtension));
            IsTrue(() => FileExtension   (x.TapeBound.Tape       ).Is(fileExtension));
            IsTrue(() => FileExtension   (x.TapeBound.TapeConfig ).Is(fileExtension));
            IsTrue(() => FileExtension   (x.TapeBound.TapeActions).Is(fileExtension));
            IsTrue(() => FileExtension   (x.TapeBound.TapeAction ).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.TapeBound.Tape       ).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.TapeBound.TapeConfig ).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.TapeBound.TapeActions).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.TapeBound.TapeAction ).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (x.TapeBound.Tape       ).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (x.TapeBound.TapeConfig ).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (x.TapeBound.TapeActions).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (x.TapeBound.TapeAction ).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(x.TapeBound.Tape       ).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(x.TapeBound.TapeConfig ).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(x.TapeBound.TapeActions).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(x.TapeBound.TapeAction ).Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.Tape       .IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.TapeConfig .IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.TapeAction .IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.Tape       .IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.TapeConfig .IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.TapeActions.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.TapeAction .IsWav());
        }
                        
        private void Assert_BuffBound_Getters(TestEntities x, string fileExtension)
        {
            IsTrue(() => x.BuffBound.Buff           .FileExtension   ().Is(fileExtension));
            IsTrue(() => x.BuffBound.AudioFileOutput.FileExtension   ().Is(fileExtension));
            IsTrue(() => x.BuffBound.Buff           .GetFileExtension().Is(fileExtension));
            IsTrue(() => x.BuffBound.AudioFileOutput.GetFileExtension().Is(fileExtension));
            IsTrue(() => FileExtension   (x.BuffBound.Buff           ).Is(fileExtension));
            IsTrue(() => FileExtension   (x.BuffBound.AudioFileOutput).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.BuffBound.Buff           ).Is(fileExtension));
            IsTrue(() => GetFileExtension(x.BuffBound.AudioFileOutput).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (x.BuffBound.Buff           ).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (x.BuffBound.AudioFileOutput).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(x.BuffBound.Buff           ).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(x.BuffBound.AudioFileOutput).Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => x.BuffBound.Buff           .IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.BuffBound.AudioFileOutput.IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => x.BuffBound.Buff           .IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.BuffBound.AudioFileOutput.IsWav());
        }

        private void Assert_Independent_Getters(Sample sample, string fileExtension)
        {
            IsTrue(() => sample.FileExtension   ().Is(fileExtension));
            IsTrue(() => sample.GetFileExtension().Is(fileExtension));
            IsTrue(() => FileExtension   (sample).Is(fileExtension));
            IsTrue(() => GetFileExtension(sample).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (sample).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(sample).Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => sample.IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => sample.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, string fileExtension)
        {
            IsTrue(() => audioFileFormatEnum.FileExtension   ().Is(fileExtension));
            IsTrue(() => audioFileFormatEnum.GetFileExtension().Is(fileExtension));
            IsTrue(() => audioFileFormatEnum.AsFileExtension ().Is(fileExtension));
            IsTrue(() => audioFileFormatEnum.ToFileExtension ().Is(fileExtension));
            IsTrue(() => audioFileFormatEnum.AudioFormatToFileExtension().Is(fileExtension));
            IsTrue(() => FileExtension   (audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => GetFileExtension(audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => AsFileExtension (audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => ToFileExtension (audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => AudioFormatToFileExtension(audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => ConfigWishes.AsFileExtension (audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => ConfigWishes.ToFileExtension (audioFileFormatEnum).Is(fileExtension));
            IsTrue(() => ConfigWishes.AudioFormatToFileExtension(audioFileFormatEnum).Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => audioFileFormatEnum.IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => audioFileFormatEnum.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, string fileExtension)
        {
            IsNotNull(() => audioFormatEntity);
            IsTrue(() => audioFormatEntity.FileExtension   ().Is(fileExtension));
            IsTrue(() => audioFormatEntity.GetFileExtension().Is(fileExtension));
            IsTrue(() => audioFormatEntity.AsFileExtension ().Is(fileExtension));
            IsTrue(() => audioFormatEntity.ToFileExtension ().Is(fileExtension));
            IsTrue(() => FileExtension   (audioFormatEntity).Is(fileExtension));
            IsTrue(() => GetFileExtension(audioFormatEntity).Is(fileExtension));
            IsTrue(() => AsFileExtension (audioFormatEntity).Is(fileExtension));
            IsTrue(() => ToFileExtension (audioFormatEntity).Is(fileExtension));
            IsTrue(() => ConfigWishes.FileExtension   (audioFormatEntity).Is(fileExtension));
            IsTrue(() => ConfigWishes.GetFileExtension(audioFormatEntity).Is(fileExtension));
            IsTrue(() => ConfigWishes.AsFileExtension (audioFormatEntity).Is(fileExtension));
            IsTrue(() => ConfigWishes.ToFileExtension (audioFormatEntity).Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => audioFormatEntity.IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => audioFormatEntity.IsWav());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, string fileExtension)
        {
            if (fileExtension.Is(".wav"))
            {
                NotEqual(default, () => wavHeader);
                IsTrue(() => wavHeader.IsWav());
                IsTrue(() => wavHeader.FileExtension   ().Is(fileExtension));
                IsTrue(() => wavHeader.GetFileExtension().Is(fileExtension));
            }
            else
            {
                AreEqual(default, () => wavHeader);
                IsFalse(() => wavHeader.IsRaw());
                IsFalse(() => wavHeader.FileExtension   ().Is(fileExtension));
                IsFalse(() => wavHeader.GetFileExtension().Is(fileExtension));
            }
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities((string fileExtension, AudioFileFormatEnum? audioFormat) init) 
            => new TestEntities(x => x.WithLoggingDisabled().AudioFormat(init.audioFormat).SamplingRate(HighPerfHz));
        
        // ncrunch: no coverage start
        
        static object TestParametersInit => new[] 
        {
            new object[] { ".raw", (int)Raw       },
            new object[] { ".wav", (int)Wav       },
            new object[] { ".RAW", (int)Raw       },
            new object[] { ".WAV", (int)Wav       },
            new object[] { ".RAw", (int)Raw       },
            new object[] { ".wAV", (int)Wav       },
            new object[] { ""    , (int)Undefined },
            new object[] { ""    ,  null          },
            new object[] { null  , (int)Undefined },
            new object[] { null  ,  null          },
        };
        
        static object TestParameters => new[]
        {
            new object[] { ".raw", (int)Raw, ".wav", (int)Wav },
            new object[] { ".wav", (int)Wav, ".raw", (int)Raw },
            new object[] { ".RAW", (int)Raw, ".WAV", (int)Wav },
            new object[] { ".WAV", (int)Wav, ".RAW", (int)Raw }
        };
        
        static object TestParametersWithEmpty => new[]
        {
            new object[] { ".raw", (int)Raw      , ".wav", (int)Wav       },
            new object[] { ".wav", (int)Wav      , ".raw", (int)Raw       },
            new object[] { ".RAW", (int)Raw      , ".WAV", (int)Wav       },
            new object[] { ".WAV", (int)Wav      , ".RAW", (int)Raw       },
            new object[] { ".raw", (int)Raw      ,  ""   , (int)Undefined },
            new object[] { ".raw", (int)Raw      ,  null ,  null          },
            new object[] { ""    , (int)Undefined, ".raw", (int)Raw       },
            new object[] {  null ,  null         , ".raw", (int)Raw       },
        };
        
        // ncrunch: no coverage end
    } 
}