using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.Configuration;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.ConfigTests.TestEntities;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;

#pragma warning disable CS0618
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Configuration")]
    public class MaxAmplitudeWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_MaxAmplitude(int maxAmplitude, int? bits)
        { 
            var init = (maxAmplitude, bits);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.maxAmplitude);
        }

        [TestMethod]
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_MaxAmplitude(int initMaxAmplitude, int? initBits, int maxAmplitude, int? bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters        (x, init.maxAmplitude);
                
                setter(x);
                
                Assert_SynthBound_Getters (x, val .maxAmplitude);
                Assert_TapeBound_Getters  (x, init.maxAmplitude);
                Assert_BuffBound_Getters  (x, init.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters  (x, init.maxAmplitude);
                
                x.Record();
                Assert_All_Getters        (x, val .maxAmplitude);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits    (val.bits)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithBits(val.bits)));
            
            AssertProp(x => { switch (val.bits) {
                case  8: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With8Bit ()); break;
                case 16: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With16Bit()); break;
                case 32: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .With32Bit()); break;
                default: AreEqual(x.SynthBound.SynthWishes,    () => x.SynthBound.SynthWishes   .WithBits (val.bits)); break; } });
            
            AssertProp(x => { switch (val.bits) {
                case  8: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With8Bit ()); break;
                case 16: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With16Bit()); break;
                case 32: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .With32Bit()); break; 
                default: AreEqual(x.SynthBound.FlowNode,       () => x.SynthBound.FlowNode      .Bits     (val.bits)); break; } });
            
            AssertProp(x => { switch (val.bits) {
                case  8: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With8Bit ()); break;
                case 16: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With16Bit()); break;
                case 32: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With32Bit()); break;
                default: AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.Bits     (val.bits)); break; } });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxAmplitude);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.maxAmplitude);
                Assert_TapeBound_Getters(x, val.maxAmplitude);
                Assert_BuffBound_Getters(x, init.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters(x, init.maxAmplitude);
                
                x.Record();
                
                Assert_All_Getters(x, init.maxAmplitude); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
            }

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Bits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Bits  (val.bits)));
            AssertProp(x =>                                         x.TapeBound.TapeConfig .Bits = val.bits);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits  (val.bits)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Bits  (val.bits)));
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With8Bit());
                if (val.bits == 16) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With16Bit());
                if (val.bits == 32) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With32Bit()); });
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With8Bit());
                if (val.bits == 16) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With16Bit());
                if (val.bits == 32) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With32Bit()); });
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With8Bit());
                if (val.bits == 16) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With16Bit());
                if (val.bits == 32) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With32Bit()); });
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With8Bit());
                if (val.bits == 16) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With16Bit());
                if (val.bits == 32) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With32Bit()); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxAmplitude);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.maxAmplitude);
                Assert_TapeBound_Getters(x, init.maxAmplitude);
                Assert_BuffBound_Getters(x, val.maxAmplitude);
                Assert_Independent_Getters(x, init.maxAmplitude);
                Assert_Immutable_Getters(x, init.maxAmplitude);
                
                x.Record();
                Assert_All_Getters(x, init.maxAmplitude);
            }

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Bits(val.bits, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Bits(val.bits, x.SynthBound.Context)));
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With8Bit (x.SynthBound.Context));
                if (val.bits == 16) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With16Bit(x.SynthBound.Context));
                if (val.bits == 32) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With32Bit(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With8Bit (x.SynthBound.Context));
                if (val.bits == 16) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With16Bit(x.SynthBound.Context));
                if (val.bits == 32) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With32Bit(x.SynthBound.Context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            // Independent after Taping

            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);

            // Sample
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxAmplitude);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.Sample, val.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init.maxAmplitude);
                    Assert_Immutable_Getters(x, init.maxAmplitude);

                    x.Record();
                    Assert_All_Getters(x, init.maxAmplitude);
                }
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits(val.bits, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (val.bits ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit (x.SynthBound.Context));
                    if (val.bits == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit(x.SynthBound.Context));
                    if (val.bits == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit(x.SynthBound.Context)); });
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxAmplitude);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.Sample, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, val.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init.maxAmplitude);
                    Assert_Immutable_Getters(x, init.maxAmplitude);

                    x.Record();
                    Assert_All_Getters(x, init.maxAmplitude);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Bits  (val.bits)));
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits = val.bits);
                
                AssertProp(() => {
                    if (val.bits ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit());
                    if (val.bits == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                    if (val.bits == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxAmplitude);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.Sample, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init.maxAmplitude);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, val.maxAmplitude);
                    Assert_Immutable_Getters(x, init.maxAmplitude);

                    x.Record();
                    Assert_All_Getters(x, init.maxAmplitude);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Bits(val.bits)));
                
                AssertProp(() => {
                    if (val.bits ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit());
                    if (val.bits == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                    if (val.bits == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutable_MaxAmplitude(int initMaxAmplitude, int initBits, int maxAmplitude, int bits)
        {
            var init = (maxAmplitude: initMaxAmplitude, bits: initBits);
            var val = (maxAmplitude, bits);
            var x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init.maxAmplitude);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init.maxAmplitude);
                    Assert_Immutable_Getters(wavHeader2, val.maxAmplitude);
                    
                    wavHeaders.Add(wavHeader2);
                }
                
                AssertProp(() => x.Immutable.WavHeader.Bits(val.bits));
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.WavHeader.With8Bit();
                    if (val.bits == 16) return x.Immutable.WavHeader.With16Bit();
                    if (val.bits == 32) return x.Immutable.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }

            // SampleDataTypeEnum

            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxAmplitude);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxAmplitude);
                    Assert_Immutable_Getters(sampleDataTypeEnum2, val.maxAmplitude);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.Bits(val.bits));
                AssertProp(() => val.bits.BitsToEnum());
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (val.bits == 16) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (val.bits == 32) return x.Immutable.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }

            // SampleDataType

            var sampleDataTypes = new List<SampleDataType>();
            {
                void AssertProp(Func<SampleDataType> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init.maxAmplitude);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init.maxAmplitude);
                    Assert_Immutable_Getters(sampleDataType2, val.maxAmplitude);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            
                AssertProp(() => x.Immutable.SampleDataType.Bits(val.bits, x.SynthBound.Context));
                AssertProp(() => val.bits.BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (val.bits == 16) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (val.bits == 32) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
                    return default; // ncrunch: no coverage
                });

            }

            // Type

            var types = new List<Type>();
            {
                void AssertProp(Func<Type> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.Type, init.maxAmplitude);
                    
                    var type2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Type, init.maxAmplitude);
                    Assert_Immutable_Getters(type2, val.maxAmplitude);
                    
                    types.Add(type2);
                }

                AssertProp(() => x.Immutable.Type.Bits(val.bits));
                AssertProp(() => val.bits.BitsToType());
                
                AssertProp(() => 
                {
                    if (val.bits ==  8) return x.Immutable.Type.With8Bit();
                    if (val.bits == 16) return x.Immutable.Type.With16Bit();
                    if (val.bits == 32) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init.maxAmplitude);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, val.maxAmplitude));
            sampleDataTypeEnums.ForEach(e => Assert_Immutable_Getters(e, val.maxAmplitude));
            sampleDataTypes    .ForEach(s => Assert_Immutable_Getters(s, val.maxAmplitude));
            types              .ForEach(t => Assert_Immutable_Getters(t, val.maxAmplitude));
        }

        [TestMethod]
        public void ConfigSection_MaxAmplitude()
        {
            // Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(1, () => DefaultBits.MaxAmplitude());
            AreEqual(DefaultBits.MaxAmplitude(), () => configSection.MaxAmplitude());
        }

        [TestMethod]
        public void MaxAmplitude_WithTypeArguments()
        {
            // ReSharper disable once PossibleLossOfFraction
            AreEqual(byte .MaxValue/ 2, () => MaxAmplitude<byte>());
            AreEqual(short.MaxValue,    () => MaxAmplitude<short>());
            AreEqual(1,                 () => MaxAmplitude<float>());
        }

        // Getter Helpers

        private void Assert_All_Getters(TestEntities x, int maxAmplitude)
        {
            Assert_Bound_Getters(x, maxAmplitude);
            Assert_Independent_Getters(x, maxAmplitude);
            Assert_Immutable_Getters(x, maxAmplitude);
        }

        private void Assert_Bound_Getters(TestEntities x, int maxAmplitude)
        {
            Assert_SynthBound_Getters(x, maxAmplitude);
            Assert_TapeBound_Getters(x, maxAmplitude);
            Assert_BuffBound_Getters(x, maxAmplitude);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int maxAmplitude)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, maxAmplitude);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, maxAmplitude);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, maxAmplitude);
        }

        private void Assert_Immutable_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, maxAmplitude);
            Assert_Immutable_Getters(x.Immutable.Type, maxAmplitude);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);

            AreEqual(maxAmplitude, () => x.SynthBound.SynthWishes.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.FlowNode.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.SynthBound.ConfigResolver.MaxAmplitude());
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);

            AreEqual(maxAmplitude, () => x.TapeBound.Tape.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeConfig.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeActions.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.TapeBound.TapeAction.MaxAmplitude());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int maxAmplitude)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            
            AreEqual(maxAmplitude, () => x.BuffBound.Buff.MaxAmplitude());
            AreEqual(maxAmplitude, () => x.BuffBound.AudioFileOutput.MaxAmplitude());
        }
        
        private void Assert_Independent_Getters(Sample sample, int maxAmplitude)
        {
            IsNotNull(         () => sample);
            AreEqual(maxAmplitude, () => sample.MaxAmplitude());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int maxAmplitude)
        {
            IsNotNull(         () => audioInfoWish);
            AreEqual(maxAmplitude, () => audioInfoWish.MaxAmplitude());
        }
        
        void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int maxAmplitude)
        {
            IsNotNull(         () => audioFileInfo);
            AreEqual(maxAmplitude, () => audioFileInfo.MaxAmplitude());
        }
        
        void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => wavHeader.MaxAmplitude());
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int maxAmplitude)
        {
            AreEqual(maxAmplitude, () => sampleDataTypeEnum.MaxAmplitude());
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int maxAmplitude)
        {
            IsNotNull(         () => sampleDataType);
            AreEqual(maxAmplitude, () => sampleDataType.MaxAmplitude());
        }
        
        private void Assert_Immutable_Getters(Type type, int maxAmplitude)
        {
            IsNotNull(         () => type);
            AreEqual(maxAmplitude, () => type.MaxAmplitude());
        }
        
         // Test Data Helpers

        private TestEntities CreateTestEntities((double maxAmplitude, int? bits) init) => new TestEntities(x => x.Bits(init.bits));
        
        // ncrunch: no coverage start
        
        static object TestParametersInit => new[]
        {
            new object[] { byte .MaxValue / 2 ,    8 },
            new object[] { short.MaxValue     ,   16 },
            new object[] { 1                  ,   32 },
            new object[] { 1                  , null },
            new object[] { 1                  ,    0 }
        };
        
        static object TestParameters => new[] 
        {
            new object[] { 1                  , 32 , short.MaxValue ,   16 },
            new object[] { short.MaxValue     , 16 , 1              ,   32 },
            new object[] { byte .MaxValue / 2 ,  8 , short.MaxValue ,   16 },
            new object[] { byte .MaxValue / 2 ,  8 , 1              ,   32 },
        };
        
        static object TestParametersWithEmpty => new[] 
        {
            new object[] { 1                  , 32 , short.MaxValue ,   16 },
            new object[] { short.MaxValue     , 16 , 1              ,   32 },
            new object[] { byte .MaxValue / 2 ,  8 , short.MaxValue ,   16 },
            new object[] { byte .MaxValue / 2 ,  8 , 1              ,   32 },
            new object[] { 1                  ,  0 , short.MaxValue ,   16 },
            new object[] { byte .MaxValue / 2 ,  8 , 1              , null }
        };
        
         // ncrunch: no coverage end
    } 
}