using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.ConfigTests.ConfigTestEntities;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
// ReSharper disable ArrangeStaticMemberQualifier

#pragma warning disable CS0611
#pragma warning disable MSTEST0018
#pragma warning disable IDE0002

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Config")]
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
            
            void AssertProp(Action<ConfigTestEntities> setter)
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

            void AssertProp(Action<ConfigTestEntities> setter)
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
            
            void AssertProp(Action<ConfigTestEntities> setter)
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
                ConfigTestEntities x = default;

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
            fileExtensions.ForEach(s => Assert_Immutable_Getters(s, value));
            audioFormats.ForEach(e => Assert_Immutable_Getters(e, value));
            audioFormatEntities.ForEach(s => Assert_Immutable_Getters(s, value));
        }

        [TestMethod]
        public void ConfigSection_AudioFormat()
        {
            // Synth-Bound. Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(DefaultAudioFormat == Raw, () => configSection.IsRaw());
            AreEqual(DefaultAudioFormat == Wav, () => configSection.IsWav());
            AreEqual(DefaultAudioFormat,        () => configSection.AudioFormat);
            AreEqual(DefaultAudioFormat,        () => configSection.AudioFormat());
            AreEqual(DefaultAudioFormat,        () => configSection.GetAudioFormat());

            // TODO: Program ConfigWishesAccessor for its internals.
            //AreEqual(DefaultAudioFormat == Raw, () => IsRaw(configSection));
            //AreEqual(DefaultAudioFormat == Wav, () => IsWav(configSection));
            //AreEqual(DefaultAudioFormat,        () => AudioFormat(configSection));
            //AreEqual(DefaultAudioFormat,        () => AudioFormat(configSection));
            //AreEqual(DefaultAudioFormat,        () => GetAudioFormat(configSection));
            
            //AreEqual(DefaultAudioFormat == Raw, () => ConfigWishesAccessor.IsRaw(configSection));
            //AreEqual(DefaultAudioFormat == Wav, () => ConfigWishesAccessor.IsWav(configSection));
            //AreEqual(DefaultAudioFormat,        () => ConfigWishesAccessor.AudioFormat(configSection));
            //AreEqual(DefaultAudioFormat,        () => ConfigWishesAccessor.AudioFormat(configSection));
            //AreEqual(DefaultAudioFormat,        () => ConfigWishesAccessor.GetAudioFormat(configSection));
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Bound_Getters(x, audioFormat);
            Assert_Independent_Getters(x, audioFormat);
            Assert_Immutable_Getters(x, audioFormat);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_SynthBound_Getters(x, audioFormat);
            Assert_TapeBound_Getters(x, audioFormat);
            Assert_BuffBound_Getters(x, audioFormat);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, audioFormat);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            Assert_Immutable_Getters(x.Immutable.WavHeader, audioFormat);
            Assert_Immutable_Getters(x.Immutable.FileExtension, audioFormat);
            Assert_Immutable_Getters(x.Immutable.AudioFormat, audioFormat);
            Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, audioFormat);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Property Syntax
            
            AreEqual(audioFormat == Wav, () => x.SynthBound.SynthWishes.IsWav);
            AreEqual(audioFormat == Wav, () => x.SynthBound.FlowNode.IsWav);
            AreEqual(audioFormat == Raw, () => x.SynthBound.SynthWishes.IsRaw);
            AreEqual(audioFormat == Raw, () => x.SynthBound.FlowNode.IsRaw);
            AreEqual(audioFormat,        () => x.SynthBound.SynthWishes.GetAudioFormat);
            AreEqual(audioFormat,        () => x.SynthBound.FlowNode.GetAudioFormat);
            AreEqual(audioFormat,        () => x.SynthBound.ConfigResolver.GetAudioFormat);
            
            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => x.SynthBound.SynthWishes.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.FlowNode.IsWav());
            AreEqual(audioFormat == Wav, () => x.SynthBound.ConfigResolver.IsWav);
            AreEqual(audioFormat == Wav, () => x.SynthBound.ConfigResolver.IsWav());
            AreEqual(audioFormat == Raw, () => x.SynthBound.SynthWishes.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.FlowNode.IsRaw());
            AreEqual(audioFormat == Raw, () => x.SynthBound.ConfigResolver.IsRaw);
            AreEqual(audioFormat == Raw, () => x.SynthBound.ConfigResolver.IsRaw());
            AreEqual(audioFormat,        () => x.SynthBound.SynthWishes.AudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.FlowNode.AudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.ConfigResolver.AudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.SynthWishes.GetAudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.FlowNode.GetAudioFormat());
            AreEqual(audioFormat,        () => x.SynthBound.ConfigResolver.GetAudioFormat());

            // Using Static Syntax
            
            AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.FlowNode));
            //AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.ConfigResolver));

            AreEqual(audioFormat == Raw, () => IsRaw(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Raw, () => IsRaw(x.SynthBound.FlowNode));
            //AreEqual(audioFormat == Raw, () => IsRaw(x.SynthBound.ConfigResolver));

            AreEqual(audioFormat,       () => AudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat,       () => AudioFormat(x.SynthBound.FlowNode));
            //AreEqual(audioFormat,       () => AudioFormat(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat,       () => GetAudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat,       () => GetAudioFormat(x.SynthBound.FlowNode));
            //AreEqual(audioFormat,       () => GetAudioFormat(x.SynthBound.ConfigResolver));

            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.SynthBound.FlowNode));
            //AreEqual(audioFormat == Wav, () => IsWav(x.SynthBound.ConfigResolver));

            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.SynthBound.SynthWishes));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.SynthBound.FlowNode));
            //AreEqual(audioFormat == Raw, () => ConfigWishesAccessor.IsRaw(x.SynthBound.ConfigResolver));

            AreEqual(audioFormat, () => ConfigWishes.AudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat, () => ConfigWishes.AudioFormat(x.SynthBound.FlowNode));
            //AreEqual(audioFormat, () => ConfigWishesAccessor.AudioFormat(x.SynthBound.ConfigResolver));
            AreEqual(audioFormat, () => ConfigWishes.GetAudioFormat(x.SynthBound.SynthWishes));
            AreEqual(audioFormat, () => ConfigWishes.GetAudioFormat(x.SynthBound.FlowNode));
            //AreEqual(audioFormat, () => ConfigWishesAccessor.GetAudioFormat(x.SynthBound.ConfigResolver));
        }

        private void Assert_TapeBound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            // Property Syntax
            
            AreEqual(audioFormat, () => x.TapeBound.TapeConfig.AudioFormat);

            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => x.TapeBound.Tape.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeConfig.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeActions.IsWav());
            AreEqual(audioFormat == Wav, () => x.TapeBound.TapeAction.IsWav());
            AreEqual(audioFormat == Raw, () => x.TapeBound.Tape.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeConfig.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(audioFormat == Raw, () => x.TapeBound.TapeAction.IsRaw());
            AreEqual(audioFormat,        () => x.TapeBound.Tape.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeConfig.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeActions.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeAction.AudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.Tape.GetAudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeConfig.GetAudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeActions.GetAudioFormat());
            AreEqual(audioFormat,        () => x.TapeBound.TapeAction.GetAudioFormat());
            
            // Using Static Syntax
           
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.Tape));
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Wav, () => IsWav(x.TapeBound.TapeAction));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.Tape));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Raw, () => IsRaw(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => AudioFormat(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => GetAudioFormat(x.TapeBound.TapeAction));
            
            // Static Syntax
           
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.Tape));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.TapeBound.TapeAction));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.Tape));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.TapeConfig));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.TapeActions));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.TapeBound.TapeAction));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.Tape));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.TapeConfig));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.TapeActions));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(x.TapeBound.TapeAction));
        }
                        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, AudioFileFormatEnum audioFormat)
        {
            
            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => x.BuffBound.Buff.IsWav());
            AreEqual(audioFormat == Wav, () => x.BuffBound.AudioFileOutput.IsWav());
            AreEqual(audioFormat == Raw, () => x.BuffBound.Buff.IsRaw());
            AreEqual(audioFormat == Raw, () => x.BuffBound.AudioFileOutput.IsRaw());
            AreEqual(audioFormat,        () => x.BuffBound.Buff.AudioFormat());
            AreEqual(audioFormat,        () => x.BuffBound.AudioFileOutput.AudioFormat());
            AreEqual(audioFormat,        () => x.BuffBound.Buff.GetAudioFormat());
            AreEqual(audioFormat,        () => x.BuffBound.AudioFileOutput.GetAudioFormat());
            
            // Using Static Syntax
            
            AreEqual(audioFormat == Wav, () => IsWav(x.BuffBound.Buff));
            AreEqual(audioFormat == Wav, () => IsWav(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat == Raw, () => IsRaw(x.BuffBound.Buff));
            AreEqual(audioFormat == Raw, () => IsRaw(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => AudioFormat(x.BuffBound.AudioFileOutput));
            
            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.BuffBound.Buff));
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.BuffBound.Buff));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.AudioFileOutput));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.Buff));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(x.BuffBound.AudioFileOutput));
        }

        private void Assert_Independent_Getters(Sample sample, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => sample.IsWav());
            AreEqual(audioFormat == Raw, () => sample.IsRaw());
            AreEqual(audioFormat,        () => sample.AudioFormat());
            AreEqual(audioFormat,        () => sample.GetAudioFormat());
            
            // Using Static Syntax
            
            AreEqual(audioFormat == Wav, () => IsWav(sample));
            AreEqual(audioFormat == Raw, () => IsRaw(sample));
            AreEqual(audioFormat,        () => AudioFormat(sample));
            AreEqual(audioFormat,        () => GetAudioFormat(sample));
            
            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(sample));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(sample));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(sample));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(sample));
        }
        
        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, AudioFileFormatEnum audioFormat)
        {
            if (audioFormat == Wav)
            {
                NotEqual(default, () => wavHeader);
                
                IsTrue(() => wavHeader.IsWav());
                AreEqual(audioFormat, () => wavHeader.AudioFormat());
                AreEqual(audioFormat, () => wavHeader.GetAudioFormat());

                IsTrue(() => IsWav(wavHeader));
                AreEqual(audioFormat, () => AudioFormat(wavHeader));
                AreEqual(audioFormat, () => GetAudioFormat(wavHeader));
                
                IsTrue(() => ConfigWishes.IsWav(wavHeader));
                AreEqual(audioFormat, () => ConfigWishes.AudioFormat(wavHeader));
                AreEqual(audioFormat, () => ConfigWishes.GetAudioFormat(wavHeader));
            }
            else
            {
                AreEqual(default, () => wavHeader);
                
                IsFalse(() => wavHeader.IsRaw());
                NotEqual(audioFormat, () => wavHeader.AudioFormat());
                NotEqual(audioFormat, () => wavHeader.GetAudioFormat());

                IsFalse(() => IsRaw(wavHeader));
                NotEqual(audioFormat, () => AudioFormat(wavHeader));
                NotEqual(audioFormat, () => GetAudioFormat(wavHeader));
                
                IsFalse(() => ConfigWishes.IsRaw(wavHeader));
                NotEqual(audioFormat, () => ConfigWishes.AudioFormat(wavHeader));
                NotEqual(audioFormat, () => ConfigWishes.GetAudioFormat(wavHeader));
            }
        }
                 
        private void Assert_Immutable_Getters(string fileExtension, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => fileExtension.IsWav());
            AreEqual(audioFormat == Raw, () => fileExtension.IsRaw());
            AreEqual(audioFormat,        () => fileExtension.AudioFormat());
            AreEqual(audioFormat,        () => fileExtension.AsAudioFormat());
            AreEqual(audioFormat,        () => fileExtension.ToAudioFormat());
            AreEqual(audioFormat,        () => fileExtension.GetAudioFormat());

            // Using Static Syntax
            
            AreEqual(audioFormat == Wav, () => IsWav(fileExtension));
            AreEqual(audioFormat == Raw, () => IsRaw(fileExtension));
            AreEqual(audioFormat,        () => AudioFormat(fileExtension));
            AreEqual(audioFormat,        () => AsAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ToAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => GetAudioFormat(fileExtension));

            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(fileExtension));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormat(fileExtension));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(fileExtension));
        }

        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, AudioFileFormatEnum audioFormat)
        {
            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => audioFileFormatEnum.IsWav());
            AreEqual(audioFormat == Raw, () => audioFileFormatEnum.IsRaw());
            AreEqual(audioFormat,        () => audioFileFormatEnum.AudioFormat());
            AreEqual(audioFormat,        () => audioFileFormatEnum.ToAudioFormat());
            AreEqual(audioFormat,        () => audioFileFormatEnum.AsAudioFormat());
            AreEqual(audioFormat,        () => audioFileFormatEnum.GetAudioFormat());
            
            // Using Static Syntax
            
            AreEqual(audioFormat == Wav, () => IsWav(audioFileFormatEnum));
            AreEqual(audioFormat == Raw, () => IsRaw(audioFileFormatEnum));
            AreEqual(audioFormat,        () => AudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ToAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => AsAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => GetAudioFormat(audioFileFormatEnum));
            
            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(audioFileFormatEnum));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormat(audioFileFormatEnum));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(audioFileFormatEnum));
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, AudioFileFormatEnum audioFormat)
        {
            if (audioFormatEntity == null) throw new NullException(() => audioFormatEntity);

            // Extension Method Syntax
            
            AreEqual(audioFormat == Wav, () => audioFormatEntity.IsWav());
            AreEqual(audioFormat == Raw, () => audioFormatEntity.IsRaw());
            AreEqual(audioFormat,        () => audioFormatEntity.AudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.AsAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.ToAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.GetAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.AsEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.ToEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.GetEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.AsAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.ToAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.GetAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.EntityToEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.EntityToAudioFormat());
            AreEqual(audioFormat,        () => audioFormatEntity.EntityToAudioFormatEnum());
            AreEqual(audioFormat,        () => audioFormatEntity.AudioFormatEntityToEnum());

            // Using Static Syntax
            
            AreEqual(audioFormat == Wav, () => IsWav(audioFormatEntity));
            AreEqual(audioFormat == Raw, () => IsRaw(audioFormatEntity));
            AreEqual(audioFormat,        () => AudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => AsAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => GetAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => AsEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => GetEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => AsAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => GetAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => EntityToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => EntityToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => EntityToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => AudioFormatEntityToEnum(audioFormatEntity));

            // Static Syntax
            
            AreEqual(audioFormat == Wav, () => ConfigWishes.IsWav(audioFormatEntity));
            AreEqual(audioFormat == Raw, () => ConfigWishes.IsRaw(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AsEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.ToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.GetEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AsAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.ToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.GetAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.EntityToEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.EntityToAudioFormat(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.EntityToAudioFormatEnum(audioFormatEntity));
            AreEqual(audioFormat,        () => ConfigWishes.AudioFormatEntityToEnum(audioFormatEntity));
        }

        // Test Data Helpers
        
        private ConfigTestEntities CreateTestEntities(AudioFileFormatEnum? audioFormat) 
            => new ConfigTestEntities(x => x.WithAudioFormat(audioFormat));

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