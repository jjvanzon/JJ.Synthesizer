using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Config;
using JJ.Framework.Wishes.Common;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.ConfigTests.ConfigTestEntities;
using static JJ.Business.Synthesizer.Wishes.Config.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

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
            
            void AssertProp(Action<ConfigTestEntities> setter)
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .FileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .FileExtension  (val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.FileExtension  (val.fileExtension)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .AudioFormat    (val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .AudioFormat    (val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat    (val.audioFormat)));
                                                                      
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,          x.SynthBound.SynthWishes   .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,             x.SynthBound.FlowNode      .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.WithAudioFormat(val.audioFormat)));
            
            AssertProp(x => {
                if (val.audioFormat == Raw      ) AreEqual(x.SynthBound.SynthWishes  , () => x.SynthBound.SynthWishes.AsRaw());
                if (val.audioFormat == Wav      ) AreEqual(x.SynthBound.SynthWishes  , () => x.SynthBound.SynthWishes.AsWav()); 
                if (val.audioFormat == Undefined) AreEqual(x.SynthBound.SynthWishes  ,       x.SynthBound.SynthWishes.AudioFormat(Undefined)); 
                if (val.audioFormat == 0        ) AreEqual(x.SynthBound.SynthWishes  ,       x.SynthBound.SynthWishes.AudioFormat(0));
                if (val.audioFormat == null     ) AreEqual(x.SynthBound.SynthWishes  ,       x.SynthBound.SynthWishes.AudioFormat(null)); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw      ) AreEqual(x.SynthBound.FlowNode     , () => x.SynthBound.FlowNode   .AsRaw());
                if (val.audioFormat == Wav      ) AreEqual(x.SynthBound.FlowNode     , () => x.SynthBound.FlowNode   .AsWav());
                if (val.audioFormat == Undefined) AreEqual(x.SynthBound.FlowNode     ,       x.SynthBound.FlowNode   .AudioFormat(Undefined)); 
                if (val.audioFormat == 0        ) AreEqual(x.SynthBound.FlowNode     ,       x.SynthBound.FlowNode   .AudioFormat(0));
                if (val.audioFormat == null     ) AreEqual(x.SynthBound.FlowNode     ,       x.SynthBound.FlowNode   .AudioFormat(null));});
            
            AssertProp(x => {
                if (val.audioFormat == Raw      ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsRaw());
                if (val.audioFormat == Wav      ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsWav());
                if (val.audioFormat == Undefined) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat(Undefined));
                if (val.audioFormat == 0        ) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat(0));
                if (val.audioFormat == null     ) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.AudioFormat(null)); });
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);

            void AssertProp(Action<ConfigTestEntities> setter)
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.FileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.FileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.FileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.FileExtension(val.fileExtension)));

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.AudioFormat(val.audioFormat)));
            AssertProp(x =>                                         x.TapeBound.TapeConfig.AudioFormat = val.audioFormat);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.AudioFormat(val.audioFormat)));
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.AsWav()); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);
            
            void AssertProp(Action<ConfigTestEntities> setter)
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

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.FileExtension(val.fileExtension, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.FileExtension(val.fileExtension, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.AudioFormat(val.audioFormat, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AudioFormat(val.audioFormat, x.SynthBound.Context)));
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.AsWav(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsRaw(x.SynthBound.Context));
                if (val.audioFormat == Wav) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.AsWav(x.SynthBound.Context)); });
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
                ConfigTestEntities x = default;

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

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.FileExtension(val.fileExtension, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioFormat(val.audioFormat, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (val.audioFormat == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsRaw(x.SynthBound.Context));
                    if (val.audioFormat == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsWav(x.SynthBound.Context)); });
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

                AssertProp(() => x.Immutable.AudioFormat.FileExtension(val.fileExtension));
                AssertProp(() => val.fileExtension.FileExtensionToAudioFormat());
                
                AssertProp(() => x.Immutable.AudioFormat.AudioFormat(val.audioFormat));
                AssertProp(() => val.audioFormat.AudioFormat());
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormat.AsRaw() : x.Immutable.AudioFormat.AsWav());
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
                
                AssertProp(() => x.Immutable.AudioFormatEntity.FileExtension(val.fileExtension, x.SynthBound.Context));
                
                AssertProp(() => x.Immutable.AudioFormatEntity.AudioFormat(val.audioFormat, x.SynthBound.Context));
                AssertProp(() => val.audioFormat.ToEntity(x.SynthBound.Context));
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormatEntity.AsRaw(x.SynthBound.Context) : x.Immutable.AudioFormatEntity.AsWav(x.SynthBound.Context));
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
        public void ConfigSection_FileExtension()
        {
            // Synth-Bound. Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(".wav", () => DefaultAudioFormat.FileExtension());
            AreEqual(DefaultAudioFormat.FileExtension(), () => configSection.FileExtension());
        }
        
        [TestMethod]
        public void FileExtension_EdgeCases()
        {
            // For code coverage
            ThrowsException(() => ".abc".FileExtensionToAudioFormat());
        }

        // Getter Helpers
        
        private void Assert_All_Getters(ConfigTestEntities x, string fileExtension)
        {
            Assert_Bound_Getters(x, fileExtension);
            Assert_Independent_Getters(x, fileExtension);
            Assert_Immutable_Getters(x, fileExtension);
        }

        private void Assert_Bound_Getters(ConfigTestEntities x, string fileExtension)
        {
            Assert_SynthBound_Getters(x, fileExtension);
            Assert_TapeBound_Getters(x, fileExtension);
            Assert_BuffBound_Getters(x, fileExtension);
        }
        
        private void Assert_Independent_Getters(ConfigTestEntities x, string fileExtension)
        {
            // Independent after Taping
            Assert_Independent_Getters(x.Independent.Sample, fileExtension);
        }

        private void Assert_Immutable_Getters(ConfigTestEntities x, string fileExtension)
        {
            Assert_Immutable_Getters(x.Immutable.AudioFormat, fileExtension);
            Assert_Immutable_Getters(x.Immutable.AudioFormatEntity, fileExtension);
            Assert_Immutable_Getters(x.Immutable.WavHeader, fileExtension);
        }

        private void Assert_SynthBound_Getters(ConfigTestEntities x, string fileExtension)
        {
            IsTrue(() => x.SynthBound.SynthWishes.FileExtension().Is(fileExtension));
            IsTrue(() => x.SynthBound.FlowNode.FileExtension().Is(fileExtension));
            IsTrue(() => x.SynthBound.ConfigResolver.FileExtension().Is(fileExtension));
            
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.SynthWishes.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.SynthWishes.IsRaw);
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.FlowNode.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.FlowNode.IsRaw);
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.ConfigResolver.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.SynthBound.ConfigResolver.IsRaw);
            
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.SynthWishes.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.SynthWishes.IsWav);
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.FlowNode.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.FlowNode.IsWav);
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.ConfigResolver.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.SynthBound.ConfigResolver.IsWav);
        }

        private void Assert_TapeBound_Getters(ConfigTestEntities x, string fileExtension)
        {
            IsTrue(() => x.TapeBound.Tape.FileExtension().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeConfig.FileExtension().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeActions.FileExtension().Is(fileExtension));
            IsTrue(() => x.TapeBound.TapeAction.FileExtension().Is(fileExtension));
            
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.Tape.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.TapeConfig.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.TapeBound.TapeAction.IsRaw());
        
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.Tape.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.TapeConfig.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.TapeActions.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.TapeBound.TapeAction.IsWav());
        }
                        
        private void Assert_BuffBound_Getters(ConfigTestEntities x, string fileExtension)
        {
            IsTrue(() => x.BuffBound.Buff.FileExtension().Is(fileExtension));
            IsTrue(() => x.BuffBound.AudioFileOutput.FileExtension().Is(fileExtension));
            
            AreEqual(fileExtension.Is(".raw"), () => x.BuffBound.Buff.IsRaw());
            AreEqual(fileExtension.Is(".raw"), () => x.BuffBound.AudioFileOutput.IsRaw());
            
            AreEqual(fileExtension.Is(".wav"), () => x.BuffBound.Buff.IsWav());
            AreEqual(fileExtension.Is(".wav"), () => x.BuffBound.AudioFileOutput.IsWav());
        }

        private void Assert_Independent_Getters(Sample sample, string fileExtension)
        {
            IsTrue(() => sample.FileExtension().Is(fileExtension));
        }
        
        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, string fileExtension)
        {
            IsTrue(() => audioFileFormatEnum.FileExtension().Is(fileExtension));
            IsTrue(() => audioFileFormatEnum.AudioFormatToFileExtension().Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => audioFileFormatEnum.IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => audioFileFormatEnum.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, string fileExtension)
        {
            IsNotNull(() => audioFormatEntity);
            IsTrue(() => audioFormatEntity.FileExtension().Is(fileExtension));
            AreEqual(fileExtension.Is(".raw"), () => audioFormatEntity.IsRaw());
            AreEqual(fileExtension.Is(".wav"), () => audioFormatEntity.IsWav());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, string fileExtension)
        {
            if (fileExtension.Is(".wav"))
            {
                NotEqual(default, () => wavHeader);
                IsTrue(() => wavHeader.IsWav());
                IsTrue(() => wavHeader.FileExtension().Is(fileExtension));
            }
            else
            {
                AreEqual(default, () => wavHeader);
                IsFalse(() => wavHeader.IsRaw());
                IsFalse(() => wavHeader.FileExtension().Is(fileExtension));
            }
        }

        // Test Data Helpers
        
        private ConfigTestEntities CreateTestEntities((string fileExtension, AudioFileFormatEnum? audioFormat) init) 
            => new ConfigTestEntities(x => x.AudioFormat(init.audioFormat));
        
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