using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Tests.Technical.TestEntities;
using static JJ.Business.Synthesizer.Wishes.AttributeWishes.AttributeExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable MSTEST0018 // DynamicData members should be IEnumerable<object[]>

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class SizeOfBitDepthWishesTests
    {
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(4)]
        public void Init_SizeOfBitDepth(int init)
        { 
            var x = CreateTestEntities(init);
            Assert_All_Getters(x, init);
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void SynthBound_SizeOfBitDepth(int init, int value)
        {
            void AssertProp(Action<TestEntities> setter)
            {
                var x = CreateTestEntities(init);
                Assert_All_Getters(x, init);
                
                setter(x);
                
                Assert_SynthBound_Getters(x, value);
                Assert_TapeBound_Getters(x, init);
                Assert_BuffBound_Getters(x, init);
                Assert_Independent_Getters(x, init);
                Assert_Immutable_Getters(x, init);
                
                x.Record();
                Assert_All_Getters(x, value);
            }

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes .SizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode    .SizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.SizeOfBitDepth(value)));
            
            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,        x.SynthBound.SynthWishes .Bits    (value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,           x.SynthBound.FlowNode    .Bits    (value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes,       x.SynthBound.ConfigWishes.Bits    (value * 8)));

            AssertProp(x => AreEqual(x.SynthBound.SynthWishes,        x.SynthBound.SynthWishes .WithBits(value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.FlowNode,           x.SynthBound.FlowNode    .WithBits(value * 8)));
            AssertProp(x => AreEqual(x.SynthBound.ConfigWishes,       x.SynthBound.ConfigWishes.WithBits(value * 8)));
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With8Bit());
                if (value == 2) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With16Bit());
                if (value == 4) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With8Bit());
                if (value == 2) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With16Bit());
                if (value == 4) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With8Bit());
                if (value == 2) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With16Bit());
                if (value == 4) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With32Bit()); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void TapeBound_SizeOfBitDepth(int init, int value)
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

            AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.SizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.SizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.SizeOfBitDepth(value)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.SizeOfBitDepth(value)));
            
            AssertProp(x => AreEqual(x.TapeBound.Tape,        x.TapeBound.Tape.Bits(value * 8)));
            AssertProp(x =>                                   x.TapeBound.TapeConfig.Bits = value * 8);
            AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  x.TapeBound.TapeConfig.Bits(value * 8)));
            AssertProp(x => AreEqual(x.TapeBound.TapeActions, x.TapeBound.TapeActions.Bits(value * 8)));
            AssertProp(x => AreEqual(x.TapeBound.TapeAction,  x.TapeBound.TapeAction.Bits(value * 8)));
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.Tape, () => x.TapeBound.Tape.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.TapeConfig, () => x.TapeBound.TapeConfig.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.With32Bit()); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With8Bit());
                if (value == 2) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With16Bit());
                if (value == 4) AreEqual(x.TapeBound.TapeAction, () => x.TapeBound.TapeAction.With32Bit()); });
        }

        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void BuffBound_SizeOfBitDepth(int init, int value)
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
            
            AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.SizeOfBitDepth(value, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.SizeOfBitDepth(value, x.SynthBound.Context)));

            AssertProp(x => AreEqual(x.BuffBound.Buff,            x.BuffBound.Buff.Bits(value * 8, x.SynthBound.Context)));
            AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, x.BuffBound.AudioFileOutput.Bits(value * 8, x.SynthBound.Context)));
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With8Bit(x.SynthBound.Context));
                if (value == 2) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With16Bit(x.SynthBound.Context));
                if (value == 4) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With32Bit(x.SynthBound.Context)); });
            
            AssertProp(x => {
                if (value == 1) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With8Bit(x.SynthBound.Context));
                if (value == 2) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With16Bit(x.SynthBound.Context));
                if (value == 4) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With32Bit(x.SynthBound.Context)); });
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Independent_SizeOfBitDepth(int init, int value)
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
                
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.SizeOfBitDepth(value, x.SynthBound.Context)));
                AssertProp(() => AreEqual(x.Independent.Sample, x.Independent.Sample.Bits(value * 8, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit(x.SynthBound.Context));
                    if (value == 2) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit(x.SynthBound.Context));
                    if (value == 4) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit(x.SynthBound.Context)); });
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
                
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.SizeOfBitDepth(value)));
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish,       x.Independent.AudioInfoWish.Bits(value * 8)));
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits = value * 8);
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit());
                    if (value == 2) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                    if (value == 4) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });
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
                    Assert_Independent_Getters(x.Independent.AudioFileInfo, value);
                    Assert_Independent_Getters(x.Independent.AudioInfoWish, init);
                    Assert_Independent_Getters(x.Independent.Sample, init);
                    Assert_Immutable_Getters(x, init);

                    x.Record();
                    Assert_All_Getters(x, init);
                }
                
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.SizeOfBitDepth(value)));
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, x.Independent.AudioFileInfo.Bits(value * 8)));
                
                AssertProp(() => {
                    if (value == 1) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit());
                    if (value == 2) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                    if (value == 4) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });
            }
        }
        
        [TestMethod]
        [DynamicData(nameof(TestParameters))]
        public void Immutable_SizeOfBitDepth(int init, int value)
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
                
                AssertProp(() => x.Immutable.WavHeader.SizeOfBitDepth(value));
                AssertProp(() => x.Immutable.WavHeader.Bits(value * 8));
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.WavHeader.With8Bit();
                    if (value == 2) return x.Immutable.WavHeader.With16Bit();
                    if (value == 4) return x.Immutable.WavHeader.With32Bit();
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
                
                AssertProp(() => x.Immutable.SampleDataTypeEnum.SizeOfBitDepth(value));
                AssertProp(() => x.Immutable.SampleDataTypeEnum.Bits(value * 8));
                AssertProp(() => (value * 8).BitsToEnum());
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (value == 2) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (value == 4) return x.Immutable.SampleDataTypeEnum.With32Bit();
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
            
                AssertProp(() => x.Immutable.SampleDataType.SizeOfBitDepth(value, x.SynthBound.Context));
                AssertProp(() => x.Immutable.SampleDataType.Bits(value * 8, x.SynthBound.Context));
                AssertProp(() => (value * 8).BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (value == 2) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (value == 4) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
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

                AssertProp(() => x.Immutable.Type.SizeOfBitDepth(value));
                AssertProp(() => x.Immutable.Type.Bits(value * 8));
                AssertProp(() => (value * 8).BitsToType());
                
                AssertProp(() => 
                {
                    if (value == 1) return x.Immutable.Type.With8Bit();
                    if (value == 2) return x.Immutable.Type.With16Bit();
                    if (value == 4) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
            }
                
                
            // To bits

            var bitsList = new List<int>();
            {
                void AssertProp(Func<int> setter)
                {
                    Assert_Immutable_Getters(x.Immutable.Bits, init);

                    int bits2 = setter();
                    
                    Assert_Immutable_Getters(x.Immutable.Bits, init);
                    Assert_Immutable_Getters(bits2, value);
                    
                    bitsList.Add(bits2);
                }
            
                AssertProp(() => x.Immutable.Bits.SizeOfBitDepth(value));
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
            bitsList           .ForEach(b => Assert_Immutable_Getters(b, value));
        }
        
        [TestMethod]
        public void ConfigSection_SizeOfBitDepth()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = GetConfigSectionAccessor();
            
            AreEqual(DefaultBits / 8, () => configSection.SizeOfBitDepth());
        }

        [TestMethod]
        public void SizeOfBitDepth_WithTypeArguments()
        {
            // Getters
            AreEqual(1, () => SizeOfBitDepth<byte>());
            AreEqual(2, () => SizeOfBitDepth<short>());
            AreEqual(4, () => SizeOfBitDepth<float>());
        
            // Setters
            AreEqual(typeof(byte), () => SizeOfBitDepth<byte>(1));
            AreEqual(typeof(byte), () => SizeOfBitDepth<short>(1));
            AreEqual(typeof(byte), () => SizeOfBitDepth<float>(1));
            
            AreEqual(typeof(short), () => SizeOfBitDepth<byte>(2));
            AreEqual(typeof(short), () => SizeOfBitDepth<short>(2));
            AreEqual(typeof(short), () => SizeOfBitDepth<float>(2));
            
            AreEqual(typeof(float), () => SizeOfBitDepth<byte>(4));
            AreEqual(typeof(float), () => SizeOfBitDepth<short>(4));
            AreEqual(typeof(float), () => SizeOfBitDepth<float>(4));
            
            // Conversion-Style Getters
            AreEqual(1, () => TypeToSizeOfBitDepth<byte>());
            AreEqual(2, () => TypeToSizeOfBitDepth<short>());
            AreEqual(4, () => TypeToSizeOfBitDepth<float>());

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
        public void SizeOfBitDepth_EdgeCases()
        {
            // For code coverage
            ThrowsException(() => typeof(string).TypeToSizeOfBitDepth());
            ThrowsException(() => (-1).SizeOfBitDepthToType());
        }

        // Helpers

        private TestEntities CreateTestEntities(int sizeOfBitDepth) => new TestEntities(x => x.SizeOfBitDepth(sizeOfBitDepth));
        
        static object TestParameters => new[] // ncrunch: no coverage
        {
            new object[] { 1, 2 },
            new object[] { 1, 4 },
            new object[] { 2, 1 },
            new object[] { 2, 4 },
            new object[] { 4, 1 },
            new object[] { 4, 2 },
            new object[] { 4, 4 },
        };

        private void Assert_All_Getters(TestEntities x, int sizeOfBitDepth)
        {
            Assert_Bound_Getters(x, sizeOfBitDepth);
            Assert_Independent_Getters(x, sizeOfBitDepth);
            Assert_Immutable_Getters(x, sizeOfBitDepth);
        }

        private void Assert_Bound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            Assert_SynthBound_Getters(x, sizeOfBitDepth);
            Assert_TapeBound_Getters(x, sizeOfBitDepth);
            Assert_BuffBound_Getters(x, sizeOfBitDepth);
        }
        
        private void Assert_Independent_Getters(TestEntities x, int sizeOfBitDepth)
        {
            // Independent after Taping
            IsNotNull(() => x);
            IsNotNull(() => x.Independent);
            Assert_Independent_Getters(x.Independent.Sample, sizeOfBitDepth);
            Assert_Independent_Getters(x.Independent.AudioInfoWish, sizeOfBitDepth);
            Assert_Independent_Getters(x.Independent.AudioFileInfo, sizeOfBitDepth);
        }

        private void Assert_Immutable_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.Immutable);
            Assert_Immutable_Getters(x.Immutable.WavHeader, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.SampleDataTypeEnum, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.SampleDataType, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.Type, sizeOfBitDepth);
            Assert_Immutable_Getters(x.Immutable.Bits, sizeOfBitDepth);
        }

        private void Assert_SynthBound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.SynthBound);
            IsNotNull(() => x.SynthBound.SynthWishes);
            IsNotNull(() => x.SynthBound.FlowNode);
            IsNotNull(() => x.SynthBound.ConfigWishes);

            AreEqual(sizeOfBitDepth, () => x.SynthBound.SynthWishes .SizeOfBitDepth() );
            AreEqual(sizeOfBitDepth, () => x.SynthBound.FlowNode    .SizeOfBitDepth() );
            AreEqual(sizeOfBitDepth, () => x.SynthBound.ConfigWishes.SizeOfBitDepth() );
            
            AreEqual(sizeOfBitDepth, x.SynthBound.SynthWishes .Bits()  / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.SynthWishes .GetBits / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.FlowNode    .Bits()  / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.FlowNode    .GetBits / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.ConfigWishes.Bits()  / 8);
            AreEqual(sizeOfBitDepth, x.SynthBound.ConfigWishes.GetBits / 8);
            
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.SynthWishes.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.SynthWishes.Is8Bit);
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.FlowNode.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.FlowNode.Is8Bit);
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.ConfigWishes.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.SynthBound.ConfigWishes.Is8Bit);
            
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.SynthWishes.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.SynthWishes.Is16Bit);
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.FlowNode.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.FlowNode.Is16Bit);
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.ConfigWishes.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.SynthBound.ConfigWishes.Is16Bit);
            
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.SynthWishes.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.SynthWishes.Is32Bit);
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.FlowNode.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.FlowNode.Is32Bit);
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.ConfigWishes.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.SynthBound.ConfigWishes.Is32Bit);
        }
        
        private void Assert_TapeBound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.TapeBound);
            IsNotNull(() => x.TapeBound.Tape);
            IsNotNull(() => x.TapeBound.TapeConfig);
            IsNotNull(() => x.TapeBound.TapeActions);
            IsNotNull(() => x.TapeBound.TapeAction);

            AreEqual(sizeOfBitDepth, () => x.TapeBound.Tape.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeConfig.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeActions.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.TapeBound.TapeAction.SizeOfBitDepth());
            
            AreEqual(sizeOfBitDepth, x.TapeBound.Tape       .Bits() / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeConfig .Bits() / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeConfig .Bits   / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeActions.Bits() / 8);
            AreEqual(sizeOfBitDepth, x.TapeBound.TapeAction .Bits() / 8);
            
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.Tape.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.TapeConfig.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.TapeActions.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.TapeBound.TapeAction.Is8Bit());
        
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.Tape.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.TapeConfig.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.TapeActions.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.TapeBound.TapeAction.Is16Bit());
        
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.Tape.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.TapeConfig.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.TapeActions.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.TapeBound.TapeAction.Is32Bit());
        }
        
        private void Assert_BuffBound_Getters(TestEntities x, int sizeOfBitDepth)
        {
            IsNotNull(() => x);
            IsNotNull(() => x.BuffBound);
            IsNotNull(() => x.BuffBound.Buff);

            AreEqual(sizeOfBitDepth, () => x.BuffBound.Buff.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth, () => x.BuffBound.AudioFileOutput.SizeOfBitDepth());
            
            AreEqual(sizeOfBitDepth * 8, () => x.BuffBound.Buff.Bits());
            AreEqual(sizeOfBitDepth * 8, () => x.BuffBound.AudioFileOutput.Bits());
            
            AreEqual(sizeOfBitDepth == 1, () => x.BuffBound.Buff.Is8Bit());
            AreEqual(sizeOfBitDepth == 1, () => x.BuffBound.AudioFileOutput.Is8Bit());
            
            AreEqual(sizeOfBitDepth == 2, () => x.BuffBound.Buff.Is16Bit());
            AreEqual(sizeOfBitDepth == 2, () => x.BuffBound.AudioFileOutput.Is16Bit());
            
            AreEqual(sizeOfBitDepth == 4, () => x.BuffBound.Buff.Is32Bit());
            AreEqual(sizeOfBitDepth == 4, () => x.BuffBound.AudioFileOutput.Is32Bit());
        }
                
        private void Assert_Independent_Getters(Sample sample, int sizeOfBitDepth)
        {
            IsNotNull(                    () => sample);
            AreEqual(sizeOfBitDepth,      () => sample.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => sample.Bits());
            AreEqual(sizeOfBitDepth == 1, () => sample.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => sample.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => sample.Is32Bit());
        }
        
        private void Assert_Independent_Getters(AudioInfoWish audioInfoWish, int sizeOfBitDepth)
        {
            IsNotNull(                    () => audioInfoWish);
            AreEqual(sizeOfBitDepth,      () => audioInfoWish.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => audioInfoWish.Bits);
            AreEqual(sizeOfBitDepth * 8,  () => audioInfoWish.Bits());
            AreEqual(sizeOfBitDepth == 1, () => audioInfoWish.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => audioInfoWish.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => audioInfoWish.Is32Bit());
        }
        
        private void Assert_Independent_Getters(AudioFileInfo audioFileInfo, int sizeOfBitDepth)
        {
            IsNotNull(                    () => audioFileInfo);
            AreEqual(sizeOfBitDepth,      () => audioFileInfo.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => audioFileInfo.Bits());
            AreEqual(sizeOfBitDepth == 1, () => audioFileInfo.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => audioFileInfo.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => audioFileInfo.Is32Bit());
        }

        private void Assert_Immutable_Getters(WavHeaderStruct wavHeader, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth,      () => wavHeader.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => wavHeader.BitsPerValue);
            AreEqual(sizeOfBitDepth * 8,  () => wavHeader.Bits());
            AreEqual(sizeOfBitDepth == 1, () => wavHeader.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => wavHeader.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => wavHeader.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(SampleDataTypeEnum sampleDataTypeEnum, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth,      () => sampleDataTypeEnum.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => sampleDataTypeEnum.Bits());
            AreEqual(sizeOfBitDepth * 8,  () => sampleDataTypeEnum.EnumToBits());
            AreEqual(sizeOfBitDepth == 1, () => sampleDataTypeEnum.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => sampleDataTypeEnum.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => sampleDataTypeEnum.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(SampleDataType sampleDataType, int sizeOfBitDepth)
        {
            IsNotNull(                    () => sampleDataType);
            AreEqual(sizeOfBitDepth,      () => sampleDataType.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => sampleDataType.Bits());
            AreEqual(sizeOfBitDepth * 8,  () => sampleDataType.EntityToBits());
            AreEqual(sizeOfBitDepth == 1, () => sampleDataType.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => sampleDataType.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => sampleDataType.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(Type type, int sizeOfBitDepth)
        {
            IsNotNull(                    () => type);
            AreEqual(sizeOfBitDepth,      () => type.SizeOfBitDepth());
            AreEqual(sizeOfBitDepth * 8,  () => type.Bits());
            AreEqual(sizeOfBitDepth * 8,  () => type.TypeToBits());
            AreEqual(sizeOfBitDepth == 1, () => type.Is8Bit());
            AreEqual(sizeOfBitDepth == 2, () => type.Is16Bit());
            AreEqual(sizeOfBitDepth == 4, () => type.Is32Bit());
        }
        
        private void Assert_Immutable_Getters(int bits, int sizeOfBitDepth)
        {
            AreEqual(sizeOfBitDepth, () => bits.SizeOfBitDepth());
        }
    } 
}