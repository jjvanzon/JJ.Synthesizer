using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Enums.AudioFileFormatEnum;
using static JJ.Business.Synthesizer.Tests.ConfigTests.TestEntities;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0611
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Configuration")]
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

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .AudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.AudioFormat(val.audioFormat)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithAudioFormat(val.audioFormat)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithAudioFormat(val.audioFormat)));
            
            AssertProp(x => { switch (val.audioFormat) {
                case Raw: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsRaw()); break;
                case Wav: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .AsWav()); break;
                default : AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithAudioFormat(val.audioFormat)); break; } });
                                                                
            AssertProp(x => { switch (val.audioFormat) {        
                case Raw: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsRaw()); break;
                case Wav: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AsWav()); break;
                default : AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .AudioFormat(val.audioFormat)); break; } });
            
            AssertProp(x => { switch (val.audioFormat) {
                case Raw: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsRaw()); break;
                case Wav: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AsWav()); break;
                default : AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.AudioFormat(val.audioFormat)); break; } });
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

                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.AudioFormat(val.audioFormat, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (val.audioFormat == Raw) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsRaw(x.SynthBound.Context));
                    if (val.audioFormat == Wav) AreEqual(x.Independent.Sample, () => x.Independent.Sample.AsWav(x.SynthBound.Context)); });
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

                AssertProp(() => x.Immutable.AudioFormat.AudioFormat(val.audioFormat));
                AssertProp(() => val.audioFormat.AudioFormat());
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormat.AsRaw() : x.Immutable.AudioFormat.AsWav());
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
                
                AssertProp(() => x.Immutable.AudioFormatEntity.AudioFormat(val.audioFormat, x.SynthBound.Context));
                AssertProp(() => val.audioFormat.ToEntity(x.SynthBound.Context));
                AssertProp(() => val.audioFormat == Raw ? x.Immutable.AudioFormatEntity.AsRaw(x.SynthBound.Context) : x.Immutable.AudioFormatEntity.AsWav(x.SynthBound.Context));
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.headerLength);
            
            // Except for our variables
            audioFormats.ForEach(e => Assert_Immutable_Getters(e, val.headerLength));
            audioFormatEntities.ForEach(s => Assert_Immutable_Getters(s, val.headerLength));
        }

        [TestMethod] 
        public void GlobalBound_HeaderLength()
        {
            // Immutable. Get-only.
            AreEqual(44, () => DefaultAudioFormat.HeaderLength());
            AreEqual(DefaultAudioFormat.HeaderLength(), () => GetConfigSectionAccessor().HeaderLength());
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
            AreEqual(headerLength, () => x.SynthBound.SynthWishes.HeaderLength());
            AreEqual(headerLength, () => x.SynthBound.FlowNode.HeaderLength());
            AreEqual(headerLength, () => x.SynthBound.ConfigResolver.HeaderLength());
            
            AreEqual(headerLength == 0, () => x.SynthBound.SynthWishes.IsRaw());
            AreEqual(headerLength == 0, () => x.SynthBound.SynthWishes.IsRaw);
            AreEqual(headerLength == 0, () => x.SynthBound.FlowNode.IsRaw());
            AreEqual(headerLength == 0, () => x.SynthBound.FlowNode.IsRaw);
            AreEqual(headerLength == 0, () => x.SynthBound.ConfigResolver.IsRaw());
            AreEqual(headerLength == 0, () => x.SynthBound.ConfigResolver.IsRaw);
            
            AreEqual(headerLength == 44, () => x.SynthBound.SynthWishes.IsWav());
            AreEqual(headerLength == 44, () => x.SynthBound.SynthWishes.IsWav);
            AreEqual(headerLength == 44, () => x.SynthBound.FlowNode.IsWav());
            AreEqual(headerLength == 44, () => x.SynthBound.FlowNode.IsWav);
            AreEqual(headerLength == 44, () => x.SynthBound.ConfigResolver.IsWav());
            AreEqual(headerLength == 44, () => x.SynthBound.ConfigResolver.IsWav);
        }

        private void Assert_TapeBound_Getters(TestEntities x, int headerLength)
        {
            AreEqual(headerLength, () => x.TapeBound.Tape.HeaderLength());
            AreEqual(headerLength, () => x.TapeBound.TapeConfig.HeaderLength());
            AreEqual(headerLength, () => x.TapeBound.TapeActions.HeaderLength());
            AreEqual(headerLength, () => x.TapeBound.TapeAction.HeaderLength());
            
            AreEqual(headerLength == 0, () => x.TapeBound.Tape.IsRaw());
            AreEqual(headerLength == 0, () => x.TapeBound.TapeConfig.IsRaw());
            AreEqual(headerLength == 0, () => x.TapeBound.TapeActions.IsRaw());
            AreEqual(headerLength == 0, () => x.TapeBound.TapeAction.IsRaw());
        
            AreEqual(headerLength == 44, () => x.TapeBound.Tape.IsWav());
            AreEqual(headerLength == 44, () => x.TapeBound.TapeConfig.IsWav());
            AreEqual(headerLength == 44, () => x.TapeBound.TapeActions.IsWav());
            AreEqual(headerLength == 44, () => x.TapeBound.TapeAction.IsWav());
        }
                        
        private void Assert_BuffBound_Getters(TestEntities x, int headerLength)
        {
            AreEqual(headerLength, () => x.BuffBound.Buff.HeaderLength());
            AreEqual(headerLength, () => x.BuffBound.AudioFileOutput.HeaderLength());
            
            AreEqual(headerLength == 0, () => x.BuffBound.Buff.IsRaw());
            AreEqual(headerLength == 0, () => x.BuffBound.AudioFileOutput.IsRaw());
            
            AreEqual(headerLength == 44, () => x.BuffBound.Buff.IsWav());
            AreEqual(headerLength == 44, () => x.BuffBound.AudioFileOutput.IsWav());
        }

        private void Assert_Independent_Getters(Sample sample, int headerLength)
        {
            AreEqual(headerLength, () => sample.HeaderLength());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormatEnum audioFileFormatEnum, int headerLength)
        {
            AreEqual(headerLength, () => audioFileFormatEnum.HeaderLength());
            AreEqual(headerLength == 0, () => audioFileFormatEnum.IsRaw());
            AreEqual(headerLength == 44, () => audioFileFormatEnum.IsWav());
        }
        
        private void Assert_Immutable_Getters(AudioFileFormat audioFormatEntity, int headerLength)
        {
            IsNotNull(() => audioFormatEntity);
            AreEqual(headerLength, () => audioFormatEntity.HeaderLength());
            AreEqual(headerLength == 0, () => audioFormatEntity.IsRaw());
            AreEqual(headerLength == 44, () => audioFormatEntity.IsWav());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int headerLength)
        {
            if (headerLength == 44)
            {
                NotEqual(default, () => wavHeader);
                IsTrue(() => wavHeader.IsWav());
                AreEqual(headerLength, () => wavHeader.HeaderLength());
            }
            else
            {
                AreEqual(default, () => wavHeader);
                IsFalse(() => wavHeader.IsRaw());
                NotEqual(headerLength, () => wavHeader.HeaderLength());
            }
        }

        // Test Data Helpers
        
        private TestEntities CreateTestEntities((int headerLength, AudioFileFormatEnum? audioFormat) init) 
            => new TestEntities(x => x.AudioFormat(init.audioFormat));
        
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