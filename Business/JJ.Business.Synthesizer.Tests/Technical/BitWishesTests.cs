using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.AttributeWishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.AttributeWishes.AttributeExtensionWishes;
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
                var x = new TestEntities(init);
                x.Assert_All_Bits(init);
            }

            // Synth-Bound Changes
            {
                AssertProp(x => AreEqual(x.SynthWishes,  () => x.SynthWishes.Bits(value)));
                AssertProp(x => AreEqual(x.SynthWishes,        x.SynthWishes.WithBits(value)));
                AssertProp(x => AreEqual(x.FlowNode,     () => x.FlowNode.Bits(value)));
                AssertProp(x => AreEqual(x.FlowNode,           x.FlowNode.WithBits(value)));
                AssertProp(x => AreEqual(x.ConfigWishes, () => x.ConfigWishes.Bits(value)));
                AssertProp(x => AreEqual(x.ConfigWishes,       x.ConfigWishes.WithBits(value)));
                
                AssertProp(x =>
                {
                    if (value == 8 ) AreEqual(x.SynthWishes, () => x.SynthWishes.With8Bit());
                    if (value == 16) AreEqual(x.SynthWishes, () => x.SynthWishes.With16Bit());
                    if (value == 32) AreEqual(x.SynthWishes, () => x.SynthWishes.With32Bit());
                });
                
                AssertProp(x =>
                {
                    if (value == 8 ) AreEqual(x.FlowNode, () => x.FlowNode.With8Bit());
                    if (value == 16) AreEqual(x.FlowNode, () => x.FlowNode.With16Bit());
                    if (value == 32) AreEqual(x.FlowNode, () => x.FlowNode.With32Bit());
                });
                
                AssertProp(x =>
                {
                    if (value == 8 ) AreEqual(x.ConfigWishes, () => x.ConfigWishes.With8Bit());
                    if (value == 16) AreEqual(x.ConfigWishes, () => x.ConfigWishes.With16Bit());
                    if (value == 32) AreEqual(x.ConfigWishes, () => x.ConfigWishes.With32Bit());
                });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = new TestEntities(init);
                    x.Assert_All_Bits(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Bits(value);
                    x.Assert_TapeBound_Bits(init);
                    x.Assert_BuffBound_Bits(init);
                    x.Assert_Independent_Bits(init);
                    x.Assert_Immutable_Bits(init);
                    
                    x.Record();
                    
                    x.Assert_All_Bits(value);
                }
            }

            // Tape-Bound Changes
            {
                AssertProp(x => AreEqual(x.Tape,        () => x.Tape.Bits(value)));
                AssertProp(x => AreEqual(x.TapeConfig,  () => x.TapeConfig.Bits(value)));
                AssertProp(x =>                               x.TapeConfig.Bits = value);
                AssertProp(x => AreEqual(x.TapeActions, () => x.TapeActions.Bits(value)));
                AssertProp(x => AreEqual(x.TapeAction,  () => x.TapeAction.Bits(value)));
                
                AssertProp(x =>
                {
                    if (value == 8) AreEqual(x.Tape, () => x.Tape.With8Bit());
                    if (value == 16) AreEqual(x.Tape, () => x.Tape.With16Bit());
                    if (value == 32) AreEqual(x.Tape, () => x.Tape.With32Bit());
                });
                
                AssertProp(x =>
                {
                    if (value == 8) AreEqual(x.TapeConfig, () => x.TapeConfig.With8Bit());
                    if (value == 16) AreEqual(x.TapeConfig, () => x.TapeConfig.With16Bit());
                    if (value == 32) AreEqual(x.TapeConfig, () => x.TapeConfig.With32Bit());
                });
                
                AssertProp(x =>
                {
                    if (value == 8) AreEqual(x.TapeActions, () => x.TapeActions.With8Bit());
                    if (value == 16) AreEqual(x.TapeActions, () => x.TapeActions.With16Bit());
                    if (value == 32) AreEqual(x.TapeActions, () => x.TapeActions.With32Bit());
                });
                
                AssertProp(x =>
                {
                    if (value == 8) AreEqual(x.TapeAction, () => x.TapeAction.With8Bit());
                    if (value == 16) AreEqual(x.TapeAction, () => x.TapeAction.With16Bit());
                    if (value == 32) AreEqual(x.TapeAction, () => x.TapeAction.With32Bit());
                });
                
                void AssertProp(Action<TestEntities> setter)
                {
                    var x = new TestEntities(init);
                    x.Assert_All_Bits(init);
                    
                    setter(x);
                    
                    x.Assert_SynthBound_Bits(init);
                    x.Assert_TapeBound_Bits(value);
                    x.Assert_BuffBound_Bits(init);
                    x.Assert_Independent_Bits(init);
                    x.Assert_Immutable_Bits(init);
                    
                    x.Record();
                    
                    x.Assert_All_Bits(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                AssertProp(x => AreEqual(x.Buff,            () => x.Buff.Bits(value, x.Context)));
                AssertProp(x => AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.Bits(value, x.Context)));
                
                AssertProp(x =>
                {
                    if (value == 8) AreEqual(x.Buff, () => x.Buff.With8Bit(x.Context));
                    if (value == 16) AreEqual(x.Buff, () => x.Buff.With16Bit(x.Context));
                    if (value == 32) AreEqual(x.Buff, () => x.Buff.With32Bit(x.Context));
                });
                
                AssertProp(x =>
                {
                    if (value == 8) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With8Bit(x.Context));
                    if (value == 16) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With16Bit(x.Context));
                    if (value == 32) AreEqual(x.AudioFileOutput, () => x.AudioFileOutput.With32Bit(x.Context));
                });

                void AssertProp(Action<TestEntities> setter)
                {    
                    var x = new TestEntities(init);
                    x.Assert_All_Bits(init);
                    
                    setter(x);

                    x.Assert_SynthBound_Bits(init);
                    x.Assert_TapeBound_Bits(init);
                    x.Assert_BuffBound_Bits(value);
                    x.Assert_Independent_Bits(init);
                    x.Assert_Immutable_Bits(init);
                    
                    x.Record();
                    x.Assert_All_Bits(init);
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
            var x = new TestEntities(init);

            // Sample
            {
                AssertProp(() => AreEqual(x.Sample, () => x.Sample.Bits(value, x.Context)));
                
                AssertProp(() =>
                {
                    if (value == 8) AreEqual(x.Sample, () => x.Sample.With8Bit(x.Context));
                    if (value == 16) AreEqual(x.Sample, () => x.Sample.With16Bit(x.Context));
                    if (value == 32) AreEqual(x.Sample, () => x.Sample.With32Bit(x.Context));
                });
                
                void AssertProp(Action setter)
                {
                    x.Initialize(init);
                    x.Assert_All_Bits(init);
                    
                    setter();
                    
                    x.Sample.Assert_Bit_Getters(value);
                    
                    x.AudioInfoWish.Assert_Bit_Getters(init);
                    x.AudioFileInfo.Assert_Bit_Getters(init);
                    x.Assert_Immutable_Bits(init);
                    x.Assert_Bound_Bits(init);

                    x.Record();
                    x.Assert_All_Bits(init);
                }
            }
            
            // AudioInfoWish
            {
                AssertProp(() => AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.Bits(value)));
                AssertProp(() =>                                 x.AudioInfoWish.Bits = value);
                
                AssertProp(() =>
                {
                    if (value ==  8) AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With8Bit());
                    if (value == 16) AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With16Bit());
                    if (value == 32) AreEqual(x.AudioInfoWish, () => x.AudioInfoWish.With32Bit());
                });
                
                void AssertProp(Action setter)
                {
                    x.Initialize(init);
                    x.Assert_All_Bits(init);
                    
                    setter();
                    
                    x.AudioInfoWish.Assert_Bit_Getters(value);
                    
                    x.AudioFileInfo.Assert_Bit_Getters(init);
                    x.Sample.Assert_Bit_Getters(init);
                    x.Assert_Immutable_Bits(init);
                    x.Assert_Bound_Bits(init);

                    x.Record();
                    x.Assert_All_Bits(init);
                }
            }
                        
            // AudioFileInfo
            {
                AssertProp(() => AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.Bits(value)));
                
                AssertProp(() =>
                {
                    if (value ==  8) AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With8Bit());
                    if (value == 16) AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With16Bit());
                    if (value == 32) AreEqual(x.AudioFileInfo, () => x.AudioFileInfo.With32Bit());
                });
                
                void AssertProp(Action setter)
                {
                    x.Initialize(init);
                    x.Assert_All_Bits(init);
                    
                    setter();
                    
                    x.AudioFileInfo.Assert_Bit_Getters(value);
                    
                    x.AudioInfoWish.Assert_Bit_Getters(init);
                    x.Sample.Assert_Bit_Getters(init);
                    x.Assert_Bound_Bits(init);
                    x.Assert_Immutable_Bits(init);

                    x.Record();
                    x.Assert_All_Bits(init);
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
            var x = new TestEntities(init);

            var wavHeaders = new List<WavHeaderStruct>();
            {
                AssertProp(() => x.WavHeader.Bits(value));
                
                AssertProp(() => 
                {
                    if (value == 8) return x.WavHeader.With8Bit();
                    if (value == 16) return x.WavHeader.With16Bit();
                    if (value == 32) return x.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<WavHeaderStruct> setter)
                {
                    x.WavHeader.Assert_Bit_Getters(init);
                    
                    var wavHeader2 = setter();
                    
                    x.WavHeader.Assert_Bit_Getters(init);
                    wavHeader2.Assert_Bit_Getters(value);
                    
                    wavHeaders.Add(wavHeader2);
                }
            }
            
            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                AssertProp(() => x.SampleDataTypeEnum.Bits(value));
                
                AssertProp(() => 
                {
                    if (value ==  8) return x.SampleDataTypeEnum.With8Bit();
                    if (value == 16) return x.SampleDataTypeEnum.With16Bit();
                    if (value == 32) return x.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<SampleDataTypeEnum> setter)
                {
                    x.SampleDataTypeEnum.Assert_Bit_Getters(init);
                    
                    var sampleDataTypeEnum2 = setter();
                    
                    x.SampleDataTypeEnum.Assert_Bit_Getters(init);
                    sampleDataTypeEnum2.Assert_Bit_Getters(value);
                    
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
            }
                        
            var sampleDataTypes = new List<SampleDataType>();
            {
                AssertProp(() => x.SampleDataType.Bits(value, x.Context));
                
                AssertProp(() => 
                {
                    if (value == 8) return x.SampleDataType.With8Bit(x.Context);
                    if (value == 16) return x.SampleDataType.With16Bit(x.Context);
                    if (value == 32) return x.SampleDataType.With32Bit(x.Context);
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<SampleDataType> setter)
                {
                    x.SampleDataType.Assert_Bit_Getters(init);

                    var sampleDataType2 = setter();
                    
                    x.SampleDataType.Assert_Bit_Getters(init);
                    sampleDataType2.Assert_Bit_Getters(value);
                    
                    sampleDataTypes.Add(sampleDataType2);
                }
            }
            
            var types = new List<Type>();
            {
                AssertProp(() => x.Type.Bits(value));
                
                AssertProp(() => 
                {
                    if (value == 8) return x.Type.With8Bit();
                    if (value == 16) return x.Type.With16Bit();
                    if (value == 32) return x.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void AssertProp(Func<Type> setter)
                {
                    x.Type.Assert_Bit_Getters(init);
                    
                    var type2 = setter();
                    
                    x.Type.Assert_Bit_Getters(init);
                    type2.Assert_Bit_Getters(value);
                    
                    types.Add(type2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            x.Assert_All_Bits(init);
            
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
            var configSection = GetConfigSectionAccessor();
            
            AreEqual(ConfigWishes.DefaultBits,       () => configSection.Bits);
            AreEqual(ConfigWishes.DefaultBits,       () => configSection.Bits());
            AreEqual(ConfigWishes.DefaultBits == 8,  () => configSection.Is8Bit());
            AreEqual(ConfigWishes.DefaultBits == 16, () => configSection.Is16Bit());
            AreEqual(ConfigWishes.DefaultBits == 32, () => configSection.Is32Bit());
        }
                
        // Bits Conversion-Style
        
        [TestMethod] public void Test_Bits_ConversionStyle()
        {
            foreach (int bits in new[] { 8, 16, 32 })
            {
                var x = new TestEntities(bits);
                
                // Getters
                AreEqual(x.SampleDataTypeEnum, () => bits.BitsToEnum());
                AreEqual(x.SampleDataType,     () => bits.BitsToEntity(x.Context));
                AreEqual(x.Type,               () => bits.BitsToType());
            
                // Setters
                AreEqual(bits, () => x.SampleDataTypeEnum.EnumToBits());
                AreEqual(bits, () => x.SampleDataType    .EntityToBits());
                AreEqual(bits, () => x.Type              .TypeToBits());
            }
            
            // For test coverage
            ThrowsException(() => default(Type).TypeToBits());
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
        
        // Old
 
        /// <inheritdoc cref="docs._testattributewishesold"/>
        [TestMethod]
        public void ChannelCountToSpeakerSetup_Test()
        {
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
        }

        // Helpers

        private ConfigSectionAccessor GetConfigSectionAccessor() => new ConfigWishesAccessor(new SynthWishes().Config)._section;

        private class TestEntities
        {
            // SynthWishes-Bound
            public SynthWishes           SynthWishes        { get; private set; }
            public IContext              Context            { get; private set; }
            public FlowNode              FlowNode           { get; private set; }
            public ConfigWishes          ConfigWishes       { get; private set; }

            // Tape-Bound
            public Tape                  Tape               { get; private set; }
            public TapeConfig            TapeConfig         { get; private set; }
            public TapeActions           TapeActions        { get; private set; }
            public TapeAction            TapeAction         { get; private set; }

            // Buff-Bound
            public Buff                  Buff               { get; private set; }
            public AudioFileOutput       AudioFileOutput    { get; private set; }

            // Independent after Taping
            public Sample                Sample             { get; private set; }
            public AudioInfoWish         AudioInfoWish      { get; private set; }
            public AudioFileInfo         AudioFileInfo      { get; private set; }
            
            // Immutable
            public WavHeaderStruct       WavHeader          { get; private set; }
            public SampleDataTypeEnum    SampleDataTypeEnum { get; private set; }
            public SampleDataType        SampleDataType     { get; private set; }
            public Type                  Type               { get; private set; }

            public TestEntities(int bits) => Initialize(bits);

            private TestEntities(Action<SynthWishes> initialize = null) => Initialize(initialize);
            
            public void Initialize(int bits)
            {
                Initialize(x => x.WithBits(bits));
            }
            
            public void Initialize(Action<SynthWishes> initialize)
            {
                // SynthWishes-Bound
                SynthWishes  = new SynthWishes();
                Context      = SynthWishes.Context;
                ConfigWishes = SynthWishes.Config;
                FlowNode     = SynthWishes.Sine();
                
                // Initialize
                SynthWishes.WithSamplingRate(100);
                initialize?.Invoke(SynthWishes);
                
                Record();
            }
            
            public void Record()
            {
                // Record
                Tape = null;
                SynthWishes.RunOnThis(() => FlowNode.AfterRecord(x => Tape = x));
                IsNotNull(() => Tape);
                
                // Tape-Bound
                TapeConfig  = Tape.Config;
                TapeActions = Tape.Actions;
                TapeAction  = Tape.Actions.AfterRecord;
                
                // Buff-Bound
                Buff            = Tape.Buff;
                AudioFileOutput = Tape.UnderlyingAudioFileOutput;
                
                // Independent after Taping
                Sample        = Tape.UnderlyingSample;
                AudioInfoWish = Sample.ToWish();
                AudioFileInfo = AudioInfoWish.FromWish();
                
                // Immutable
                WavHeader          = Sample.ToWavHeader();
                SampleDataTypeEnum = Sample.GetSampleDataTypeEnum();
                SampleDataType     = Sample.SampleDataType;

                int bits = SynthWishes.GetBits;
                switch (bits)
                {
                    case 8:  Type = typeof(byte);  break;
                    case 16: Type = typeof(short); break;
                    case 32: Type = typeof(float); break;
                    default: throw new Exception($"{new { bits }} not supported.");
                }
            }

            public void Assert_All_Bits(int bits)
            {
                Assert_SynthBound_Bits(bits);
                Assert_TapeBound_Bits(bits);
                Assert_BuffBound_Bits(bits);
                Assert_Independent_Bits(bits);
                Assert_Immutable_Bits(bits);
            }

            public void Assert_Bound_Bits(int bits)
            {
                Assert_SynthBound_Bits(bits);
                Assert_TapeBound_Bits(bits);
                Assert_BuffBound_Bits(bits);
            }

            public void Assert_SynthBound_Bits(int bits)
            {
                AreEqual(bits, () => SynthWishes.Bits());
                AreEqual(bits, () => SynthWishes.GetBits);
                AreEqual(bits, () => FlowNode.Bits());
                AreEqual(bits, () => FlowNode.GetBits);
                AreEqual(bits, () => ConfigWishes.Bits());
                AreEqual(bits, () => ConfigWishes.GetBits);
                
                AreEqual(bits == 8, () => SynthWishes.Is8Bit());
                AreEqual(bits == 8, () => SynthWishes.Is8Bit);
                AreEqual(bits == 8, () => FlowNode.Is8Bit());
                AreEqual(bits == 8, () => FlowNode.Is8Bit);
                AreEqual(bits == 8, () => ConfigWishes.Is8Bit());
                AreEqual(bits == 8, () => ConfigWishes.Is8Bit);
                
                AreEqual(bits == 16, () => SynthWishes.Is16Bit());
                AreEqual(bits == 16, () => SynthWishes.Is16Bit);
                AreEqual(bits == 16, () => FlowNode.Is16Bit());
                AreEqual(bits == 16, () => FlowNode.Is16Bit);
                AreEqual(bits == 16, () => ConfigWishes.Is16Bit());
                AreEqual(bits == 16, () => ConfigWishes.Is16Bit);
                
                AreEqual(bits == 32, () => SynthWishes.Is32Bit());
                AreEqual(bits == 32, () => SynthWishes.Is32Bit);
                AreEqual(bits == 32, () => FlowNode.Is32Bit());
                AreEqual(bits == 32, () => FlowNode.Is32Bit);
                AreEqual(bits == 32, () => ConfigWishes.Is32Bit());
                AreEqual(bits == 32, () => ConfigWishes.Is32Bit);
            }
            
            public void Assert_TapeBound_Bits(int bits)
            {
                AreEqual(bits, () => Tape.Bits());
                AreEqual(bits, () => TapeConfig.Bits());
                AreEqual(bits, () => TapeConfig.Bits);
                AreEqual(bits, () => TapeActions.Bits());
                AreEqual(bits, () => TapeAction.Bits());
                
                AreEqual(bits == 8, () => Tape.Is8Bit());
                AreEqual(bits == 8, () => TapeConfig.Is8Bit());
                AreEqual(bits == 8, () => TapeActions.Is8Bit());
                AreEqual(bits == 8, () => TapeAction.Is8Bit());
            
                AreEqual(bits == 16, () => Tape.Is16Bit());
                AreEqual(bits == 16, () => TapeConfig.Is16Bit());
                AreEqual(bits == 16, () => TapeActions.Is16Bit());
                AreEqual(bits == 16, () => TapeAction.Is16Bit());
            
                AreEqual(bits == 32, () => Tape.Is32Bit());
                AreEqual(bits == 32, () => TapeConfig.Is32Bit());
                AreEqual(bits == 32, () => TapeActions.Is32Bit());
                AreEqual(bits == 32, () => TapeAction.Is32Bit());
            }
            
            public void Assert_BuffBound_Bits(int bits)
            {
                AreEqual(bits, () => Buff.Bits());
                AreEqual(bits, () => AudioFileOutput.Bits());
                
                AreEqual(bits == 8, () => Buff.Is8Bit());
                AreEqual(bits == 8, () => AudioFileOutput.Is8Bit());
                
                AreEqual(bits == 16, () => Buff.Is16Bit());
                AreEqual(bits == 16, () => AudioFileOutput.Is16Bit());
                
                AreEqual(bits == 32, () => Buff.Is32Bit());
                AreEqual(bits == 32, () => AudioFileOutput.Is32Bit());
            }

            public void Assert_Independent_Bits(int bits)
            {
                // Independent after Taping
                Sample.Assert_Bit_Getters(bits);
                AudioInfoWish.Assert_Bit_Getters(bits);
                AudioFileInfo.Assert_Bit_Getters(bits);
            }

            public void Assert_Immutable_Bits(int bits)
            {
                WavHeader.Assert_Bit_Getters(bits);
                SampleDataTypeEnum.Assert_Bit_Getters(bits);
                SampleDataType.Assert_Bit_Getters(bits);
                Type.Assert_Bit_Getters(bits);
            }
        }
    }
    
    internal static class AttributeWishesTestExtensions
    {
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
            AreEqual(bits == 8,  () => sampleDataTypeEnum.Is8Bit());
            AreEqual(bits == 16, () => sampleDataTypeEnum.Is16Bit());
            AreEqual(bits == 32, () => sampleDataTypeEnum.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this SampleDataType sampleDataType, int bits)
        {
            if (sampleDataType == null) throw new NullException(() => sampleDataType);
            AreEqual(bits,       () => sampleDataType.Bits());
            AreEqual(bits == 8,  () => sampleDataType.Is8Bit());
            AreEqual(bits == 16, () => sampleDataType.Is16Bit());
            AreEqual(bits == 32, () => sampleDataType.Is32Bit());
        }
        
        public static void Assert_Bit_Getters(this Type type, int bits)
        {
            if (type == null) throw new NullException(() => type);
            AreEqual(bits,       () => type.Bits());
            AreEqual(bits == 8,  () => type.Is8Bit());
            AreEqual(bits == 16, () => type.Is16Bit());
            AreEqual(bits == 32, () => type.Is32Bit());
        }
    } 
}