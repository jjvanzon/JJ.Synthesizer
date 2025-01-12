using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Technical.Attributes.TestEntities;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.AttributeWishes.AttributeExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;

#pragma warning disable CS0618
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.Technical.Attributes
{
    [TestClass]
    [TestCategory("Technical")]
    public class MaxValueWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_MaxValue(int maxValue, int bits)
        { 
            var init = (maxValue, bits);
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init.maxValue);
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_MaxValue(int initMaxValue, int initBits, int maxValue, int bits)
        {
            var init = (maxValue: initMaxValue, bits: initBits);
            var val = (maxValue, bits);
            
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxValue);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, val.maxValue);
                Assert_TapeBound_Getters(x, init.maxValue);
                Assert_BuffBound_Getters(x, init.maxValue);
                Assert_Independent_Getters(x, init.maxValue);
                Assert_Immutable_Getters(x, init.maxValue);
                
                x.Record();
                Assert_All_Getters(x, val.maxValue);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode    .Bits    (val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.Bits    (val.bits)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  x.SynthBound.SynthWishes .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     x.SynthBound.FlowNode    .WithBits(val.bits)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, x.SynthBound.ConfigWishes.WithBits(val.bits)));
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With8Bit());
                if (val.bits == 16) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With16Bit());
                if (val.bits == 32) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With32Bit()); });
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With8Bit());
                if (val.bits == 16) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With16Bit());
                if (val.bits == 32) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With32Bit()); });
            
            AssertProp(x => {
                if (val.bits ==  8) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With8Bit());
                if (val.bits == 16) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With16Bit());
                if (val.bits == 32) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With32Bit()); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_MaxValue(int initMaxValue, int initBits, int maxValue, int bits)
        {
            var init = (maxValue: initMaxValue, bits: initBits);
            var val = (maxValue, bits);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxValue);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.maxValue);
                Assert_TapeBound_Getters(x, val.maxValue);
                Assert_BuffBound_Getters(x, init.maxValue);
                Assert_Independent_Getters(x, init.maxValue);
                Assert_Immutable_Getters(x, init.maxValue);
                
                x.Record();
                
                Assert_All_Getters(x, init.maxValue); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
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
        public void BuffBound_MaxValue(int initMaxValue, int initBits, int maxValue, int bits)
        {
            var init = (maxValue: initMaxValue, bits: initBits);
            var val = (maxValue, bits);

            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init.maxValue);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, init.maxValue);
                Assert_TapeBound_Getters(x, init.maxValue);
                Assert_BuffBound_Getters(x, val.maxValue);
                Assert_Independent_Getters(x, init.maxValue);
                Assert_Immutable_Getters(x, init.maxValue);
                
                x.Record();
                Assert_All_Getters(x, init.maxValue);
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
        public void Independent_MaxValue(int initMaxValue, int initBits, int maxValue, int bits)
        {
            // Independent after Taping

            var init = (maxValue: initMaxValue, bits: initBits);
            var val = (maxValue, bits);

            // Sample
            {
                TestEntities x = default;
                
                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init.maxValue);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxValue);
                    Assert_Independent_Getters(x.Independent.Sample, val.maxValue);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init.maxValue);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init.maxValue);
                    Assert_Immutable_Getters(x, init.maxValue);

                    x.Record();
                    Assert_All_Getters(x, init.maxValue);
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
                    Assert_All_Getters(x, init.maxValue);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxValue);
                    Assert_Independent_Getters(x.Independent.Sample, init.maxValue);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, val.maxValue);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init.maxValue);
                    Assert_Immutable_Getters(x, init.maxValue);

                    x.Record();
                    Assert_All_Getters(x, init.maxValue);
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
                    Assert_All_Getters(x, init.maxValue);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init.maxValue);
                    Assert_Independent_Getters(x.Independent.Sample, init.maxValue);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init.maxValue);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, val.maxValue);
                    Assert_Immutable_Getters(x, init.maxValue);

                    x.Record();
                    Assert_All_Getters(x, init.maxValue);
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
        public void Immutable_MaxValue(int initMaxValue, int initBits, int maxValue, int bits)
        {
            var init = (maxValue: initMaxValue, bits: initBits);
            var val = (maxValue, bits);
            var x = CreateTestEntities(init);

            // WavHeader
            
            var wavHeaders = new List<WavHeaderStruct>();
            {
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init.maxValue);
                    
                    WavHeaderStruct wavHeader2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.WavHeader, init.maxValue);
                    Assert_Immutable_Getters(wavHeader2, val.maxValue);
                    
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
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxValue);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init.maxValue);
                    Assert_Immutable_Getters(sampleDataTypeEnum2, val.maxValue);
                    
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
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init.maxValue);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init.maxValue);
                    Assert_Immutable_Getters(sampleDataType2, val.maxValue);
                    
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
                    Assert_Immutable_Getters(x.Immutable.Type, init.maxValue);
                    
                    var type2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Type, init.maxValue);
                    Assert_Immutable_Getters(type2, val.maxValue);
                    
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
            Assert_All_Getters(x, init.maxValue);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, val.maxValue));
            sampleDataTypeEnums.ForEach(e => Assert_Immutable_Getters(e, val.maxValue));
            sampleDataTypes    .ForEach(s => Assert_Immutable_Getters(s, val.maxValue));
            types              .ForEach(t => Assert_Immutable_Getters(t, val.maxValue));
        }

        [TestMethod]
        public void ConfigSection_MaxValue()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            AreEqual(DefaultBits.MaxValue(), () => configSection.MaxValue());
        }

        [TestMethod]
        public void Bits_WithTypeArguments()
        {
            // ReSharper disable once PossibleLossOfFraction
            AreEqual(byte .MaxValue / 2, () => MaxValue<byte>());
            AreEqual(short.MaxValue,     () => MaxValue<short>());
            AreEqual(1,                  () => MaxValue<float>());
        }
        
        
        [TestMethod]
        public void Bits_EdgeCases()
        {
            // For code coverage
            ThrowsException(() => typeof(string).TypeToBits());
        }

        // Getter Helpers

        private void Assert_All_Getters(TestEntities x, int maxValue)
        {
            Assert_Bound_Getters(x, maxValue);
            Assert_Independent_Getters(x, maxValue);
            Assert_Immutable_Getters(x, maxValue);
        }

        private void Assert_Bound_Getters(TestEntities x, int maxValue)
        {
            Assert_SynthBound_Getters(x, maxValue);
            Assert_TapeBound_Getters(x, maxValue);
            Assert_BuffBound_Getters(x, maxValue);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int maxValue)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, maxValue);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, maxValue);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, maxValue);
        }

        private void Assert_Immutable_Getters(TestEntities x, int maxValue)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, maxValue);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, maxValue);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, maxValue);
            Assert_Immutable_Getters(x.Immutable.Type, maxValue);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int maxValue)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigWishes);

            AreEqual(maxValue, () => x.SynthBound.SynthWishes.MaxValue());
            AreEqual(maxValue, () => x.SynthBound.FlowNode.MaxValue());
            AreEqual(maxValue, () => x.SynthBound.ConfigWishes.MaxValue());
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int maxValue)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);

            AreEqual(maxValue, () => x.TapeBound.Tape.MaxValue());
            AreEqual(maxValue, () => x.TapeBound.TapeConfig.MaxValue());
            AreEqual(maxValue, () => x.TapeBound.TapeActions.MaxValue());
            AreEqual(maxValue, () => x.TapeBound.TapeAction.MaxValue());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int maxValue)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);
            
            AreEqual(maxValue, () => x.BuffBound.Buff.MaxValue());
            AreEqual(maxValue, () => x.BuffBound.AudioFileOutput.MaxValue());
        }
        
        private void Assert_Independent_Getters(Sample sample, int maxValue)
        {
            IsNotNull(         () => sample);
            AreEqual(maxValue, () => sample.MaxValue());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int maxValue)
        {
            IsNotNull(         () => audioInfoWish);
            AreEqual(maxValue, () => audioInfoWish.MaxValue());
        }
        
        void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int maxValue)
        {
            IsNotNull(         () => audioFileInfo);
            AreEqual(maxValue, () => audioFileInfo.MaxValue());
        }
        
        void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int maxValue)
        {
            AreEqual(maxValue, () => wavHeader.MaxValue());
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int maxValue)
        {
            AreEqual(maxValue, () => sampleDataTypeEnum.MaxValue());
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int maxValue)
        {
            IsNotNull(         () => sampleDataType);
            AreEqual(maxValue, () => sampleDataType.MaxValue());
        }
        
        private void Assert_Immutable_Getters(Type type, int maxValue)
        {
            IsNotNull(         () => type);
            AreEqual(maxValue, () => type.MaxValue());
        }
        
         // Test Data Helpers

        private TestEntities CreateTestEntities((double maxValue, int bits) init) => new TestEntities(x => x.Bits(init.bits));
        
        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { 1, 32, short.MaxValue, 16 },
            new object[] { short.MaxValue, 16, 1, 32 },
            new object[] { byte.MaxValue / 2, 8, short.MaxValue, 16 },
            new object[] { byte.MaxValue / 2, 8, 1, 32 }
        };
        
        static object TestParametersInit => new[] // ncrunch: no coverage
        {
            new object[] { byte.MaxValue / 2, 8 },
            new object[] { short.MaxValue, 16 },
            new object[] { 1, 32 }
        };
    } 
}