using System;
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
        
        // Bits for Independently Changeable or Immutable
        
        [TestMethod] public void Bits_IndependentsAndImmutables()
        {
            Bits_IndependentsAndImmutables(init: 32, value: 8);
            Bits_IndependentsAndImmutables(init: 32, value: 16);
            Bits_IndependentsAndImmutables(init: 16, value: 32);
        }
        void Bits_IndependentsAndImmutables(int init, int value)
        {
            var x = new TestEntities(init);
            
            // Test Mutations

            // Independent after Taping
            AreEqual(init, () => x.Sample.Bits());
            x.Sample.Bits(value, x.Context);
            AreEqual(value, () => x.Sample.Bits());
            
            AreEqual(init, () => x.AudioInfoWish.Bits());
            x.AudioInfoWish.Bits(value);
            AreEqual(value, () => x.AudioInfoWish.Bits());
            
            AreEqual(init, () => x.AudioFileInfo.Bits());
            x.AudioFileInfo.Bits(value);
            AreEqual(value, () => x.AudioFileInfo.Bits());

            // Immutable                        
            AreEqual(init, () => x.WavHeader.Bits());
            var wavHeaderAfter = x.WavHeader.Bits(value);
            AreEqual(init, () => x.WavHeader.Bits());
            AreEqual(value,   () => wavHeaderAfter.Bits());
            
            AreEqual(init, () => x.SampleDataTypeEnum.Bits());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.Bits(value);
            AreEqual(init, () => x.SampleDataTypeEnum.Bits());
            AreEqual(value,   () => sampleDataTypeEnumAfter.Bits());
            
            AreEqual(init, () => x.SampleDataType.Bits());
            var sampleDataTypeAfter = x.SampleDataType.Bits(value, x.Context);
            AreEqual(init, () => x.SampleDataType.Bits());
            AreEqual(value,   () => sampleDataTypeAfter.Bits());
            
            AreEqual(init, () => x.Type.Bits());
            var typeAfter = x.Type.Bits(value);
            AreEqual(init, () => x.Type.Bits());
            AreEqual(value,   () => typeAfter.Bits());
            
            // Test After-Record
            x.Record();

            // All is reset
            x.All_Bits_Equal(init);
        
            // Except for our variables
            AreEqual(value, () => wavHeaderAfter.Bits());
            AreEqual(value, () => sampleDataTypeEnumAfter.Bits());
            AreEqual(value, () => sampleDataTypeAfter.Bits());
            AreEqual(value, () => typeAfter.Bits());
        }


        [TestMethod] public void Bits_IndependentsAndImmutables_8Bit_Shorthand()
        {
            var init = 32;
            var value = 8;
            var x = new TestEntities(init);
            
            // Test Mutations

            // Independent after Taping
            x.Sample.Assert_Bits(init);
            x.Sample.With8Bit(x.Context);
            x.Sample.Assert_Bits(value);
            
            x.AudioInfoWish.Assert_Bits(init);
            x.AudioInfoWish.With8Bit();
            x.AudioInfoWish.Assert_Bits(value);
                        
            x.AudioFileInfo.Assert_Bits(init);
            x.AudioFileInfo.With8Bit();
            x.AudioFileInfo.Assert_Bits(value);

            // Immutable                        
            x.WavHeader.Assert_Bits(init);
            var wavHeaderAfter = x.WavHeader.With8Bit();
            x.WavHeader.Assert_Bits(init);
            wavHeaderAfter.Assert_Bits(value);
            
            x.SampleDataTypeEnum.Assert_Bits(init);
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With8Bit();
            x.SampleDataTypeEnum.Assert_Bits(init);
            sampleDataTypeEnumAfter.Assert_Bits(value);

            x.SampleDataType.Assert_Bits(init);
            var sampleDataTypeAfter = x.SampleDataType.With8Bit(x.Context);
            x.SampleDataType.Assert_Bits(init);
            sampleDataTypeAfter.Assert_Bits(value);

            x.Type.Assert_Bits(init);
            var typeAfter = x.Type.With8Bit();
            x.Type.Assert_Bits(init);
            typeAfter.Assert_Bits(value);
        
            // Test After Record
            x.Record();
            
            // All is reset
            x.All_Bits_Equal(init);
        
            // Except for our variables
            wavHeaderAfter.Assert_Bits(value);
            sampleDataTypeEnumAfter.Assert_Bits(value);
            sampleDataTypeAfter.Assert_Bits(value);
            typeAfter.Assert_Bits(value);
        }
                
        [TestMethod] public void Bits_IndependentsAndImmutables_16Bit_Shorthand()
        {
            var init = 32;
            var x = new TestEntities(init);
            
            // Test Mutations

            // Independent after Taping
            IsFalse(() => x.Sample.Is16Bit());
            x.Sample.With16Bit(x.Context);
            IsTrue(() => x.Sample.Is16Bit());
            
            IsFalse(() => x.AudioInfoWish.Is16Bit());
            x.AudioInfoWish.With16Bit();
            IsTrue(() => x.AudioInfoWish.Is16Bit());
                                    
            IsFalse(() => x.AudioFileInfo.Is16Bit());
            x.AudioFileInfo.With16Bit();
            IsTrue(() => x.AudioFileInfo.Is16Bit());

            // Immutable                        
            IsFalse(() => x.WavHeader.Is16Bit());
            var wavHeaderAfter = x.WavHeader.With16Bit();
            IsFalse(() => x.WavHeader.Is16Bit());
            IsTrue(() => wavHeaderAfter.Is16Bit());
            
            IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With16Bit();
            IsFalse(() => x.SampleDataTypeEnum.Is16Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is16Bit());

            IsFalse(() => x.SampleDataType.Is16Bit());
            var sampleDataTypeAfter = x.SampleDataType.With16Bit(x.Context);
            IsFalse(() => x.SampleDataType.Is16Bit());
            IsTrue(() => sampleDataTypeAfter.Is16Bit());

            IsFalse(() => x.Type.Is16Bit());
            var typeAfter = x.Type.With16Bit();
            IsFalse(() => x.Type.Is16Bit());
            IsTrue(() => typeAfter.Is16Bit());
        
            // Test After Record
            x.Record();

            // All is reset
            x.All_Bits_Equal(init);
            
            // Except for our variables
            IsTrue(() => wavHeaderAfter.Is16Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is16Bit());
            IsTrue(() => sampleDataTypeAfter.Is16Bit());
            IsTrue(() => typeAfter.Is16Bit());
        }
                
        [TestMethod] public void Bits_IndependentsAndImmutables_32Bit_Shorthand()
        {
            int init = 16;
            var x = new TestEntities(init);
            
            // Test Mutations

            // Independent after Taping
            IsFalse(() => x.Sample.Is32Bit());
            x.Sample.With32Bit(x.Context);
            IsTrue(() => x.Sample.Is32Bit());
            
            IsFalse(() => x.AudioInfoWish.Is32Bit());
            x.AudioInfoWish.With32Bit();
            IsTrue(() => x.AudioInfoWish.Is32Bit());
                                    
            IsFalse(() => x.AudioFileInfo.Is32Bit());
            x.AudioFileInfo.With32Bit();
            IsTrue(() => x.AudioFileInfo.Is32Bit());

            // Immutable                        
            IsFalse(() => x.WavHeader.Is32Bit());
            var wavHeaderAfter = x.WavHeader.With32Bit();
            IsFalse(() => x.WavHeader.Is32Bit());
            IsTrue(() => wavHeaderAfter.Is32Bit());
            
            IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
            var sampleDataTypeEnumAfter = x.SampleDataTypeEnum.With32Bit();
            IsFalse(() => x.SampleDataTypeEnum.Is32Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is32Bit());

            IsFalse(() => x.SampleDataType.Is32Bit());
            var sampleDataTypeAfter = x.SampleDataType.With32Bit(x.Context);
            IsFalse(() => x.SampleDataType.Is32Bit());
            IsTrue(() => sampleDataTypeAfter.Is32Bit());

            IsFalse(() => x.Type.Is32Bit());
            var typeAfter = x.Type.With32Bit();
            IsFalse(() => x.Type.Is32Bit());
            IsTrue(() => typeAfter.Is32Bit());
        
            // Test After Record
            x.Record();

            // All is reset
            x.All_Bits_Equal(init);
        
            // Except for our variables
            IsTrue(() => wavHeaderAfter.Is32Bit());
            IsTrue(() => sampleDataTypeEnumAfter.Is32Bit());
            IsTrue(() => sampleDataTypeAfter.Is32Bit());
            IsTrue(() => typeAfter.Is32Bit());
        }

        // Helpers

        private class TestEntities
        {
            // Global-Bound
            public ConfigSectionAccessor ConfigSection      { get; }

            // SynthWishes-Bound
            public SynthWishes           SynthWishes        { get; }
            public IContext              Context            { get; }
            public FlowNode              FlowNode           { get; }
            public ConfigWishes          ConfigWishes       { get; }

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

            public TestEntities(int bits) : this(x => x.WithBits(bits)) 
            { }
            
            private TestEntities(Action<SynthWishes> initialize = null) 
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