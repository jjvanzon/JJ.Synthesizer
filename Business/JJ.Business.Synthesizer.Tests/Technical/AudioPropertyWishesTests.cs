using System;
using System.Collections.Generic;
using JJ.Business.Synthesizer.Enums;
using JJ.Business.Synthesizer.Extensions;
using JJ.Business.Synthesizer.Infos;
using JJ.Business.Synthesizer.Structs;
using JJ.Business.Synthesizer.Tests.Accessors;
using JJ.Business.Synthesizer.Wishes;
using JJ.Business.Synthesizer.Wishes.TapeWishes;
using JJ.Framework.Persistence;
using JJ.Framework.Reflection;
using JJ.Persistence.Synthesizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static JJ.Business.Synthesizer.Wishes.AudioPropertyWishes;
using static JJ.Framework.Testing.AssertHelper;
#pragma warning disable CS0618 // Type or member is obsolete

namespace JJ.Business.Synthesizer.Tests.Technical
{
    [TestClass]
    [TestCategory("Technical")]
    public class AudioPropertyWishesTests
    {
        // Bits

        [TestMethod] public void Bits_Normal()
        {
            Bits_Normal(32, 8);
            Bits_Normal(32, 16);
            Bits_Normal(16, 32);
        }
        
        void Bits_Normal(int init, int value)
        {
            // Check Before Change
            { 
                var x = new TestEntities(init);
                x.All_Bits_Equal(init);
            }

            // Synth-Bound Changes
            {
                TestSetter(x => x.SynthWishes.Bits(value));
                TestSetter(x => x.FlowNode.Bits(value));
                TestSetter(x => x.ConfigWishes.Bits(value));
                TestSetter(x =>
                {
                    if (value == 8) x.SynthWishes.With8Bit();
                    if (value == 16) x.SynthWishes.With16Bit();
                    if (value == 32) x.SynthWishes.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.FlowNode.With8Bit();
                    if (value == 16) x.FlowNode.With16Bit();
                    if (value == 32) x.FlowNode.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.ConfigWishes.With8Bit();
                    if (value == 16) x.ConfigWishes.With16Bit();
                    if (value == 32) x.ConfigWishes.With32Bit();
                });
                
                void TestSetter(Action<TestEntities> setter)
                {
                    var x = new TestEntities(init);
                    x.All_Bits_Equal(init);
                    
                    setter(x);
                    
                    x.SynthBound_Bits_Equal(value);
                    x.TapeBound_Bits_Equal(init);
                    x.BuffBound_Bits_Equal(init);
                    x.Independent_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);
                    
                    x.Record();
                    
                    x.All_Bits_Equal(value);
                }
            }

            // Tape-Bound Changes
            {
                TestSetter(x => x.Tape.Bits(value));
                TestSetter(x => x.TapeConfig.Bits(value));
                TestSetter(x => x.TapeActions.Bits(value));
                TestSetter(x => x.TapeAction.Bits(value));
                TestSetter(x =>
                {
                    if (value == 8) x.Tape.With8Bit();
                    if (value == 16) x.Tape.With16Bit();
                    if (value == 32) x.Tape.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.TapeConfig.With8Bit();
                    if (value == 16) x.TapeConfig.With16Bit();
                    if (value == 32) x.TapeConfig.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.TapeActions.With8Bit();
                    if (value == 16) x.TapeActions.With16Bit();
                    if (value == 32) x.TapeActions.With32Bit();
                });
                TestSetter(x =>
                {
                    if (value == 8) x.TapeAction.With8Bit();
                    if (value == 16) x.TapeAction.With16Bit();
                    if (value == 32) x.TapeAction.With32Bit();
                });
                
                void TestSetter(Action<TestEntities> setter)
                {
                    var x = new TestEntities(init);
                    x.All_Bits_Equal(init);
                    
                    setter(x);
                    
                    x.SynthBound_Bits_Equal(init);
                    x.TapeBound_Bits_Equal(value);
                    x.BuffBound_Bits_Equal(init);
                    x.Independent_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);
                    
                    x.Record();
                    
                    x.All_Bits_Equal(init); // By Design: Currently you can't record over the same tape. So you always get a new tape, resetting the values.
                }
            }

            // Buff-Bound Changes
            {
                TestSetter(x => x.Buff.Bits(value, x.Context));
                TestSetter(x => x.AudioFileOutput.Bits(value, x.Context));
                TestSetter(x =>
                {
                    if (value == 8) x.Buff.With8Bit(x.Context);
                    if (value == 16) x.Buff.With16Bit(x.Context);
                    if (value == 32) x.Buff.With32Bit(x.Context);
                });
                TestSetter(x =>
                {
                    if (value == 8) x.AudioFileOutput.With8Bit(x.Context);
                    if (value == 16) x.AudioFileOutput.With16Bit(x.Context);
                    if (value == 32) x.AudioFileOutput.With32Bit(x.Context);
                });

                void TestSetter(Action<TestEntities> setter)
                {    
                    var x = new TestEntities(init);
                    x.All_Bits_Equal(init);
                    
                    setter(x);

                    x.SynthBound_Bits_Equal(init);
                    x.TapeBound_Bits_Equal(init);
                    x.BuffBound_Bits_Equal(value);
                    x.Independent_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);
                    
                    x.Record();
                    x.All_Bits_Equal(init);
                }
            }
        }

        // Bits With Type Arguments
        
        [TestMethod] public void Bits_WithTypeArguments()
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
        
        // Bits Conversion-Style
                
        [TestMethod] public void Bits_ConversionStyle()
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
        
        // Bits for Independently Changeable
        
        [TestMethod] public void Bits_Independents()
        {
            Bits_Independents(init: 32, value: 8);
            Bits_Independents(init: 32, value: 16);
            Bits_Independents(init: 16, value: 32);
        }
        
        void Bits_Independents(int init, int value)
        {
            // Independent after Taping
            var x = new TestEntities(init);

            // Sample
            {
                TestSetter(() => x.Sample.Bits(value, x.Context));
                TestSetter(() =>
                {
                    if (value == 8) x.Sample.With8Bit(x.Context);
                    if (value == 16) x.Sample.With16Bit(x.Context);
                    if (value == 32) x.Sample.With32Bit(x.Context);
                });
                
                void TestSetter(Action setter)
                {
                    x.Initialize(init);
                    x.All_Bits_Equal(init);
                    
                    setter();
                    
                    x.Sample.Assert_Bits(value);
                    
                    x.AudioInfoWish.Assert_Bits(init);
                    x.AudioFileInfo.Assert_Bits(init);
                    x.Immutable_Bits_Equal(init);
                    x.Bound_Bits_Equal(init);

                    x.Record();
                    x.All_Bits_Equal(init);
                }
            }
            
            // AudioInfoWish
            {
                TestSetter(() => x.AudioInfoWish.Bits(value));
                TestSetter(() =>
                {
                    if (value == 8) x.AudioInfoWish.With8Bit();
                    if (value == 16) x.AudioInfoWish.With16Bit();
                    if (value == 32) x.AudioInfoWish.With32Bit();
                });
                
                void TestSetter(Action setter)
                {
                    x.Initialize(init);
                    x.All_Bits_Equal(init);
                    
                    setter();
                    
                    x.AudioInfoWish.Assert_Bits(value);
                    
                    x.AudioFileInfo.Assert_Bits(init);
                    x.Sample.Assert_Bits(init);
                    x.Immutable_Bits_Equal(init);
                    x.Bound_Bits_Equal(init);

                    x.Record();
                    x.All_Bits_Equal(init);
                }
            }
                        
            // AudioFileInfo
            {
                TestSetter(() => x.AudioFileInfo.Bits(value));
                TestSetter(() =>
                {
                    if (value == 8) x.AudioFileInfo.With8Bit();
                    if (value == 16) x.AudioFileInfo.With16Bit();
                    if (value == 32) x.AudioFileInfo.With32Bit();
                });
                
                void TestSetter(Action setter)
                {
                    x.Initialize(init);
                    x.All_Bits_Equal(init);
                    
                    setter();
                    
                    x.AudioFileInfo.Assert_Bits(value);
                    
                    x.AudioInfoWish.Assert_Bits(init);
                    x.Sample.Assert_Bits(init);
                    x.Bound_Bits_Equal(init);
                    x.Immutable_Bits_Equal(init);

                    x.Record();
                    x.All_Bits_Equal(init);
                }
            }
        }
        
        // Bits for Immutables

        [TestMethod] public void Bits_Immutables()
        {
            Bits_Immutables(init: 32, value: 8);
            Bits_Immutables(init: 32, value: 16);
            Bits_Immutables(init: 16, value: 32);
        }
        
        void Bits_Immutables(int init, int value)
        {
            var x = new TestEntities(init);

            var wavHeaders = new List<WavHeaderStruct>();
            {
                TestSetter(() => x.WavHeader.Bits(value));
                TestSetter(() => 
                {
                    if (value == 8) return x.WavHeader.With8Bit();
                    if (value == 16) return x.WavHeader.With16Bit();
                    if (value == 32) return x.WavHeader.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void TestSetter(Func<WavHeaderStruct> setter)
                {
                    x.WavHeader.Assert_Bits(init);
                    var wavHeader2 = setter();
                    x.WavHeader.Assert_Bits(init);
                    wavHeader2.Assert_Bits(value);
                    wavHeaders.Add(wavHeader2);
                }
            }
            
            var sampleDataTypeEnums = new List<SampleDataTypeEnum>();
            {
                TestSetter(() => x.SampleDataTypeEnum.Bits(value));
                TestSetter(() => 
                {
                    if (value == 8) return x.SampleDataTypeEnum.With8Bit();
                    if (value == 16) return x.SampleDataTypeEnum.With16Bit();
                    if (value == 32) return x.SampleDataTypeEnum.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void TestSetter(Func<SampleDataTypeEnum> setter)
                {
                    x.SampleDataTypeEnum.Assert_Bits(init);
                    var sampleDataTypeEnum2 = setter();
                    x.SampleDataTypeEnum.Assert_Bits(init);
                    sampleDataTypeEnum2.Assert_Bits(value);
                    sampleDataTypeEnums.Add(sampleDataTypeEnum2);
                }
            }
                        
            var sampleDataTypes = new List<SampleDataType>();
            {
                TestSetter(() => x.SampleDataType.Bits(value, x.Context));
                TestSetter(() => 
                {
                    if (value == 8) return x.SampleDataType.With8Bit(x.Context);
                    if (value == 16) return x.SampleDataType.With16Bit(x.Context);
                    if (value == 32) return x.SampleDataType.With32Bit(x.Context);
                    return default; // ncrunch: no coverage
                });
                
                void TestSetter(Func<SampleDataType> setter)
                {
                    x.SampleDataType.Assert_Bits(init);
                    var sampleDataType2 = setter();
                    x.SampleDataType.Assert_Bits(init);
                    sampleDataType2.Assert_Bits(value);
                    sampleDataTypes.Add(sampleDataType2);
                }
            }
                                    
            var types = new List<Type>();
            {
                TestSetter(() => x.Type.Bits(value));
                TestSetter(() => 
                {
                    if (value == 8) return x.Type.With8Bit();
                    if (value == 16) return x.Type.With16Bit();
                    if (value == 32) return x.Type.With32Bit();
                    return default; // ncrunch: no coverage
                });
                
                void TestSetter(Func<Type> setter)
                {
                    x.Type.Assert_Bits(init);
                    var type2 = setter();
                    x.Type.Assert_Bits(init);
                    type2.Assert_Bits(value);
                    types.Add(type2);
                }
            }
            
            // After-Record
            x.Record();
            
            // All is reset
            x.All_Bits_Equal(init);
            
            // Except for our variables
            wavHeaders         .ForEach(w => w.Assert_Bits(value));
            sampleDataTypeEnums.ForEach(e => e.Assert_Bits(value));
            sampleDataTypes    .ForEach(s => s.Assert_Bits(value));
            types              .ForEach(t => t.Assert_Bits(value));
        }

        // Helpers

        private class TestEntities
        {
            // Global-Bound
            public ConfigSectionAccessor ConfigSection      { get; private set; }

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
                
                // Global-Bound
                ConfigSection = new ConfigWishesAccessor(ConfigWishes)._section;
                
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
                    
            public void GlobalBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => ConfigSection.Bits());
            }

            public void All_Bits_Equal(int bits)
            {
                //GlobalBound_Bits_Equal(bits); // Skip because stays the same.
                SynthBound_Bits_Equal(bits);
                TapeBound_Bits_Equal(bits);
                BuffBound_Bits_Equal(bits);
                Independent_Bits_Equal(bits);
                Immutable_Bits_Equal(bits);
            }

            public void Bound_Bits_Equal(int bits)
            {
                SynthBound_Bits_Equal(bits);
                TapeBound_Bits_Equal(bits);
                BuffBound_Bits_Equal(bits);
            }

            public void SynthBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => SynthWishes.Bits());
                AreEqual(bits, () => FlowNode.Bits());
                AreEqual(bits, () => ConfigWishes.Bits());
                
                AreEqual(bits == 8, () => SynthWishes.Is8Bit());
                AreEqual(bits == 8, () => FlowNode.Is8Bit());
                AreEqual(bits == 8, () => ConfigWishes.Is8Bit());
                
                AreEqual(bits == 16, () => SynthWishes.Is16Bit());
                AreEqual(bits == 16, () => FlowNode.Is16Bit());
                AreEqual(bits == 16, () => ConfigWishes.Is16Bit());
                
                AreEqual(bits == 32, () => SynthWishes.Is32Bit());
                AreEqual(bits == 32, () => FlowNode.Is32Bit());
                AreEqual(bits == 32, () => ConfigWishes.Is32Bit());
            }
            
            public void TapeBound_Bits_Equal(int bits)
            {
                AreEqual(bits, () => Tape.Bits());
                AreEqual(bits, () => TapeConfig.Bits());
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
            
            public void BuffBound_Bits_Equal(int bits)
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

            public void Independent_Bits_Equal(int bits)
            {
                // Independent after Taping
                Sample.Assert_Bits(bits);
                AudioInfoWish.Assert_Bits(bits);
                AudioFileInfo.Assert_Bits(bits);
            }

            public void Immutable_Bits_Equal(int bits)
            {
                WavHeader.Assert_Bits(bits);
                SampleDataTypeEnum.Assert_Bits(bits);
                SampleDataType.Assert_Bits(bits);
                Type.Assert_Bits(bits);
            }
        }
    
        // Old
 
        /// <inheritdoc cref="docs._testaudiopropertywishesold"/>
        [TestMethod]
        public void ChannelCountToSpeakerSetup_Test()
        {
            AreEqual(SpeakerSetupEnum.Mono,   () => 1.ChannelsToEnum());
            AreEqual(SpeakerSetupEnum.Stereo, () => 2.ChannelsToEnum());
        }
    }
    
    internal static class AudioPropertyWishesTestExtensions
    {
        public static void Assert_Bits(this AudioFileInfo audioFileInfo, int bits)
        {
            AreEqual(bits,       () => audioFileInfo.Bits());
            AreEqual(bits == 8,  () => audioFileInfo.Is8Bit());
            AreEqual(bits == 16, () => audioFileInfo.Is16Bit());
            AreEqual(bits == 32, () => audioFileInfo.Is32Bit());
        }
        
        public static void Assert_Bits(this Sample sample, int bits)
        {
            AreEqual(bits, () => sample.Bits());
            AreEqual(bits == 8, () => sample.Is8Bit());
            AreEqual(bits == 16, () => sample.Is16Bit());
            AreEqual(bits == 32, () => sample.Is32Bit());
        }
        
        public static void Assert_Bits(this AudioInfoWish audioInfoWish, int bits)
        {
            AreEqual(bits, () => audioInfoWish.Bits());
            AreEqual(bits == 8, () => audioInfoWish.Is8Bit());
            AreEqual(bits == 16, () => audioInfoWish.Is16Bit());
            AreEqual(bits == 32, () => audioInfoWish.Is32Bit());
        }

        public static void Assert_Bits(this WavHeaderStruct wavHeader, int bits)
        {
            AreEqual(bits,       () => wavHeader.Bits());
            AreEqual(bits == 8,  () => wavHeader.Is8Bit());
            AreEqual(bits == 16, () => wavHeader.Is16Bit());
            AreEqual(bits == 32, () => wavHeader.Is32Bit());
        }
        
        public static void Assert_Bits(this SampleDataTypeEnum sampleDataTypeEnum, int bits)
        {
            AreEqual(bits,       () => sampleDataTypeEnum.Bits());
            AreEqual(bits == 8,  () => sampleDataTypeEnum.Is8Bit());
            AreEqual(bits == 16, () => sampleDataTypeEnum.Is16Bit());
            AreEqual(bits == 32, () => sampleDataTypeEnum.Is32Bit());
        }
        
        public static void Assert_Bits(this SampleDataType sampleDataType, int bits)
        {
            if (sampleDataType == null) throw new NullException(() => sampleDataType);
            AreEqual(bits,       () => sampleDataType.Bits());
            AreEqual(bits == 8,  () => sampleDataType.Is8Bit());
            AreEqual(bits == 16, () => sampleDataType.Is16Bit());
            AreEqual(bits == 32, () => sampleDataType.Is32Bit());
        }
        
        public static void Assert_Bits(this Type type, int bits)
        {
            if (type == null) throw new NullException(() => type);
            AreEqual(bits,       () => type.Bits());
            AreEqual(bits == 8,  () => type.Is8Bit());
            AreEqual(bits == 16, () => type.Is16Bit());
            AreEqual(bits == 32, () => type.Is32Bit());
        }
    } 
}