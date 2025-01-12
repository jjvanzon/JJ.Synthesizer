using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.Technical.Attributes.TestEntities;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Attributes
{
    [TestClass]
    [TestCategory("Technical")]
    public class FileExtensionWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_FileExtension(string initFileExtension, int initAudioFormatInt)
        { 
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.fileExtension);
        }

        [TestMethod] 
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_FileExtension(string initFileExtension, int initAudioFormatInt, string fileExtension, int audioFormatInt)
        {
            var init = (fileExtension: initFileExtension, audioFormat: (AudioFileFormatEnum)initAudioFormatInt);
            var val  = (fileExtension, audioFormat: (AudioFileFormatEnum)audioFormatInt);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.fileExtension);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, val.fileExtension);
                Assert_TapeBound_Getters(x, init.fileExtension);
                Assert_BuffBound_Getters(x, init.fileExtension);
                Assert_Independent_Getters(x, init.fileExtension);
                Assert_Immutable_Getters(x, init.fileExtension);
                
                x.Record();
                Assert_All_Getters(x, val.fileExtension);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes .FileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode    .FileExtension(val.fileExtension)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.FileExtension(val.fileExtension)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes .AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode    .AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.AudioFormat(val.audioFormat)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode    .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.WithAudioFormat(val.audioFormat)));
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.AsWav()); });
            
            AssertProp(x => {
                if (val.audioFormat == Raw) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.AsRaw());
                if (val.audioFormat == Wav) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.AsWav()); });
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
        public void GlobalBound_FileExtension()
        {
            // Immutable. Get-only.
            AreEqual(".wav",                             () => DefaultAudioFormat.FileExtension());
            AreEqual(DefaultAudioFormat.FileExtension(), () => GetConfigSectionAccessor().FileExtension());
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
            //Assert_Immutable_Getters(x.Immutable.FileExtension, fileExtension);
            Assert_Immutable_Getters(x.Immutable.WavHeader, fileExtension);
        }

        private void Assert_SynthBound_Getters(TestEntities x, string fileExtension)
        {
            AreEqual(fileExtension, () => x.SynthBound.SynthWishes.FileExtension());
            AreEqual(fileExtension, () => x.SynthBound.FlowNode.FileExtension());
            AreEqual(fileExtension, () => x.SynthBound.ConfigWishes.FileExtension());
            
            //AreEqual(fileExtension == Raw, () => x.SynthBound.SynthWishes.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.SynthBound.SynthWishes.IsRaw);
            //AreEqual(fileExtension == Raw, () => x.SynthBound.FlowNode.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.SynthBound.FlowNode.IsRaw);
            //AreEqual(fileExtension == Raw, () => x.SynthBound.ConfigWishes.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.SynthBound.ConfigWishes.IsRaw);
            
            //AreEqual(fileExtension == Wav, () => x.SynthBound.SynthWishes.IsWav());
            //AreEqual(fileExtension == Wav, () => x.SynthBound.SynthWishes.IsWav);
            //AreEqual(fileExtension == Wav, () => x.SynthBound.FlowNode.IsWav());
            //AreEqual(fileExtension == Wav, () => x.SynthBound.FlowNode.IsWav);
            //AreEqual(fileExtension == Wav, () => x.SynthBound.ConfigWishes.IsWav());
            //AreEqual(fileExtension == Wav, () => x.SynthBound.ConfigWishes.IsWav);
        }

        private void Assert_TapeBound_Getters(TestEntities x, string fileExtension)
        {
            AreEqual(fileExtension, () => x.TapeBound.Tape.FileExtension());
            AreEqual(fileExtension, () => x.TapeBound.TapeConfig.FileExtension());
            AreEqual(fileExtension, () => x.TapeBound.TapeActions.FileExtension());
            AreEqual(fileExtension, () => x.TapeBound.TapeAction.FileExtension());
            
            //AreEqual(fileExtension == Raw, () => x.TapeBound.Tape.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.TapeBound.TapeConfig.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.TapeBound.TapeActions.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.TapeBound.TapeAction.IsRaw());
        
            //AreEqual(fileExtension == Wav, () => x.TapeBound.Tape.IsWav());
            //AreEqual(fileExtension == Wav, () => x.TapeBound.TapeConfig.IsWav());
            //AreEqual(fileExtension == Wav, () => x.TapeBound.TapeActions.IsWav());
            //AreEqual(fileExtension == Wav, () => x.TapeBound.TapeAction.IsWav());
        }
                        
        private void Assert_BuffBound_Getters(TestEntities x, string fileExtension)
        {
            AreEqual(fileExtension, () => x.BuffBound.Buff.FileExtension());
            AreEqual(fileExtension, () => x.BuffBound.AudioFileOutput.FileExtension());
            
            //AreEqual(fileExtension == Raw, () => x.BuffBound.Buff.IsRaw());
            //AreEqual(fileExtension == Raw, () => x.BuffBound.AudioFileOutput.IsRaw());
            
            //AreEqual(fileExtension == Wav, () => x.BuffBound.Buff.IsWav());
            //AreEqual(fileExtension == Wav, () => x.BuffBound.AudioFileOutput.IsWav());
        }

        private void Assert_Independent_Getters(Sample sample, string fileExtension)
        {
            AreEqual(fileExtension,        () => sample.FileExtension());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, string fileExtension)
        {
            AreEqual(fileExtension,        () => audioFileFormatEnum.FileExtension());
            //AreEqual(fileExtension == Raw, () => audioFileFormatEnum.IsRaw());
            //AreEqual(fileExtension == Wav, () => audioFileFormatEnum.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, string fileExtension)
        {
            if (audioFormatEntity == null) throw new NullException(() => audioFormatEntity);
            AreEqual(fileExtension,        () => audioFormatEntity.FileExtension());
            //AreEqual(fileExtension == Raw, () => audioFormatEntity.IsRaw());
            //AreEqual(fileExtension == Wav, () => audioFormatEntity.IsWav());
        }
         
        //private void Assert_Immutable_Getters(string fileExtension, string fileExtension)
        //{
        //    AreEqual(fileExtension,        () => fileExtension.AudioFormat());
        //    AreEqual(fileExtension == Raw, () => fileExtension.IsRaw());
        //    AreEqual(fileExtension == Wav, () => fileExtension.IsWav());
        //}

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, string fileExtension)
        {
            if (fileExtension == ".wav")
            {
                NotEqual(default, () => wavHeader);
                IsTrue(() => wavHeader.IsWav());
                AreEqual(fileExtension, () => wavHeader.FileExtension());
            }
            else
            {
                AreEqual(default, () => wavHeader);
                IsFalse(() => wavHeader.IsRaw());
                NotEqual(fileExtension, () => wavHeader.FileExtension());
            }
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities((string fileExtension, AudioFileFormatEnum audioFormat) init) 
            => new TestEntities(x => x.AudioFormat(init.audioFormat));
        
        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { ".raw", (int)Raw, ".wav", (int)Wav },
            new object[] { ".wav", (int)Wav, ".raw", (int)Raw }
        };
         
        static object TestParametersInit => new[] // ncrunch: no coverage
        {
            new object[] { ".raw", (int)Raw },
            new object[] { ".wav", (int)Wav }
        };
    } 
}