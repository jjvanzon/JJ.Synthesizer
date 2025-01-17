using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Technical.Configuration.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Configuration
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioFormatAttributeTests
    {
        [TestMethod, DataRow(Raw), DataRow(Wav), DataRow(Undefined), DataRow(null)]
        public void Init_AudioFormat(AudioFileFormatEnum? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceAudioFormat(init));
        }

        [TestMethod] 
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_AudioFormat(int? initAsInt, int? valueAsInt)
        {            
            var init  = (AudioFileFormatEnum?)initAsInt;
            var value = (AudioFileFormatEnum?)valueAsInt;
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceAudioFormat(init));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceAudioFormat(value));
                Assert_TapeBound_Getters  (x, CoalesceAudioFormat(init));
                Assert_BuffBound_Getters  (x, CoalesceAudioFormat(init));
                Assert_Independent_Getters(x, CoalesceAudioFormat(init));
                Assert_Immutable_Getters  (x, CoalesceAudioFormat(init));
                
                x.Record();
                Assert_All_Getters(x, CoalesceAudioFormat(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioFormat(value)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioFormat(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioFormat(value)));
            
            AssertProp(x => {
                if (value == Raw      ) AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes .AsRaw       ());
                if (value == Wav      ) AreEqual(x.SynthBound.SynthWishes   , () => x.SynthBound.SynthWishes .AsWav       ()); 
                if (value == Undefined) AreEqual(x.SynthBound.SynthWishes   ,       x.SynthBound.SynthWishes .AudioFormat (Undefined)); 
                if (value == 0        ) AreEqual(x.SynthBound.SynthWishes   ,       x.SynthBound.SynthWishes .AudioFormat (0)); 
                if (value == null     ) AreEqual(x.SynthBound.SynthWishes   ,       x.SynthBound.SynthWishes .AudioFormat (null)); });
            
            AssertProp(x => {
                if (value == Raw      ) AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode    .AsRaw       ());
                if (value == Wav      ) AreEqual(x.SynthBound.FlowNode      , () => x.SynthBound.FlowNode    .AsWav       ()); 
                if (value == Undefined) AreEqual(x.SynthBound.FlowNode      ,       x.SynthBound.FlowNode    .AudioFormat (Undefined));
                if (value == 0        ) AreEqual(x.SynthBound.FlowNode      ,       x.SynthBound.FlowNode    .AudioFormat (0));
                if (value == default  ) AreEqual(x.SynthBound.FlowNode      ,       x.SynthBound.FlowNode    .AudioFormat (default)); });
            
            AssertProp(x => {
                if (value == Raw      ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsRaw       ());
                if (value == Wav      ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsWav       ());
                if (value == Undefined) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat (Undefined)); 
                if (value == 0        ) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat (0));
                if (value == null     ) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat (null)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_AudioFormat(int initAsInt, int valueAsInt)
        {
            var init  = (AudioFileFormatEnum)initAsInt;
            var value = (AudioFileFormatEnum)valueAsInt;

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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.AudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.AudioFormat(value)));
            AssertProp(x =>                                         x.TapeBound.TapeConfig.AudioFormat = value);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioFormat(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.AudioFormat(value)));
            
            AssertProp(x => {
                if (value == Raw) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsRaw());
                if (value == Wav) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsWav()); });
            
            AssertProp(x => {
                if (value == Raw) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsRaw());
                if (value == Wav) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsWav()); });
            
            AssertProp(x => {
                if (value == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsRaw());
                if (value == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsWav()); });
            
            AssertProp(x => {
                if (value == Raw) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsRaw());
                if (value == Wav) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsWav()); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_AudioFormat(int initAsInt, int valueAsInt)
        {
            var init  = (AudioFileFormatEnum)initAsInt;
            var value = (AudioFileFormatEnum)valueAsInt;
            
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

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.AudioFormat(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AudioFormat(value, x.SynthBound.Context)));
            
            AssertProp(x => {
                if (value == Raw) AreEqual(x.BuffBound.Buff,  () => x.BuffBound.Buff.AsRaw(x.SynthBound.Context));
                if (value == Wav) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsWav(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (value == Raw) AreEqual(x.BuffBound.AudioFileOutput,  () => x.BuffBound.AudioFileOutput.AsRaw(x.SynthBound.Context));
                if (value == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsWav(x.SynthBound.Context)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_AudioFormat(int initAsInt, int valueAsInt)
        {
            // Independent after Taping
            
            var init  = (AudioFileFormatEnum)initAsInt;
            var value = (AudioFileFormatEnum)valueAsInt;

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
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioFormat(value, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsRaw(x.SynthBound.Context));
                    if (value == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsWav(x.SynthBound.Context)); });
            }
        }
        
        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void Immutable_AudioFormat(int intAsInit, int intAsValue)
        {
            var init  = (AudioFileFormatEnum)intAsInit;
            var value = (AudioFileFormatEnum)intAsValue;
            var x = CreateTestEntities(init);

            // AudioFileFormatEnum
            
            var audioFormats = new List<AudioFileFormatEnum>();
            {
                void AssertProp(Func<AudioFileFormatEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init);
                    
                    AudioFileFormatEnum audioFormat2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormat, init);
                    Assert_Immutable_Getters(audioFormat2, value);
                    
                    audioFormats.Add(audioFormat2);
                }

                AssertProp(() => x.Immutable.AudioFormat.AudioFormat(value));
                AssertProp(() => value.AudioFormat());
                AssertProp(() => value == Raw ? x.Immutable.AudioFormat.AsRaw() : x.Immutable.AudioFormat.AsWav());
            }

            // AudioFormat Entity
            
            var audioFormatEntities = new List<AudioFileFormat>();
            {
                void AssertProp(Func<AudioFileFormat> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init);

                    AudioFileFormat audioFormatEntity2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init);
                    Assert_Immutable_Getters(audioFormatEntity2, value);
                    
                    audioFormatEntities.Add(audioFormatEntity2);
                }
                
                AssertProp(() => x.Immutable.AudioFormatEntity.AudioFormat(value, x.SynthBound.Context));
                AssertProp(() => value.ToEntity(x.SynthBound.Context));
                AssertProp(() => value == Raw ? x.Immutable.AudioFormatEntity.AsRaw(x.SynthBound.Context) : x.Immutable.AudioFormatEntity.AsWav(x.SynthBound.Context));
            }
            
            // FileExtension
            
            var fileExtensions = new List<string>();
            {
                void AssertProp(Func<string> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, init);

                    string fileExtension2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.FileExtension, init);
                    Assert_Immutable_Getters(fileExtension2, value);
                    
                    fileExtensions.Add(fileExtension2);
                }
                
                AssertProp(() => x.Immutable.FileExtension.AudioFormat(value));
                AssertProp(() => value == Raw ? x.Immutable.FileExtension.AsRaw() : x.Immutable.FileExtension.AsWav());
            }

            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            audioFormats.ForEach(e => Assert_Immutable_Getters(e, value));
            audioFormatEntities.ForEach(s => Assert_Immutable_Getters(s, value));
            fileExtensions.ForEach(s => Assert_Immutable_Getters(s, value));
        }

        [TestMethod]
        public void ConfigSections_AudioFormat()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            
            AreEqual(DefaultAudioFormat,        () => configSection.AudioFormat);
            AreEqual(DefaultAudioFormat,        () => configSection.AudioFormat());
            AreEqual(DefaultAudioFormat == Raw, () => configSection.IsRaw());
            AreEqual(DefaultAudioFormat == Wav, () => configSection.IsWav());
        }

        // Getter Helpers
        
        private void Assert_All_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Bound_Getters(x, audioFormat);
            Assert_Independent_Getters(x, audioFormat);
            Assert_Immutable_Getters(x, audioFormat);
        }

        private void Assert_Bound_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_SynthBound_Getters(x, audioFormat);
            Assert_TapeBound_Getters(x, audioFormat);
            Assert_BuffBound_Getters(x, audioFormat);
        }
        
        private void Assert_Independent_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, audioFormat);
        }

        private void Assert_Immutable_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Immutable_Getters(x.Immutable.AudioFormat, audioFormat);
            Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, audioFormat);
            Assert_Immutable_Getters(x.Immutable.FileExtension, audioFormat);
            Assert_Immutable_Getters(x.Immutable.WavHeader, audioFormat);
        }

        private void Assert_SynthBound_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            AreEqual(audioFormat, () => x.SynthBound.SynthWishes.AudioFormat());
            AreEqual(audioFormat, () => x.SynthBound.SynthWishes.GetAudioFormat);
            AreEqual(audioFormat, () => x.SynthBound.FlowNode.AudioFormat());
            AreEqual(audioFormat, () => x.SynthBound.FlowNode.GetAudioFormat);
            AreEqual(audioFormat, () => x.SynthBound.ConfigResolver.AudioFormat());
            AreEqual(audioFormat, () => x.SynthBound.ConfigResolver.GetAudioFormat);
            
            AreEqual(audioFormat == Raw, () => x.SynthBound.SynthWishes.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.SynthWishes.IsRaw);
            AreEqual(audioFormat == Raw, () => x.SynthBound.FlowNode.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.FlowNode.IsRaw);
            AreEqual(audioFormat == Raw, () => x.SynthBound.ConfigResolver.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.ConfigResolver.IsRaw);
            
            AreEqual(audioFormat == Wav, () => x.SynthBound.SynthWishes.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.SynthWishes.IsWav);
            AreEqual(audioFormat == Wav, () => x.SynthBound.FlowNode.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.FlowNode.IsWav);
            AreEqual(audioFormat == Wav, () => x.SynthBound.ConfigResolver.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.ConfigResolver.IsWav);
        }

        private void Assert_TapeBound_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            AreEqual(audioFormat, () => x.TapeBound.Tape.AudioFormat());
            AreEqual(audioFormat, () => x.TapeBound.TapeConfig.AudioFormat());
            AreEqual(audioFormat, () => x.TapeBound.TapeConfig.AudioFormat);
            AreEqual(audioFormat, () => x.TapeBound.TapeActions.AudioFormat());
            AreEqual(audioFormat, () => x.TapeBound.TapeAction.AudioFormat());
            
            AreEqual(audioFormat == Raw, () => x.TapeBound.Tape.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeConfig.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeAction.IsRaw());
        
            AreEqual(audioFormat == Wav, () => x.TapeBound.Tape.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeConfig.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeActions.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeAction.IsWav());
        }
                        
        private void Assert_BuffBound_Getters(TestEntities x, AudioFileFormatEnum audioFormat)
        {
            AreEqual(audioFormat, () => x.BuffBound.Buff.AudioFormat());
            AreEqual(audioFormat, () => x.BuffBound.AudioFileOutput.AudioFormat());
            
            AreEqual(audioFormat == Raw, () => x.BuffBound.Buff.IsRaw());
            AreEqual(audioFormat == Raw, () => x.BuffBound.AudioFileOutput.IsRaw());
            
            AreEqual(audioFormat == Wav, () => x.BuffBound.Buff.IsWav());
            AreEqual(audioFormat == Wav, () => x.BuffBound.AudioFileOutput.IsWav());
        }

        private void Assert_Independent_Getters(Sample sample, AudioFileFormatEnum audioFormat)
        {
            AreEqual(audioFormat,        () => sample.AudioFormat());
            AreEqual(audioFormat == Raw, () => sample.IsRaw());
            AreEqual(audioFormat == Wav, () => sample.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, AudioFileFormatEnum audioFormat)
        {
            AreEqual(audioFormat,        () => audioFileFormatEnum.AudioFormat());
            AreEqual(audioFormat == Raw, () => audioFileFormatEnum.IsRaw());
            AreEqual(audioFormat == Wav, () => audioFileFormatEnum.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, AudioFileFormatEnum audioFormat)
        {
            if (audioFormatEntity == null) throw new NullException(() => audioFormatEntity);
            AreEqual(audioFormat,        () => audioFormatEntity.AudioFormat());
            AreEqual(audioFormat == Raw, () => audioFormatEntity.IsRaw());
            AreEqual(audioFormat == Wav, () => audioFormatEntity.IsWav());
        }
         
        private void Assert_Immutable_Getters(string fileExtension, AudioFileFormatEnum audioFormat)
        {
            AreEqual(audioFormat,        () => fileExtension.AudioFormat());
            AreEqual(audioFormat == Raw, () => fileExtension.IsRaw());
            AreEqual(audioFormat == Wav, () => fileExtension.IsWav());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, AudioFileFormatEnum audioFormat)
        {
            if (audioFormat == Wav)
            {
                NotEqual(default, () => wavHeader);
                IsTrue(() => wavHeader.IsWav());
                AreEqual(audioFormat, () => wavHeader.AudioFormat());
            }
            else
            {
                AreEqual(default, () => wavHeader);
                IsFalse(() => wavHeader.IsRaw());
                NotEqual(audioFormat, () => wavHeader.AudioFormat());
            }
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities(AudioFileFormatEnum? audioFormat) 
            => new TestEntities(x => x.WithAudioFormat(audioFormat));

        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { (int)Raw, (int)Wav },
            new object[] { (int)Wav, (int)Raw }
        };

        static object TestParametersWithEmpty => new[] // ncrunch: no coverage
        {
            new object[] { (int)Raw, (int)Wav       },
            new object[] { (int)Wav, (int)Raw       },
            new object[] { (int)Raw, (int)Undefined },
            new object[] { null    , (int)Raw       },
            new object[] { (int)Raw, null           }
        };
    } 
}