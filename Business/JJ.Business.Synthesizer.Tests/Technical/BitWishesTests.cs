using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.AttributeWishes.AttributeExtensionWishes;
using static JJ.Business.Synthesizer.Wishes.ConfigWishes;
using static JJ.Framework.Testing.AssertHelper;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class BitWishesTests
    {
        [TestMethod] public void Test_Bits_InTandem()
        {
            Test_Bits_InTandem(32, 8);
            Test_Bits_InTandem(32, 16);
            Test_Bits_InTandem(16, 32);
        }
        
        void Test_Bits_InTandem(int init, int value)
        {
            // Check Before Change
            { 
                var x = CreateTestEntities(init);
                x.Assert_All_Getters(init);
            }

            // Synth-Bound Changes
            {
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,  () => x.SynthBound.SynthWishes.Bits(value)));
                AssertProp(x => AreEqual(x.SynthBound.SynthWishes,        x.SynthBound.SynthWishes.WithBits(value)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,     () => x.SynthBound.FlowNode.Bits(value)));
                AssertProp(x => AreEqual(x.SynthBound.FlowNode,           x.SynthBound.FlowNode.WithBits(value)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.Bits(value)));
                AssertProp(x => AreEqual(x.SynthBound.ConfigWishes,       x.SynthBound.ConfigWishes.WithBits(value)));
                
                AssertProp(x => {
                    if (value == 8 ) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With8Bit());
                    if (value == 16) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With16Bit());
                    if (value == 32) AreEqual(x.SynthBound.SynthWishes, () => x.SynthBound.SynthWishes.With32Bit()); });
                
                AssertProp(x => {
                    if (value == 8 ) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With8Bit());
                    if (value == 16) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With16Bit());
                    if (value == 32) AreEqual(x.SynthBound.FlowNode, () => x.SynthBound.FlowNode.With32Bit()); });
                
                AssertProp(x => {
                    if (value == 8 ) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With8Bit());
                    if (value == 16) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With16Bit());
                    if (value == 32) AreEqual(x.SynthBound.ConfigWishes, () => x.SynthBound.ConfigWishes.With32Bit()); });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = CreateTestEntities(init);
                    x.Assert_All_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Getters(value);
                    x.Assert_TapeBound_Getters(init);
                    x.Assert_BuffBound_Getters(init);
                    x.Assert_Independent_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Getters(value);
                }
            }

            // Tape-Bound Changes
            {
                AssertProp(x => AreEqual(x.TapeBound.Tape,        () => x.TapeBound.Tape.Bits(value)));
                AssertProp(x => AreEqual(x.TapeBound.TapeConfig,  () => x.TapeBound.TapeConfig.Bits(value)));
                AssertProp(x =>                                         x.TapeBound.TapeConfig.Bits = value);
                AssertProp(x => AreEqual(x.TapeBound.TapeActions, () => x.TapeBound.TapeActions.Bits(value)));
                AssertProp(x => AreEqual(x.TapeBound.TapeAction,  () => x.TapeBound.TapeAction.Bits(value)));
                
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
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = CreateTestEntities(init);
                    x.Assert_All_Getters(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Getters(init);
                    x.Assert_TapeBound_Getters(value);
                    x.Assert_BuffBound_Getters(init);
                    x.Assert_Independent_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    
                    x.Record();
                    
                    x.Assert_All_Getters(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                AssertProp(x => AreEqual(x.BuffBound.Buff,            () => x.BuffBound.Buff.Bits(value, x.SynthBound.Context)));
                AssertProp(x => AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.Bits(value, x.SynthBound.Context)));
                
                AssertProp(x => {
                    if (value == 8) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With8Bit(x.SynthBound.Context));
                    if (value == 16) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With16Bit(x.SynthBound.Context));
                    if (value == 32) AreEqual(x.BuffBound.Buff, () => x.BuffBound.Buff.With32Bit(x.SynthBound.Context)); });
                
                AssertProp(x => {
                    if (value == 8) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With8Bit(x.SynthBound.Context));
                    if (value == 16) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With16Bit(x.SynthBound.Context));
                    if (value == 32) AreEqual(x.BuffBound.AudioFileOutput, () => x.BuffBound.AudioFileOutput.With32Bit(x.SynthBound.Context)); });

                void AssertProp(Action<TestEntities> setter)
                {    
                    var x = CreateTestEntities(init);
                    x.Assert_All_Getters(init);
                    
                    setter(x);

                    x.Assert_SynthBound_Getters(init);
                    x.Assert_TapeBound_Getters(init);
                    x.Assert_BuffBound_Getters(value);
                    x.Assert_Independent_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    
                    x.Record();
                    x.Assert_All_Getters(init);
                }
            }
        }
        
        // Bits for Independently Changeable
        
        [TestMethod] public void Test_Bits_IndependentAfterTaping()
        {
            Test_Bits_IndependentAfterTaping(init: 32, value: 8);
            Test_Bits_IndependentAfterTaping(init: 32, value: 16);
            Test_Bits_IndependentAfterTaping(init: 16, value: 32);
        }
        
        void Test_Bits_IndependentAfterTaping(int init, int value)
        {
            // Independent after Taping
            var x = CreateTestEntities(init);

            // Sample
            {
                AssertProp(() => AreEqual(x.Independent.Sample, () => x.Independent.Sample.Bits(value, x.SynthBound.Context)));
                
                AssertProp(() => {
                    if (value == 8) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With8Bit(x.SynthBound.Context));
                    if (value == 16) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With16Bit(x.SynthBound.Context));
                    if (value == 32) AreEqual(x.Independent.Sample, () => x.Independent.Sample.With32Bit(x.SynthBound.Context)); });
                
                void AssertProp(Action setter)
                {
                    Initialize(x, init);
                    x.Assert_All_Getters(init);
                    
                    setter();
                    
                    x.Independent.Sample.Assert_Bit_Getters(value);
                    
                    x.Independent.AudioInfoWish.Assert_Bit_Getters(init);
                    x.Independent.AudioFileInfo.Assert_Bit_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    x.Assert_Bound_Bit_Getters(init);

                    x.Record();
                    x.Assert_All_Getters(init);
                }
            }
            
            // AudioInfoWish
            {
                AssertProp(() => AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.Bits(value)));
                AssertProp(() =>                                             x.Independent.AudioInfoWish.Bits = value);
                
                AssertProp(() => {
                    if (value ==  8) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With8Bit());
                    if (value == 16) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With16Bit());
                    if (value == 32) AreEqual(x.Independent.AudioInfoWish, () => x.Independent.AudioInfoWish.With32Bit()); });
                
                void AssertProp(Action setter)
                {
                    Initialize(x, init);
                    x.Assert_All_Getters(init);
                    
                    setter();
                    
                    x.Independent.AudioInfoWish.Assert_Bit_Getters(value);
                    
                    x.Independent.AudioFileInfo.Assert_Bit_Getters(init);
                    x.Independent.Sample.Assert_Bit_Getters(init);
                    x.Assert_Immutable_Getters(init);
                    x.Assert_Bound_Bit_Getters(init);

                    x.Record();
                    x.Assert_All_Getters(init);
                }
            }
                        
            // AudioFileInfo
            {
                AssertProp(() => AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.Bits(value)));
                
                AssertProp(() => {
                    if (value ==  8) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With8Bit());
                    if (value == 16) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With16Bit());
                    if (value == 32) AreEqual(x.Independent.AudioFileInfo, () => x.Independent.AudioFileInfo.With32Bit()); });
                
                void AssertProp(Action setter)
                {
                    Initialize(x, init);
                    x.Assert_All_Getters(init);
                    
                    setter();
                    
                    x.Independent.AudioFileInfo.Assert_Bit_Getters(value);
                    
                    x.Independent.AudioInfoWish.Assert_Bit_Getters(init);
                    x.Independent.Sample.Assert_Bit_Getters(init);
                    x.Assert_Bound_Bit_Getters(init);
                    x.Assert_Immutable_Getters(init);

                    x.Record();
                    x.Assert_All_Getters(init);
                }
            }
        }
        
        // Bits for Immutables

        [TestMethod] public void Test_Bits_Immutable()
        {
            Test_Bits_Immutable(init: 32, value: 8);
            Test_Bits_Immutable(init: 32, value: 16);
            Test_Bits_Immutable(init: 16, value: 32);
        }
        
        void Test_Bits_Immutable(int init, int value)
        {
            var x = CreateTestEntities(init);

            var wavHeaders = new List<WavHeaderStruct>();
            {
                AssertProp(() => x.Immutable.WavHeader.Bits(value));
                
                AssertProp(() => 
                {
                    if (value == 8) return x.Immutable.WavHeader.With8Bit();
                    if (value == 16) return x.Immutable.WavHeader.With16Bit();
                    if (value == 32) return x.Immutable.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    x.Immutable.WavHeader.Assert_Bit_Getters(init);
                    
                    var wavHeader2 = setter();
                    
                    x.Immutable.WavHeader.Assert_Bit_Getters(init);
                    wavHeader2.Assert_Bit_Getters(value);
                    
                    wavHeaders.Add(wavHeader2);
                }
            }
            
            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                AssertProp(() => x.Immutable.SampleDataTypeEnum.Bits(value));
                AssertProp(() => value.BitsToEnum());
                
                AssertProp(() => 
                {
                    if (value ==  8) return x.Immutable.SampleDataTypeEnum.With8Bit();
                    if (value == 16) return x.Immutable.SampleDataTypeEnum.With16Bit();
                    if (value == 32) return x.Immutable.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    x.Immutable.SampleDataTypeEnum.Assert_Bit_Getters(init);
                    
                    var sampleDataTypeEnum2 = setter();
                    
                    x.Immutable.SampleDataTypeEnum.Assert_Bit_Getters(init);
                    sampleDataTypeEnum2.Assert_Bit_Getters(value);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
            }
                        
            var sampleDataTypes = new List<SampleDataType>();
            {
                AssertProp(() => x.Immutable.SampleDataType.Bits(value, x.SynthBound.Context));
                AssertProp(() => value.BitsToEntity(x.SynthBound.Context));
                
                AssertProp(() => 
                {
                    if (value == 8) return x.Immutable.SampleDataType.With8Bit(x.SynthBound.Context);
                    if (value == 16) return x.Immutable.SampleDataType.With16Bit(x.SynthBound.Context);
                    if (value == 32) return x.Immutable.SampleDataType.With32Bit(x.SynthBound.Context);
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<SampleDataType> setter)
                {
                    x.Immutable.SampleDataType.Assert_Bit_Getters(init);

                    var sampleDataType2 = setter();
                    
                    x.Immutable.SampleDataType.Assert_Bit_Getters(init);
                    sampleDataType2.Assert_Bit_Getters(value);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            }
            
            var types = new List<Type>();
            {
                AssertProp(() => x.Immutable.Type.Bits(value));
                AssertProp(() => value.BitsToType());
                
                AssertProp(() => 
                {
                    if (value == 8) return x.Immutable.Type.With8Bit();
                    if (value == 16) return x.Immutable.Type.With16Bit();
                    if (value == 32) return x.Immutable.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<Type> setter)
                {
                    x.Immutable.Type.Assert_Bit_Getters(init);
                    
                    var type2 = setter();
                    
                    x.Immutable.Type.Assert_Bit_Getters(init);
                    type2.Assert_Bit_Getters(value);
                    
                    types.Add(type2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            x.Assert_All_Getters(init);
            
            // Except for our variables
            wavHeaders         .ForEach(w => w.Assert_Bit_Getters(value));
            sampleDataTypeEnums.ForEach(e => e.Assert_Bit_Getters(value));
            sampleDataTypes    .ForEach(s => s.Assert_Bit_Getters(value));
            types              .ForEach(t => t.Assert_Bit_Getters(value));
        }

        // Bits in ConfigSection
        
        [TestMethod] public void Test_Bits_ConfigSection()
        {
            // Global-Bound. Immutable. Get-only.
            var configSection = TestEntities.GetConfigSectionAccessor();
            
            AreEqual(DefaultBits,       () => configSection.Bits);
            AreEqual(DefaultBits,       () => configSection.Bits());
            AreEqual(DefaultBits == 8,  () => configSection.Is8Bit());
            AreEqual(DefaultBits == 16, () => configSection.Is16Bit());
            AreEqual(DefaultBits == 32, () => configSection.Is32Bit());
        }

        // Bits With Type Arguments
        
        [TestMethod] public void Test_Bits_TypeArguments()
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
        
        
        [TestMethod] public void Test_Bits_EdgeCases()
        {
            // For code coverage
            ThrowsException(() => typeof(string).TypeToBits());
        }

        // Helpers

        private TestEntities CreateTestEntities(int bits) => new TestEntities(x => x.Bits(bits));
        private static void Initialize(TestEntities x, int bits) => x.Initialize(s => s.Bits(bits));
    }
    
    internal static class BitWishesTestExtensions
    {
        
        public static void Assert_All_Getters(this TestEntities x, int bits)
        {
            x.Assert_SynthBound_Getters(bits);
            x.Assert_TapeBound_Getters(bits);
            x.Assert_BuffBound_Getters(bits);
            x.Assert_Independent_Getters(bits);
            x.Assert_Immutable_Getters(bits);
        }

        public static void Assert_Bound_Bit_Getters(this TestEntities x, int bits)
        {
            x.Assert_SynthBound_Getters(bits);
            x.Assert_TapeBound_Getters(bits);
            x.Assert_BuffBound_Getters(bits);
        }

        public static void Assert_SynthBound_Getters(this TestEntities x, int bits)
        {
            AreEqual(bits, () => x.SynthBound.SynthWishes.Bits());
            AreEqual(bits, () => x.SynthBound.SynthWishes.GetBits);
            AreEqual(bits, () => x.SynthBound.FlowNode.Bits());
            AreEqual(bits, () => x.SynthBound.FlowNode.GetBits);
            AreEqual(bits, () => x.SynthBound.ConfigWishes.Bits());
            AreEqual(bits, () => x.SynthBound.ConfigWishes.GetBits);
            
            AreEqual(bits == 8, () => x.SynthBound.SynthWishes.Is8Bit());
            AreEqual(bits == 8, () => x.SynthBound.SynthWishes.Is8Bit);
            AreEqual(bits == 8, () => x.SynthBound.FlowNode.Is8Bit());
            AreEqual(bits == 8, () => x.SynthBound.FlowNode.Is8Bit);
            AreEqual(bits == 8, () => x.SynthBound.ConfigWishes.Is8Bit());
            AreEqual(bits == 8, () => x.SynthBound.ConfigWishes.Is8Bit);
            
            AreEqual(bits == 16, () => x.SynthBound.SynthWishes.Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.SynthWishes.Is16Bit);
            AreEqual(bits == 16, () => x.SynthBound.FlowNode.Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.FlowNode.Is16Bit);
            AreEqual(bits == 16, () => x.SynthBound.ConfigWishes.Is16Bit());
            AreEqual(bits == 16, () => x.SynthBound.ConfigWishes.Is16Bit);
            
            AreEqual(bits == 32, () => x.SynthBound.SynthWishes.Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.SynthWishes.Is32Bit);
            AreEqual(bits == 32, () => x.SynthBound.FlowNode.Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.FlowNode.Is32Bit);
            AreEqual(bits == 32, () => x.SynthBound.ConfigWishes.Is32Bit());
            AreEqual(bits == 32, () => x.SynthBound.ConfigWishes.Is32Bit);
        }
        
        public static void Assert_TapeBound_Getters(this TestEntities x, int bits)
        {
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
        
        public static void Assert_BuffBound_Getters(this TestEntities x, int bits)
        {
            AreEqual(bits, () => x.BuffBound.Buff.Bits());
            AreEqual(bits, () => x.BuffBound.AudioFileOutput.Bits());
            
            AreEqual(bits == 8, () => x.BuffBound.Buff.Is8Bit());
            AreEqual(bits == 8, () => x.BuffBound.AudioFileOutput.Is8Bit());
            
            AreEqual(bits == 16, () => x.BuffBound.Buff.Is16Bit());
            AreEqual(bits == 16, () => x.BuffBound.AudioFileOutput.Is16Bit());
            
            AreEqual(bits == 32, () => x.BuffBound.Buff.Is32Bit());
            AreEqual(bits == 32, () => x.BuffBound.AudioFileOutput.Is32Bit());
        }

        public static void Assert_Independent_Getters(this TestEntities x, int bits)
        {
            // Independent after Taping
            x.Independent.Sample.Assert_Bit_Getters(bits);
            x.Independent.AudioInfoWish.Assert_Bit_Getters(bits);
            x.Independent.AudioFileInfo.Assert_Bit_Getters(bits);
        }

        public static void Assert_Immutable_Getters(this TestEntities x, int bits)
        {
            x.Immutable.WavHeader.Assert_Bit_Getters(bits);
            x.Immutable.SampleDataTypeEnum.Assert_Bit_Getters(bits);
            x.Immutable.SampleDataType.Assert_Bit_Getters(bits);
            x.Immutable.Type.Assert_Bit_Getters(bits);
        }

        public static void Assert_Bit_Getters(this AudioFileInfo audioFileInfo, int bits)
        {
            AreEqual(bits,       () => audioFileInfo.Bits());
            AreEqual(bits == 8,  () => audioFileInfo.Is8Bit());
            AreEqual(bits == 16, () => audioFileInfo.Is16Bit());
            AreEqual(bits == 32, () => audioFileInfo.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this Sample sample, int bits)
        {
            AreEqual(bits,       () => sample.Bits());
            AreEqual(bits == 8,  () => sample.Is8Bit());
            AreEqual(bits == 16, () => sample.Is16Bit());
            AreEqual(bits == 32, () => sample.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this AudioInfoWish audioInfoWish, int bits)
        {
            AreEqual(bits,       () => audioInfoWish.Bits());
            AreEqual(bits,       () => audioInfoWish.Bits);
            AreEqual(bits == 8,  () => audioInfoWish.Is8Bit());
            AreEqual(bits == 16, () => audioInfoWish.Is16Bit());
            AreEqual(bits == 32, () => audioInfoWish.Is32Bit());
        }

        public static void Assert_Bit_Getters(this WavHeaderStruct wavHeader, int bits)
        {
            AreEqual(bits,       () => wavHeader.Bits());
            AreEqual(bits,       () => wavHeader.BitsPerValue);
            AreEqual(bits == 8,  () => wavHeader.Is8Bit());
            AreEqual(bits == 16, () => wavHeader.Is16Bit());
            AreEqual(bits == 32, () => wavHeader.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this SampleDataTypeEnum sampleDataTypeEnum, int bits)
        {
            AreEqual(bits,       () => sampleDataTypeEnum.Bits());
            AreEqual(bits,       () => sampleDataTypeEnum.EnumToBits());
            AreEqual(bits == 8,  () => sampleDataTypeEnum.Is8Bit());
            AreEqual(bits == 16, () => sampleDataTypeEnum.Is16Bit());
            AreEqual(bits == 32, () => sampleDataTypeEnum.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this SampleDataType sampleDataType, int bits)
        {
            if (sampleDataType == null) throw new NullException(() => sampleDataType);
            AreEqual(bits,       () => sampleDataType.Bits());
            AreEqual(bits,       () => sampleDataType.EntityToBits());
            AreEqual(bits == 8,  () => sampleDataType.Is8Bit());
            AreEqual(bits == 16, () => sampleDataType.Is16Bit());
            AreEqual(bits == 32, () => sampleDataType.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this Type type, int bits)
        {
            if (type == null) throw new NullException(() => type);
            AreEqual(bits,       () => type.Bits());
            AreEqual(bits,       () => type.TypeToBits());
            AreEqual(bits == 8,  () => type.Is8Bit());
            AreEqual(bits == 16, () => type.Is16Bit());
            AreEqual(bits == 32, () => type.Is32Bit());
        }
    } 
}