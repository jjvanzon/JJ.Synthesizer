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
using static JJ.Framework.Testing.AssertHelper;
using static JJ.Framework.Wishes.Common.FilledInWishes;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using static JJ.Business.Synthesizer.Wishes.Configuration.ConfigWishes;

#pragma warning disable CS0618
#pragma warning disable MSTEST0018

namespace JJ.Business.Synthesizer.Tests.ConfigTests
{
    [TestClass]
    [TestCategory("Configuration")]
    public class BitWishesTests
    {
        [TestMethod]
        [DynamicData(nameof(TestParametersInit))]
        public void Init_Bits(int? init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, CoalesceBits(init));
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParametersWithEmpty))]
        public void SynthBound_Bits(int? init, int? value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, CoalesceBits(init));
                
                setter(x);
                
                Assert_SynthBound_Getters (x, CoalesceBits(value));
                Assert_TapeBound_Getters  (x, CoalesceBits(init ));
                Assert_BuffBound_Getters  (x, CoalesceBits(init ));
                Assert_Independent_Getters(x, CoalesceBits(init ));
                Assert_Immutable_Getters  (x, CoalesceBits(init ));
                
                x.Record();
                Assert_All_Getters(x, CoalesceBits(value));
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .Bits(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .Bits(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.Bits(value)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,    x.SynthBound.SynthWishes   .WithBits(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode      .WithBits(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigResolver, x.SynthBound.ConfigResolver.WithBits(value)));
            
            AssertProp(x => {
                if (value == 8 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With8Bit());
                if (value == 16) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With16Bit());
                if (value == 32) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With32Bit()); 
                if (!Has(value)) AreEqual(x.SynthBound.SynthWishes,       x.SynthBound.SynthWishes.Bits(value)); });
                                                                     
            AssertProp(x => {                                        
                if (value == 8 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With8Bit());
                if (value == 16) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With16Bit());
                if (value == 32) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With32Bit()); 
                if (!Has(value)) AreEqual(x.SynthBound.FlowNode,       x.SynthBound.FlowNode.Bits(0)); });
            
            AssertProp(x => {
                if (value == 8 ) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With8Bit());
                if (value == 16) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With16Bit());
                if (value == 32) AreEqual(x.SynthBound.ConfigResolver, () => x.SynthBound.ConfigResolver.With32Bit());
                if (!Has(value)) AreEqual(x.SynthBound.ConfigResolver,       x.SynthBound.ConfigResolver.Bits(default)); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_Bits(int init, int value)
        {
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape       .Bits  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig .Bits  (value)));
            AssertProp(x =>                                         x.TapeBound.TapeConfig .Bits = value);
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits  (value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction .Bits  (value)));
            
            AssertProp(x => {
                if (value ==  8) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With8Bit());
                if (value == 16) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With16Bit());
                if (value == 32) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With32Bit()); });
            
            AssertProp(x => {
                if (value ==  8) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With8Bit());
                if (value == 16) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With16Bit());
                if (value == 32) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With32Bit()); });
            
            AssertProp(x => {
                if (value ==  8) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With8Bit());
                if (value == 16) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With16Bit());
                if (value == 32) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With32Bit()); });
            
            AssertProp(x => {
                if (value ==  8) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With8Bit());
                if (value == 16) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With16Bit());
                if (value == 32) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With32Bit()); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_Bits(int init, int value)
        {
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

            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff           .Bits(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Bits(value, x.SynthBound.Context)));
            
            AssertProp(x => {
                if (value ==  8) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With8Bit (x.SynthBound.Context));
                if (value == 16) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With16Bit(x.SynthBound.Context));
                if (value == 32) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With32Bit(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (value ==  8) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With8Bit (x.SynthBound.Context));
                if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With16Bit(x.SynthBound.Context));
                if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With32Bit(x.SynthBound.Context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_Bits(int init, int value)
        {
            // Independent after Taping

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
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits(value, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value ==  8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit (x.SynthBound.Context));
                    if (value == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit(x.SynthBound.Context));
                    if (value == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit(x.SynthBound.Context)); });
            }
            
            // AudioInfoWish
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, value);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Bits  (value)));
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits = value);
                
                AssertProp(() => {
                    if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit());
                    if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                    if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });
            }
                        
            // AudioFileInfo
            {
                TestEntities x = default;

                void AssertProp(Action setter)
                {
                    x = CreateTestEntities(init);
                    Assert_All_Getters(x, init);
                    
                    setter();
                    
                    Assert_Bound_Getters(x, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Bits(value)));
                
                AssertProp(() => {
                    if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit());
                    if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                    if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutable_Bits(int init, int value)
        {
            var x = CreateTestEntities(init);

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
                
                AssertProp(() => x.Immutable.WavHeader.Bits(value));
                
                AssertProp(() => 
                {
                    if (value ==  8) return x.Immutable.WavHeader.With8Bit();
                    if (value == 16) return x.Immutable.WavHeader.With16Bit();
                    if (value == 32) return x.Immutable.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }

            // SampleDataTypeEnum

            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init);
                    
                    SampleDataTypeEnum sampleDataTypeEnum2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, init);
                    Assert_Immutable_Getters(sampleDataTypeEnum2, value);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.Bits(value));
                AssertProp(() => value.BitsToEnum());
                
                AssertProp(() => 
                {
                    if (value ==  8) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (value == 16) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (value == 32) return x.Immutable.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }

            // SampleDataType

            var sampleDataTypes = new List<SampleDataType>();
            {
                void AssertProp(Func<SampleDataType> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init);

                    SampleDataType sampleDataType2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.SampleDataType, init);
                    Assert_Immutable_Getters(sampleDataType2, value);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            
                AssertProp(() => x.Immutable.SampleDataType.Bits(value, x.SynthBound.Context));
                AssertProp(() => value.BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (value ==  8) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (value == 16) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (value == 32) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
                    return default; // ncrunch: no coverage
                });

            }

            // Type

            var types = new List<Type>();
            {
                void AssertProp(Func<Type> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.Type, init);
                    
                    var type2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Type, init);
                    Assert_Immutable_Getters(type2, value);
                    
                    types.Add(type2);
                }

                AssertProp(() => x.Immutable.Type.Bits(value));
                AssertProp(() => value.BitsToType());
                
                AssertProp(() => 
                {
                    if (value ==  8) return x.Immutable.Type.With8Bit();
                    if (value == 16) return x.Immutable.Type.With16Bit();
                    if (value == 32) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            Assert_All_Getters(x, init);
            
            // Except for our variables
            wavHeaders         .ForEach(w => Assert_Immutable_Getters(w, value));
            sampleDataTypeEnums.ForEach(e => Assert_Immutable_Getters(e, value));
            sampleDataTypes    .ForEach(s => Assert_Immutable_Getters(s, value));
            types              .ForEach(t => Assert_Immutable_Getters(t, value));
        }

        [TestMethod]
        public void ConfigSection_Bits()
        {
            // Synth-Bound. Immutable. Get-only.
            var x = CreateTestEntities(default);
            var configSection = x.SynthBound.ConfigSection;
            AreEqual(DefaultBits,       () => configSection.Bits);
            AreEqual(DefaultBits,       () => configSection.Bits());
            AreEqual(DefaultBits == 8,  () => configSection.Is8Bit());
            AreEqual(DefaultBits == 16, () => configSection.Is16Bit());
            AreEqual(DefaultBits == 32, () => configSection.Is32Bit());
        }

        [TestMethod]
        public void Bits_WithTypeArguments()
        {
            // Getters
            AreEqual(8, () => Bits<byte>());
            AreEqual(16, () => Bits<short>());
            AreEqual(32, () => Bits<float>());
        
            // Conversion-Style Getters
            AreEqual(8, () => TypeToBits<byte>());
            AreEqual(16, () => TypeToBits<short>());
            AreEqual(32, () => TypeToBits<float>());

            // Shorthand Getters          
            IsTrue(() => Is8Bit<byte>());
            IsFalse(() => Is8Bit<short>());
            IsFalse(() => Is8Bit<float>());

            IsFalse(() => Is16Bit<byte>());
            IsTrue(() => Is16Bit<short>());
            IsFalse(() => Is16Bit<float>());

            IsFalse(() => Is32Bit<byte>());
            IsFalse(() => Is32Bit<short>());
            IsTrue(() => Is32Bit<float>());

            // Setters
            AreEqual(typeof(byte), () => Bits<byte>(8));
            AreEqual(typeof(byte), () => Bits<short>(8));
            AreEqual(typeof(byte), () => Bits<float>(8));
            
            AreEqual(typeof(short), () => Bits<byte>(16));
            AreEqual(typeof(short), () => Bits<short>(16));
            AreEqual(typeof(short), () => Bits<float>(16));
            
            AreEqual(typeof(float), () => Bits<byte>(32));
            AreEqual(typeof(float), () => Bits<short>(32));
            AreEqual(typeof(float), () => Bits<float>(32));

            // 'Shorthand' Setters
            AreEqual(typeof(byte), () => With8Bit<byte>());
            AreEqual(typeof(byte), () => With8Bit<short>());
            AreEqual(typeof(byte), () => With8Bit<float>());

            AreEqual(typeof(short), () => With16Bit<byte>());
            AreEqual(typeof(short), () => With16Bit<short>());
            AreEqual(typeof(short), () => With16Bit<float>());

            AreEqual(typeof(float), () => With32Bit<byte>());
            AreEqual(typeof(float), () => With32Bit<short>());
            AreEqual(typeof(float), () => With32Bit<float>());
        }
        
        [TestMethod]
        public void Bits_EdgeCases()
        {
            var x = CreateTestEntities(bits: 32);
            ThrowsException(() => typeof(string).TypeToBits());
            ThrowsException(() => x.TapeBound.TapeConfig.Bits = 0);
            ThrowsException(() => x.TapeBound.TapeConfig.Bits = 3);
        }
        
        // Getter Helpers

        private void Assert_All_Getters(TestEntities x, int bits)
        {
            Assert_Bound_Getters(x, bits);
            Assert_Independent_Getters(x, bits);
            Assert_Immutable_Getters(x, bits);
        }

        private void Assert_Bound_Getters(TestEntities x, int bits)
        {
            Assert_SynthBound_Getters(x, bits);
            Assert_TapeBound_Getters(x, bits);
            Assert_BuffBound_Getters(x, bits);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int bits)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, bits);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, bits);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, bits);
        }

        private void Assert_Immutable_Getters(TestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, bits);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, bits);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, bits);
            Assert_Immutable_Getters(x.Immutable.Type, bits);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigResolver);

            AreEqual(bits, () => x.SynthBound.SynthWishes.Bits());
            AreEqual(bits, () => x.SynthBound.SynthWishes.GetBits);
            AreEqual(bits, () => x.SynthBound.FlowNode.Bits());
            AreEqual(bits, () => x.SynthBound.FlowNode.GetBits);
            AreEqual(bits, () => x.SynthBound.ConfigResolver.Bits());
            AreEqual(bits, () => x.SynthBound.ConfigResolver.GetBits);
            
            AreEqual(bits == 8, () => x.SynthBound.SynthWishes.Is8Bit());
            AreEqual(bits == 8, () => x.SynthBound.SynthWishes.Is8Bit);
            AreEqual(bits == 8, () => x.SynthBound.FlowNode.Is8Bit());
            AreEqual(bits == 8, () => x.SynthBound.FlowNode.Is8Bit);
            AreEqual(bits == 8, () => x.SynthBound.ConfigResolver.Is8Bit());
            AreEqual(bits == 8, () => x.SynthBound.ConfigResolver.Is8Bit);
            
            AreEqual(bits == 16, () => x.SynthBound.SynthWishes.Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.SynthWishes.Is16Bit);
            AreEqual(bits == 16, () => x.SynthBound.FlowNode.Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.FlowNode.Is16Bit);
            AreEqual(bits == 16, () => x.SynthBound.ConfigResolver.Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.ConfigResolver.Is16Bit);
            
            AreEqual(bits == 32, () => x.SynthBound.SynthWishes.Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.SynthWishes.Is32Bit);
            AreEqual(bits == 32, () => x.SynthBound.FlowNode.Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.FlowNode.Is32Bit);
            AreEqual(bits == 32, () => x.SynthBound.ConfigResolver.Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.ConfigResolver.Is32Bit);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int bits)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);

            AreEqual(bits, () => x.TapeBound.Tape.Bits());
            AreEqual(bits, () => x.TapeBound.TapeConfig.Bits());
            AreEqual(bits, () => x.TapeBound.TapeConfig.Bits);
            AreEqual(bits, () => x.TapeBound.TapeActions.Bits());
            AreEqual(bits, () => x.TapeBound.TapeAction.Bits());
            
            AreEqual(bits == 8, () => x.TapeBound.Tape.Is8Bit());
            AreEqual(bits == 8, () => x.TapeBound.TapeConfig.Is8Bit());
            AreEqual(bits == 8, () => x.TapeBound.TapeActions.Is8Bit());
            AreEqual(bits == 8, () => x.TapeBound.TapeAction.Is8Bit());
        
            AreEqual(bits == 16, () => x.TapeBound.Tape.Is16Bit());
            AreEqual(bits == 16, () => x.TapeBound.TapeConfig.Is16Bit());
            AreEqual(bits == 16, () => x.TapeBound.TapeActions.Is16Bit());
            AreEqual(bits == 16, () => x.TapeBound.TapeAction.Is16Bit());
        
            AreEqual(bits == 32, () => x.TapeBound.Tape.Is32Bit());
            AreEqual(bits == 32, () => x.TapeBound.TapeConfig.Is32Bit());
            AreEqual(bits == 32, () => x.TapeBound.TapeActions.Is32Bit());
            AreEqual(bits == 32, () => x.TapeBound.TapeAction.Is32Bit());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int bits)
        {
            IsNotNull(           () => x);
            IsNotNull(           () => x.BuffBound);
            IsNotNull(           () => x.BuffBound.Buff);

            AreEqual(bits,       () => x.BuffBound.Buff.Bits());
            AreEqual(bits,       () => x.BuffBound.AudioFileOutput.Bits());
            
            AreEqual(bits == 8,  () => x.BuffBound.Buff.Is8Bit());
            AreEqual(bits == 8,  () => x.BuffBound.AudioFileOutput.Is8Bit());
            
            AreEqual(bits == 16, () => x.BuffBound.Buff.Is16Bit());
            AreEqual(bits == 16, () => x.BuffBound.AudioFileOutput.Is16Bit());
            
            AreEqual(bits == 32, () => x.BuffBound.Buff.Is32Bit());
            AreEqual(bits == 32, () => x.BuffBound.AudioFileOutput.Is32Bit());
        }
        
        private void Assert_Independent_Getters(Sample sample, int bits)
        {
            IsNotNull(           () => sample);
            AreEqual(bits,       () => sample.Bits());
            AreEqual(bits == 8,  () => sample.Is8Bit());
            AreEqual(bits == 16, () => sample.Is16Bit());
            AreEqual(bits == 32, () => sample.Is32Bit());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int bits)
        {
            IsNotNull(           () => audioInfoWish);
            AreEqual(bits,       () => audioInfoWish.Bits);
            AreEqual(bits,       () => audioInfoWish.Bits());
            AreEqual(bits == 8,  () => audioInfoWish.Is8Bit());
            AreEqual(bits == 16, () => audioInfoWish.Is16Bit());
            AreEqual(bits == 32, () => audioInfoWish.Is32Bit());
        }

        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int bits)
        {
            IsNotNull(           () => audioFileInfo);
            AreEqual(bits,       () => audioFileInfo.Bits());
            AreEqual(bits == 8,  () => audioFileInfo.Is8Bit());
            AreEqual(bits == 16, () => audioFileInfo.Is16Bit());
            AreEqual(bits == 32, () => audioFileInfo.Is32Bit());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int bits)
        {
            AreEqual(bits,       () => wavHeader.BitsPerValue);
            AreEqual(bits,       () => wavHeader.Bits());
            AreEqual(bits == 8,  () => wavHeader.Is8Bit());
            AreEqual(bits == 16, () => wavHeader.Is16Bit());
            AreEqual(bits == 32, () => wavHeader.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int bits)
        {
            AreEqual(bits,       () => sampleDataTypeEnum.Bits());
            AreEqual(bits,       () => sampleDataTypeEnum.EnumToBits());
            AreEqual(bits == 8,  () => sampleDataTypeEnum.Is8Bit());
            AreEqual(bits == 16, () => sampleDataTypeEnum.Is16Bit());
            AreEqual(bits == 32, () => sampleDataTypeEnum.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int bits)
        {
            IsNotNull(           () => sampleDataType);
            AreEqual(bits,       () => sampleDataType.Bits());
            AreEqual(bits,       () => sampleDataType.EntityToBits());
            AreEqual(bits == 8,  () => sampleDataType.Is8Bit());
            AreEqual(bits == 16, () => sampleDataType.Is16Bit());
            AreEqual(bits == 32, () => sampleDataType.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(Type type, int bits)
        {
            IsNotNull(           () => type);
            AreEqual(bits,       () => type.Bits());
            AreEqual(bits,       () => type.TypeToBits());
            AreEqual(bits == 8,  () => type.Is8Bit());
            AreEqual(bits == 16, () => type.Is16Bit());
            AreEqual(bits == 32, () => type.Is32Bit());
        }
 
        // Test Data Helpers

        private TestEntities CreateTestEntities(int? bits) => new TestEntities(x => x.Bits(bits));
                
        static object TestParametersInit => new[] // ncrunch: no coverage
        { 
            new object[] { null },
            new object[] { 0 },
            new object[] { 8 },
            new object[] { 16 },
            new object[] { 32 },
        };

        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { 32, 8 },
            new object[] { 32, 16 },
            new object[] { 16, 32 },
        };

        static object TestParametersWithEmpty => new[] // ncrunch: no coverage
        {
            new object[] { 32, 8 },
            new object[] { 32, 16 },
            new object[] { 16, 32 },
            new object[] { 16, null },
            new object[] { 16, 0 },
            new object[] { null, 16 },
            new object[] { 0, 16 },
        };
                
    } 
}